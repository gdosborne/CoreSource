using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GregOsborne.ApplicationData.Exceptions.ContinuousDeliverer {
    public class DataDirectoryMissingException : ApplicationException {
        public DataDirectoryMissingException()
            : base() { }

        public DataDirectoryMissingException(string message)
            : base(message) { }

        public DataDirectoryMissingException(string message, Exception innerException)
            : base(message, innerException) { }

        public DataDirectoryMissingException(SerializationInfo info, StreamingContext context)
         : base(info, context) { }

        public DataDirectoryMissingException(string message, string dataDirectory)
            : base(message) {
            DataDirectory = dataDirectory;
        }

        public DataDirectoryMissingException(string message, string dataDirectory, Exception innerException)
            : base(message, innerException) {
            DataDirectory = dataDirectory;
        }


        public string DataDirectory { get; private set; }
    }
}
