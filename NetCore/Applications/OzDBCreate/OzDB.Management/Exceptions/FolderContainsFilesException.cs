using System;
using System.Runtime.Serialization;

namespace OzDB.Management.Exceptions {
    public class FolderContainsFilesException : ApplicationException {
        public FolderContainsFilesException(string message, string folder)
            : base(message) { Folder = folder; }
        public FolderContainsFilesException(string message, Exception innerException, string folder)
            : base(message, innerException) { Folder = folder; }
        public FolderContainsFilesException(SerializationInfo info, StreamingContext context, string folder)
            : base(info, context) { Folder = folder; }

        public string Folder { get; private set; }

    }
}
