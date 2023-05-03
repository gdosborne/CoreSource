namespace MVVMFramework {
	using System;

	public delegate void CheckAccessEventHandler(object sender, CheckAccessEventArgs e);
	public class CheckAccessEventArgs : EventArgs {

		public Windows.UI.Core.CoreDispatcher Dispatcher { get; set; }

		public bool HasAccess { get; set; }
	}
}
