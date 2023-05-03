namespace EnableVersioning {
	using System;

	public delegate void UpdateLocationHandler(object sender, UpdateLocationChangedEventArgs e);

	public class UpdateLocationChangedEventArgs : EventArgs {

		public UpdateLocationChangedEventArgs(bool value) => this.Value = value;

		public bool Value { get; private set; } = default;
	}
}