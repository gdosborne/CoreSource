using Common.Application.Primitives;
using Common.Application.Text;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace CongregationManager.Data {
    [JsonObject("member")]
    public class Member : ItemBase {
        [Flags]
        public enum PrivilegeFlags : long {
            [Description("Is A Publisher (not pioneer)")]
            Publisher                  = 1,
            [Description("Is An Auxiliary Pioneer")]
            AuxiliaryPioneer           = Publisher * 2,
            [Description("Is A Continuous Auxiliary Pioneer")]
            ContinuousAuxiliaryPioneer = AuxiliaryPioneer * 2,
            [Description("Is A Regular Pioneer")]
            RegularPioneer             = ContinuousAuxiliaryPioneer * 2,
            [Description("Is A Special Fulltime Pioneer")]
            SpecialPioneer             = RegularPioneer * 2,
            [Description("Is A Ministerial Servant")]
            MinisterialServant         = SpecialPioneer * 2,
            [Description("Is An Elder")]
            Elder                      = MinisterialServant * 2,
            [Description("Is A Group Overseer")]
            GroupOverseer              = Elder * 2,
            [Description("Is A Group Assistant")]
            GroupAssistant             = GroupOverseer * 2,
            [Description("Is The Service Overseer")]
            ServiceOverseer            = GroupAssistant * 2,
            [Description("Is The Service Overseer Assistant")]
            ServiceAssistant           = ServiceOverseer * 2,
            [Description("Is The School Overseer")]
            SchoolOverseer             = ServiceAssistant * 2,
            [Description("Is The School Overseer Assistant")]
            SchoolAssistant            = SchoolOverseer * 2,
            [Description("Is The Secretary")]
            Secretary                  = SchoolAssistant * 2,
            [Description("Is The Secretary Assistant")]
            SecretaryAssistant         = Secretary * 2,
            [Description("Is An Operating Committee Member")]
            OperatingCommitteeMember   = SecretaryAssistant * 2,
            [Description("Is The Coordinator Body of Elders")]
            COBE                       = OperatingCommitteeMember * 2,
            [Description("Is The Coordinator Body of Elsers Assistant")]
            COBE_Assistant             = COBE * 2,
            [Description("Is The Watchtower Conductor")]
            WatchtowerConductor        = COBE_Assistant * 2,
            [Description("Is The Public Talks Coordinator")]
            PublicTalksCoordinator     = WatchtowerConductor * 2,
            [Description("Is The Accounts Servant")]
            AccountsServant            = PublicTalksCoordinator * 2,
            [Description("Is The Sound Servant")]
            SoundServant               = AccountsServant * 2,
            [Description("Is The Attendant Servant")]
            AttendantServant           = SoundServant * 2,
            [Description("Is The Literature Servant")]
            LiteratureServant          = AttendantServant * 2,
            [Description("Is The Territory Servant")]
            TerritoryServant           = LiteratureServant * 2,
            [Description("Can Be Zoom Host")]
            SoundZoomHost              = TerritoryServant * 2,
            [Description("Can Run Console")]
            SoundConsole               = SoundZoomHost * 2,
            [Description("Can Manage Stage")]
            SoundStage                 = SoundConsole * 2,
            [Description("Can Run Microphone")]
            SoundMicrophone            = SoundStage * 2,
            [Description("Can Be Attendant")]
            Attendant                  = SoundMicrophone * 2,
            [Description("Can Be Zoom Attendant")]
            ZoomAttendant              = Attendant * 2,
            [Description("Can Be Sunday Chairman")]
            SundayChairman             = ZoomAttendant * 2,
            [Description("Can Read Watchtower")]
            WatchtowerReader           = SundayChairman * 2,
            [Description("Can Be School Chairman")]
            SchoolChairman             = WatchtowerReader * 2,
            [Description("Can Give Public Talks")]
            PublicTalkSpeaker          = SchoolChairman * 2,
            [Description("Can Give External Public Talks")]
            PublicTalkSpeakerExternal  = PublicTalkSpeaker * 2,
            [Description("Is A School member")]
            SchoolMember               = PublicTalkSpeakerExternal * 2
        }

        public enum Genders {
            Unknown,
            Male,
            Female
        }

        public enum Statuses {
            Good,
            Exemplary,
            Restricted,
            Disfellowshipped,
            Inactive
        }

        [JsonIgnore]
        public string FullName => $"{FirstName} {LastName}";

        #region IsNew Property
        private bool _IsNew = default;
        /// <summary>Gets/sets the IsNew.</summary>
        /// <value>The IsNew.</value>
        [JsonIgnore]
        public bool IsNew {
            get => _IsNew;
            set {
                _IsNew = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region LastName Property
        private string _LastName = default;
        /// <summary>Gets/sets the LastName.</summary>
        /// <value>The LastName.</value>
        [JsonProperty("lastname")]
        public string LastName {
            get => _LastName;
            set {
                _LastName = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsSelected Property
        private bool _IsSelected = default;
        /// <summary>Gets/sets the IsSelected.</summary>
        /// <value>The IsSelected.</value>
        [JsonIgnore]
        public bool IsSelected {
            get => _IsSelected;
            set {
                _IsSelected = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region FirstName Property
        private string _FirstName = default;
        /// <summary>Gets/sets the FirstName.</summary>
        /// <value>The FirstName.</value>
        [JsonProperty("firstname")]
        public string FirstName {
            get => _FirstName;
            set {
                _FirstName = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Priveleges Property
        private PrivilegeFlags _Priveleges = default;
        /// <summary>Gets/sets the Priveleges.</summary>
        /// <value>The Priveleges.</value>
        [JsonProperty("privileges")]
        public PrivilegeFlags Priveleges {
            get => _Priveleges;
            set {
                _Priveleges = value;
                PrivelegeValue = ((long)Priveleges).ToString("#,0");
                OnPropertyChanged();
            }
        }
        #endregion

        #region Gender Property
        private Genders _Gender = default;
        /// <summary>Gets/sets the Gender.</summary>
        /// <value>The Gender.</value>
        [JsonProperty("gender")]
        public Genders Gender {
            get => _Gender;
            set {
                _Gender = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region BaptismDate Property
        private DateTime? _BaptismDate = default;
        /// <summary>Gets/sets the BaptismDate.</summary>
        /// <value>The BaptismDate.</value>
        [JsonProperty("baptismdate")]
        public DateTime? BaptismDate {
            get => _BaptismDate;
            set {
                _BaptismDate = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Status Property
        private Statuses _Status = default;
        /// <summary>Gets/sets the Status.</summary>
        /// <value>The Status.</value>
        [JsonProperty("status")]
        public Statuses Status {
            get => _Status;
            set {
                _Status = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region PrivelegeValue Property
        private string _PrivelegeValue = default;
        /// <summary>Gets/sets the PrivelegeValue.</summary>
        /// <value>The PrivelegeValue.</value>
        [JsonIgnore]
        public string PrivelegeValue {
            get => _PrivelegeValue;
            set {
                _PrivelegeValue = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region EMailAddress Property
        private string _EMailAddress = default;
        /// <summary>Gets/sets the EMailAddress.</summary>
        /// <value>The EMailAddress.</value>
        [JsonProperty("emailaddress")]
        public string EMailAddress {
            get => _EMailAddress;
            set {
                _EMailAddress = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region HomePhone Property
        private string _HomePhone = default;
        /// <summary>Gets/sets the HomePhone.</summary>
        /// <value>The HomePhone.</value>
        [JsonProperty("homephone")]
        public string HomePhone {
            get => _HomePhone.ToPhoneNumber();
            set {
                _HomePhone = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CellPhone Property
        private string _CellPhone = default;
        /// <summary>Gets/sets the CellPhone.</summary>
        /// <value>The CellPhone.</value>
        [JsonProperty("cellphone")]
        public string CellPhone {
            get => _CellPhone.ToPhoneNumber();
            set {
                _CellPhone = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Address Property
        private string _Address = default;
        /// <summary>Gets/sets the Address.</summary>
        /// <value>The Address.</value>
        [JsonProperty("address")]
        public string Address {
            get => _Address;
            set {
                _Address = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region City Property
        private string _City = default;
        /// <summary>Gets/sets the City.</summary>
        /// <value>The City.</value>
        [JsonProperty("city")]
        public string City {
            get => _City;
            set {
                _City = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region StateProvence Property
        private string _StateProvence = default;
        /// <summary>Gets/sets the StateProvence.</summary>
        /// <value>The StateProvence.</value>
        [JsonProperty("stateprovence")]
        public string StateProvence {
            get => _StateProvence;
            set {
                _StateProvence = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region PostalCode Property
        private string _PostalCode = default;
        /// <summary>Gets/sets the PostalCode.</summary>
        /// <value>The PostalCode.</value>
        [JsonProperty("postalcode")]
        public string PostalCode {
            get => _PostalCode;
            set {
                _PostalCode = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsEnabled Property
        private bool _IsEnabled = default;
        /// <summary>Gets/sets the IsEnabled.</summary>
        /// <value>The IsEnabled.</value>
        [JsonIgnore]
        public bool IsEnabled {
            get => _IsEnabled;
            set {
                _IsEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion

        [JsonIgnore]
        public ResourceDictionary Resources { get; set; }

        private void UpdateIcon() {
            var result = default(char);
            if (Gender == Genders.Unknown) {
                Icon = Resources["user"].CastTo<string>();
            }
            else {
                if (Gender == Genders.Female) {
                    Icon = Resources["woman-04"].CastTo<string>();
                }
                else {
                    Icon = Resources["businessman"].CastTo<string>();
                    if (Priveleges.HasFlag(PrivilegeFlags.Elder)) {
                        Icon = Resources["principal-01"].CastTo<string>();
                    }
                    else if (Priveleges.HasFlag(PrivilegeFlags.MinisterialServant)) {
                        Icon = Resources["employee"].CastTo<string>();
                    }
                }
            }
        }

        private string _Icon;
        [JsonIgnore]
        public string Icon {
            get {
                UpdateIcon();
                return _Icon; ;
            } 
            private set {
                _Icon = value;
            } 
        }
    }
}
