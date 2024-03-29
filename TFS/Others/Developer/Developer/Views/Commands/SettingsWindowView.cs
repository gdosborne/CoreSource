namespace SNC.OptiRamp.Application.Developer.Views {

	using MVVMFramework;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	internal partial class SettingsWindowView {

		#region Private Methods
		private void Cancel(object state) {
			DialogResult = false;
		}
		private void OK(object state) {
			DialogResult = true;
		}
		private bool ValidateCancelState(object state) {
			return true;
		}
		private bool ValidateOKState(object state) {
			return true;
		}
		#endregion Private Methods

		#region Private Fields
		private DelegateCommand _CancelCommand = null;
		private DelegateCommand _OKCommand = null;
		#endregion Private Fields

		#region Public Properties
		public DelegateCommand CancelCommand {
			get {
				if (_CancelCommand == null)
					_CancelCommand = new DelegateCommand(Cancel, ValidateCancelState);
				return _CancelCommand as DelegateCommand;
			}
		}
		public DelegateCommand OKCommand {
			get {
				if (_OKCommand == null)
					_OKCommand = new DelegateCommand(OK, ValidateOKState);
				return _OKCommand as DelegateCommand;
			}
		}
		#endregion Public Properties
	}
}
