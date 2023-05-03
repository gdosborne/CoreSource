namespace SNC.Applications.Developer.Views
{
	using MVVMFramework;
	using System;
	using System.ComponentModel;

	public class PropertiesWindowView : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public void UpdateInterface() {
			OKCommand.RaiseCanExecuteChanged();
			CancelCommand.RaiseCanExecuteChanged();
		}
		public void InitView() {

		}
		private bool? _DialogResult;
		public bool? DialogResult {
			get { return _DialogResult; }
			set {
				_DialogResult = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("DialogResult"));
			}
		}
		private DelegateCommand _OKCommand = null;
		public DelegateCommand OKCommand {
			get {
				if (_OKCommand == null)
					_OKCommand = new DelegateCommand(OK, ValidateOKState);
				return _OKCommand as DelegateCommand;
			}
		}
		private void OK(object state) {
			DialogResult = true;
		}
		private bool ValidateOKState(object state) {
			return true;
		}
		private DelegateCommand _CancelCommand = null;
		public DelegateCommand CancelCommand {
			get {
				if (_CancelCommand == null)
					_CancelCommand = new DelegateCommand(Cancel, ValidateCancelState);
				return _CancelCommand as DelegateCommand;
			}
		}
		private void Cancel(object state) {
			DialogResult = false;
		}
		private bool ValidateCancelState(object state) {
			return true;
		}

	}
}
