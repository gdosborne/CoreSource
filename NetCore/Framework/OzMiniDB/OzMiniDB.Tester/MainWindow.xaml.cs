using Ookii.Dialogs.Wpf;

using OzFramework.Primitives;
using OzFramework.Text;

using System.IO;
using System.Windows;

using static OzMiniDB.Builder.MainWindowView;

namespace OzMiniDB.Builder {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            View.Initialize();
            View.ExecuteUiAction += View_ExecuteUiAction;

            Closing += (s, e) => {
                if (View.Database.HasChanges) {
                    var yesBtn = new TaskDialogButton {
                        Text = "Yes",
                        CommandLinkNote = "Saves the database and exits"
                    };
                    var noBtn = new TaskDialogButton {
                        Text = "No",
                        CommandLinkNote = "Exits without saving the database"
                    };
                    var cancelBtn = new TaskDialogButton {
                        Text = "Cancel",
                        CommandLinkNote = "Aborts the exit and returns to the application"
                    };
                    var result = Dialogs.ShowCustomDialog(this, "Database has changes", "Database has changes",
                        "Your database schema has changes. If you exit now your changes will be lost.\n\nWould you like to save the database?",
                        Ookii.Dialogs.Wpf.TaskDialogIcon.Warning, 220, yesBtn, noBtn, cancelBtn);
                    if (result == cancelBtn) {
                        e.Cancel = true;
                        return;
                    }
                    if (result == yesBtn) {
                        View.Database.Save();
                    }
                }

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
                case ActionTypes.SetFieldSelected: {
                        var field = e.Parameters["field"].As<Items.Field>();
                        field.IsSelected = true;
                    }
                    break;
                case ActionTypes.AddNewTable:
                    AddNewTable();
                    break;
                case ActionTypes.ShowSettings:
                    ShowSettings();
                    break;
                case ActionTypes.RemoveField:
                    RemoveField();
                    break;
                case ActionTypes.RemoveTable:
                    RemoveTable();
                    break;
            }
        }

        private void RemoveTable() {
            if (!View.SelectedTable.IsNull()) {
                var yesBtn = new TaskDialogButton {
                    Text = "Yes",
                    CommandLinkNote = "Removes the database table"
                };
                var noBtn = new TaskDialogButton {
                    Text = "No",
                    CommandLinkNote = "Leaves the database unchanged"
                };
                var result = Dialogs.ShowCustomDialog(this, "Remove Table", "Removing database table",
                    $"You are attempting to remove the table \"{View.SelectedTable.Name}\" from the database. " +
                    $"If you save the database, the data and the table definition will be changed. " +
                    $"This action is not reversible.\n\nAre you sure you want to remove the table?",
                    TaskDialogIcon.Warning, 220, yesBtn, noBtn);
                if (result == yesBtn) {
                    View.Database.Tables.Remove(View.SelectedTable);
                    if (View.Database.Tables.Count > 0) {
                        View.SelectedTable = View.Database.Tables.First();
                    }
                }
            }
        }

        private void RemoveField() {
            if (View.SelectedTable.Fields.Any(x => x.IsSelected)) {
                var field = View.SelectedTable.Fields.First(x => x.IsSelected);
                var yesBtn = new TaskDialogButton {
                    Text = "Yes",
                    CommandLinkNote = "Removes the table field"
                };
                var noBtn = new TaskDialogButton {
                    Text = "No",
                    CommandLinkNote = "Leaves the table unchanged"
                };
                var result = Dialogs.ShowCustomDialog(this, "Remove Field", "Removing table field",
                    $"You are attempting to remove the field \"{field.Name}\" from the table " +
                    $"\"{View.SelectedTable.Name}\". If you save the database, the data and the " +
                    $"field definition will be changed. This action is not reversible." +
                    $"\n\nAre you sure you want to remove the field?",
                    TaskDialogIcon.Warning, 220, yesBtn, noBtn);
                if (result == yesBtn) {
                    View.SelectedTable.Fields.Remove(field);
                }
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

            App.Session.ApplicationSettings.AddOrUpdateSetting(App.Constants.Application, App.Constants.SaveWinSizeAndLoc,
                win.View.Groups.First(x => x.Name == App.Constants.UserInterface)
                    .Values.First(x => x.Name == App.Constants.SaveWinSizeAndLoc).Value);

            View.Database.GenerateTopLevelDBEngineClass = win.View.Groups.First(x => x.Name == App.Constants.Database)
                .Values.First(x => x.Name == App.Constants.GenTopLevelDBEClass).Value.CastTo<bool>();
            View.Database.ImplementPropertyChanged = win.View.Groups.First(x => x.Name == App.Constants.Database)
                .Values.First(x => x.Name == App.Constants.ImplementPropertyChanged).Value.CastTo<bool>();
            var newFilename = win.View.Groups.First(x => x.Name == App.Constants.Database)
                .Values.First(x => x.Name == App.Constants.FileName)
                .Value.CastTo<FileInfo>().FullName;
            if (!View.Database.Filename.EqualsIgnoreCase(newFilename)) {
                View.Database.Save(newFilename);
            }
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