namespace GregOsborne.Application.Primitives {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Media;

    public static class Extension {
        private const double oneKb = 1024.0;
        public static double OneMegaByte => oneKb * 1000;
        public static double OneGigaByte => OneMegaByte * 1000;

        public static double OneTeraByte => OneGigaByte * 1000;
        private static void CheckTheType(Type theType) {
            if (theType == null) {
                throw new ArgumentNullException("theType must not be null");
            }
            if (!theType.IsEnum) {
                throw new ArgumentException("theType must be an enumerated type");
            }
        }

        
        public static PropertyInfo GetPropertyIgnoreCase(this Type type, string name) {
            var result = type.GetProperty(name);
            if (result == null) {
                foreach (var prop in type.GetProperties()) {
                    if (prop.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) {
                        result = prop;
                        break;
                    }
                }
            }
            return result;
        }

        public static bool Contains<T>(this Type theType, T theValue) where T : struct, IConvertible {
            CheckTheType(theType);
            return Enum.IsDefined(theType, theValue);
        }

        public static bool Contains<T>(this T hardValue, T theValue) where T : struct, IConvertible {
            CheckTheType(typeof(T));
            var hardVal = ((IConvertible)hardValue).ToInt32(CultureInfo.InvariantCulture);
            var theVal = ((IConvertible)theValue).ToInt32(CultureInfo.InvariantCulture);
            return (hardVal & theVal) == theVal;
        }

        public static bool Contains<T>(this Type theType, T theValue, T flagValueToCheck) where T : struct, IConvertible {
            CheckTheType(theType);
            if (theType.GetCustomAttributes(true).Any(x => x is FlagsAttribute)) {
                return theValue.Contains(flagValueToCheck);
            }
            return theType.Contains(theValue);
        }

        public static T As<T>(this object item) where T : class => item as T;

        public static bool Between(this DateTime value, DateTime start, DateTime end) => value >= start && value <= end;

        public static T CastTo<T>(this object value) => value.CastTo<T>(default);

        public static T CastTo<T>(this object value, T defaultValue) => value.CastTo<T>(defaultValue, null);

        public static object CastTo(this object value, Type type) {
            var result = default(object);
            try {
                result = Convert.ChangeType(value, type);
            }
            catch (System.Exception) {
                if (type == typeof(bool?)) {
                    result = ((string)value).ToBool();
                }
                else if (type == typeof(byte?)) {
                    result = ((string)value).ToByte();
                }
                else if (type == typeof(DateTime?)) {
                    result = ((string)value).ToDateTime();
                }
                else if (type == typeof(double?)) {
                    result = ((string)value).ToDouble();
                }
                else if (type == typeof(short?)) {
                    result = ((string)value).ToInt16();
                }
                else if (type == typeof(int?)) {
                    result = ((string)value).ToInt32();
                }
                else if (type == typeof(long?)) {
                    result = ((string)value).ToInt64();
                }
                else if (type == typeof(float?)) {
                    result = ((string)value).ToFloat();
                }
                else if (type == typeof(FontFamily)) {
                    result = new FontFamily((string)value);
                }
            }
            return result;
        }

