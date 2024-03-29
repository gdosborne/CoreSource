namespace GregOsborne.Dialog
{
	using MVVMFramework;
	using System;
	using System.ComponentModel;
	using System.Windows;

	public class SetAddressDialogView : INotifyPropertyChanged
	{
		#region Public Methods
		public void UpdateInterface()
		{
			OKCommand.RaiseCanExecuteChanged();
		}
		#endregion Public Methods

		#region Private Methods
		private void Cancel(object state)
		{
			DialogResult = false;
		}
		private void OK(object state)
		{
			DialogResult = true;
		}
		private bool ValidateCancelState(object state)
		{
			return true;
		}
		private bool ValidateOKState(object state)
		{
			return !string.IsNullOrEmpty(Address);
		}
		#endregion Private Methods

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private string _Address;
		private DelegateCommand _CancelCommand = null;
		private bool? _DialogResult;
		private DelegateCommand _OKCommand = null;
		#endregion Private Fields

		#region Public Properties
		public string Address
		{
			get
			{
				return _Address;
			}
			set
			{
				_Address = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Address"));
			}
		}
		public DelegateCommand CancelCommand
		{
			get
			{
				if (_CancelCommand == null)
					_CancelCommand = new DelegateCommand(Cancel, ValidateCancelState);
				return _CancelCommand as DelegateCommand;
			}
		}
		public bool? DialogResult
		{
			get
			{
				return _DialogResult;
			}
			set
			{
				_DialogResult = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("DialogResult"));
			}
		}
		public DelegateCommand OKCommand
		{
			get
			{
				if (_OKCommand == null)
					_OKCommand = new DelegateCommand(OK, ValidateOKState);
				return _OKCommand as DelegateCommand;
			}
		}
		#endregion Public Properties
	}
}
