using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyApplication.Logging
{
	public static class Logger
	{
		#region Public Properties
		public static string LogDirectory { get; set; }
		#endregion

		#region Public Methods

		public static void LogException(Exception ex)
		{
			LogMessage(ex.ToString(), true);
		}

		public static void LogMessage(string message)
		{
			LogMessage(message, false);
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
		public static void LogMessage(string message, bool isException)
		{
			if (string.IsNullOrEmpty(LogDirectory) || !Directory.Exists(LogDirectory))
				throw new LogDirectoryMissingException("Log directory missing;");
			var sb = new StringBuilder();
			sb.Append(DateTime.Now.ToString().PadRight(30));
			if (isException)
				sb.Append("Exception".PadRight(15));
			else
				sb.Append("Information".PadRight(15));
			var lines = GetLines(message);
			sb.AppendLine(lines[0]);
			if (lines.Count > 1)
			{
				for (int i = 1; i < lines.Count; i++)
				{
					sb.Append(new String(' ', 48));
					sb.AppendLine(lines[i]);
				}
			}
			var logFile = Path.Combine(LogDirectory, "log.txt");
			if (File.Exists(logFile) && new FileInfo(logFile).Length > 1024000)
			{
				var num = 0;
				var fileName = Path.Combine(LogDirectory, string.Format("log_backup_{0}.txt", num.ToString("000")));
				while (File.Exists(fileName))
				{
					num++;
					fileName = Path.Combine(LogDirectory, string.Format("log_backup_{0}.txt", num.ToString("000")));
				}
				File.Move(Path.Combine(LogDirectory, "log.txt"), fileName);
			}
			using (var fs = new FileStream(logFile, FileMode.Append, FileAccess.Write, FileShare.None))
			using (var sw = new StreamWriter(fs))
			{
				sw.Write(sb.ToString());
			}
		}

		#endregion

		#region Private Methods

		private static List<string> GetLines(string value)
		{
			var result = new List<string>();
			using (StringReader sr = new StringReader(value))
			{
				string line;
				while ((line = sr.ReadLine()) != null) { result.Add(line); }
			}
			return result;
		}

		#endregion
	}
}