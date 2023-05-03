namespace ServiceData.Reporting {
    using System;

    public delegate void ReportExceptionHandler(object sender, ReportExceptionEventArgs e);
    public class ReportExceptionEventArgs : EventArgs {
        public ReportExceptionEventArgs(Exception ex) {
            Exception = ex;
        }

        public Exception Exception { get; private set; }
    }
}
