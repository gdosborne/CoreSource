// -----------------------------------------------------------------------
// Copyright GDOsborne.com 2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// AccountWindowView
//
namespace MoMoney.Views
{
	using MoMoney.Data;
	using MVVMFramework;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Linq;

	public class AccountWindowView : ViewModelBase
	{
		#region Public Methods
		public override void InitView() {
			AccountTypes = new ObservableCollection<Data.AccountTypes>();
			Enum.GetNames(typeof(AccountTypes)).ToList().ForEach(x => AccountTypes.Add((AccountTypes)Enum.Parse(typeof(AccountTypes), x, false)));
			SelectedAccountType = Data.AccountTypes.Checking;
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
			if (BeginningBalance == 0) {
				if (ExecuteUIAction != null)
					ExecuteUIAction(this, new ExecuteUIActionEventArgs("ShowZeroBalanceQuestion"));
			}
			else
				DialogResult = true;
		}
		private bool ValidateCancelState(object state) {
			return true;
		}
		private bool ValidateOKState(object state) {
			return !string.IsNullOrEmpty(Name);
		}
		#endregion Private Methods

		#region Public Events
		public event ExecuteUIActionHandler ExecuteUIAction;
		public override event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private ObservableCollection<AccountTypes> _AccountTypes;
		private decimal _BeginningBalance;
		private DelegateCommand _CancelCommand = null;
		private bool? _DialogResult;
		private string _Name;
		private DelegateCommand _OKCommand = null;
		private AccountTypes _SelectedAccountType;
		#endregion Private Fields

		#region Public Properties
		public ObservableCollection<AccountTypes> AccountTypes {
			get {
				return _AccountTypes;
			}
			set {
				_AccountTypes = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AccountTypes"));
			}
		}
		public decimal BeginningBalance {
			get {
				return _BeginningBalance;
			}
			set {
				_BeginningBalance = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("BeginningBalance"));
			}
		}
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
		public string Name {
			get {
				return _Name;
			}
			set {
				_Name = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Name"));
			}
		}
		public DelegateCommand OKCommand {
			get {
				if (_OKCommand == null)
					_OKCommand = new DelegateCommand(OK, ValidateOKState);
				return _OKCommand as DelegateCommand;
			}
		}
		public AccountTypes SelectedAccountType {
			get {
				return _SelectedAccountType;
			}
			set {
				_SelectedAccountType = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedAccountType"));
			}
		}
		#endregion Public Properties
	}
}
