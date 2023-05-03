namespace AppSystem.ApplicationHelpers {
	using System;
	using System.IO;
	using System.Text;
	using System.Threading.Tasks;
	using AppSystem.IO;
	using AppSystem.Text;
	using Windows.Storage;

	public class LoggingManager {
		public enum EntryTypes {
			Information,
			Warning,
			Error
		}

		private StorageFile currentLogFile = default;
		private readonly int dateSize = 30;
		private readonly int typeSize = 13;

		public void LogMessage(string message, EntryTypes entryTypes) {
			var sb = new StringBuilder();
			sb.Append(DateTime.Now.ToString().PadRight(this.dateSize));
			sb.Append(entryTypes.ToString().PadRight(this.typeSize));
			var firstLine = true;
			using (var sr = new StringReader(message)) {
				while (sr.Peek() > -1) {
					if (!firstLine) {
						sb.Append(GlobalMethods.Spaces(this.dateSize + this.typeSize));
					}
					sb.AppendLine(sr.ReadLine());
					firstLine = false;
				}
			}
			var filename = $"{DateTime.Now.ToString("yyyy-MM-dd")}.txt";
			var logFile = default(StorageFile);
			try {
				var logFolder = ApplicationData.Current.RoamingFolder.GetFolderAsync("Logs").AsTask().Result;
				try {
					logFile = logFolder.GetFileAsync(filename).AsTask().Result;
				}
				catch { logFile = logFolder.CreateFileAsync(filename).AsTask().Result; }
				FileIO.AppendTextAsync(logFile, sb.ToString()).AsTask();
			}
			catch { throw; }
			logFile = null;
		}

		public async void LogException(Exception ex) => this.LogMessage(ex.ToString(), EntryTypes.Error);

	}
}
