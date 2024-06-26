using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MyApplication.Primitives
{
	public static class Extension
	{
		#region Private Fields
		private const double oneKB = 1024.0;
		#endregion

		#region Public Methods

		public static T As<T>(this object item) where T : class
		{
			return item as T;
		}

		public static bool Between(this DateTime value, DateTime start, DateTime end)
		{
			return value >= start && value <= end;
		}

		public static string ExpandKey(this string value, List<KeyValuePair<string, string>> allValues, char valueIdKey)
		{
			if (string.IsNullOrEmpty(value) || allValues.Count == 0)
				return value;
			if (value.Contains(valueIdKey.ToString()))
				value = value.Replace(valueIdKey.ToString(), string.Empty);
			var result = allValues.FirstOrDefault(x => x.Key == value).Value;
			if (string.IsNullOrEmpty(result))
				return result;
			while (result.Contains(valueIdKey))
			{
				var foundValue = false;
				foreach (var item in allValues)
				{
					var itemKey = string.Format("{0}{1}{0}", valueIdKey, item.Key);
					if (result.Contains(itemKey))
					{
						result = result.Replace(itemKey, allValues.First(x => x.Key == item.Key).Value);
						foundValue = true;
					}
				}
				if (!foundValue)
					break;
			}
			return result;
		}

		public static string ExpandValue(this string value, List<KeyValuePair<string, string>> allValues, char valueIdKey)
		{
			if (string.IsNullOrEmpty(value) || allValues.Count == 0)
				return value;
			var result = value;
			while (true)
			{
				var start = result.IndexOf(valueIdKey);
				if (start == -1)
					break;
				var end = result.IndexOf(valueIdKey, start + 1);
				if (end == -1)
					break;
				var pName = result.Substring(start, end - start + 1);
				var tester = pName.Replace(valueIdKey.ToString(), string.Empty);
				if (allValues.Any(x => x.Key == tester))
				{
					var pValue = pName.ExpandKey(allValues, valueIdKey);
					result = result.Replace(pName, pValue);
				}
				else
					break;
			}
			return result;
		}

		public static bool Is<T>(this object item) where T : class
		{
			return item is T;
		}

		public static bool IsAfter(this DateTime value, DateTime theDate)
		{
			return value > theDate;
		}

		public static bool IsBefore(this DateTime value, DateTime theDate)
		{
			return value < theDate;
		}

		public static bool IsBetween<T>(this T value, T minValue, T maxValue, bool inclusive) where T : IComparable
		{
			if (inclusive)
				return value.CompareTo(minValue) >= 0 && value.CompareTo(maxValue) <= 0;
			return value.CompareTo(minValue) > 0 && value.CompareTo(maxValue) < 0;
		}

		public static bool IsFuture(this DateTime value)
		{
			return value.IsAfter(DateTime.Now);
		}

		public static bool IsIn<T>(this T source, params T[] list)
		{
			if (null == source) throw new ArgumentNullException("source");
			return list.Contains(source);
		}

		public static bool IsNot<T>(this object item) where T : class
		{
			return !(item.Is<T>());
		}

		public static bool IsPast(this DateTime value)
		{
			return value < DateTime.Now;
		}

		public static string RemoveNonNumeric(this string value)
		{
			var sb = new StringBuilder();
			var chars = value.ToCharArray();
			for (int i = 0; i < chars.Length; i++)
			{
				if (Char.IsNumber(chars[i]))
					sb.Append(chars[i]);
			}
			return sb.ToString();
		}

		public static bool? ToBool(this string value)
		{
			bool t;
			if (bool.TryParse(value, out t))
				return t;
			return null;
		}

		public static byte? ToByte(this string value)
		{
			byte t;
			if (byte.TryParse(value, out t))
				return t;
			return null;
		}

		public static DateTime? ToDateTime(this string value)
		{
			DateTime t;
			if (DateTime.TryParse(value, out t))
				return t;
			return null;
		}

		public static double? ToDouble(this string value)
		{
			double t;
			if (double.TryParse(value, out t))
				return t;
			return null;
		}

		public static double ToGB(this long value)
		{
			return ((Convert.ToDouble(value) / oneKB) / oneKB) / oneKB;
		}

		public static Int16? ToInt16(this string value)
		{
			Int16 t;
			if (Int16.TryParse(value, out t))
				return t;
			return null;
		}

		public static Int32? ToInt32(this string value)
		{
			Int32 t;
			if (Int32.TryParse(value, out t))
				return t;
			return null;
		}

		public static Int64? ToInt64(this string value)
		{
			Int64 t;
			if (Int64.TryParse(value, out t))
				return t;
			return null;
		}

		public static double ToKB(this long value)
		{
			return Convert.ToDouble(value) / oneKB;
		}

		public static double ToMB(this long value)
		{
			return (Convert.ToDouble(value) / oneKB) / oneKB;
		}

		public static Point ToPoint(this string value)
		{
			return Point.Parse(value);
		}

		public static Size ToSize(this string value)
		{
			return Size.Parse(value);
		}

		public static double ToTB(this long value)
		{
			return (((Convert.ToDouble(value) / oneKB) / oneKB) / oneKB) / oneKB;
		}

		public static TimeSpan? ToTimeSpan(this string value)
		{
			TimeSpan t;
			if (TimeSpan.TryParse(value, out t))
				return t;
			return null;
		}

		public static Version ToVersion(this string value)
		{
			Version t;
			if (Version.TryParse(value, out t))
				return t;
			return null;
		}

		public static WindowState ToWindowState(this string value)
		{
			var names = new List<string>(Enum.GetNames(typeof(WindowState)));
			if (names.Contains(value))
				return (WindowState)Enum.Parse(typeof(WindowState), value, true);
			else
				return WindowState.Normal;
		}

		#endregion
	}
}