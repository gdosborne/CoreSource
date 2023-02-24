using Common.Application.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Common.Application.Linq {
    public static class Extensions {
        public static IList<T> Randomize<T>(this IList<T> value, Random r = null) {
            r ??= new Random();
            var count = value.Count();
            for (var i = 0; i < count; i++) {
                var next = r.Next(0, count);
                if (i != next) {
                    value.Swap(i, next);
                }
            }
            return value;
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
        public static void AddRange<T>(this ObservableCollection<T> original, IEnumerable<T> additional) =>
            additional.ToList().ForEach(x => original.Add(x));

        public enum Positions {
            Before, After
        }
        
        /*
        public static ObservableCollection<T1> Insert<T1, T2>(this ObservableCollection<T1> original, T1 newItem, Positions pos, string fieldName, object value) where T2: struct {
            var theType = typeof(T1);
            var theProperty = theType.GetProperty(fieldName);
            var theField = theType.GetField(fieldName);

            if (theProperty == null && theField == null) {
                throw new ArgumentException($"{theType.Name} does not contain the field/property \"{fieldName}\"");
            }

            var isProp = theType.GetProperty(fieldName) != null;
            var item = default(T1);

            if (isProp) {
                if (theProperty.PropertyType != value.GetType()) {
                    throw new ArgumentException($"The type value does not match the type for the property \"{fieldName}\"");
                }
                foreach (var x in original) {
                    if (theProperty.GetValue(x) == value) {
                        item = x;
                    }
                    if (pos == Positions.Before && item != null) {
                        break;
                    }
                }
                //item = original.FirstOrDefault(x => theProperty.GetValue(x) == value);
                if (item == null) {
                    throw new ArgumentException($"Cannot insert the new item in the source because the target value ({value}) " +
                        $"does not exist within the source");
                }
            }
            else {
                if (theField.FieldType != value.GetType()) {
                    throw new ArgumentException($"The type value does not match the type for the field \"{fieldName}\"");
                }
                //item = original.FirstOrDefault(x => theField.GetValue(x) == value);
                //if (item == null) {
                //    throw new ArgumentException($"Cannot insert the new item in the source because the target value ({value}) " +
                //        $"does not exist within the source");
                //}
            }

            var o = original.ToList();
            var index = o.IndexOf(item);
            switch (pos) {
                case Positions.After:
                    o.Insert(index + 1, newItem);
                    break;
                case Positions.Before:
                default:
                    o.Insert(index, newItem);
                    break;
            }
            return new ObservableCollection<T1>(o);
        }
        */

        public static double MaxOf(params double[] p) => p.Max();
    }
}