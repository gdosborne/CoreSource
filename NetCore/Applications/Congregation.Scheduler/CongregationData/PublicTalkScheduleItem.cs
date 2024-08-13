using CongregationData.Helpers;

using System.Xml.Linq;

namespace CongregationData {
    public class PublicTalkScheduleItem : ScheduleItem {
        private PublicTalkScheduleItem () { }

        public enum TalkDirections {
            In, Out
        }

        #region Direction Property
        private TalkDirections _Direction = default;
        public TalkDirections Direction {
            get => _Direction;
            set {
                _Direction = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Congregation Property
        private Congregation _Congregation = default;
        public Congregation Congregation {
            get => _Congregation;
            set {
                _Congregation = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public static PublicTalkScheduleItem GetNew (TalkDirections direction = default, Congregation congregation = default) =>
            new PublicTalkScheduleItem {
                Direction = direction,
                Congregation = congregation
            };

        public static PublicTalkScheduleItem FromXElement (XElement xElement) => xElement.ItemFromXElement<PublicTalkScheduleItem>();

        public override XElement ToXElement () => this.ItemToXElement();
    }
}
