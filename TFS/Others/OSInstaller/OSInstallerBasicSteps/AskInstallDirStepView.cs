namespace OSInstallerBasicSteps
{
	using GregOsborne.MVVMFramework;
	using System;
	using System.ComponentModel;
	using System.Windows.Media;

	public class AskInstallDirStepView : ViewModelBase, INotifyPropertyChanged
	{
		#region Public Constructors
		public AskInstallDirStepView()
		{
			Header = "Header";
			InstallationDirectoryPrompt = "Prompt";
			InstallationDirectory = string.Empty;
		}
		#endregion Public Constructors

		#region Public Methods
		public override void UpdateInterface()
		{
		}
		#endregion Public Methods

		#region Private Methods
		private void SelectDirectory(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUiActionEventArgs("SelectInstallationDirectory"));
		}
		private bool ValidateSelectDirectoryState(object state)
		{
			return true;
		}
		#endregion Private Methods

		#region Public Events
		public event GregOsborne.MVVMFramework.ExecuteUiActionHandler ExecuteUIAction;
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private string _Header;
		private string _InstallationDirectory;
		private string _InstallationDirectoryPrompt;
		private DelegateCommand _SelectDirectoryCommand = null;
		private Brush _WindowText;
		#endregion Private Fields

		#region Public Properties
		public string Header
		{
			get
			{
				return _Header;
			}
			set
			{
				_Header = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Header"));
			}
		}
		public string InstallationDirectory
		{
			get
			{
				return _InstallationDirectory;
			}
			set
			{
				_InstallationDirectory = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("InstallationDirectory"));
			}
		}
		public string InstallationDirectoryPrompt
		{
			get
			{
				return _InstallationDirectoryPrompt;
			}
			set
			{
				_InstallationDirectoryPrompt = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("InstallationDirectoryPrompt"));
			}
		}
		public DelegateCommand SelectDirectoryCommand
		{
			get
			{
				if (_SelectDirectoryCommand == null)
					_SelectDirectoryCommand = new DelegateCommand(SelectDirectory, ValidateSelectDirectoryState);
				return _SelectDirectoryCommand as DelegateCommand;
			}
		}
		public Brush WindowText
		{
			get
			{
				return _WindowText;
			}
			set
			{
				_WindowText = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("WindowText"));
			}
		}
		#endregion Public Properties
	}
}
