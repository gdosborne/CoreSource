/* File="Extension"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using Common.Primitives;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Universal.Common;

namespace Common.Text {
    public static class Extension {
        public static string Strip(this string value, params char[] characters) {
            var result = new StringBuilder(value);
            foreach (var c in characters) {
                result = result.Replace(c, ' ');
            }
            return result.ToString().Trim();
        }

        public static string ToNullIfEmpty(this string value) => value.IsNull() ? null : value;

        public static string BreakAtCapitals(this string value) {
            var result = new StringBuilder();
            foreach (var c in value.ToCharArray()) {
                if (char.IsUpper(c)) result.Append($" {c}");
                else result.Append(c);
            }
            return result.ToString();
        }

        public static string ToCamelCase(this string value) {
            var chars = value.ToCharArray();
            var result = default(string);
            var wasLastCap = false;
            var isProcessingStopped = false;
            for (var i = 0; i < chars.Length; i++) {
                var c = chars[i];
                if (i == 0 && char.IsLower(c)) {
                    result = value;
                    break;
                }
                if (isProcessingStopped) {
                    result += c;
                } else {
                    if (char.IsLower(c)) {
                        result += c;
                        isProcessingStopped = true;
                    } else if (char.IsUpper(c)) {
                        if (wasLastCap || i == 0) {
                            result += c.ToLower();
                        } else {
                            result += c;
                        }
                        wasLastCap = true;
                    }
                    wasLastCap = true;
                }
            }
            return result;
        }

        public static string RemoveValue(this string valueList, string fieldName, char separator = ',') {
            var values = valueList.Replace($"{separator} ", separator.ToString()).Split(separator).ToList();
            var valField = values.FirstOrDefault(x => x.EqualsIgnoreCase(fieldName));
            if (!valField.IsNull()) {
                values.Remove(valField);
                return string.Join(',', values);
            }
            return valueList;
        }

        public static string GetNextSequenceNumber(string current) {
            var isPrefixSet = false;
            var isNumberSet = false;
            var prefix = string.Empty;
            var suffix = string.Empty;
            var number = string.Empty;
            if (current.IsNull()) return null;
            var chars = current.ToCharArray();
            foreach (var c in chars) {
                if (char.IsNumber(c)) {
                    if (!isPrefixSet) {
                        isPrefixSet = true;
                    }
                    number += c;
                } else if (char.IsLetter(c)) {
                    if (!isPrefixSet) {
                        prefix += c;
                    } else if (!isNumberSet) {
                        isNumberSet = true;
                        suffix += c;
                    } else {
                        suffix += c;
                    }
                }
            }
            var nextNumber = int.Parse(number) + 1;
            return $"{prefix}{nextNumber.ToString().PadLeft(number.Length, '0')}{suffix}";
        }

        public static string PrecededByDateTime(this string value, int tabIndex = 0) =>
            $"{DateTime.Now:yyyy-MM-dd hh:mm:ss.fffff tt} => {new string(' ', tabIndex * 4)}{value}";

        public static StringBuilder AppendLine(this StringBuilder value, StringBuilder adding, int count) {
            value.AppendLine(adding.ToString());
            for (int i = 0; i < count; i++) {
                value.AppendLine();
            }
            return value;
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

        public static StringBuilder AppendLineFormat(this StringBuilder value, string format, object arg1, object arg2 = null, object arg3 = null) {
            if (arg1.IsNull() && arg2.IsNull() && arg3.IsNull())
                return value;
            else if (!arg1.IsNull() && arg2.IsNull() && arg3.IsNull())
                value.AppendFormat(format, arg1);
            else if (!arg1.IsNull() && !arg2.IsNull() && arg3.IsNull())
                value.AppendFormat(format, arg1, arg2);
            else
                value.AppendFormat(format, arg1, arg2, arg3);
            value.AppendLine();
            return value;
        }

        public static StringBuilder Return(this StringBuilder value) {
            value.AppendLine();
            return value;
        }

        public static string RemoveNonNumbers(this string value, bool keepDecimal = true) {
            value ??= string.Empty;
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
            var startIndex = value.IndexOfIgnoreCase(findText);
            if (startIndex == -1) {
                return value;
            }

            var endIndex = startIndex + findText.Length;
            var left = startIndex == 0 ? string.Empty : value.Substring(0, startIndex);
            var right = value.Substring(endIndex);
            return left + replaceText + right;
        }

        public static bool ContainsIgnoreCase(this string value, string findText) => value.IsNull() ? false : value.Contains(findText, StringComparison.OrdinalIgnoreCase);
        
        public static bool EqualsIgnoreCase(this string value, string findText) => value.IsNull() ? false : value.Equals(findText, StringComparison.OrdinalIgnoreCase);

        public static bool StartsWithIgnoreCase(this string value, string findText) => value.IsNull() ? false : value.StartsWith(findText, StringComparison.OrdinalIgnoreCase);

        public static bool EndsWithIgnoreCase(this string value, string findText) => value.IsNull() ? false : value.EndsWith(findText, StringComparison.OrdinalIgnoreCase);

        public static int IndexOfIgnoreCase(this string value, string findText) => value.IsNull() ? -1 : value.IndexOf(findText, StringComparison.OrdinalIgnoreCase);
    }
}
