using Common.Applicationn.Primitives;
using Newtonsoft.Json;
using System;
using System.Windows;
using System.Windows.Media;

namespace CongregationManager.Data {
    [JsonObject("member")]
    public class Member : ItemBase {
        [Flags]
        public enum PrivilegeFlags : long {
            Publisher = 1,
            AuxiliaryPioneer = Publisher * 2,
            ContinuousAuxiliaryPioneer = AuxiliaryPioneer * 2,
            RegularPioneer = ContinuousAuxiliaryPioneer * 2,
            SpecialPioneer = RegularPioneer * 2,
            MinisterialServant = SpecialPioneer * 2,
            Elder = MinisterialServant * 2,
            GroupOverseer = Elder * 2,
            GroupAssistant = GroupOverseer * 2,
            ServiceOverseer = GroupAssistant * 2,
            ServiceAssistant = ServiceOverseer * 2,
            SchoolOverseer = ServiceAssistant * 2,
            SchoolAssistant = SchoolOverseer * 2,
            Secretary = SchoolOverseer * 2,
            SecretaryAssistant = Secretary * 2,
            OperatingCommitteeMember = SecretaryAssistant * 2,
            COBE = OperatingCommitteeMember * 2,
            COBEAssistant = COBE * 2,
            WatchtowerConductor = COBEAssistant * 2,
            PublicTalksCoordinator = WatchtowerConductor * 2,
            AccountsServant = PublicTalksCoordinator * 2,
            SoundServant = AccountsServant * 2,
            AttendantServant = SoundServant * 2,
            LiteratureServant = AttendantServant * 2,
            TerritoryServant = LiteratureServant * 2,
            SoundZoomHost = TerritoryServant * 2,
            SoundConsole = SoundZoomHost * 2,
            SoundStage = SoundConsole * 2,
            SoundMicrophone = SoundStage * 2,
            Attendant = SoundMicrophone * 2,
            ZoomAttendant = Attendant * 2,
            SundayChairman = ZoomAttendant * 2,
            WatchtowerReader = SundayChairman * 2,
            SchoolChairman = WatchtowerReader * 2,
            PublicTalkSpeaker = SchoolChairman * 2,
            SchoolMember = PublicTalkSpeaker * 2
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
