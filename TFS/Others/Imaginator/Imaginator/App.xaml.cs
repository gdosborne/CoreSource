namespace Imaginator
{
	using System;
	using System.Collections.Generic;
	using System.Configuration;
	using System.Data;
	using System.IO;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Xml.Linq;
    using GregOsborne.Application.Primitives;

	public partial class App : Application
	{
		public App()
		{
            this.Startup += (object sender, StartupEventArgs e) =>
            {
                DataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Imaginator");
                if (!Directory.Exists(DataFolder))
                    Directory.CreateDirectory(DataFolder);
                ApplicationConfigFileName = Path.Combine(DataFolder, "configuration.xml");
                Settings = new Dictionary<string, object>();
                if (File.Exists(ApplicationConfigFileName))
                {
                    var doc = XDocument.Load(ApplicationConfigFileName);
                    var settingsRoot = doc.Root.Element("settings");
                    if (settingsRoot != null)
                    {
                        settingsRoot.Elements().ToList().ForEach(x =>
                        {
                            var type = Type.GetType(x.Attribute("type").Value);
                            var name = x.Attribute("key").Value;
                            var value = Convert.ChangeType(x.Attribute("value").Value, type);
                            Settings.Add(name, value);
                        });
                    }
                }
            };
        }
        
		public static void SetSetting<T>(string name, T value)
		{
			XDocument doc = null;
			var mustAddRoot = false;
			var mustSave = false;
			if (!File.Exists(ApplicationConfigFileName))
			{
				doc = new XDocument(new XElement("config",
					new XElement("settings")));
				doc.Save(ApplicationConfigFileName);
			}
			else
				doc = XDocument.Load(ApplicationConfigFileName);
			var settingsRoot = doc.Root.Element("settings");
			if (settingsRoot == null)
			{
				mustAddRoot = true;
				mustSave = true;
				settingsRoot = new XElement("settings");
			}
			if (settingsRoot != null)
			{
				var settingElement = settingsRoot.Elements().FirstOrDefault(x => x.Attribute("key").Value.Equals(name));
				if (settingElement != null)
					settingElement.Attribute("value").Value = (string)Convert.ChangeType(value, typeof(string));
				else
					settingsRoot.Add(
						new XElement("setting",
							new XAttribute("key", name),
							new XAttribute("value", Convert.ChangeType(value, typeof(string))),
							new XAttribute("type", typeof(T).ToString())));
				mustSave = true;
			}
			if (mustAddRoot)
				doc.Root.Add(settingsRoot);
            if (mustSave)
				doc.Save(ApplicationConfigFileName);

			if (Settings.ContainsKey(name))
				Settings[name] = value;
			else
				Settings.Add(name, value);
		}
		public static T GetSetting<T>(string name, T defaultValue)
		{
			if (Settings.ContainsKey(name))
				return (T)Settings[name];
			return defaultValue;
		}
		public static string DataFolder { get; private set; }
		public static string ApplicationConfigFileName { get; private set; }
		public static Dictionary<string, object> Settings { get; private set; }
	}
}
