namespace DesktopClock
{
	using DesktopClock.Classes;
	using MyApplication.Primitives;
	using OSControls;
	using System;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Data;
	using System.Windows.Input;

	public partial class MainWindow : Window
	{
		#region Public Constructors
		public MainWindow()
		{
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Protected Methods
		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
		}
		#endregion Protected Methods

		#region Private Methods
		private void AnalogClock_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
		}

		private void MainWindowView_ExecuteUIAction(object sender, MVVMFramework.ExecuteUIActionEventArgs e)
		{
			switch (e.CommandToExecute)
			{
				case "Settings":
					var settingsWin = new SettingsWindow();
					settingsWin.PropertyChanged += settingsWin_PropertyChanged;
					settingsWin.AddNewAnalogClock += settingsWin_AddNewAnalogClock;
					var result = settingsWin.ShowDialog();
					break;

				case "Exit":
					App.Current.Shutdown();
					break;
			}
		}

		private void MainWindowView_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "AlwaysOnTop":
					this.Topmost = View.AlwaysOnTop;
					break;
			}
		}
		private void SetBindings(AnalogClock clock)
		{
			BindingOperations.SetBinding(clock, AnalogClock.ClockSizeProperty, "ClockSize".GetBinding(View));
			BindingOperations.SetBinding(clock, AnalogClock.HubSizeProperty, "HubSize".GetBinding(View));
			BindingOperations.SetBinding(clock, AnalogClock.MoonSizeProperty, "MoonSize".GetBinding(View));
			BindingOperations.SetBinding(clock, AnalogClock.HandThicknessProperty, "HandThickness".GetBinding(View));
			BindingOperations.SetBinding(clock, AnalogClock.HandShortenAmountProperty, "HandShortenAmount".GetBinding(View));
			BindingOperations.SetBinding(clock, AnalogClock.MoonMarginProperty, "MoonMargin".GetBinding(View));
			BindingOperations.SetBinding(clock, AnalogClock.DateOffsetProperty, "DateOffset".GetBinding(View));
			BindingOperations.SetBinding(clock, AnalogClock.SecondsVisibilityProperty, "SecondsHandVisibility".GetBinding(View));
			BindingOperations.SetBinding(clock, AnalogClock.SmoothSecondsHandProperty, "SmoothSecondsHand".GetBinding(View));
			BindingOperations.SetBinding(clock, AnalogClock.ShowHandsDropShadowProperty, "ShowHandsDropShadow".GetBinding(View));
			BindingOperations.SetBinding(clock, AnalogClock.MoonPhaseVisibilityProperty, "MoonVisibility".GetBinding(View));
			BindingOperations.SetBinding(clock, AnalogClock.DateVisibilityProperty, "DateVisibility".GetBinding(View));
			BindingOperations.SetBinding(clock, AnalogClock.MoonOpacityProperty, "MoonOpacity".GetBinding(View));
			BindingOperations.SetBinding(clock, AnalogClock.HandsBrushProperty, "HandsBrush".GetBinding(View));
			BindingOperations.SetBinding(clock, AnalogClock.SecondsBrushProperty, "SecondsBrush".GetBinding(View));
			BindingOperations.SetBinding(clock, AnalogClock.HubBrushProperty, "HubBrush".GetBinding(View));
			BindingOperations.SetBinding(clock, AnalogClock.ForegroundProperty, "DateBrush".GetBinding(View));
			BindingOperations.SetBinding(clock, AnalogClock.FontFamilyProperty, "SelectedDateFontFamily".GetBinding(View));
			BindingOperations.SetBinding(clock, AnalogClock.FontSizeProperty, "SelectedDateFontSize".GetBinding(View));
			BindingOperations.SetBinding(clock, AnalogClock.FontWeightProperty, "SelectedDateFontWeight".GetBinding(View));
		}
		private void settingsWin_AddNewAnalogClock(object sender, Classes.AddNewAnalogClockEventArgs e)
		{
			SetBindings(e.Clock);
			e.Clock.ClockFaceSource = MyClock.ClockFaceSource;
			e.Clock.DropShadowColor = MyClock.DropShadowColor;
			LayoutRoot.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			e.Clock.SetValue(Grid.RowProperty, LayoutRoot.RowDefinitions.Count - 1);
			LayoutRoot.Children.Add(e.Clock);
		}
		private void settingsWin_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "ClockSize":
					View.ClockSize = sender.As<SettingsWindow>().View.ClockSize;
					break;

				case "HubSize":
					View.HubSize = sender.As<SettingsWindow>().View.HubSize;
					break;

				case "MoonSize":
					View.MoonSize = sender.As<SettingsWindow>().View.MoonSize;
					break;

				case "HandThickness":
					View.HandThickness = sender.As<SettingsWindow>().View.HandThickness;
					break;

				case "HandShortenAmount":
					View.HandShortenAmount = sender.As<SettingsWindow>().View.HandShortenAmount;
					break;

				case "MoonHorizontalOffset":
					View.MoonHorizontalOffset = sender.As<SettingsWindow>().View.MoonHorizontalOffset;
					break;

				case "MoonVerticalOffset":
					View.MoonVerticalOffset = sender.As<SettingsWindow>().View.MoonVerticalOffset;
					break;

				case "DateOffset":
					View.DateOffset = sender.As<SettingsWindow>().View.DateOffset;
					break;

				case "LocationLeft":
					this.Left = sender.As<SettingsWindow>().View.LocationLeft;
					break;

				case "LocationTop":
					this.Top = sender.As<SettingsWindow>().View.LocationTop;
					break;

				case "ShowSecondsHand":
					View.ShowSecondsHand = sender.As<SettingsWindow>().View.ShowSecondsHand;
					break;

				case "SmoothSecondsHand":
					View.SmoothSecondsHand = sender.As<SettingsWindow>().View.SmoothSecondsHand;
					break;

				case "ShowHandsDropShadow":
					View.ShowHandsDropShadow = sender.As<SettingsWindow>().View.ShowHandsDropShadow;
					break;

				case "OtherClockFaceFileName":
					if (!sender.As<SettingsWindow>().View.IsInitializing)
						View.OtherClockFaceFileName = sender.As<SettingsWindow>().View.OtherClockFaceFileName;
					break;

				case "HubIsDiskActivityIndicator":
					View.HubIsDiskActivityIndicator = sender.As<SettingsWindow>().View.HubIsDiskActivityIndicator;
					break;

				case "ShowMoon":
					View.ShowMoon = sender.As<SettingsWindow>().View.ShowMoon;
					break;

				case "ShowDate":
					View.ShowDate = sender.As<SettingsWindow>().View.ShowDate;
					break;

				case "AlwaysOnTop":
					View.AlwaysOnTop = sender.As<SettingsWindow>().View.AlwaysOnTop;
					break;

				case "MoonOpacity":
					View.MoonOpacity = sender.As<SettingsWindow>().View.MoonOpacity;
					break;

				case "HandsBrush":
					View.HandsBrush = sender.As<SettingsWindow>().View.HandsBrush;
					break;

				case "SecondsBrush":
					View.SecondsBrush = sender.As<SettingsWindow>().View.SecondsBrush;
					break;

				case "HubBrush":
					View.HubBrush = sender.As<SettingsWindow>().View.HubBrush;
					break;

				case "ActivityBrush":
					View.ActivityBrush = sender.As<SettingsWindow>().View.ActivityBrush;
					break;

				case "DateBrush":
					View.DateBrush = sender.As<SettingsWindow>().View.DateBrush;
					break;

				case "SelectedDateFontFamily":
					View.SelectedDateFontFamily = sender.As<SettingsWindow>().View.SelectedDateFontFamily;
					break;

				case "SelectedDateFontSize":
					View.SelectedDateFontSize = sender.As<SettingsWindow>().View.SelectedDateFontSize;
					break;

				case "SelectedDateFontWeight":
					View.SelectedDateFontWeight = sender.As<SettingsWindow>().View.SelectedDateFontWeight;
					break;

				case "SelectedTimeZone":
					View.SelectedTimeZone = sender.As<SettingsWindow>().View.SelectedTimeZone;
					break;
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Left = View.LocationLeft;
			Top = View.LocationTop;
			View.IsInitialLocationSet = true;
			this.Topmost = View.AlwaysOnTop;
		}

		private void Window_LocationChanged(object sender, EventArgs e)
		{
			if (!View.IsInitialLocationSet)
				return;
			View.LocationTop = this.Top;
			View.LocationLeft = this.Left;
		}

		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
				this.DragMove();
		}

		private void Window_Unloaded(object sender, RoutedEventArgs e)
		{
			View.Activity.Dispose();
		}
		#endregion Private Methods

		#region Public Properties
		public MainWindowView View
		{
			get
			{
				if (DesignerProperties.GetIsInDesignMode(this))
					return new MainWindowView();
				var result = LayoutRoot.DataContext as MainWindowView;
				result.Initialize(this);
				return result;
			}
		}
		#endregion Public Properties
	}
}
