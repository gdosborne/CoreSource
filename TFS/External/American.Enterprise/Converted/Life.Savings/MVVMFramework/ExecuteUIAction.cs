namespace GregOsborne.MVVMFramework {
    using System;
    using System.Collections.Generic;

    public delegate void ExecuteUiActionHandler(object sender, ExecuteUiActionEventArgs e);

    public class ExecuteUiActionEventArgs : EventArgs
    {
        #region Public Constructors

        public ExecuteUiActionEventArgs(string commandToExecute, Dictionary<string, object> parameters)
            : this(commandToExecute)
        {
            Parameters = parameters;
        }

        public ExecuteUiActionEventArgs(string commandToExecute)
        {
            CommandToExecute = commandToExecute;
        }

        #endregion Public Constructors

        #region Public Properties

        public string CommandToExecute { get; private set; }

        public Dictionary<string, object> Parameters { get; private set; }

        #endregion Public Properties
    }
}
