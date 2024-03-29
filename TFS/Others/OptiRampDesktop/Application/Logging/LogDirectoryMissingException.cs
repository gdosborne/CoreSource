using System;

namespace MyApplication.Logging
{
	[Serializable]
	public class LogDirectoryMissingException : Exception
	{
		#region Public Constructors

		public LogDirectoryMissingException()
			: base() { }

		public LogDirectoryMissingException(string message)
			: base(message) { }

		public LogDirectoryMissingException(string message, Exception innerException)
			: base(message, innerException) { }

		#endregion
	}
}