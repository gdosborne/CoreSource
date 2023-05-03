using System.Linq;
using GregOsborne.MVVMFramework;

namespace Greg.Osborne.Installer {
	public partial class MainWindowView {
		private DelegateCommand closeCommand = default;
		public DelegateCommand CloseCommand => this.closeCommand ?? (this.closeCommand = new DelegateCommand(this.Close, this.ValidateCloseState));
		private bool ValidateCloseState(object state) => true;
		private void Close(object state) => ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("CloseWindow"));

		private DelegateCommand maximizeCommand = default;
		public DelegateCommand MaximizeCommand => this.maximizeCommand ?? (this.maximizeCommand = new DelegateCommand(this.Maximize, this.ValidateMaximizeState));
		private bool ValidateMaximizeState(object state) => true;
		private void Maximize(object state) => ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("MaximizeRestore"));

		private DelegateCommand minimizeCommand = default;
		public DelegateCommand MinimizeCommand => this.minimizeCommand ?? (this.minimizeCommand = new DelegateCommand(this.Minimize, this.ValidateMinimizeState));
		private bool ValidateMinimizeState(object state) => true;
		private void Minimize(object state) => ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("Minimize"));

		private DelegateCommand provideFeedbackCommand = default;
		public DelegateCommand ProvideFeedbackCommand => this.provideFeedbackCommand ?? (this.provideFeedbackCommand = new DelegateCommand(this.ProvideFeedback, this.ValidateProvideFeedbackState));
		private bool ValidateProvideFeedbackState(object state) => true;
		private void ProvideFeedback(object state) => ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("ProvideFeedback"));

		private DelegateCommand cancelInstalltionCommand = default;
		public DelegateCommand CancelInstallationCommand => this.cancelInstalltionCommand ?? (this.cancelInstalltionCommand = new DelegateCommand(this.CancelInstallation, this.ValidateCancelInstallationState));
		private bool ValidateCancelInstallationState(object state) => true;
		private void CancelInstallation(object state) => ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("CancelInstallation"));

		private DelegateCommand continueInstallationCommand = default;
		public DelegateCommand ContinueInstallationCommand => this.continueInstallationCommand ?? (this.continueInstallationCommand = new DelegateCommand(this.ContinueInstallation, this.ValidateContinueInstallationState));
		private bool ValidateContinueInstallationState(object state) => this.InstallationItems.Any(x => x.Status == Support.InstallItem.InstallStatuses.Install);
		private void ContinueInstallation(object state) => ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("ContinueInstallation"));

		private DelegateCommand installAllCommand = default;
		public DelegateCommand InstallAllCommand => this.installAllCommand ?? (this.installAllCommand = new DelegateCommand(this.InstallAll, this.ValidateInstallAllState));
		private bool ValidateInstallAllState(object state) => true;
		private void InstallAll(object state) => this.InstallationItems.ToList().ForEach(x => x.Status = Support.InstallItem.InstallStatuses.Install);

	}
}
