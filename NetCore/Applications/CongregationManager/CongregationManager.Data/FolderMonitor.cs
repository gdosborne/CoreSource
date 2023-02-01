using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Threading;

namespace CongregationManager.Data {
    public class FolderMonitor : IDisposable {
        public FolderMonitor(string path) 
            : this(path, "*.*") { }

        public FolderMonitor(string path, string filespec)
            : this(path, filespec, TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(500)) { }

        public FolderMonitor(string path, string filespec, TimeSpan startInterval, TimeSpan subsequentInterval) {
            Path = path;
            Filespec = filespec;
            subInterval = subsequentInterval;

            if (!Directory.Exists(Path)) {
                throw new DirectoryNotFoundException(Path);
            }
            files = new List<FileInfo>();
            dInfo = new DirectoryInfo(Path);
            dt = new DispatcherTimer {
                Interval = startInterval
            };
            dt.Tick += Dt_Tick;
            dt.Start();
        }

        private TimeSpan subInterval = default;
        private void Dt_Tick(object? sender, EventArgs e) {
            dt.Stop();
            var temp = dInfo.GetFiles(Filespec).ToList();
            var newFiles = new List<FileInfo>();
            var removedFiles = new List<FileInfo>();
            var changedFiles = new List<FileInfo>();
            if (temp.Any()) {
                temp.ForEach(x => {
                    if (!files.Any(y => y.Name == x.Name)) {
                        newFiles.Add(x);
                    }
                });
            }
            files.ForEach(x => {
                if (!temp.Any(y => y.Name == x.Name)) {
                    removedFiles.Add(x);
                }
                else if (temp.Any(y => y.Name == x.Name)) {
                    var orig = files.First(y => y.Name == x.Name);
                    var changed = temp.First(y => y.Name == x.Name);
                    if (orig.LastWriteTime != changed.LastWriteTime) {
                        changedFiles.Add(changed);
                    }
                }
            });
            if (changedFiles.Any() || removedFiles.Any() || newFiles.Any()) {
                var ea = new FilesUpdatedEventArgs(newFiles, removedFiles, changedFiles);
                FilesUpdated?.Invoke(this, ea);

            }

            files = temp;
            if (dt.Interval != subInterval)
                dt.Interval = subInterval;
            dt.Start();
        }

        public event FilesUpdatedHandler FilesUpdated;

        private List<FileInfo> files = default;
        private DispatcherTimer dt = default;
        private bool isDisposed;

        public string Filespec { get; set; } = "*.*";
        public string Path { get; private set; }
        private DirectoryInfo dInfo { get; set; }

        protected virtual void Dispose(bool isDisposing) {
            if (!isDisposed) {
                if (isDisposing) {
                    dt.Stop();
                    dt = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                isDisposed = true;
            }
        }

        public void Dispose() {
            Dispose(isDisposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
