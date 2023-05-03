namespace GregOsborne.Application.Install {
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Windows;
	using GregOsborne.Application.IO;
	using GregOsborne.Application.Text;
	using IO = System.IO;

	public sealed class UpdateChecker {

		private void ProcessPath(bool isTest) {
			this.ApplicationDirectory = IO.Path.Combine(this.ApplicationDirectory, this.ApplicationName);
			var szTest = new IO.DirectoryInfo(this.ApplicationDirectory).Parent.Size();
			if (isTest) {
				this.ApplicationDirectory += " Test";
			}

			new IO.DirectoryInfo(this.ApplicationDirectory).CreateIfNotExist();
		}

		public Version GetPreviousVersion() {
			var maxVersion = this.AllVersions.OrderByDescending(x => x).First();
			return this.AllVersions.OrderByDescending(x => x).Skip(1).First();
		}

		public string ApplicationDirectory { get; set; }

		public string ApplicationName { get; set; }

		public Version CurrentVersion { get; set; }

		public Guid ApplicationGuid { get; set; }

		public string AppInstallExeName { get; set; }

		public IEnumerable<Version> AllVersions {
			get {
				var dir = new IO.DirectoryInfo(this.ApplicationDirectory);
				if (!dir.Exists) {
					return new List<Version>();
				}

				var result = dir
					.GetDirectories()
					.Where(x => Version.TryParse(x.Name, out var version) && IO.File.Exists(IO.Path.Combine(this.ApplicationDirectory, version.ToString(), this.AppInstallExeName)))
					.Select(x => Version.Parse(x.Name))
					.ToList();
				return result;
			}
		}

		public Version HighestVersion {
			get {
				var allVersions = this.AllVersions;
				if (!allVersions.Any()) {
					return new Version();
				}

				return this.AllVersions.Max(x => x);
			}
		}

		public string SetupFileName { get; set; }

		public override string ToString() => $"{this.ApplicationName} : {this.CurrentVersion}";

		public bool UpdateExists(Version version) => this.HighestVersion > version;

		public bool UpdateExists() => this.UpdateExists(this.CurrentVersion);

		private void UnloadAllInstances(string applicationTitle) {
			var currentProcessId = Process.GetCurrentProcess().Id;
			var allProcesses = Process.GetProcesses();
			var procs = Process.GetProcesses().Where(x => applicationTitle.ContainsIgnoreCase(x.ProcessName) || x.ProcessName.ContainsIgnoreCase(applicationTitle));
			if (procs.Any()) {
				procs.ToList().ForEach(x => {
					if (currentProcessId != x.Id) {
						x.Kill();
					}
				});
			}
		}

		public bool RunInstallation(Version version, string applicationTitle) {
			this.UnloadAllInstances(applicationTitle);
			try {
				var runFile = this.DownloadVersionExe(version);
				if (runFile == null) {
					return false;
				}
				var p = new Process {
					StartInfo = new ProcessStartInfo {
						FileName = runFile
					}
				};
				p.Start();
			}
			catch (Exception ex) {
				MessageBox.Show($"An error occurred installing new version\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}
			return true;
		}

		private string DownloadVersionExe(Version newVersion) {
			if (!this.AllVersions.Any(x => x == newVersion)) {
				return null;
			}

			var directory = new IO.DirectoryInfo(this.ApplicationDirectory);
			foreach (var subDirectory in directory.GetDirectories()) {
				if (Version.TryParse(subDirectory.Name, out var version)) {
					if (version == newVersion) {
						var exeFileName = IO.Path.Combine(this.ApplicationDirectory, version.ToString(), this.AppInstallExeName);
						if (IO.File.Exists(exeFileName)) {
							var tempDir = IO.Path.Combine(IO.Path.GetTempPath(), Guid.NewGuid().ToString());
							if (!IO.Directory.Exists(tempDir)) {
								IO.Directory.CreateDirectory(tempDir);
							}

							var tempFileName = IO.Path.Combine(tempDir, this.AppInstallExeName);
							IO.File.Copy(exeFileName, tempFileName, true);
							return tempFileName;
						}
						break;
					}
				}
			}
			return default;
		}
	}
}