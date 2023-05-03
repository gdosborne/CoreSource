//-------------------------------------------------------------------
// (©) 2015 Statistics & Control Inc.  All rights reserved.
// Created by:	Rex Gray
//-------------------------------------------------------------------
//
// Open and read an encrypted, password-protected zip file.
//
#region Using Directives (.NET)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
#endregion

#region Using Directives (SNC)
#endregion

namespace SNC.OptiRamp.Services.fSncZip
{
    class ReadSncZipFileCommand : ZipCommand
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
                string zipFileName = (string)parameters["ZipFileName"];
                string leftShift = (string)parameters["LeftShift"];

                MemoryStream outStream = new MemoryStream();

                using (var zipFileStream = File.OpenRead(zipFileName))
                {
                    ZipFile zipFile = new ZipFile(zipFileStream);
                    zipFile.UseZip64 = UseZip64.Off;
                    zipFile.Password = leftShift;

                    foreach (ZipEntry zipEntry in zipFile)
                    {
                        if (!zipEntry.IsFile)
                            continue;

                        var zipStream = zipFile.GetInputStream(zipEntry);
                        StreamUtils.Copy(zipStream, outStream, buffer);
                    }
                    zipFile.Close();

                    outStream.Seek(0, SeekOrigin.Begin);
                }

                Value = outStream;
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
        #endregion
    }
}
