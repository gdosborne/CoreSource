using System;
using System.Collections.Generic;
using System.Windows;
using MyApplication.Windows;
using OptiRampDesktop.Helpers;

namespace OptiRampDesktop
{
	public partial class App : System.Windows.Application
	{
		#region Public Constructors

		public App()
		{
			this.Startup += App_Startup;
			this.Exit += App_Exit;
			this.DispatcherUnhandledException += App_DispatcherUnhandledException;
		}

		#endregion

		#region Private Methods

		private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			if (ApplicationSettings.ApplicationMode == ApplicationModes.Kiosk)
				SafeNativeMethods.Show();
		}

		private void App_Exit(object sender, ExitEventArgs e)
		{
			if (ApplicationSettings.ApplicationMode == ApplicationModes.Kiosk)
				SafeNativeMethods.Show();
		}

		private void App_Startup(object sender, StartupEventArgs e)
		{
		}

		#endregion
	}
}