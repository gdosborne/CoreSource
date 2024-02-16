using GregOsborne.Application;
using GregOsborne.Application.IO;
using System.Reflection;
using System.Windows;

namespace ManageVersioning {
    public partial class App : System.Windows.Application {
        public static string DataFile { get; private set; }
        public static Session Session { get; private set; }
        public static string ApplicationName => "Versioning";
        public static string ApplicationDirectory { get; private set; }

        private static string filename = "UpdateVersion.Projects.xml";

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            ApplicationDirectory = SysIO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName);
            new SysIO.DirectoryInfo(ApplicationDirectory).CreateIfNotExist();
            DataFile = SysIO.Path.Combine(ApplicationDirectory, filename);
            if (!SysIO.File.Exists(DataFile)) {
                var sourceDir = SysIO.Path.Combine(SysIO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Data", filename);
                SysIO.File.Copy(sourceDir, DataFile);
            }
            Session = new Session(ApplicationDirectory, ApplicationName, GregOsborne.Application.Logging.ApplicationLogger.StorageTypes.FlatFile,
                GregOsborne.Application.Logging.ApplicationLogger.StorageOptions.NewestFirstLogEntry);
        }

        public static T GetResourceItem<T>(ResourceDictionary resourceDic, string name) {
            try {
                T result = default;
                var item = resourceDic[name];
                if (item != null && item is T) {
                    result = (T)item;
                    return result;
                }
                resourceDic.MergedDictionaries.ToList().ForEach(res => {
                    if (result != null)
                        return;
                    result = GetResourceItem<T>(res, name);
                });
                return result;
            }
            catch { return default(T); }
        }

        public static class Settings {
            public static bool AreWindowPositionsSaved {
                get => App.Session.ApplicationSettings.GetValue("Application", nameof(AreWindowPositionsSaved), false);
                set => App.Session.ApplicationSettings.AddOrUpdateSetting("Application", nameof(AreWindowPositionsSaved), value);
            }

            public static bool IsTestConsoleEditable {
                get => App.Session.ApplicationSettings.GetValue("Application", nameof(IsTestConsoleEditable), false);
                set => App.Session.ApplicationSettings.AddOrUpdateSetting("Application", nameof(IsTestConsoleEditable), value);
            }
        }

    }

}
