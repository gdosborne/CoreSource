// -----------------------------------------------------------------------
// Copyright GDOsborne.com 2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// PasswordWindowView
//
namespace MoMoney.Views
{
	using MVVMFramework;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;

	public class PasswordWindowView : ViewModelBase
	{
		#region Public Methods
		public override void InitView() {
			UpdateInterface();
		}
		public override void UpdateInterface() {
			OKCommand.RaiseCanExecuteChanged();
			CancelCommand.RaiseCanExecuteChanged();
		}
		#endregion Public Methods

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
			return !string.IsNullOrEmpty(Password);
		}
		#endregion Private Methods

		#region Public Events
		public event ExecuteUIActionHandler ExecuteUIAction;
		public override event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private DelegateCommand _CancelCommand = null;
		private bool? _DialogResult;
		private string _FileName;
		private DelegateCommand _OKCommand = null;
		private string _Password;
		#endregion Private Fields

		#region Public Properties
		public DelegateCommand CancelCommand {
			get {
				if (_CancelCommand == null)
					_CancelCommand = new DelegateCommand(Cancel, ValidateCancelState);
				return _CancelCommand as DelegateCommand;
			}
		}
		public bool? DialogResult {
			get {
				return _DialogResult;
			}
			set {
				_DialogResult = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("DialogResult"));
			}
		}
		public string FileName {
			get {
				return _FileName;
			}
			set {
				_FileName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FileName"));
			}
		}
		public DelegateCommand OKCommand {
			get {
				if (_OKCommand == null)
					_OKCommand = new DelegateCommand(OK, ValidateOKState);
				return _OKCommand as DelegateCommand;
			}
		}
		public string Password {
			get {
				return _Password;
			}
			set {
				_Password = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Password"));
			}
		}
		#endregion Public Properties
	}
}
