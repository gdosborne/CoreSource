using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OzDB.Management.Exceptions {
    public abstract class OzDBException : ApplicationException {
        private OzDBException(string message)
           : base(message) { }
        public OzDBException(string message, Exception innerException)
            : base(message, innerException) { }
        private OzDBException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
