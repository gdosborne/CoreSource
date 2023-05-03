namespace EnableVersioning {
	using GregOsborne.MVVMFramework;

	public partial class AddNewSchemaWindowView {
		private DelegateCommand closeCommand = default;
		private DelegateCommand saveSchemaCommand = default;

		private void Close(object state) => this.DialogResult = false;
		private void SaveSchema(object state) => this.DialogResult = true;
		private bool ValidateCloseState(object state) => true;
		private bool ValidateSaveSchemaState(object state) => !string.IsNullOrEmpty(this.SchemaName);

		public DelegateCommand CloseCommand => this.closeCommand ?? (this.closeCommand = new DelegateCommand(this.Close, this.ValidateCloseState));
		public DelegateCommand SaveSchemaCommand => this.saveSchemaCommand ?? (this.saveSchemaCommand = new DelegateCommand(this.SaveSchema, this.ValidateSaveSchemaState));
	}
}