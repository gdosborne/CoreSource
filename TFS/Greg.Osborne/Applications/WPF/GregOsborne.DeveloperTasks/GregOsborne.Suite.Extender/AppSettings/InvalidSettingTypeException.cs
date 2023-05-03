using System;
using System.Runtime.Serialization;

namespace GregOsborne.Suite.Extender.AppSettings {
	/// <summary>Invalid setting type exception</summary>
	public class InvalidSettingTypeException : ApplicationException {
		/// <summary>Initializes a new instance of the <see cref="InvalidSettingTypeException" /> class.</summary>
		public InvalidSettingTypeException()
			: base() { }

		/// <summary>Initializes a new instance of the <see cref="InvalidSettingTypeException" /> class.</summary>
		/// <param name="message">A message that describes the error.</param>
		public InvalidSettingTypeException(string message)
			: base(message) { }

		/// <summary>Initializes a new instance of the <see cref="InvalidSettingTypeException" /> class.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">
		/// The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a <span class="keyword">catch</span> block that handles the inner exception.
		/// </param>
		public InvalidSettingTypeException(string message, Exception innerException)
			: base(message, innerException) { }

		/// <summary>Initializes a new instance of the <see cref="InvalidSettingTypeException" /> class.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		public InvalidSettingTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }
	}
}
