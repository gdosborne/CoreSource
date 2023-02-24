using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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

            if (!Directory.Exists(Path)) 
                throw new DirectoryNotFoundException(Path);
            
            files = new List<FileStatus>();
            dInfo = new DirectoryInfo(Path);
            areTimeSpansMatching = startInterval.TotalMilliseconds == subsequentInterval.TotalMilliseconds;
            dt = new DispatcherTimer {
                Interval = startInterval
            };
            dt.Tick += Dt_Tick;
            dt.Start();
        }

        private class FileStatus {
            public string FullName;
            public bool IsNew;
            public bool IsDeleted;
            public bool IsModified;
            public DateTime LastWrite;
            public FileInfo FileInfo => new(FullName);
        }

        private readonly bool areTimeSpansMatching = false;
        private readonly TimeSpan subInterval = default;
        private List<FileStatus> files = default;

        private void Dt_Tick(object? sender, EventArgs e) {
            dt.Stop();
            var currentFiles = dInfo.GetFiles(Filespec).ToList();

            currentFiles.ForEach(current => {
                var earlierStatus = files.FirstOrDefault(x => x.FullName.Equals(current.FullName, StringComparison.OrdinalIgnoreCase));
                if (earlierStatus == null) {
                    files.Add(new FileStatus { FullName = current.FullName, IsNew = true, LastWrite = current.LastWriteTime });
                    return;
                }
                earlierStatus.IsNew = false;
                earlierStatus.IsModified = earlierStatus.LastWrite != current.LastWriteTime;
            });
            files.ForEach(earlierStatus => {
                if (!currentFiles.Any(x => x.FullName.Equals(earlierStatus.FullName, StringComparison.OrdinalIgnoreCase))) {
                    earlierStatus.IsDeleted = true;
                }
            });
            if (files.Any(x => x.IsNew || x.IsDeleted || x.IsModified)) {
                var changedFiles = new List<FileInfo>();
                var removedFiles = new List<FileInfo>();
                var newFiles = new List<FileInfo>();
                files.ForEach(file => {
                    if (file.IsNew)
                        newFiles.Add(file.FileInfo);
                    else if (file.IsDeleted)
                        removedFiles.Add(file.FileInfo);
                    else if (file.IsModified)
                        changedFiles.Add(file.FileInfo);
                });
                var ea = new FilesUpdatedEventArgs(newFiles, removedFiles, changedFiles);
                FilesUpdated?.Invoke(this, ea);
            }
            files = currentFiles.Select(x => new FileStatus {
                FullName = x.FullName,
                LastWrite = x.LastWriteTime,
                IsNew = false,
                IsDeleted = false,
                IsModified = false
            }).ToList();

            if (!areTimeSpansMatching && dt.Interval != subInterval)
                dt.Interval = subInterval;
            dt.Start();
        }

        public event FilesUpdatedHandler FilesUpdated;

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
