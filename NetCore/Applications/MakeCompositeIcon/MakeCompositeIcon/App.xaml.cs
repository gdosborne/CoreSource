using Common.Application;
using Common.Application.Primitives;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using static Common.Application.Media.Extensions;
using sysio = System.IO;
using static Common.Application.Exception.Extensions;
using ApplicationFramework.Windows.Theme;

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
            App.ThisApp.MySession.Logger.LogMessage("Application starting",
                Common.Application.Logging.ApplicationLogger.EntryTypes.Information);

            ProcessDirectories();

            Exit += App_Exit;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            ThisApp.Resources["RootFontSize"] = ThisApp.BaseFontSize;
            ThisApp.Resources["AppFontFamily"] = this.BaseFontFamily;
            CachedCharacters = new Dictionary<string, List<CharInfo>>();
            OtherFilesFilename = Path.Combine(SettingsDirectory, "mru.json");

            Watcher = new ThemeWatcher();
            Watcher.ThemeChanged += Watcher_ThemeChanged;
            Watcher.WatchTheme();

            var currentTheme = MySession.ApplicationSettings.GetValue("Application", "Theme", ThemeWatcher.WindowsTheme.Light);
            SwapThemes(currentTheme);

            if (e.Args.Any()) {
                StartupFilename = e.Args[0];
            }
        }

        public void SwapThemes(ThemeWatcher.WindowsTheme theme) {
            var themeName = theme == ThemeWatcher.WindowsTheme.Light ? "Light" : "Dark";
            this.Resources.MergedDictionaries[0].Source =
                new Uri($"/Resources/{themeName}.xaml", UriKind.Relative);
        }

        private void Watcher_ThemeChanged(object sender, ThemeChangedEventArgs e) {
            MySession.ApplicationSettings.AddOrUpdateSetting("Application", "Theme", e.Theme);
            SwapThemes(e.Theme);
        }

        public static App ThisApp => App.Current.As<App>();

        private void App_Exit(object sender, ExitEventArgs e) {
            App.ThisApp.MySession.Logger.LogMessage("Application exiting",
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

        internal ThemeWatcher Watcher { get; private set; }

        public string ApplicationName { get; private set; } = "Make Composite Icon";
        public string ApplicationDirectory { get; private set; }
        public string TempDirectory { get; private set; }
        public string FilesDirectory { get; private set; }
        public string RecycleDirectory { get; private set; }
        public string SettingsDirectory { get; private set; }
        public Session MySession { get; private set; }
        public Dictionary<string, List<CharInfo>> CachedCharacters {get; private set; }
        public string StartupFilename { get; private set; }
        public string OtherFilesFilename { get; private set; }

        public bool IsUseLastPositionChecked {
            get => App.ThisApp.MySession.ApplicationSettings.GetValue("Application", nameof(IsUseLastPositionChecked), true);
            set => App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting("Application", nameof(IsUseLastPositionChecked), value);
        }

        public bool AreGuidesShown {
            get => App.ThisApp.MySession.ApplicationSettings.GetValue("Application", nameof(AreGuidesShown), true);
            set => App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting("Application", nameof(AreGuidesShown), value);
        }

        public double BaseFontSize {
            get => App.ThisApp.MySession.ApplicationSettings.GetValue("Application", nameof(BaseFontSize), 18.0);
            set {
                App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting("Application", nameof(BaseFontSize), value);
                App.ThisApp.Resources["RootFontSize"] = value;
            }
        }

        public FontFamily BaseFontFamily {
            get => new FontFamily(App.ThisApp.MySession.ApplicationSettings.GetValue("Application", nameof(BaseFontFamily), 
                App.ThisApp.Resources["AppFontFamily"].As<FontFamily>().Source));
            set {
                App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting("Application", nameof(BaseFontFamily), value.Source);
                App.ThisApp.Resources["AppFontFamily"] = value;
            }
        }

        internal void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) {
            HandleException(e.Exception);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            HandleException(e.ExceptionObject.As<Exception>());
        }

        public static async void HandleException(Exception ex) {
            ThisApp.MySession.Logger.LogMessage(await ex.ToStringRecurseAsync(true), 
                Common.Application.Logging.ApplicationLogger.EntryTypes.Error);
        }

        public static void ClearRecycleBin() {
            var numberOfFiles = 0;
            var dir = new DirectoryInfo(App.ThisApp.RecycleDirectory);
            if (dir.Exists) {
                numberOfFiles = dir.GetFiles().Count();
            }
            else {
                return;
            }
            var td = new TaskDialog {
                MainIcon = TaskDialogIcon.Shield,
                MainInstruction = $"Clear recycle bin?",
                AllowDialogCancellation = true,
                ButtonStyle = TaskDialogButtonStyle.Standard,
                Content = $"You are about to clear the recycle bin of {numberOfFiles} icons. Doing so " +
                    $"will remove those files permanently.\n\nAre you sure you want to clear" +
                    $"the recycle bin?",
                CenterParent = true,
                MinimizeBox = false,
                WindowTitle = "Clear recycle bin..."
            };
            td.Buttons.Add(new TaskDialogButton(ButtonType.Yes));
            td.Buttons.Add(new TaskDialogButton(ButtonType.No));
            var result = td.ShowDialog();
            if (result.ButtonType == ButtonType.No) {
                return;
            }
            dir.GetFiles().ToList().ForEach(x => x.Delete());
        }

        public static bool RecyleBinHasFiles {
            get {
                var dir = new DirectoryInfo(App.ThisApp.RecycleDirectory);
                return dir.Exists && dir.GetFiles().Any();
            }
        }
    }
}
