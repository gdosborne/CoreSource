using Common.Application;
using Common.MVVMFramework;
using CongregationExtension.ViewModels;
using CongregationManager.Data;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace TerritoryManager.Extension.ViewModels {
    public class CheckinoutTerritoryWindowViewModel : LocalBase {
        public CheckinoutTerritoryWindowViewModel() {
            Title = "Check Out Territory [design]";
            SelectedDate = DateTime.Now;
            IsMemberEnabled= true;
        }

        public override void Initialize(Settings appSettings, DataManager dataManager) {
            base.Initialize(appSettings, dataManager);

            Title = "Check Out Territory";

            Members = new ObservableCollection<Member>(
                DataManager.CurrentCongregation.Members.OrderBy(x => x.FullName)
            );
        }

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

        #region SelectedDate Property
        private DateTime _SelectedDate = default;
        /// <summary>Gets/sets the SelectedDate.</summary>
        /// <value>The SelectedDate.</value>
        public DateTime SelectedDate {
            get => _SelectedDate;
            set {
                _SelectedDate = value;
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

        #region AcceptDataCommand
        private DelegateCommand _AcceptDataCommand = default;
        /// <summary>Gets the AcceptData command.</summary>
        /// <value>The AcceptData command.</value>
        public DelegateCommand AcceptDataCommand => _AcceptDataCommand ?? (_AcceptDataCommand = new DelegateCommand(AcceptData, ValidateAcceptDataState));
        private bool ValidateAcceptDataState(object state) => SelectedDate > DateTime.MinValue && SelectedMember != null;
        private void AcceptData(object state) {
            ExecuteAction(nameof(Actions.AcceptData));
        }
        #endregion

        #region Notes Property
        private string _Notes = default;
        /// <summary>Gets/sets the Notes.</summary>
        /// <value>The Notes.</value>
        public string Notes {
            get => _Notes;
            set {
                _Notes = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsMemberEnabled Property
        private bool _IsMemberEnabled = default;
        /// <summary>Gets/sets the IsMemberEnabled.</summary>
        /// <value>The IsMemberEnabled.</value>
        public bool IsMemberEnabled {
            get => _IsMemberEnabled;
            set {
                _IsMemberEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion


    }
}
