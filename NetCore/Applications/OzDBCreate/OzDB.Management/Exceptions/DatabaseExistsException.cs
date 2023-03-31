using System;
using System.Runtime.Serialization;

namespace OzDB.Management.Exceptions {
    public class DatabaseExistsException : ApplicationException {
        public DatabaseExistsException(string message)
           : base(message) { }
        public DatabaseExistsException(string message, Exception innerException)
            : base(message, innerException) { }
        public DatabaseExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

    }
}
