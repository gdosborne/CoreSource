using CongregationData.Helpers;

using System.Xml.Linq;

namespace CongregationData {
    public class MemberFlags : DomainItem {

        //IS area

        #region IsFamilyHead Property
        private bool _IsFamilyHead = default;
        public bool IsFamilyHead {
            get => _IsFamilyHead;
            set {
                _IsFamilyHead = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsInactive Property
        private bool _IsInactive = default;
        public bool IsInactive {
            get => _IsInactive;
            set {
                _IsInactive = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsNoLongerAWitcness Property
        private bool _IsNoLongerAWitcness = default;
        public bool IsNoLongerAWitcness {
            get => _IsNoLongerAWitcness;
            set {
                _IsNoLongerAWitcness = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsBrother Property
        private bool _IsBrother = default;
        public bool IsBrother {
            get => _IsBrother;
            set {
                _IsBrother = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsPublisher Property
        private bool _IsPublisher = default;
        public bool IsPublisher {
            get => _IsPublisher;
            set {
                _IsPublisher = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsBaptized Property
        private bool _IsBaptized = default;
        public bool IsBaptized {
            get => _IsBaptized;
            set {
                _IsBaptized = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsSchoolMember Property
        private bool _IsSchoolMember = default;
        public bool IsSchoolMember {
            get => _IsSchoolMember;
            set {
                _IsSchoolMember = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsMinisterialServant Property
        private bool _IsMinisterialServant = default;
        public bool IsMinisterialServant {
            get => _IsMinisterialServant;
            set {
                _IsMinisterialServant = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsElder Property
        private bool _IsElder = default;
        public bool IsElder {
            get => _IsElder;
            set {
                _IsElder = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsCoordinatorOfTheBodyOfElders Property
        private bool _IsCoordinatorOfTheBodyOfElders = default;
        public bool IsCoordinatorOfTheBodyOfElders {
            get => _IsCoordinatorOfTheBodyOfElders;
            set {
                _IsCoordinatorOfTheBodyOfElders = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsCoordinatorOfTheBodyOfEldersAssistant Property
        private bool _IsCoordinatorOfTheBodyOfEldersAssistant = default;
        public bool IsCoordinatorOfTheBodyOfEldersAssistant {
            get => _IsCoordinatorOfTheBodyOfEldersAssistant;
            set {
                _IsCoordinatorOfTheBodyOfEldersAssistant = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsServiceOverseer Property
        private bool _IsServiceOverseer = default;
        public bool IsServiceOverseer {
            get => _IsServiceOverseer;
            set {
                _IsServiceOverseer = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsServiceOverseerAssistant Property
        private bool _IsServiceOverseerAssistant = default;
        public bool IsServiceOverseerAssistant {
            get => _IsServiceOverseerAssistant;
            set {
                _IsServiceOverseerAssistant = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsSecretary Property
        private bool _IsSecretary = default;
        public bool IsSecretary {
            get => _IsSecretary;
            set {
                _IsSecretary = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsSecretaryAssistant Property
        private bool _IsSecretaryAssistant = default;
        public bool IsSecretaryAssistant {
            get => _IsSecretaryAssistant;
            set {
                _IsSecretaryAssistant = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsOperatingCommitteeMember Property
        private bool _IsOperatingCommitteeMember = default;
        public bool IsOperatingCommitteeMember {
            get => _IsOperatingCommitteeMember;
            set {
                _IsOperatingCommitteeMember = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsPublicTalkCoordinator Property
        private bool _IsPublicTalkCoordinator = default;
        public bool IsPublicTalkCoordinator {
            get => _IsPublicTalkCoordinator;
            set {
                _IsPublicTalkCoordinator = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsAccountServant Property
        private bool _IsAccountServant = default;
        public bool IsAccountServant {
            get => _IsAccountServant;
            set {
                _IsAccountServant = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsAccountServantAssistant Property
        private bool _IsAccountServantAssistant = default;
        public bool IsAccountServantAssistant {
            get => _IsAccountServantAssistant;
            set {
                _IsAccountServantAssistant = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsSoundServant Property
        private bool _IsSoundServant = default;
        public bool IsSoundServant {
            get => _IsSoundServant;
            set {
                _IsSoundServant = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsSoundServantAssistant Property
        private bool _IsSoundServantAssistant = default;
        public bool IsSoundServantAssistant {
            get => _IsSoundServantAssistant;
            set {
                _IsSoundServantAssistant = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsGroupOverseer Property
        private bool _IsGroupOverseer = default;
        public bool IsGroupOverseer {
            get => _IsGroupOverseer;
            set {
                _IsGroupOverseer = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsGroupServant Property
        private bool _IsGroupServant = default;
        public bool IsGroupServant {
            get => _IsGroupServant;
            set {
                _IsGroupServant = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsGroupAssistant Property
        private bool _IsGroupAssistant = default;
        public bool IsGroupAssistant {
            get => _IsGroupAssistant;
            set {
                _IsGroupAssistant = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsWatchtowerConductor Property
        private bool _IsWatchtowerConductor = default;
        public bool IsWatchtowerConductor {
            get => _IsWatchtowerConductor;
            set {
                _IsWatchtowerConductor = value;
                OnPropertyChanged();
            }
        }
        #endregion

        //CAN Area

        #region CanGiveMinistryParts Property
        private bool _CanGiveMinistryParts = default;
        public bool CanGiveMinistryParts {
            get => _CanGiveMinistryParts;
            set {
                _CanGiveMinistryParts = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CanBeLifeAndMinistryChairman Property
        private bool _CanBeLifeAndMinistryChairman = default;
        public bool CanBeLifeAndMinistryChairman {
            get => _CanBeLifeAndMinistryChairman;
            set {
                _CanBeLifeAndMinistryChairman = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CanBePublicMeetingChairman Property
        private bool _CanBePublicMeetingChairman = default;
        public bool CanBePublicMeetingChairman {
            get => _CanBePublicMeetingChairman;
            set {
                _CanBePublicMeetingChairman = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CanGiveTreasuresPart Property
        private bool _CanGiveTreasuresPart = default;
        public bool CanGiveTreasuresPart {
            get => _CanGiveTreasuresPart;
            set {
                _CanGiveTreasuresPart = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CanGiveGemsPart Property
        private bool _CanGiveGemsPart = default;
        public bool CanGiveGemsPart {
            get => _CanGiveGemsPart;
            set {
                _CanGiveGemsPart = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CanGiveBibleReadingPart Property
        private bool _CanGiveBibleReadingPart = default;
        public bool CanGiveBibleReadingPart {
            get => _CanGiveBibleReadingPart;
            set {
                _CanGiveBibleReadingPart = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CanGiveExplainBeliefsPart Property
        private bool _CanGiveExplainBeliefsPart = default;
        public bool CanGiveExplainBeliefsPart {
            get => _CanGiveExplainBeliefsPart;
            set {
                _CanGiveExplainBeliefsPart = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CanGiveLivingAsChristiansPart Property
        private bool _CanGiveLivingAsChristiansPart = default;
        public bool CanGiveLivingAsChristiansPart {
            get => _CanGiveLivingAsChristiansPart;
            set {
                _CanGiveLivingAsChristiansPart = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CanBeBibleStudyConductor Property
        private bool _CanBeBibleStudyConductor = default;
        public bool CanBeBibleStudyConductor {
            get => _CanBeBibleStudyConductor;
            set {
                _CanBeBibleStudyConductor = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CanBeGroupLead Property
        private bool _CanBeGroupLead = default;
        public bool CanBeGroupLead {
            get => _CanBeGroupLead;
            set {
                _CanBeGroupLead = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CanBeHallAttendant Property
        private bool _CanBeHallAttendant = default;
        public bool CanBeHallAttendant {
            get => _CanBeHallAttendant;
            set {
                _CanBeHallAttendant = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CanBeEntranceAttendant Property
        private bool _CanBeEntranceAttendant = default;
        public bool CanBeEntranceAttendant {
            get => _CanBeEntranceAttendant;
            set {
                _CanBeEntranceAttendant = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CanBeZoomHost Property
        private bool _CanBeZoomHost = default;
        public bool CanBeZoomHost {
            get => _CanBeZoomHost;
            set {
                _CanBeZoomHost = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CanRunConsole Property
        private bool _CanRunConsole = default;
        public bool CanRunConsole {
            get => _CanRunConsole;
            set {
                _CanRunConsole = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CanBeAMicrophoneHandler Property
        private bool _CanBeAMicrophoneHandler = default;
        public bool CanBeAMicrophoneHandler {
            get => _CanBeAMicrophoneHandler;
            set {
                _CanBeAMicrophoneHandler = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CanRunStage Property
        private bool _CanRunStage = default;
        public bool CanRunStage {
            get => _CanRunStage;
            set {
                _CanRunStage = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CanGiveLocalPublicTalks Property
        private bool _CanGiveLocalPublicTalks = default;
        public bool CanGiveLocalPublicTalks {
            get => _CanGiveLocalPublicTalks;
            set {
                _CanGiveLocalPublicTalks = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CanGiveOutgoingPublicTalks Property
        private bool _CanGiveOutgoingPublicTalks = default;
        public bool CanGiveOutgoingPublicTalks {
            get => _CanGiveOutgoingPublicTalks;
            set {
                _CanGiveOutgoingPublicTalks = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CanBeWatchtowerReader Property
        private bool _CanBeWatchtowerReader = default;
        public bool CanBeWatchtowerReader {
            get => _CanBeWatchtowerReader;
            set {
                _CanBeWatchtowerReader = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CanDoCartWitnessing Property
        private bool _CanDoCartWitnessing = default;
        public bool CanDoCartWitnessing {
            get => _CanDoCartWitnessing;
            set {
                _CanDoCartWitnessing = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public static MemberFlags GetNew () => new MemberFlags();

        public static MemberFlags FromXElement (XElement xElement) => xElement.ItemFromXElement<MemberFlags>();

        public override XElement ToXElement () => this.ItemToXElement();

    }
}
