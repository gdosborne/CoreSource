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
                HasChanges = HasChanges || prev != GenerateTopLevelDBEngineClass;
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
                HasChanges = HasChanges || prev != ImplementPropertyChanged;
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
                HasChanges = HasChanges || prev != Name;
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
                HasChanges = HasChanges || prev != Description;
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

        public XElement ToXElement() {
            var result = new XElement(nameof(Database),
                new XAttribute(nameof(Name), Name),
                new XElement(nameof(Description), Description));
            var settingElem = new XElement("Settings",
                new XAttribute(nameof(GenerateTopLevelDBEngineClass), GenerateTopLevelDBEngineClass.ToString()),
                new XAttribute(nameof(ImplementPropertyChanged), ImplementPropertyChanged.ToString()));

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
                    } else {
                        db.GenerateTopLevelDBEngineClass = true;
                        db.ImplementPropertyChanged = true;
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
