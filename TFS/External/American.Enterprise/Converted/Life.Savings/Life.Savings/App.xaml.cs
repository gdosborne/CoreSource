using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using GregOsborne.Application;
using Life.Savings.Data;
using GregOsborne.Application.Logging;
using Life.Savings.Data.Model;
using System.Linq;
using GregOsborne.Dialog;

namespace Life.Savings {
    public partial class App {
        public static int EditingClientId { get; set; }
        private static IAppData _CurrentDataSet = null;
        public static IAppData CurrentDataSet {
            get => _CurrentDataSet ?? Repository.Ls2Data;
            set => _CurrentDataSet = value;
        }
        public static string ApplicationName = "Life.Savings";
        public static IllustrationInfo Illustration = null;
        public static string DataBaseFolder;
        public static IRepository Repository;
        public static string InsuranceCompanyName;
        public static string IllustrationSaveLocation { get; set; }
        public static State DefaultState {
            get {
                if (Repository == null)
                    return null;
                else
                    return Repository.States.FirstOrDefault(x => x.Abbreviation == Settings.GetSetting(ApplicationName, "Application", "DefaultState", string.Empty));
            }
            set => Settings.SetSetting(ApplicationName, "Application", "DefaultState", value == null ? string.Empty : value.Abbreviation);

        }
        public static string LogFolder { get; private set; }
        public static CheckBox GetAdditionalCheckBox(string text) {
            return new CheckBox {
                Margin = new Thickness(0, -10, 0, 0),
                Content = text,
                IsChecked = false,
                VerticalContentAlignment = VerticalAlignment.Center,
                Background = Brushes.Transparent
            };
        }
        public static Rect GetWindowBounds(string winId, double defaultWidth = 800.0, double defaultHeight = 400.0) {
            return new Rect(
                Settings.GetSetting(ApplicationName, winId, "Left", 100.0),
                Settings.GetSetting(ApplicationName, winId, "Top", 100.0),
                Settings.GetSetting(ApplicationName, winId, "Width", defaultWidth),
                Settings.GetSetting(ApplicationName, winId, "Height", defaultHeight)
            );
        }
        public static void SavePosition(bool isInitializing, string winId, Window win, bool saveHeight = false) {
            if (isInitializing) return;

            Settings.SetSetting(ApplicationName, winId, "Left", win.RestoreBounds.Left);
            Settings.SetSetting(ApplicationName, winId, "Top", win.RestoreBounds.Top);
            Settings.SetSetting(ApplicationName, winId, "Width", win.RestoreBounds.Width);
            if (saveHeight)
                Settings.SetSetting(ApplicationName, winId, "Height", win.RestoreBounds.Height);
        }
        protected override void OnExit(ExitEventArgs e) {
            Logger.LogMessage($"Exiting application ({e.ApplicationExitCode})");
        }
        protected override void OnStartup(StartupEventArgs e) {
            //AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            //Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            Dispatcher.UnhandledException += Dispatcher_UnhandledException;

            var appFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (!Directory.Exists(appFolder))
                Directory.CreateDirectory(appFolder);
            appFolder = Path.Combine(appFolder, ApplicationName);
            if (!Directory.Exists(appFolder))
                Directory.CreateDirectory(appFolder);

            LogFolder = Path.Combine(appFolder, "logs");
            if (!Directory.Exists(LogFolder))
                Directory.CreateDirectory(LogFolder);
            Logger.LogDirectory = LogFolder;
            Logger.SingleLogMaxSize = Convert.ToInt32(Math.Pow(10.24, 7));
            Logger.LogMessage("Starting application");

            DataBaseFolder = Settings.GetSetting(ApplicationName, "Application", "RepositoryPath", appFolder);
            InsuranceCompanyName = Settings.GetSetting(ApplicationName, "Application", "InsuranceCompanyName", "American Enterprise Insurance Company");

            IllustrationSaveLocation = Settings.GetSetting(ApplicationName, "Application", "IllustrationSaveLocation", string.Empty);
            EventManager.RegisterClassHandler(typeof(DatePicker), FrameworkElement.LoadedEvent, new RoutedEventHandler(DatePicker_Loaded));
        }
        private static void DatePicker_Loaded(object sender, RoutedEventArgs e) {
            var dp = sender as DatePicker;
            if (dp == null) return;
            var tb = (DatePickerTextBox)GregOsborne.Application.Windows.Extension.GetFirstChild<DatePickerTextBox>(dp);
            var wm = tb?.Template.FindName("PART_Watermark", tb) as ContentControl;
            if (wm == null) return;
            if (Window.GetWindow(dp) is MainWindow)
                wm.Content = string.Empty;//"<Birthdate>";
        }
        private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) {
            HandleException(e.Exception);
            e.Handled = ShowExceptionMessage($"An exception has occurred in the application:\n\n{e.Exception.Message}\n\nWould you like to exit the application?");
        }

        private bool ShowExceptionMessage(string message) {
            var td = new TaskDialog {
                Image = ImagesTypes.Error,
                MessageText = message,
                Title = "Application exception",
                Width = 500,
                Height = 250                
            };
            td.AddButtons(ButtonTypes.Yes, ButtonTypes.No);
            var result = td.ShowDialog(this.MainWindow);
            if (result == (int)ButtonTypes.Yes)
                App.Current.Shutdown();
            return false;
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) {
            //HandleException(e.Exception);
            //e.Handled = ShowExceptionMessage($"An exception has occurred in the application:\n\n{e.Exception.Message}\n\nWould you like to exit the application?");
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            //HandleException((Exception)e.ExceptionObject);
            //ShowExceptionMessage($"An exception has occurred in the application:\n\n{((Exception)e.ExceptionObject).Message}\n\nWould you like to exit the application?");
        }

        private void HandleException(Exception ex) {
            Logger.LogException(ex);
            ShowExceptionMessage($"An exception has occurred in the application:\n\n{((Exception)ex).Message}\n\nWould you like to exit the application?");
        }
    }
}