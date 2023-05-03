namespace ConsoleHelpers
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    public static class Utilities
    {
        public static Tuple<T1, T2> GetTuple<T1, T2>(T1 value1, T2 value2)
        {
            return new Tuple<T1, T2>(value1, value2);
        }

        public static string DescriptionAttr<T>(this T source)
        {
            var fi = source.GetType().GetField(source.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes != null && attributes.Length > 0
                ? attributes[0].Description
                : source.ToString();
        }

        public static string Replace(this string source, params Tuple<string, string>[] with)
        {
            with.ToList().ForEach(x => source = source.Replace(x.Item1, x.Item2));
            return source;
        }
    }
}
