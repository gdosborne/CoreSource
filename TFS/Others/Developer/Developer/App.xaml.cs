namespace SNC.OptiRamp.Application.Developer {

	using SNC.OptiRamp.Application.Developer.Classes.Management;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	internal partial class App : System.Windows.Application {

		#region Internal Methods
		public static SplashWindow SplashWindow = null;
		internal static void OpenSplashScreen() {
			SplashWindow = new SplashWindow();
			SplashWindow.Show();
		}
		#endregion Internal Methods

		#region Public Fields
		public static Composition ApplicationExtensions = null;
		#endregion Public Fields
	}
}
