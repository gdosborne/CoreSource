namespace OSInstallerBasicSteps
{
	using GregOsborne.MVVMFramework;
	using System;
	using System.ComponentModel;
	using System.Windows.Media;

	public class InformationCollectedStepView : ViewModelBase, INotifyPropertyChanged
	{
		#region Public Constructors
		public InformationCollectedStepView()
		{
			Header = "Header";
			Paragraph1Text = string.Empty;
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
		private string _CompletedText;
		private string _Header;
		private string _Paragraph1Text;
		private Brush _WindowText;
		#endregion Private Fields

		#region Public Properties
		public string CompletedText
		{
			get
			{
				return _CompletedText;
			}
			set
			{
				_CompletedText = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CompletedText"));
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
