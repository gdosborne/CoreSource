using System;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml.Media;

namespace AppSystem.Primitives {
    public static class ExtensionMethods {
        public static T As<T>(this object value) where T : class => value as T;
        public static bool Is<T>(this object value) where T : class => value is T;
        public static T CastTo<T>(this object value) => value.CastTo<T>(default);

        public static T CastTo<T>(this object value, T defaultValue) => value.CastTo<T>(defaultValue, null);

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
            try {
                if (defaultValue is Enum)
                    return value is string ? (T)Enum.Parse(typeof(T), (string)value) : (T)value;
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (System.Exception ex) {
                var iFaces = value.GetType().GetInterfaces();

                if (iFaces.Any(x => x.Name == "IConvertible") && formatProvider != null) {
                    try {
                        result = (T)value.As<IConvertible>().ToType(typeof(T), formatProvider);
                    }
                    catch {
                        throw new Exception($"The type {typeof(T).Name} is not convertable or does not implement IConvertable", ex);
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
    }
}
