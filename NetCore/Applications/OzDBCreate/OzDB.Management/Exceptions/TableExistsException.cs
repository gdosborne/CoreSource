using System;
using System.Runtime.Serialization;

namespace OzDB.Management.Exceptions {
    public class TableExistsException : ApplicationException {
        public TableExistsException(string message)
           : base(message) { }
        public TableExistsException(string message, Exception innerException)
            : base(message, innerException) { }
        public TableExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
