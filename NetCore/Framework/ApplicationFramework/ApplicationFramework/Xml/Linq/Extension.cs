using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Common.Applicationn.Xml.Linq {
    public static class Extension {
        public static void AddOrUpdateElement(this XElement parent, string name, object value, bool isCData = false) {
            if (parent == null) {
                throw new ArgumentException("Parent is required");
            }
            else if (string.IsNullOrEmpty(name)) {
                throw new ArgumentException("Name is required");
            }
            if (parent.ElementExists(name)) {
                parent.Element(name).UpdateValue(value, isCData);
            }
            else {
                var item = new XElement(name);
                item.UpdateValue(value);
                parent.Add(item);
            }
        }

        public static void AddOrUpdateAttribute(this XElement parent, string name, object value) {
            if (parent == null) {
                throw new ArgumentException("Parent is required");
            }
            else if (string.IsNullOrEmpty(name)) {
                throw new ArgumentException("Name is required");
            }
            if (parent.AttributeExists(name)) {
                parent.Attribute(name).UpdateValue(value);
            }
            else {
                var item = new XAttribute(name, string.Empty);
                item.UpdateValue(value);
                parent.Add(item);
            }
        }

        public static T GetAttributeValue<T>(this IEnumerable<XAttribute> value, string name) {
            foreach (var item in value) {
                if (item.LocalName().Equals(name, System.StringComparison.OrdinalIgnoreCase)) {
                    if (string.IsNullOrEmpty(item.Value))
                        return default;
                    return (T)Convert.ChangeType(item.Value, typeof(T).IsEnum ? typeof(int) : typeof(T));
                }
            }
            return default;
        }

        public static void UpdateValue(this XElement element, object value, bool isCData = false) {
            if (value == null) {
                element.Value = string.Empty;
            }
            else {
                if (isCData) {
                    element.Add(new XCData(value.ToString()));
                }
                else {
                    element.Value = value.ToString();
                }
            }
        }

        public static void UpdateValue(this XAttribute attribute, object value) {
            if (value == null) {
                attribute.Value = string.Empty;
            }
            else {
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
                if (item.LocalName().Equals(name, System.StringComparison.OrdinalIgnoreCase)) {
                    return string.IsNullOrEmpty(item.Value) 
                        ? default 
                        : (T)Convert.ChangeType(item.Value, typeof(T).IsEnum ? typeof(int) : typeof(T));
                }
            }
            return default;
        }

        public static XDocument GetXDocument(this string fileName) {
            if (File.Exists(fileName)) {
                try {
                    File.SetAttributes(fileName, File.GetAttributes(fileName) & ~FileAttributes.ReadOnly);
                    return XDocument.Load(fileName);
                }
                catch (System.Exception ex) { Console.WriteLine(ex.Message); }
            }
            return null;
        }

        public static bool AttributeExists(this XElement value, string name) => value.Attribute(name) != null;

        public static bool ElementExists(this XElement value, string name) => value.Element(name) != null;

        public static bool HasValue(this XAttribute value) => value != null && !string.IsNullOrEmpty(value.Value);

        public static bool HasValue(this XElement value) => value != null && !string.IsNullOrEmpty(value.Value);

        public static string LocalName(this XAttribute value) => value.Name.LocalName;

        public static string LocalName(this XElement value) => value.Name.LocalName;

        public static bool TryXElementParse(this string value, out XElement outValue) {
            outValue = null;
            try {
                outValue = XElement.Parse(value);
                return true;
            }
            catch { }
            return false;
        }
    }
}

