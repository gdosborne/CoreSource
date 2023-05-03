namespace EnableVersioning {
	using System.Collections.Generic;
	using GregOsborne.MVVMFramework;

	public partial class SchemaWindowview {
		public event ExecuteUiActionHandler ExecuteUIAction;

		private DelegateCommand closeWindowCommand = default;
		public DelegateCommand CloseWindowCommand => this.closeWindowCommand ?? (this.closeWindowCommand = new DelegateCommand(this.CloseWindow, this.ValidateCloseWindowState));
		private bool ValidateCloseWindowState(object state) => true;
		private void CloseWindow(object state) => ExecuteUIAction?.Invoke(this, new ExecuteUiActionEventArgs("close window"));

		private DelegateCommand saveCommand = default;
		public DelegateCommand SaveCommand => this.saveCommand ?? (this.saveCommand = new DelegateCommand(this.Save, this.ValidateSaveState));
		private bool ValidateSaveState(object state) => this.HasChanges;
		private void Save(object state) {
			ExecuteUIAction?.Invoke(this, new ExecuteUiActionEventArgs("save projects"));
			this.HasChanges = false;
		}

		private DelegateCommand removeCommand = default;
		public DelegateCommand RemoveCommand => this.removeCommand ?? (this.removeCommand = new DelegateCommand(this.Remove, this.ValidateRemoveState));
		private bool ValidateRemoveState(object state) => this.SelectedProject != null;
		private void Remove(object state) {
			var p = new Dictionary<string, object> {
				{ "cancel", false }
			};
			ExecuteUIAction?.Invoke(this, new ExecuteUiActionEventArgs("remove schema", p));
			if ((bool)p["cancel"]) {
				return;
			}
			this.Projects.Remove(this.SelectedProject);
			this.HasChanges = true;
		}

	}
}
