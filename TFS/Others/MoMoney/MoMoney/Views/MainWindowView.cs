// -----------------------------------------------------------------------
// Copyright GDOsborne.com 2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// MainWindowView
//
namespace MoMoney.Views
{
	using MoMoney.Data.Providers;
	using MVVMFramework;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.IO;

	public class MainWindowView : ViewModelBase
	{
		#region Public Constructors
		public MainWindowView() {
			CurrentFileName = "No file";
			TodaysBalance = 0.0m;
		}
		#endregion Public Constructors

		#region Public Methods
		public void CreateNewFile(string fileName) {
		}
		public override void InitView() {
			if (MoMoney.Properties.Settings.Default.OpenLastMMFFile && !string.IsNullOrEmpty(MoMoney.Properties.Settings.Default.LastMMFFile)) {
				CurrentFileName = MoMoney.Properties.Settings.Default.LastMMFFile;
				OpenFile();
			}
			UpdateInterface();
		}
		public void OpenFile() {
			DataProvider = new EncryptedXMLProvider(CurrentPassword);
			DataProvider.DataChanged += DataProvider_DataChanged;
			try {
				DataProvider.Open(CurrentFileName);
				MoMoney.Properties.Settings.Default.LastMMFFile = CurrentFileName;
				MoMoney.Properties.Settings.Default.Save();
			}
			catch (System.Exception ex) {
				DataProvider = null;
				if (ExecuteUIAction != null)
					ExecuteUIAction(this, new ExecuteUIActionEventArgs("DisplayException", new Dictionary<string, object> { { "exception", new ApplicationException("Cannot open mmf file", ex) } }));
			}
		}

		public void SaveFileAs(string fileName) {
			_IsSaveFile = true;
			CurrentFileName = fileName;
			if (DataProvider == null)
				DataProvider = new EncryptedXMLProvider(CurrentPassword);
			Save(null);
		}
		public override void UpdateInterface() {
			SaveCommand.RaiseCanExecuteChanged();
			SaveAsCommand.RaiseCanExecuteChanged();
			CloseFileCommand.RaiseCanExecuteChanged();
			OpenFileCommand.RaiseCanExecuteChanged();
			ExitCommand.RaiseCanExecuteChanged();
			NewFileCommand.RaiseCanExecuteChanged();
			SettingsCommand.RaiseCanExecuteChanged();
			NewAccountCommand.RaiseCanExecuteChanged();
			NewTransactionCommand.RaiseCanExecuteChanged();
		}
		#endregion Public Methods

