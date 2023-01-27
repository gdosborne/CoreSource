using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongregationManager.Data {
    public delegate void FilesUpdatedHandler(object sender, FilesUpdatedEventArgs e);
    public class FilesUpdatedEventArgs {
        public FilesUpdatedEventArgs(
                List<FileInfo> filesAdded, 
                List<FileInfo> filesRemoved, 
                List<FileInfo> filesChanged) {
            FilesAdded = filesAdded;
            FilesRemoved = filesRemoved;
            FilesChanged = filesChanged;
        }

        public List<FileInfo> FilesAdded { get; private set; }
        public List<FileInfo> FilesRemoved { get; private set; }
        public List<FileInfo> FilesChanged { get; private set; }
    }
}
