using Common.Application;
using Common.Application.Primitives;
using System;
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
            App.Current.As<App>().MySession.Logger.LogMessage("Application starting",
                Common.Application.Logging.ApplicationLogger.EntryTypes.Information);

            ProcessDirectories();

            Exit += App_Exit;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void App_Exit(object sender, ExitEventArgs e) {
            App.Current.As<App>().MySession.Logger.LogMessage("Application exiting",
                Common.Application.Logging.ApplicationLogger.EntryTypes.Information);
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
            RecycleDirectory = sysio.Path.Combine(FilesDirectory, "Recycle Bin");
            if (!sysio.Directory.Exists(RecycleDirectory)) {
                sysio.Directory.CreateDirectory(RecycleDirectory);
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
        public string RecycleDirectory { get; private set; }
        public string SettingsDirectory { get; private set; }
        public Session MySession { get; private set; }

        internal void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) {
            HandleException(e.Exception);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            HandleException(e.ExceptionObject.As<Exception>());
        }

        public static void HandleException(Exception ex) {
            App.Current.As<App>().MySession.Logger.LogMessage(ex.ToString(), Common.Application.Logging.ApplicationLogger.EntryTypes.Error);
        }
    }
}
