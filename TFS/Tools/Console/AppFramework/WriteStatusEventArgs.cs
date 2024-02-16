namespace GregOsborne.Application {
	using System;
	using GregOsborne.Application.Logging;

	public delegate void WriteStatusHandler(object sender, WriteStatusEventArgs e);

	public class WriteStatusEventArgs : EventArgs {
		public WriteStatusEventArgs(string message, int loginId, int catId, int queueId = 0) {
			this.Message = message;
			this.LoginId = loginId;
			this.CatId = catId;
			this.QueueId = queueId;
			this.LogType = ApplicationLogger.EntryTypes.Information;
		}

		public WriteStatusEventArgs(string message, int queueId = 0) {
			this.Message = message;
			this.LoginId = 0;
			this.CatId = 0;
			this.QueueId = queueId;
			this.LogType = ApplicationLogger.EntryTypes.Information;
		}

		public WriteStatusEventArgs(string message, ApplicationLogger.EntryTypes logType, int loginId, int catId, int queueId = 0)
			: this(message, loginId, catId, queueId) => this.LogType = logType;

		public WriteStatusEventArgs(System.Exception ex)
			: this(ex, 0, 0, 0) { }
		public WriteStatusEventArgs(System.Exception ex, int loginId, int catId, int queueId = 0) {
			this.LogType = ApplicationLogger.EntryTypes.Error;
			this.LoginId = loginId;
			this.CatId = catId;
			this.QueueId = queueId;
			this.Exception = ex;
		}

		public WriteStatusEventArgs(bool isProcessComplate, int queueId = 0) {
			this.IsProcessComplete = isProcessComplate;
			this.QueueId = queueId;
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
