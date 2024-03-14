using GregOsborne.Application;
using GregOsborne.Application.IO;
using GregOsborne.Application.Theme;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace ManageVersioning {
    public partial class App : System.Windows.Application {
        public static string DataFile { get; private set; }
        public static Session Session { get; private set; }
        public static string ApplicationName => "Versioning";
        public static string ApplicationDirectory { get; private set; }
        public static ThemeManager ThemeManager { get; private set; }

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
            ThemeManager = new ThemeManager();

            var lightThemeFile = SysIO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Themes", "Light.theme");
            var darkThemeFile = SysIO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Themes", "Dark.theme");
            var lightTheme = default(ApplicationTheme);
            var darkTheme = default(ApplicationTheme);
            if (!SysIO.File.Exists(lightThemeFile)) {
                lightTheme = new ApplicationTheme(lightThemeFile) {
                    HasChanges = true,
                    Name = "Light"
                };
                lightTheme.Save();
            }
            else {
                var temp = ApplicationTheme.Create(lightThemeFile);
                lightTheme = temp.FirstOrDefault(x => x.Name.EqualsIgnoreCase("Light"));
            }
            ThemeManager.Themes.Add(lightTheme);

            if (!SysIO.File.Exists(darkThemeFile)) {
                darkTheme = new ApplicationTheme(darkThemeFile) {
                    HasChanges = true,
                    Name = "Dark"
                };
                darkTheme.Save();
            }
            else {
                var temp = ApplicationTheme.Create(darkThemeFile);
                darkTheme = temp.FirstOrDefault(x => x.Name.EqualsIgnoreCase("Dark"));
            }
            ThemeManager.Themes.Add(darkTheme);
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
