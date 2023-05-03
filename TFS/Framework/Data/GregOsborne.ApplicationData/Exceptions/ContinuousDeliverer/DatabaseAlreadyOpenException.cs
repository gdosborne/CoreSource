using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GregOsborne.ApplicationData.Exceptions.ContinuousDeliverer {
    public class DatabaseAlreadyOpenException : ApplicationException {
        public DatabaseAlreadyOpenException()
            : base() { }

        public DatabaseAlreadyOpenException(string message)
            : base(message) { }

        public DatabaseAlreadyOpenException(string message, Exception innerException)
            : base(message, innerException) { }
        public DatabaseAlreadyOpenException(SerializationInfo info, StreamingContext context)
         : base(info, context) { }
    }
}
