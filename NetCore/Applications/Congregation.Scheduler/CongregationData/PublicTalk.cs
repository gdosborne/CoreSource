using CongregationData.Helpers;

using System.Xml.Linq;

namespace CongregationData {
    public class PublicTalk : DomainItem {
        private PublicTalk () { }

        #region Title Property
        private string _Title = default;
        public string Title {
            get => _Title;
            set {
                _Title = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Number Property
        private ushort _Number = ushort.MinValue;
        public ushort Number {
            get => _Number;
            set {
                _Number = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public static PublicTalk GetNew (ushort? number = default, string title = default) =>
            new PublicTalk {
                Number = number.HasValue ? number.Value : default,
                Title = title
            };

        public static PublicTalk FromXElement (XElement xElement) => xElement.ItemFromXElement<PublicTalk>();

        public override XElement ToXElement () => this.ItemToXElement();
    }
}
