namespace OptiRampDesignerModel.Concrete
{
    using System;
    using System.Text;

    public class Log : ILog
    {
        #region Private Fields

        private int lastLogIndex = 0;

        private string logDirectory = null;

        private string logExtension = null;

        private string logFileNameBase = null;

        #endregion Private Fields

        #region Public Constructors

        public Log(string logFileName)
        {
            LogFileName = logFileName;
            logFileNameBase = System.IO.Path.GetFileNameWithoutExtension(LogFileName);
            logDirectory = System.IO.Path.GetDirectoryName(LogFileName);
            logExtension = GregOsborne.Application.IO.File.Extension(LogFileName);
        }

        #endregion Public Constructors

        #region Public Properties

        public string LogFileName { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public void WriteException(Exception ex, bool recursive = false)
        {
            var sb = new StringBuilder();
            sb.AppendLine(ex.Message);
            sb.AppendLine(ex.StackTrace);
            if (recursive)
            {
                var tab = 1;
                ex = ex.InnerException;
                while (ex != null)
                {
                    sb.Append(new string(' ', tab * 4));
                    sb.AppendLine(ex.Message);
                    sb.Append(new string(' ', tab * 4));
                    sb.AppendLine(ex.StackTrace);
                    tab++;
                    ex = ex.InnerException;
                }
            }
            WriteMessage(sb.ToString());
        }

        public void WriteMessage(string message)
        {
            //create new log if > 10 mb
            if (!System.IO.File.Exists(LogFileName) || GregOsborne.Application.IO.File.Size(LogFileName) > (10 * (1024 * 1024)))
            {
                lastLogIndex++;
                LogFileName = System.IO.Path.Combine(logDirectory, string.Format("{0}_{1}{2}", logFileNameBase, lastLogIndex, logExtension));
                using (var fs = new System.IO.FileInfo(LogFileName).OpenWrite())
                using (var sw = new System.IO.StreamWriter(fs))
                {
                    sw.WriteLine(string.Format("Log file created {0}", DateTime.Now));
                    sw.WriteLine();
                }
            }
            using (var fs = new System.IO.FileStream(LogFileName, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.None))
            using (var sw = new System.IO.StreamWriter(fs))
            {
                sw.Write(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff").PadRight(24));
                sw.WriteLine(message);
            }
        }

        public void WriteMessage(string format, params object[] p)
        {
            WriteMessage(string.Format(format, p));
        }

        #endregion Public Methods
    }
}