using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongregationManager.Data {
    public delegate void FileChangedDetectedHandler(object sender, FileChangeDetectedEventArgs e);
    
    public enum ChangeTypes {
        Add,
        Remove,
        Change
    }

    public enum FileTypes {
        Data,
        Extension
    }

    public class FileChangeDetectedEventArgs {
        public FileChangeDetectedEventArgs(string filename, ChangeTypes changeType, FileTypes fileType) {
            Filename = filename;
            ChangeType = changeType;
            FileType = fileType;
        }

        public string Filename { get; private set; }
        public ChangeTypes ChangeType { get; private set; }
        public FileTypes FileType { get; private set; }
    }
}
