﻿using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GregOsborne.Application.Text
{
    public static class Extension
    {
		public static bool StartsWithIgnoreCase(this string value, string compareTo) {
			return value.ContainsIgnoreCase(compareTo) && value.ToLower().IndexOf(compareTo.ToLower()) == 0;
		}

		public static void AppendLineFormat(this StringBuilder value, string format, object[] args)
        {
            value.AppendFormat(format, args);
            value.AppendLine();
        }

        public static void AppendLineFormat(this StringBuilder value, string format, object arg1)
        {
            value.AppendLineFormat(format, new[] { arg1 });
        }

        public static void AppendLineFormat(this StringBuilder value, string format, object arg1, object arg2)
        {
            value.AppendLineFormat(format, new[] { arg1, arg2 });
        }

        public static void AppendLineFormat(this StringBuilder value, string format, object arg1, object arg2, object arg3)
        {
            value.AppendLineFormat(format, new[] { arg1, arg2, arg3 });
        }

        public static void Return(this StringBuilder value)
        {
            value.AppendLine();
        }

        public static string RemoveNonNumbers(this string value)
        {
            value = value ?? string.Empty;
            return new string(value.Where(c => char.IsDigit(c)).ToArray());
        }

        public static string ToPhoneNumber(this string value)
        {
            switch (value.Length)
            {
                case 7:
                    return $"{value.Substring(0, 3)}-{value.Substring(3, 4)}";
                case 10:
                    return $"({value.Substring(0, 3)}) {value.Substring(3, 3)}-{value.Substring(6, 4)}";
                case 11:
                    return $"{value.Substring(0, 1)} ({value.Substring(1, 3)}) {value.Substring(4, 3)}-{value.Substring(7, 4)}";
                default:
                    return value;
            }
        }

        public static string ReplaceIgnoreCase(this string value, string findText, string replaceText)
        {
            var startIndex = value.IndexOf(findText, System.StringComparison.OrdinalIgnoreCase);
            if (startIndex == -1)
                return value;
            var endIndex = startIndex + findText.Length;
            var left = startIndex == 0 ? string.Empty : value.Substring(0, startIndex);
            var right = value.Substring(endIndex);
            return left + replaceText + right;
        }

        public static bool ContainsIgnoreCase(this string value, string findText)
        {
            return System.Globalization.CultureInfo.CurrentCulture.CompareInfo.IndexOf(value, findText, System.Globalization.CompareOptions.IgnoreCase) >= 0;
        }

		public static string WrapLongString(this string value, int wrapAt, bool isBreakOnWord = true) {
			var result = new StringBuilder();
			if (value.Length <= wrapAt)
				return value;
			if (!isBreakOnWord) {
				while (value.Length > wrapAt) {
					result.AppendLine(value.Substring(0, wrapAt));
					value = value.Substring(wrapAt, value.Length - wrapAt).TrimStart();
				}
				result.AppendLine(value);
			} else {
				while (value.Length > wrapAt) {
					if(value.Substring(wrapAt,1) == " ") {
						result.AppendLine(value.Substring(0, wrapAt));
						value = value.Substring(wrapAt, value.Length - wrapAt);
					} else {
						var wa = wrapAt;
						while(value.Substring(wa, 1) != " ") {
							wa--;
						}
						result.AppendLine(value.Substring(0, wa));
						value = value.Substring(wa, value.Length - wa).TrimStart();
					}
				}
				result.AppendLine(value);
			}
			return result.ToString();
		}
    }
}