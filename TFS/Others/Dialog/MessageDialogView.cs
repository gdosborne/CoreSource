namespace GregOsborne.Dialog {
	using MVVMFramework;
	using System;
	using System.ComponentModel;
	using System.Windows;
	using System.Windows.Media;

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
	internal class MessageDialogView : INotifyPropertyChanged {
		public MessageDialogView() {
			Source = TaskDialog.GetImageSourceByName("Information.png");
			Button1Visibility = Visibility.Collapsed;
			Button2Visibility = Visibility.Collapsed;
			Button3Visibility = Visibility.Collapsed;
			AdditionalInfoVisibility = Visibility.Collapsed;
			Button1Width = 60;
			Button2Width = 60;
			Button3Width = 60;
		}
		public event PropertyChangedEventHandler PropertyChanged;
		public void UpdateInterface() {

		}
		public void InitView() {

		}
		private int _ButtonValue;
		public int ButtonValue {
			get {
				return _ButtonValue;
			}
			set {
				_ButtonValue = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ButtonValue"));
			}
		}
		private ImageSource _Source;
		public ImageSource Source {
			get {
				return _Source;
			}
			set {
				_Source = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Source"));
			}
		}
		private string _MessageText;
		public string MessageText {
			get {
				return _MessageText;
			}
			set {
				_MessageText = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("MessageText"));
			}
		}
		private double _Button1Width;
		public double Button1Width {
			get {
				return _Button1Width;
			}
			set {
				_Button1Width = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Button1Width"));
			}
		}
		private double _Button2Width;
		public double Button2Width {
			get {
				return _Button2Width;
			}
			set {
				_Button2Width = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Button2Width"));
			}
		}
		private double _Button3Width;
		public double Button3Width {
			get {
				return _Button3Width;
			}
			set {
				_Button3Width = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Button3Width"));
			}
		}
		private Button _Button1;
		public Button Button1 {
			get {
				return _Button1;
			}
			set {
				_Button1 = value;
				Button1Text = value.Text;
				if (value.Width > 0)
					Button1Width = value.Width;
				Button1Visibility = Visibility.Visible;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Button1"));
			}
		}
		private Button _Button2;
		public Button Button2 {
			get {
				return _Button2;
			}
			set {
				_Button2 = value;
				Button2Text = value.Text;
				if (value.Width > 0)
					Button2Width = value.Width;
				Button2Visibility = Visibility.Visible;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Button2"));
			}
		}
		private Button _Button3;
		public Button Button3 {
			get {
				return _Button3;
			}
			set {
				_Button3 = value;
				Button3Text = value.Text;
				if (value.Width > 0)
					Button3Width = value.Width;
				Button3Visibility = Visibility.Visible;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Button3"));
			}
		}
		private string _Button1Text;
		public string Button1Text {
			get {
				return _Button1Text;
			}
			set {
				_Button1Text = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Button1Text"));
			}
		}
		private string _Button2Text;
		public string Button2Text {
			get {
				return _Button2Text;
			}
			set {
				_Button2Text = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Button2Text"));
			}
		}
		private string _Button3Text;
		public string Button3Text {
			get {
				return _Button3Text;
			}
			set {
				_Button3Text = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Button3Text"));
			}
		}
		private Visibility _Button1Visibility;
		public Visibility Button1Visibility {
			get {
				return _Button1Visibility;
			}
			set {
				_Button1Visibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Button1Visibility"));
			}
		}
		private Visibility _Button2Visibility;
		public Visibility Button2Visibility {
			get {
				return _Button2Visibility;
			}
			set {
				_Button2Visibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Button2Visibility"));
			}
		}
		private Visibility _Button3Visibility;
		public Visibility Button3Visibility {
			get {
				return _Button3Visibility;
			}
			set {
				_Button3Visibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Button3Visibility"));
			}
		}
		private DelegateCommand _Button1Command = null;
		public DelegateCommand Button1Command {
			get {
				if (_Button1Command == null)
					_Button1Command = new DelegateCommand(Button1x, ValidateButton1State);
				return _Button1Command as DelegateCommand;
			}
		}
		private void Button1x(object state) {
			ButtonValue = Button1.CustomValue;
		}
		private bool ValidateButton1State(object state) {
			return true;
		}
		private DelegateCommand _Button2Command = null;
		public DelegateCommand Button2Command {
			get {
				if (_Button2Command == null)
					_Button2Command = new DelegateCommand(Button2x, ValidateButton2State);
				return _Button2Command as DelegateCommand;
			}
		}
		private void Button2x(object state) {
			ButtonValue = Button2.CustomValue;
		}
		private bool ValidateButton2State(object state) {
			return true;
		}
		private DelegateCommand _Button3Command = null;
		public DelegateCommand Button3Command {
			get {
				if (_Button3Command == null)
					_Button3Command = new DelegateCommand(Button3x, ValidateButton3State);
				return _Button3Command as DelegateCommand;
			}
		}
		private void Button3x(object state) {
			ButtonValue = Button3.CustomValue;
		}
		private bool ValidateButton3State(object state) {
			return true;
		}
		private string _AdditionalInformation;
		public string AdditionalInformation {
			get {
				return _AdditionalInformation;
			}
			set {
				_AdditionalInformation = value;
				AdditionalInfoVisibility = string.IsNullOrEmpty(_AdditionalInformation) ? Visibility.Collapsed : Visibility.Visible;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AdditionalInformation"));
			}
		}
		private Visibility _AdditionalInfoVisibility;
		public Visibility AdditionalInfoVisibility {
			get {
				return _AdditionalInfoVisibility;
			}
			set {
				_AdditionalInfoVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AdditionalInfoVisibility"));
			}
		}
		private bool _IsAdditionalInformationExpanded;
		public bool IsAdditionalInformationExpanded {
			get {
				return _IsAdditionalInformationExpanded;
			}
			set {
				_IsAdditionalInformationExpanded = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IsAdditionalInformationExpanded"));
			}
		}
	}
}
