using System.Runtime.CompilerServices;

namespace Common.AppFramework.Reflection {
    public static class Extensions {
        public static string GetPropertyName([CallerMemberName] string caller = null) => caller;
    }
}
