namespace GregOsborne.RoamSync.Tray {
	using System;
	using System.Collections.Generic;
	using System.Configuration;
	using System.Data;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Windows;
	using GregOsborne.Application;
	using GregOsborne.Application.Windows.Dialog;

	public partial class App : System.Windows.Application {
        public Session SyncRoamingSession { get; private set; } = default;
		public string ApplicationName { get; private set; } = "Synchronize Roaming Folder";

		protected override void OnStartup(StartupEventArgs e) {
			this.SyncRoamingSession = new Session(this.ApplicationName, GregOsborne.Application.Logging.ApplicationLogger.StorageTypes.CsvFile, GregOsborne.Application.Logging.ApplicationLogger.StorageOptions.CreateFolderForEachDay);
		}

		protected override void OnExit(ExitEventArgs e) {
			
		}
	}
}
