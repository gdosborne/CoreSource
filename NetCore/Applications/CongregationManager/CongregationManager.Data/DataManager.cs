using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Security;

namespace CongregationManager.Data {
    public class DataManager : IDisposable {
        public DataManager(string dataFolder, string password) {
            this.password = password;
            DataFolder = dataFolder;
            Congregations = new ObservableCollection<Congregation>();
            folderMonitor = new FolderMonitor(dataFolder, "*.congregation");
            folderMonitor.FilesUpdated += FolderMonitor_FilesUpdated;
        }

        private void FolderMonitor_FilesUpdated(object sender, FilesUpdatedEventArgs e) {
            if (folderMonitor != null) {
                if (e.FilesRemoved.Any()) {
                    e.FilesRemoved.ForEach(x => {
                        var cName = x.ShortenedName();
                        var item = Congregations.FirstOrDefault(y => y.Name.Equals(cName, StringComparison.OrdinalIgnoreCase));
                        if (item != null) {
                            Congregations.Remove(item);
                        }
                    });
                }
                if (e.FilesChanged.Any()) {
                    e.FilesChanged.ForEach(x => {
                        var cName = x.ShortenedName();
                        var item = Congregations.FirstOrDefault(y => y.Name.Equals(cName, StringComparison.OrdinalIgnoreCase));
                        if (item != null) {
                            Refresh(item);
                        }
                    });
                }
                if (e.FilesAdded.Any()) {
                    e.FilesAdded.ForEach(x => {
                        var item = Congregations.FirstOrDefault(y => y.Name.Equals(x.Name, StringComparison.OrdinalIgnoreCase));
                        if (item != null) {
                            throw new ApplicationException($"Congregation {item.Name} already exists");
                        }
                        var cName = x.ShortenedName();
                        Congregations.Add(new Congregation {
                            Name = cName,
                            Filename = x.FullName
                        });
                    });
                }
            }
        }

        private FolderMonitor folderMonitor { get; set; }
        private string password = default;
        private string _DataFolder = default;
        public string DataFolder {
            get => _DataFolder;
            private set {
                _DataFolder = value;
                Refresh();
            }
        }

        #region Congregations Property
        private ObservableCollection<Congregation> _Congregations = default;
        private bool isDisposed;

        /// <summary>Gets/sets the Congregations.</summary>
        /// <value>The Congregations.</value>
        public ObservableCollection<Congregation> Congregations {
            get => _Congregations;
            set {
                _Congregations = value;
            }
        }
        #endregion

        internal void Delete(Congregation cong) {
            if(File.Exists(cong.Filename)) {
                File.Delete(cong.Filename);
            }
        }

        public void SaveCongregation(Congregation cong) {
            cong.DataPath = DataFolder;
            if (cong != null) {
                if (cong.ID == 0)
                    cong.ID = !Congregations.Any() ? 1 : Congregations.Max(x => x.ID) + 1;
                cong.Save(password);
            }
        }

        public void Refresh(Congregation cong) {
            Refresh();
        }

        public void Refresh() {
            if (Congregations != null)
                Congregations.Clear();
            else
                Congregations = new ObservableCollection<Congregation>();
            var dir = new DirectoryInfo(DataFolder);
            if (!dir.Exists)
                return;
            var files = dir.GetFiles("*.congregation");
            foreach (var file in files) {
                var cong = Congregation.OpenFromFile(file.FullName, password);
                Congregations.Add(cong);
            }
        }

        protected virtual void Dispose(bool isDisposing) {
            if (!isDisposed) {
                if (isDisposing) {
                    if (Congregations != null) {
                        Congregations.Clear();
                        Congregations = null;
                    }
                }
                isDisposed = true;
            }
        }
        public void Dispose() {
            Dispose(isDisposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
