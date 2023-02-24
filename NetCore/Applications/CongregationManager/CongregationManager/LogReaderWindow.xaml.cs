using Common.Application.Primitives;
using Common.Application.Windows;
using CongregationManager.ViewModels;
using Ookii.Dialogs.Wpf;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using static Common.Application.Logging.ApplicationLogger;
using Path = System.IO.Path;
using static ApplicationFramework.Dialogs.Helpers;

namespace CongregationManager {
    public partial class LogReaderWindow : Window {
        public LogReaderWindow() {
            InitializeComponent();

            this.SetBounds(App.ApplicationSession.ApplicationSettings);
            App.LogMessage("Opening log viewer", EntryTypes.Information);

            Closing += LogReaderWindow_Closing;

            View.Initialize();
            View.ExecuteUiAction += View_ExecuteUiAction;
        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            var action = (LogReaderWindowViewModel.Actions)Enum.Parse(typeof(LogReaderWindowViewModel.Actions), e.CommandToExecute);
            switch (action) {
                case LogReaderWindowViewModel.Actions.CloseWindow:
                    Close();
                    break;
                case LogReaderWindowViewModel.Actions.ClearDay: {
                        var title = "Clear log entries";
                        var msg = "You are preparing to remove the log entries for " +
                                $"{View.SelectedLogDate}.\n\nAre you sure?";
                        if (ShowYesNoDialog(title, msg, TaskDialogIcon.Warning)) {
                            var dir = Path.Combine(App.ApplicationFolder, "Logs", DateTime.Parse(View.SelectedLogDate).ToString("yyyy-MM-dd"));
                            var filename = Path.Combine(dir, "application.xml");
                            var logDate = View.SelectedLogDate;
                            File.Delete(filename);
                            View.LogEntries.Clear();
                            View.LogDates.Remove(View.SelectedLogDate);
                            View.SelectedLogDate = null;

                            App.LogMessage($"Log cleared for {logDate}", EntryTypes.Information);
                        }
                        break;
                    }
                case LogReaderWindowViewModel.Actions.ClearAllDays: {
                        var title = "Clear log entries";
                        var msg = "You are preparing to remove all if the log " +
                            "entries.\n\nAre you sure?";
                        if (ShowYesNoDialog(title, msg, TaskDialogIcon.Warning)) {
                            var dir = Path.Combine(App.ApplicationFolder, "Logs");
                            var dirs = new DirectoryInfo(dir).GetDirectories();
                            foreach (var d in dirs) {
                                d.Delete(true);
                            }
                            View.LogEntries.Clear();
                            View.LogDates.Clear();
                            View.SelectedLogDate = null;

                            App.LogMessage("All application logs cleared", EntryTypes.Information);
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        private void LogReaderWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) {
            this.SaveBounds(App.ApplicationSession.ApplicationSettings);
            App.LogMessage("Closing log viewer", EntryTypes.Information);

        }

        public LogReaderWindowViewModel View => DataContext.As<LogReaderWindowViewModel>();

        private void TitlebarBorder_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }
    }
}
