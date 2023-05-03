namespace GregOsborne.Developers.Suite {
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Windows;
	using GregOsborne.Application;
	using GregOsborne.Application.Logging;
	using GregOsborne.Developers.Suite.Configuration;
	using GregOsborne.Dialogs;
	using GregOsborne.Suite.Extender;

	public partial class App : System.Windows.Application {
		public string ApplicationName { get; } = "Developer Suite";
		public string ApplicationDirectory { get; private set; } = default;
		public string LogDirectory { get; private set; } = default;
		public ExtensionManager ExtensionManager { get; private set; } = default;
		public List<AssemblyInformation> Extensions { get; private set; } = new List<AssemblyInformation>();
		public List<AssemblyInformation> Assemblies { get; private set; } = new List<AssemblyInformation>();
		public string CurrentConfigurationFileName { get; set; } = default;
		public string ExtensionsDirectory { get; private set; } = default;

		protected override void OnStartup(StartupEventArgs e) {
			this.ApplicationDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), this.ApplicationName);
			if (!Directory.Exists(this.ApplicationDirectory)) {
				Directory.CreateDirectory(this.ApplicationDirectory);
			}
			this.LogDirectory = Path.Combine(this.ApplicationDirectory, "Logs");
			if (!Directory.Exists(this.LogDirectory)) {
				Directory.CreateDirectory(this.LogDirectory);
			}
			Settings.SettingsFileName = Path.Combine(this.ApplicationDirectory, "Settings.xml");
			Logger.LogDirectory = this.LogDirectory;
			Logger.LogMessage($"Starting {this.ApplicationName}");

			var triggerFileName = Path.Combine(this.ApplicationDirectory, "_trigger.ext");
			if (File.Exists(triggerFileName)) {
				Logger.LogMessage("Extension trigger file exists");
				using (var fs = new FileStream(triggerFileName, FileMode.Open, FileAccess.Read, FileShare.None))
				using (var sr = new StreamReader(fs)) {
					while (sr.Peek() > -1) {
						var fName = sr.ReadLine();
						if (File.Exists(fName)) {
							var dir = Path.GetDirectoryName(fName);
							Logger.LogMessage($"Deleting extension {Path.GetFileName(dir)}");
							Directory.Delete(dir, true);
						}
					}
				}
				File.Delete(triggerFileName);
			}

			this.CurrentConfigurationFileName = Settings.GetSetting(this.ApplicationName, "Application", "Configuration File Name", string.Empty);
			var appDir = Path.GetDirectoryName(typeof(AssemblyInformation).Assembly.Location);
			this.ExtensionsDirectory = Path.Combine(this.ApplicationDirectory, "Extensions");

			var assyDirs = new string[] {
				appDir,
				this.ExtensionsDirectory
			};

			var assys = new List<AssemblyInformation>();
			assyDirs.ToList().ForEach(dir => {
				assys.AddRange(AssemblyInformation.LoadAssemblies(dir));

			});

			var result = new List<AssemblyInformation>();
			assys.Select(x => x.AssemblyName)
				.Distinct()
				.ToList()
				.ForEach(name => result.Add(assys.Where(a => a.AssemblyName == name)
				.OrderByDescending(z => z.BuildDate).FirstOrDefault()));
			if (result.OrderBy(x => x.AssemblyName).Any()) {
				for (var i = result.Count - 1; i >= 0; i--) {
					if (result[i].ExtensionNames.Any() && !this.Extensions.Any(x => x.AssemblyFileName == result[i].AssemblyFileName)) {
						this.Extensions.Add(result[i]);
					} else {
						this.Assemblies.Add(result[i]);
					}
					result.RemoveAt(i);
				}
			}

			this.ExtensionManager = new ExtensionManager();
			this.ExtensionManager.ExtensionAdded += this.ExtensionManager_ExtensionAdded;
			this.Extensions.ForEach(ext => {
				foreach (var name in ext.ExtensionNames) {
					Logger.LogMessage($"Loading extension {name}");
				}
				this.ExtensionManager.AddExtension(ext.Path);
			});
			this.ShutdownMode = ShutdownMode.OnMainWindowClose;
		}

		private void ExtensionManager_ExtensionAdded(object sender, ExtensionAddedEventArgs e) {
			if (!this.Extensions.Any(x => x.ExtensionNames.Contains(e.Extension.Title))) {
				var extensionName = e.Extension.Title;
				var assyInfo = AssemblyInformation.FromExtension(e.Extension);
				this.Extensions.Add(assyInfo);
			}
		}

		private void RemoveDeletedExtensionAssemblies() {
			if (!this.ExtensionManager.AssemblyFileNamesToDeleteOnShutdown.Any()) {
				return;
			}

			var triggerFileName = Path.Combine(this.ApplicationDirectory, "_trigger.ext");
			if (File.Exists(triggerFileName)) {
				File.Delete(triggerFileName);
			}
			var result = this.DisplayMessage($"Developer Suite will remove the following extensions on next startup:\n\n{string.Join("\n", this.ExtensionManager.AssemblyFileNamesToDeleteOnShutdown.Select(x => Path.GetFileName(x)))}\n\nDo you still want to do this?", MessageTypes.Warning, "Extension removal", ButtonType.Yes, ButtonType.No);
			if (result == "No") {
				return;
			}
			using (var fs = new FileStream(triggerFileName, FileMode.CreateNew, FileAccess.Write, FileShare.None))
			using (var sw = new StreamWriter(fs)) {
				this.ExtensionManager.AssemblyFileNamesToDeleteOnShutdown.ForEach(x => {
					Logger.LogMessage($"Marking extension {Path.GetFileName(x)} for deletion");
					sw.WriteLine(x);
				});
			}
			this.ExtensionManager.AssemblyFileNamesToDeleteOnShutdown.Clear();
		}

		protected override void OnExit(ExitEventArgs e) {
			Logger.LogMessage($"Exiting {this.ApplicationName}");
			this.RemoveDeletedExtensionAssemblies();
		}

		protected override void OnSessionEnding(SessionEndingCancelEventArgs e) {
			Logger.LogMessage($"Session ending for {this.ApplicationName}");
			this.RemoveDeletedExtensionAssemblies();
		}

		public enum MessageTypes {
			Information,
			Warning,
			Error
		}

		public string DisplayMessage(string message, MessageTypes messageType, string title, params ButtonType[] buttons) {
			var buttonsLocal = new List<TaskDialogButton>();
			foreach (var bt in buttons) {
				buttonsLocal.Add(new TaskDialogButton(bt) { Text = bt.ToString() });
			}
			return this.DisplayMessage(message, messageType, title, buttonsLocal.ToArray());
		}

		public string DisplayMessage(string message, MessageTypes messageType, string title, params TaskDialogButton[] buttons) {
			var td = new TaskDialog {
				WindowTitle = title,
				Content = message,
				AllowDialogCancellation = false,
				ButtonStyle = TaskDialogButtonStyle.Standard,
				MainInstruction = title,
				MainIcon = messageType == MessageTypes.Information ? TaskDialogIcon.Information : messageType == MessageTypes.Warning ? TaskDialogIcon.Warning : TaskDialogIcon.Error
			};
			foreach (var bt in buttons) {
				td.Buttons.Add(bt);
			}
			var result = td.ShowDialog();
			return result.Text;
		}

		public void ExitApplication() => this.Shutdown();
	}
}
