using Common.Application.IO;
using Common.MVVMFramework;
using OzDB.Management;
using OzDB.Management.Exceptions;
using System;
using System.IO;

namespace OzDBCreate.ViewModel {
    public class MainWindowView : ViewModelBase {
        public MainWindowView() {
            Title = $"{App.AppName} [designer]";
        }

        public override void Initialize() {
            base.Initialize();

            Title = App.AppName;
        }

        #region commands
        internal enum Actions {
            AskSaveChanges,
            OpenDatabase,
            ShowProperties,
            AskNewFileName
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
        private bool ValidateSaveAsState(object state) => CurrentDatabase != null;
        private void SaveAs(object state) {
            ExecuteAction(nameof(Actions.AskNewFileName));
        }
        #endregion

        #region SaveCommand
        private DelegateCommand _SaveCommand = default;
        /// <summary>Gets the Save command.</summary>
        /// <value>The Save command.</value>
        public DelegateCommand SaveCommand => _SaveCommand ??= new DelegateCommand(Save, ValidateSaveState);
        private bool ValidateSaveState(object state) => CurrentDatabase != null && CurrentDatabase.HasChanges;
        private async void Save(object state) {
            try {
                await CurrentDatabase?.SaveAsync();
            }
            catch(Exception ex) {
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
            CurrentDatabase = await OzDBDatabase.Create(name, tempDirName, id);
            ShowProperties(null);
        }
        #endregion

        #region OpenCommand
        private DelegateCommand _OpenCommand = default;
        /// <summary>Gets the Open command.</summary>
        /// <value>The Open command.</value>
        public DelegateCommand OpenCommand => _OpenCommand ??= new DelegateCommand(Open, ValidateOpenState);
        private bool ValidateOpenState(object state) => true;
        private void Open(object state) {
            if(CurrentDatabase != null) {
                Close(null);
            }
            ExecuteAction(nameof(Actions.OpenDatabase));
        }
        #endregion

        #region CloseCommand
        private DelegateCommand _CloseCommand = default;
        /// <summary>Gets the Close command.</summary>
        /// <value>The Close command.</value>
        public DelegateCommand CloseCommand => _CloseCommand ??= new DelegateCommand(Close, ValidateCloseState);
        private bool ValidateCloseState(object state) => CurrentDatabase != null;
        private void Close(object state) {
            if(CurrentDatabase == null) return;
            if(CurrentDatabase.HasChanges) {
                ExecuteAction(nameof(Actions.AskSaveChanges));
            }
            CurrentDatabase.Close();
            CurrentDatabase = null;
        }
        #endregion

        #region ShowPropertiesCommand
        private DelegateCommand _ShowPropertiesCommand = default;
        /// <summary>Gets the ShowProperties command.</summary>
        /// <value>The ShowProperties command.</value>
        public DelegateCommand ShowPropertiesCommand => _ShowPropertiesCommand ??= new DelegateCommand(ShowProperties, ValidateShowPropertiesState);
        private bool ValidateShowPropertiesState(object state) => CurrentDatabase != null;
        private void ShowProperties(object state) {
            ExecuteAction(nameof(Actions.ShowProperties));
        }
        #endregion

        #endregion

        #region CurrentDatabase Property
        private OzDBDatabase _CurrentDatabase = default;
        /// <summary>Gets/sets the CurrentDatabase.</summary>
        /// <value>The CurrentDatabase.</value>
        public OzDBDatabase CurrentDatabase {
            get => _CurrentDatabase;
            set {
                _CurrentDatabase = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
