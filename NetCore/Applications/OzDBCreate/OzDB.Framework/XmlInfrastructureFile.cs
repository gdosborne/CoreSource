namespace OzDB.Application {
	using System;
	using System.IO;

	public abstract class XmlInfrastructureFile {
		public enum FileTypes {
			Settings,
			Logs
		}

		public string ApplicationName {
			get; protected set;
		}

		public string ActualFileName {
			get; protected set;
		}

		public bool SettingsFileExists => !string.IsNullOrEmpty(this.ActualFileName) && File.Exists(this.ActualFileName);

		protected static bool DoesFileNameExist(string settingsDirectory, FileTypes type) => File.Exists(GetFileName(settingsDirectory, type));

		protected static string GetFileName(string settingsDirectory, FileTypes type) {
			if (!Directory.Exists(settingsDirectory)) {
				Directory.CreateDirectory(settingsDirectory);
			}

			return Path.Combine(settingsDirectory, $"{type}.xml");
		}
	}
}
