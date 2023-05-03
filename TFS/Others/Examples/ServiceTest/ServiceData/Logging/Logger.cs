namespace ServiceData.Logging {
    using GregOsborne.Application.Exception;
    using System;
    using System.IO;

    public sealed class Logger : ILogger {
        public static Logger Create(string logLocation) {
            var result = new Logger(logLocation);
            return result;
        }

        public enum LogTypes {
            Information,
            Warning,
            Error,
            Critical
        }

        private Logger(string logLocation) => LogLocation = logLocation;

        public string LogLocation { get; private set; }

        public void LogMessage(string message, LogTypes logType = LogTypes.Information) => WriteToLogFile(message, logType);

        public void LogException(Exception ex, LogTypes logType = LogTypes.Error) {
            var message = ex.ToStringRecurse();
            LogMessage(message.ToString(), logType);
        }

        private readonly object _fileLock = new object();
        private readonly int _dateWidth = 26;
        private readonly int _typeWidth = 13;

        private void WriteToLogFile(string message, LogTypes logType) {
            lock (_fileLock) {
                using (var fileStream = new FileStream(LogLocation, FileMode.Append, FileAccess.Write, FileShare.Read))
                using (var writer = new StreamWriter(fileStream)) {
                    using(var reader = new StreamReader(message)) {
                        var firstLine = true;
                        while(reader.Peek() > -1) {
                            var line = reader.ReadLine();
                            if (firstLine) {
                                writer.Write(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt").PadRight(_dateWidth));
                                writer.Write(logType.ToString().PadRight(_typeWidth));
                            }
                            else
                                writer.Write(string.Empty.PadRight(_dateWidth + _typeWidth + 2));
                            writer.WriteLine(line);
                            firstLine = false;
                        }
                    }
                }
            }
        }
    }
}
