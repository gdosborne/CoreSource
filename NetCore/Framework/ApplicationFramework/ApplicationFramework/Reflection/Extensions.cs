using System.Runtime.CompilerServices;

namespace Common.OzApplication.Reflection {
    public static class Extensions {
        public static string GetPropertyName([CallerMemberName] string caller = null) => caller;
    }
}
