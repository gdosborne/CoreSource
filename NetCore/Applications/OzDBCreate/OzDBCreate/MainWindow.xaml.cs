using ApplicationFramework.Dialogs;
using Common.Application.IO;
using Common.Application.Primitives;
using Common.Application.Windows.Controls;
using Ookii.Dialogs.Wpf;
using OzDB.Management;
using OzDBCreate.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using static ApplicationFramework.Dialogs.Helpers;
using IO = System.IO;

namespace OzDBCreate {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            Closing += MainWindow_Closing;

            View.Initialize();
            View.ExecuteUiAction += View_ExecuteUiAction;
        }

        private async void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            if (Enum.TryParse(typeof(MainWindowView.Actions), e.CommandToExecute, out var action)) {
                switch (action) {
                    case MainWindowView.Actions.AskNewFileName: {
                            var initialDir = App.AppSession.ApplicationSettings.GetValue("Application", "LastDatabaseFolder",
                                Environment.GetFolderPath(Environment.SpecialFolder.Personal));
                            var dlg = new VistaSaveFileDialog {
                                Title = "Save database as...",
                                AddExtension = true,
                                DefaultExt = OzDBDatabase.DatabaseExtension,
                                CheckFileExists = false,
                                CheckPathExists = true,
                                Filter = $"Database|*{OzDBDatabase.DatabaseExtension}",
                                InitialDirectory = initialDir,
                                RestoreDirectory = true
                            };
                            var result = dlg.ShowDialog(this);
                            if (!result.HasValue || !result.Value) return;
                            App.AppSession.ApplicationSettings.AddOrUpdateSetting("Application", "LastDatabaseFolder",
                                IO.Path.GetDirectoryName(dlg.FileName));
                            try {
                                if (IO.File.Exists(dlg.FileName)) return;
                                if(View.CurrentDatabase.HasChanges) {
                                    await View.CurrentDatabase.SaveAsAsync(dlg.FileName);
                                }
                            }
                            catch (Exception ex) {
                                await App.HandleExceptionAsync(ex);
                            }
                            break;
                        }
                    case MainWindowView.Actions.ShowProperties: {
                            var win = new DatabasePropertiesWindow {
                                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                                Owner = this,
                            };
                            win.View.DatabaseName = View.CurrentDatabase.Name;
                            win.View.DatabaseDescription = View.CurrentDatabase.Description;

                            var result = win.ShowDialog();
                            if (!result.HasValue || !result.Value) return;

                            View.CurrentDatabase.Name = win.View.DatabaseName;
                            View.CurrentDatabase.Description = win.View.DatabaseDescription;

                            break;
                        }
                    case MainWindowView.Actions.AskSaveChanges: {
                            var result = this.ShowYesNoDialog("Database Changes",
                                "The database you are editing has changes. Would you like to save those " +
                                "changes before closing the database?", Ookii.Dialogs.Wpf.TaskDialogIcon.Shield,
                                250);
                            if (!result) return;
                            await View.CurrentDatabase?.SaveAsync();
                            break;
                        }
                    case MainWindowView.Actions.OpenDatabase: {
                            var initialDir = App.AppSession.ApplicationSettings.GetValue("Application", "LastDatabaseFolder",
                                Environment.GetFolderPath(Environment.SpecialFolder.Personal));
                            var dlg = new VistaOpenFileDialog {
                                Title = "Open database...",
                                AddExtension = true,
                                DefaultExt = OzDBDatabase.DatabaseExtension,
                                CheckFileExists = true,
                                CheckPathExists = true,
                                Filter = $"Database|*{OzDBDatabase.DatabaseExtension}",
                                InitialDirectory = initialDir,
                                RestoreDirectory = true
                            };
                            var result = dlg.ShowDialog(this);
                            if (!result.HasValue || !result.Value) return;
                            App.AppSession.ApplicationSettings.AddOrUpdateSetting("Application", "LastDatabaseFolder",
                                IO.Path.GetDirectoryName(dlg.FileName));
                            try {
                                View.CurrentDatabase = OzDBDatabase.FromDatabaseFile(dlg.FileName);
                            }
                            catch (Exception ex) {
                                await App.HandleExceptionAsync(ex);
                            }
                            break;
                        }
                }
            }
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) {
            App.SaveWindowBounds(this, true);
            App.AppSession.ApplicationSettings.AddOrUpdateSetting(this.GetType().Name, "SplitterPosition", SplitterColumn.ActualWidth);
        }

        public MainWindowView View => DataContext.As<MainWindowView>();

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);

            MainToolbar.RemoveOverflow();
            App.RestoreWindowBounds(this, true);
            var pos = App.AppSession.ApplicationSettings.GetValue(this.GetType().Name, "SplitterPosition", 150.0);
            SplitterColumn.Width = new GridLength(pos);
        }
    }
}
