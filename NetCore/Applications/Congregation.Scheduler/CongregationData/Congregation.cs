using CongregationData.Helpers;

using System.Collections.ObjectModel;
using System.Xml.Linq;

using Universal.Common;

namespace CongregationData {
    public class Congregation : DomainItem {
        private Congregation () {
            Members = new ObservableCollection<Member>();
        }

        #region Members Property
        private ObservableCollection<Member> _Members = default;
        public ObservableCollection<Member> Members {
            get => _Members;
            set {
                _Members = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Name Property
        private string _Name = default;
        public string Name {
            get => _Name;
            set {
                _Name = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Number Property
        private string _Number = default;
        public string Number {
            get => _Number;
            set {
                _Number = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Address Property
        private Address _Address = default;
        public Address Address {
            get => _Address;
            set {
                _Address = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region PhoneNumber Property
        private PhoneNumber _PhoneNumber = default;
        public PhoneNumber PhoneNumber {
            get => _PhoneNumber;
            set {
                _PhoneNumber = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public static Congregation GetNew (string name = default, string number = default, Address address = default, 
            PhoneNumber phoneNumber = default, params Member[] members) {
            var result = new Congregation {
                Name = name,
                Number = number,
                Address = address,
            };
            members.ForEach(result.Members.Add);
            return result;
        }

        public static Congregation FromXElement (XElement xElement) => xElement.ItemFromXElement<Congregation>();

        public override XElement ToXElement () => this.ItemToXElement();
    }
}