		#region Private Methods
		private void CloseFile(object state) {
			if (!DataProvider.HasChanges) {
				DataProvider = null;
				CurrentFileName = "No file";
			}
		}
		private void DataProvider_DataChanged(object sender, EventArgs e) {
			UpdateInterface();
		}
		private void Exit(object state) {
			if (DataProvider != null && DataProvider.HasChanges) {
				if (ExecuteUIAction != null)
					ExecuteUIAction(this, new ExecuteUIActionEventArgs("SaveFileRequired"));
				return;
			}
			App.Current.Shutdown(0);
		}
		private void NewAccount(object state) {
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("ShowNewAccountWindow"));
		}
		private void NewFile(object state) {
			IsSaveFile = true;
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("ShowFilePassword"));
		}
		private void NewTransaction(object state) {
		}
		private void OpenFileAction(object state) {
			_IsSaveFile = false;
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("OpenFile"));
		}
		private bool OpenFileValidate(object state) {
			return true;
		}
		private void Save(object state) {
			DataProvider.Save(CurrentFileName);
			UpdateInterface();
		}
		private void SaveAs(object state) {
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("SaveFileAs"));
		}
		private void Settings(object state) {
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("ShowSettings"));
		}
		private bool ValidateCloseFileState(object state) {
			return DataProvider != null;
		}
		private bool ValidateExitState(object state) {
			return true;
		}
		private bool ValidateNewAccountState(object state) {
			return DataProvider != null;
		}
		private bool ValidateNewFileState(object state) {
			return true;
		}
		private bool ValidateNewTransactionState(object state) {
			return DataProvider != null && SelectedAccount != null;
		}
		private bool ValidateSaveAsState(object state) {
			return DataProvider != null;
		}
		private bool ValidateSaveState(object state) {
			return DataProvider != null && DataProvider.HasChanges;
		}
		private bool ValidateSettingsState(object state) {
			return true;
		}
		#endregion Private Methods

		#region Public Events
		public event ExecuteUIActionHandler ExecuteUIAction;
		public override event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private DelegateCommand _CloseFileCommand = null;
		private string _CurrentFileName = null;
		private string _CurrentPassword;
		private IDataProvider _DataProvider = null;
		private DelegateCommand _ExitCommand = null;
		private bool _IsSaveFile;
		private DelegateCommand _NewAccountCommand = null;
		private DelegateCommand _NewFileCommand = null;
		private DelegateCommand _NewTransactionCommand = null;
		private DelegateCommand _OpenFileCommand = null;
		private DelegateCommand _SaveAsCommand = null;
		private DelegateCommand _SaveCommand = null;
		private Data.Account _SelectedAccount;
		private DelegateCommand _SettingsCommand = null;
		private decimal _TodaysBalance;
		#endregion Private Fields

		#region Public Properties
		public DelegateCommand CloseFileCommand {
			get {
				if (_CloseFileCommand == null)
					_CloseFileCommand = new DelegateCommand(CloseFile, ValidateCloseFileState);
				return _CloseFileCommand as DelegateCommand;
			}
		}
		public string CurrentFileName {
			get {
				return _CurrentFileName;
			}
			set {
				_CurrentFileName = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CurrentFileName"));
				if (!File.Exists(_CurrentFileName))
					return;
				if (!_IsSaveFile) {
					if (ExecuteUIAction != null)
						ExecuteUIAction(this, new ExecuteUIActionEventArgs("ShowFilePassword"));
				}
			}
		}
		public string CurrentPassword {
			get {
				return _CurrentPassword;
			}
			set {
				_CurrentPassword = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CurrentPassword"));
			}
		}
		public IDataProvider DataProvider {
			get {
				return _DataProvider;
			}
			set {
				_DataProvider = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("DataProvider"));
			}
		}
		public DelegateCommand ExitCommand {
			get {
				if (_ExitCommand == null)
					_ExitCommand = new DelegateCommand(Exit, ValidateExitState);
				return _ExitCommand;
			}
		}
		public bool IsSaveFile {
			get {
				return _IsSaveFile;
			}
			set {
				_IsSaveFile = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IsSaveFile"));
			}
		}
		public DelegateCommand NewAccountCommand {
			get {
				if (_NewAccountCommand == null)
					_NewAccountCommand = new DelegateCommand(NewAccount, ValidateNewAccountState);
				return _NewAccountCommand as DelegateCommand;
			}
		}
		public DelegateCommand NewFileCommand {
			get {
				if (_NewFileCommand == null)
					_NewFileCommand = new DelegateCommand(NewFile, ValidateNewFileState);
				return _NewFileCommand as DelegateCommand;
			}
		}
		public DelegateCommand NewTransactionCommand {
			get {
				if (_NewTransactionCommand == null)
					_NewTransactionCommand = new DelegateCommand(NewTransaction, ValidateNewTransactionState);
				return _NewTransactionCommand as DelegateCommand;
			}
		}
		public DelegateCommand OpenFileCommand {
			get {
				if (_OpenFileCommand == null)
					_OpenFileCommand = new DelegateCommand(OpenFileAction, OpenFileValidate);
				return _OpenFileCommand;
			}
		}
		public DelegateCommand SaveAsCommand {
			get {
				if (_SaveAsCommand == null)
					_SaveAsCommand = new DelegateCommand(SaveAs, ValidateSaveAsState);
				return _SaveAsCommand as DelegateCommand;
			}
		}
		public DelegateCommand SaveCommand {
			get {
				if (_SaveCommand == null)
					_SaveCommand = new DelegateCommand(Save, ValidateSaveState);
				return _SaveCommand as DelegateCommand;
			}
		}
		public Data.Account SelectedAccount {
			get {
				return _SelectedAccount;
			}
			set {
				_SelectedAccount = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedAccount"));
			}
		}
		public DelegateCommand SettingsCommand {
			get {
				if (_SettingsCommand == null)
					_SettingsCommand = new DelegateCommand(Settings, ValidateSettingsState);
				return _SettingsCommand as DelegateCommand;
			}
		}
		public decimal TodaysBalance {
			get {
				return _TodaysBalance;
			}
			set {
				_TodaysBalance = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("TodaysBalance"));
			}
		}
		#endregion Public Properties
	}
}
