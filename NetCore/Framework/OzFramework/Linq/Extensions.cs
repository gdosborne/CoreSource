/* File="Extensions"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using OzFramework.Primitives;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OzFramework.Linq {
    public static class Extensions {
        public static IList<T> Randomize<T>(this IList<T> value, System.Random r = null) {
            r ??= new System.Random();
            var count = value.Count();
            for (var i = 0; i < count; i++) {
                var next = r.Next(0, count);
                if (i != next) {
                    value.Swap(i, next);
                }
            }
            return value;
        }

        public static bool Contains(this IList<string> list, string value, StringComparison comparison) {
            foreach (var item in list) {
                if (item.Equals(value, comparison))
                    return true;
            }
            return false;
        }

        public static int IndexOf(this IList<string> list, string value, StringComparison comparison) {
            for (int i = 0; i < list.Count; i++) {
                if (list[i].Equals(value, comparison))
                    return i;
            }
            return -1;
        }

        public static void Swap<T>(this IList<T> list, int index1, int index2) {
            var temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }

        public static IList<string> GetEnumItems(this Type enumType) {
            if (!enumType.IsEnum) {
                throw new ArgumentException("Type must be an enum trype");
            }
            return new List<string>(Enum.GetNames(enumType));
        }

        public static void AddRange<T>(this ObservableCollection<T> original, IEnumerable<T> additional) {
            if (original.IsNull() || additional.IsNull() || !additional.Any()) return;
            additional.ToList().ForEach(x => original.Add(x));
        }

        public enum Positions {
            Before, After
        }

        //public static IEnumerable<TSource> DistinctBy<TSource, TKey> (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) {
        //    HashSet<TKey> seenKeys = new HashSet<TKey>();
        //    foreach (TSource element in source) {
        //        if (seenKeys.Add(keySelector(element))) {
        //            yield return element;
        //        }
        //    }
        //}

        public static double MaxOf(params double[] p) => p.Max();
    }
}
