namespace GregOsborne.Application.Exception {
	using System;
	using System.Runtime.Serialization;

	public class IgnoredException : ApplicationException {
		public IgnoredException(string message) : base(message) { }
		public IgnoredException(string message, System.Exception innerException) : base(message, innerException) { }
		public IgnoredException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
