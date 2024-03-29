namespace OSInstallerBasicSteps
{
	using GregOsborne.MVVMFramework;
	using System;
	using System.ComponentModel;
	using System.Windows.Media;

	public class StartStepView : ViewModelBase, INotifyPropertyChanged
	{
		#region Public Constructors
		public StartStepView()
		{
			Header = "Header";
			Paragraph1Text = "Paragraph1";
			Paragraph2Text = "Paragraph2";
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
		private string _Paragraph1Text;
		private string _Paragraph2Text;

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
		public string Paragraph2Text
		{
			get
			{
				return _Paragraph2Text;
			}
			set
			{
				_Paragraph2Text = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Paragraph2Text"));
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
