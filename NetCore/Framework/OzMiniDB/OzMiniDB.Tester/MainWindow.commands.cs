using Common.MVVMFramework;

using OzFramework.Primitives;

using System.Windows.Threading;

using Universal.Common;

namespace OzMiniDB.Builder {
    public partial class MainWindowView {
        public enum ActionTypes {
            AskSaveDatabase,
            AskReplaceDatabase,
            AskCreateNewDatabase,
            AskOpenDatabase,
            AddNewTable,
            ShowSettings
        }

        private void ExecuteAction(ActionTypes actionType) =>
            ExecuteAction(actionType.ToString());

        private void ExecuteAction(ActionTypes actionType, Dictionary<string, object> parameters) =>
            ExecuteAction(actionType.ToString(), parameters);

        #region AddTable Command
        private DelegateCommand _AddTableCommand = default;
        public DelegateCommand AddTableCommand => _AddTableCommand ??= new DelegateCommand(AddTable, ValidateAddTableState);
        private bool ValidateAddTableState(object state) => !Database.IsNull();
        private void AddTable(object state) {
            ExecuteAction(ActionTypes.AddNewTable);
        }
        #endregion

        #region RemoveTable Command
        private DelegateCommand _RemoveTableCommand = default;
        public DelegateCommand RemoveTableCommand => _RemoveTableCommand ??= new DelegateCommand(RemoveTable, ValidateRemoveTableState);
        private bool ValidateRemoveTableState(object state) => !Database.IsNull() && !SelectedTable.IsNull();
        private void RemoveTable(object state) {

        }
        #endregion

        #region NewDatabase Command
        private DelegateCommand _NewDatabaseCommand = default;
        public DelegateCommand NewDatabaseCommand => _NewDatabaseCommand ??= new DelegateCommand(NewDatabase, ValidateNewDatabaseState);
        private bool ValidateNewDatabaseState(object state) => true;
        private void NewDatabase(object state) {
            if (!Database.IsNull()) {
                if (Database.HasChanges) {
                    var p = new Dictionary<string, object> {
                        { "filename", Database.Filename },
                        { "cancel", false }
                    };
                    ExecuteAction(ActionTypes.AskSaveDatabase, p);
                }
            }
            Database = new Items.Database() { Name = "New Database" };
        }
        #endregion

        #region OpenDatabase Command
        private DelegateCommand _OpenDatabaseCommand = default;
        public DelegateCommand OpenDatabaseCommand => _OpenDatabaseCommand ??= new DelegateCommand(OpenDatabase, ValidateOpenDatabaseState);
        private bool ValidateOpenDatabaseState(object state) => true;
        private void OpenDatabase(object state) {
            var filename = string.Empty;
            var p = default(Dictionary<string, object>);
            if (!Database.IsNull() && Database.HasChanges) {
                if (string.IsNullOrEmpty(Database.Filename)) {
                    p = new Dictionary<string, object> {
                        { "filename", string.Empty },
                        { "cancel", false }
                    };
                    ExecuteAction(ActionTypes.AskSaveDatabase, p);
                    filename = p["filename"].CastTo<string>();
                    if (!p["cancel"].CastTo<bool>() && !string.IsNullOrEmpty(filename)) {
                        Database.Save(filename);
                    } else if (p["cancel"].CastTo<bool>() || string.IsNullOrEmpty(filename)) {
                        return;
                    }
                } else {
                    Database.Save();
                }
            }
            Database = null;
            p = new Dictionary<string, object> {
                { "filename", string.Empty },
                { "cancel", false }
            };
            ExecuteAction(ActionTypes.AskOpenDatabase, p);
            filename = p["filename"].CastTo<string>();
            if (p["cancel"].CastTo<bool>())
                return;
            Database = Items.Database.Open(filename);
            UpdateInterface();
        }
        #endregion

        #region SaveDatabase Command
        private DelegateCommand _SaveDatabaseCommand = default;
        public DelegateCommand SaveDatabaseCommand => _SaveDatabaseCommand ??= new DelegateCommand(SaveDatabase, ValidateSaveDatabaseState);
        private bool ValidateSaveDatabaseState(object state) {
            var result = !Database.IsNull() && Database.HasChanges;
            if (!Database.IsNull() && Database.Tables.Any()) {
                Database.Tables.ForEach(t => {
                    result |= t.HasChanges;
                    if (t.Fields.Any()) {
                        t.Fields.ForEach(f => {
                            result |= f.HasChanges;
                        });
                    }
                    if (t.ForeignKeys.Any()) {
                        t.ForeignKeys.ForEach(f => {
                            result |= f.HasChanges;
                        });
                    }
                });
            }
            return result;
        }
        private void SaveDatabase(object state) {
            if (!Database.IsNull() && string.IsNullOrWhiteSpace(Database.Filename)) {
                var p = new Dictionary<string, object> {
                    { "filename", Database.Filename },
                    { "cancel", false }
                };
                ExecuteAction(ActionTypes.AskSaveDatabase, p);
                var filename = p["filename"].CastTo<string>();
                if (!p["cancel"].CastTo<bool>() && !string.IsNullOrEmpty(filename)) {
                    Database.Save(filename);
                }
            } else {
                Database.Save();
            }
            var t = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            t.Tick += (s, e) => {
                s.As<DispatcherTimer>().Stop();
                Database.HasChanges = false;
                UpdateInterface();
            };
            t.Start();
        }
        #endregion

        #region AddField Command
        private DelegateCommand _AddFieldCommand = default;
        public DelegateCommand AddFieldCommand => _AddFieldCommand ??= new DelegateCommand(AddField, ValidateAddFieldState);
        private bool ValidateAddFieldState(object state) => !Database.IsNull() && !SelectedTable.IsNull();
        private void AddField(object state) {
            SelectedTable.Fields.Add(new Items.Field {
                Name = "_NewField_",
                Description = string.Empty,
                DataType = Items.Field.DBDataType.WholeNumber
            });
        }
        #endregion

        #region RemoveField Command
        private DelegateCommand _RemoveFieldCommand = default;
        public DelegateCommand RemoveFieldCommand => _RemoveFieldCommand ??= new DelegateCommand(RemoveField, ValidateRemoveFieldState);
        private bool ValidateRemoveFieldState(object state) => !Database.IsNull() && !SelectedTable.IsNull() && SelectedTable.Fields.Count > 1;
        private void RemoveField(object state) {

        }
        #endregion

        #region GenerateClasses Command
        private DelegateCommand _GenerateClassesCommand = default;
        public DelegateCommand GenerateClassesCommand => _GenerateClassesCommand ??= new DelegateCommand(GenerateClasses, ValidateGenerateClassesState);
        private bool ValidateGenerateClassesState(object state) => !Database.IsNull();
        private void GenerateClasses(object state) {

        }
        #endregion

        #region DBSettings Command
        private DelegateCommand _DBSettingsCommand = default;
        public DelegateCommand DBSettingsCommand => _DBSettingsCommand ??= new DelegateCommand(DBSettings, ValidateDBSettingsState);
        private bool ValidateDBSettingsState(object state) => !Database.IsNull();
        private void DBSettings(object state) {
            ExecuteAction(ActionTypes.ShowSettings);
        }
        #endregion

    }
}
