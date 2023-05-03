namespace GregOsborne.Developers.Suite {
	using System.Windows;
	using GregOsborne.MVVMFramework;

	public partial class MainWindowView {
		private DelegateCommand newConfigFileCommand = default;
		private DelegateCommand openConfigFileCommand = default;
		private DelegateCommand saveConfiFileCommand = default;
		private DelegateCommand saveConfigFileAsCommand = default;
		private DelegateCommand exitApplicationCommand = default;
		private DelegateCommand undoCommand = default;
		private DelegateCommand redoCommand = default;
		private DelegateCommand cutCommand = default;
		private DelegateCommand copyCommand = default;
		private DelegateCommand pasteCommand = default;
		private DelegateCommand managerCommand = default;
		private DelegateCommand aboutCommand = default;
		private DelegateCommand settingsCommand = default;

		public DelegateCommand NewConfigFileCommand => this.newConfigFileCommand ?? (this.newConfigFileCommand = new DelegateCommand(this.NewConfigFile, this.ValidateNewConfigFileState));
		public DelegateCommand OpenConfigFileCommand => this.openConfigFileCommand ?? (this.openConfigFileCommand = new DelegateCommand(this.OpenConfigFile, this.ValidateOpenConfigFileState));
		public DelegateCommand SaveConfigFileCommand => this.saveConfiFileCommand ?? (this.saveConfiFileCommand = new DelegateCommand(this.SaveConfigFile, this.ValidateSaveConfigFileState));
		public DelegateCommand SaveConfigFileAsCommand => this.saveConfigFileAsCommand ?? (this.saveConfigFileAsCommand = new DelegateCommand(this.SaveConfigFileAs, this.ValidateSaveConfigFileAsState));
		public DelegateCommand ExitApplicationCommand => this.exitApplicationCommand ?? (this.exitApplicationCommand = new DelegateCommand(this.ExitApplication, this.ValidateExitApplicationState));
		public DelegateCommand UndoCommand => this.undoCommand ?? (this.undoCommand = new DelegateCommand(this.Undo, this.ValidateUndoState));
		public DelegateCommand RedoCommand => this.redoCommand ?? (this.redoCommand = new DelegateCommand(this.Redo, this.ValidateRedoState));
		public DelegateCommand CutCommand => this.cutCommand ?? (this.cutCommand = new DelegateCommand(this.Cut, this.ValidateCutState));
		public DelegateCommand CopyCommand => this.copyCommand ?? (this.copyCommand = new DelegateCommand(this.Copy, this.ValidateCopyState));
		public DelegateCommand PasteCommand => this.pasteCommand ?? (this.pasteCommand = new DelegateCommand(this.Paste, this.ValidatePasteState));
		public DelegateCommand ManagerCommand => this.managerCommand ?? (this.managerCommand = new DelegateCommand(this.Manager, this.ValidateManagerState));
		public DelegateCommand AboutCommand => this.aboutCommand ?? (this.aboutCommand = new DelegateCommand(this.About, this.ValidateAboutState));
		public DelegateCommand SettingsCommand => this.settingsCommand ?? (this.settingsCommand = new DelegateCommand(this.Settings, this.ValidateSettingsState));

		private bool ValidateNewConfigFileState(object state) => true;
		private bool ValidateOpenConfigFileState(object state) => true;
		private bool ValidateSaveConfigFileState(object state) => true;
		private bool ValidateSaveConfigFileAsState(object state) => true;
		private bool ValidateExitApplicationState(object state) => true;
		private bool ValidateUndoState(object state) => true;
		private bool ValidateRedoState(object state) => true;
		private bool ValidateCutState(object state) => true;
		private bool ValidateCopyState(object state) => true;
		private bool ValidatePasteState(object state) => true;
		private bool ValidateManagerState(object state) => true;
		private bool ValidateAboutState(object state) => true;
		private bool ValidateSettingsState(object state) => true;

		private void NewConfigFile(object state) => this.IsSaveRequired = !this.IsSaveRequired;// MessageBox.Show($"{this.NewConfigFileTip}");
		private void OpenConfigFile(object state) => MessageBox.Show($"{this.OpenConfigFileTip}");
		private void SaveConfigFile(object state) => this.ConfigurationFile.Save();
		private void SaveConfigFileAs(object state) => MessageBox.Show($"{this.SaveConfigFileAsTip}");
		private void ExitApplication(object state) {
			if (this.IsSaveRequired) {
				this.SaveConfigFileAsCommand.Execute(null);
			}
			//App.Current.As<App>().ExitApplication();
			ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("ExitApplication"));
		}
		private void Undo(object state) => MessageBox.Show($"{this.UndoTip}");
		private void Redo(object state) => MessageBox.Show($"{this.RedoTip}");
		private void Cut(object state) => MessageBox.Show($"{this.CutTip}");
		private void Copy(object state) => MessageBox.Show($"{this.CopyTip}");
		private void Paste(object state) => MessageBox.Show($"{this.PasteTip}");
		private void Manager(object state) => ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("ShowManagerWindow"));
		private void About(object state) => ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("ShowAboutWindow"));
		private void Settings(object state) => ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("ShowSettingsWindow"));
	}
}
