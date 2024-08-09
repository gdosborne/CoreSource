using Common;
using Common.Linq;
using Common.MVVMFramework;
using CongregationManager.Data;
using CongregationManager.Extensibility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CongregationExtension.ViewModels {
    public class GroupWindowViewModel : LocalBase {
        public GroupWindowViewModel()
            : base() {

            Title = "Group [design]";
            Overseers = new ObservableCollection<Member>();
            Members = new ObservableCollection<Member>();
            Assistants = new ObservableCollection<Member>();
        }

        public override void Initialize(AppSettings appSettings, DataManager dataManager) {
            base.Initialize(appSettings, dataManager);

            Title = "Group";

        }

        #region GroupName Property
        private string _GroupName = default;
        /// <summary>Gets/sets the GroupName.</summary>
        /// <value>The GroupName.</value>
        public string GroupName {
            get => _GroupName;
            set {
                _GroupName = value;
                if (Group != null) {
                    Group.Name = GroupName;
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region Overseers Property
        private ObservableCollection<Member> _Overseers = default;
        /// <summary>Gets/sets the Overseers.</summary>
        /// <value>The Overseers.</value>
        public ObservableCollection<Member> Overseers {
            get => _Overseers;
            set {
                _Overseers = value;
                if (Group != null) {
                    Group.OverseerMemberID = SelectedOverseer.ID;
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region Assistants Property
        private ObservableCollection<Member> _Assistants = default;
        /// <summary>Gets/sets the Assistants.</summary>
        /// <value>The Assistants.</value>
        public ObservableCollection<Member> Assistants {
            get => _Assistants;
            set {
                _Assistants = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Congregation Property
        private Congregation _Congregation = default;
        /// <summary>Gets/sets the Congregation.</summary>
        /// <value>The Congregation.</value>
        public Congregation Congregation {
            get => _Congregation;
            set {
                _Congregation = value;
                if (Congregation != null) {
                    var currentOsIds = Congregation.Groups
                        .Where(x => x.OverseerMemberID > 0).
                        Select(x => x.OverseerMemberID).ToList();
                    var currentAOsIds = Congregation.Groups
                        .Where(x => x.AssistantMemberID > 0)
                        .Select(x => x.AssistantMemberID).ToList();

                    var os = Congregation.Members.Where(x => x.Priveleges.HasFlag(Member.PrivilegeFlags.GroupOverseer))
                        .ToList();
                    Overseers.AddRange(os);

                    var aos = Congregation.Members.Where(x => x.Priveleges.HasFlag(Member.PrivilegeFlags.GroupAssistant))
                        .ToList();
                    aos.AddRange(os);
                    var joined = aos.OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
                    Assistants.AddRange(joined);

                    Members.AddRange(Congregation.Members);
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region GroupMemberIDs Property
        private List<int> _GroupMemberIDs = default;
        /// <summary>Gets/sets the GroupMemberIDs.</summary>
        /// <value>The GroupMemberIDs.</value>
        public List<int> GroupMemberIDs {
            get => _GroupMemberIDs;
            set {
                _GroupMemberIDs = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SelectedOverseer Property
        private Member _SelectedOverseer = default;
        /// <summary>Gets/sets the SelectedOverseer.</summary>
        /// <value>The SelectedOverseer.</value>
        public Member SelectedOverseer {
            get => _SelectedOverseer;
            set {
                if (SelectedOverseer != null) {
                    Congregation.Members.First(x => x.ID == SelectedOverseer.ID).IsSelected = false;
                    SelectedOverseer.IsSelected = false;
                    if (Group != null)
                        Group.OverseerMemberID = 0;
                }
                _SelectedOverseer = value;
                if (SelectedOverseer != null) {
                    Congregation.Members.First(x => x.ID == SelectedOverseer.ID).IsSelected = true;
                    SelectedOverseer.IsSelected = true;
                    if (Group != null)
                        Group.OverseerMemberID = SelectedOverseer.ID;
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region SelectedAssistant Property
        private Member _SelectedAssistant = default;
        /// <summary>Gets/sets the SelectedAssistant.</summary>
        /// <value>The SelectedAssistant.</value>
        public Member SelectedAssistant {
            get => _SelectedAssistant;
            set {
                if (SelectedAssistant != null) {
                    Congregation.Members.First(x => x.ID == SelectedAssistant.ID).IsSelected = false;
                    SelectedAssistant.IsSelected = false;
                    if (Group != null)
                        Group.AssistantMemberID = 0;
                }
                _SelectedAssistant = value;
                if (SelectedAssistant != null) {
                    Congregation.Members.First(x => x.ID == SelectedAssistant.ID).IsSelected = true;
                    SelectedAssistant.IsSelected = true;
                    if (Group != null)
                        Group.AssistantMemberID = SelectedAssistant.ID;
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region Group Property
        private Group _Group = default;
        /// <summary>Gets/sets the Group.</summary>
        /// <value>The Group.</value>
        public Group Group {
            get => _Group;
            set {
                _Group = value;
                if (Group != null) {
                    GroupName = Group.Name;
                    SelectedOverseer = null;
                    SelectedAssistant = null;
                    if (Group.OverseerMemberID > 0)
                        SelectedOverseer = Overseers.FirstOrDefault(x => x.ID == Group.OverseerMemberID);
                    if (Group.AssistantMemberID > 0)
                        SelectedAssistant = Assistants.FirstOrDefault(x => x.ID == Group.AssistantMemberID);
                    if (Congregation != null)
                        Group.MemberIDs.ForEach(x => Congregation.Members.FirstOrDefault(y => y.ID == x).IsSelected = true);
                    //Members = new ObservableCollection<Member>(Congregation.Members.Where(x => Group.MemberIDs.Contains(x.ID)));
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region AcceptGroupCommand
        private DelegateCommand _AcceptGroupCommand = default;
        /// <summary>Gets the AcceptGroup command.</summary>
        /// <value>The AcceptGroup command.</value>
        public DelegateCommand AcceptGroupCommand => _AcceptGroupCommand ?? (_AcceptGroupCommand = new DelegateCommand(AcceptGroup, ValidateAcceptGroupState));
        private bool ValidateAcceptGroupState(object state) => !string.IsNullOrEmpty(GroupName) &&
            SelectedOverseer != null;
        private void AcceptGroup(object state) {
            GroupMemberIDs = Members.Where(x => x.IsSelected).Select(x => x.ID).ToList();
            ExecuteAction(nameof(Actions.AcceptData));
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

        #region UnassignedMembers Property
        private ObservableCollection<Member> _UnassignedMembers = default;
        /// <summary>Gets/sets the UnassignedMembers.</summary>
        /// <value>The UnassignedMembers.</value>
        public ObservableCollection<Member> UnassignedMembers {
            get => _UnassignedMembers;
            set {
                _UnassignedMembers = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
