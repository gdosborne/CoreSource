using Common.MVVMFramework;

using OzMiniDB.Builder.Helper;
using OzMiniDB.Items;

namespace OzMiniDB.Builder {
    public partial class DatabaseSettingsWindowView : ViewModelBase {
        public DatabaseSettingsWindowView() {
            Title = "Settings [designer]";
        }

        public override void Initialize() {
            base.Initialize();
            Title = "Settings";
            Groups = [];

        }

        #region DialogResult Property
        private bool _DialogResult = default;
        public bool DialogResult {
            get => _DialogResult;
            set {
                _DialogResult = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Database Property
        private Database _Database = default;
        public Database Database {
            get => _Database;
            set {
                _Database = value;
                var dbSettings = new SettingGroup {
                    Name = "Database"
                };
                var dbGenSettings = new SettingGroup {
                    Name = App.Constants.General,
                    Parent = dbSettings
                };
                dbSettings.Groups.Add(dbGenSettings);
                var nameValue = new SettingValue(App.Constants.Name, Database.Name,
                    App.Constants.TheDBNameTip);
                dbGenSettings.Values.Add(nameValue);
                var filenameValue = new SettingValue(App.Constants.FileName, new SysIO.FileInfo(Database.Filename),
                    App.Constants.PathToDBDefFileTip);
                dbGenSettings.Values.Add(filenameValue);
                var genEngineValue = new SettingValue(App.Constants.GenTopLevelDBEClass, Database.GenerateTopLevelDBEngineClass,
                    App.Constants.GenTopLevDBDClassTip);
                dbGenSettings.Values.Add(genEngineValue);
                Groups.Add(dbSettings);
                var uiSettings = new SettingGroup {
                    Name = App.Constants.UserInterface
                };
                var uiGenSettings = new SettingGroup {
                    Name = App.Constants.General,
                    Parent = uiSettings
                };
                var saveLocationValue = new SettingValue(App.Constants.SaveWinSizeAndLoc,
                    App.Session.ApplicationSettings.GetValue(App.Constants.Application, App.Constants.SaveWinSizeAndLoc, true));
                uiGenSettings.Values.Add(saveLocationValue);
                uiSettings.Groups.Add(uiGenSettings);
                Groups.Add(uiSettings);
                OnPropertyChanged();
            }
        }
        #endregion

        #region Groups Property
        private List<SettingGroup> _Groups = default;
        public List<SettingGroup> Groups {
            get => _Groups;
            set {
                _Groups = value;
                OnPropertyChanged();
            }
        }
        #endregion

    }
}