        public static T CastTo<T>(this object value, T defaultValue, IFormatProvider formatProvider) {
            var result = defaultValue;
            if (typeof(T).IsEnum) {
                result = (T)Enum.Parse(typeof(T), value.ToString());
                return result;
            }
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
                }
                else {
                    if (typeof(T) == typeof(bool?)) {
                        result = (T)(object)((string)value).ToBool();
                    }
                    else if (typeof(T) == typeof(byte?)) {
                        result = (T)(object)((string)value).ToByte();
                    }
                    else if (typeof(T) == typeof(DateTime?)) {
                        result = (T)(object)((string)value).ToDateTime();
                    }
                    else if (typeof(T) == typeof(double?)) {
                        result = (T)(object)((string)value).ToDouble();
                    }
                    else if (typeof(T) == typeof(short?)) {
                        result = (T)(object)((string)value).ToInt16();
                    }
                    else if (typeof(T) == typeof(int?)) {
                        result = (T)(object)((string)value).ToInt32();
                    }
                    else if (typeof(T) == typeof(long?)) {
                        result = (T)(object)((string)value).ToInt64();
                    }
                    else if (typeof(T) == typeof(float?)) {
                        result = (T)(object)((string)value).ToFloat();
                    }
                }
            }
            return result;
        }

        public static bool ContainsIgnoreCase(this IEnumerable<string> values, string testValue) => values.Any(x => x.Equals(testValue, StringComparison.OrdinalIgnoreCase));

        public static bool ContainsKeyIgnoreCase<T>(this IDictionary<string, T> values, string testValue) => values.Any(x => x.Key.Equals(testValue, StringComparison.OrdinalIgnoreCase));

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
                }
                else {
                    break;
                }
            }
            return result;
        }

        public static string GetDescription<T>(this Enum value) where T : Enum {
            var defaultValue = value.ToString();
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0) {
                return attributes[0].Description;
            }

            return defaultValue;
        }

        public static string GetPropertyName(this MethodBase value) => value.Name.Replace("set_", string.Empty);

        public static bool HasMethod(this Type type, string methodName) {
            var methods = type.GetMethods();
            return methods.Any(x => x.Name == methodName);
        }

        public static bool HasProperty(this Type value, string name, out PropertyInfo property) {
            property = value.GetProperty(name);
            return property != null;
        }

        public static List<PropertyInfo> GetFilteredProperties(this Type value, params string[] propertyNames) => new List<PropertyInfo>(value.GetProperties().Where(x => propertyNames.Contains(x.Name)));

        public static List<PropertyInfo> GetFilteredProperties(this Type value, params Type[] propertyTypes) => new List<PropertyInfo>(value.GetProperties().Where(x => propertyTypes.Contains(x.PropertyType)));

        public static bool Is<T>(this object item) where T : class => item is T;

        public static bool IsAfter(this DateTime value, DateTime theDate) => value > theDate;

        public static bool IsBefore(this DateTime value, DateTime theDate) => value < theDate;

        public static bool IsBetween<T>(this T value, T minValue, T maxValue, bool inclusive) where T : IComparable {
            if (inclusive) {
                return value.CompareTo(minValue) >= 0 && value.CompareTo(maxValue) <= 0;
            }

            return value.CompareTo(minValue) > 0 && value.CompareTo(maxValue) < 0;
        }

        public static bool IsFuture(this DateTime value) => value.IsAfter(DateTime.Now);

        public static bool IsIn<T>(this T source, params T[] list) {
            if (null == source) {
                throw new ArgumentNullException(nameof(source));
            }

            return list.Contains(source);
        }

        public static bool IsNot<T>(this object item) where T : class => !item.Is<T>();

        public static bool IsPast(this DateTime value) => value < DateTime.Now;

        public static string RemoveNonNumeric(this string value) => value.ToCharArray().Where(char.IsNumber).ToString();

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

        public static double ToGb(this long value) => Convert.ToDouble(value) / OneGigaByte;

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

        public static double ToKb(this long value) => Convert.ToDouble(value) / oneKb;

        public static string ToKbString<T>(this T value, bool includeCommas = true) {
            var format = includeCommas ? "#,0.00 KB" : "0.00 KB";
            var kbValue = Convert.ToInt64(value).ToKb();
            return kbValue.ToString(format);
        }

        public static double ToMb(this long value) => Convert.ToDouble(value) / OneMegaByte;

        public static string ToMbString<T>(this T value, bool includeCommas = true) {
            var format = includeCommas ? "#,0.00 MB" : "0.00 MB";
            var mbValue = Convert.ToInt64(value).ToMb();
            return mbValue.ToString(format);
        }

        public static Point ToPoint(this string value) => Point.Parse(value);

        public static Size ToSize(this string value) => Size.Parse(value);

        public static double ToTb(this long value) => Convert.ToDouble(value) / OneTeraByte;

        public static TimeSpan? ToTimeSpan(this string value) => TimeSpan.TryParse(value, out var t) ? (TimeSpan?)t : null;

        public static Version ToVersion(this string value) => Version.TryParse(value, out var t) ? t : null;

        public static WindowState ToWindowState(this string value) {
            var names = new List<string>(Enum.GetNames(typeof(WindowState)));
            return names.Contains(value) ? (WindowState)Enum.Parse(typeof(WindowState), value, true) : WindowState.Normal;
        }
    }
}
