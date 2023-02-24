using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Application.Text {
    public static class Extension {
        public static string PrecededByDateTime(this string value, int tabIndex = 0) =>
            $"{DateTime.Now:yyyy-MM-dd hh:mm:ss.fffff tt} => {new string(' ', tabIndex * 4)}{value}";

        public static void AppendLineFormat(this StringBuilder value, string format, object[] args) {
            value.AppendFormat(format, args);
            value.AppendLine();
        }

        public static string MakeSingleLine(this string value) {
            var result = value.Replace("\n", " ")
                .Replace("\t", " ")
                .Trim(' ');
            result = Regex.Replace(result, @"\s+", " ");
            return result;
        }

        public static string SplitAtCaps(this string value, bool isReplaceUndersore = true) {
            var result = new StringBuilder();
            var chars = value.ToCharArray();
            for (int i = 0; i < chars.Length; i++) {
                if (chars[i] == '_' && isReplaceUndersore) {
                    result.Append(' ');
                    continue;
                }
                    
                var shouldAddSpace = i > 0
                    && char.IsLetter(chars[i])
                    && char.IsUpper(chars[i])
                    && !char.IsUpper(chars[i - 1]);
                if (shouldAddSpace)
                    result.Append(' ');
                result.Append(chars[i]);
            }
            return result.ToString();
        }

        public static void AppendLineFormat(this StringBuilder value, string format, object arg1) => value.AppendLineFormat(format, new[] { arg1 });

        public static void AppendLineFormat(this StringBuilder value, string format, object arg1, object arg2) => value.AppendLineFormat(format, new[] { arg1, arg2 });

        public static void AppendLineFormat(this StringBuilder value, string format, object arg1, object arg2, object arg3) => value.AppendLineFormat(format, new[] { arg1, arg2, arg3 });

        public static void Return(this StringBuilder value) => value.AppendLine();

        public static string RemoveNonNumbers(this string value, bool keepDecimal = true) {
            value = value ?? string.Empty;
            return new string(value.Where(c => char.IsDigit(c) || (keepDecimal && c == '.')).ToArray());
        }

        public static string ToPhoneNumber(this string value) {
            value = value.RemoveNonNumbers(false);
            switch (value.Length) {
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

        public static string ReplaceIgnoreCase(this string value, string findText, string replaceText) {
            var startIndex = value.IndexOf(findText, System.StringComparison.OrdinalIgnoreCase);
            if (startIndex == -1) {
                return value;
            }

            var endIndex = startIndex + findText.Length;
            var left = startIndex == 0 ? string.Empty : value.Substring(0, startIndex);
            var right = value.Substring(endIndex);
            return left + replaceText + right;
        }

        public static bool ContainsIgnoreCase(this string value, string findText) => System.Globalization.CultureInfo.CurrentCulture.CompareInfo.IndexOf(value, findText, System.Globalization.CompareOptions.IgnoreCase) >= 0;

        public static bool StartsWithIgnoreCase(this string value, string findText) => System.Globalization.CultureInfo.CurrentCulture.CompareInfo.IndexOf(value, findText, System.Globalization.CompareOptions.IgnoreCase) == 0;
    }
}