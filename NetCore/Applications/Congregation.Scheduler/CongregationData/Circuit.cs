using CongregationData.Helpers;

using System.Collections.ObjectModel;
using System.Xml.Linq;

using Universal.Common;


namespace CongregationData {
    public class Circuit : DomainItem {
        private Circuit () {
            Congregations = new ObservableCollection<Congregation>();
        }

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

        #region Congregations Property
        private ObservableCollection<Congregation> _Congregations = default;
        public ObservableCollection<Congregation> Congregations {
            get => _Congregations;
            set {
                _Congregations = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public static Circuit GetNew (string name = default, params Congregation[] congregations) {
            var result = new Circuit {
                Name = name
            };
            congregations.ForEach(x => result.Congregations.Add(x));
            return result;
        }

        public static Circuit FromXElement (XElement xElement) => xElement.ItemFromXElement<Circuit>();

        public override XElement ToXElement () => this.ItemToXElement();

    }
}
