using Common.Applicationn.Linq;
using Common.MVVMFramework;
using CongregationManager.Data;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace CongregationManager.ViewModels {
    public class LogReaderWindowViewModel : ViewModelBase {
        public LogReaderWindowViewModel() {
            Title = "Log Viewer [design]";
        }

        public override void Initialize() {
            base.Initialize();

            Title = "Log Viewer";
            LogDates = new ObservableCollection<string>();
            LogEntries = new ObservableCollection<LogEntry>();
            var dirs = new DirectoryInfo(Path.Combine(App.ApplicationFolder, "Logs")).GetDirectories();
            foreach (var dtDir in dirs) {
                if (DateTime.TryParse(dtDir.Name, out var dt)) {
                    LogDates.Add(dt.ToShortDateString());
                }
            }
            UpdateInterface();
        }

        public enum Actions {
            CloseWindow,
            ClearDay,
            ClearAllDays
        }

        #region LogDates Property
        private ObservableCollection<string> _LogDates = default;
        /// <summary>Gets/sets the LogDates.</summary>
        /// <value>The LogDates.</value>
        public ObservableCollection<string> LogDates {
            get => _LogDates;
            set {
                _LogDates = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SelectedLogDate Property
        private string _SelectedLogDate = default;
        /// <summary>Gets/sets the SelectedLogDate.</summary>
        /// <value>The SelectedLogDate.</value>
        public string SelectedLogDate {
            get => _SelectedLogDate;
            set {
                _SelectedLogDate = value;
                LogEntries.Clear();
                if (!string.IsNullOrEmpty(SelectedLogDate)) {
                    var logDir = Path.Combine(App.ApplicationFolder, "Logs");
                    LogEntries.AddRange(LogEntry.GetLogEntriesForLog(logDir, SelectedLogDate));
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region LogEntries Property
        private ObservableCollection<LogEntry> _LogEntries = default;
        /// <summary>Gets/sets the LogEntries.</summary>
        /// <value>The LogEntries.</value>
        public ObservableCollection<LogEntry> LogEntries {
            get => _LogEntries;
            set {
                _LogEntries = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CloseWindowCommand
        private DelegateCommand _CloseWindowCommand = default;
        /// <summary>Gets the CloseWindow command.</summary>
        /// <value>The CloseWindow command.</value>
        public DelegateCommand CloseWindowCommand => _CloseWindowCommand ?? (_CloseWindowCommand = new DelegateCommand(CloseWindow, ValidateCloseWindowState));
        private bool ValidateCloseWindowState(object state) => true;
        private void CloseWindow(object state) {
            ExecuteAction(nameof(Actions.CloseWindow));
        }
        #endregion

        #region ClearDayCommand
        private DelegateCommand _ClearDayCommand = default;
        /// <summary>Gets the ClearDay command.</summary>
        /// <value>The ClearDay command.</value>
        public DelegateCommand ClearDayCommand => _ClearDayCommand ?? (_ClearDayCommand = new DelegateCommand(ClearDay, ValidateClearDayState));
        private bool ValidateClearDayState(object state) => !string.IsNullOrEmpty(SelectedLogDate);
        private void ClearDay(object state) {
            ExecuteAction(nameof(Actions.ClearDay));
        }
        #endregion

        #region ClearAllDaysCommand
        private DelegateCommand _ClearAllDaysCommand = default;
        /// <summary>Gets the ClearAllDays command.</summary>
        /// <value>The ClearAllDays command.</value>
        public DelegateCommand ClearAllDaysCommand => _ClearAllDaysCommand ?? (_ClearAllDaysCommand = new DelegateCommand(ClearAllDays, ValidateClearAllDaysState));
        private bool ValidateClearAllDaysState(object state) => LogDates != null && LogDates.Any();
        private void ClearAllDays(object state) {
            ExecuteAction(nameof(Actions.ClearAllDays));
        }
        #endregion

    }
}
