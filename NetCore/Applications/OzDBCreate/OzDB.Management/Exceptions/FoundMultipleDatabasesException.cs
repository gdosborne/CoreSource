using System;
using System.Runtime.Serialization;

namespace OzDB.Management.Exceptions {
    public class FoundMultipleDatabasesException : ApplicationException {
        public FoundMultipleDatabasesException(string message)
           : base(message) { }
        public FoundMultipleDatabasesException(string message, Exception innerException)
            : base(message, innerException) { }
        public FoundMultipleDatabasesException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public string Folder { get; private set; }
    }
}
