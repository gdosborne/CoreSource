namespace GregOsborne.Application.Logging {
    using System.Globalization;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Logger
    /// </summary>
    public static class Logger {
        static Logger() {
            SingleLogMaxSize = Convert.ToInt32(System.Math.Pow(10.24, 6));
        }
        /// <summary>
        /// Log directory
        /// </summary>
        public static string LogDirectory { get; set; }
        /// <summary>
        /// Max file size of a single log file
        /// </summary>
        public static int SingleLogMaxSize { get; set; }
        /// <summary>
        /// Log exception
        /// </summary>
        /// <param name="ex"></param>
        public static void LogException(Exception ex) {
            LogMessage(ex.ToString(), true);
        }
        /// <summary>
        /// Log message
        /// </summary>
        /// <param name="message"></param>
        public static void LogMessage(string message) {
            LogMessage(message, false);
        }
        /// <summary>
        /// Log message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="isException"></param>
        public static void LogMessage(string message, bool isException) {
            if (string.IsNullOrEmpty(LogDirectory) || !Directory.Exists(LogDirectory))
                throw new LogDirectoryMissingException($"The log directory {LogDirectory} is missing.");
            var sb = new StringBuilder();
            sb.Append(DateTime.Now.ToString(CultureInfo.InvariantCulture).PadRight(30));
            sb.Append(isException ? "Exception".PadRight(15) : "Information".PadRight(15));
            var lines = GetLines(message);
            sb.AppendLine(lines[0]);
            if (lines.Count > 1)
            {
                for (var i = 1; i < lines.Count; i++)
                {
                    sb.Append(new string(' ', 48));
                    sb.AppendLine(lines[i]);
                }
            }
            var logFile = Path.Combine(LogDirectory, "log.txt");
            if (File.Exists(logFile) && new FileInfo(logFile).Length > SingleLogMaxSize)
            {
                var num = 0;
                var fileName = Path.Combine(LogDirectory, $"log_backup_{num:000}.txt");
                while (File.Exists(fileName))
                {
                    num++;
                    fileName = Path.Combine(LogDirectory, $"log_backup_{num:000}.txt");
                }
                File.Move(Path.Combine(LogDirectory, "log.txt"), fileName);
            }
            using (var fs = new FileStream(logFile, FileMode.Append, FileAccess.Write, FileShare.None))
            using (var sw = new StreamWriter(fs))
            {
                sw.Write(sb.ToString());
            }
        }
        private static List<string> GetLines(string value) {
            var result = new List<string>();
            using (var sr = new StringReader(value))
            {
                string line;
                while ((line = sr.ReadLine()) != null) { result.Add(line); }
            }
            return result;
        }
    }
}
