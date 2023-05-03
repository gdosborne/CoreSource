namespace DesktopClock
{
	using DesktopClock.Classes;
	using Microsoft.Win32;
	using MVVMFramework;
	using MyApplication.Primitives;
	using MyApplication.Windows;
	using OSControls;
	using OSoftComponents;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Controls.Primitives;
	using System.Windows.Data;
	using System.Windows.Media;

	public class SettingsWindowView : ViewModelBase, INotifyPropertyChanged
	{
		#region Public Constructors
		public SettingsWindowView()
		{
		}
		#endregion Public Constructors

		#region Public Methods
		public void BeginInitialization()
		{
			RegistryKey runKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
			StartWithWindows = runKey.GetValueNames().Contains("DesktopClock");
			var settings = new AppSettings(SettingsLocations.AppData, "OSoft");

			SetDefaults(settings);

			dynamicSettings = new Dictionary<TreeViewItem, Dictionary<string, FrameworkElement>>();

			appearanceTreeViewItem = new TreeViewItem { Header = "Appearance" };
			environmentTreeViewItem = new TreeViewItem { Header = "Environment" };

			clockTreeViewItem = new TreeViewItem { Header = "Clock" };
			colorsTreeViewItem = new TreeViewItem { Header = "Colors" };
			fontsTreeViewItem = new TreeViewItem { Header = "Fonts" };
			sizeTreeViewItem = new TreeViewItem { Header = "Size" };
			positionTreeViewItem = new TreeViewItem { Header = "Position" };

			generalTreeViewItem = new TreeViewItem { Header = "General" };
			indicatorTreeViewItem = new TreeViewItem { Header = "Indicators" };

			appearanceTreeViewItem.Items.Add(clockTreeViewItem);
			appearanceTreeViewItem.Items.Add(colorsTreeViewItem);
			appearanceTreeViewItem.Items.Add(fontsTreeViewItem);
			appearanceTreeViewItem.Items.Add(sizeTreeViewItem);
			appearanceTreeViewItem.Items.Add(positionTreeViewItem);

			environmentTreeViewItem.Items.Add(generalTreeViewItem);
			environmentTreeViewItem.Items.Add(indicatorTreeViewItem);

			var generalItems = new Dictionary<string, FrameworkElement>();
			var indicatorItems = new Dictionary<string, FrameworkElement>();
			var clockItems = new Dictionary<string, FrameworkElement>();
			var sizeItems = new Dictionary<string, FrameworkElement>();
			var positionItems = new Dictionary<string, FrameworkElement>();
			var colorItems = new Dictionary<string, FrameworkElement>();
			var fontItems = new Dictionary<string, FrameworkElement>();

			generalItems.Add("Start with windows", GetUIElement<CheckBox>("Start with windows", StartWithWindows, "StartWithWindows".GetBinding(this), null));
			generalItems.Add("Always on top", GetUIElement<CheckBox>("Always on top", AlwaysOnTop, "AlwaysOnTop".GetBinding(this), null));
			generalItems.Add("Time zone", GetUIElement<TimeZoneAndLabel>("Time zone", SelectedTimeZone, "SelectedTimeZone".GetBinding(this), null));

			indicatorItems.Add("Center hub is disk activity", GetUIElement<CheckBox>("Center hub is disk activity", HubIsDiskActivityIndicator, "HubIsDiskActivityIndicator".GetBinding(this), null));
			indicatorItems.Add("Show moon phase", GetUIElement<CheckBox>("Show moon phase", ShowMoon, "ShowMoon".GetBinding(this), null));
			indicatorItems.Add("Show date", GetUIElement<CheckBox>("Show date", ShowDate, "ShowDate".GetBinding(this), null));

			clockItems.Add("Show seconds hand", GetUIElement<CheckBox>("Show seconds hand", ShowSecondsHand, "ShowSecondsHand".GetBinding(this), null));
			clockItems.Add("Smooth seconds hand", GetUIElement<CheckBox>("Smooth seconds hand", SmoothSecondsHand, "SmoothSecondsHand".GetBinding(this), null));
			clockItems.Add("Show hands drop shadow", GetUIElement<CheckBox>("Show hands drop shadow", ShowHandsDropShadow, "ShowHandsDropShadow".GetBinding(this), null));
			clockItems.Add("Use default clock face", GetUIElement<RadioButton>("Use default clock face", string.IsNullOrEmpty(settings.GetSetting<string>("Current", "OtherFaceFileName", string.Empty)), "DefaultFaceSelected".GetBinding(this), null, "ClockFace"));
			clockItems.Add("Use other clock face", GetUIElement<RadioButton>("Use other clock face", !string.IsNullOrEmpty(settings.GetSetting<string>("Current", "OtherFaceFileName", string.Empty)), "OtherFileNameSelected".GetBinding(this), null, "ClockFace"));
			clockItems.Add("Clock face file name", GetUIElement<StackPanel>("Clock face file name", OtherClockFaceFileName, "OtherClockFaceFileName".GetBinding(this), "SelectFileNameVisibility".GetBinding(this), SelectClockFaceCommand));

			sizeItems.Add("Clock", GetUIElement<SliderAndLabel>("Clock", ClockSize, "ClockSize".GetBinding(this), null, new Dictionary<string, object> { { "Minimum", 75.0 }, { "Maximum", Screen.MaxHeight }, { "TickFrequency", Screen.MaxHeight / 100 }, { "IsSnapToTickEnabled", false } }));
			sizeItems.Add("Moon", GetUIElement<SliderAndLabel>("Moon", MoonSize, "MoonSize".GetBinding(this), null, new Dictionary<string, object> { { "Minimum", 16.0 }, { "Maximum", 256.0 }, { "TickFrequency", 10.0 }, { "IsSnapToTickEnabled", false } }));
			sizeItems.Add("Hub", GetUIElement<SliderAndLabel>("Hub", HubSize, "HubSize".GetBinding(this), null, new Dictionary<string, object> { { "Minimum", 2.0 }, { "Maximum", 256.0 }, { "TickFrequency", 10.0 }, { "IsSnapToTickEnabled", false } }));
			sizeItems.Add("Hand thickness", GetUIElement<SliderAndLabel>("Hand thickness", HandThickness, "HandThickness".GetBinding(this), null, new Dictionary<string, object> { { "Minimum", 1.0 }, { "Maximum", 6.0 }, { "TickFrequency", 1.0 }, { "IsSnapToTickEnabled", true } }));
			sizeItems.Add("Hand shorten", GetUIElement<SliderAndLabel>("Hand shorten", HandShortenAmount, "HandShortenAmount".GetBinding(this), null, new Dictionary<string, object> { { "Minimum", 0.0 }, { "Maximum", 100.0 }, { "TickFrequency", 100.0 / 10.0 }, { "IsSnapToTickEnabled", false } }));

			positionItems.Add("Clock top", GetUIElement<SliderAndLabel>("Clock top", LocationTop, "LocationTop".GetBinding(this), null, new Dictionary<string, object> { { "Minimum", 0.0 }, { "Maximum", Screen.MaxHeight }, { "TickFrequency", Screen.MaxHeight / 10 }, { "IsSnapToTickEnabled", false } }));
			positionItems.Add("Clock left", GetUIElement<SliderAndLabel>("Clock left", LocationLeft, "LocationLeft".GetBinding(this), null, new Dictionary<string, object> { { "Minimum", 0.0 }, { "Maximum", Screen.FullWidth }, { "TickFrequency", Screen.FullWidth / 10 }, { "IsSnapToTickEnabled", false } }));
			positionItems.Add("Date offset", GetUIElement<SliderAndLabel>("Date offset", DateOffset, "DateOffset".GetBinding(this), null, new Dictionary<string, object> { { "Minimum", 0.0 }, { "Maximum", ClockSize / 2 }, { "TickFrequency", 10.0 }, { "IsSnapToTickEnabled", false } }));
			positionItems.Add("Moon horizontal offset", GetUIElement<SliderAndLabel>("Moon horizontal offset", MoonHorizontalOffset, "MoonHorizontalOffset".GetBinding(this), null, new Dictionary<string, object> { { "Minimum", 0.0 }, { "Maximum", ClockSize - MoonSize }, { "TickFrequency", (ClockSize - MoonSize) / 10 }, { "IsSnapToTickEnabled", false } }));
			positionItems.Add("Moon vertical offset", GetUIElement<SliderAndLabel>("Moon vertical offset", MoonVerticalOffset, "MoonVerticalOffset".GetBinding(this), null, new Dictionary<string, object> { { "Minimum", 0.0 }, { "Maximum", ClockSize - MoonSize }, { "TickFrequency", (ClockSize - MoonSize) / 10 }, { "IsSnapToTickEnabled", false } }));

			colorItems.Add("Moon opacity", GetUIElement<SliderAndLabel>("Moon opacity", MoonOpacity, "MoonOpacity".GetBinding(this), null, new Dictionary<string, object> { { "Minimum", 0.0 }, { "Maximum", 1.0 }, { "TickFrequency", 0.1 }, { "IsSnapToTickEnabled", false } }));
			colorItems.Add("Hands", GetUIElement<ColorPickerAndLabel>("Hands", HandsColor, "HandsColor".GetBinding(this), null));
			colorItems.Add("Seconds", GetUIElement<ColorPickerAndLabel>("Seconds", SecondsColor, "SecondsColor".GetBinding(this), null));
			colorItems.Add("Hub", GetUIElement<ColorPickerAndLabel>("Hub", HubColor, "HubColor".GetBinding(this), null));
			colorItems.Add("Disk activity", GetUIElement<ColorPickerAndLabel>("Disk activity", ActivityColor, "ActivityColor".GetBinding(this), null));
			colorItems.Add("Date", GetUIElement<ColorPickerAndLabel>("Date", DateColor, "DateColor".GetBinding(this), null));

			FontAndLabel fnl = GetUIElement<FontAndLabel>("Date", null, null, null, new Dictionary<string, object> { { "Tag", "DateFont" }, { "Size", SelectedDateFontSize }, { "Family", SelectedDateFontFamily }, { "Weight", SelectedDateFontWeight } });
			var sizeBinding = "SelectedDateFontSize".GetBinding(this);
			var familyBinding = "SelectedDateFontFamily".GetBinding(this);
			var weightBinding = "SelectedDateFontWeight".GetBinding(this);
			BindingOperations.SetBinding(fnl, FontAndLabel.SelectedFontSizeProperty, sizeBinding);
			BindingOperations.SetBinding(fnl, FontAndLabel.SelectedFontFamilyProperty, familyBinding);
			BindingOperations.SetBinding(fnl, FontAndLabel.SelectedFontWeightProperty, weightBinding);
			fnl.SizeSelectionChanged += fnl_SizeSelectionChanged;
			fnl.FamilySelectionChanged += fnl_FamilySelectionChanged;
			fnl.WeightSelectionChanged += fnl_WeightSelectionChanged;
			fontItems.Add("Date", fnl);

			dynamicSettings.Add(generalTreeViewItem, generalItems);
			dynamicSettings.Add(indicatorTreeViewItem, indicatorItems);
			dynamicSettings.Add(clockTreeViewItem, clockItems);
			dynamicSettings.Add(sizeTreeViewItem, sizeItems);
			dynamicSettings.Add(positionTreeViewItem, positionItems);
			dynamicSettings.Add(colorsTreeViewItem, colorItems);
			dynamicSettings.Add(fontsTreeViewItem, fontItems);

			appearanceTreeViewItem.Expanded += appearanceTreeViewItem_Expanded;
			environmentTreeViewItem.Expanded += environmentTreeViewItem_Expanded;
			if (AddTopLevelSetting != null)
			{
				AddTopLevelSetting(this, new AddTopLevelSettingEventArgs(appearanceTreeViewItem));
				AddTopLevelSetting(this, new AddTopLevelSettingEventArgs(environmentTreeViewItem));
			}

			appearanceTreeViewItem.IsSelected = true;
			appearanceTreeViewItem.IsExpanded = true;
		}
		#endregion Public Methods

		#region Private Methods
		private void appearanceTreeViewItem_Expanded(object sender, RoutedEventArgs e)
		{
			environmentTreeViewItem.IsExpanded = false;
			sender.As<TreeViewItem>().IsSelected = true;
		}

		private void Cancel(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("Close", new Dictionary<string, object> { { "result", false } }));
		}

		private void cp_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
		{
			switch (sender.As<ColorPickerAndLabel>().Tag.As<string>())
			{
				case "HandsColor":
					HandsColor = e.NewValue;
					break;

				case "SecondsColor":
					SecondsColor = e.NewValue;
					break;

				case "ActivityColor":
					ActivityColor = e.NewValue;
					break;

				case "DateColor":
					DateColor = e.NewValue;
					break;

				case "HubColor":
					HubColor = e.NewValue;
					break;
			}
		}

		private void environmentTreeViewItem_Expanded(object sender, RoutedEventArgs e)
		{
			appearanceTreeViewItem.IsExpanded = false;
			sender.As<TreeViewItem>().IsSelected = true;
		}

		private void fnl_FamilySelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			SelectedDateFontFamily = (FontFamily)e.AddedItems[0];
		}

		private void fnl_SizeSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			SelectedDateFontSize = (double)e.AddedItems[0];
		}

		private void fnl_WeightSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			SelectedDateFontWeight = (FontWeight)e.AddedItems[0];
		}

		private T GetUIElement<T>(string text, object value, Binding binding, Binding visibilityBinding) where T : FrameworkElement
		{
			return GetUIElement<T>(text, value, binding, visibilityBinding, string.Empty);
		}

		private T GetUIElement<T>(string text, object value, Binding binding, Binding visibilityBinding, object otherData) where T : FrameworkElement
		{
			if (typeof(T) == typeof(CheckBox))
			{
				var cb = new CheckBox
				{
					Content = text,
					IsChecked = (bool)value,
					Margin = new Thickness(0, 2.5, 0, 2.5)
				};
				if (binding != null)
					BindingOperations.SetBinding(cb, CheckBox.IsCheckedProperty, binding);
				if (visibilityBinding != null)
					BindingOperations.SetBinding(cb, CheckBox.VisibilityProperty, visibilityBinding);
				return cb as T;
			}
			else if (typeof(T) == typeof(RadioButton))
			{
				var rb = new RadioButton
				{
					GroupName = (string)otherData,
					Content = text,
					IsChecked = (bool)value,
					Margin = new Thickness(0, 2.5, 0, 2.5)
				};
				if (binding != null)
					BindingOperations.SetBinding(rb, RadioButton.IsCheckedProperty, binding);
				if (visibilityBinding != null)
					BindingOperations.SetBinding(rb, RadioButton.VisibilityProperty, visibilityBinding);
				return rb as T;
			}
			else if (typeof(T) == typeof(StackPanel))
			{
				var tb = new TextBox
				{
					VerticalAlignment = VerticalAlignment.Center,
					MinWidth = 200
				};
				if (binding != null)
					BindingOperations.SetBinding(tb, TextBox.TextProperty, binding);
				var lb = new Label
				{
					Content = text,
					VerticalAlignment = VerticalAlignment.Center
				};
				var bt = new Button
				{
					Content = "...",
					Width = 30,
					Margin = new Thickness(3, 0, 0, 0),
					VerticalAlignment = VerticalAlignment.Center,
					Command = otherData.As<DelegateCommand>()
				};
				var sp = new StackPanel
				{
					Orientation = Orientation.Horizontal
				};
				if (visibilityBinding != null)
					BindingOperations.SetBinding(sp, StackPanel.VisibilityProperty, visibilityBinding);
				sp.Children.Add(lb);
				sp.Children.Add(tb);
				sp.Children.Add(bt);
				return sp as T;
			}
			else if (typeof(T) == typeof(SliderAndLabel))
			{
				var settings = otherData as Dictionary<string, object>;
				var sl = new SliderAndLabel
				{
					SliderMinimum = (double)settings["Minimum"],
					SliderMaximum = (double)settings["Maximum"],
					SliderTickFrequency = (double)settings["TickFrequency"],
					SliderTickPlacement = TickPlacement.BottomRight,
					SliderIsSnapToTickEnabled = (bool)settings["IsSnapToTickEnabled"],
					LabelText = text
				};
				if (binding != null)
					BindingOperations.SetBinding(sl, SliderAndLabel.SliderValueProperty, binding);
				return sl as T;
			}
			else if (typeof(T) == typeof(ColorPickerAndLabel))
			{
				var cp = new ColorPickerAndLabel
				{
					LabelText = text,
					Tag = binding.Path.Path
				};
				cp.SelectedColorChanged += cp_SelectedColorChanged;
				if (binding != null)
					BindingOperations.SetBinding(cp, ColorPickerAndLabel.SelectedColorProperty, binding);
				return cp as T;
			}
			else if (typeof(T) == typeof(FontAndLabel))
			{
				var settings = otherData as Dictionary<string, object>;
				var ft = new FontAndLabel
				{
					LabelText = text,
					Tag = (string)settings["Tag"]
				};
				return ft as T;
			}
			else if (typeof(T) == typeof(TimeZoneAndLabel))
			{
				var tz = new TimeZoneAndLabel
				{
					LabelText = text,
					Tag = binding.Path.Path
				};
				tz.TimeZoneSelectionChanged += tz_TimeZoneSelectionChanged;
				if (binding != null)
					BindingOperations.SetBinding(tz, TimeZoneAndLabel.SelectedTimeZoneProperty, binding);
				return tz as T;
			}
			return default(T);
		}

		private void SelectClockFace(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("SelectClockFace", null));
		}

		private void SetDefaults(AppSettings settings)
		{
			IsInitializing = true;
			LastClockFaceDirectory = settings.GetSetting<string>("Current", "LastClockFaceDir", Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
			OtherClockFaceFileName = settings.GetSetting<string>("Current", "OtherFaceFileName", string.Empty);
			DefaultFaceSelected = string.IsNullOrEmpty(OtherClockFaceFileName);
			OtherFileNameSelected = !string.IsNullOrEmpty(OtherClockFaceFileName);
			HubIsDiskActivityIndicator = settings.GetSetting<bool>("Current", "HubIsDiskActivityIndicator", true);
			AlwaysOnTop = settings.GetSetting<bool>("Current", "AlwaysOnTop", false);
			SmoothSecondsHand = settings.GetSetting<bool>("Current", "SmoothSecondsHand", false);
			ShowMoon = settings.GetSetting<bool>("Current", "ShowMoon", true);
			ShowSecondsHand = settings.GetSetting<bool>("Current", "ShowSecondsHand", true);
			ShowDate = settings.GetSetting<bool>("Current", "ShowDate", true);
			ShowHandsDropShadow = settings.GetSetting<bool>("Current", "ShowHandsDropShadow", true);
			ClockSize = settings.GetSetting<double>("Current", "ClockSize", 200);
			MoonSize = settings.GetSetting<double>("Current", "MoonSize", 30);
			HubSize = settings.GetSetting<double>("Current", "HubSize", 30);
			HandThickness = settings.GetSetting<double>("Current", "HandsThickness", 1.0);
			HandShortenAmount = settings.GetSetting<double>("Current", "HandShortenAmount", 0);
			LocationTop = settings.GetSetting<double>("Current", "LocationTop", 200);
			LocationLeft = settings.GetSetting<double>("Current", "LocationLeft", 200);
			DateOffset = settings.GetSetting<double>("Current", "DateOffset", 50);
			MoonHorizontalOffset = settings.GetSetting<double>("Current", "MoonHorizontalOffset", 0);
			MoonVerticalOffset = settings.GetSetting<double>("Current", "MoonVerticalOffset", 0);
			MoonOpacity = settings.GetSetting<double>("Current", "MoonOpacity", 1.0);

			SelectedDateFontSize = settings.GetSetting<double>("Current", "SelectedDateFontSize", 12);
			SelectedDateFontWeight = (settings.GetSetting<string>("Current", "SelectedDateFontWeight", "Normal") == "Normal" || settings.GetSetting<string>("Current", "SelectedDateFontWeight", "Normal") == "Regular") ? System.Windows.FontWeights.Normal : System.Windows.FontWeights.Bold;
			SelectedDateFontFamily = new FontFamily(settings.GetSetting<string>("Current", "SelectedDateFontFamily", "Segoe UI"));

			var handsBrushKey = settings.GetSetting<string>("Current", "HandsBrush", "#FF000000");
			var secondsBrushKey = settings.GetSetting<string>("Current", "SecondsBrush", "#FF000000");
			var dateBrushKey = settings.GetSetting<string>("Current", "DateBrush", "#FF000000");
			var hubBrushKey = settings.GetSetting<string>("Current", "HubBrush", "#FF000000");
			var activityBrushKey = settings.GetSetting<string>("Current", "ActivityBrush", "#FFFF000");

			HandsColor = handsBrushKey.ToColor();
			SecondsColor = secondsBrushKey.ToColor();
			DateColor = dateBrushKey.ToColor();
			HubColor = hubBrushKey.ToColor();
			ActivityColor = activityBrushKey.ToColor();

			SelectedTimeZone = TimeZoneInfo.Local;

			IsInitializing = false;
		}

		private void tz_TimeZoneSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			SelectedTimeZone = sender.As<TimeZoneAndLabel>().SelectedTimeZone;
		}

		private bool ValidateCancelState(object state)
		{
			return true;
		}

		private bool ValidateSelectClockFaceState(object state)
		{
			return true;
		}
		#endregion Private Methods

		#region Public Events
		public event AddControlToEditorHandler AddControlToEditor;

		public event AddTopLevelSettingHandler AddTopLevelSetting;

		public event ExecuteUIActionHandler ExecuteUIAction;

		public override event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Public Fields
		public bool IsInitializing = true;
		#endregion Public Fields

		#region Private Fields
		private Brush _ActivityBrush;
		private Color? _ActivityColor;
		private bool _AlwaysOnTop;
		private DelegateCommand _CancelCommand = null;
		private double _ClockSize;
		private Brush _DateBrush;
		private Color? _DateColor;
		private double _DateOffset;
		private bool _DefaultFaceSelected;
		private Brush _HandsBrush;
		private Color? _HandsColor;
		private double _HandShortenAmount;
		private double _HandThickness;
		private Brush _HubBrush;
		private Color? _HubColor;
		private bool _HubIsDiskActivityIndicator;
		private double _HubSize;
		private string _LastClockFaceDirectory;
		private double _LocationLeft;
		private double _LocationTop;
		private double _MoonHorizontalOffset;
		private double _MoonOpacity;
		private double _MoonSize;
		private double _MoonVerticalOffset;
		private string _OtherClockFileFileName;
		private bool _OtherFileNameSelected;
		private Brush _SecondsBrush;
		private Color? _SecondsColor;
		private DelegateCommand _SelectClockFaceCommand = null;
		private FontFamily _SelectedDateFontFamily;
		private double _SelectedDateFontSize;
		private FontWeight _SelectedDateFontWeight;
		private TimeZoneInfo _SelectedTimeZone;
		private TimeZoneInfo _SelectedTimeZone2;
		private TimeZoneInfo _SelectedTimeZone3;
		private TimeZoneInfo _SelectedTimeZone4;
		private TreeViewItem _SelectedTreeViewItem;
		private Visibility _SelectFileNameVisibility;
		private bool _ShowDate;
		private bool _ShowHandsDropShadow;
		private bool _ShowMoon;
		private bool _ShowSecondsHand;
		private bool _SmoothSecondsHand;
		private bool _StartWithWindows;
		private TreeViewItem appearanceTreeViewItem = null;
		private TreeViewItem clockTreeViewItem = null;
		private TreeViewItem colorsTreeViewItem = null;
		private Dictionary<TreeViewItem, Dictionary<string, FrameworkElement>> dynamicSettings = null;
		private TreeViewItem environmentTreeViewItem = null;
		private TreeViewItem fontsTreeViewItem = null;
		private TreeViewItem generalTreeViewItem = null;
		private TreeViewItem indicatorTreeViewItem = null;
		private TreeViewItem positionTreeViewItem = null;
		private TreeViewItem sizeTreeViewItem = null;
		#endregion Private Fields

		#region Public Properties
		public Brush ActivityBrush
		{
			get
			{
				return _ActivityBrush;
			}
			set
			{
				_ActivityBrush = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ActivityBrush"));
			}
		}

		public Color? ActivityColor
		{
			get
			{
				return _ActivityColor;
			}
			set
			{
				_ActivityColor = value;
				if (value.HasValue)
					ActivityBrush = new SolidColorBrush(value.Value);
				else
					ActivityBrush = null;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ActivityColor"));
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
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AlwaysOnTop"));
			}
		}

		public DelegateCommand CancelCommand
		{
			get
			{
				if (_CancelCommand == null)
					_CancelCommand = new DelegateCommand(Cancel, ValidateCancelState);
				return _CancelCommand as DelegateCommand;
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
				if (sizeTreeViewItem != null)
					dynamicSettings[sizeTreeViewItem]["Hand shorten"].As<SliderAndLabel>().SliderMaximum = ClockSize / 2;
				if (positionTreeViewItem != null)
				{
					dynamicSettings[positionTreeViewItem]["Date offset"].As<SliderAndLabel>().SliderMaximum = ClockSize / 2;
					dynamicSettings[positionTreeViewItem]["Moon horizontal offset"].As<SliderAndLabel>().SliderMaximum = ClockSize - MoonSize;
					dynamicSettings[positionTreeViewItem]["Moon vertical offset"].As<SliderAndLabel>().SliderMaximum = ClockSize - MoonSize;
				}
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
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("DateBrush"));
			}
		}

		public Color? DateColor
		{
			get
			{
				return _DateColor;
			}
			set
			{
				_DateColor = value;
				if (value.HasValue)
					DateBrush = new SolidColorBrush(value.Value);
				else
					DateBrush = null;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("DateColor"));
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
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("DateOffset"));
			}
		}

		public bool DefaultFaceSelected
		{
			get
			{
				return _DefaultFaceSelected;
			}
			set
			{
				_DefaultFaceSelected = value;
				SelectFileNameVisibility = value ? Visibility.Collapsed : Visibility.Visible;
				if (!IsInitializing)
					OtherClockFaceFileName = string.Empty;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("DefaultFaceSelected"));
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
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("HandsBrush"));
			}
		}

		public Color? HandsColor
		{
			get
			{
				return _HandsColor;
			}
			set
			{
				_HandsColor = value;
				if (value.HasValue)
					HandsBrush = new SolidColorBrush(value.Value);
				else
					HandsBrush = null;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("HandsColor"));
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
				_HandThickness = value;
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
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("HubBrush"));
			}
		}

		public Color? HubColor
		{
			get
			{
				return _HubColor;
			}
			set
			{
				_HubColor = value;
				if (value.HasValue)
					HubBrush = new SolidColorBrush(value.Value);
				else
					HubBrush = null;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("HubColor"));
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
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("HubSize"));
			}
		}

		public string LastClockFaceDirectory
		{
			get
			{
				return _LastClockFaceDirectory;
			}
			set
			{
				_LastClockFaceDirectory = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("LastClockFaceDirectory"));
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
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("MoonHorizontalOffset"));
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
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("MoonVerticalOffset"));
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
				_OtherClockFileFileName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("OtherClockFaceFileName"));
			}
		}

		public bool OtherFileNameSelected
		{
			get
			{
				return _OtherFileNameSelected;
			}
			set
			{
				_OtherFileNameSelected = value;
				var settings = new AppSettings(SettingsLocations.AppData, "OSoft");

				SelectFileNameVisibility = value ? Visibility.Visible : Visibility.Collapsed;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("OtherFileNameSelected"));
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
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SecondsBrush"));
			}
		}

		public Color? SecondsColor
		{
			get
			{
				return _SecondsColor;
			}
			set
			{
				_SecondsColor = value;
				if (value.HasValue)
					SecondsBrush = new SolidColorBrush(value.Value);
				else
					SecondsBrush = null;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SecondsColor"));
			}
		}

		public DelegateCommand SelectClockFaceCommand
		{
			get
			{
				if (_SelectClockFaceCommand == null)
					_SelectClockFaceCommand = new DelegateCommand(SelectClockFace, ValidateSelectClockFaceState);
				return _SelectClockFaceCommand as DelegateCommand;
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
		public TreeViewItem SelectedTreeViewItem
		{
			get
			{
				return _SelectedTreeViewItem;
			}
			set
			{
				_SelectedTreeViewItem = value;
				if (_SelectedTreeViewItem.Parent.GetType() == typeof(TreeView) && _SelectedTreeViewItem.Items.Count > 0)
					_SelectedTreeViewItem = _SelectedTreeViewItem.Items[0].As<TreeViewItem>();
				if (AddControlToEditor != null)
				{
					Dictionary<string, FrameworkElement> items = null;
					if (dynamicSettings.TryGetValue(_SelectedTreeViewItem, out items))
					{
						foreach (var item in items)
						{
							AddControlToEditor(this, new AddControlToEditorEventArgs(item.Value));
						}
					}
				}
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedTreeViewItem"));
			}
		}

		public Visibility SelectFileNameVisibility
		{
			get
			{
				return _SelectFileNameVisibility;
			}
			set
			{
				_SelectFileNameVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectFileNameVisibility"));
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
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ShowSecondsHand"));
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
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("StartWithWindows"));
			}
		}
		#endregion Public Properties
	}
}
