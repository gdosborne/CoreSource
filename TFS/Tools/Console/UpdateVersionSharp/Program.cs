namespace UpdateVersionSharp {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using ConsoleUtilities;
	using GregOsborne.Application.IO;
	using VersionMaster;
	using static VersionMaster.Enumerations;
	using sysio = System.IO;

	internal class Program {

		private const int errorBase = 600;
		private const int padSize = 18;
		private static string projectName = string.Empty;
		private static AppSingleton appSingleton = default;

		public enum ErrorValues {
			[Description("Path to project is required")]
			ProjectPathInvalid = 601,

			[Description("Project folder does not exist")]
			ProjectFolderInvalid = 602,

			[Description("AssemblyInfo file missing")]
			AssemblyInfoFileMissing = 603,

			[Description("Project file does not exist")]
			ProjectFileInvalid = 604,

			[Description("Error getting assembly version")]
			CannotGetAssemblyVersion = 605,

			[Description("Project is skipped")]
			ProjectSkipped = 606,

			[Description("Project configuration file missing")]
			ConfigMissing = 607,

			[Description("Project not recognized - use EnableVersioning to add the project")]
			ProjectNotRecognized = 608,

			[Description("Unknown error - see console")]
			UnKnown = errorBase + 100
		}

		private static void Main(string[] args) {

			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

			var assy = Assembly.GetEntryAssembly();
			var version = assy.GetName().Version;

			WriteLineToConsole($"Version Updater (c#) V{version}{Environment.NewLine}", projectName);

			if (args.Length == 0) {
				ShowHelp();
			}

			appSingleton = new AppSingleton();
			appSingleton.WaitForPreviousTermination();

			var reader = default(ProjectConfigurationReader);
			try {

				var executablePath = string.Empty;
				var arguments = CommandLine.GetArguments(Environment.CommandLine, '-', out executablePath);
				var dataDir = sysio.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UpdateVersion");
				if (!sysio.Directory.Exists(dataDir)) {
					sysio.Directory.CreateDirectory(dataDir);
				}
				new DirectoryInfo(dataDir).CleanupDirectory(".xml");

				arguments.Select(x => x.Key).ToList().ForEach(x => {
					if (arguments.Any(y => y.Key == x)) {
						var item = arguments.FirstOrDefault(y => y.Key == x);
						switch (x) {

							case "-p":
								projectName = item.Value;
								break;

							case "-e":
								ShowErrorCodes();
								break;
						}
					}
				});

				var projectsFileName = sysio.Path.Combine(dataDir, "UpdateVersion.Projects.xml");

				if (!sysio.File.Exists(projectsFileName)) {
					var source = sysio.Path.Combine(sysio.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "UpdateVersion.Projects.xml");
					sysio.File.Copy(source, projectsFileName);
				}

				reader = new ProjectConfigurationReader(projectsFileName);
				reader.DisplayMessage += Reader_DisplayMessage;
				reader.Initialize();

                var project = reader.Projects.FirstOrDefault(x => x.FullPath.Equals(projectName, StringComparison.OrdinalIgnoreCase));
				if (project == null) {
					ShowError(ErrorValues.ProjectNotRecognized, $"{projectName} doesn't exist", projectName);
				}

				if (string.IsNullOrEmpty(project.ProjectFileName) || string.IsNullOrEmpty(project.ProjectDirectory)) {
					ShowError(ErrorValues.ProjectPathInvalid, null, projectName);
				}

				if (!sysio.Directory.Exists(project.ProjectDirectory)) {
					ShowError(ErrorValues.ProjectFolderInvalid, null, projectName);
				}

				WriteLineToConsole($"{("Project file:").PadLeft(padSize)} {project.ProjectFileName}", projectName);
				WriteLineToConsole($"{("Project folder:").PadLeft(padSize)} {project.ProjectDirectory}", projectName);

                if (!sysio.File.Exists(project.FullPath)) {
					ShowError(ErrorValues.ProjectFileInvalid, null, projectName);
				}

				reader.SelectedSchema = reader.Schemas.FirstOrDefault(x => x.Name.Equals(project.SchemaName));

				WriteLineToConsole($"{("Schema:").PadLeft(padSize)} {reader.SelectedSchema.Name}", projectName);
				Environment.CurrentDirectory = project.ProjectDirectory;

				WriteLineToConsole($"{("AssemblyInfo file:").PadLeft(padSize)} {project.AssemblyInfoPath}", projectName);

				if (!sysio.File.Exists(project.AssemblyInfoPath)) {
					ShowError(ErrorValues.AssemblyInfoFileMissing, null, projectName);
				}

				var assyVersion = project.CurrentAssemblyVersion;
				if (assyVersion == null) {
					ShowError(ErrorValues.CannotGetAssemblyVersion, null, projectName);
				}

				project.ReportProgress += Project_ReportProgress;

				WriteLineToConsole($"{Environment.NewLine}Modifying version for project {project.Name}", projectName);
				project.ModifyVersion();

				VersionParts.Assembly.UpdateAll(project.AssemblyInfoPath, assyVersion, project.ModifiedAssemblyVersion, project.LastBuildDate);

				project.UpdateLastBuildDate();
				reader.Save();
			}
			catch (Exception ex) {
				ShowError(ErrorValues.UnKnown, ex.Message, projectName);
			}
			finally {
				appSingleton.RemoveTriggerFile();
				if (reader != null) {
					reader.Dispose();
				}
			}
			var pause = false;
#if DEBUG
			pause = true;
#endif
			ExitApp(0, pause, true, projectName);
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) => LogMessage(e.ExceptionObject.ToString(), "Unknown project");

		private static void Reader_DisplayMessage(object sender, DisplayMessageEventArgs e) => WriteToConsole(e.Message);

		private static void Project_ReportProgress(object sender, ReportProgressEventArgs e) => WriteLineToConsole(e.Message, projectName);

		private static string GetMaxLengthString(string value, int length, string trailer = "...") {
			if (!string.IsNullOrEmpty(trailer)) {
				length -= trailer.Length;
			}

			if (value.Length < length) {
				return value;
			}

			return value.Substring(0, length) + trailer;
		}

		internal static void LogMessage(string message, string projectName) {
			var logFileName = sysio.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UpdateVersion");
			if (!sysio.Directory.Exists(logFileName)) {
				sysio.Directory.CreateDirectory(logFileName);
			}

			logFileName = sysio.Path.Combine(logFileName, $"{projectName}.applog.txt");
			using (var fs = new FileStream(logFileName, FileMode.Append, FileAccess.Write, FileShare.Read))
			using (var sw = new StreamWriter(fs)) {
				sw.WriteLine(message);
			}
		}

		internal static void WriteToConsole(string message) => Console.Write(message);
		private static List<string> savedLines = new List<string>();

		internal static void WriteLineToConsole(string message, string projectName = default, bool forceAll = default) {
			if (string.IsNullOrEmpty(projectName)) {
				savedLines.Add(message);
			} else {
				if (savedLines.Any()) {
					savedLines.ForEach(x => LogMessage(x, projectName));
					savedLines.Clear();
				}
				LogMessage(message, projectName);
			}
			Console.WriteLine(message);
		}

		private static void ShowError(ErrorValues errorNumber, string message, string projectName) {
			message = errorNumber.Description<ErrorValues>();
			if (string.IsNullOrEmpty(projectName)) {
				savedLines.Add(message);
			} else {
				if (savedLines.Any()) {
					savedLines.ForEach(x => LogMessage(x, projectName));
				}

				LogMessage(message, projectName);
			}
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(message);
			ExitApp((int)errorNumber, true, true, projectName);
		}

		private static void ExitApp(int exitCode, bool forcePause, bool exitApplication, string projectName) {
			if (exitApplication) {
				//must exit with exit code of 0 so that build will continue
				Environment.Exit(0);
			}
		}

		private static void ShowErrorCodes() => ShowFile("_errors.txt");

		private static void ShowHelp() => ShowFile("_readme.txt");

		private static void ShowFile(string fileName) {
			var filePath = sysio.Path.Combine(sysio.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
			if (sysio.File.Exists(filePath)) {
				using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
				using (var sr = new StreamReader(fs)) {
					WriteLineToConsole(sr.ReadToEnd(), projectName, true);
				}
				Console.ReadKey();
			}
			ExitApp(0, true, true, projectName);
		}
	}
}
