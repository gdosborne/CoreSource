namespace GregOsborne.Suite.Extender {
	using System;

	public static class Enumerations {
		[Flags]
		public enum MessageOptions {
			None = 0,

			//message types
			Informational = 1,
			Warning = 2,
			ErrorOccurred = 4,

			//message priorities
			LowPriority = 128,
			MediumPriority = 256,
			HighPriority = 512,

			//options
			ResponseRequired = 8192
		}
	}
}
