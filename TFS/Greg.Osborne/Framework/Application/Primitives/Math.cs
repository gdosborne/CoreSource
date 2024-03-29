using System.Linq;

namespace GregOsborne.Application.Primitives {
    public static class Math {
        public static T MaxOf<T>(T[] values) => values.ToList().Max();
        public static T MaxOf<T>(T value1, T value2) => MaxOf(new[] {value1, value2});
    }
}