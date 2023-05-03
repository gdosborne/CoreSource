namespace OSInstallerBasicSteps
{
	using MVVMFramework;
	using System;
	using System.ComponentModel;
	using System.Windows.Media;

	public class InstallingStepView : ViewModelBase, INotifyPropertyChanged
	{
		#region Public Constructors
		public InstallingStepView()
		{
			Header = "Header";
			ProgressMessage = "Installing...";
			ProgressMaximum = 100;
			ProgressValue = 0;
		}
		#endregion Public Constructors

		#region Public Methods
		public override void UpdateInterface()
		{
		}
		#endregion Public Methods

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private string _Header;
		private double _ProgressMaximum;
		private string _ProgressMessage;
		private double _ProgressValue;
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
		public double ProgressMaximum
		{
			get
			{
				return _ProgressMaximum;
			}
			set
			{
				_ProgressMaximum = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProgressMaximum"));
			}
		}
		public string ProgressMessage
		{
			get
			{
				return _ProgressMessage;
			}
			set
			{
				_ProgressMessage = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProgressMessage"));
			}
		}
		public double ProgressValue
		{
			get
			{
				return _ProgressValue;
			}
			set
			{
				_ProgressValue = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProgressValue"));
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
