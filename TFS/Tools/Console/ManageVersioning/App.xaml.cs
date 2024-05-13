using GregOsborne.Application;
using GregOsborne.Application.IO;
using GregOsborne.Application.Theme;

using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

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
            Session = new Session(ApplicationDirectory, ApplicationName, GregOsborne.Application.Logging.ApplicationLogger.StorageTypes.FlatFile,
                GregOsborne.Application.Logging.ApplicationLogger.StorageOptions.NewestFirstLogEntry);

            if (Settings.UseSharedVersionFile && !string.IsNullOrWhiteSpace(Settings.SharedVersionFilePath)) {
                var hasSharedFile = true;
                try {
                    hasSharedFile = SysIO.File.Exists(Settings.SharedVersionFilePath);
                }
                finally { }
                if (hasSharedFile) {
                    DataFile = Settings.SharedVersionFilePath;
                } else {
                    DataFile = SysIO.Path.Combine(ApplicationDirectory, filename);
                }
            } else {
                DataFile = SysIO.Path.Combine(ApplicationDirectory, filename);
            }


            if (!SysIO.File.Exists(DataFile)) {
                var sourceDir = SysIO.Path.Combine(SysIO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Data", filename);
                SysIO.File.Copy(sourceDir, DataFile);
            }


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
            public static string SharedVersionFilePath {
                get => App.Session.ApplicationSettings.GetValue("Application", nameof(SharedVersionFilePath), default(string));
                set => App.Session.ApplicationSettings.AddOrUpdateSetting("Application", nameof(SharedVersionFilePath), value);
            }

            public static bool UseSharedVersionFile {
                get => App.Session.ApplicationSettings.GetValue("Application", nameof(UseSharedVersionFile), false);
                set => App.Session.ApplicationSettings.AddOrUpdateSetting("Application", nameof(UseSharedVersionFile), value);
            }

            public static bool AreWindowPositionsSaved {
                get => App.Session.ApplicationSettings.GetValue("Application", nameof(AreWindowPositionsSaved), false);
                set => App.Session.ApplicationSettings.AddOrUpdateSetting("Application", nameof(AreWindowPositionsSaved), value);
            }

            public static bool IsTestConsoleEditable {
                get => App.Session.ApplicationSettings.GetValue("Application", nameof(IsTestConsoleEditable), false);
                set => App.Session.ApplicationSettings.AddOrUpdateSetting("Application", nameof(IsTestConsoleEditable), value);
            }

            public static bool IsConsoleBackgroundBrushUsed {
                get => App.Session.ApplicationSettings.GetValue("Application", nameof(IsConsoleBackgroundBrushUsed), false);
                set => App.Session.ApplicationSettings.AddOrUpdateSetting("Application", nameof(IsConsoleBackgroundBrushUsed), value);
            }

            public static string ConsoleBrushFilePath {
                get => App.Session.ApplicationSettings.GetValue("Application", nameof(ConsoleBrushFilePath), string.Empty);
                set => App.Session.ApplicationSettings.AddOrUpdateSetting("Application", nameof(ConsoleBrushFilePath), value);
            }

            public static int ConsoleBrushOpacity {
                get => App.Session.ApplicationSettings.GetValue("Application", nameof(ConsoleBrushOpacity), 40);
                set => App.Session.ApplicationSettings.AddOrUpdateSetting("Application", nameof(ConsoleBrushOpacity), value);
            }

            public static Color ConsoleForeground {
                get => App.Session.ApplicationSettings.GetValue("Application", nameof(ConsoleForeground), Colors.Black);
                set => App.Session.ApplicationSettings.AddOrUpdateSetting("Application", nameof(ConsoleForeground), value);
            }

        }

    }

}
