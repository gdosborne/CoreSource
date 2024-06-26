namespace Imaginator.Data
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GregOsborne.Application.Primitives;

    public class DirectoryItem
    {
        public bool InitialLoad { get; private set; }
        public string Name { get; set; }
        private string _FullPath = null;
        public string FullPath
        {
            get { return _FullPath; }
            set
            {
                monitor = new FileSystemWatcher(value);
                monitor.Created += monitor_Created;
                monitor.Deleted += monitor_Deleted;
                monitor.Renamed += monitor_Renamed;
                monitor.EnableRaisingEvents = true;
                _FullPath = value;
            }
        }

        void monitor_Renamed(object sender, RenamedEventArgs e)
        {
            try
            {
                InitialLoad = false;
                LoadFiles();
                if (MustReloadFiles != null)
                    MustReloadFiles(this, EventArgs.Empty);
            }
            catch { }
        }
        void monitor_Deleted(object sender, FileSystemEventArgs e)
        {
            try
            {
                InitialLoad = false;
                LoadFiles();
                if (MustReloadFiles != null)
                    MustReloadFiles(this, EventArgs.Empty);
            }
            catch { }
        }
        void monitor_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
                InitialLoad = false;
                LoadFiles();
                if (MustReloadFiles != null)
                    MustReloadFiles(this, EventArgs.Empty);
            }
            catch { }
        }
        private FileSystemWatcher monitor = null;
        [Flags]
        public enum ExtensionFlags
        {
            None = 0,
            Png = 1,
            Jpg = 2,
            Bmp = 4,
            Ico = 8
        }
        private ExtensionFlags _Extensions;
        public ExtensionFlags Extensions
        {
            get { return _Extensions; }
            set
            {
                _Extensions = value;
                foreach (var item in Directories)
                {
                    item.Extensions = value;
                }
                InitialLoad = false;
                LoadFiles();
                if (MustReloadFiles != null)
                    MustReloadFiles(this, EventArgs.Empty);
            }
        }
        public static DirectoryItem Create(string path, ExtensionFlags extensions)
        {
            if (!Directory.Exists(path))
                return null;
            var dInfo = new DirectoryInfo(path);
            var result = new DirectoryItem(extensions)
            {
                FullPath = dInfo.FullName,
                Name = dInfo.Name
            };
            foreach (var d in dInfo.GetDirectories())
            {
                var di = DirectoryItem.Create(d.FullName, extensions);
                result.Directories.Add(di);
            }
            return result;
        }
        private DirectoryItem(ExtensionFlags extensions)
        {
            _Extensions = extensions;
            Directories = new ObservableCollection<DirectoryItem>();
            Files = new ObservableCollection<FileItem>();
        }
        public void LoadFiles()
        {
            if (InitialLoad)
                return;
            try
            {
                this.Files.Clear();
                foreach (var f in new DirectoryInfo(FullPath).GetFiles())
                {
                    if (Extensions.HasFlag(ExtensionFlags.Png) && f.Extension.Equals(".png", StringComparison.OrdinalIgnoreCase) ||
                        Extensions.HasFlag(ExtensionFlags.Jpg) && f.Extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        Extensions.HasFlag(ExtensionFlags.Bmp) && f.Extension.Equals(".bmp", StringComparison.OrdinalIgnoreCase) ||
                        Extensions.HasFlag(ExtensionFlags.Ico) && f.Extension.Equals(".ico", StringComparison.OrdinalIgnoreCase))
                        this.Files.Add(FileItem.Create(f.FullName));
                }
            }
            catch { }
            InitialLoad = true;
        }
        public ObservableCollection<DirectoryItem> Directories { get; set; }
        public ObservableCollection<FileItem> Files { get; set; }
        public event EventHandler MustReloadFiles;
    }
}
