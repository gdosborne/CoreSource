namespace OptiRampDesignerModel
{
    using System;

    public interface ILog
    {
        #region Public Properties

        string LogFileName { get; }

        #endregion Public Properties

        #region Public Methods

        void WriteException(Exception ex, bool recursive = false);

        void WriteMessage(string message);

        void WriteMessage(string format, params object[] p);

        #endregion Public Methods
    }
}