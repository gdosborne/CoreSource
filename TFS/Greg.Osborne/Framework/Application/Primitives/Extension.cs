using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace GregOsborne.Application.Primitives {
	public static class Extension {

		private const double OneKb = 1024.0;

		public static bool HasMethod(this Type type, string methodName) {
			var methods = type.GetMethods();
			return methods.Any(x => x.Name == methodName);
		}
		public static T As<T>(this object item) where T : class {
			return item as T;
		}

		public static bool Between(this DateTime value, DateTime start, DateTime end) {
			return value >= start && value <= end;
		}

		public static string ExpandKey(this string value, List<KeyValuePair<string, string>> allValues, char valueIdKey) {
			if (string.IsNullOrEmpty(value) || allValues.Count == 0) {
				return value;
			}

			if (value.Contains(valueIdKey.ToString())) {
				value = value.Replace(valueIdKey.ToString(), string.Empty);
			}

			var result = allValues.FirstOrDefault(x => x.Key == value).Value;
			if (string.IsNullOrEmpty(result)) {
				return result;
			}

			while (result.Contains(valueIdKey)) {
				var foundValue = false;
				foreach (var item in allValues) {
					var itemKey = $"{valueIdKey}{item.Key}{valueIdKey}";
					if (!result.Contains(itemKey)) {
						continue;
					}

					result = result.Replace(itemKey, allValues.First(x => x.Key == item.Key).Value);
					foundValue = true;
				}
				if (!foundValue) {
					break;
				}
			}
			return result;
		}

		public static string ExpandValue(this string value, List<KeyValuePair<string, string>> allValues, char valueIdKey) {
			if (string.IsNullOrEmpty(value) || allValues.Count == 0) {
				return value;
			}

			var result = value;
			while (true) {
				var start = result.IndexOf(valueIdKey);
				if (start == -1) {
					break;
				}

				var end = result.IndexOf(valueIdKey, start + 1);
				if (end == -1) {
					break;
				}

				var pName = result.Substring(start, end - start + 1);
				var tester = pName.Replace(valueIdKey.ToString(), string.Empty);
				if (allValues.Any(x => x.Key == tester)) {
					var pValue = pName.ExpandKey(allValues, valueIdKey);
					result = result.Replace(pName, pValue);
				} else {
					break;
				}
			}
			return result;
		}

		public static string GetPropertyName(this MethodBase value) {
			return value.Name.Replace("set_", string.Empty);
		}

		public static bool Is<T>(this object item) where T : class {
			return item is T;
		}

		public static bool IsAfter(this DateTime value, DateTime theDate) {
			return value > theDate;
		}

		public static bool IsBefore(this DateTime value, DateTime theDate) {
			return value < theDate;
		}

		public static bool IsBetween<T>(this T value, T minValue, T maxValue, bool inclusive) where T : IComparable {
			if (inclusive) {
				return value.CompareTo(minValue) >= 0 && value.CompareTo(maxValue) <= 0;
			}

			return value.CompareTo(minValue) > 0 && value.CompareTo(maxValue) < 0;
		}

		public static bool IsFuture(this DateTime value) {
			return value.IsAfter(DateTime.Now);
		}

		public static bool IsIn<T>(this T source, params T[] list) {
			if (null == source) {
				throw new ArgumentNullException(nameof(source));
			}

			return list.Contains(source);
		}

		public static bool IsNot<T>(this object item) where T : class {
			return !item.Is<T>();
		}

		public static bool IsPast(this DateTime value) {
			return value < DateTime.Now;
		}

		public static string RemoveNonNumeric(this string value) {
			return value.ToCharArray().Where(char.IsNumber).ToString();
		}

		public static T CastTo<T>(this object value) {
			return value.CastTo<T>(default);
		}

		public static T CastTo<T>(this object value, T defaultValue, IFormatProvider formatProvider) {
			var result = defaultValue;
			try {
				result = (T)Convert.ChangeType(value, typeof(T));
			}
			catch (System.Exception ex) {
				if (value.GetType().GetInterface("IConvertible", true) != null && formatProvider != null) {
					try {
						result = (T)value.As<IConvertible>().ToType(typeof(T), formatProvider);
					}
					catch {
						throw new ApplicationException($"The type {typeof(T).Name} is not convertable or does not implement IConvertable", ex);
					}
				} else {
					if (typeof(T) == typeof(bool?)) {
						result = (T)(object)((string)value).ToBool();
					} else if (typeof(T) == typeof(byte?)) {
						result = (T)(object)((string)value).ToByte();
					} else if (typeof(T) == typeof(DateTime?)) {
						result = (T)(object)((string)value).ToDateTime();
					} else if (typeof(T) == typeof(double?)) {
						result = (T)(object)((string)value).ToDouble();
					} else if (typeof(T) == typeof(short?)) {
						result = (T)(object)((string)value).ToInt16();
					} else if (typeof(T) == typeof(int?)) {
						result = (T)(object)((string)value).ToInt32();
					} else if (typeof(T) == typeof(long?)) {
						result = (T)(object)((string)value).ToInt64();
					} else if (typeof(T) == typeof(float?)) {
						result = (T)(object)((string)value).ToFloat();
					}
				}
			}
			return result;
		}

		public static T CastTo<T>(this object value, T defaultValue) {
			return value.CastTo<T>(defaultValue, null);
		}

		public static bool? ToBool(this string value) {
			if (value == "null") {
				return null;
			}

			return bool.TryParse(value, out var t) ? (bool?)t : null;
		}

		public static byte? ToByte(this string value) {
			if (value == "null") {
				return null;
			}

			return byte.TryParse(value, out var t) ? (byte?)t : null;
		}

		public static DateTime? ToDateTime(this string value) {
			if (value == "null") {
				return null;
			}

			return DateTime.TryParse(value, out var t) ? (DateTime?)t : null;
		}

		public static double? ToDouble(this string value) {
			if (value == "null") {
				return null;
			}

			return double.TryParse(value, out var t) ? (double?)t : null;
		}

		public static double? ToFloat(this string value) {
			if (value == "null") {
				return null;
			}

			return float.TryParse(value, out var t) ? (float?)t : null;
		}

		public static double ToGb(this long value) {
			return Convert.ToDouble(value) / OneKb / OneKb / OneKb;
		}

		public static short? ToInt16(this string value) {
			if (value == "null") {
				return null;
			}

			return short.TryParse(value, out var t) ? (short?)t : null;
		}

		public static int? ToInt32(this string value) {
			if (value == "null") {
				return null;
			}

			return int.TryParse(value, out var t) ? (int?)t : null;
		}

		public static long? ToInt64(this string value) {
			if (value == "null") {
				return null;
			}

			return long.TryParse(value, out var t) ? (long?)t : null;
		}

		public static double ToKb(this long value) {
			return Convert.ToDouble(value) / OneKb;
		}

		public static string ToKbString<T>(this T value, bool includeCommas = true) {
			var format = includeCommas ? "#,0.00 KB" : "0.00 KB";
			var mbValue = Convert.ToDouble(value) / 1024.0;
			return mbValue.ToString(format);
		}

		public static double ToMb(this long value) {
			return Convert.ToDouble(value) / OneKb / OneKb;
		}

		public static string ToMbString<T>(this T value, bool includeCommas = true) {
			var format = includeCommas ? "#,0.00 MB" : "0.00 MB";
			var mbValue = Convert.ToDouble(value) / (1024.0 * 1024.0);
			return mbValue.ToString(format);
		}

		public static Point ToPoint(this string value) {
			return Point.Parse(value);
		}

		public static Size ToSize(this string value) {
			return Size.Parse(value);
		}

		public static double ToTb(this long value) {
			return Convert.ToDouble(value) / OneKb / OneKb / OneKb / OneKb;
		}

		public static TimeSpan? ToTimeSpan(this string value) {
			return TimeSpan.TryParse(value, out var t) ? (TimeSpan?)t : null;
		}

		public static Version ToVersion(this string value) {
#if !DOTNET3_5
			return Version.TryParse(value, out var t) ? t : null;
#else
			try {
				return new Version(value);
			}
			catch { }
			return new Version();
#endif
		}

		public static WindowState ToWindowState(this string value) {
			var names = new List<string>(Enum.GetNames(typeof(WindowState)));
			return names.Contains(value) ? (WindowState)Enum.Parse(typeof(WindowState), value, true) : WindowState.Normal;
		}
	}
}