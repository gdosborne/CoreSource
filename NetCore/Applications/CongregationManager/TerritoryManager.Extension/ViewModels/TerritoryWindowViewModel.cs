using Common.Application;
using Common.MVVMFramework;
using CongregationExtension.ViewModels;
using CongregationManager.Data;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace TerritoryManager.Extension.ViewModels {
    public class TerritoryWindowViewModel : LocalBase {
        public TerritoryWindowViewModel() {
            Title = "Territory [design]";
        }

        public override void Initialize(Settings appSettings, DataManager dataManager) {
            base.Initialize(appSettings, dataManager);

            Title = "Territory";
            Members = new ObservableCollection<Member>(
                DataManager.CurrentCongregation.Members.OrderBy(x => x.FullName)
            );
        }

        #region SelectedMember Property
        private Member _SelectedMember = default;
        /// <summary>Gets/sets the SelectedMember.</summary>
        /// <value>The SelectedMember.</value>
        public Member SelectedMember {
            get => _SelectedMember;
            set {
                _SelectedMember = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Territory Property
        private Territory _Territory = default;
        /// <summary>Gets/sets the Territory.</summary>
        /// <value>The Territory.</value>
        public Territory Territory {
            get => _Territory;
            set {
                _Territory = value;
                if (Territory != null) {
                    if (!Territory.History.Any()) {
                        HistoryItem = new TerritoryHistory {
                            ID = 0,
                            CheckOutDate = DateTime.Now
                        };
                    }
                    else {
                        HistoryItem = Territory.History.OrderByDescending(x => x.CheckOutDate).FirstOrDefault();
                    }
                    Territory.PropertyChanged += Territory_PropertyChanged;
                }
                OnPropertyChanged();
            }
        }

        private void Territory_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
            UpdateInterface();
        }
        #endregion

        #region HistoryItem Property
        private TerritoryHistory _HistoryItem = default;
        /// <summary>Gets/sets the HistoryItem.</summary>
        /// <value>The HistoryItem.</value>
        public TerritoryHistory HistoryItem {
            get => _HistoryItem;
            set {
                _HistoryItem = value;
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

        #region AcceptValueCommand
        private DelegateCommand _AcceptValueCommand = default;
        /// <summary>Gets the AcceptValue command.</summary>
        /// <value>The AcceptValue command.</value>
        public DelegateCommand AcceptValueCommand => _AcceptValueCommand ?? (_AcceptValueCommand = new DelegateCommand(AcceptValue, ValidateAcceptValueState));
        private bool ValidateAcceptValueState(object state) => Territory != null && !string.IsNullOrEmpty(Territory.Number);
        private void AcceptValue(object state) {
            ExecuteAction(nameof(Actions.AcceptData));
        }
        #endregion

        #region ShowDoNotCallsCommand
        private DelegateCommand _ShowDoNotCallsCommand = default;
        /// <summary>Gets the ShowDoNotCalls command.</summary>
        /// <value>The ShowDoNotCalls command.</value>
        public DelegateCommand ShowDoNotCallsCommand => _ShowDoNotCallsCommand ?? (_ShowDoNotCallsCommand = new DelegateCommand(ShowDoNotCalls, ValidateShowDoNotCallsState));
        private bool ValidateShowDoNotCallsState(object state) => true;
        private void ShowDoNotCalls(object state) {
            ExecuteAction(nameof(Actions.ShowDoNotCall));
        }
        #endregion

        #region ShowHistoryCommand
        private DelegateCommand _ShowHistoryCommand = default;
        /// <summary>Gets the ShowHistory command.</summary>
        /// <value>The ShowHistory command.</value>
        public DelegateCommand ShowHistoryCommand => _ShowHistoryCommand ?? (_ShowHistoryCommand = new DelegateCommand(ShowHistory, ValidateShowHistoryState));
        private bool ValidateShowHistoryState(object state) => true;
        private void ShowHistory(object state) {
            ExecuteAction(nameof(Actions.ShowHistory));
        }
        #endregion

        #region Members Property
        private ObservableCollection<Member> _Members = default;
        /// <summary>Gets/sets the Members.</summary>
        /// <value>The Members.</value>
        public ObservableCollection<Member> Members {
            get => _Members;
            set {
                _Members = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
