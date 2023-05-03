// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc.. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
// 
// Primitive extensions
//
namespace SNC.OptiRamp.Application.Extensions.Primitives
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Windows;

	/// <summary>
	/// Class Extension.
	/// </summary>
	public static class Extension
	{
		#region Public Methods
		/// <summary>
		/// Ases the specified item.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item">The item.</param>
		/// <returns>T.</returns>
		public static T As<T>(this object item) where T : class {
			return item as T;
		}
		/// <summary>
		/// Determines whether [contains] [the specified find].
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="find">The find.</param>
		/// <param name="comparer">The comparer.</param>
		/// <returns><c>true</c> if [contains] [the specified find]; otherwise, <c>false</c>.</returns>
		public static bool Contains(this string source, string find, StringComparison comparer) {
			if (find == null)
				throw new ArgumentNullException("find");
			return source.IndexOf(find, comparer) >= 0;
		}
		/// <summary>
		/// Determines whether [contains] [the specified find].
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="find">The find.</param>
		/// <param name="comparer">The comparer.</param>
		/// <returns><c>true</c> if [contains] [the specified find]; otherwise, <c>false</c>.</returns>
		public static bool Contains(this List<string> source, string find, StringComparison comparer) {
			if (find == null)
				throw new ArgumentNullException("find");
			return source.ToArray().Contains(find, comparer);
		}
		/// <summary>
		/// Determines whether [contains] [the specified find].
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="find">The find.</param>
		/// <param name="comparer">The comparer.</param>
		/// <returns><c>true</c> if [contains] [the specified find]; otherwise, <c>false</c>.</returns>
		public static bool Contains(this string[] source, string find, StringComparison comparer) {
			if (find == null)
				throw new ArgumentNullException("find");

			foreach (var item in source) {
				if (item.Equals(find, comparer))
					return true;
			}
			return false;
		}
		/// <summary>
		/// Expands the key.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="allValues">All values.</param>
		/// <param name="valueIdKey">The value identifier key.</param>
		/// <returns>System.String.</returns>
		public static string ExpandKey(this string value, List<KeyValuePair<string, string>> allValues, char valueIdKey) {
			if (allValues == null)
				throw new ArgumentNullException("allValues");

			if (allValues.Count == 0)
				return value;
			if (value.Contains(valueIdKey.ToString()))
				value = value.Replace(valueIdKey.ToString(), string.Empty);
			var result = allValues.FirstOrDefault(x => x.Key == value).Value;
			if (string.IsNullOrEmpty(result))
				return result;
			while (result.Contains(valueIdKey)) {
				var foundValue = false;
				foreach (var item in allValues) {
					var itemKey = string.Format("{0}{1}{0}", valueIdKey, item.Key);
					if (result.Contains(itemKey)) {
						result = result.Replace(itemKey, allValues.First(x => x.Key == item.Key).Value);
						foundValue = true;
					}
				}
				if (!foundValue)
					break;
			}
			return result;
		}
		/// <summary>
		/// Expands the value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="allValues">All values.</param>
		/// <param name="valueIdKey">The value identifier key.</param>
		/// <returns>System.String.</returns>
		public static string ExpandValue(this string value, List<KeyValuePair<string, string>> allValues, char valueIdKey) {
			if (allValues == null)
				throw new ArgumentNullException("allValues");

			if (allValues.Count == 0)
				return value;
			var result = value;
			while (true) {
				var start = result.IndexOf(valueIdKey);
				if (start == -1)
					break;
				var end = result.IndexOf(valueIdKey, start + 1);
				if (end == -1)
					break;
				var pName = result.Substring(start, end - start + 1);
				var tester = pName.Replace(valueIdKey.ToString(), string.Empty);
				if (allValues.Any(x => x.Key == tester)) {
					var pValue = pName.ExpandKey(allValues, valueIdKey);
					result = result.Replace(pName, pValue);
				}
				else
					break;
			}
			return result;
		}
		/// <summary>
		/// Firsts the day of month.
		/// </summary>
		/// <param name="theDate">The date.</param>
		/// <returns>DateTime.</returns>
		public static DateTime FirstDayOfMonth(this DateTime theDate) {
			return new DateTime(theDate.Year, theDate.Month, 1);
		}
		/// <summary>
		/// Determines whether [is] [the specified item].
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item">The item.</param>
		/// <returns><c>true</c> if [is] [the specified item]; otherwise, <c>false</c>.</returns>
		public static bool Is<T>(this object item) where T : class {
			return item is T;
		}
		/// <summary>
		/// Determines whether the specified the date is after.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="theDate">The date.</param>
		/// <returns><c>true</c> if the specified the date is after; otherwise, <c>false</c>.</returns>
		public static bool IsAfter(this DateTime value, DateTime theDate) {
			return value > theDate;
		}
		/// <summary>
		/// Determines whether the specified the date is before.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="theDate">The date.</param>
		/// <returns><c>true</c> if the specified the date is before; otherwise, <c>false</c>.</returns>
		public static bool IsBefore(this DateTime value, DateTime theDate) {
			return value < theDate;
		}
		/// <summary>
		/// Determines whether the specified minimum value is between.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="minValue">The minimum value.</param>
		/// <param name="maxValue">The maximum value.</param>
		/// <param name="inclusive">if set to <c>true</c> [inclusive].</param>
		/// <returns><c>true</c> if the specified minimum value is between; otherwise, <c>false</c>.</returns>
		public static bool IsBetween<T>(this T value, T minValue, T maxValue, bool inclusive) where T : IComparable {
			if (inclusive)
				return value.CompareTo(minValue) >= 0 && value.CompareTo(maxValue) <= 0;
			return value.CompareTo(minValue) > 0 && value.CompareTo(maxValue) < 0;
		}
		/// <summary>
		/// Determines whether the specified value is future.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns><c>true</c> if the specified value is future; otherwise, <c>false</c>.</returns>
		public static bool IsFuture(this DateTime value) {
			return value.IsAfter(DateTime.Now);
		}
		/// <summary>
		/// Determines whether the specified list is in.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The source.</param>
		/// <param name="list">The list.</param>
		/// <returns><c>true</c> if the specified list is in; otherwise, <c>false</c>.</returns>
		/// <exception cref="System.ArgumentNullException">source</exception>
		public static bool IsIn<T>(this T source, params T[] list) {
			if (source == null)
				throw new ArgumentNullException("source");
			if (list == null)
				throw new ArgumentNullException("list");
			return list.Contains(source);
		}
		/// <summary>
		/// Determines whether the specified item is not.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item">The item.</param>
		/// <returns><c>true</c> if the specified item is not; otherwise, <c>false</c>.</returns>
		public static bool IsNot<T>(this object item) where T : class {
			return !(item.Is<T>());
		}
		/// <summary>
		/// Determines whether the specified value is past.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns><c>true</c> if the specified value is past; otherwise, <c>false</c>.</returns>
		public static bool IsPast(this DateTime value) {
			return value < DateTime.Now;
		}
		/// <summary>
		/// Removes the non numeric.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>System.String.</returns>
		public static string RemoveNonNumeric(this string value) {
			var sb = new StringBuilder();
			var chars = value.ToCharArray();
			for (int i = 0; i < chars.Length; i++) {
				if (Char.IsNumber(chars[i]))
					sb.Append(chars[i]);
			}
			return sb.ToString();
		}
		/// <summary>
		/// To the bool.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public static bool? ToBool(this string value) {
			bool t;
			if (bool.TryParse(value, out t))
				return t;
			return null;
		}
		/// <summary>
		/// To the byte.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>System.Nullable&lt;System.Byte&gt;.</returns>
		public static byte? ToByte(this string value) {
			byte t;
			if (byte.TryParse(value, out t))
				return t;
			return null;
		}
		/// <summary>
		/// To the date time.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>System.Nullable&lt;DateTime&gt;.</returns>
		public static DateTime? ToDateTime(this string value) {
			DateTime t;
			if (DateTime.TryParse(value, out t))
				return t;
			return null;
		}
		/// <summary>
		/// To the double.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>System.Nullable&lt;System.Double&gt;.</returns>
		public static double? ToDouble(this string value) {
			double t;
			if (double.TryParse(value, out t))
				return t;
			return null;
		}
		/// <summary>
		/// To the gb.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>System.Double.</returns>
		public static double ToGB(this long value) {
			return ((Convert.ToDouble(value) / oneKB) / oneKB) / oneKB;
		}
		/// <summary>
		/// To the int16.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>System.Nullable&lt;Int16&gt;.</returns>
		public static Int16? ToInt16(this string value) {
			Int16 t;
			if (Int16.TryParse(value, out t))
				return t;
			return null;
		}
		/// <summary>
		/// To the int32.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>System.Nullable&lt;Int32&gt;.</returns>
		public static Int32? ToInt32(this string value) {
			Int32 t;
			if (Int32.TryParse(value, out t))
				return t;
			return null;
		}
		/// <summary>
		/// To the int64.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>System.Nullable&lt;Int64&gt;.</returns>
		public static Int64? ToInt64(this string value) {
			Int64 t;
			if (Int64.TryParse(value, out t))
				return t;
			return null;
		}
		/// <summary>
		/// To the kb.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>System.Double.</returns>
		public static double ToKB(this long value) {
			return Convert.ToDouble(value) / oneKB;
		}
		/// <summary>
		/// To the mb.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>System.Double.</returns>
		public static double ToMB(this long value) {
			return (Convert.ToDouble(value) / oneKB) / oneKB;
		}
		/// <summary>
		/// To the point.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>Point.</returns>
		public static Point ToPoint(this string value) {
			return Point.Parse(value);
		}
		/// <summary>
		/// To the size.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>Size.</returns>
		public static Size ToSize(this string value) {
			return Size.Parse(value);
		}
		/// <summary>
		/// To the tb.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>System.Double.</returns>
		public static double ToTB(this long value) {
			return (((Convert.ToDouble(value) / oneKB) / oneKB) / oneKB) / oneKB;
		}
		/// <summary>
		/// To the time span.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>System.Nullable&lt;TimeSpan&gt;.</returns>
		public static TimeSpan? ToTimeSpan(this string value) {
			TimeSpan t;
			if (TimeSpan.TryParse(value, out t))
				return t;
			return null;
		}
		/// <summary>
		/// To the version.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>Version.</returns>
		public static Version ToVersion(this string value) {
			Version t;
			if (Version.TryParse(value, out t))
				return t;
			return null;
		}
		/// <summary>
		/// To the state of the window.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>WindowState.</returns>
		public static WindowState ToWindowState(this string value) {
			var names = new List<string>(Enum.GetNames(typeof(WindowState)));
			if (names.Contains(value))
				return (WindowState)Enum.Parse(typeof(WindowState), value, true);
			else
				return WindowState.Normal;
		}
		/// <summary>
		/// Works the days.
		/// </summary>
		/// <param name="start">The start.</param>
		/// <param name="end">The end.</param>
		/// <returns>System.Int32.</returns>
		public static int WorkDays(this DateTime start, DateTime end) {
			var temp = start;
			var result = 0;
			while (temp.Date <= end.Date) {
				if (temp.DayOfWeek != DayOfWeek.Saturday && temp.DayOfWeek != DayOfWeek.Sunday)
					result++;
			}
			return result;
		}
		#endregion Public Methods

		#region Private Fields
		private const double oneKB = 1024.0;
		#endregion Private Fields
	}
}