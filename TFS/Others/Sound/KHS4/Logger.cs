using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System;
namespace KHS4
{
	internal static class Logger
	{
		public static readonly string LogFolder;
		static Logger()
		{
			LogFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Kingdom Hall Sound Logs");
			if(!Directory.Exists(LogFolder))
				Directory.CreateDirectory(LogFolder);
		}
		public static void LogException(Exception ex)
		{
			var sb = new StringBuilder();
			sb.AppendLine(ex.Message);
			var lines = GetLines(ex.StackTrace);
			lines.ForEach(x => sb.AppendLine(x));
			LogMessage(sb.ToString());
		}
		public static void LogMessage(string message)
		{
			var logFile = Path.Combine(LogFolder, string.Format("{0}.txt", DateTime.Now.ToString("yyyy-MM-dd")));
			using(var fs = new FileStream(logFile, FileMode.Append, FileAccess.Write, FileShare.None))
			using(var sw = new StreamWriter(fs))
			{
				var lines = GetLines(message);
				var firstline = true;
				sw.Write(DateTime.Now.ToString().PadRight(25));
				lines.ForEach(x =>
				{
					if(!firstline)
						sw.Write(new string(' ', 25));
					sw.WriteLine(x);
					firstline = false;
				});
				sw.Close();
				fs.Close();
			}
		}
		private static List<string> GetLines(string data)
		{
			var result = new List<string>();
			using(var sr = new StringReader(data))
			{
				while(sr.Peek() > -1)
				{
					result.Add(sr.ReadLine());
				}
			}
			return result;
		}
	}
}
