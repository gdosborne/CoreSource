namespace VersionMaster {
    using System;
    using static Enumerations;

    public delegate void ReportProgressHandler(object sender, ReportProgressEventArgs e);

    public class ReportProgressEventArgs : EventArgs {
        public ReportProgressEventArgs(string message) {
            Message = message;
        }

        public string Message {
            get; private set;
        }

    }
}
