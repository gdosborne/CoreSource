using System;
using System.Runtime.Serialization;

namespace OzDB.Management.Exceptions {
    public class TableExistsException : OzDBException {
        public TableExistsException(string message, Exception innerException)
           : base(message, innerException) { }        
    }
}
