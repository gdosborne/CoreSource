namespace SNC.OptiRamp.Application.Developer.Views {

	using MVVMFramework;
	using SNC.OptiRamp.Application.Developer.Classes.Management;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Reflection;
	using System.Windows;
using System.Windows.Threading;

	internal class SplashWindowView : INotifyPropertyChanged {

		#region Public Constructors
		public SplashWindowView() {
			MessageVisibility = Visibility.Collapsed;
			var assy = this.GetType().Assembly;
			var types = new List<Type> {
				typeof(AssemblyTitleAttribute),
				typeof(AssemblyDescriptionAttribute),
				typeof(AssemblyCompanyAttribute),
				typeof(AssemblyCopyrightAttribute)
			};
			foreach (var t in types) {
				if (t == typeof(AssemblyTitleAttribute)) {
					AssemblyTitleAttribute[] items = (AssemblyTitleAttribute[])assy.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
					Title = items != null ? items[0].Title : "OptiRamp© Developer";
					OutputMessage = Title;
				}
				else if (t == typeof(AssemblyDescriptionAttribute)) {
					AssemblyDescriptionAttribute[] items = (AssemblyDescriptionAttribute[])assy.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
					Description = items != null ? items[0].Description : "n/a";
				}
				else if (t == typeof(AssemblyCompanyAttribute)) {
					AssemblyCompanyAttribute[] items = (AssemblyCompanyAttribute[])assy.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
					Company = items != null ? items[0].Company : "n/a";
				}
				else if (t == typeof(AssemblyCopyrightAttribute)) {
					AssemblyCopyrightAttribute[] items = (AssemblyCopyrightAttribute[])assy.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
					Copyright = items != null ? items[0].Copyright : "n/a";
				}
			}
			Version = assy.GetName().Version;
		}
		#endregion Public Constructors
		private DispatcherTimer dt = null;
		private Visibility _MessageVisibility;
		public Visibility MessageVisibility {
			get {
				return _MessageVisibility;
			}
			set {
				_MessageVisibility = value;
				if (value == Visibility.Visible) {
					if (dt == null) {
						dt = new DispatcherTimer {
							Interval = TimeSpan.FromSeconds(1.5)
						};
						dt.Tick += dt_Tick;
					}
					dt.Start();
				}
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("MessageVisibility"));
			}
		}

		void dt_Tick(object sender, EventArgs e) {
			MessageVisibility = Visibility.Collapsed;
			dt.Stop();
		}
		#region Public Methods
		public void Initialize(Window window) {
		}
		public void InitView() {
			App.ApplicationExtensions.AddSplashMessage += ApplicationExtensions_AddSplashMessage;
		}
		public void Persist(Window window) {
		}
		public void UpdateInterface() {
		}
		#endregion Public Methods
		private DelegateCommand _CopyToClipboardCommand = null;
		public DelegateCommand CopyToClipboardCommand {
			get {
				if (_CopyToClipboardCommand == null)
					_CopyToClipboardCommand = new DelegateCommand(CopyToClipboard, ValidateCopyToClipboardState);
				return _CopyToClipboardCommand as DelegateCommand;
			}
		}
		private void CopyToClipboard(object state) {
			Clipboard.SetText(string.Format("Version {0}" + Environment.NewLine + "{1}", Version, OutputMessage));
			MessageVisibility = Visibility.Visible;
		}
		private bool ValidateCopyToClipboardState(object state) {
			return true;
		}

		#region Private Methods
		private void ApplicationExtensions_AddSplashMessage(object sender, AddSplashMessageEventArgs e) {
			OutputMessage += Environment.NewLine + e.Message;
		}
		private void Close(object state) {
			DialogResult = false;
		}
		private bool ValidateCloseState(object state) {
			return true;
		}
		#endregion Private Methods

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private DelegateCommand _CloseCommand = null;
		private string _Company;
		private string _Copyright;
		private string _Description;
		private bool? _DialogResult;
		private string _OutputMessage;
		private string _Title;
		private Version _Version;
		#endregion Private Fields

		#region Public Properties
		public DelegateCommand CloseCommand {
			get {
				if (_CloseCommand == null)
					_CloseCommand = new DelegateCommand(Close, ValidateCloseState);
				return _CloseCommand as DelegateCommand;
			}
		}
		public string Company {
			get {
				return _Company;
			}
			set {
				_Company = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Company"));
			}
		}
		public string Copyright {
			get {
				return _Copyright;
			}
			set {
				_Copyright = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Copyright"));
			}
		}
		public string Description {
			get {
				return _Description;
			}
			set {
				_Description = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Description"));
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
		public string OutputMessage {
			get {
				return _OutputMessage;
			}
			set {
				_OutputMessage = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("OutputMessage"));
			}
		}
		public string Title {
			get {
				return _Title;
			}
			set {
				_Title = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Title"));
			}
		}
		public Version Version {
			get {
				return _Version;
			}
			set {
				_Version = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Version"));
			}
		}
		#endregion Public Properties
	}
}
