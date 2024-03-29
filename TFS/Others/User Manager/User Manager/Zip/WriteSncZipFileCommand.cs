//-------------------------------------------------------------------
// (©) 2015 Statistics & Control Inc.  All rights reserved.
// Created by:	Rex Gray
//-------------------------------------------------------------------
//
// Create and write an encrypted, password-protected zip file.
//
#region Using Directives (.NET)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
#endregion

#region Using Directives (SNC)
#endregion

namespace SNC.OptiRamp.Services.fSncZip
{
    public class WriteSncZipFileCommand : ZipCommand
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
                string contentFileName = (string)parameters["ContentFileName"];
                string leftShift = (string)parameters["LeftShift"];
                Stream inStream = (Stream)parameters["Data"];
                inStream.Seek(0, SeekOrigin.Begin);

                using (var outStream = new ZipOutputStream(new FileStream(zipFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite)))
                {
                    outStream.SetLevel(9);
                    outStream.UseZip64 = UseZip64.Off;
                    outStream.Password = leftShift;

                    ZipEntry zipEntry = new ZipEntry(contentFileName);
                    zipEntry.DateTime = DateTime.Now.ToLocalTime();
                    // Encryption is enabled ONLY when there is a password.
                    zipEntry.AESKeySize = string.IsNullOrEmpty(leftShift) ? 0 : 256;
                    outStream.PutNextEntry(zipEntry);

                    StreamUtils.Copy(inStream, outStream, buffer);

                    outStream.Finish();
                    outStream.Close();
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
        #endregion
    }
}
