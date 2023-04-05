using Common.Application.Primitives;
using Common.MVVMFramework;
using OzDB.Management;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace OzDBCreate.ViewModel {
    public class MainWindowView : ViewModelBase {
        public MainWindowView() {
            Title = $"{App.AppName} [designer]";
        }

        public override void Initialize() {
            base.Initialize();

            Title = App.AppName;
            Databases = new ObservableCollection<OzDBDatabase>();
        }

        #region commands
        internal enum Actions {
            AskSaveChanges,
            OpenDatabase,
            ShowProperties,
            AskNewFileName,
            AddTable,
            ShowSettings
        }

        #region ExitCommand
        private DelegateCommand _ExitCommand = default;
        /// <summary>Gets the Exit command.</summary>
        /// <value>The Exit command.</value>
        public DelegateCommand ExitCommand => _ExitCommand ??= new DelegateCommand(Exit, ValidateExitState);
        private bool ValidateExitState(object state) => true;
        private void Exit(object state) {
            Environment.Exit(0);
        }
        #endregion

        #region SaveAsCommand
        private DelegateCommand _SaveAsCommand = default;
        /// <summary>Gets the SaveAs command.</summary>
        /// <value>The SaveAs command.</value>
        public DelegateCommand SaveAsCommand => _SaveAsCommand ??= new DelegateCommand(SaveAs, ValidateSaveAsState);
        private bool ValidateSaveAsState(object state) => Databases.Any();
        private void SaveAs(object state) {
            ExecuteAction(nameof(Actions.AskNewFileName));
        }
        #endregion

        #region SaveCommand
        private DelegateCommand _SaveCommand = default;
        /// <summary>Gets the Save command.</summary>
        /// <value>The Save command.</value>
        public DelegateCommand SaveCommand => _SaveCommand ??= new DelegateCommand(Save, ValidateSaveState);
        private bool ValidateSaveState(object state) => SelectedDatabase != null && SelectedDatabase.HasChanges;
        private async void Save(object state) {
            try {
                await SelectedDatabase?.SaveAsync();
            }
            catch (Exception ex) {
                await App.HandleExceptionAsync(ex);
            }
        }
        #endregion

        #region NewCommand
        private DelegateCommand _NewCommand = default;
        /// <summary>Gets the New command.</summary>
        /// <value>The New command.</value>
        public DelegateCommand NewCommand => _NewCommand ??= new DelegateCommand(New, ValidateNewState);
        private bool ValidateNewState(object state) => true;
        private async void New(object state) {
            var name = "[New Database]";
            var id = Guid.NewGuid();
            var tempDirName = System.IO.Path.Combine(App.WorkingDirectory.FullName, $"{id}");
            SelectedDatabase = await OzDBDatabase.Create(name, tempDirName, id);
            SelectedDatabase.IconFontFamily = App.Current.Resources["AppIconFontFamily"].As<FontFamily>();
            Databases.Add(SelectedDatabase);
            ShowProperties(null);
            UpdateInterface();
        }
        #endregion

        #region OpenCommand
        private DelegateCommand _OpenCommand = default;
        /// <summary>Gets the Open command.</summary>
        /// <value>The Open command.</value>
        public DelegateCommand OpenCommand => _OpenCommand ??= new DelegateCommand(Open, ValidateOpenState);
        private bool ValidateOpenState(object state) => true;
        private void Open(object state) {
            ExecuteAction(nameof(Actions.OpenDatabase));
        }
        #endregion

        #region CloseCommand
        private DelegateCommand _CloseCommand = default;
        /// <summary>Gets the Close command.</summary>
        /// <value>The Close command.</value>
        public DelegateCommand CloseCommand => _CloseCommand ??= new DelegateCommand(Close, ValidateCloseState);
        private bool ValidateCloseState(object state) => SelectedDatabase != null;
        private void Close(object state) {
            if (SelectedDatabase == null) return;
            if (SelectedDatabase.HasChanges) {
                ExecuteAction(nameof(Actions.AskSaveChanges));
            }
            SelectedDatabase.Close();
            Databases.Remove(SelectedDatabase);
            SelectedDatabase = null;
        }
        #endregion

        #region ShowPropertiesCommand
        private DelegateCommand _ShowPropertiesCommand = default;
        /// <summary>Gets the ShowProperties command.</summary>
        /// <value>The ShowProperties command.</value>
        public DelegateCommand ShowPropertiesCommand => _ShowPropertiesCommand ??= new DelegateCommand(ShowProperties, ValidateShowPropertiesState);
        private bool ValidateShowPropertiesState(object state) => SelectedDatabase != null;
        private void ShowProperties(object state) {
            ExecuteAction(nameof(Actions.ShowProperties));
        }
        #endregion

        #region AddTableCommand
        private DelegateCommand _AddTableCommand = default;
        public DelegateCommand AddTableCommand => _AddTableCommand ??= new DelegateCommand(AddTable, ValidateAddTableState);
        private bool ValidateAddTableState(object state) => true;
        private void AddTable(object state) {
            ExecuteAction(nameof(Actions.AddTable));
        }
        #endregion

        #region Settings Command
        private DelegateCommand _SettingsCommand = default;
        public DelegateCommand SettingsCommand => _SettingsCommand ??= new DelegateCommand(Settings, ValidateSettingsState);
        private bool ValidateSettingsState(object state) => true;
        private void Settings(object state) {
            ExecuteAction(nameof(Actions.ShowSettings));
        }
        #endregion

        #endregion

        #region Databases Property
        private ObservableCollection<OzDBDatabase> _Databases = default;
        public ObservableCollection<OzDBDatabase> Databases {
            get => _Databases;
            set {
                _Databases = value;
                InvokePropertyChanged(nameof(Databases));
            }
        }
        #endregion

        #region SelectedDatabase Property
        private OzDBDatabase _SelectedDatabase = default;
        public OzDBDatabase SelectedDatabase {
            get => _SelectedDatabase;
            set {
                _SelectedDatabase = value;
                InvokePropertyChanged(nameof(SelectedDatabase));
            }
        }
        #endregion
    }
}
