using System;
using System.Collections.Generic;
using System.Linq;

namespace GregOsborne.Application.Linq {
    public static class Extensions {
        public static IList<T> Randomize<T>(this IList<T> value, Random r = null) {
            if (r == null)
                r = new Random();
            var count = value.Count();
            for (var i = 0; i < count; i++) {
                var next = r.Next(0, count);
                if (i != next)
                    value.Swap(i, next);
            }
            return value;
        }

        public static void Swap<T>(this IList<T> list, int index1, int index2) {
            var temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
    }
}