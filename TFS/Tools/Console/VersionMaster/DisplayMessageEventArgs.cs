namespace VersionMaster {
	using System;

	public delegate void DIsplayMessageHandler(object sender, DisplayMessageEventArgs e);

	public class DisplayMessageEventArgs : EventArgs {
		public DisplayMessageEventArgs(string message) => Message = message;
		public string Message {
			get; private set;
		}
	}
}
