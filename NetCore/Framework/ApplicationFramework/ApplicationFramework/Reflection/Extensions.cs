using System.Runtime.CompilerServices;

namespace Common.Applicationn.Reflection {
    public static class Extensions {
        public static string GetPropertyName([CallerMemberName] string caller = null) => caller;
    }
}
