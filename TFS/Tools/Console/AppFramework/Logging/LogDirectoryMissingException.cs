﻿namespace GregOsborne.Application.Logging {
	using System;

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