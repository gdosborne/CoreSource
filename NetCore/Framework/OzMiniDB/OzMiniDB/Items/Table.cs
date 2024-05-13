using OzFramework.Primitives;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

using Universal.Common;

namespace OzMiniDB.Items {
    public class Table : INotifyPropertyChanged, IXElementItem {
        public new event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = default) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public Table() {
            Fields = [];
            ForeignKeys = [];
            Fields.CollectionChanged += (s, e) => {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add) {
                    foreach (Field field in e.NewItems) {
                        field.PropertyChanged += (s, e) => {
                            if (e.PropertyName != nameof(HasChanges)) {
                                OnPropertyChanged(e.PropertyName);
                            }
                        };
                    }
                }
            };
            ForeignKeys.CollectionChanged += (s, e) => {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add) {
                    foreach (ForeignKey fKey in e.NewItems) {
                        fKey.PropertyChanged += (s, e) => {
                            if (e.PropertyName != nameof(HasChanges)) {
                                OnPropertyChanged(e.PropertyName);
                            }
                        };
                    }
                }
            };
        }

        #region Fields Property
        private ObservableCollection<Field> _Fields = default;
        public ObservableCollection<Field> Fields {
            get => _Fields;
            set {
                _Fields = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ForeignKeys Property
        private ObservableCollection<ForeignKey> _ForeignKeys = default;
        public ObservableCollection<ForeignKey> ForeignKeys {
            get => _ForeignKeys;
            set {
                _ForeignKeys = value;
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
                Fields.ToList().ForEach(f => {
                    f.HasChanges = HasChanges;
                });
                ForeignKeys.ToList().ForEach(f => {
                    f.HasChanges = HasChanges;
                });
                OnPropertyChanged();
            }
        }
        #endregion

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

        public static Table Create(string name, string description) {
            return new Table {
                Name = name,
                Description = description
            };
        }

        public static Table FromXElement(XElement element) {
            var table = new Table {
                Name = element.Attribute("Name").Value,
                Description = element.Element("Description").Value
            };
            var fieldsElem = element.Element("Fields");
            foreach (var field in fieldsElem.Elements()) {
                var fld = Field.FromXElement(field);
                table.Fields.Add(fld);
            }
            var fKeyElem = element.Element("ForeignKeys");
            foreach (var fkey in fKeyElem.Elements()) {
                var key = ForeignKey.FromXElement(fkey);
                table.ForeignKeys.Add(key);
            }
            table.HasChanges = false;
            return table;
        }

        public XElement ToXElement() {
            var result = new XElement(nameof(Table),
                new XAttribute(nameof(Name), Name),
                new XElement(nameof(Description), Description));
            var fieldsElem = new XElement(nameof(Fields));
            foreach (var item in Fields) {
                fieldsElem.Add(item.ToXElement());
            }
            result.Add(fieldsElem);
            var fKeysElem = new XElement(nameof(ForeignKeys));
            foreach (var item in ForeignKeys) {
                fKeysElem.Add(item.ToXElement());
            }
            result.Add(fKeysElem);
            return result;
        }
    }
}
