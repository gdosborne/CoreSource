namespace GregOsborne.Application.Generation {
	using System;

	public delegate void EnumerationStartGenerationHandler(object sender, EnumerationStartGenerationEventArgs e);
	public class EnumerationStartGenerationEventArgs : EventArgs {
		public EnumerationStartGenerationEventArgs(string enumerationName) => this.EnumerationName = enumerationName;

		public string EnumerationName { get; private set; } = default;
	}
}
