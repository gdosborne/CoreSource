using Common.AppFramework.Logging;
using System;

namespace Common.AppFramework {
    public delegate void WriteStatusHandler(object sender, WriteStatusEventArgs e);

    public class WriteStatusEventArgs : EventArgs {
        public WriteStatusEventArgs(string message, int loginId, int catId, int queueId = 0) {
            Message = message;
            LoginId = loginId;
            CatId = catId;
            QueueId = queueId;
            LogType = ApplicationLogger.EntryTypes.Information;
        }

        public WriteStatusEventArgs(string message, int queueId = 0) {
            Message = message;
            LoginId = 0;
            CatId = 0;
            QueueId = queueId;
            LogType = ApplicationLogger.EntryTypes.Information;
        }

        public WriteStatusEventArgs(string message, ApplicationLogger.EntryTypes logType, int loginId, int catId, int queueId = 0)
            : this(message, loginId, catId, queueId) => LogType = logType;

        public WriteStatusEventArgs(System.Exception ex)
            : this(ex, 0, 0, 0) { }
        public WriteStatusEventArgs(System.Exception ex, int loginId, int catId, int queueId = 0) {
            LogType = ApplicationLogger.EntryTypes.Error;
            LoginId = loginId;
            CatId = catId;
            QueueId = queueId;
            Exception = ex;
        }

        public WriteStatusEventArgs(bool isProcessComplate, int queueId = 0) {
            IsProcessComplete = isProcessComplate;
            QueueId = queueId;
        }

        public ApplicationLogger.EntryTypes LogType {
            get; private set;
        }

        public string Message {
            get; private set;
        }

        public bool IsProcessComplete { get; private set; }

        public System.Exception Exception { get; private set; }

        public int LoginId { get; private set; }

        public int CatId { get; private set; }

        public int QueueId { get; private set; }
    }
}
