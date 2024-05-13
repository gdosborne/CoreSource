/* File="LogDirectoryMissingException"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;

namespace OzFramework.Logging {
    [Serializable]
    public class LogDirectoryMissingException : System.Exception {
        #region Public Constructors

        public LogDirectoryMissingException() { }

        public LogDirectoryMissingException(string message)
            : base(message) { }

        public LogDirectoryMissingException(string message, System.Exception innerException)
            : base(message, innerException) { }

        #endregion Public Constructors
    }
}
