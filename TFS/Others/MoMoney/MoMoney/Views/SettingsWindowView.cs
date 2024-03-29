// -----------------------------------------------------------------------
// Copyright GDOsborne.com 2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
//
//
namespace MoMoney.Views
{
	using MVVMFramework;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;

	public class SettingsWindowView : ViewModelBase
	{
		#region Public Methods
		public override void InitView() {
			OpenLastMMFFile = MoMoney.Properties.Settings.Default.OpenLastMMFFile;
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
			return true;
		}
		#endregion Private Methods

		#region Public Events
		public event ExecuteUIActionHandler ExecuteUIAction;
		public override event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private DelegateCommand _CancelCommand = null;
		private bool? _DialogResult;
		private DelegateCommand _OKCommand = null;
		private bool _OpenLastMMFFile;
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
		public DelegateCommand OKCommand {
			get {
				if (_OKCommand == null)
					_OKCommand = new DelegateCommand(OK, ValidateOKState);
				return _OKCommand as DelegateCommand;
			}
		}
		public bool OpenLastMMFFile {
			get {
				return _OpenLastMMFFile;
			}
			set {
				_OpenLastMMFFile = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("OpenLastMMFFile"));
			}
		}
		#endregion Public Properties
	}
}
