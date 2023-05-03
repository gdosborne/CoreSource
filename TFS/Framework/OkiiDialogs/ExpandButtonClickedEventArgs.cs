namespace GregOsborne.Dialogs {
	using System;

	public class ExpandButtonClickedEventArgs : EventArgs {
		private bool expanded;
		public ExpandButtonClickedEventArgs(bool expanded) => this.expanded = expanded;
		public bool Expanded => this.expanded;
	}
}
