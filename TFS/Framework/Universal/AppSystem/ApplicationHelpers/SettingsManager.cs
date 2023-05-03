namespace AppSystem.ApplicationHelpers {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Xml.Linq;
	using AppSystem.Primitives;
	using Windows.Storage;

	public class SettingsManager {
		public SettingsManager() => this.sections = new Dictionary<string, XDocument>();

		private StorageFolder settingsFolder = default;
		private readonly Dictionary<string, XDocument> sections = default;

		private XDocument GetXDocument(string sectionName) {
			var result = default(XDocument);
			if (!this.sections.ContainsKey(sectionName)) {
				if (this.settingsFolder == null) {
					var roamingFolder = ApplicationData.Current.RoamingFolder;
					this.settingsFolder = this.ValidateSettingsFolder(roamingFolder);
				}
				StorageFile sectionFile = default;
				try {
					sectionFile = this.settingsFolder.GetFileAsync($"{sectionName}.xml").AsTask().Result;
					var data = FileIO.ReadTextAsync(sectionFile).AsTask().Result;
					result = XDocument.Parse(data);
					if (!this.sections.Any(x => x.Key == sectionName)) {
						this.sections.Add(sectionName, result);
					}
					result = this.sections[sectionName];
				}
				catch {
					result = this.CreateNewSectionFile(sectionName);
				}
			} else {
				result = this.sections[sectionName];
			}
			return result;
		}

		private XDocument CreateNewSectionFile(string sectionName) {
			var result = new XDocument(
				new XElement("settings"));
			var sectionFile = this.settingsFolder.CreateFileAsync($"{sectionName}.xml").AsTask().Result;
			FileIO.WriteTextAsync(sectionFile, result.ToString()).AsTask();
			this.sections.Add(sectionName, result);
			return result;
		}

		public void SetValue<T>(string sectionName, string keyword, T value) {
			var doc = this.GetXDocument(sectionName);
			if (doc == null) {
				doc = this.CreateNewSectionFile(sectionName);
			}
			var element = doc.Root.Elements().FirstOrDefault(x => x.Attribute("name").Value == keyword);
			if (element != null) {
				element.Attribute("value").Value = value.ToString();
			} else {
				element = new XElement("setting",
					new XAttribute("name", keyword),
					new XAttribute("value", value.ToString()));
				doc.Root.Add(element);
			}
			var sectionFile = this.settingsFolder.GetFileAsync($"{sectionName}.xml").AsTask().Result;
			this.sections[sectionName] = doc;
			FileIO.WriteTextAsync(sectionFile, doc.ToString()).AsTask();
		}

		public T GetValue<T>(string sectionName, string keyword, T defaultValue = default) {
			var doc = this.GetXDocument(sectionName);
			if (doc == null) {
				return defaultValue;
			}

			var element = doc.Root.Elements().FirstOrDefault(x => x.Attribute("name").Value == keyword);
			if (element == null) {
				return defaultValue;
			}
			var value = element.Attribute("value").Value;
			if (string.IsNullOrEmpty(value)) {
				return defaultValue;
			}
			return value.CastTo<T>(defaultValue);
		}

		private StorageFolder ValidateSettingsFolder(StorageFolder folder) {
			StorageFolder result = default;
			try {
				result = folder.GetFolderAsync("Settings").AsTask().Result;
			}
			catch {
				result = folder.CreateFolderAsync("Settings").AsTask().Result;
			}
			return result;
		}
	}
}
