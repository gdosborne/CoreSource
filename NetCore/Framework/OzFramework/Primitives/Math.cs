/* File="Math"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System.Linq;

namespace Common.Primitives {
    public static class Math {
        public static T MaxOf<T>(T[] values) => values.ToList().Max();
        public static T MaxOf<T>(T value1, T value2) => MaxOf(new[] { value1, value2 });
        public static T MinOf<T>(T[] values) => values.ToList().Min();
        public static T MinOf<T>(T value1, T value2) => MinOf(new[] { value1, value2 });
    }
}
