using System.Reflection;
using VersionMaster;
using static VersionMaster.UpdateVersionException;
using static VersionMasterCore.Utils;
using SysIO = System.IO;

namespace UpdateVersion {
    internal static class StaticValues {
        public const int errorBase = 600;
        public const int padSize = 18;
        public static string projectName = string.Empty;
        public static AppSingleton appSingleton = default;

        internal static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            LogMessage(e.ExceptionObject.ToString(), "Unknown project");
        }

        internal static void Reader_DisplayMessage(object sender, DisplayMessageEventArgs e) {
            WriteToConsole(e.Message);
        }

        internal static void Project_ReportProgress(object sender, ReportProgressEventArgs e) {
            WriteLineToConsole(e.Message, projectName);
        }

        internal static string GetMaxLengthString(string value, int length, string trailer = "...") {
            if (!string.IsNullOrEmpty(trailer)) {
                length -= trailer.Length;
            }

            if (value.Length < length) {
                return value;
            }

            return value.Substring(0, length) + trailer;
        }

        internal static void LogMessage(string message, string projectName) {
            var logFileName = SysIO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UpdateVersion");
            if (!SysIO.Directory.Exists(logFileName)) {
                SysIO.Directory.CreateDirectory(logFileName);
            }

            logFileName = SysIO.Path.Combine(logFileName, $"{projectName}.applog.txt");
            using (var fs = new FileStream(logFileName, FileMode.Append, FileAccess.Write, FileShare.Read))
            using (var sw = new StreamWriter(fs)) {
                sw.WriteLine(message);
            }
        }

        internal static void WriteToConsole(string message) {
            Console.Write(message);
        }

        private static List<string> savedLines = new List<string>();

        internal static void WriteLineToConsole(string message, string projectName = default, bool forceAll = default) {
            if (string.IsNullOrEmpty(projectName)) {
                savedLines.Add(message);
            }
            else {
                if (savedLines.Count != 0) {
                    savedLines.ForEach(x => LogMessage(x, projectName));
                    savedLines.Clear();
                }
                LogMessage(message, projectName);
            }
            Console.WriteLine(message);
        }

        internal static void ShowError(ErrorValues errorNumber, string message, string projectName) {
            message = errorNumber.Description();
            if (string.IsNullOrEmpty(projectName)) {
                savedLines.Add(message);
            }
            else {
                if (savedLines.Count != 0) {
                    savedLines.ForEach(x => LogMessage(x, projectName));
                }

                LogMessage(message, projectName);
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
            ExitApp((int)errorNumber, true, true, projectName);
        }

        internal static void ExitApp(int exitCode, bool forcePause, bool exitApplication, string projectName) {
            if (exitApplication) {
                //must exit with exit code of 0 so that build will continue
                Environment.Exit(0);
            }
        }

        internal static void ShowErrorCodes() {
            ShowFile("_errors.txt");
        }

        internal static void ShowHelp() {
            ShowFile("_readme.txt");
        }

        internal static void ShowFile(string fileName) {
            var filePath = SysIO.Path.Combine(SysIO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
            if (SysIO.File.Exists(filePath)) {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var sr = new StreamReader(fs)) {
                    WriteLineToConsole(sr.ReadToEnd(), projectName, true);
                }
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }
            ExitApp(0, true, true, projectName);
        }
    }
}
