namespace ManageVersioning {
    public partial class MainWindowView {
        public enum UIActions {
            AddNewSchema,
            AddNewProject,
            SaveData,
            ExitApplication,
            UndoChange,
            RedoChange,
            Delete,
            CutValue,
            CopyValue,
            PasteValue,
            ShowSettings,
            TestVersion,
            GotoDataFileDirectory,
            EditProject,
            Minimize,
            Maximize,
            ShowAbout
        }

        public override void UpdateInterface() {
            base.UpdateInterface();
            _HasUndo = UndoItems.Count > 0;
        }

        #region Close Command
        private DelegateCommand _CloseCommand = default;
        public DelegateCommand CloseCommand => _CloseCommand ??= new DelegateCommand(Close, ValidateCloseState);
        private bool ValidateCloseState(object state) => true;
        private void Close(object state) {
            ExecuteAction(nameof(UIActions.ExitApplication));
        }
        #endregion

        #region Minimize Command
        private DelegateCommand _MinimizeCommand = default;
        public DelegateCommand MinimizeCommand => _MinimizeCommand ??= new DelegateCommand(Minimize, ValidateMinimizeState);
        private bool ValidateMinimizeState(object state) => true;
        private void Minimize(object state) {
            ExecuteAction(nameof(UIActions.Minimize));
        }
        #endregion

        #region Maximize Command
        private DelegateCommand _MaximizeCommand = default;
        public DelegateCommand MaximizeCommand => _MaximizeCommand ??= new DelegateCommand(Maximize, ValidateMaximizeState);
        private bool ValidateMaximizeState(object state) => true;
        private void Maximize(object state) {
            ExecuteAction(nameof(UIActions.Maximize));
        }
        #endregion

        #region ShowAbout Command
        private DelegateCommand _ShowAboutCommand = default;
        public DelegateCommand ShowAboutCommand => _ShowAboutCommand ??= new DelegateCommand(ShowAbout, ValidateShowAboutState);
        private bool ValidateShowAboutState(object state) => true;
        private void ShowAbout(object state) {
            ExecuteAction(nameof(UIActions.ShowAbout));
        }
        #endregion

        #region NewSchema Command
        private DelegateCommand _NewSchemaCommand = default;
        public DelegateCommand NewSchemaCommand => _NewSchemaCommand ??= new DelegateCommand(NewSchema, ValidateNewSchemaState);
        private bool ValidateNewSchemaState(object state) => true;
        private void NewSchema(object state) {
            ExecuteAction(nameof(UIActions.AddNewSchema));
        }
        #endregion

        #region NewProject Command
        private DelegateCommand _NewProjectCommand = default;
        public DelegateCommand NewProjectCommand => _NewProjectCommand ??= new DelegateCommand(NewProject, ValidateNewProjectState);
        private bool ValidateNewProjectState(object state) => true;
        private void NewProject(object state) {
            ExecuteAction(nameof(UIActions.AddNewProject));
        }
        #endregion

        #region Save Command
        private DelegateCommand _SaveCommand = default;
        public DelegateCommand SaveCommand => _SaveCommand ??= new DelegateCommand(Save, ValidateSaveState);
        private bool ValidateSaveState(object state) => HasChanges;
        private void Save(object state) {
            ExecuteAction(nameof(UIActions.SaveData));
        }
        #endregion

        #region Exit Command
        private DelegateCommand _ExitCommand = default;
        public DelegateCommand ExitCommand => _ExitCommand ??= new DelegateCommand(Exit, ValidateExitState);
        private bool ValidateExitState(object state) => true;
        private void Exit(object state) {
            ExecuteAction(nameof(UIActions.ExitApplication));
        }
        #endregion

        #region Undo Command
        private DelegateCommand _UndoCommand = default;
        public DelegateCommand UndoCommand => _UndoCommand ??= new DelegateCommand(Undo, ValidateUndoState);
        private bool ValidateUndoState(object state) => HasUndo;
        private void Undo(object state) {
            ExecuteAction(nameof(UIActions.UndoChange));
        }
        #endregion

