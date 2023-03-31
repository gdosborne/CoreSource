using Common.Application;
using Common.Application.Exception;
using Common.Application.Windows;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OzDBCreate {
    public partial class App : System.Windows.Application {
        public App() {
            AppName = "Oz Database Manager";
            ProcessDirectories();
            AppSession = new Session(AppDirectory.FullName, AppName,
                Common.Application.Logging.ApplicationLogger.StorageTypes.FlatFile,
                Common.Application.Logging.ApplicationLogger.StorageOptions.CreateFolderForEachDay);
        }

        private void ProcessDirectories() {
            var appDir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
            AppDirectory = !appDir.GetDirectories().Any(x => x.Name == AppName)
                ? appDir.CreateSubdirectory(AppName)
                : appDir.GetDirectories().FirstOrDefault(x => x.Name == AppName);
            TempDirectory = AppDirectory.GetDirectories().Any(x => x.Name == "temp")
                ? AppDirectory.GetDirectories().FirstOrDefault(x => x.Name == "temp")
                : AppDirectory.CreateSubdirectory("temp");
            WorkingDirectory = AppDirectory.GetDirectories().Any(x => x.Name == "working")
                ? AppDirectory.GetDirectories().FirstOrDefault(x => x.Name == "working")
                : AppDirectory.CreateSubdirectory("working");

        }

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

        }

        public static void SaveWindowBounds(Window win, bool includeState = false) =>
            win.SaveBounds(AppSession.ApplicationSettings, includeState);

        public static void RestoreWindowBounds(Window win, bool includeState = false) =>
            win.SetBounds(App.AppSession.ApplicationSettings, includeState);

        public static Session AppSession { get; private set; }
        public static DirectoryInfo AppDirectory { get; private set; }
        public static DirectoryInfo TempDirectory { get; private set; }
        public static DirectoryInfo WorkingDirectory { get; private set; }
        public static string AppName { get; private set; }

        public static async Task HandleExceptionAsync(Exception ex) {
            var exText = await ex.ToStringRecurseAsync();
            if (exText == null) return;
            await App.AppSession.Logger.LogMessageAsync(exText.ToString(), 
                Common.Application.Logging.ApplicationLogger.EntryTypes.Error);
        }
    }
}
