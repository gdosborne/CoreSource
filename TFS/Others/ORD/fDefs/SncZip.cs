//-------------------------------------------------------------------
// (©) 2015 Statistics & Control Inc.  All rights reserved.
// Created by:	Rex Gray
//-------------------------------------------------------------------
//
// Zip file processing.
//
#region Using Directives (.NET)
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
#endregion

#region Using Directives (SNC)
#endregion

namespace SNC.OptiRamp.Services.fSncZip
{
    public interface ISncZip
    {
        /// <summary>
        /// Zip a list of files.
        /// </summary>
        /// <param name="fileNames">The list of files to be zipped.</param>
        /// <param name="fullPathZipFileName">The name of the zip file to be created.</param>
        /// <param name="error">An error message.</param>
        /// <param name="hideFileNames">True indicates that file names should be hidden in the zip manifest.</param>
        void ZipFiles(IEnumerable<string> fileNames, string fullPathZipFileName, out string error, bool hideFileNames = false);

        /// <summary>
        /// Unzip a zip file.
        /// </summary>
        /// <param name="fullPathZipFileName">The name of the file to be unzipped.</param>
        /// <param name="result">A result message.</param>
        void UnzipFiles(string fullPathZipFileName, out string result);

        /// <summary>
        /// Zip a list of files with encryption.
        /// </summary>
        /// <param name="fileNames">The list of files to be zipped.</param>
        /// <param name="fullPathZipFileName">The name of the zip file to be created.</param>
        /// <param name="password">The encryption password.</param>
        /// <param name="error">An error message.</param>
        /// <param name="hideFileNames">True indicates that file names should be hidden in the zip manifest.</param>
        void ZipFilesWithEncryption(IEnumerable<string> fileNames, string fullPathZipFileName, string password, out string error, bool hideFileNames = false);

        /// <summary>
        /// Unzip an encrypted zip file.
        /// </summary>
        /// <param name="fullPathZipFileName">The name of the file to be unzipped.</param>
        /// <param name="password">The encryption password.</param>
        /// <param name="result">A result message.</param>
        void UnzipEncryptedFiles(string fullPathZipFileName, string password, out string result);

        /// <summary>
        /// Create an encrypted, password-protected zip file and stream SNCUser data to it. The content file name and
        /// password are fixed.
        /// </summary>
        /// <param name="fullPathSncZipFileName">The name of the snc zip file to be created.</param>
        /// <param name="data">The stream of content to be zipped.</param>
        /// <param name="error">An error message.</param>
        void ZipSncFile(string fullPathSncZipFileName, Stream data, out string error);

        /// <summary>
        /// Open and read the encrypted, password-protected, SNCUser zip file. The content file name and
        /// password are fixed.
        /// </summary>
        /// <param name="fullPathSncZipFileName">The name of the snc zip file to be unzipped.</param>
        /// <param name="result">A result message.</param>
        /// <returns>A Stream object.</returns>
        Stream UnzipSncFile(string fullPathSncZipFileName, out string result);

        /// <summary>
        /// Read a text file and zip it into a stream for storage as a BLOB in a database.
        /// </summary>
        /// <param name="fullPathFileName">The name of the file to be streamed into a BLOB.</param>
        /// <param name="error">An error message.</param>
        /// <returns>A Stream object that will be stored in a database as a BLOB.</returns>
        MemoryStream ZipFileToBlob(string fullPathFileName, out string error);

        /// <summary>
        /// Unzip a BLOB stream read from a database.
        /// </summary>
        /// <param name="fullPathFileName">The name of the file created to hold the BLOB content.</param>
        /// <param name="data">The BLOB to be unzipped.</param>
        /// <param name="error">An error message.</param>
        void UnzipFileFromBlob(string fullPathFileName, MemoryStream data, out string error);

        /// <summary>
        /// Zip a MemoryStream for storage as a BLOB in a database.
        /// </summary>
        /// <param name="zipEntryName">An internal file name corresponding to the BLOB.</param>
        /// <param name="blob">The MemoryStream to be zipped.</param>
        /// <param name="error">An error message.</param>
        /// <returns>A MemoryStream that can be stored as a BLOB in a database.</returns>
        MemoryStream ZipBlob(string zipEntryName, MemoryStream blob, out string error);

        /// <summary>
        /// Unzip a database BLOB into a MemoryStream.
        /// </summary>
        /// <param name="blob">A BLOB read from a database.</param>
        /// <param name="error">An error message.</param>
        /// <returns>The MemoryStream unzipped from the BLOB.</returns>
        MemoryStream UnzipBlob(MemoryStream blob, out string error);

        /// <summary>
        /// Read suffix bytes from the end of a zip file.
        /// </summary>
        /// <param name="fullPathFileName">Name of the zip file.</param>
        /// <param name="error">An error message.</param>
        /// <returns>An SncVersion object.</returns>
        SncVersion ReadFileSuffix(string fullPathFileName, out string error);
    }

    /// <summary>
    /// A representation of version information that can be added to the end of zip files.
    /// </summary>
    public struct SncVersion
    {
        #region Public Members
        public byte[] data;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="year">The last two digits of the year on the date the assembly was built.</param>
        /// <param name="month">The month on the date the assembly was built.</param>
        /// <param name="day">The day on the date the assembly was built.</param>
        /// <param name="build">The build count on the date the assembly was built.</param>
        public SncVersion(byte year, byte month, byte day, byte build)
        {
            data = new byte[8];
            data[0] = 0x53;  // 'S'tatistics
            data[1] = 0x6E;  // a'n'd
            data[2] = 0x43;  // 'C'ontrol
            data[3] = year;  // Year from Assembly Version
            data[4] = month; // Month from Assembly Version
            data[5] = day;   // Day from Assembly Version
            data[6] = build; // Build from Assembly Version
            data[7] = 0;     // Zero
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data_">The bytes read from the end of a zip file.</param>
        public SncVersion(byte[] data_)
        {
            data = new byte[8];
            data[0] = data_[0]; // 'S'tatistics
            data[1] = data_[1]; // a'n'd
            data[2] = data_[2]; // 'C'ontrol
            data[3] = data_[3]; // Year from Assembly Version
            data[4] = data_[4]; // Month from Assembly Version
            data[5] = data_[5]; // Day from Assembly Version
            data[6] = data_[6]; // Build from Assembly Version
            data[7] = data_[7]; // Zero
        }
        #endregion
    }
}
