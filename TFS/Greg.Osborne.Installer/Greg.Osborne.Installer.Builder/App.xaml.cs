namespace Greg.Osborne.Installer.Builder {
	using System;
	using System.IO;
	using System.Windows;
	using GregOsborne.Application;

	public partial class App : Application {

		public string ApplicationName { get; private set; } = "Installer Controller Builder";
		public DirectoryInfo ApplicationDirectory { get; private set; } = default;

		private Random randomizer = new Random();

		protected override void OnStartup(StartupEventArgs e) {
			var appDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), this.ApplicationName);
			this.ApplicationDirectory = new DirectoryInfo(appDir);
			if (!this.ApplicationDirectory.Exists) {
				this.ApplicationDirectory.Create();
			}

			Settings.SettingsFileName = Path.Combine(appDir, "settings.xml");

		}

		public string GetTempFileName() {
			var dt = DateTime.Parse("1/1/1990").Date;
			var currentDateTime = DateTime.Now;
			var daysFrom = currentDateTime.Date.Subtract(dt).Days;
			var minutesFrom = daysFrom * (24 * 60);
			//var minutesFrom = daysFrom * ((24 + currentDateTime.Hour * 60) + currentDateTime.Minute;
			var index = 0;
			var fileRoot = $"{minutesFrom}-{index}.tmp";
			var filename = Path.Combine(ApplicationDirectory.FullName, fileRoot);
			while(string.IsNullOrEmpty(filename) || File.Exists(filename)) {
				index++;
				fileRoot = $"{minutesFrom}-{index}.tmp";
				filename = Path.Combine(ApplicationDirectory.FullName, fileRoot);
			}
			return filename;
		}
	}
}
