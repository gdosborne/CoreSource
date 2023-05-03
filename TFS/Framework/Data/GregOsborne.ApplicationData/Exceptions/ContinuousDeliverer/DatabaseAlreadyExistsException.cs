using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GregOsborne.ApplicationData.Exceptions.ContinuousDeliverer {
    public class DatabaseAlreadyExistsException : ApplicationException {
        public DatabaseAlreadyExistsException()
            : base() { }

        public DatabaseAlreadyExistsException(string message)
            : base(message) { }

        public DatabaseAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException) { }
        public DatabaseAlreadyExistsException(SerializationInfo info, StreamingContext context)
         : base(info, context) { }
    }
}
