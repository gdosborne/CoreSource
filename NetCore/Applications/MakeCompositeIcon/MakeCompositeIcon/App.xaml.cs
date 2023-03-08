using Common.Application;
using Common.Application.IO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using sysio = System.IO;

namespace MakeCompositeIcon {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application {
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            ApplicationDirectory = sysio.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName);
            MySession = new Session(ApplicationDirectory, ApplicationName,
                Common.Application.Logging.ApplicationLogger.StorageTypes.FlatFile,
                Common.Application.Logging.ApplicationLogger.StorageOptions.CreateFolderForEachDay);
            ProcessDirectories();
        }

        private void ProcessDirectories() {
            if (!sysio.Directory.Exists(ApplicationDirectory)) {
                sysio.Directory.CreateDirectory(ApplicationDirectory);
            }
            TempDirectory = sysio.Path.Combine(ApplicationDirectory, ".temp");
            if (!sysio.Directory.Exists(TempDirectory)) {
                sysio.Directory.CreateDirectory(TempDirectory);
            }
            FilesDirectory = sysio.Path.Combine(ApplicationDirectory, "Files");
            if (!sysio.Directory.Exists(FilesDirectory)) {
                sysio.Directory.CreateDirectory(FilesDirectory);
            }
            SettingsDirectory = sysio.Path.Combine(ApplicationDirectory, "Settings");
            if (!sysio.Directory.Exists(SettingsDirectory)) {
                sysio.Directory.CreateDirectory(SettingsDirectory);
            }
        }

        public string ApplicationName { get; private set; } = "Make Composite Icon";
        public string ApplicationDirectory { get; private set; }
        public string TempDirectory { get; private set; }
        public string FilesDirectory { get; private set; }
        public string SettingsDirectory { get; private set; }
        public Session MySession { get; private set; }
    }
}
