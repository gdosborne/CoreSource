using AppFramework.Timers;
using GregOsborne.Application.Dialogs;
using GregOsborne.Application.Exception;
using GregOsborne.Application.Primitives;
using GregOsborne.Application.Theme;
using InnoData;
using SharpDX;
using System.IO;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace BuildInno {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            View.Initialize(this);
            View.ExecuteUiAction += View_ExecuteUiAction;
            SourceInitialized += (s, e) => {
                var isWindowPositionSaved = App.Session.ApplicationSettings.GetValue("Application", nameof(App.SettingsWindow.View.IsWindowPositionSaved), false);
                if (isWindowPositionSaved) {
                    Left = App.Session.ApplicationSettings.GetValue(this.GetType().Name, "Left", this.RestoreBounds.Left);
                    Top = App.Session.ApplicationSettings.GetValue(this.GetType().Name, "Top", this.RestoreBounds.Top);
                    Width = App.Session.ApplicationSettings.GetValue(this.GetType().Name, "Width", this.RestoreBounds.Width);
                    Height = App.Session.ApplicationSettings.GetValue(this.GetType().Name, "Height", this.RestoreBounds.Height);

                    var splWidth = App.Session.ApplicationSettings.GetValue(this.GetType().Name, "SplitterPosition", 200);
                    theLeftColumn.Width = new GridLength(splWidth, GridUnitType.Pixel);
                }
            };
            TitlebarBorder.PreviewMouseDown += (s, e) => {
                this.DragMove();
            };
            Closing += (s, e) => {
                App.Session.ApplicationSettings.AddOrUpdateSetting(this.GetType().Name, "Left", this.RestoreBounds.Left);
                App.Session.ApplicationSettings.AddOrUpdateSetting(this.GetType().Name, "Top", this.RestoreBounds.Top);
                App.Session.ApplicationSettings.AddOrUpdateSetting(this.GetType().Name, "Width", this.RestoreBounds.Width);
                App.Session.ApplicationSettings.AddOrUpdateSetting(this.GetType().Name, "Height", this.RestoreBounds.Height);

                App.Session.ApplicationSettings.AddOrUpdateSetting(this.GetType().Name, "SplitterPosition", treeView.ActualWidth);
            };
            LoadPrevious(null);
        }

        private void LoadPrevious(string newFile) {
            var temp = new Dictionary<int, string>();
            if (!string.IsNullOrEmpty(newFile) && File.Exists(newFile)) {
                temp.Add(0, newFile);
            }
            for (int i = 1; i <= 20; i++) {
                var item = App.Session.ApplicationSettings.GetValue("PreviouslyOpened", i.ToString(), string.Empty);
                if (!string.IsNullOrEmpty(item)) {
                    if (!item.EqualsIgnoreCase(newFile)) {
                        temp.Add(i + 1, item);
                    }
                }
                else {
                    break;
                }
            }
            App.Session.ApplicationSettings.ClearAllSettings("PreviouslyOpened");
            View.BuildInnoProjects.Clear();

            var index = 0;
            temp.OrderBy(x => x.Key).ToList().ForEach(item => {
                index++;
                App.Session.ApplicationSettings.AddOrUpdateSetting("PreviouslyOpened", index.ToString(), item.Value);
                View.BuildInnoProjects.Add(new BuildInnoProject(item.Value));
            });
            if (!string.IsNullOrEmpty(newFile) && File.Exists(newFile)) {
                View.SelectedProject = View.BuildInnoProjects.FirstOrDefault(x => x.Filename.EqualsIgnoreCase(newFile));
            }
        }

        private void View_ExecuteUiAction(object sender, GregOsborne.MVVMFramework.ExecuteUiActionEventArgs e) {
            var action = (MainWindowView.Actions)Enum.Parse(typeof(MainWindowView.Actions), e.CommandToExecute);
            switch (action) {
                case MainWindowView.Actions.CopyGuid: {
                        Clipboard.SetText(Guid.NewGuid().ToString());
                    }
                    break;
                case MainWindowView.Actions.ShowToolsContentMenu: {
                        TimeSpan.FromMilliseconds(100).GetAutoStartStopTimer().Tick += (s, e) => {
                            //ToolButton.ContextMenu.IsOpen = true;
                        };
                    }
                    break;
                case MainWindowView.Actions.ShowSettings: {
                        App.SettingsWindow = new SettingsWindow {
                            WindowStartupLocation = WindowStartupLocation.CenterOwner,
                            Owner = this
                        };
                        App.SettingsWindow.View.IsLightThemeChecked = ThemeWatcher.GetWindowsTheme() == ThemeWatcher.WindowsTheme.Light;
                        App.SettingsWindow.View.IsDarkThemeChecked = ThemeWatcher.GetWindowsTheme() == ThemeWatcher.WindowsTheme.Dark;
                        App.SettingsWindow.View.IsWindowPositionSaved = App.Session.ApplicationSettings
                            .GetValue("Application", nameof(App.SettingsWindow.View.IsWindowPositionSaved), false);
                        var result = App.SettingsWindow.ShowDialog();
                        if (result.HasValue && result.Value) {
                            var themeVal = App.SettingsWindow.View.IsLightThemeChecked
                                ? ThemeWatcher.WindowsTheme.Light : ThemeWatcher.WindowsTheme.Dark;
                            ThemeWatcher.SetWindowsTheme(themeVal);
                            App.Session.ApplicationSettings
                                .AddOrUpdateSetting("Application", nameof(App.SettingsWindow.View.IsWindowPositionSaved), App.SettingsWindow.View.IsWindowPositionSaved);

                        }
                        App.SettingsWindow = null;
                    }
                    break;
                case MainWindowView.Actions.Close: {
                        Close();
                    }
                    break;
                case MainWindowView.Actions.Minimize: {
                        WindowState = WindowState.Minimized;
                    }
                    break;
                case MainWindowView.Actions.CreateNew: {

                    }
                    break;
                case MainWindowView.Actions.OpenFile: {
                        var lastDir = App.Session.ApplicationSettings.GetValue("Application", "LastDir", string.Empty);
                        var result = DialogHelpers.ShowOpenFileDialog("Open Inno Setup File...", lastDir, "*.iss", ("*.iss", "Inno Setup"));
                        if (string.IsNullOrWhiteSpace(result)) {
                            return;
                        }
                        App.Session.ApplicationSettings.AddOrUpdateSetting("Application", "LastDir", Path.GetDirectoryName(result));
                        try {
                            LoadPrevious(result);
                        }
                        catch (Exception ex) {
                            DialogHelpers.ShowOKDialog("Error", ex.ToStringRecurse().ToString(), Ookii.Dialogs.Wpf.TaskDialogIcon.Error);
                        }
                    }
                    break;
                case MainWindowView.Actions.SaveFile: {

                    }
                    break;
            }
        }

        public MainWindowView View => DataContext.As<MainWindowView>();

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            if (e.NewValue.Is<BuildInnoProject>()) {
                View.EditorData = e.NewValue.As<BuildInnoProject>().Data;
                View.Title = $"{App.ApplicationName} = {e.NewValue.As<BuildInnoProject>().Filename}";
            }
            else {
                View.EditorData = string.Join('\n', e.NewValue.As<BuildInnoSection>().Lines);
                View.Title = $"{App.ApplicationName} = {e.NewValue.As<BuildInnoSection>().Project.Filename}";
            }
        }
    }
}