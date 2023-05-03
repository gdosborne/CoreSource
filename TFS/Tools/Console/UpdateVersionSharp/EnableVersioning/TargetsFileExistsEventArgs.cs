namespace EnableVersioning {
	using System;

	public delegate void TargetsFileExistsHandler(object sender, TargetsFileExistsEventArgs e);

	public class TargetsFileExistsEventArgs : EventArgs {

		public TargetsFileExistsEventArgs(string fileName) => this.FileName = fileName;

		public bool Cancel { get; set; }
		public string FileName { get; private set; } = default;
	}
}