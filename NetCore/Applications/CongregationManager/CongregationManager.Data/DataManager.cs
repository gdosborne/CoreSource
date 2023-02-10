using Common.Applicationn.Linq;
using Common.Applicationn.Primitives;
using Common.Applicationn.Security;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Media;

namespace CongregationManager.Data {
    public class DataManager : IDisposable {
        public DataManager(string dataFolder, string extensionsFolder, SecureString password,
                ResourceDictionary resources) {
            this.password = password;
            DataFolder = dataFolder;
            Congregations = new ObservableCollection<Congregation>();
            dataFolderMonitor = new FolderMonitor(dataFolder, "*.congregation");
            extensionsFolderMonitor = new FolderMonitor(extensionsFolder, "*.dll");
            dataFolderMonitor.FilesUpdated += DataFolderMonitor_FilesUpdated;
            extensionsFolderMonitor.FilesUpdated += ExtensionsFolderMonitor_FilesUpdated;
            Resources = resources;
        }

        private void ExtensionsFolderMonitor_FilesUpdated(object sender, FilesUpdatedEventArgs e) {
            if (extensionsFolderMonitor != null) {
                if (e.FilesRemoved.Any()) {
                    e.FilesRemoved.ForEach(x => {
                        MakeExtensionChange(x.FullName, ChangeTypes.Remove);
                    });
                }
                if (e.FilesChanged.Any()) {
                    e.FilesChanged.ForEach(x => {
                        MakeExtensionChange(x.FullName, ChangeTypes.Change);
                    });
                }
                if (e.FilesAdded.Any()) {
                    e.FilesAdded.ForEach(x => {
                        MakeExtensionChange(x.FullName, ChangeTypes.Add);
                    });
                }
            }
        }

        public event FileChangedDetectedHandler FileChangedDetected;

        private void MakeExtensionChange(string filename, ChangeTypes changeType) =>
            FileChangedDetected?.Invoke(this, new FileChangeDetectedEventArgs(filename, changeType, FileTypes.Extension));

        private void DataFolderMonitor_FilesUpdated(object sender, FilesUpdatedEventArgs e) {
            if (dataFolderMonitor != null) {
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
                        var cong = Congregation.OpenFromFile(x.FullName, password.ToStandardString());
                        cong.Members = cong.Members.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();

                        cong.Members.ToList().ForEach(x => {
                            x.Resources = Resources;
                        });
                        cong.Filename = x.Name;
                        cong.SaveThisItem += Cong_SaveThisItem;
                        //cong.EditThisItem += Cong_EditThisItem;
                        cong.Original = cong.Clone().As<Congregation>();
                        Congregations.Add(cong);
                    });
                }
            }
        }

        public ResourceDictionary Resources { get; private set; }

        private void Cong_SaveThisItem(object? sender, EventArgs e) {
            var congregation = sender.As<Congregation>();
            try {
                SaveCongregation(congregation);
                congregation.IsNew = false;
                congregation.Original = congregation.Clone().As<Congregation>();
            }
            catch (Exception ex) {

            }
        }

        private FolderMonitor dataFolderMonitor { get; set; }
        private FolderMonitor extensionsFolderMonitor { get; set; }

        private SecureString password = default;
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
            if (File.Exists(cong.Filename)) {
                File.Delete(cong.Filename);
            }
        }

        public void SaveCongregation(Congregation cong) {
            cong.DataPath = DataFolder;
            var isNewCong = cong.ID == 0;
            if (cong != null) {
                if (isNewCong)
                    cong.ID = !Congregations.Any() ? 1 : Congregations.Max(x => x.ID) + 1;
                cong.Save(password.ToStandardString());                
            }
            GC.Collect();
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
                var cong = Congregation.OpenFromFile(file.FullName, password.ToStandardString());
                cong.Filename = file.Name;
                Congregations.Add(cong);
            }
            GC.Collect();
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
