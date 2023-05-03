namespace GregOsborne.Application
{
	using System;
	using System.Linq;
	using System.Windows;
	using System.Xml.Linq;

	public static class Settings
	{
		public static T GetSetting<T>(string applicationName, string sectionName, string settingName, T defaultValue)
		{
			var fileName = GetSettingsFileName(applicationName, true);
            var canOpenFile = false;
            XDocument xDoc = null;
            while (!canOpenFile) {
                try {
                    xDoc = XDocument.Load(fileName);
                    canOpenFile = true;
                } catch (System.Exception) {
                    canOpenFile = false;
                    System.Threading.Thread.Sleep(100);
                }
            }
			var root = xDoc.Root;
		    if (root == null) return defaultValue;
            var sectionXElement = root.Elements().FirstOrDefault(x => {
                var attribute = x.Attribute("name");
                return attribute != null && (x.Name.LocalName.Equals("section") && attribute.Value.Equals(sectionName));
            });
		    if (sectionXElement == null) return defaultValue;
		    var settingXElement = sectionXElement.Elements().FirstOrDefault(x => {
		        var attribute = x.Attribute("name");
		        return attribute != null && (x.Name.LocalName.Equals("value") && attribute.Value.Equals(settingName));
		    });
		    if (settingXElement == null) return defaultValue;
		    var xAttribute = settingXElement.Attribute("value");
		    if (xAttribute == null) return defaultValue;
		    var value = xAttribute.Value;
		    if (typeof(T) == typeof(Point))
		        return (T)(object)Point.Parse(value);
		    if (typeof(T) == typeof(Size))
		        return (T)(object)Size.Parse(value);
		    if (typeof(T) == typeof(Decimal) || typeof(T) == typeof(decimal) || typeof(T) == typeof(float) || typeof(T) == typeof(Single) || typeof(T) == typeof(Double) || typeof(T) == typeof(double))
		        return (T)Convert.ChangeType(value, typeof(T));
		    if (typeof(T) == typeof(Int64) || typeof(T) == typeof(long) || typeof(T) == typeof(Int32) || typeof(T) == typeof(int) || typeof(T) == typeof(Int16) || typeof(T) == typeof(short) || typeof(T) == typeof(byte))
		        return (T)(object)Convert.ChangeType(value, typeof(T));
		    if (typeof(T) == typeof(bool) || typeof(T) == typeof(Boolean))
		        return (T)(object)bool.Parse(value);
		    if (typeof(T) == typeof(WindowState))
		        return (T)(object)(WindowState)Enum.Parse(typeof(WindowState), value, true);
		    if (typeof(T) == typeof(string) || typeof(T) == typeof(String))
		        return (T)(object)value;
		    return (T)(object)null;
		}

		public static void SetSetting<T>(string applicationName, string sectionName, string settingName, T value)
		{
			var fileName = GetSettingsFileName(applicationName, true);
            var canOpenFile = false;
            XDocument xDoc = null;
            while (!canOpenFile) {
                try {
                    xDoc = XDocument.Load(fileName);
                    canOpenFile = true;
                } catch (System.Exception) {
                    canOpenFile = false;
                    System.Threading.Thread.Sleep(100);
                }
            }
            var root = xDoc.Root;
		    if (root != null) {
		        var sectionXElement = root.Elements().FirstOrDefault(x => {
		            var xAttribute = x.Attribute("name");
		            return xAttribute != null && (x.Name.LocalName.Equals("section") && xAttribute.Value.Equals(sectionName));
		        });
		        if (sectionXElement == null)
		        {
		            sectionXElement = new XElement("section", new XAttribute("name", sectionName));
		            root.Add(sectionXElement);
		        }
		        var settingXElement = sectionXElement.Elements().FirstOrDefault(x => {
		            var xAttribute = x.Attribute("name");
		            return xAttribute != null && (x.Name.LocalName.Equals("value") && xAttribute.Value.Equals(settingName));
		        });
		        if (settingXElement == null)
		        {
		            settingXElement = new XElement("value", new XAttribute("name", settingName), new XAttribute("value", value.ToString()));
		            sectionXElement.Add(settingXElement);
		        }
		        else {
		            var xAttribute = settingXElement.Attribute("value");
		            if (xAttribute != null) xAttribute.Value = value.ToString();
		        }
		    }
		    xDoc.Save(fileName);
		}

		private static string GetSettingsFileName(string applicationName, bool isRoaming = false)
		{
			var settingsFolder = isRoaming
				? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
				: Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			settingsFolder = System.IO.Path.Combine(settingsFolder, applicationName);
			if (!System.IO.Directory.Exists(settingsFolder))
				System.IO.Directory.CreateDirectory(settingsFolder);
			var result = System.IO.Path.Combine(settingsFolder, "settings.xml");
		    if (System.IO.File.Exists(result)) return result;
		    var xDoc = new XDocument(new XElement("appSettings"));
		    xDoc.Save(result);
		    return result;
		}
	}
}
