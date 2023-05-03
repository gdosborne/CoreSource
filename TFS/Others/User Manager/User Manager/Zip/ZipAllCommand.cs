//-------------------------------------------------------------------
// (©) 2015 Statistics & Control Inc.  All rights reserved.
// Created by:	Rex Gray
//-------------------------------------------------------------------
//
// Create and write an encrypted, password-protected zip file containing multiple files.
//
#region Using Directives (.NET)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
#endregion

#region Using Directives (SNC)
#endregion

namespace SNC.OptiRamp.Services.fSncZip
{
    class ZipAllCommand : ZipCommand
    {
        #region Private Members
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

        #region Events
        #endregion

        #region Public Methods
        public override void Execute(Dictionary<string, object> parameters)
        {
            try
            {
                byte[] buffer = new byte[4096];

                string leftShift = null;
                if (parameters.ContainsKey("LeftShift"))
                {
                    leftShift = (string)parameters["LeftShift"];
                }

                IEnumerable<string> fileNames_ = (IEnumerable<string>)parameters["FileNames"];
                List<string> fileNames = new List<string>(fileNames_);

                string zipFileName = (string)parameters["ZipFileName"];
                bool hideFileName = (bool)parameters["HideFileName"];

                bool initController = false;
                if (parameters.ContainsKey("InitController"))
                    initController = (bool)parameters["InitController"];

                // Prepare zip file manifest data.
                if (initController)
                {
                    foreach (string fileName in fileNames)
                    {
                        // Add file to File list, hiding name as needed.
                        var fInfo = new FileInfo(fileName);
                        AddFileToManifest(fInfo, hideFileName);
                    }
                    // Add this file last so that it is not part of the manifest list.
                    fileNames.Insert(0, CONTROLLER_FILE_NAME);
                }

                using (var zipStream = new ZipOutputStream(new FileStream(zipFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite)))
                {
                    zipStream.SetLevel(3); // Compression level can be from 0 to 9.
                    zipStream.UseZip64 = UseZip64.Off;
                    // Encryption is enabled ONLY when there is a non-null, non-empty password AND a non-zero AESKeySize.
                    zipStream.Password = leftShift;

                    foreach (string fileName in fileNames)
                    {
                        // Create zip entry describing the file to be added to the zip file.
                        string entryName = System.IO.Path.GetFileName(fileName);
                        if (initController && !fileName.Equals(CONTROLLER_FILE_NAME))
                        {
                            entryName = (from f in Files
                                         where f.OriginalSource.Equals(fileName)
                                         select f.EntryName).FirstOrDefault();
                            if (entryName == null)
                                continue;
                        }
                        FileInfo fi = new FileInfo(fileName);
                        ZipEntry zipEntry = new ZipEntry(entryName);
                        zipEntry.DateTime = fi.LastWriteTime;
                        zipEntry.AESKeySize = string.IsNullOrEmpty(leftShift) ? 0 : 256;
                        zipStream.PutNextEntry(zipEntry);

                        // Create Controller.xml, if necessary.
                        if (initController && fileName.Equals(CONTROLLER_FILE_NAME))
                        {
                            XDocument doc = new XDocument();
                            var root = new XElement("files");
                            doc.Add(root);
                            AddFilesToController(doc);

                            using (var controllerData = new MemoryStream())
                            {
                                doc.Save(controllerData);
                                controllerData.Seek(0, SeekOrigin.Begin);

                                StreamUtils.Copy(controllerData, zipStream, buffer);
                            }
                            continue;
                        }

                        // Stream the contents of the file to the zip file.
                        using (FileStream contentFileStream = File.OpenRead(fileName))
                        {
                            StreamUtils.Copy(contentFileStream, zipStream, buffer);
                        }
                    }

                    zipStream.IsStreamOwner = true; // Causes Close to also Close the underlying file stream.
                    zipStream.Finish();
                    zipStream.Close();
                }

                Result = CommandResults.Success;
                ZipCommandComplete(this);
            }
            catch (Exception ex)
            {
                Result = CommandResults.Failure;
                Value = ex.Message;
            }
        }
        #endregion

        #region Private Methods
        private void AddFileToManifest(FileInfo fInfo, bool hideFileName)
        {
            var g = Guid.NewGuid();
            var entryName = hideFileName ? g.ToString() : fInfo.Name;
            Files.Add(new FileData
            {
                OriginalSource = fInfo.FullName,
                Guid = g,
                EntryName = entryName
            });
            ChangeStatus(this, string.Format("File added to package - {0}.", fInfo.Name));
        }
        #endregion
    }
}
