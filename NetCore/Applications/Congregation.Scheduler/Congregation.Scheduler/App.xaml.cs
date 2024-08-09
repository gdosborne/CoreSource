using Common;
using Common.IO;

using System.Configuration;
using System.Data;
using System.Windows;

namespace Congregation.Scheduler {
    public partial class App : System.Windows.Application {
        internal static Session? Session { get; private set; }
        internal static string? ApplicationDirectory { get; private set; }
        internal static string ApplicationName => "Congregation Scheduler";

        protected override void OnStartup (StartupEventArgs e) {
            base.OnStartup(e);
            SetupDirectories();
            Session = new Session(ApplicationDirectory, ApplicationName, Common.Logging.ApplicationLogger.StorageTypes.FlatFile,
                Common.Logging.ApplicationLogger.StorageOptions.CreateFolderForEachDay);
        }

        private void SetupDirectories () {
            ApplicationDirectory = SysIO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName);
            new SysIO.DirectoryInfo(ApplicationDirectory).CreateIfNotExist();
        }
    }

}
