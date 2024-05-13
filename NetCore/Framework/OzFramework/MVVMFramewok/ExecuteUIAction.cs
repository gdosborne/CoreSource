/* File="ExecuteUIAction"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.MVVMFramework {
    public delegate void ExecuteUiActionHandler(object sender, ExecuteUiActionEventArgs e);
    public class ExecuteUiActionEventArgs : EventArgs {
        public ExecuteUiActionEventArgs(string commandToExecute) {
            CommandToExecute = commandToExecute;
            Parameters = new Dictionary<string, object>();
        }

        public ExecuteUiActionEventArgs(string commandToExecute, Dictionary<string, object> parameters)
            : this(commandToExecute) => Parameters = parameters;

        public ExecuteUiActionEventArgs(string commandToExecute, params KeyValuePair<string, object>[] parameters)
            : this(commandToExecute) {
            Parameters = new Dictionary<string, object>();
            parameters.ToList().ForEach(x => Parameters.Add(x.Key, x.Value));
        }

        public ExecuteUiActionEventArgs(string commandToExecute, UiActionParameters parameters)
            : this(commandToExecute) {
            Parameters = new Dictionary<string, object>();
            parameters.ToList().ForEach(x => Parameters.Add(x.Key, x.Value));
        }

        public string? CommandToExecute { get; private set; }

        public Dictionary<string, object> Parameters { get; private set; }

        public UiActionParameters? UiParameters { get; private set; } = default;
    }
}
