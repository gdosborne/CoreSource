namespace ServiceData.Logging {
    using System;

    public interface ILogger {
        string LogLocation { get; }
        void LogMessage(string message, Logger.LogTypes logType);
        void LogException(Exception ex, Logger.LogTypes logType);
    }
}
