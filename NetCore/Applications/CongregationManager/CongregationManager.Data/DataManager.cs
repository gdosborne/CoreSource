using Common.Application.Primitives;
using Common.Application.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Windows;

namespace CongregationManager.Data {
    public class DataManager : IDisposable {
        public DataManager(string dataFolder, string extensionsFolder, string recycleFolder,
                SecureString password, ResourceDictionary resources) {
            this.password = password;
            DataFolder = dataFolder;
            RecycleFolder = recycleFolder;

            Congregations = new ObservableCollection<Congregation>();

            extensionsFolderMonitor = new FolderMonitor(extensionsFolder, "*.dll");
            extensionsFolderMonitor.FilesUpdated += ExtensionsFolderMonitor_FilesUpdated;

            //congregations must be loaded first as all other
            // items will connection an existing congregation
            congregationFolderMonitor = new FolderMonitor(dataFolder, "*.congregation");
            congregationFolderMonitor.FilesUpdated += CongregationFolderMonitor_FilesUpdated;

            Resources = resources;
        }

        public event ChangeNotificationHandler ChangeNotification;

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

        private void CongregationFolderMonitor_FilesUpdated(object sender, FilesUpdatedEventArgs e) {
            var fm = sender.As<FolderMonitor>();
            if (congregationFolderMonitor != null) {
                if (e.FilesRemoved.Any()) {
                    e.FilesRemoved.ForEach(x => {
                        var cName = x.ShortenedName();
                        var item = Congregations.FirstOrDefault(y => y.Name.Equals(cName, StringComparison.OrdinalIgnoreCase));
                        if (item != null) {
                            Congregations.Remove(item);
                            ChangeNotification?.Invoke(this, new ChangeNotificationEventArgs(item, ModificationTypes.Deleted));
                        }
                    });
                }
                if (e.FilesChanged.Any()) {
                    e.FilesChanged.ForEach(x => {
                        var cName = x.ShortenedName();
                        var item = Congregations.FirstOrDefault(y => y.Name.Equals(cName, StringComparison.OrdinalIgnoreCase));
                        if (item != null) {
                            Refresh(item);
                            ChangeNotification?.Invoke(this, new ChangeNotificationEventArgs(item, ModificationTypes.Modified));
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
                        cong.Original = cong.Clone().As<Congregation>();
                        Congregations.Add(cong);
                        ChangeNotification?.Invoke(this, new ChangeNotificationEventArgs(cong, ModificationTypes.Added));
                    });
                }
            }
        }

        public ResourceDictionary Resources { get; private set; }

        private void Cong_SaveThisItem(object? sender, EventArgs e) {
            var congregation = sender.As<Congregation>();
            try {
                var eaType = ModificationTypes.Modified;
                if (congregation.ID == 0)
                    eaType = ModificationTypes.Added;
                var ea = new ChangeNotificationEventArgs(congregation, eaType);
                ChangeNotification?.Invoke(this, ea);
                if (ea.Cancel)
                    return;
                SaveCongregation(congregation);
                congregation.IsNew = false;
                congregation.Original = congregation.Clone().As<Congregation>();
            }
            catch (Exception ex) { }
        }

        public IEnumerable<RecycleGroup> RecycleBinItems() {
            var dInfo = new DirectoryInfo(RecycleFolder);
            var groups = new List<RecycleGroup>();
            if (dInfo.Exists) {
                foreach (var d in dInfo.GetDirectories()) {
                    var originalFileName = d.Name.Substring(1);
                    var g = new RecycleGroup(originalFileName);
                    var files = d.GetFiles();
                    foreach (var file in files) {
                        var item = new RecycleItem {
                            RecycleFileName = file.FullName,
                            RecycleDateTime = file.LastWriteTime
                        };
                        g.Items.Add(item);
                    }
                    g.Items = new ObservableCollection<RecycleItem>(g.Items.OrderByDescending(x => x.RecycleDateTime));
                    groups.Add(g);
                }
            }
            return groups.OrderBy(x => x.Name);
        }

        private void RecycleCongregation(Congregation cong) {
            var newDir = Path.Combine(RecycleFolder, Path.GetFileName(cong.Filename));
            //var territoryDir = Path.Combine(DataFolder, $"{cong.Name}.Territories");

            if (!Directory.Exists(newDir)) {
                Directory.CreateDirectory(newDir);
            }

            var g = Guid.NewGuid();

            var recycleFile = Path.Combine(newDir, g.ToString());
            var fullFile = Path.Combine(DataFolder, cong.Filename);
            
            var file = new FileInfo(fullFile);
            file.LastWriteTime = DateTime.Now;
            file.MoveTo(recycleFile, true);

        }

        public bool DeleteCongregation(Congregation cong) {
            try {
                if (string.IsNullOrEmpty(cong.Filename))
                    return false;
                RecycleCongregation(cong);
                Congregations.Remove(cong);
                ChangeNotification?.Invoke(this, new ChangeNotificationEventArgs(cong, ModificationTypes.Deleted));
            }
            catch (Exception ex) {
                throw;
            }
            return true;
        }

        private FolderMonitor congregationFolderMonitor { get; set; }
        
        private FolderMonitor extensionsFolderMonitor { get; set; }
        
        private SecureString password { get; set; } = default;

        #region DataFolder Property
        private string _DataFolder = default;
        public string DataFolder {
            get => _DataFolder;
            private set {
                _DataFolder = value;
                Refresh();
            }
        }
        #endregion

        #region RecycleFolder Property
        private string _RecycleFolder = default;
        public string RecycleFolder {
            get => _RecycleFolder;
            set {
                _RecycleFolder = value;
            }
        }
        #endregion

        #region Congregations Property
        private ObservableCollection<Congregation> _Congregations = default;
        /// <summary>Gets/sets the Congregations.</summary>
        /// <value>The Congregations.</value>
        public ObservableCollection<Congregation> Congregations {
            get => _Congregations;
            set {
                _Congregations = value;
            }
        }
        #endregion

        public event CongregationChangedHandler CongregationChanged;

        #region CurrentCongregation Property
        private Congregation _CurrentCongregation = default;
        /// <summary>Gets/sets the CurrentCongregation.</summary>
        /// <value>The CurrentCongregation.</value>
        public Congregation CurrentCongregation {
            get => _CurrentCongregation;
            set {
                _CurrentCongregation = value;
                CongregationChanged?.Invoke(this, new CongregationChangedEventArgs(CurrentCongregation));
            }
        }
        #endregion

        public void SaveCongregation(Congregation cong) {
            cong.DataPath = DataFolder;
            var isNewCong = cong.ID == 0;
            if (cong != null) {
                if (isNewCong)
                    cong.ID = !Congregations.Any() ? 1 : Congregations.Max(x => x.ID) + 1;
                cong.Save(password.ToStandardString());
            }
        }

        public void Refresh(Congregation cong) =>
            Refresh();

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

        private bool isDisposed;
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
