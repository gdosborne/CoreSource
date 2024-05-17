/* File="Extension"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using OzFramework.Attributes;
using OzFramework.Text;
using Microsoft.IdentityModel.Tokens;
using SharpDX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using static OzFramework.ByteSize;
using static OzFramework.Primitives.Extension;

namespace OzFramework.Primitives {
    public static class Extension {
        [DebuggerStepThrough]
        public static IEnumerable<T> AddRange<T>(this IEnumerable<T> value, T min, T max) {
            if (!typeof(T).IsNumericType()) 
                return value;
            var result = new List<T>(value);
            for (double i = min.CastTo<double>(); i <= max.CastTo<double>(); i++) {
                result.Add((T)(object)i);
            }
            return result;
        }

        [DebuggerStepThrough]
        public static IEnumerable<uint> Range(uint min, uint max) {
            var result = new List<uint>();
            for (uint i = min; i <= max; i++) {
                result.Add(i);
            }
            return result;
        }

        [DebuggerStepThrough]
        public static Version Increment(this Version value, int maxPartValue = 99) {
            var result = new Version(1, 0, 0, 0);
            if (value.Revision == maxPartValue) {
                result = new Version(value.Major, value.Minor, value.Build + 1, 0);
                if (value.Build == maxPartValue) {
                    result = new Version(value.Major, value.Minor + 1, 0, 0);
                    if (value.Minor == maxPartValue) {
                        result = new Version(value.Major + 1, 0, 0, 0);
                    }
                }
            }
            return result;
        }

        [DebuggerStepThrough]
        public static Version DeIncrement(this Version value, int maxPartValue = 99) {
            var result = new Version(value.Major, value.Minor, value.Build, value.Revision - 1);
            //Debug.WriteLine(result);
            if (result.Revision <= 0) {
                result = new Version(value.Major, value.Minor, value.Build - 1, maxPartValue);
                if (result.Build <= 0) {
                    result = new Version(value.Major, value.Minor - 1, maxPartValue, maxPartValue);
                    if (result.Minor <= 0) {
                        result = new Version(value.Major - 1, maxPartValue, maxPartValue, maxPartValue);
                        if (result.Major <= 0) {
                            result = new Version(0, 0, 0, 0);
                        }
                    }
                }
            }
            return result;
        }

        [DebuggerStepThrough]
        public static Guid ToGuid(this ReadOnlySpan<byte> values, bool forceUpperCase = true) {
            if (values == null) throw new ArgumentNullException(nameof(values));
            if (values.Length != 16) throw new ApplicationException("The value must contain exactly 16 byte entries");

            var result = new Guid(values);
            if (forceUpperCase)
                return Guid.Parse(new Guid(values).ToString().ToUpper());
            return result;
        }

        [DebuggerStepThrough]
        public static bool IsNullableType(this Type type) =>
            !Nullable.GetUnderlyingType(type).IsNull();

        [DebuggerStepThrough]
        public static bool IsNullableType(this object value) =>
            !Nullable.GetUnderlyingType(value.GetType()).IsNull();

        [DebuggerStepThrough]
        public static bool IsReferenceType(this object value) {
            try {
                if (value.IsNull())
                    return false;
                return !value.GetType().IsValueType;
            } catch {
                return false;
            }
        }

        [DebuggerStepThrough]
        public static bool IsGuidType(this object value) {
            if (value.IsNull() || (value.IsReferenceType() && !value.IsNullableType()))
                return false;
            else {
                return value.GetType() == typeof(Guid) || value.GetType() == typeof(Guid?);
            }
        }

        [DebuggerStepThrough]
        public static bool IsEmptyGuid(this Guid? value) {
            var isNullable = !Nullable.GetUnderlyingType(value.GetType()).IsNull();
            var result = false;
            if (isNullable) {
                result = Nullable.GetUnderlyingType(value.GetType()) == typeof(Guid);
                result &= !value.HasValue;
            } else {
                result = value.GetType() == typeof(Guid);
            }
            return result &= value == new Guid();
        }

        [DebuggerStepThrough]
        public static bool IsEmpty(this object value) {
            try {
                if (value.IsGuidType()) {
                    Guid? g = (Guid)value;
                    return g.IsEmptyGuid();
                } else {
                    return value == default;
                }
            } catch {
                return value == default;
            }
        }

        [DebuggerStepThrough]
        public static T FindMax<T>() {
            try {
                return (T)typeof(T).GetField("MaxValue").GetValue(null);
            } catch {
                throw new InvalidOperationException($"Unsupported type {typeof(T)}");
            }
        }

        [DebuggerStepThrough]
        public static bool IsNull(this object value) {
            if (value.Is<string>()) {
                return string.IsNullOrEmpty(value.As<string>()) || string.IsNullOrWhiteSpace(value.As<string>());
            }
            return value == default(object);
        }

        [DebuggerStepThrough]
        private static void CheckTheType(Type theType) {
            if (theType.IsNull()) {
                throw new ArgumentNullException("theType must not be null");
            }
            if (!theType.IsEnum) {
                throw new ArgumentException("theType must be an enumerated type");
            }
        }

        [DebuggerStepThrough]
        public static bool IsNumericValue(this string theValue) {
            double.TryParse(theValue, out var result);
            return theValue.Equals(result.ToString());
        }

        [DebuggerStepThrough]
        public static bool IsNumericType(this Type type) {
            if (type == typeof(byte) || type == typeof(sbyte)
                || type == typeof(Int16) || type == typeof(UInt16)
                || type == typeof(Int32) || type == typeof(UInt32)
                || type == typeof(Int64) || type == typeof(UInt64)
                || type == typeof(Single) || type == typeof(Double)
                || type == typeof(Decimal)) { return true; }
            return false;
        }

        [DebuggerStepThrough]
        public static PropertyInfo GetPropertyIgnoreCase(this Type type, string name) {
            var result = type.GetProperty(name);
            if (result.IsNull()) {
                foreach (var prop in type.GetProperties()) {
                    if (prop.Name.EqualsIgnoreCase(name)) {
                        result = prop;
                        break;
                    }
                }
            }
            return result;
        }

        [DebuggerStepThrough]
        public static string GetValueName(this Type theType, int theValue) {
            if (theType.IsEnum) {
                return Enum.GetName(theType, theValue);
            }
            var result = "None";
            if (theValue > 0) {
                try {
                    var p = theType.GetProperties(BindingFlags.Static | BindingFlags.Public)
                        .FirstOrDefault(x => ((int)x.GetValue(null)) == theValue);
                    if (!p.IsNull()) {
                        result = p.Name;
                    }
                } finally { }
            }
            return result;
        }

        [DebuggerStepThrough]
        public static int? GetValue(this Type theType, string valueName) {
            if (theType.IsNull()) {
                return null;
            }
            if (theType.IsEnum) {
                return (int)Enum.Parse(theType, valueName);
            }
            var result = 0;
            try {
                var p = theType.GetProperty(valueName, BindingFlags.Static | BindingFlags.Public);
                if (!p.IsNull()) {
                    result = (int)p.GetValue(null);
                }
            } finally { }
            return result;
        }

        [DebuggerStepThrough]
        public static IEnumerable<string> GetAllNames(this Type theType) =>
            theType.IsEnum
                ? Enum.GetNames(theType).OrderBy(x => x)
                : theType
                    .GetProperties(BindingFlags.Static | BindingFlags.Public)
                    .Where(x => x.CanRead)
                    .Select(x => x.Name)
                    .OrderBy(x => x);

        [DebuggerStepThrough]
        public static bool IsDefault(this object theValue) =>
            theValue == default;

        [DebuggerStepThrough]
        public static bool Contains<T>(this Type theType, T theValue) where T : struct, IConvertible {
            CheckTheType(theType);
            return Enum.IsDefined(theType, theValue);
        }

        [DebuggerStepThrough]
        public static bool Contains<T>(this T hardValue, T theValue) where T : struct, IConvertible {
            CheckTheType(typeof(T));
            var hardVal = ((IConvertible)hardValue).ToInt32(CultureInfo.InvariantCulture);
            var theVal = ((IConvertible)theValue).ToInt32(CultureInfo.InvariantCulture);
            return (hardVal & theVal) == theVal;
        }

        [DebuggerStepThrough]
        public static bool Contains<T>(this Type theType, T theValue, T flagValueToCheck)
            where T : struct, IConvertible {
            CheckTheType(theType);
            if (theType.GetCustomAttributes(true).Any(x => x is FlagsAttribute)) {
                return theValue.Contains(flagValueToCheck);
            }
            return theType.Contains(theValue);
        }

        [DebuggerStepThrough]
        public static T As<T>(this object item) where T : class => item as T;

        [DebuggerStepThrough]
        public static T CastTo<T>(this object value) => value.CastTo<T>(default);

        [DebuggerStepThrough]
        public static T CastTo<T>(this object value, T defaultValue) => value.CastTo<T>(defaultValue, null);

        [DebuggerStepThrough]
        public static object CastTo(this object value, Type type) {
            var result = default(object);
            try {
                result = Convert.ChangeType(value, type);
            } catch (System.Exception) {
                if (type == typeof(bool?)) {
                    result = ((string)value).ToBool();
                } else if (type == typeof(byte?)) {
                    result = ((string)value).ToByte();
                } else if (type == typeof(DateTime?)) {
                    result = ((string)value).ToDateTime();
                } else if (type == typeof(double?)) {
                    result = ((string)value).ToDouble();
                } else if (type == typeof(short?)) {
                    result = ((string)value).ToInt16();
                } else if (type == typeof(int?)) {
                    result = ((string)value).ToInt32();
                } else if (type == typeof(long?)) {
                    result = ((string)value).ToInt64();
                } else if (type == typeof(float?)) {
                    result = ((string)value).ToFloat();
                } else if (type == typeof(FontFamily)) {
                    result = new FontFamily((string)value);
                }
            }
            return result;
        }

        [DebuggerStepThrough]
        public static T CastTo<T>(this object value, T defaultValue, IFormatProvider formatProvider) {
            var result = defaultValue;
            if (typeof(T).IsEnum) {
                if (value.Is<string>())
                    result = (T)Enum.Parse(typeof(T), value.ToString());
                else if (value.GetType() == typeof(int))
                    result = (T)value;
                return result;
            }
            try {
                result = (T)Convert.ChangeType(value, typeof(T));
            } catch (System.Exception ex) {
                if (!value.GetType().GetInterface("IConvertible", true).IsNull() && !formatProvider.IsNull()) {
                    try {
                        result = (T)value.As<IConvertible>().ToType(typeof(T), formatProvider);
                    } catch {
                        throw new ApplicationException($"The type {typeof(T).Name}" +
                            $" is not convertable or does not implement IConvertable", ex);
                    }
                } else {
                    if (typeof(T) == typeof(bool?)) {
                        result = (T)(object)(((bool?)value).HasValue ? value.ToString().ToBool() : null);
                    } else if (typeof(T) == typeof(byte?)) {
                        result = (T)(object)(((byte?)value).HasValue ? value.ToString().ToByte() : null);
                    } else if (typeof(T) == typeof(DateTime?)) {
                        result = (T)(object)(((DateTime?)value).HasValue ? value.ToString().ToDateTime() : null);
                    } else if (typeof(T) == typeof(double?)) {
                        result = (T)(object)(((double?)value).HasValue ? value.ToString().ToDouble() : null);
                    } else if (typeof(T) == typeof(short?)) {
                        result = (T)(object)(((short?)value).HasValue ? value.ToString().ToInt16() : null);
                    } else if (typeof(T) == typeof(int?)) {
                        result = (T)(object)(((int?)value).HasValue ? value.ToString().ToInt32() : null);
                    } else if (typeof(T) == typeof(long?)) {
                        result = (T)(object)(((long?)value).HasValue ? value.ToString().ToInt64() : null);
                    } else if (typeof(T) == typeof(float?)) {
                        result = (T)(object)(((float?)value).HasValue ? value.ToString().ToFloat() : null);
                    } else {
                        result = (T)(object)(((float?)value)).ToString();
                    }
                }
            }
            return result;
        }

        [DebuggerStepThrough]
        public static bool ContainsKeyIgnoreCase<T>(this IDictionary<string, T> values, string testValue) =>
            values.Any(x => x.Key.EqualsIgnoreCase(testValue));

        public static string ExpandKey(this string value, List<KeyValuePair<string, string>> allValues, char valueIdKey) {
            if (value.IsNull() || allValues.Count == 0) {
                return value;
            }

            if (value.Contains(valueIdKey.ToString())) {
                value = value.Replace(valueIdKey.ToString(), string.Empty);
            }

            var result = allValues.FirstOrDefault(x => x.Key == value).Value;
            if (result.IsNull()) {
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

        [DebuggerStepThrough]
        public static string ExpandValue(this string value, List<KeyValuePair<string, string>> allValues, char valueIdKey) {
            if (value.IsNull() || allValues.Count == 0) {
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

        [DebuggerStepThrough]
        public static string GetDescription<T>(this Enum value) where T : Enum {
            var defaultValue = value.ToString();
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (!attributes.IsNull() && attributes.Length > 0) {
                return attributes[0].Description;
            } else {
                var attributes1 = (EnumMetaDataAttribute[])fi.GetCustomAttributes(typeof(EnumMetaDataAttribute), false);
                if (!attributes1.IsNull() && attributes1.Length > 0) {
                    return attributes1[0].Description;
                }
            }

            return defaultValue;
        }

        [DebuggerStepThrough]
        public static string GetPropertyName(this MethodBase value) => value.Name.Replace("set_", string.Empty);

        [DebuggerStepThrough]
        public static bool HasMethod(this Type type, string methodName) =>
            type.GetMethods().Any(x => x.Name == methodName);

        [DebuggerStepThrough]
        public static bool HasProperty(this Type value, string name, out PropertyInfo property) {
            property = value.GetProperty(name);
            return !property.IsNull();
        }

        [DebuggerStepThrough]
        public static List<PropertyInfo> GetFilteredProperties(this Type value, params string[] propertyNames) =>
            new(value.GetProperties().Where(x => propertyNames.Contains(x.Name)));

        [DebuggerStepThrough]
        public static List<PropertyInfo> GetFilteredProperties(this Type value, params Type[] propertyTypes) =>
            new(value.GetProperties().Where(x => propertyTypes.Contains(x.PropertyType)));

        [DebuggerStepThrough]
        public static bool Is<T>(this object item) where T : class => item is T;

        [DebuggerStepThrough]
        public static bool IsAfter(this DateTime value, DateTime theDate) => value > theDate;

        [DebuggerStepThrough]
        public static bool IsBefore(this DateTime value, DateTime theDate) => value < theDate;

        [DebuggerStepThrough]
        public static bool IsBetween<T>(this T value, T minValue, T maxValue, bool inclusive) where T : IComparable {
            if (inclusive) {
                return value.CompareTo(minValue) >= 0 && value.CompareTo(maxValue) <= 0;
            }

            return value.CompareTo(minValue) > 0 && value.CompareTo(maxValue) < 0;
        }

        [DebuggerStepThrough]
        public static bool IsFuture(this DateTime value) => value.IsAfter(DateTime.Now);

        [DebuggerStepThrough]
        public static bool IsPast(this DateTime value) => value.IsBefore(DateTime.Now);

        [DebuggerStepThrough]
        public static bool IsIn<T>(this T source, params T[] list) {
            if (null == source) {
                throw new ArgumentNullException(nameof(source));
            }

            return list.Contains(source);
        }

        [DebuggerStepThrough]
        public static bool IsNot<T>(this object item) where T : class => !item.Is<T>();

        [DebuggerStepThrough]
        public static string RemoveNonNumeric(this string value) {
            var chars = value.ToCharArray().Where(c => char.IsNumber(c)).ToArray();
            return new string(chars);
        }

        [DebuggerStepThrough]
        public static bool? ToBool(this string value) {
            if (value == "null" || value.IsNull()) {
                return null;
            }
            return bool.TryParse(value, out var t) ? (bool?)t : null;
        }

        [DebuggerStepThrough]
        public static byte? ToByte(this string value) {
            if (value == "null" || value.IsNull()) {
                return null;
            }

            return byte.TryParse(value, out var t) ? (byte?)t : null;
        }

        [DebuggerStepThrough]
        public static DateTime? ToDateTime(this string value) {
            if (value == "null" || value.IsNull()) {
                return null;
            }

            return DateTime.TryParse(value, out var t) ? (DateTime?)t : null;
        }

        [DebuggerStepThrough]
        public static double? ToDouble(this string value) {
            if (value == "null" || value.IsNull()) {
                return null;
            }

            return double.TryParse(value, out var t) ? (double?)t : null;
        }

        [DebuggerStepThrough]
        public static double? ToFloat(this string value) {
            if (value == "null" || value.IsNull()) {
                return null;
            }

            return float.TryParse(value, out var t) ? (float?)t : null;
        }

        [DebuggerStepThrough]
        public static short? ToInt16(this string value) {
            if (value == "null" || value.IsNull()) {
                return null;
            }

            return short.TryParse(value, out var t) ? (short?)t : null;
        }

        [DebuggerStepThrough]
        public static int? ToInt32(this string value) {
            if (value == "null" || value.IsNull()) {
                return null;
            }

            return int.TryParse(value, out var t) ? (int?)t : null;
        }

        [DebuggerStepThrough]
        public static long? ToInt64(this string value) {
            if (value == "null" || value.IsNull()) {
                return null;
            }

            return long.TryParse(value, out var t) ? (long?)t : null;
        }

        [DebuggerStepThrough]
        public static bool ContainsIgnoreCase(this IEnumerable<string> group, string value) =>
            group.Any(x => x.EqualsIgnoreCase(value));

        [DebuggerStepThrough]
        public static string ToPrecisionString<T>(this T value, ConversionType convertTo, bool includeCommas = true) where T : unmanaged, IComparable, IEquatable<T> {
            var result = value.CastTo<double>();
            var internalValue = new ByteSize(result);
            var suffix = default(string);
            var decimals = 0;
            switch (convertTo) {
                case ConversionType.KiloBytes:
                    result = internalValue.KiloBytes;
                    suffix = $" {KiloByteSymbol}";
                    decimals = 2;
                    break;
                case ConversionType.MegaBytes:
                    result = internalValue.MegaBytes;
                    suffix = $" {MegaByteSymbol}";
                    decimals = 2;
                    break;
                case ConversionType.GigaBytes:
                    result = internalValue.GigaBytes;
                    suffix = $" {GigaByteSymbol}";
                    decimals = 3;
                    break;
                case ConversionType.TeraBytes:
                    result = internalValue.TeraBytes;
                    suffix = $" {TeraByteSymbol}";
                    decimals = 4;
                    break;
                case ConversionType.PetaBytes:
                    result = internalValue.PetaBytes;
                    suffix = $" {PetaByteSymbol}";
                    decimals = 5;
                    break;
            }
            var format = includeCommas
                ? $"#,0.{new string('0', decimals)}{suffix}"
                : $"0.{new string('0', decimals)}{suffix}";
            return result.ToString(format);
        }

        [DebuggerStepThrough]
        public static Point ToPoint(this string value) => Point.Parse(value);

        [DebuggerStepThrough]
        public static Size ToSize(this string value) => Size.Parse(value);

        [DebuggerStepThrough]
        public static TimeSpan? ToTimeSpan(this string value) => TimeSpan.TryParse(value, out var t) ? (TimeSpan?)t : null;

        [DebuggerStepThrough]
        public static Version ToVersion(this string value) => Version.TryParse(value, out var t) ? t : null;

        [DebuggerStepThrough]
        public static WindowState ToWindowState(this string value) {
            var names = new List<string>(Enum.GetNames(typeof(WindowState)));
            return names.Contains(value) ? (WindowState)Enum.Parse(typeof(WindowState), value, true) : WindowState.Normal;
        }

        [DebuggerStepThrough]
        public static string AddText(this string theValue, string additionalText) => $"{theValue}{additionalText}";

        [DebuggerStepThrough]
        public static string RandomHexValue(this string prefix, int length = 8) =>
            new System.Random().Next().ToString("X").PadLeft(length, '0');

        [DebuggerStepThrough]
        public static Guid ToSpecializedGuid(this string value) {
            var appVersion = Assembly.GetEntryAssembly().GetName().Version;
            var rnd = new System.Random();
            var p = new List<byte>(System.Text.Encoding.ASCII.GetBytes(value.ToCharArray()));

            var i = 0;
            while (p.Count < 16) {
                var test = default(byte);
                switch (i) {
                    case 0:
                        test = Convert.ToByte(appVersion.Major);
                        break;
                    case 1:
                        test = Convert.ToByte(appVersion.Minor);
                        break;
                    case 2:
                        test = Convert.ToByte(appVersion.Build);
                        break;
                    case 3:
                        test = Convert.ToByte(appVersion.Revision);
                        break;
                    case 4:
                        test = Convert.ToByte(DateTime.Now.Day);
                        break;
                    case 5:
                        test = Convert.ToByte(DateTime.Now.Month);
                        break;
                    case 6:
                        var remaining = 16 - p.Count;
                        var bt = new byte[remaining];
                        rnd.NextBytes(bt);
                        p.AddRange(bt);
                        break;
                }
                if (p.Count + 1 < 16) {
                    p.Add(test);
                    i++;
                }
            }

            var values = new ReadOnlySpan<byte>(p.ToArray());
            return values.ToGuid();
        }
    }
}
