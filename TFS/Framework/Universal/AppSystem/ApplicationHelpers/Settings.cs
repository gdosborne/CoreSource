namespace AppSystem.ApplicationHelpers {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Windows.Storage;

	public static class Settings {
		public static readonly char BracketLeft = '[';
		public static readonly char BracketRight = ']';
		public static readonly string CommentIdentifier = "--";
		public static readonly List<SettingsSection> Sections = default;
		public static readonly char TypeLeft = '{';
		public static readonly char TypeRight = '}';
		static Settings() => Sections = new List<SettingsSection>();

		public static StorageFile GetStorageFile(string sectionName) {
			var roamingFolder = ApplicationData.Current.RoamingFolder;
			var allFolders = roamingFolder.GetFoldersAsync().AsTask().Result;
			var settingsFolder = default(StorageFolder);
			var sectionFile = default(StorageFile);
			if (!allFolders.Any(x => x.Name == "Settings")) {
				settingsFolder = roamingFolder.CreateFolderAsync("Settings").AsTask().Result;
			} else {
				settingsFolder = allFolders.FirstOrDefault(x => x.Name == "Settings");
			}
			var allFiles = settingsFolder.GetFilesAsync().AsTask().Result;
			if (!allFiles.Any(x => x.Name == $"{sectionName}.data")) {
				sectionFile = settingsFolder.CreateFileAsync($"{sectionName}.data", CreationCollisionOption.ReplaceExisting).GetResults();
			} else {
				sectionFile = allFiles.FirstOrDefault(x => x.Name == $"{sectionName}.data");
			}
			return sectionFile;
		}

		public static T GetValue<T>(string section, string key, T defaultValue = default) {
			var settingsSection = default(SettingsSection);

			if (!Sections.Any()) {
				return defaultValue;
			}
			settingsSection = Sections.FirstOrDefault(x => x.Name == section);
			if (settingsSection != null) {
				var item = settingsSection.Values.FirstOrDefault(x => x.Key == key);
				if (item != null) {
					return (T)item.Value;
				}
			}
			return defaultValue;
		}

		public static void LoadAllSettings(List<string> sectionNames = default) {
			try {
				var roamingFolder = ApplicationData.Current.RoamingFolder;
				var allFolders = roamingFolder.GetFoldersAsync().AsTask().Result;
				var settingsFolder = default(StorageFolder);
				if (!allFolders.Any(x => x.Name == "Settings")) {
					settingsFolder = roamingFolder.CreateFolderAsync("Settings").AsTask().Result;
				} else {
					settingsFolder = allFolders.FirstOrDefault(x => x.Name == "Settings");
				}
				if (sectionNames != null && sectionNames.Any()) {
					//read only these section names
					sectionNames.ForEach(sectionName => {
						Sections.Add(LoadFromFile(sectionName));
					});
				} else {
					//read what's in the folder
					var allFiles = settingsFolder.GetFilesAsync().AsTask().Result;
					allFiles.ToList().ForEach(file => {
						var sectionName = file.Name.Replace(".data", string.Empty);
						Sections.Add(LoadFromFile(sectionName));
					});
				}
			}
			catch (Exception) {
				return;
			}
		}

		public static void SetValue<T>(string section, string key, T value) {
			if (!Sections.Any()) {
				try {
					var settingsSection = new SettingsSection(section);
					Sections.Add(settingsSection);
				}
				catch (Exception) {
					///put stuff here
				}
			}
			var selectedSection = Sections.FirstOrDefault(x => x.Name == section);
			if (selectedSection != null) {
				var item = selectedSection.Values.FirstOrDefault(x => x.Key == key);
				if (item != null) {
					item.Value = value;
				} else {
					item = SettingsKey.GetKey(value.GetType(), key, value);
					selectedSection.Values.Add(item);
				}
				selectedSection.Save();

			}
		}

		private static SettingsSection LoadFromFile(string sectionName) {
			var sectionFile = GetStorageFile(sectionName);
			var data = FileIO.ReadTextAsync(sectionFile).AsTask().Result;
			var temp = new SettingsSection(sectionName);
			if (string.IsNullOrEmpty(data)) {
				data = $"{CommentIdentifier}{sectionName}.data created {DateTime.Now}{Environment.NewLine}";
			}
			var lines = new List<string>();
			using (var sr = new System.IO.StringReader(data)) {
				while (sr.Peek() > -1) {
					lines.Add(sr.ReadLine());
				}
			}
			foreach (var line in lines) {
				//starts with comment - ignore
				if (line.StartsWith(CommentIdentifier)) {
					continue;
				}
				//must start with left bracket - TrimStart gets rid of spaces and tabs
				if (!line.TrimStart().StartsWith(BracketLeft.ToString())) {
					continue;
				}
				var typeName = "System.String";
				var theType = typeof(string);
				var value = default(object);
				var lineTemp = line.Substring(1);
				var rightBracketPosition = lineTemp.IndexOf(BracketRight);
				var keyName = lineTemp.Substring(0, rightBracketPosition);
				lineTemp = lineTemp.Substring(rightBracketPosition + 1);

				var typeStartPosition = lineTemp.IndexOf(TypeLeft);
				if (typeStartPosition > -1) {
					var typeEndPosition = lineTemp.IndexOf(TypeRight, typeStartPosition + 1);
					typeName = lineTemp.Substring(typeStartPosition + 1, typeEndPosition - typeStartPosition - 1);
					theType = Type.GetType(typeName);
					if (typeEndPosition == lineTemp.Length - 1) {
						lineTemp = lineTemp.Substring(0, typeStartPosition);
					} else if (typeStartPosition == 0) {
						lineTemp = lineTemp.Substring(typeEndPosition + 1);
					}
					value = Convert.ChangeType(lineTemp, theType);
				} else {
					value = lineTemp;
				}

				var key = SettingsKey.GetKey(theType, keyName, value);
				temp.Values.Add(key);
			}
			return temp;
		}
	}
}
