namespace GregOsborne.Dialogs {
	using System;

	public class HyperlinkClickedEventArgs : EventArgs {
		private string href;
		public HyperlinkClickedEventArgs(string href) => this.href = href;
		public string Href => this.href;
	}
}
