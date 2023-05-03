using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greg.Osborne.Installer.Support.ApplicationUpdates {
	public class Updater {
		public Updater(string applicationName, Version currentVersion, string updateDirectory, string controllerFileName) {
			this.ApplicationName = applicationName;
			this.CurrentVersion = currentVersion;
			this.UpdateDirectory = updateDirectory;
			this.ControllerFileName = controllerFileName;
		}

		public string ApplicationName { get; private set; } = default;

		public Version CurrentVersion { get; private set; } = default;

		public string UpdateDirectory { get; private set; } = default;

		public string ControllerFileName { get; private set; } = default;

	}
}
