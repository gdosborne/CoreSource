namespace GregOsborne.Application {
	using System.Runtime.CompilerServices;

	public static class Reflection {
		public static string GetPropertyName([CallerMemberName] string caller = null) => caller;
	}
}
