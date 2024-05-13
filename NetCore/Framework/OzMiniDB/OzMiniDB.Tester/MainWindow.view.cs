using Common.MVVMFramework;

using OzFramework.Primitives;

using OzMiniDB.Items;

using System.Windows;

namespace OzMiniDB.Builder {
    public partial class MainWindowView : ViewModelBase {
        public MainWindowView() {
            Title = "OzMiniDB Tester [designer]";
            IsTableDesignerEnabled = false;
        }

        public override void Initialize() {
            base.Initialize();
            Title = "OzMiniDB Tester";
            IsMainGroupEnabled = false;
        }

        #region Database Property
        private Database _Database = default;
        public Database Database {
            get => _Database;
            set {
                if (!Database.IsNull()) {
                    if (Database.HasChanges) {
                        Database.Save();
                    }
                    Database.AskReplaceDatabaseFilename -= (s, e) => { };
                }
                _Database = value;
                if (!Database.IsNull()) {
                    Database.AskReplaceDatabaseFilename += (s, e) => {
                        var p = new Dictionary<string, object> {
                            { App.Constants.Fname, e.Filename },
                            { App.Constants.Cancel, false }
                        };
                        ExecuteAction(ActionTypes.AskReplaceDatabase, p);
                        e.IsReplaceSet = !p[App.Constants.Cancel].CastTo<bool>();
                    };
                    Database.AskCreateDatabase += (s, e) => {
                        if (!string.IsNullOrWhiteSpace(e.Filename) && SysIO.File.Exists(e.Filename)) {
                            var p = new Dictionary<string, object> {
                                { App.Constants.Fname, e.Filename },
                                { App.Constants.Cancel, false }
                            };
                            ExecuteAction(ActionTypes.AskReplaceDatabase, p);
                            e.Filename = p[App.Constants.Fname].As<string>();
                            e.IsCreateSet = !p[App.Constants.Cancel].CastTo<bool>();
                        } else {
                            var p = new Dictionary<string, object> {
                                { App.Constants.Fname, e.Filename },
                                { App.Constants.Cancel, false }
                            };
                            ExecuteAction(ActionTypes.AskCreateNewDatabase, p);
                            e.Filename = p[App.Constants.Fname].As<string>();
                            e.IsCreateSet = !p[App.Constants.Cancel].CastTo<bool>();
                        }
                    };
                }
                IsMainGroupEnabled = !Database.IsNull();
                IsTableDesignerEnabled = !Database.IsNull();
                if (IsTableDesignerEnabled) {
                    Database.PropertyChanged += (s, e) => {
                        UpdateInterface();
                    };
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region SelectedTable Property
        private Table _SelectedTable = default;
        public Table SelectedTable {
            get => _SelectedTable;
            set {
                _SelectedTable = value;
                IsTableDesignerEnabled = !SelectedTable.IsNull();
                FieldsMessageVisibility = !SelectedTable.IsNull() ? Visibility.Collapsed : Visibility.Visible;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsTableDesignerEnabled Property
        private bool _IsTableDesignerEnabled = default;
        public bool IsTableDesignerEnabled {
            get => _IsTableDesignerEnabled;
            set {
                _IsTableDesignerEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsMainGroupEnabled Property
        private bool _IsMainGroupEnabled = default;
        public bool IsMainGroupEnabled {
            get => _IsMainGroupEnabled;
            set {
                _IsMainGroupEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region FieldsMessageVisibility Property
        private Visibility _FieldsMessageVisibility = default;
        public Visibility FieldsMessageVisibility {
            get => _FieldsMessageVisibility;
            set {
                _FieldsMessageVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

    }
}
