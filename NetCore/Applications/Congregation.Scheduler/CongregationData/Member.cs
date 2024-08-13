using CongregationData.Helpers;

using System.Xml.Linq;

namespace CongregationData {
    public class Member : DomainItem {

        private Member () { }

        #region MemberFlags Property
        private MemberFlags _MemberFlags = default;
        public MemberFlags MemberFlags {
            get => _MemberFlags;
            set {
                _MemberFlags = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region LastName Property
        private string _LastName = default;
        public string LastName {
            get => _LastName;
            set {
                _LastName = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region FirstName Property
        private string _FirstName = default;
        public string FirstName {
            get => _FirstName;
            set {
                _FirstName = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region NameSuffix Property
        private string _NameSuffix = default;
        public string NameSuffix {
            get => _NameSuffix;
            set {
                _NameSuffix = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region BaptismDate Property
        private DateTime? _BaptismDate = default;
        public DateTime? BaptismDate {
            get => _BaptismDate;
            set {
                _BaptismDate = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region MiddleNameInitial Property
        private string _MiddleNameInitial = default;
        public string MiddleNameInitial {
            get => _MiddleNameInitial;
            set {
                _MiddleNameInitial = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public static Member GetNew (string lastName = default, string firstName = default, string middleNameOrInitial = default,
            string nameSuffix = default, DateTime? baptismDate = default, MemberFlags flags = default) =>
            new Member {
                LastName = lastName,
                FirstName = firstName,
                MiddleNameInitial = middleNameOrInitial,
                NameSuffix = nameSuffix,
                BaptismDate = baptismDate,
                MemberFlags = flags
            };

        public static Member FromXElement (XElement xElement) => xElement.ItemFromXElement<Member>();

        public override XElement ToXElement () => this.ItemToXElement();
    }
}
