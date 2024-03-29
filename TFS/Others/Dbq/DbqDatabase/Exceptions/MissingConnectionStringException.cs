using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbqDatabase.Exceptions {
    public class MissingConnectionStringException : ApplicationException {
        public MissingConnectionStringException()
            : base() { }
        public MissingConnectionStringException(string message)
            : base(message) { }
        public MissingConnectionStringException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
