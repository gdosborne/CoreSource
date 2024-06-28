using OzFramework.Primitives;
using OzFramework.Text;
using OzFramework.Xml.Linq;

using OzMiniDB.EventHandling;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.Linq;

using Universal.Common;

namespace OzMiniDB.Items {
    public class Database : INotifyPropertyChanged, IXElementItem {
        public enum ListTypes {
            IListOfType,
            IEnumerableOfType,
            ObservableCollectionOfType
        }

        [Flags]
        public enum PartialMethodNames {
            None = 0,
            InsertItem = 1,
            UpdateItem = 2,
            DeleteItem = 4,
        }

        public new event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = default) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public Database() {
            Tables = [];
            Tables.CollectionChanged += (s, e) => {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add) {
                    foreach (Table table in e.NewItems) {
                        table.PropertyChanged += (s, e) => {
                            if (e.PropertyName != nameof(HasChanges)) {
                                OnPropertyChanged(e.PropertyName);
                                HasChanges = true;
                            }
                        };
                    }
                }
            };
            ClassesDirectory = "Domain";
            ClassTemplateFilename = "class.template";
            StandardPropertyTemplateFilename = "standardproperty.template";
            NotificationPropertyTemplateFilename = "notificationproperty.template";
            HasChanges = false;
        }
        public event AskReplaceDatabaseFilenameHandler AskReplaceDatabaseFilename;
        public event AskCreateDatabaseHandler AskCreateDatabase;
        public ObservableCollection<Table> Tables { get; set; }

        #region GenerateTopLevelDBEngineClass Property
        private bool _GenerateTopLevelDBEngineClass = default;
        public bool GenerateTopLevelDBEngineClass {
            get => _GenerateTopLevelDBEngineClass;
            set {
                var prev = GenerateTopLevelDBEngineClass;
                _GenerateTopLevelDBEngineClass = value;
                HasChanges |= prev != GenerateTopLevelDBEngineClass;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ImplementPropertyChanged Property
        private bool _ImplementPropertyChanged = default;
        public bool ImplementPropertyChanged {
            get => _ImplementPropertyChanged;
            set {
                var prev = ImplementPropertyChanged;
                _ImplementPropertyChanged = value;
                HasChanges |= prev != ImplementPropertyChanged;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ImplementedPartialMethods Property
        private PartialMethodNames _ImplementedPartialMethods = default;
        public PartialMethodNames ImplementedPartialMethods {
            get => _ImplementedPartialMethods;
            set {
                var prev = ImplementedPartialMethods;
                _ImplementedPartialMethods = value;
                HasChanges |= prev != ImplementedPartialMethods;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ListType Property
        private ListTypes _ListType = default;
        public ListTypes ListType {
            get => _ListType;
            set {
                var prev = ListType;
                _ListType = value;
                HasChanges |= prev != ListType;
                OnPropertyChanged();
            }
        }
        #endregion

        #region MethodNames Property
        private PartialMethodNames _MethodNames = default;
        public PartialMethodNames MethodNames {
            get => _MethodNames;
            set {
                var prev = MethodNames;
                _MethodNames = value;
                HasChanges |= prev != MethodNames;
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
                HasChanges |= prev != Name;
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
                HasChanges |= prev != Description;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Filename Property
        private string _Filename = default;
        public string Filename {
            get => _Filename;
            set {
                _Filename = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region HasChanges Property
        private bool _HasChanges = default;
        public bool HasChanges {
            get => _HasChanges;
            set {
                _HasChanges = value;
                Tables.ForEach(t => {
                    t.HasChanges = HasChanges;
                });
                OnPropertyChanged();
            }
        }
        #endregion

        #region ClassesPath Property
        private string _ClassesPath = default;
        public string ClassesPath {
            get => _ClassesPath;
            set {
                var prev = ClassesPath;
                _ClassesPath = value;
                HasChanges |= prev != ClassesPath;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ClassesDirectory Property
        private string _ClassesDirectory = default;
        public string ClassesDirectory {
            get => _ClassesDirectory;
            set {
                _ClassesDirectory = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ClassTemplateFilename Property
        private string _ClassTemplateFilename = default;
        public string ClassTemplateFilename {
            get => _ClassTemplateFilename;
            set {
                _ClassTemplateFilename = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region StandardPropertyTemplateFilename Property
        private string _StandardPropertyTemplateFilename = default;
        public string StandardPropertyTemplateFilename {
            get => _StandardPropertyTemplateFilename;
            set {
                _StandardPropertyTemplateFilename = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region NotificationPropertyTemplateFilename Property
        private string _NotificationPropertyTemplateFilename = default;
        public string NotificationPropertyTemplateFilename {
            get => _NotificationPropertyTemplateFilename;
            set {
                _NotificationPropertyTemplateFilename = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public XElement ToXElement() {
            var result = new XElement(nameof(Database),
                new XAttribute(nameof(Name), Name),
                new XElement(nameof(Description), Description));
            var settingElem = new XElement("Settings",
                new XAttribute(nameof(GenerateTopLevelDBEngineClass), GenerateTopLevelDBEngineClass.ToString()),
                new XAttribute(nameof(ImplementPropertyChanged), ImplementPropertyChanged.ToString()),
                new XAttribute(nameof(ClassesPath), ClassesPath),
                new XAttribute(nameof(ListType), ListType.ToString()));

            result.Add(settingElem);
            var tablesElem = new XElement("Tables");
            foreach (var item in Tables) {
                tablesElem.Add(item.ToXElement());
            }
            result.Add(tablesElem);
            return result;
        }

        public static Database Open(string filename) {
            var data = default(string);
            using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                using var reader = new StreamReader(fs);
                data = reader.ReadToEnd();
            }
            var db = default(Database);
            if (!string.IsNullOrWhiteSpace(data)) {
                var doc = XDocument.Parse(data);
                db = new Database {
                    Filename = filename,
                };
                var root = doc.Root;
                if (root.LocalName().EqualsIgnoreCase("Database")) {
                    db.Name = root.Attribute("Name").Value;
                    db.Description = root.Element("Description").Value;
                    var settingsElem = root.Element("Settings");
                    if (!settingsElem.IsNull()) {
                        db.GenerateTopLevelDBEngineClass = settingsElem.Attribute(nameof(GenerateTopLevelDBEngineClass)).IsNull()
                            ? true : bool.Parse(settingsElem.Attribute(nameof(GenerateTopLevelDBEngineClass)).Value);
                        db.ImplementPropertyChanged = settingsElem.Attribute(nameof(ImplementPropertyChanged)).IsNull()
                           ? true : bool.Parse(settingsElem.Attribute(nameof(ImplementPropertyChanged)).Value);
                        db.ClassesPath = settingsElem.Attribute(nameof(ClassesPath)).IsNull()
                            ? default 
                            : settingsElem.Attribute(nameof(ClassesPath)).Value;
                        db.ListType = settingsElem.Attribute(nameof(ListType)).IsNull()
                            ? ListTypes.IListOfType                            
                            : (ListTypes)Enum.Parse(typeof(ListTypes), settingsElem.Attribute(nameof(ListType)).Value);
                        db.ImplementedPartialMethods = settingsElem.Attribute(nameof(ImplementedPartialMethods)).IsNull()
                            ? PartialMethodNames.None
                            : (PartialMethodNames)Enum.Parse(typeof(PartialMethodNames), settingsElem.Attribute(nameof(ImplementedPartialMethods)).Value);
                    } else {
                        db.GenerateTopLevelDBEngineClass = true;
                        db.ImplementPropertyChanged = true;
                        db.ListType = ListTypes.IListOfType;
                    }

                    var tableElem = root.Element("Tables");
                    var temp = new List<Table>();
                    if (!tableElem.IsNull()) {
                        foreach (var t in tableElem.Elements()) {
                            var table = Table.FromXElement(t);
                            temp.Add(table);
                        }
                    }
                    if (temp.Count != 0) {
                        db.Tables.AddRange(temp.OrderBy(x => x.Name));
                    }
                }
            }
            db.HasChanges = false;
            return db;
        }

        public void Save(string filename) {
            if (string.IsNullOrWhiteSpace(filename) || Filename.EqualsIgnoreCase(filename)) {
                Save();
                return;
            }
            Filename = filename;
            if (File.Exists(Filename)) {
                var e = new AskReplaceDatabaseFilenameEventArgs(Filename);
                AskReplaceDatabaseFilename?.Invoke(this, e);
                if (e.IsReplaceSet) {
                    Filename = filename;
                    Save();
                }
            } else
                Save();
        }
        public void Save() {
            if (string.IsNullOrWhiteSpace(Filename)) {
                var e = new AskCreateDatabaseEventArgs();
                AskCreateDatabase?.Invoke(this, e);
                if (string.IsNullOrWhiteSpace(e.Filename)) {
                    return;
                }
                Filename = e.Filename;
                SaveDatabase(e.Filename);
                return;
            }
            SaveDatabase();
        }

        private void SaveDatabase() {
            var element = this.ToXElement();
            var doc = new XDocument(element);

            using var writer = new XmlTextWriter(Filename, new UTF8Encoding(false));
            doc.Save(writer);
            HasChanges = false;
        }

        private void SaveDatabase(string filename) {
            SaveDatabase(filename);
        }
    }
}
