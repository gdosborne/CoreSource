namespace DesktopClock
{
	using DesktopClock.Classes;
	using Microsoft.Win32;
	using MVVMFramework;
	using OSControls;
	using OSoftComponents;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.IO;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;
	using System.Windows.Media.Imaging;

	public class MainWindowView : ViewModelBase, INotifyPropertyChanged
	{
		#region Public Constructors
		public MainWindowView()
		{
			RegistryKey runKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
			StartWithWindows = runKey.GetValueNames().Contains("DesktopClock");

			ApplicationSettings = new AppSettings(SettingsLocations.AppData, "OSoft");

			HubIsDiskActivityIndicator = ApplicationSettings.GetSetting<bool>("Current", "HubIsDiskActivityIndicator", true);
			LastClockFaceDirectory = ApplicationSettings.GetSetting<string>("Current", "LastClockFaceDir", Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
			ClockSize = ApplicationSettings.GetSetting<double>("Current", "ClockSize", 200);
			MoonSize = ApplicationSettings.GetSetting<double>("Current", "MoonSize", 30);
			LocationTop = ApplicationSettings.GetSetting<double>("Current", "LocationTop", 200);
			LocationLeft = ApplicationSettings.GetSetting<double>("Current", "LocationLeft", 200);
			DateOffset = ApplicationSettings.GetSetting<double>("Current", "DateOffset", 50);
			MoonOpacity = ApplicationSettings.GetSetting<double>("Current", "MoonOpacity", 1.0);
			AlwaysOnTop = ApplicationSettings.GetSetting<bool>("Current", "AlwaysOnTop", false);
			SmoothSecondsHand = ApplicationSettings.GetSetting<bool>("Current", "SmoothSecondsHand", false);
			ShowMoon = ApplicationSettings.GetSetting<bool>("Current", "ShowMoon", true);
			ShowSecondsHand = ApplicationSettings.GetSetting<bool>("Current", "ShowSecondsHand", true);
			if (!string.IsNullOrEmpty(ApplicationSettings.GetSetting<string>("Current", "OtherFaceFileName", string.Empty)))
			{
				OtherClockFaceFileName = ApplicationSettings.GetSetting<string>("Current", "OtherFaceFileName", string.Empty);
				IsDefaultFaceChecked = false;
			}
			else
			{
				OtherClockFaceFileName = string.Empty;
				IsDefaultFaceChecked = true;
			}
			ClockFaceSource = IsDefaultFaceChecked ? null : OtherClockFaceSource;
			ShowDate = ApplicationSettings.GetSetting<bool>("Current", "ShowDate", true);
			ShowHandsDropShadow = ApplicationSettings.GetSetting<bool>("Current", "ShowHandsDropShadow", true);
			HandShortenAmount = ApplicationSettings.GetSetting<double>("Current", "HandShortenAmount", 0);
			HubSize = ApplicationSettings.GetSetting<double>("Current", "HubSize", 30);
			MoonHorizontalOffset = ApplicationSettings.GetSetting<double>("Current", "MoonHorizontalOffset", 0);
			MoonVerticalOffset = ApplicationSettings.GetSetting<double>("Current", "MoonVerticalOffset", 0);
			HandThickness = ApplicationSettings.GetSetting<double>("Current", "HandsThickness", 1.0);
			SelectedDateFontSize = ApplicationSettings.GetSetting<double>("Current", "SelectedDateFontSize", 12);
			SelectedDateFontWeight = (ApplicationSettings.GetSetting<string>("Current", "SelectedDateFontWeight", "Normal") == "Normal" || ApplicationSettings.GetSetting<string>("Current", "SelectedDateFontWeight", "Normal") == "Regular") ? System.Windows.FontWeights.Normal : System.Windows.FontWeights.Bold;
			SelectedDateFontFamily = new FontFamily(ApplicationSettings.GetSetting<string>("Current", "SelectedDateFontFamily", "Segoe UI"));

			var handsBrushKey = ApplicationSettings.GetSetting<string>("Current", "HandsBrush", "#FF000000");
			var secondsBrushKey = ApplicationSettings.GetSetting<string>("Current", "SecondsBrush", "#FF000000");
			var dateBrushKey = ApplicationSettings.GetSetting<string>("Current", "DateBrush", "#FF000000");
			var hubBrushKey = ApplicationSettings.GetSetting<string>("Current", "HubBrush", "#FF000000");
			var activityBrushKey = ApplicationSettings.GetSetting<string>("Current", "ActivityBrush", "#FFFF000");

			HandsBrush = new SolidColorBrush(handsBrushKey.ToColor());
			SecondsBrush = new SolidColorBrush(secondsBrushKey.ToColor());
			DateBrush = new SolidColorBrush(dateBrushKey.ToColor());
			HubBrush = new SolidColorBrush(hubBrushKey.ToColor());
			ActivityBrush = new SolidColorBrush(activityBrushKey.ToColor());

			TimeZones = new List<TimeZoneInfo>(TimeZoneInfo.GetSystemTimeZones());
			SelectedTimeZone = TimeZoneInfo.Local;

			_IsInitializing = false;
		}
		#endregion Public Constructors

		#region Public Methods
		public void Initialize(MainWindow window)
		{
			var activityInstanceName = ApplicationSettings.GetSetting<string>("Current", "ActivityInstanceName", string.Empty);

			Activity = new DiskActivity(activityInstanceName, true);
			if (Activity.ActivityIsEnabled)
				Activity.ActivitySensed += Activity_ActivitySensed;
			else
			{
				var instanceWin = new SelectPerformanCounterInstanceName();
				instanceWin.View.CounterNames = new List<string>(Activity.InstanceNames);
				var result = instanceWin.ShowDialog();
				if (result.GetValueOrDefault())
				{
					var instanceName = instanceWin.View.SelectedInstanceName;
					Activity.SetupCounter(instanceName);
					if (Activity.ActivityIsEnabled)
					{
						Activity.ActivitySensed += Activity_ActivitySensed;
						ApplicationSettings.SetSetting<string>("Current", "ActivityInstanceName", instanceName);
					}
				}
				else
					App.Current.Shutdown();
			}
		}

		public void SaveSettings()
		{
			if (_IsInitializing)
				return;

			ApplicationSettings.SetSetting<string>("Current", "OtherFaceFileName", (OtherClockFaceFileName == null).Choose<string>(string.Empty, OtherClockFaceFileName));
			ApplicationSettings.SetSetting<string>("Current", "LastClockFaceDir", LastClockFaceDirectory);
			ApplicationSettings.SetSetting<string>("Current", "HandsBrush", (HandsBrush as SolidColorBrush).Color.ToHexValue());
			ApplicationSettings.SetSetting<string>("Current", "DateBrush", (DateBrush as SolidColorBrush).Color.ToHexValue());
			ApplicationSettings.SetSetting<string>("Current", "SecondsBrush", (SecondsBrush as SolidColorBrush).Color.ToHexValue());
			ApplicationSettings.SetSetting<string>("Current", "HubBrush", (HubBrush as SolidColorBrush).Color.ToHexValue());
			ApplicationSettings.SetSetting<string>("Current", "ActivityBrush", (ActivityBrush as SolidColorBrush).Color.ToHexValue());
			ApplicationSettings.SetSetting<double>("Current", "MoonHorizontalOffset", MoonHorizontalOffset);
			ApplicationSettings.SetSetting<double>("Current", "MoonOpacity", MoonOpacity);
			ApplicationSettings.SetSetting<double>("Current", "MoonVerticalOffset", MoonVerticalOffset);
			ApplicationSettings.SetSetting<bool>("Current", "ShowHandsDropShadow", ShowHandsDropShadow);
			ApplicationSettings.SetSetting<bool>("Current", "ShowMoon", ShowMoon);
			ApplicationSettings.SetSetting<bool>("Current", "ShowDate", ShowDate);
			ApplicationSettings.SetSetting<bool>("Current", "ShowSecondsHand", ShowSecondsHand);
			ApplicationSettings.SetSetting<bool>("Current", "AlwaysOnTop", AlwaysOnTop);
			ApplicationSettings.SetSetting<bool>("Current", "SmoothSecondsHand", SmoothSecondsHand);
			ApplicationSettings.SetSetting<bool>("Current", "HubIsDiskActivityIndicator", HubIsDiskActivityIndicator);
			ApplicationSettings.SetSetting<double>("Current", "HandShortenAmount", HandShortenAmount);
			ApplicationSettings.SetSetting<double>("Current", "HubSize", HubSize);
			ApplicationSettings.SetSetting<double>("Current", "HandsThickness", HandThickness);
			ApplicationSettings.SetSetting<double>("Current", "ClockSize", ClockSize);
			ApplicationSettings.SetSetting<double>("Current", "MoonSize", MoonSize);
			ApplicationSettings.SetSetting<double>("Current", "LocationTop", LocationTop);
			ApplicationSettings.SetSetting<double>("Current", "LocationLeft", LocationLeft);
			ApplicationSettings.SetSetting<double>("Current", "DateOffset", DateOffset);
			ApplicationSettings.SetSetting<double>("Current", "SelectedDateFontSize", SelectedDateFontSize);
			ApplicationSettings.SetSetting<string>("Current", "SelectedDateFontWeight", SelectedDateFontWeight.ToString());
			ApplicationSettings.SetSetting<string>("Current", "SelectedDateFontFamily", SelectedDateFontFamily.ToString());
		}
		#endregion Public Methods

		#region Private Methods
		private void Activity_ActivitySensed(object sender, ActivityEventArgs e)
		{
			//TheHubBrush = e.IsOn ? ActivityBrush : HubBrush;
			TheHubBrush = HubBrush;
			ActivityValue = e.Value;
		}
		private void ActivityColor(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("ShowActivityColorPopup", null));
		}
		private void AlwaysOnTopChange(object state)
		{
			AlwaysOnTop = !AlwaysOnTop;
		}
		private void Exit(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("Exit", null));
		}
		private void Settings(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("Settings", null));
		}
		private bool ValidateExitState(object state)
		{
			return true;
		}
		private bool ValidateSettingsState(object state)
		{
			return true;
		}
		#endregion Private Methods

		#region Public Events
		public event ExecuteUIActionHandler ExecuteUIAction;
		public override event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private Brush _ActivityBrush;
		private double _ActivityHeight;
		private Thickness _ActivityMargin;
		private double _ActivityValue;
		private Visibility _ActivityVisibility;
		private double _ActivityWidth;
		private bool _AlwaysOnTop;
		private AppSettings _ApplicationSettings;
		private ImageSource _ClockFaceSource;
		private double _ClockSize;
		private Visibility _ColorsVisibility;
		private Brush _DateBrush;
		private double _DateOffset;
		private Visibility _DateVisibility;
		private DelegateCommand _ExitCommand = null;
		private List<MenuItem> _FontSizes;
		private List<MenuItem> _FontWeights;
		private Brush _HandsBrush;
		private double _HandShortenAmount;
		private double _HandThickness;
		private Brush _HubBrush;
		private bool _HubIsDiskActivityIndicator;
		private double _HubSize;
		private bool _IsDefaultFaceChecked;
		private bool _IsInitializing = true;
		private bool _IsInitialLocationSet;
		private string _LastClockFaceDir;
		private double _LocationLeft;
		private double _LocationTop;
		private double _MoonHorizontalOffset;
		private Thickness _MoonMargin;
		private double _MoonOpacity;
		private double _MoonSize;
		private double _MoonVerticalOffset;
		private Visibility _MoonVisibility;
		private ImageSource _OtherClockFaceSource;
		private string _OtherClockFileFileName;
		private Brush _SecondsBrush;
		private Visibility _SecondsVisibility;
		private FontFamily _SelectedDateFontFamily;
		private double _SelectedDateFontSize;
		private FontWeight _SelectedDateFontWeight;
		private TimeZoneInfo _SelectedTimeZone;
		private DelegateCommand _SettingsCommand = null;
		private bool _ShowDate;
		private bool _ShowHandsDropShadow;
		private bool _ShowMoon;
		private bool _ShowSecondsHand;
		private double _SliderMaximum;
		private double _SliderMinimum;
		private double _SliderSmallChange;
		private bool _SliderSnapToTick;
		private double _SliderTickFrequency;
		private double _SliderValue;
		private Visibility _SliderVisibility;
		private bool _SmoothSecondsHand;
		private bool _StartWithWindows;
		private Brush _TheHubBrush;
		private List<TimeZoneInfo> _TimeZones;
		private Visibility _TimeZoneVisibility;
		#endregion Private Fields

		#region Public Properties
		public DiskActivity Activity
		{
			get
			{
				return App.Activity;
			}
			set
			{
				App.Activity = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Activity"));
			}
		}
		public Brush ActivityBrush
		{
			get
			{
				return _ActivityBrush;
			}
			set
			{
				_ActivityBrush = value;
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ActivityBrush"));
			}
		}
		public double ActivityHeight
		{
			get
			{
				return _ActivityHeight;
			}
			set
			{
				_ActivityHeight = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ActivityHeight"));
			}
		}
		public Thickness ActivityMargin
		{
			get
			{
				return _ActivityMargin;
			}
			set
			{
				_ActivityMargin = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ActivityMargin"));
			}
		}
		public double ActivityValue
		{
			get
			{
				return _ActivityValue;
			}
			set
			{
				_ActivityValue = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ActivityValue"));
			}
		}
		public Visibility ActivityVisibility
		{
			get
			{
				return _ActivityVisibility;
			}
			set
			{
				_ActivityVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ActivityVisibility"));
			}
		}
		public double ActivityWidth
		{
			get
			{
				return _ActivityWidth;
			}
			set
			{
				_ActivityWidth = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ActivityWidth"));
			}
		}
		public bool AlwaysOnTop
		{
			get
			{
				return _AlwaysOnTop;
			}
			set
			{
				_AlwaysOnTop = value;
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AlwaysOnTop"));
			}
		}

		public AppSettings ApplicationSettings
		{
			get
			{
				return _ApplicationSettings;
			}
			set
			{
				_ApplicationSettings = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ApplicationSettings"));
			}
		}

		public ImageSource ClockFaceSource
		{
			get
			{
				return _ClockFaceSource;
			}
			set
			{
				_ClockFaceSource = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ClockFaceSource"));
			}
		}

		public double ClockSize
		{
			get
			{
				return _ClockSize;
			}
			set
			{
				_ClockSize = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ClockSize"));
			}
		}

		public Brush DateBrush
		{
			get
			{
				return _DateBrush;
			}
			set
			{
				_DateBrush = value;
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("DateBrush"));
			}
		}

		public double DateOffset
		{
			get
			{
				return _DateOffset;
			}
			set
			{
				_DateOffset = value;
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("DateOffset"));
			}
		}

		public Visibility DateVisibility
		{
			get
			{
				return _DateVisibility;
			}
			set
			{
				_DateVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("DateVisibility"));
			}
		}

		public DelegateCommand ExitCommand
		{
			get
			{
				if (_ExitCommand == null)
					_ExitCommand = new DelegateCommand(Exit, ValidateExitState);
				return _ExitCommand as DelegateCommand;
			}
		}

		public List<MenuItem> FontSizes
		{
			get
			{
				return _FontSizes;
			}
			set
			{
				_FontSizes = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FontSizes"));
			}
		}

		public List<MenuItem> FontWeights
		{
			get
			{
				return _FontWeights;
			}
			set
			{
				_FontWeights = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FontWeights"));
			}
		}

		public Brush HandsBrush
		{
			get
			{
				return _HandsBrush;
			}
			set
			{
				_HandsBrush = value;
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("HandsBrush"));
			}
		}

		public double HandShortenAmount
		{
			get
			{
				return _HandShortenAmount;
			}
			set
			{
				_HandShortenAmount = value;
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("HandShortenAmount"));
			}
		}

		public double HandThickness
		{
			get
			{
				return _HandThickness;
			}
			set
			{
				value = (value < 1.0).Choose<double>(1.0, (value > 6.0).Choose<double>(6.0, value));
				_HandThickness = value;
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("HandThickness"));
			}
		}

		public Brush HubBrush
		{
			get
			{
				return _HubBrush;
			}
			set
			{
				_HubBrush = value;
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("HubBrush"));
			}
		}

		public bool HubIsDiskActivityIndicator
		{
			get
			{
				return _HubIsDiskActivityIndicator;
			}
			set
			{
				_HubIsDiskActivityIndicator = value;
				ActivityVisibility = value ? Visibility.Visible : Visibility.Collapsed;
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("HubIsDiskActivityIndicator"));
			}
		}
		public double HubSize
		{
			get
			{
				return _HubSize;
			}
			set
			{
				_HubSize = value;
				ActivityMargin = new Thickness(0, (2 * value), 0, 0);
				ActivityHeight = _HubSize / 2;
				ActivityWidth = _HubSize * 3;
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("HubSize"));
			}
		}
		public bool IsDefaultFaceChecked
		{
			get
			{
				return _IsDefaultFaceChecked;
			}
			set
			{
				_IsDefaultFaceChecked = value;
				if (_IsDefaultFaceChecked)
					OtherClockFaceFileName = string.Empty;
				ClockFaceSource = _IsDefaultFaceChecked.Choose<ImageSource>(null, OtherClockFaceSource);
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IsDefaultFaceChecked"));
			}
		}

		public bool IsInitialLocationSet
		{
			get
			{
				return _IsInitialLocationSet;
			}
			set
			{
				_IsInitialLocationSet = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IsInitialLocationSet"));
			}
		}

		public string LastClockFaceDirectory
		{
			get
			{
				return _LastClockFaceDir;
			}
			set
			{
				_LastClockFaceDir = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("LastClockFaceDir"));
			}
		}

		public double LocationLeft
		{
			get
			{
				return _LocationLeft;
			}
			set
			{
				_LocationLeft = value;
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("LocationLeft"));
			}
		}

		public double LocationTop
		{
			get
			{
				return _LocationTop;
			}
			set
			{
				_LocationTop = value;
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("LocationTop"));
			}
		}

		public double MoonHorizontalOffset
		{
			get
			{
				return _MoonHorizontalOffset;
			}
			set
			{
				_MoonHorizontalOffset = value;
				SaveSettings();
				MoonMargin = new Thickness(MoonHorizontalOffset, MoonVerticalOffset, 0, 0);
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("MoonHorizontalOffset"));
			}
		}

		public Thickness MoonMargin
		{
			get
			{
				return _MoonMargin;
			}
			set
			{
				_MoonMargin = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("MoonMargin"));
			}
		}

		public double MoonOpacity
		{
			get
			{
				return _MoonOpacity;
			}
			set
			{
				_MoonOpacity = value;
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("MoonOpacity"));
			}
		}

		public double MoonSize
		{
			get
			{
				return _MoonSize;
			}
			set
			{
				_MoonSize = value;
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("MoonSize"));
			}
		}

		public double MoonVerticalOffset
		{
			get
			{
				return _MoonVerticalOffset;
			}
			set
			{
				_MoonVerticalOffset = value;
				SaveSettings();
				MoonMargin = new Thickness(MoonHorizontalOffset, MoonVerticalOffset, 0, 0);
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("MoonVerticalOffset"));
			}
		}

		public Visibility MoonVisibility
		{
			get
			{
				return _MoonVisibility;
			}
			set
			{
				_MoonVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("MoonVisibility"));
			}
		}

		public string OtherClockFaceFileName
		{
			get
			{
				return _OtherClockFileFileName;
			}
			set
			{
				if (!string.IsNullOrEmpty(value) && File.Exists(value))
				{
					BitmapImage bi = new BitmapImage();
					bi.BeginInit();
					bi.UriSource = new Uri(value, UriKind.RelativeOrAbsolute);
					bi.EndInit();
					OtherClockFaceSource = bi;
					IsDefaultFaceChecked = false;
				}
				else
					ClockFaceSource = null;

				_OtherClockFileFileName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("OtherClockFileFileName"));
			}
		}

		public ImageSource OtherClockFaceSource
		{
			get
			{
				return _OtherClockFaceSource;
			}
			set
			{
				_OtherClockFaceSource = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("OtherClockFaceSource"));
			}
		}

		public Brush SecondsBrush
		{
			get
			{
				return _SecondsBrush;
			}
			set
			{
				_SecondsBrush = value;
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SecondsBrush"));
			}
		}

		public Visibility SecondsVisibility
		{
			get
			{
				return _SecondsVisibility;
			}
			set
			{
				_SecondsVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SecondsVisibility"));
			}
		}

		public FontFamily SelectedDateFontFamily
		{
			get
			{
				return _SelectedDateFontFamily;
			}
			set
			{
				_SelectedDateFontFamily = value;
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedDateFontFamily"));
			}
		}

		public double SelectedDateFontSize
		{
			get
			{
				return _SelectedDateFontSize;
			}
			set
			{
				_SelectedDateFontSize = value;
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedDateFontSize"));
			}
		}

		public FontWeight SelectedDateFontWeight
		{
			get
			{
				return _SelectedDateFontWeight;
			}
			set
			{
				_SelectedDateFontWeight = value;
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedDateFontWeight"));
			}
		}

		public TimeZoneInfo SelectedTimeZone
		{
			get
			{
				return _SelectedTimeZone;
			}
			set
			{
				_SelectedTimeZone = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedTimeZone"));
			}
		}

		public DelegateCommand SettingsCommand
		{
			get
			{
				if (_SettingsCommand == null)
					_SettingsCommand = new DelegateCommand(Settings, ValidateSettingsState);
				return _SettingsCommand as DelegateCommand;
			}
		}

		public bool ShowDate
		{
			get
			{
				return _ShowDate;
			}
			set
			{
				_ShowDate = value;
				DateVisibility = value.Choose<Visibility>(Visibility.Visible, Visibility.Collapsed);
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ShowDate"));
			}
		}

		public bool ShowHandsDropShadow
		{
			get
			{
				return _ShowHandsDropShadow;
			}
			set
			{
				_ShowHandsDropShadow = value;
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ShowHandsDropShadow"));
			}
		}

		public bool ShowMoon
		{
			get
			{
				return _ShowMoon;
			}
			set
			{
				_ShowMoon = value;
				MoonVisibility = value.Choose<Visibility>(Visibility.Visible, Visibility.Collapsed);
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ShowMoon"));
			}
		}

		public bool ShowSecondsHand
		{
			get
			{
				return _ShowSecondsHand;
			}
			set
			{
				_ShowSecondsHand = value;
				SecondsVisibility = value.Choose<Visibility>(Visibility.Visible, Visibility.Collapsed);
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ShowSecondsHand"));
			}
		}

		public double SliderMaximum
		{
			get
			{
				return _SliderMaximum;
			}
			set
			{
				_SliderMaximum = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SliderMaximum"));
			}
		}

		public double SliderMinimum
		{
			get
			{
				return _SliderMinimum;
			}
			set
			{
				_SliderMinimum = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SliderMinimum"));
			}
		}

		public double SliderSmallChange
		{
			get
			{
				return _SliderSmallChange;
			}
			set
			{
				_SliderSmallChange = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SliderSmallChange"));
			}
		}

		public bool SliderSnapToTick
		{
			get
			{
				return _SliderSnapToTick;
			}
			set
			{
				_SliderSnapToTick = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SliderSnapToTick"));
			}
		}

		public double SliderTickFrequency
		{
			get
			{
				return _SliderTickFrequency;
			}
			set
			{
				_SliderTickFrequency = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SliderTickFrequency"));
			}
		}

		public double SliderValue
		{
			get
			{
				return _SliderValue;
			}
			set
			{
				_SliderValue = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SliderValue"));
			}
		}

		public bool SmoothSecondsHand
		{
			get
			{
				return _SmoothSecondsHand;
			}
			set
			{
				_SmoothSecondsHand = value;
				SaveSettings();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SmoothSecondsHand"));
			}
		}

		public bool StartWithWindows
		{
			get
			{
				return _StartWithWindows;
			}
			set
			{
				_StartWithWindows = value;
				RegistryKey runKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
				if (_StartWithWindows)
					runKey.SetValue("DesktopClock", this.GetType().Assembly.Location);
				else if (runKey.GetValueNames().Contains("DesktopClock"))
					runKey.DeleteValue("DesktopClock");

				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("StartWithWindows"));
			}
		}

		public Brush TheHubBrush
		{
			get
			{
				return _TheHubBrush;
			}
			set
			{
				_TheHubBrush = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("TheHubBrush"));
			}
		}

		public List<TimeZoneInfo> TimeZones
		{
			get
			{
				return _TimeZones;
			}
			set
			{
				_TimeZones = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("TimeZones"));
			}
		}
		#endregion Public Properties
	}
}
