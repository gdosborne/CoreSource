using Common;
using Common.Linq;
using Common.MVVMFramework;
using CongregationManager.Data;
using CongregationManager.Extensibility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CongregationExtension.ViewModels {
    public class CongregationWindowViewModel : LocalBase {
        public CongregationWindowViewModel()
            : base() {

            Title = "Congregation [design]";
            Members = new ObservableCollection<Member>();
            Groups = new ObservableCollection<Group>();
        }

        public override void Initialize(AppSettings appSettings, DataManager dataManager) {
            base.Initialize(appSettings, dataManager);

            Title = "Congregation";
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
                    Members.AddRange(Congregation.Members.OrderBy(x => x.LastName).ThenBy(x => x.FirstName));
                    Members.ToList().ForEach(x => x.IsEnabled = false);
                    Groups.AddRange(Congregation.Groups.OrderBy(x => x.Name));
                    SetGroupMembership();
                    Congregation.PropertyChanged += Congregation_PropertyChanged;
                }
                OnPropertyChanged();
            }
        }

        private void SetGroupMembership(List<int> ids = default) {
            Members.ToList().ForEach(x => x.IsSelected = false);
            if (ids != null)
                Members.ToList().ForEach(x => x.IsSelected = ids.Contains(x.ID));
        }

        private void Congregation_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (Congregation != null && Congregation.IsNew && e.PropertyName == "Name")
                Congregation.Filename = $"{Congregation.Name}.congregation";
            UpdateInterface();
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

        #region Groups Property
        private ObservableCollection<Group> _Groups = default;
        /// <summary>Gets/sets the Groups.</summary>
        /// <value>The Groups.</value>
        public ObservableCollection<Group> Groups {
            get => _Groups;
            set {
                _Groups = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region AddGroupCommand
        private DelegateCommand _AddGroupCommand = default;
        /// <summary>Gets the AddGroup command.</summary>
        /// <value>The AddGroup command.</value>
        public DelegateCommand AddGroupCommand => _AddGroupCommand ?? (_AddGroupCommand = new DelegateCommand(AddGroup, ValidateAddGroupState));
        private bool ValidateAddGroupState(object state) => true;
        private void AddGroup(object state) {
            var newGroup = App.AddEditGroup(Congregation, default);
            if (newGroup != null)
                Groups.Add(newGroup);
        }
        #endregion

        #region EditGroupCommand
        private DelegateCommand _EditGroupCommand = default;
        /// <summary>Gets the EditGroup command.</summary>
        /// <value>The EditGroup command.</value>
        public DelegateCommand EditGroupCommand => _EditGroupCommand ?? (_EditGroupCommand = new DelegateCommand(EditGroup, ValidateEditGroupState));
        private bool ValidateEditGroupState(object state) => true;
        private void EditGroup(object state) {
            var group = App.AddEditGroup(Congregation, SelectedGroup);
            if (group != null && (group.ID == SelectedGroup.ID)) {
                SelectedGroup = group;
            }
        }
        #endregion

        #region DeleteGroupCommand
        private DelegateCommand _DeleteGroupCommand = default;
        /// <summary>Gets the DeleteGroup command.</summary>
        /// <value>The DeleteGroup command.</value>
        public DelegateCommand DeleteGroupCommand => _DeleteGroupCommand ?? (_DeleteGroupCommand = new DelegateCommand(DeleteGroup, ValidateDeleteGroupState));
        private bool ValidateDeleteGroupState(object state) => true;
        private void DeleteGroup(object state) {

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
            var mbr = App.AddNewMember(Congregation);
            if (mbr != null) {
                mbr.IsEnabled = SelectedGroup != null;
                Members.Add(mbr);
                var ordered = Members
                    .OrderBy(x => x.LastName)
                    .ThenBy(x => x.FirstName)
                    .ToList();
                Members = new ObservableCollection<Member>(ordered);
            }
        }
        #endregion

        #region DeleteMemberCommand
        private DelegateCommand _DeleteMemberCommand = default;
        /// <summary>Gets the DeleteMember command.</summary>
        /// <value>The DeleteMember command.</value>
        public DelegateCommand DeleteMemberCommand => _DeleteMemberCommand ?? (_DeleteMemberCommand = new DelegateCommand(DeleteMember, ValidateDeleteMemberState));
        private bool ValidateDeleteMemberState(object state) => SelectedMember != null;
        private void DeleteMember(object state) {
            if (App.DeleteMember(SelectedMember, Congregation))
                Members.Remove(SelectedMember);
        }
        #endregion

        #region MoveMemberCommand
        private DelegateCommand _MoveMemberCommand = default;
        /// <summary>Gets the MoveMember command.</summary>
        /// <value>The MoveMember command.</value>
        public DelegateCommand MoveMemberCommand => _MoveMemberCommand ?? (_MoveMemberCommand = new DelegateCommand(MoveMember, ValidateMoveMemberState));
        private bool ValidateMoveMemberState(object state) => true;
        private void MoveMember(object state) {
            var others = DataManager.Congregations
                .Where(x => x.ID != Congregation.ID)
                .OrderBy(x => x.Name)
                .ToList();
            if(App.MoveMember(SelectedMember, Congregation, others)) {
                Members.Remove(SelectedMember);
            }
        }
        #endregion

        #region EditMemberCommand
        private DelegateCommand _EditMemberCommand = default;
        /// <summary>Gets the EditMember command.</summary>
        /// <value>The EditMember command.</value>
        public DelegateCommand EditMemberCommand => _EditMemberCommand ?? (_EditMemberCommand = new DelegateCommand(EditMember, ValidateEditMemberState));
        private bool ValidateEditMemberState(object state) => true;
        private void EditMember(object state) {
            App.EditMember(SelectedMember, Congregation);
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

        #region SelectedGroup Property
        private Group _SelectedGroup = default;
        /// <summary>Gets/sets the SelectedGroup.</summary>
        /// <value>The SelectedGroup.</value>
        public Group SelectedGroup {
            get => _SelectedGroup;
            set {
                _SelectedGroup = value;
                if (SelectedGroup != null) {
                    var ids = SelectedGroup.MemberIDs;
                    if (SelectedGroup.OverseerMemberID > 0)
                        ids.Add(SelectedGroup.OverseerMemberID);
                    if (SelectedGroup.AssistantMemberID > 0)
                        ids.Add(SelectedGroup.AssistantMemberID);
                    SetGroupMembership(ids);
                }
                ExecuteAction(nameof(Actions.GroupSelected));
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
