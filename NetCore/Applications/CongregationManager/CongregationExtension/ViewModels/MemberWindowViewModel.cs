using Common.Applicationn;
using Common.Applicationn.Primitives;
using Common.Applicationn.Text;
using Common.MVVMFramework;
using CongregationManager.Data;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using static CongregationManager.Data.Member;

namespace CongregationExtension.ViewModels {
    public class MemberWindowViewModel : ViewModelBase {
        public MemberWindowViewModel() {
            Title = "Member [design]";
            Member = new Member();
        }

        public override void Initialize() {
            base.Initialize();

            Title = "Member";
            var actual = Enum.GetNames(typeof(PrivilegeFlags)).ToList();
            var privs = actual.Select(x => new PrivValue {
                Text = x.SplitAtCaps(),
                Privilege = (PrivilegeFlags)Enum.Parse(typeof(PrivilegeFlags), x),
                ActualValue = (long)(PrivilegeFlags)Enum.Parse(typeof(PrivilegeFlags), x)
            }).OrderBy(x => x.Privilege).ToList();
            privs.ForEach(x => {
                x.PropertyChanged += X_PropertyChanged;
            });
            Privileges = new ObservableCollection<PrivValue>(privs);

        }

        private void X_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if(e.PropertyName == "IsChecked") {
                var p = sender.As<PrivValue>();
                if (p.IsChecked) {
                    Member.Priveleges = Member.Priveleges | p.Privilege;
                }
                else {
                    Member.Priveleges = Member.Priveleges & ~p.Privilege;
                }
            }
        }

        public enum Actions {
            CloseWindow
        }

        public Settings AppSettings { get; set; }
        public DataManager DataManager { get; set; }

        #region AcceptDataCommand
        private DelegateCommand _AcceptDataCommand = default;
        /// <summary>Gets the AcceptData command.</summary>
        /// <value>The AcceptData command.</value>
        public DelegateCommand AcceptDataCommand => _AcceptDataCommand ?? (_AcceptDataCommand = new DelegateCommand(AcceptData, ValidateAcceptDataState));
        private bool ValidateAcceptDataState(object state) => true;
        private void AcceptData(object state) {

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

        #region Member Property
        private Member _Member = default;
        /// <summary>Gets/sets the Member.</summary>
        /// <value>The Member.</value>
        public Member Member {
            get => _Member;
            set {
                _Member = value;
                IsGenderUnknown = Member.Gender == Genders.Unknown;
                IsGenderMale = Member.Gender == Genders.Male;
                IsGenderFemale = Member.Gender == Genders.Female;
                IsStatusGood = Member.Status == Statuses.Good;
                IsStatusExemplary = Member.Status == Statuses.Exemplary;
                IsStatusRestricted = Member.Status == Statuses.Restricted;
                IsStatusDisfellowshipped = Member.Status == Statuses.Disfellowshipped;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsGenderUnknown Property
        private bool _IsGenderUnknown = default;
        /// <summary>Gets/sets the IsGenderUnknown.</summary>
        /// <value>The IsGenderUnknown.</value>
        public bool IsGenderUnknown {
            get => _IsGenderUnknown;
            set {
                _IsGenderUnknown = value;
                if (IsGenderUnknown)
                    Member.Gender = Genders.Unknown;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsGenderMale Property
        private bool _IsGenderMale = default;
        /// <summary>Gets/sets the IsGenderMale.</summary>
        /// <value>The IsGenderMale.</value>
        public bool IsGenderMale {
            get => _IsGenderMale;
            set {
                _IsGenderMale = value;
                if (IsGenderMale)
                    Member.Gender = Genders.Male;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsGenderFemale Property
        private bool _IsGenderFemale = default;
        /// <summary>Gets/sets the IsGenderFemale.</summary>
        /// <value>The IsGenderFemale.</value>
        public bool IsGenderFemale {
            get => _IsGenderFemale;
            set {
                _IsGenderFemale = value;
                if (IsGenderFemale)
                    Member.Gender = Genders.Female;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsStatusExemplary Property
        private bool _IsStatusExemplary = default;
        /// <summary>Gets/sets the IsStatusExemplary.</summary>
        /// <value>The IsStatusExemplary.</value>
        public bool IsStatusExemplary {
            get => _IsStatusExemplary;
            set {
                _IsStatusExemplary = value;
                if (IsStatusExemplary)
                    Member.Status = Statuses.Exemplary;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsStatusGood Property
        private bool _IsStatusGood = default;
        /// <summary>Gets/sets the IsStatusGood.</summary>
        /// <value>The IsStatusGood.</value>
        public bool IsStatusGood {
            get => _IsStatusGood;
            set {
                _IsStatusGood = value;
                if (IsStatusGood)
                    Member.Status = Statuses.Good;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsStatusRestricted Property
        private bool _IsStatusRestricted = default;
        /// <summary>Gets/sets the IsStatusRestricted.</summary>
        /// <value>The IsStatusRestricted.</value>
        public bool IsStatusRestricted {
            get => _IsStatusRestricted;
            set {
                _IsStatusRestricted = value;
                if (IsStatusRestricted)
                    Member.Status = Statuses.Restricted;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsStatusDisfellowshipped Property
        private bool _IsStatusDisfellowshipped = default;
        /// <summary>Gets/sets the IsStatusDisfellowshipped.</summary>
        /// <value>The IsStatusDisfellowshipped.</value>
        public bool IsStatusDisfellowshipped {
            get => _IsStatusDisfellowshipped;
            set {
                _IsStatusDisfellowshipped = value;
                if (IsStatusDisfellowshipped)
                    Member.Status = Statuses.Disfellowshipped;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Privileges Property
        private ObservableCollection<PrivValue> _Privileges = default;
        /// <summary>Gets/sets the Privileges.</summary>
        /// <value>The Privileges.</value>
        public ObservableCollection<PrivValue> Privileges {
            get => _Privileges;
            set {
                _Privileges = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
