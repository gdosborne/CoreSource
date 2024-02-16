namespace GregOsborne.Application.Xml.Linq {
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Xml.Linq;

	public static class Extension {
		public static string GetAttributeValue(this IEnumerable<XAttribute> value, string name) {
			foreach (var item in value) {
				if (item.LocalName().Equals(name, System.StringComparison.OrdinalIgnoreCase)) {
					return item.Value;
				}
			}
			return default;
		}

		public static XElement GetElementByName(this IEnumerable<XElement> value, string name, bool ignoreCase = true) {
			var casing = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
			var result = value.FirstOrDefault(x => x.LocalName().Equals(name, casing));
			return result;
		}

		public static string GetElementValue(this IEnumerable<XElement> value, string name) {
			foreach (var item in value) {
				if (item.LocalName().Equals(name, System.StringComparison.OrdinalIgnoreCase)) {
					return item.Value;
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
				catch (Exception ex) { Console.WriteLine(ex.Message); }
			}
			return null;
		}

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

