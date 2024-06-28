using Common.MVVMFramework;

using OzFramework.Timers;

using OzMiniDB.Builder.Helper;
using OzMiniDB.Items;

using System.ComponentModel;

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
                var dbGenSettings = new SettingGroup {
                    Name = App.Constants.Database,
                };
                var nameValue = new SettingValue(App.Constants.Name, Database.Name,
                    App.Constants.TheDBNameTip);
                dbGenSettings.Values.Add(nameValue);
                var filenameValue = new SettingValue(App.Constants.FileName, new SysIO.FileInfo(Database.Filename),
                    App.Constants.PathToDBDefFileTip);
                dbGenSettings.Values.Add(filenameValue);
                var genEngineValue = new SettingValue(App.Constants.GenTopLevelDBEClass, Database.GenerateTopLevelDBEngineClass,
                    App.Constants.GenTopLevDBDClassTip);
                dbGenSettings.Values.Add(genEngineValue);
                var genImplementPropertyChanged = new SettingValue(App.Constants.ImplementPropertyChanged, Database.ImplementPropertyChanged,
                    App.Constants.ImplementPropertyChangedTip);
                dbGenSettings.Values.Add(genImplementPropertyChanged);
                var genListType = new SettingValue(App.Constants.ListType, Database.ListType, App.Constants.ListTypeTip);
                dbGenSettings.Values.Add(genListType);
                var genPartialMethodsType = new SettingValue(App.Constants.PartialMethods, Database.MethodNames, App.Constants.PartialMethodTip);
                dbGenSettings.Values.Add(genPartialMethodsType);

                Groups.Add(dbGenSettings);

                var uiGenSettings = new SettingGroup {
                    Name = App.Constants.UserInterface,
                };
                var saveLocationValue = new SettingValue(App.Constants.SaveWinSizeAndLoc,
                    App.Session.ApplicationSettings.GetValue(App.Constants.Application, App.Constants.SaveWinSizeAndLoc, true));
                uiGenSettings.Values.Add(saveLocationValue);
                Groups.Add(uiGenSettings);

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

        #region SelectedGroup Property
        private SettingGroup _SelectedGroup = default;
        public SettingGroup SelectedGroup {
            get => _SelectedGroup;
            set {
                _SelectedGroup = value;
                OnPropertyChanged();
            }
        }
        #endregion

    }
}
