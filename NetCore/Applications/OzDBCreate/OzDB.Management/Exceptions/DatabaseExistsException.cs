using System;
using System.Runtime.Serialization;

namespace OzDB.Management.Exceptions {
    public class DatabaseExistsException : OzDBException {
        public DatabaseExistsException(string message, Exception ex)
           : base(message, ex) { }

    }
}
