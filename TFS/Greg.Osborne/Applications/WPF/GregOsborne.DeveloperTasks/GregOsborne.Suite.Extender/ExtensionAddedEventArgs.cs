namespace GregOsborne.Suite.Extender {
	using System;

	public delegate void ExtensionAddedHandler(object sender, ExtensionAddedEventArgs e);

	public class ExtensionAddedEventArgs : EventArgs {
		public ExtensionAddedEventArgs(IExtender extension) {
			this.Extension = extension;
		}

		public IExtender Extension { get; private set; } = default;

	}
}
