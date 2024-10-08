namespace My_Ministry
{
	using MyMinistry;
	using MyMinistry.Utilities;
	using System;
	using Windows.ApplicationModel;
	using Windows.ApplicationModel.Activation;
	using Windows.Storage;
	using Windows.UI.ApplicationSettings;
	using Windows.UI.Xaml;
	using Windows.UI.Xaml.Controls;
	using Windows.UI.Xaml.Navigation;

	public sealed partial class App : Application
	{
		public App()
		{
			this.InitializeComponent();
			this.Suspending += OnSuspending;
		}

		protected override void OnWindowCreated(WindowCreatedEventArgs args)
		{
			base.OnWindowCreated(args);
			SettingsPane.GetForCurrentView().CommandsRequested += App_CommandsRequested;
		}

		private void App_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
		{
			args.Request.ApplicationCommands.Add(new SettingsCommand("Custom Settings", "Custom Settings", (handler) => OptionsFlyout()));
		}

		public MyMinistryUser User { get; set; }

		public void OptionsFlyout()
		{
			if (CommonData.Data == null)
				return;
			OptionsFlyout customSettingFlyout = new OptionsFlyout();
			customSettingFlyout.Unloaded += customSettingFlyout_Unloaded;
			customSettingFlyout.View.Data = CommonData.Data;
			customSettingFlyout.Show();
		}

		private void customSettingFlyout_Unloaded(object sender, RoutedEventArgs e)
		{
			//CommonData.IsCompactButtons = !(bool)ApplicationData.Current.RoamingSettings.Values["ShowAppBarLabels"];
		}

		protected override void OnLaunched(LaunchActivatedEventArgs e)
		{
#if DEBUG
			if (System.Diagnostics.Debugger.IsAttached)
			{
				this.DebugSettings.EnableFrameRateCounter = true;
			}
#endif

			Frame rootFrame = Window.Current.Content as Frame;

			// Do not repeat app initialization when the Window already has content,
			// just ensure that the window is active
			if (rootFrame == null)
			{
				// Create a Frame to act as the navigation context and navigate to the first page
				rootFrame = new Frame();
				// Set the default language
				rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

				rootFrame.NavigationFailed += OnNavigationFailed;

				if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
				{
					//TODO: Load state from previously suspended application
				}

				// Place the frame in the current Window
				Window.Current.Content = rootFrame;
			}

			if (rootFrame.Content == null)
			{
				// When the navigation stack isn't restored navigate to the first page,
				// configuring the new page by passing required information as a navigation
				// parameter
				rootFrame.Navigate(typeof(MainPage), e.Arguments);
			}
			// Ensure the current window is active
			Window.Current.Activate();
		}

		private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
		{
			throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
		}

		private void OnSuspending(object sender, SuspendingEventArgs e)
		{
			var deferral = e.SuspendingOperation.GetDeferral();
			//TODO: Save application state and stop any background activity
			deferral.Complete();
		}
	}
}
