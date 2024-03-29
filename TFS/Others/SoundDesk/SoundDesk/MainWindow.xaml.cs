namespace SoundDesk
{
	using GregOsborne.Application.Primitives;
	using GregOsborne.Dialog;
	using MVVMFramework;
	using SoundDesk.Controls;
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Threading;
	using System.Xml.Linq;

	public partial class MainWindow : Window
	{
		#region Public Constructors
		public MainWindow()
		{
			InitializeComponent();
			View.Initialize(this);
		}
		#endregion Public Constructors

		#region Public Delegates
		public delegate void UpdateValueHandler(double value, double maximum);
		#endregion Public Delegates

		#region Public Methods
		public void InstallNewApplicationVersion(bool shutdown = false)
		{
			long size = 0;
			using (var client = App.GetClient())
			{
				size = client.GetUpdateSize(App.ApplicationName);
			}
			pDialog = new ProgressDialogBox
			{
				Maximum = size,
				Minimum = 0,
				Title = "Download in progress...",
				Width = 400,
				Height = 85,
				Topmost = true,
				Owner = this,
				WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
			};
			pDialog.View.Prompt = string.Format("Downloading update for {0}...", App.ApplicationName);
			pDialog.Show();
			var dh = new DownloadHandler();
			dh.DownloadCompleted += dh_DownloadCompleted;
			dh.DownloadProgress += dh_DownloadProgress;
			forceShutDown = shutdown;
			downloadTask = Task.Run(() => dh.Start());
		}
		private bool forceShutDown = false;
		void dh_DownloadProgress(object sender, DownloadProgressEventArgs e)
		{
			if (Dispatcher.CheckAccess())
				pDialog.Value = e.Value;
			else
				Dispatcher.BeginInvoke(new SoundDesk.DownloadHandler.DownloadProgressHandler(dh_DownloadProgress), new object[] { sender, e });
		}

		void dh_DownloadCompleted(object sender, EventArgs e)
		{
			if (Dispatcher.CheckAccess())
			{
				pDialog.Close();
				if (!string.IsNullOrEmpty(sender.As<DownloadHandler>().ExePath))
				{
					new Process
					{
						StartInfo = new ProcessStartInfo
						{
							FileName = sender.As<DownloadHandler>().ExePath
						}
					}.Start();
				}
				if (forceShutDown)
					Application.Current.Shutdown(0);
			}
			else
				Dispatcher.BeginInvoke(new EventHandler(dh_DownloadCompleted), new object[] { sender, e });
		}
		#endregion Public Methods

		#region Private Methods
		
		private void Image_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (View.IsPlayingRandomSongs)
				return;
			View.SettingsVisibility = View.SettingsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
		}
		private void NumericOnlyTextBox_ValueEntered(object sender, Controls.ValueEnteredEventArgs e)
		{
			var actual = Convert.ToInt32(e.Value);
			if (actual < sender.As<NumericOnlyTextBox>().Minimum || actual > sender.As<NumericOnlyTextBox>().Maximum || (View.Song1.HasValue && View.Song2.HasValue && View.Song3.HasValue))
				return;
			if ((View.Song1.HasValue && View.Song1.Value == actual) || (View.Song2.HasValue && View.Song2.Value == actual) || (View.Song3.HasValue && View.Song3.Value == actual))
				return;
			if (!View.Song1.HasValue)
				View.Song1 = actual;
			else if (!View.Song2.HasValue)
				View.Song2 = actual;
			else
				View.Song3 = actual;
		}
		private void PBar_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var h = sender.As<ProgressBar>().ActualHeight;
			var x = sender.As<ProgressBar>().Maximum;
			var actual = h - e.GetPosition((IInputElement)sender).Y;
			var pct = actual / h;
			View.CurrentFile.Position = TimeSpan.FromSeconds(x * pct);
		}
		private void Slider_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			View.VolumeTickFrequency = sender.As<Slider>().ActualHeight.IsBetween<double>(0, 200, true) ? .1 : .05;
		}
		private void updateTimer_Tick(object sender, EventArgs e)
		{
			sender.As<DispatcherTimer>().Stop();
			if (!View.IsCheckForUpdatesOnStart)
				return;

			//var appLink = @"https://1drv.ms/u/s!ApZoQiNwCKHoi8tjPs0MvFhbcNeoUA";
			//var doc = XDocument.Load(appLink);
			//if (doc == null)
			//	return;
			//using (var client = App.GetClient())
			//{
			//	try
			//	{
			//		if (client.HasUpdate(App.ApplicationName, Assembly.GetExecutingAssembly().GetName().Version))
			//		{
			//			var updateVersion = client.MostRecentVersion(App.ApplicationName);
			//			var td = new TaskDialog
			//			{
			//				Width = 400,
			//				Title = "Application Update Available",
			//				MessageText = string.Format("Version {0} of the Sound Desk application is available for installation.\n\nWould you like to install this new version?", updateVersion),
			//				Image = ImagesTypes.Question,
			//				AdditionalInformation = string.Format("Your version is {0}", Assembly.GetExecutingAssembly().GetName().Version),
			//				IsAdditionalInformationExpanded = false
			//			};
			//			td.AddButtons(ButtonTypes.Yes, ButtonTypes.No);
			//			var result = td.ShowDialog(this);
			//			if ((ButtonTypes)result == ButtonTypes.Yes)
			//			{
			//				td = new TaskDialog
			//				{
			//					Width = 400,
			//					Title = "Install New Version",
			//					MessageText = "To ensure you have a working version for this meeting, this update can be installed when the application exits.\n\nWhen would you like to install the update?",
			//					Image = ImagesTypes.Question
			//				};
			//				td.AddButton("Now", 99);
			//				td.AddButton("On Exit", 98);
			//				td.AddButton(ButtonTypes.Cancel);
			//				result = td.ShowDialog(this);
			//				if ((ButtonTypes)result == ButtonTypes.Cancel)
			//					return;
			//				else if (result == 99)
			//				{
			//					//InstallNewApplicationVersion(true);
			//					return;
			//				}
			//				else
			//					App.InstallNewVersionOnExit = true;
			//			}
			//		}
			//	}
			//	catch (Exception ex)
			//	{
			//		App.DisplayErrorDialog("No endpoint listening at \"{0}\".", client.Endpoint.Address.ToString());
			//	}
			//}
		}
		
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			View.Persist(this);
			if (App.InstallNewVersionOnExit)
			{
				e.Cancel = true;
				App.InstallNewVersionOnExit = false;
				//InstallNewApplicationVersion(true);
			}
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			View.InitView();
			updateTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
			updateTimer.Tick += updateTimer_Tick;
			updateTimer.Start();
		}
		#endregion Private Methods

		#region Private Fields
		private Task downloadTask = null;
		private ProgressDialogBox pDialog = null;
		private DispatcherTimer updateTimer = null;
		#endregion Private Fields

		#region Public Properties
		public MainWindowView View { get { return LayoutRoot.GetView<MainWindowView>(); } }
		#endregion Public Properties
	}
}
