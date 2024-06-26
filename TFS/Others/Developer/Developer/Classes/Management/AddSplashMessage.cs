namespace SNC.OptiRamp.Application.Developer.Classes.Management {
	using System;
	using System.Windows.Controls;
	using System.Linq;

	public delegate void AddSplashMessageHandler(object sender, AddSplashMessageEventArgs e);
	public class AddSplashMessageEventArgs : EventArgs {
		public AddSplashMessageEventArgs(string message) {
			Message = message;
		}
		public string Message {
			get;
			private set;
		}
	}
}
