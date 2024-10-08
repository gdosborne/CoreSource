using System;
using System.Linq;
using System.Windows;
using SoundSettings;
namespace KHSound
{
	public partial class App : Application
	{
		static App()
		{
			MySettings = new PersonalSettings();
		}
		public App()
		{
			Application.Current.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(Current_DispatcherUnhandledException);
		}
		public static PersonalSettings MySettings { get; private set; }
		public static void DisplayExecption(Exception ex, bool isFatal)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			if(isFatal)
				Environment.Exit(9999);
		}
		protected override void OnExit(ExitEventArgs e)
		{
			Logger.LogMessage("Application ending");
			base.OnExit(e);
		}
		protected override void OnStartup(StartupEventArgs e)
		{
			Logger.LogMessage("Application starting");
			base.OnStartup(e);
		}
		private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			Logger.LogException(e.Exception);
			DisplayExecption(e.Exception, true);
		}
	}
}
