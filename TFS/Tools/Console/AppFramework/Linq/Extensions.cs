using System.Collections.ObjectModel;

namespace GregOsborne.Application.Linq {

    public static class Extensions {
        public static IList<T> Randomize<T>(this IList<T> value, Random r = null) {
            r = r ?? new Random();
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

        public static void AddRange<T>(this IList<T> list, IList<T> newValues) =>
            newValues.ToList().ForEach(x => list.Add(x));

        public static ObservableCollection<T> ReplaceWith<T>(this ObservableCollection<T> original, IEnumerable<T> newList) =>
            new(newList);


    }
}