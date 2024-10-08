using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
namespace Configuration
{
	public class ConfigurationManager
	{
		public ConfigurationManager(string configurationFileName)
		{
			ConfigurationFileName = configurationFileName;
		}
		public string ConfigurationFileName { get; private set; }
		public T GetSetting<T>(string name)
		{
			InitFile();
			T result = default(T);
			var doc = XDocument.Load(ConfigurationFileName);
			var root = doc.Root;
			var element = root.Elements().FirstOrDefault(x => x.Attribute("name").Value == name);
			if (element != null)
				result = (T)Convert.ChangeType(element.Attribute("value").Value, typeof(T));
			return result;
		}
		public void SetSetting(string name, object value)
		{
			InitFile();
			var doc = XDocument.Load(ConfigurationFileName);
			var root = doc.Root;
			var element = root.Elements().FirstOrDefault(x => x.Attribute("name").Value == name);
			if (element != null)
				element.Attribute("value").Value = Convert.ToString(value);
			else
			{
				root.Add(new XElement("setting", 
					new XAttribute("name", name),
					new XAttribute("type", value.GetType().FullName), 
					new XAttribute("value", Convert.ToString(value))));
			}
			doc.Save(ConfigurationFileName);
		}
		public Dictionary<string, object> GetAll()
		{
			var result = new Dictionary<string, object>();
			InitFile();
			var doc = XDocument.Load(ConfigurationFileName);
			var root = doc.Root;
			root.Elements().ToList().ForEach(x =>
			{
				var name = x.Attribute("name").Value;
				var value = x.Attribute("value").Value;
				var type = x.Attribute("type").Value;
				result.Add(name, Convert.ChangeType(value, Type.GetType(type)));
			});
			return result;
		}
		private void InitFile()
		{
			if(!System.IO.File.Exists(ConfigurationFileName))
			{
				var doc = new XDocument(new XElement("settings"));
				doc.Save(ConfigurationFileName);
			}
		}
	}
}
