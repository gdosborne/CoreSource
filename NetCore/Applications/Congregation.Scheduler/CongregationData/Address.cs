using CongregationData.Helpers;

using System.Xml.Linq;

namespace CongregationData {
    public class Address : DomainItem {
        private Address () { }

        #region Address1 Property
        private string _Address1 = default;
        public string Address1 {
            get => _Address1;
            set {
                _Address1 = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Address2 Property
        private string _Address2 = default;
        public string Address2 {
            get => _Address2;
            set {
                _Address2 = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region City Property
        private string _City = default;
        public string City {
            get => _City;
            set {
                _City = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region StateOrProvince Property
        private string _StateOrProvince = default;
        public string StateOrProvince {
            get => _StateOrProvince;
            set {
                _StateOrProvince = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Country Property
        private string _Country = default;
        public string Country {
            get => _Country;
            set {
                _Country = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region PostalCode Property
        private string _PostalCode = default;
        public string PostalCode {
            get => _PostalCode;
            set {
                _PostalCode = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public static Address GetNew (string address1 = default, string address2 = default, string city = default,
            string stateOrProvince = default, string country = default, string postalCode = default) => 
            new Address {
                Address1 = address1,
                Address2 = address2,
                City = city,
                StateOrProvince = stateOrProvince,
                Country = country,
                PostalCode = postalCode
            };

        public static Address FromXElement(XElement xElement) => xElement.ItemFromXElement<Address>();

        public override XElement ToXElement () => this.ItemToXElement();
    }
}
