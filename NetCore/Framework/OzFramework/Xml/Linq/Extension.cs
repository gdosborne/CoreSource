/* File="Extension"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using OzFramework.Primitives;
using OzFramework.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace OzFramework.Xml.Linq {
    public static class Extension {
        public static void AddOrUpdateElement(this XElement parent, string name, object value, bool isCData = false) {
            if (parent.IsNull()) {
                throw new ArgumentException("Parent is required");
            } else if (name.IsNull()) {
                throw new ArgumentException("Name is required");
            }
            if (parent.ElementExists(name)) {
                parent.Element(name).UpdateValue(value, isCData);
            } else {
                var item = new XElement(name);
                item.UpdateValue(value);
                parent.Add(item);
            }
        }

        public static void AddOrUpdateAttribute(this XElement parent, string name, object value) {
            if (parent.IsNull()) {
                throw new ArgumentException("Parent is required");
            } else if (name.IsNull()) {
                throw new ArgumentException("Name is required");
            }
            if (parent.AttributeExists(name)) {
                parent.Attribute(name).UpdateValue(value);
            } else {
                var item = new XAttribute(name, string.Empty);
                item.UpdateValue(value);
                parent.Add(item);
            }
        }

        public static T GetAttributeValue<T>(this IEnumerable<XAttribute> value, string name) {
            foreach (var item in value) {
                if (item.LocalName().EqualsIgnoreCase(name)) {
                    if (item.Value.IsNull())
                        return default;
                    return (T)Convert.ChangeType(item.Value, typeof(T).IsEnum ? typeof(int) : typeof(T));
                }
            }
            return default;
        }

        public static void UpdateValue(this XElement element, object value, bool isCData = false) {
            if (value.IsNull()) {
                element.Value = string.Empty;
            } else {
                if (isCData) {
                    element.Add(new XCData(value.ToString()));
                } else {
                    element.Value = value.ToString();
                }
            }
        }

        public static void UpdateValue(this XAttribute attribute, object value) {
            if (value.IsNull()) {
                attribute.Value = string.Empty;
            } else {
                attribute.Value = value.ToString();
            }
        }

        public static XElement GetElementByName(this IEnumerable<XElement> value, string name, bool ignoreCase = true) {
            var casing = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            var result = value.FirstOrDefault(x => x.LocalName().Equals(name, casing));
            return result;
        }

        public static T GetElementValue<T>(this IEnumerable<XElement> value, string name) {
            foreach (var item in value) {
                if (item.LocalName().EqualsIgnoreCase(name)) {
                    if (typeof(T) == typeof(Guid)) {
                        if (Guid.TryParse(item.Value, out var id)) {
                            return (T)(object)id;
                        }
                        return default(T);
                    } else if (typeof(T).IsNullableType()) {
                        if (item.Value.IsNull()) {
                            return default(T);
                        } else {
                            var underlyingType = Nullable.GetUnderlyingType(typeof(T));
                            return (T)Convert.ChangeType(item.Value, underlyingType);
                        }
                    } else {
                        return item.Value.IsNull()
                            ? default
                            : (T)Convert.ChangeType(item.Value, typeof(T).IsEnum ? typeof(int) : typeof(T));
                    }
                }
            }
            return default(T);
        }

        public static object GetElementValue(this IEnumerable<XElement> value, Type type, string name) {
            foreach (var item in value) {
                if (item.LocalName().EqualsIgnoreCase(name)) {
                    if (type == typeof(Guid)) {
                        if (Guid.TryParse(item.Value, out var id)) {
                            return id;
                        }
                        return null;
                    } else if (type.IsNullableType()) {
                        if (item.Value.IsNull()) {
                            return null;
                        } else {
                            var underlyingType = Nullable.GetUnderlyingType(type);
                            return Convert.ChangeType(item.Value, underlyingType);
                        }
                    } else {
                        return item.Value.IsNull()
                            ? default
                            : Convert.ChangeType(item.Value, type);
                    }
                }
            }
            return null;
        }

        public static XDocument GetXDocument(this string fileName) {
            if (File.Exists(fileName)) {
                try {
                    File.SetAttributes(fileName, File.GetAttributes(fileName) & ~FileAttributes.ReadOnly);
                    return XDocument.Load(fileName);
                } catch (System.Exception ex) { Console.WriteLine(ex.Message); }
            }
            return null;
        }

        public static bool AttributeExists(this XElement value, string name) => !value.Attribute(name).IsNull();

        public static bool ElementExists(this XElement value, string name) => !value.Element(name).IsNull();

        public static bool HasValue(this XAttribute value) => !value.IsNull() && !value.Value.IsNull();

        public static bool HasValue(this XElement value) => !value.IsNull() && !value.Value.IsNull();

        public static string LocalName(this XAttribute value) => value.Name.LocalName;

        public static string LocalName(this XElement value) => value.Name.LocalName;

        public static bool TryXElementParse(this string value, out XElement outValue) {
            outValue = null;
            try {
                outValue = XElement.Parse(value);
                return true;
            } catch { }
            return false;
        }
    }
}

