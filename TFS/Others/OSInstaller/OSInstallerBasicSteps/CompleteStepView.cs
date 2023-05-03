namespace OSInstallerBasicSteps
{
	using MVVMFramework;
	using System;
	using System.ComponentModel;
	using System.Windows.Media;

	public class CompleteStepView : ViewModelBase, INotifyPropertyChanged
	{
		#region Public Methods
		public override void UpdateInterface()
		{
		}
		#endregion Public Methods

		#region Private Methods
		private void DisplayLog(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("DisplayLog"));
		}
		private bool ValidateDisplayLogState(object state)
		{
			return true;
		}
		#endregion Private Methods

		#region Public Events
		public event ExecuteUIActionHandler ExecuteUIAction;
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private DelegateCommand _DisplayLogCommand = null;
		private string _Header;
		private string _LogFileName;
		private string _Paragraph1Text;
		private Brush _WindowText;
		#endregion Private Fields

		#region Public Properties
		public DelegateCommand DisplayLogCommand
		{
			get
			{
				if (_DisplayLogCommand == null)
					_DisplayLogCommand = new DelegateCommand(DisplayLog, ValidateDisplayLogState);
				return _DisplayLogCommand as DelegateCommand;
			}
		}
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
		public string LogFileName
		{
			get
			{
				return _LogFileName;
			}
			set
			{
				_LogFileName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("LogFileName"));
			}
		}
		public string Paragraph1Text
		{
			get
			{
				return _Paragraph1Text;
			}
			set
			{
				_Paragraph1Text = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Paragraph1Text"));
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
