using System.Runtime.CompilerServices;

namespace GregOsborne.Application.Primitives {
	public static class Reflection {
#if !DOTNET3_5
		public static string GetPropertyName([CallerMemberName]string memberName = "") => memberName;
#endif
	}
}
