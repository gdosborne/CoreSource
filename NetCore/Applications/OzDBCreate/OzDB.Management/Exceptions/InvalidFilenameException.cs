using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzDB.Management.Exceptions {
    public class InvalidFilenameException : OzDBException {
        public InvalidFilenameException(string message, Exception innerException, string invalidFilname) 
            : base(message, innerException) {
            InvalidFilename = invalidFilname;
        }

        public string InvalidFilename { get; private set; }
    }
}
