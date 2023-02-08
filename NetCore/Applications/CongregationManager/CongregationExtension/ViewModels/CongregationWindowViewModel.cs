using Common.MVVMFramework;
using CongregationManager.Data;
using System.Collections.ObjectModel;
using System.Linq;

namespace CongregationExtension.ViewModels {
    public class CongregationWindowViewModel : LocalBase {
        public CongregationWindowViewModel()
            : base() {
            Title = "Congregation [design]";
        }

        public override void Initialize() {
            base.Initialize();

            Title = "Congregation";
            SelectedMembers = new ObservableCollection<Member>();
        }

        #region Congregation Property
        private Congregation _Congregation = default;
        /// <summary>Gets/sets the Congregation.</summary>
        /// <value>The Congregation.</value>
        public Congregation Congregation {
            get => _Congregation;
            set {
                _Congregation = value;
                if (Congregation != null) {
                    Congregation.PropertyChanged += Congregation_PropertyChanged;
                }
                OnPropertyChanged();
            }
        }

        private void Congregation_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (Congregation != null && Congregation.IsNew && e.PropertyName == "Name")
                Congregation.Filename = $"{Congregation.Name}.congregation";
            UpdateInterface();
        }
        #endregion

        #region AcceptDataCommand
        private DelegateCommand _AcceptDataCommand = default;
        /// <summary>Gets the AcceptData command.</summary>
        /// <value>The AcceptData command.</value>
        public DelegateCommand AcceptDataCommand => _AcceptDataCommand ?? (_AcceptDataCommand = new DelegateCommand(AcceptData, ValidateAcceptDataState));
        private bool ValidateAcceptDataState(object state) {
            var result = true;
            result &= Congregation != null && !string.IsNullOrEmpty(Congregation.Name);
            return result;
        }
        private void AcceptData(object state) {
            ExecuteAction(nameof(Actions.AcceptData));
        }
        #endregion

        #region AddMemberCommand
        private DelegateCommand _AddMemberCommand = default;
        /// <summary>Gets the AddMember command.</summary>
        /// <value>The AddMember command.</value>
        public DelegateCommand AddMemberCommand => _AddMemberCommand ?? (_AddMemberCommand = new DelegateCommand(AddMember, ValidateAddMemberState));
        private bool ValidateAddMemberState(object state) => Congregation.ID > 0;
        private void AddMember(object state) {
            App.AddNewMember(Congregation);
        }
        #endregion

        #region DeleteMemberCommand
        private DelegateCommand _DeleteMemberCommand = default;
        /// <summary>Gets the DeleteMember command.</summary>
        /// <value>The DeleteMember command.</value>
        public DelegateCommand DeleteMemberCommand => _DeleteMemberCommand ?? (_DeleteMemberCommand = new DelegateCommand(DeleteMember, ValidateDeleteMemberState));
        private bool ValidateDeleteMemberState(object state) => SelectedMembers.Any();
        private void DeleteMember(object state) {
            if(App.DeleteMember(SelectedMembers.ToList(), Congregation)) {
                //App.DataManager.SaveCongregation(Congregation);
            }
        }
        #endregion

        #region MoveMemberCommand
        private DelegateCommand _MoveMemberCommand = default;
        /// <summary>Gets the MoveMember command.</summary>
        /// <value>The MoveMember command.</value>
        public DelegateCommand MoveMemberCommand => _MoveMemberCommand ?? (_MoveMemberCommand = new DelegateCommand(MoveMember, ValidateMoveMemberState));
        private bool ValidateMoveMemberState(object state) => SelectedMembers.Any();
        private void MoveMember(object state) {
            var others = DataManager.Congregations
                .Where(x => x.ID != Congregation.ID)
                .OrderBy(x => x.Name)
                .ToList();
            if(App.MoveMember(SelectedMembers.ToList(), Congregation, others)) {
                //App.DataManager.SaveCongregation(Congregation);
            }
        }
        #endregion

        #region SelectedMembers Property
        private ObservableCollection<Member> _SelectedMembers = default;
        /// <summary>Gets/sets the SelectedMembers.</summary>
        /// <value>The SelectedMembers.</value>
        public ObservableCollection<Member> SelectedMembers {
            get => _SelectedMembers;
            set {
                _SelectedMembers = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
