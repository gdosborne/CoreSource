using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace Dbq
{
	public partial class App : Application
	{
		private static string GetSettingsFileName()
		{
			var settingsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Dbq");
			if (!Directory.Exists(settingsFolder))
				Directory.CreateDirectory(settingsFolder);
			settingsFolder = Path.Combine(settingsFolder, "Settings");
			if (!Directory.Exists(settingsFolder))
				Directory.CreateDirectory(settingsFolder);
			var result = Path.Combine(settingsFolder, "applicationSettings.xml");
			if (!File.Exists(result))
			{
				var xDoc = new XDocument(new XElement("appSettings"));
				xDoc.Save(result);
			}
			return result;
		}
		private static XElement GetValueElement(XElement sectionRoot, string name)
		{
			return sectionRoot.Elements().ToList().FirstOrDefault(x => x.Name.LocalName.Equals("value") && x.Attribute("name") != null && x.Attribute("name").Value.Equals(name));
		}
		private static XElement GetSectionElement(XElement root, string name)
		{
			return root.Elements().ToList().FirstOrDefault(x => x.Name.LocalName.Equals("section") && x.Attribute("name") != null && x.Attribute("name").Value.Equals(name));
		}
		
		public static Stack<string> AddMRUFile(string fileName)
		{
			var xmlFileName = GetSettingsFileName();
			var current = GetMRUFiles();
			var count = 0;
			if (current.Contains(fileName, StringComparer.Create(CultureInfo.CurrentCulture, true)))
			{
				var temp = new Stack<string>();
				while (current.Any())
				{
					var item = current.Pop();
					if (!item.Equals(fileName, StringComparison.OrdinalIgnoreCase) && count < 19)
					{
						temp.Push(item);
						count++;
					}
				}
				current = temp;
			}
			current.Push(fileName);
			count = 0;
			while (current.Any())
			{
				if (count > 19)
					break;
				var item = current.Pop();
				SetSettingValue<string>("MRUFiles", count.ToString(), item);
				count++;
			}
			return current;
		}
		public static Stack<string> GetMRUFiles()
		{
			var result = new Stack<string>();
			var fileName = GetSettingsFileName();
			var xDoc = XDocument.Load(fileName);
			var root = xDoc.Root;
			var sectionElement = GetSectionElement(root, "MRUFiles");
			if (sectionElement == null)
				sectionElement = new XElement("section", new XAttribute("name", "MRUFiles"));
			sectionElement.Elements().ToList().ForEach(x => result.Push(x.Attribute("value").Value));
			return result;
		}
		public static T GetSettingValue<T>(string section, string name, T defaultValue)
		{
			var fileName = GetSettingsFileName();
			var xDoc = XDocument.Load(fileName);
			var root = xDoc.Root;
			var sectionElement = GetSectionElement(root, section);
			if (sectionElement == null)
				sectionElement = new XElement("section", new XAttribute("name", section));
			var valueElement = GetValueElement(sectionElement, name);
			if (valueElement == null)
				valueElement = new XElement("value", new XAttribute("name", name), new XAttribute("value", defaultValue.ToString()));
			if (typeof(T) == typeof(Size))
			{
				var t = Size.Parse(valueElement.Attribute("value").Value);
				return (T)(object)t;
			}
			else if (typeof(T) == typeof(Point))
			{
				var t = Point.Parse(valueElement.Attribute("value").Value);
				return (T)(object)t;
			}
			else if (typeof(T) == typeof(WindowState))
			{
				var t = (WindowState)Enum.Parse(typeof(WindowState), valueElement.Attribute("value").Value, true);
				return (T)(object)t;
			}
			else if (typeof(T) == typeof(decimal) || typeof(T) == typeof(double) || typeof(T) == typeof(float) || typeof(T) == typeof(Single) || typeof(T) == typeof(Double) || typeof(T) == typeof(Decimal))
			{
				var t = decimal.Parse(valueElement.Attribute("value").Value);
				return (T)(object)t;
			}
			else if (typeof(T) == typeof(int) || typeof(T) == typeof(Int32) || typeof(T) == typeof(long) || typeof(T) == typeof(Int64) || typeof(T) == typeof(short) || typeof(T) == typeof(Int16) || typeof(T) == typeof(byte))
			{
				var t = Int64.Parse(valueElement.Attribute("value").Value);
				return (T)(object)t;
			}
			else if (typeof(T) == typeof(bool) || typeof(T) == typeof(Boolean))
			{
				var t = bool.Parse(valueElement.Attribute("value").Value);
				return (T)(object)t;
			}
			else if (typeof(T) == typeof(string))
			{
				return (T)(object)valueElement.Attribute("value").Value;
			}
			return (T)(object)null;
		}
		public static void SetSettingValue<T>(string section, string name, T value)
		{
			var fileName = GetSettingsFileName();
			var xDoc = XDocument.Load(fileName);
			var root = xDoc.Root;
			var sectionElement = GetSectionElement(root, section);
			if (sectionElement == null)
			{
				sectionElement = new XElement("section", new XAttribute("name", section));
				root.Add(sectionElement);
			}
			var valueElement = GetValueElement(sectionElement, name);
			if (valueElement == null)
			{
				valueElement = new XElement("value", new XAttribute("name", name), new XAttribute("value", value.ToString()));
				sectionElement.Add(valueElement);
			}
			else
				valueElement.Attribute("value").Value = value.ToString();
			xDoc.Save(fileName);
		}
	}
}
