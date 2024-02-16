namespace ConsoleUtilities {
	using System;
	using System.Collections.Generic;
	using System.Configuration;
	using System.Diagnostics;
	using System.Linq;
	using System.Text;
	using GregOsborne.Application;
	using GregOsborne.Application.Exception;
	using GregOsborne.Application.Install;
	using GregOsborne.Application.Logging;
	using GregOsborne.Application.Primitives;

	public abstract class ConsoleApplication {
		private Stopwatch stopWatch = null;
		private ApplicationSettingsBase appSettings = null;

		protected List<string> OnValues {
			get;
		} = new List<string> { "on", "true", "yes" };

		public abstract void DisplayHelp(string[] args);

		public bool TruncateConsoleText { get; set; } = false;

		public ConsoleApplication(string[] args, string title, string logDir, string appName, Version version, ApplicationSettingsBase settings, string logDirectory, WriteStatusHandler writeHandler, string installerDirectory, string installerBaseName) {
			this.Session = new Session(logDir, appName, ApplicationLogger.StorageTypes.FlatFile, ApplicationLogger.StorageOptions.CreateFolderForEachDay, null, writeHandler);

			this.EMailBody = new StringBuilder(Convert.ToInt32(ByteSize.BytesInMegaByte));
			this.CommandLineArguments = CommandLine.GetArguments(args, out var exePath);
			this.ExecutablePath = exePath;
			this.appSettings = settings;
			this.Version = version;
			this.Title = title;
			Console.Title = this.Title;
			//var installerExeName = Path.Combine(installerDirectory, $"{installerBaseName}.exe");

			if (false && !string.IsNullOrEmpty(installerDirectory)) {
				var checker = new UpdateChecker {
					ApplicationDirectory = installerDirectory,
					ApplicationName = title,
					CurrentVersion = version,
					AppInstallExeName = default
				};
				if (checker.UpdateExists()) {
					this.WriteMessage($"Version {checker.HighestVersion} of {title} is available.\n");
					this.WriteMessage($"Would you like to download version {checker.HighestVersion} of {title} and install it?");
					this.WriteMessageStay("(Y)es or (N)o? ");
					var key = Console.ReadKey();
					if (key.KeyChar == 'Y' || key.KeyChar == 'y') {
						this.IsInstalling = true;
						checker.RunInstallation(checker.HighestVersion, title);
						return;
					}
				}
			}

			this.WriteMessage(this.Title);
			this.WriteMessage(this.Version.ToString());
			this.WriteMessage(string.Empty);
			this.WriteMessage($"Process started {DateTime.Now}");
			this.IsTimingEnabled = true;

			Environment.CurrentDirectory = System.IO.Path.GetDirectoryName(this.ExecutablePath);
		}

		~ConsoleApplication() {
			if (this.IsTimingEnabled && this.stopWatch != null) {
				this.stopWatch.Stop();
				this.WriteMessage($"The process took {this.stopWatch.Elapsed.Minutes} minutes {this.stopWatch.Elapsed.Seconds.ToString("00")}.{this.stopWatch.Elapsed.Milliseconds.ToString("00")} seconds");
			}
		}

		public Dictionary<string, string> CommandLineArguments {
			get;
			private set;
		}

		public bool IsInstalling { get; set; }
		public ConsoleColor DefaultColor { get; set; }
		public ConsoleColor WarningColor { get; set; }
		public ConsoleColor ErrorColor { get; set; }
		public static string PressAnyKeyMessage => "Press any key to exit...";

		public string ExecutablePath {
			get;
			private set;
		}

		public bool IsTimingEnabled {
			get; set;
		}

		public string Title {
			get; private set;
		}

		public string ApplicationInstallExeName {
			get; set;
		}

		public Version Version {
			get; private set;
		}

		public StringBuilder EMailBody {
			get; set;
		}

		public virtual void Run() {
			if (this.IsTimingEnabled) {
				this.stopWatch = new Stopwatch();
				this.stopWatch.Start();
			}
		}

		public T GetSettingValue<T>(string name, T defaultValue = default(T)) {
			foreach (SettingsProperty item in this.appSettings.Properties) {
				if (item.Name.Equals(name)) {
					return item.DefaultValue.CastTo<T>();
				}
			}
			return defaultValue;
		}

		public void WritePropertiesToConsole() {
			this.WriteMessage("Application Settings");
			foreach (SettingsProperty item in this.appSettings.Properties) {
				this.WriteMessage($"{item.Name}:={item.DefaultValue}");
			}
		}

		public void WriteArgumentsToConsole() {
			this.WriteMessage("Command Line Arguments");
			this.CommandLineArguments.Keys.ToList().ForEach(x => this.WriteMessage($"{x}:={this.CommandLineArguments[x]}"));
		}

		public Session Session { get; private set; }

		public void WriteToLogAndConsole(Exception ex) {
			if (this.Session == null) {
				return;
			}

			this.Session.Logger.LogMessage(ex.ToStringRecurse(), ApplicationLogger.EntryTypes.Error);
			this.WriteMessage(ex.Message, this.GetConsoleColor(ApplicationLogger.EntryTypes.Error), false);
		}

		public void WriteToLogAndConsole(string msg, ApplicationLogger.EntryTypes logType = ApplicationLogger.EntryTypes.Information, bool truncate = false) {
			if (this.Session == null) {
				return;
			}

			this.Session.Logger.LogMessage(msg, logType);
			this.WriteMessage(msg, this.GetConsoleColor(logType), truncate);
		}

		private string GetTruncatedString(string msg, bool truncate = false) {
			if (truncate || this.TruncateConsoleText) {
				if (msg.Length > 79) {
					msg = $"{msg.Substring(0, 74)}...";
				}
			}
			return msg;
		}

		public void WriteMessage(string msg, ConsoleColor color = ConsoleColor.White, bool truncate = false) {
			msg = this.GetTruncatedString(msg, truncate);
			Console.ForegroundColor = color;
			Console.WriteLine(msg);
			Console.ForegroundColor = this.DefaultColor;
		}

		public void WriteMessageStay(string msg, ConsoleColor color = ConsoleColor.White, bool truncate = false) {
			msg = this.GetTruncatedString(msg, truncate);
			Console.ForegroundColor = color;
			Console.Write(msg);
			Console.ForegroundColor = this.DefaultColor;
		}

		public void WriteMessage() => this.WriteMessage(string.Empty);

		private ConsoleColor GetConsoleColor(ApplicationLogger.EntryTypes logType) => logType == ApplicationLogger.EntryTypes.Error
			? this.ErrorColor
			: logType == ApplicationLogger.EntryTypes.Warning
				? this.WarningColor
				: this.DefaultColor;
	}
}
