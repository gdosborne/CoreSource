namespace OSoftComponents
{
	using Microsoft.Win32;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Xml.Linq;

	public enum SettingsLocations
	{
		Registry,
		AppData
	}

	public sealed class AppSettings
	{
		#region Public Constructors
		public AppSettings(SettingsLocations location, string applicationName)
		{
			ApplicationName = applicationName;
			Location = location;
		}
		#endregion Public Constructors

		#region Public Methods
		public T GetSetting<T>(string sectionName, string keyName, T defaultValue) where T : IConvertible
		{
			if (string.IsNullOrEmpty(ApplicationName) || string.IsNullOrEmpty(sectionName) || string.IsNullOrEmpty(keyName))
				throw new ApplicationException("Some values required to set and get application settings are missing");

			if (Location == SettingsLocations.Registry)
			{
				var key = Registry.CurrentUser.OpenSubKey(@"Software", true);
				key = key.CreateSubKey(ApplicationName);
				key = key.CreateSubKey(sectionName);
				var valString = key.GetValue(keyName, defaultValue.ToString());
				return typeof(T) == typeof(string) ? (T)valString : (T)Convert.ChangeType(valString, typeof(T));
			}
			else
			{
				var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ApplicationName);
				if (!Directory.Exists(path))
					Directory.CreateDirectory(path);
				var fileName = Path.Combine(path, "settings.xml");
				if (!File.Exists(fileName))
					return defaultValue;
				var doc = XDocument.Load(fileName);
				var sectionElement = doc.Root.Elements().FirstOrDefault(x => x.Name.LocalName == "section" && x.Attribute("name").Value == sectionName);
				if (sectionElement == null)
					return defaultValue;
				var valueElement = sectionElement.Elements().FirstOrDefault(x => x.Name.LocalName == "value" && x.Attribute("key").Value == keyName);
				if (valueElement == null)
					return defaultValue;
				return (T)Convert.ChangeType(valueElement.Attribute("value").Value, typeof(T));
			}
		}
		public void SetSetting<T>(string sectionName, string keyName, T value) where T : IConvertible
		{
			if (string.IsNullOrEmpty(ApplicationName) || string.IsNullOrEmpty(sectionName) || string.IsNullOrEmpty(keyName))
				throw new ApplicationException("Some values required to set and get application settings are missing");

			if (Location == SettingsLocations.Registry)
			{
				var key = Registry.CurrentUser.OpenSubKey(@"Software", true);
				key = key.CreateSubKey(ApplicationName);
				key = key.CreateSubKey(sectionName);
				key.SetValue(keyName, value.ToString());
			}
			else
			{
				var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ApplicationName);
				if (!Directory.Exists(path))
					Directory.CreateDirectory(path);
				var fileName = Path.Combine(path, "settings.xml");
				XDocument doc = null;
				if (!File.Exists(fileName))
				{
					doc = new XDocument(new XElement("sections",
						new XElement("section",
							new XAttribute("name", sectionName))));
				}
				else
					doc = XDocument.Load(fileName);
				var sectionElement = doc.Root.Elements().FirstOrDefault(x => x.Name.LocalName == "section" && x.Attribute("name").Value == sectionName);
				var valueElement = sectionElement.Elements().FirstOrDefault(x => x.Name.LocalName == "value" && x.Attribute("key").Value == keyName);
				if (valueElement == null)
					sectionElement.Add(new XElement("value",
						new XAttribute("key", keyName),
						new XAttribute("value", value.ToString())));
				else
					valueElement.Attribute("value").Value = value.ToString();
				doc.Save(fileName);
			}
		}
		#endregion Public Methods

		#region Public Properties
		public string ApplicationName { get; private set; }
		public SettingsLocations Location { get; private set; }
		#endregion Public Properties
	}
}
