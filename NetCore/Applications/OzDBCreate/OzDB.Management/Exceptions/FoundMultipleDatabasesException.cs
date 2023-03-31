using System;
using System.Runtime.Serialization;

namespace OzDB.Management.Exceptions {
    public class FoundMultipleDatabasesException : OzDBException {
        public FoundMultipleDatabasesException(string message, Exception innerException)
           : base(message, innerException) { }
        
    }
}
