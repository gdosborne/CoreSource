using CongregationData.Helpers;

using System.Xml.Linq;

using Windows.ApplicationModel.UserDataTasks.DataProvider;

namespace CongregationData {
    public class PhoneNumber : DomainItem {
        private PhoneNumber() { }

        #region CountryCode Property
        private byte? _CountryCode = default;
        public byte? CountryCode {
            get => _CountryCode;
            set {
                _CountryCode = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region AreaCode Property
        private ushort? _AreaCode = default;
        public ushort? AreaCode {
            get => _AreaCode;
            set {
                _AreaCode = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Prefix Property
        private ushort? _Prefix = default;
        public ushort? Prefix {
            get => _Prefix;
            set {
                _Prefix = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Suffix Property
        private ushort? _Suffix = default;
        public ushort? Suffix {
            get => _Suffix;
            set {
                _Suffix = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Extension Property
        private ushort? _Extension = default;
        public ushort? Extension {
            get => _Extension;
            set {
                _Extension = value;
                OnPropertyChanged();
            }
        }
        #endregion


        public static PhoneNumber GetNew (byte? countryCode = default, ushort? areaCode = default, ushort? prefix = default,
            ushort? suffix = default, ushort? extension = default) =>
            new PhoneNumber {
                CountryCode = countryCode,
                AreaCode = areaCode,
                Prefix = prefix,
                Suffix = suffix,
                Extension = extension
            };

        public static PhoneNumber FromXElement (XElement xElement) => xElement.ItemFromXElement<PhoneNumber>();

        public override XElement ToXElement () => this.ItemToXElement();

    }
}
