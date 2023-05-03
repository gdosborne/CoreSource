using System;
using System.Linq;
using System.Text.RegularExpressions;
using AppSystem.Primitives;

namespace AppSystem.Text {
	public static class GlobalMethods {
		public static string Spaces(int count) => new string(' ', count);
        public static string RemoveNonNumericCharacters(this string value) {
            var result = default(string);
            if (string.IsNullOrEmpty(value))
                return result;
            value.ToCharArray().ToList().ForEach(x => {
                if (char.IsNumber(x))
                    result += x;
            });
            return result;
        }

        public static string FormatAsPhone(this string value) {
            var result = default(string);
            if (string.IsNullOrEmpty(value))
                return result;
            var temp = value.RemoveNonNumericCharacters();
            if (temp.Length != 10)
                return value;
            result = string.Format("{0:(###) ###-####}", temp.CastTo<long>());
            return result;
        }

        public static string ToTimeString(this TimeSpan value) {
            var ap = value.Hours > 11 ? "pm" : "am";
            var hour = value.Hours > 12 ? value.Hours - 12 : value.Hours;
            var min = value.Minutes;
            return $"{hour}:{min.ToString("00")} {ap}";
        }

        public static TimeSpan FromTimeString(this string value) {
            var isAfternoon = value.EndsWith("pm");
            var temp = value.Replace(" am", string.Empty).Replace(" pm", string.Empty);
            var hour = int.Parse(temp.Split(':')[0]);
            hour += isAfternoon ? 12 : 0;
            var min = int.Parse(temp.Split(':')[1]);
            return new TimeSpan(hour, min, 0);
        }
    }
}
