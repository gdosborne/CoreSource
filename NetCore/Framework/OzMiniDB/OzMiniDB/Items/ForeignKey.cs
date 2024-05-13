using Newtonsoft.Json;

using OzFramework.Primitives;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace OzMiniDB.Items {
    public class ForeignKey : INotifyPropertyChanged, IXElementItem {
        public new event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = default) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public enum CascadeTypes {
            None,
            Delete
        }

        #region Name Property
        private string _Name = default;
        public string Name {
            get => _Name;
            set {
                var prev = Name;
                _Name = value;
                HasChanges = prev != Name;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Description Property
        private string _Description = default;
        public string Description {
            get => _Description;
            set {
                var prev = Description;
                _Description = value;
                HasChanges = prev != Description;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ParentTableName Property
        private string _ParentTableName = default;
        public string ParentTableName {
            get => _ParentTableName;
            set {
                _ParentTableName = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ParentFieldName Property
        private string _ParentFieldName = default;
        public string ParentFieldName {
            get => _ParentFieldName;
            set {
                _ParentFieldName = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ChildFieldName Property
        private string _ChildFieldName = default;
        public string ChildFieldName {
            get => _ChildFieldName;
            set {
                _ChildFieldName = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CascadeType Property
        private CascadeTypes _CascadeType = default;
        public CascadeTypes CascadeType {
            get => _CascadeType;
            set {
                _CascadeType = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region HasChanges Property
        private bool _HasChanges = default;
        public bool HasChanges {
            get => _HasChanges;
            internal set {
                _HasChanges = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public static ForeignKey FromXElement(XElement element) {
            var fKey = new ForeignKey {
                Name = element.Attribute(nameof(Name)).Value,
                Description = element.Element(nameof(Description)).Value,
                ParentTableName = element.Element(nameof(ParentTableName)).Value,
                ParentFieldName = element.Element(nameof(ParentFieldName)).Value,
                ChildFieldName = element.Element(nameof(ChildFieldName)).Value,
                CascadeType = Enum.Parse(typeof(CascadeTypes), element.Element(nameof(CascadeType)).Value).CastTo<CascadeTypes>()
            };
            fKey.HasChanges = false;
            return fKey;
        }

        public XElement ToXElement() {
            var result = new XElement(nameof(ForeignKey),
                new XAttribute(nameof(Name), Name),
                new XElement(nameof(Description), Description));
            var fkeyData = new XElement("Data");
            fkeyData.Add(new XElement(nameof(ParentTableName), ParentTableName));
            fkeyData.Add(new XElement(nameof(ParentFieldName), ParentFieldName));
            fkeyData.Add(new XElement(nameof(ChildFieldName), ChildFieldName));
            fkeyData.Add(new XElement(nameof(CascadeType), CascadeType.ToString()));
            result.Add(fkeyData);
            return result;
        }

    }
}
