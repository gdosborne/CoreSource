using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GregOsborne.ApplicationData.Exceptions.ContinuousDeliverer {
    public class DatabaseTemplateFileMissingException : ApplicationException {
        public DatabaseTemplateFileMissingException()
            : base() { }

        public DatabaseTemplateFileMissingException(string message)
            : base(message) { }

        public DatabaseTemplateFileMissingException(string message, Exception innerException)
            : base(message, innerException) { }
        public DatabaseTemplateFileMissingException(SerializationInfo info, StreamingContext context)
         : base(info, context) { }
    }
}
