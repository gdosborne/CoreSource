using OzFramework.Primitives;

using System.Windows;

using static OzMiniDB.Builder.MainWindowView;

namespace OzMiniDB.Builder {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            View.Initialize();
            View.ExecuteUiAction += View_ExecuteUiAction;

            Closing += (s, e) => {
                App.Session.ApplicationSettings.AddOrUpdateSetting(GetType().Name, App.Constants.Left, RestoreBounds.Left);
                App.Session.ApplicationSettings.AddOrUpdateSetting(GetType().Name, App.Constants.Top, RestoreBounds.Top);
                App.Session.ApplicationSettings.AddOrUpdateSetting(GetType().Name, App.Constants.Width, RestoreBounds.Width);
                App.Session.ApplicationSettings.AddOrUpdateSetting(GetType().Name, App.Constants.Height, RestoreBounds.Height);
                App.Session.ApplicationSettings.AddOrUpdateSetting(GetType().Name, App.Constants.WindowState, WindowState);
            };

            SourceInitialized += (s, e) => {
                Left = App.Session.ApplicationSettings.GetValue(GetType().Name, App.Constants.Left, RestoreBounds.Left);
                Top = App.Session.ApplicationSettings.GetValue(GetType().Name, App.Constants.Top, RestoreBounds.Top);
                Width = App.Session.ApplicationSettings.GetValue(GetType().Name, App.Constants.Width, RestoreBounds.Width);
                Height = App.Session.ApplicationSettings.GetValue(GetType().Name, App.Constants.Height, RestoreBounds.Height);
                WindowState = App.Session.ApplicationSettings.GetValue(GetType().Name, App.Constants.WindowState, WindowState);

            };
        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            var action = Enum.Parse(typeof(ActionTypes), e.CommandToExecute);
            switch (action) {
                case ActionTypes.AskSaveDatabase:
                case ActionTypes.AskCreateNewDatabase: {
                        var fname = e.Parameters[App.Constants.Fname].As<string>();
                        var result = AskSaveDatabase(ref fname);
                        e.Parameters[App.Constants.Fname] = fname;
                        e.Parameters[App.Constants.Cancel] = !result;
                    }
                    break;
                case ActionTypes.AskReplaceDatabase: {
                        var result = AskReplaceDatabase(e.Parameters[App.Constants.Fname].As<string>());
                        e.Parameters[App.Constants.Cancel] = !result;
                    }
                    break;
                case ActionTypes.AskOpenDatabase: {
                        var result = AskOpenDatabase(out var filename);
                        e.Parameters[App.Constants.Cancel] = !result;
                        if (!result) {
                            return;
                        }
                        e.Parameters[App.Constants.Fname] = filename;
                    }
                    break;
                case ActionTypes.AddNewTable:
                    AddNewTable();
                    break;
                case ActionTypes.ShowSettings:
                    ShowSettings();
                    break;
            }
        }

        private void ShowSettings() {
            var win = new DatabaseSettingsWindow {
                Owner = this
            };
            var hasSizeAndPosition = App.Session.ApplicationSettings.GetValue(win.GetType().Name, App.Constants.HasSettings, false);
            win.WindowStartupLocation = hasSizeAndPosition ? WindowStartupLocation.Manual : WindowStartupLocation.CenterScreen;
            win.View.Database = View.Database;
            var result = win.ShowDialog();
            if (!result.HasValue || !result.Value) return;

        }

        private void AddNewTable() {
            var win = new NewTableWindow {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            var result = win.ShowDialog();
            if (!result.HasValue || !result.Value) return;
            View.Database.Tables.Add(Items.Table.Create(win.View.TableName, win.View.TableDescription));
            View.UpdateInterface();
        }

        private bool AskOpenDatabase(out string filename) {
            var lastFolder = App.Session.ApplicationSettings.GetValue(App.Constants.Application, App.Constants.LastFldr, 
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            var result = Dialogs.SelectFileDialog(this, lastFolder, App.Constants.OpnDB,
                (Extension: App.Constants.DbExtension, Name: App.Constants.DbExtName), (Extension: App.Constants.AllFilesExtension, 
                Name: App.Constants.AllFileExtName));
            if (!string.IsNullOrEmpty(result)) {
                App.Session.ApplicationSettings.AddOrUpdateSetting(App.Constants.Application, App.Constants.LastFldr, SysIO.Path.GetDirectoryName(result));
            }
            filename = result;
            return !string.IsNullOrEmpty(result);
        }

        private bool AskSaveDatabase(ref string filename) {
            if (!string.IsNullOrWhiteSpace(filename)) {
                if (SysIO.File.Exists(filename)) {
                    return AskReplaceDatabase(filename);
                }
                var result1 = Dialogs.ShowYesNoDialogNew(this, App.Constants.SaveDbFile, App.Constants.SaveDbFile,
                    $"The file {filename} does not exist.\n\nWould you like to create it?", Ookii.Dialogs.Wpf.TaskDialogIcon.Warning);
                return result1.HasValue && result1.Value;
            }
            var lastFolder = App.Session.ApplicationSettings.GetValue(App.Constants.Application, App.Constants.LastFldr, 
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            var result = Dialogs.SaveFileDialog(this, lastFolder, App.Constants.SaveDB, App.Constants.DefaultDBName,
                (Extension: App.Constants.DbExtension, Name: App.Constants.DbExtName), (Extension: App.Constants.AllFilesExtension, 
                Name: App.Constants.AllFileExtName));
            filename = result;
            if (!string.IsNullOrWhiteSpace(result)) {
                App.Session.ApplicationSettings.AddOrUpdateSetting(App.Constants.Application, App.Constants.LastFldr, SysIO.Path.GetDirectoryName(result));
            }
            return !string.IsNullOrWhiteSpace(result);
        }

        private bool AskReplaceDatabase(string filename) {
            var result = Dialogs.ShowYesNoDialogNew(this, App.Constants.ReplDbFile, App.Constants.ReplDbFile,
                $"The file {filename} already exists.\n\nWould you like to replace it?", Ookii.Dialogs.Wpf.TaskDialogIcon.Warning);
            return result.HasValue && result.Value;
        }

        public MainWindowView View => this.DataContext.As<MainWindowView>();
    }
}