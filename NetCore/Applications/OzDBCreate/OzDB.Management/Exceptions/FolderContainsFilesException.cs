using System;
using System.Runtime.Serialization;

namespace OzDB.Management.Exceptions {
    public class FolderContainsFilesException : OzDBException {
        public FolderContainsFilesException(string message, Exception innerException, string folder)
            : base(message, innerException) { 
            Folder = folder; 
        }

        public string Folder { get; private set; }

    }
}
