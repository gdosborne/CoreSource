namespace SNC.Applications.Developer
{
	using SNC.Applications.Developer;
	using SNC.Applications.Developer.Properties;
	using SNC.OptiRamp.Services.fDiagnostics;
	using System;
	using System.CodeDom.Compiler;
	using System.Collections.Generic;
	using System.Configuration;
	using System.Data;
	using System.Diagnostics;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;
	using System.Windows.Media.Imaging;
	using System.Windows.Threading;

	public partial class App : System.Windows.Application
	{
		public App() {
			if (Settings.Default.RequiresUpdate) {
				Settings.Default.Upgrade();
				Settings.Default.RequiresUpdate = false;
				Settings.Default.Save();
			}
			TempFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OptiRamp");
			if (!Directory.Exists(TempFolder))
				Directory.CreateDirectory(TempFolder);
			ImagesFolder = Path.Combine(TempFolder, "projectImages");
			if (!Directory.Exists(ImagesFolder))
				Directory.CreateDirectory(ImagesFolder);

			DispatcherUnhandledException += App_DispatcherUnhandledException;
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			Exit += App_Exit;

			if (Log == null) {
				var logDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				if (!Directory.Exists(logDirectory))
					Directory.CreateDirectory(logDirectory);
				Log = new OptiRampLog();
				var header = Log.DefaultHeaderData(logDirectory);
				Log.InitLog(logDirectory, LogName, header, 5);
			}
		}
		public static string TempFolder { get; private set; }
		public static string ImagesFolder { get; private set; }
		public static string CurrentProjectImagesFolder { get; set; }

		protected override void OnStartup(StartupEventArgs e) {
			base.OnStartup(e);
			ShowSplashScreen();
		}
		void App_Exit(object sender, ExitEventArgs e) {
			
		}

		void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
			
		}

		void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) {
			
		}
		private bool _contentLoaded;
		public void InitializeComponent() {
			if (_contentLoaded) {
				return;
			}
			_contentLoaded = true;
			System.Uri resourceLocater = new System.Uri("/ORD;component/app.xaml", System.UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocater);
		}
		[STAThread()]
		//[DebuggerNonUserCode()]
		//[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public static void Main() {
			//ShowSplashScreen();
			SNC.Applications.Developer.App app = new SNC.Applications.Developer.App();
			app.InitializeComponent();
			app.Run();
		}

		private static DispatcherTimer SplashTimer = null;
		private static Window SplashWindow = null;
		private void ShowSplashScreen() {
			var splashUri = "pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + " ;component/ORDSplash.png";
			var img = new Image
			{
				Source = new BitmapImage(new Uri(splashUri))
			};
			var tb = new TextBlock
			{
				Text = string.Format("Version {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString()),
				HorizontalAlignment = HorizontalAlignment.Left,
				VerticalAlignment = VerticalAlignment.Bottom,
				Margin = new Thickness(20)
			};
			MainWindow = new MainWindow();
			SplashWindow = new Window
			{
				WindowStartupLocation = WindowStartupLocation.CenterScreen,
				WindowStyle = WindowStyle.None,
				Width = 516,
				Height = 323,
				AllowsTransparency = true,
				Background = new SolidColorBrush(Colors.Transparent),
				ShowInTaskbar = false,
				ResizeMode = ResizeMode.NoResize
			};
			var g = new Grid();
			g.Children.Add(img);
			g.Children.Add(tb);
			SplashWindow.Content = g;
			SplashWindow.Show();
			SplashTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(Settings.Default.SplashScreenTimeout) };
			SplashTimer.Tick += SplashTimer_Tick;
			SplashTimer.Start();
		}
		public static OptiRampLog Log = null;
		public static readonly string LogName = "OptiRampDeveloper";
		private void SplashTimer_Tick(object sender, EventArgs e) {
			SplashTimer.Stop();
			SplashWindow.Close();
			MainWindow.Show();
		}
	}
}
