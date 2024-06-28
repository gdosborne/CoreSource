using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

namespace OzMiniDB.Items {
    public class Field : Implementable, INotifyPropertyChanged, IXElementItem {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = default) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public enum DBDataType {
            String = 1,
            FixedString,
            Note,
            WholeNumber,
            DecimalNumber,
            Boolean,
            Guid,
            Date,
            TimeSpan,
            DateTime
        }

        public Field() {
            DataTypes = new ObservableCollection<DBDataType>(Enum.GetNames(typeof(DBDataType))
                .Where(x => !x.Equals("None"))
                .OrderBy(x => x)
                .Select(x => (DBDataType)Enum.Parse(typeof(DBDataType), x)));
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

        #region DataTypes Property
        private ObservableCollection<DBDataType> _DataTypes = default;
        public ObservableCollection<DBDataType> DataTypes {
            get => _DataTypes;
            set {
                _DataTypes = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region DataType Property
        private DBDataType _DataType = default;
        public DBDataType DataType {
            get => _DataType;
            set {
                var prev = DataType;
                _DataType = value;
                if (DataType != DBDataType.String && DataType != DBDataType.FixedString) {
                    FieldLength = 0;
                }
                HasChanges = prev != DataType;
                OnPropertyChanged();
            }
        }
        #endregion

        #region FieldLength Property
        private int _FieldLength = default;
        public int FieldLength {
            get => _FieldLength;
            set {
                var prev = FieldLength;
                _FieldLength = value;
                HasChanges = prev != FieldLength;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsRequired Property
        private bool _IsRequired = default;
        public bool IsRequired {
            get => _IsRequired;
            set {
                var prev = IsRequired;
                _IsRequired = value;
                HasChanges = prev != IsRequired;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsIdentity Property
        private bool _IsIdentity = default;
        public bool IsIdentity {
            get => _IsIdentity;
            set {
                var prev = IsIdentity;
                _IsIdentity = value;
                HasChanges = prev != IsIdentity;
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

        #region IsSelected Property
        private bool _IsSelected = default;
        public bool IsSelected {
            get => _IsSelected;
            set {
                _IsSelected = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public static Field FromXElement(XElement element) {
            var name = element.Attribute(nameof(Name)).Value;
            var description = element.Element(nameof(Description)).Value;
            var dt = element.Element(nameof(DataType)).Value;
            var dataType = (DBDataType)Enum.Parse(typeof(DBDataType), dt);
            var isRequired = bool.Parse(element.Element(nameof(IsRequired)).Value);
            var isIdentity = bool.Parse(element.Element(nameof(IsIdentity)).Value);
            var field = new Field {
                Name = name,
                Description = description,
                DataType = dataType,
                IsRequired = isRequired,
                IsIdentity = isIdentity
            };
            var fl = element.Element(nameof(FieldLength)).Value;
            if (!string.IsNullOrWhiteSpace(fl)) {
                field.FieldLength = int.Parse(fl);
            }
            field.HasChanges = false;
            return field;
        }

        public XElement ToXElement() {
            var result = new XElement(nameof(Field),
                new XAttribute(nameof(Name), Name),
                new XElement(nameof(Description), Description));
            result.Add(new XElement(nameof(DataType), DataType.ToString()));
            result.Add(new XElement(nameof(FieldLength), FieldLength.ToString()));
            result.Add(new XElement(nameof(IsRequired), IsRequired.ToString()));
            result.Add(new XElement(nameof(IsIdentity), IsIdentity.ToString()));
            return result;
        }

        public override StringBuilder GetText(bool isImplementNotification, string classTemplateFilenamestring, 
                string standardPropertyTemplateFilename, string notificationPropertyTemplateFilename, string databaseName) {
            var dataType = DataType(DataType, IsRequired);
            var name = Name.Replace(" ", "_");
            var templatePath = isImplementNotification
                ? notificationPropertyTemplateFilename
                : standardPropertyTemplateFilename;

            var template = GetTemplateText(templatePath)
                .Replace(Tags.DataType, dataType)
                .Replace(Tags.PropertyName, name);

            return template;
        }
    }
}
