using GregOsborne.Application;
using GregOsborne.Application.IO;
using GregOsborne.Application.Primitives;
using GregOsborne.Application.Text;
using GregOsborne.Application.Theme;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;
using static System.Net.Mime.MediaTypeNames;
using SysIO = System.IO;

namespace BuildInno {
    public partial class App : System.Windows.Application {
        public static Session Session { get; private set; }
        public static string ApplicationName => "Build Inno Setup";
        public static string ApplicationDirectory { get; private set; }

        public static event EventHandler CurrentThemeChanged;
        public static SettingsWindow SettingsWindow { get; set; }

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            ApplicationDirectory = SysIO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName);
            new SysIO.DirectoryInfo(ApplicationDirectory).CreateIfNotExist();

            Session = new Session(ApplicationDirectory, ApplicationName, GregOsborne.Application.Logging.ApplicationLogger.StorageTypes.FlatFile,
                GregOsborne.Application.Logging.ApplicationLogger.StorageOptions.NewestFirstLogEntry);

            var watcher = new ThemeWatcher();
            SetTheme(ThemeWatcher.GetWindowsTheme());
            watcher.ThemeChanged += (s, e) => {
                Dispatcher.Invoke(() => {
                    SetTheme(e.Theme);
                    CurrentThemeChanged?.Invoke(null, EventArgs.Empty);
                });
            };
            watcher.WatchTheme();
        }

        private static void SetTheme(ThemeWatcher.WindowsTheme value) {
            var app = App.Current.As<App>();
            app.Resources.MergedDictionaries.Clear();
            var res = new ResourceDictionary();
            res.Source = value switch {
                ThemeWatcher.WindowsTheme.Dark => new Uri("BuildInno;component/Themes/Dark.xaml", UriKind.RelativeOrAbsolute),
                _ => new Uri("BuildInno;component/Themes/Light.xaml", UriKind.RelativeOrAbsolute),
            };
            app.Resources.MergedDictionaries.Add(res);
            if (App.SettingsWindow != null) {
                App.SettingsWindow.View.IsDarkThemeChecked = value == ThemeWatcher.WindowsTheme.Dark;
                App.SettingsWindow.View.IsLightThemeChecked = value != ThemeWatcher.WindowsTheme.Dark;
            }
        }
    }
}