        #region Redo Command
        private DelegateCommand _RedoCommand = default;
        public DelegateCommand RedoCommand => _RedoCommand ??= new DelegateCommand(Redo, ValidateRedoState);
        private bool ValidateRedoState(object state) => HasRedo;
        private void Redo(object state) {
            ExecuteAction(nameof(UIActions.RedoChange));
        }
        #endregion

        #region Cut Command
        private DelegateCommand _CutCommand = default;
        public DelegateCommand CutCommand => _CutCommand ??= new DelegateCommand(Cut, ValidateCutState);
        private bool ValidateCutState(object state) => SelectedProject != null && !SelectedProject.IsDeleted;
        private void Cut(object state) {
            ExecuteAction(nameof(UIActions.CutValue));
        }
        #endregion

        #region Delete Command
        private DelegateCommand _DeleteCommand = default;
        public DelegateCommand DeleteCommand => _DeleteCommand ??= new DelegateCommand(Delete, ValidateDeleteState);
        private bool ValidateDeleteState(object state) => SelectedProject != null && !SelectedProject.IsDeleted;
        private void Delete(object state) {
            ExecuteAction(nameof(UIActions.Delete));
        }
        #endregion

        #region Copy Command
        private DelegateCommand _CopyCommand = default;
        public DelegateCommand CopyCommand => _CopyCommand ??= new DelegateCommand(Copy, ValidateCopyState);
        private bool ValidateCopyState(object state) => SelectedProject != null && !SelectedProject.IsDeleted;
        private void Copy(object state) {
            ExecuteAction(nameof(UIActions.CopyValue));
        }
        #endregion

        #region Paste Command
        private DelegateCommand _PasteCommand = default;
        public DelegateCommand PasteCommand => _PasteCommand ??= new DelegateCommand(Paste, ValidatePasteState);
        private bool ValidatePasteState(object state) => HasClipboardItem;
        private void Paste(object state) {
            ExecuteAction(nameof(UIActions.PasteValue));
        }
        #endregion

        #region Settings Command
        private DelegateCommand _SettingsCommand = default;
        public DelegateCommand SettingsCommand => _SettingsCommand ??= new DelegateCommand(Settings, ValidateSettingsState);
        private bool ValidateSettingsState(object state) => true;
        private void Settings(object state) {
            ExecuteAction(nameof(UIActions.ShowSettings));
        }
        #endregion

        #region TestVersion Command
        private DelegateCommand _TestVersionCommand = default;
        public DelegateCommand TestVersionCommand => _TestVersionCommand ??= new DelegateCommand(TestVersion, ValidateTestVersionState);
        private bool ValidateTestVersionState(object state) => SelectedProject != null && !SelectedProject.IsDeleted;
        private void TestVersion(object state) {
            ExecuteAction(nameof(UIActions.TestVersion));
        }
        #endregion

        #region GoToDataDirectory Command
        private DelegateCommand _GoToDataDirectoryCommand = default;
        public DelegateCommand GoToDataDirectoryCommand => _GoToDataDirectoryCommand ??= new DelegateCommand(GoToDataDirectory, ValidateGoToDataDirectoryState);
        private bool ValidateGoToDataDirectoryState(object state) => true;
        private void GoToDataDirectory(object state) {
            ExecuteAction(nameof(UIActions.GotoDataFileDirectory));
        }
        #endregion

        #region EditProject Command
        private DelegateCommand _EditProjectCommand = default;
        public DelegateCommand EditProjectCommand => _EditProjectCommand ??= new DelegateCommand(EditProject, ValidateEditProjectState);
        private bool ValidateEditProjectState(object state) => SelectedProject != null;
        private void EditProject(object state) {
            ExecuteAction(nameof(UIActions.EditProject));
        }
        #endregion

    }
}
