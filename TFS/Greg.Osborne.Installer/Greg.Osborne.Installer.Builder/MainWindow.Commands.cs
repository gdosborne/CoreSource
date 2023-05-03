namespace Greg.Osborne.Installer.Builder {
	using System.Collections.Generic;
	using System.Windows;
	using Greg.Osborne.Installer.Support;
	using GregOsborne.Application.Primitives;
	using GregOsborne.MVVMFramework;

	public partial class MainWindowView {
		private DelegateCommand newInstallationCommand = default;
		public DelegateCommand NewInstallationCommand => this.newInstallationCommand ?? (this.newInstallationCommand = new DelegateCommand(this.NewInstallation, this.ValidateNewInstallationState));
		private bool ValidateNewInstallationState(object state) => true;
		private void NewInstallation(object state) {
			this.Controller = InstallerController.CreateNew();
			this.Controller.Filename = Application.Current.As<App>().GetTempFileName();
			this.Controller.Save();
			this.UpdateInterface();
		}

		private DelegateCommand openInstallationCommand = default;
		public DelegateCommand OpenInstallationCommand => this.openInstallationCommand ?? (this.openInstallationCommand = new DelegateCommand(this.OpenInstallation, this.ValidateOpenInstallationState));
		private bool ValidateOpenInstallationState(object state) => true;
		private void OpenInstallation(object state) {
			var p = new Dictionary<string, object> {
				{ "cancel", false },
				{ "filename", string.Empty }
			};
			ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("OpenInstallationController", p));
			if ((bool)p["cancel"]) {
				return;
			}

			this.Controller = InstallerController.FromFilename((string)p["filename"]);
			if (this.Controller == null) {
				return;
			}

			this.Controller.SideItems.ForEach(item => item.PropertyChanged += this.Item_PropertyChanged);
		}

		private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) => this.Controller.HasChanges = true;

		private DelegateCommand saveInstallationCommand = default;
		public DelegateCommand SaveInstallationCommand => this.saveInstallationCommand ?? (this.saveInstallationCommand = new DelegateCommand(this.SaveInstallation, this.ValidateSaveInstallationState));
		private bool ValidateSaveInstallationState(object state) => this.Controller != null && (this.Controller.HasChanges || this.Controller.IsTempFile);
		private void SaveInstallation(object state) {
			if (this.Controller.IsTempFile) {
				this.SaveAsInstallationCommand.Execute(null);
			} else {
				this.Controller.Save();
			}
		}

		private DelegateCommand saveAsInstallationCommand = default;
		public DelegateCommand SaveAsInstallationCommand => this.saveAsInstallationCommand ?? (this.saveAsInstallationCommand = new DelegateCommand(this.SaveAsInstallation, this.ValidateSaveAsInstallationState));
		private bool ValidateSaveAsInstallationState(object state) => this.Controller != null;
		private void SaveAsInstallation(object state) {
			var p = new Dictionary<string, object> {
				{ "cancel", false },
				{ "filename", string.Empty }
			};
			ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("SaveAsInstallationController", p));
			if ((bool)p["cancel"]) {
				return;
			}

			this.Controller.Filename = (string)p["filename"];
			this.Controller.Save();
		}

		private DelegateCommand exitCommand = default;
		public DelegateCommand ExitCommand => this.exitCommand ?? (this.exitCommand = new DelegateCommand(this.Exit, this.ValidateExitState));
		private bool ValidateExitState(object state) => true;
		private void Exit(object state) => ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("ExitApplication"));

		private DelegateCommand closeControllerCommand = default;
		public DelegateCommand CloseControllerCommand => this.closeControllerCommand ?? (this.closeControllerCommand = new DelegateCommand(this.CloseController, this.ValidateCloseControllerState));
		private bool ValidateCloseControllerState(object state) => true;
		private void CloseController(object state) {

		}

	}
}
