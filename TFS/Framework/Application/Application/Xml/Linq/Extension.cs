using System;
using System.IO;
using System.Xml.Linq;
using GregOsborne.Application.Primitives;

namespace GregOsborne.Application.Xml.Linq {
	public static class Extension {
		public static XDocument GetXDocument(this string fileName) => !File.Exists(fileName) ? null : XDocument.Load(fileName);

		public static T GetElementValue<T>(this XElement element, T defaultValue) {
			if (element == null) {
				return defaultValue;
			}

			var value = element.Value;
			if (string.IsNullOrEmpty(value)) {
				return defaultValue;
			}

			return value.CastTo<T>();
		}

		public static T GetAttributeValue<T>(this XElement element, string name, T defaultValue) {
			if (element == null) {
				return defaultValue;
			}

			var attr = element.Attribute(name);
			if (attr == null)
				return defaultValue;

			var value = attr.Value;
			if (string.IsNullOrEmpty(value)) {
				return defaultValue;
			}

			return value.CastTo<T>();
		}
	}
}