namespace MVVMFramework {
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using AppSystem.Primitives;

	public delegate void ExecuteUIActionHandler(object sender, ExecuteUIActionEventArgs e);
	public class ExecuteUIActionEventArgs : EventArgs {
		public ExecuteUIActionEventArgs(string commandToExecute, params DictionaryEntry[] parameters)
			: this(commandToExecute) {
			parameters.ToList().ForEach(x => this.Parameters.Add(x.Key.As<string>(), x.Value));
		}

		public ExecuteUIActionEventArgs(string commandToExecute) {
			this.CommandToExecute = commandToExecute;
			this.Parameters = new Dictionary<string, object>();
		}

		public string CommandToExecute {
			get; private set;
		}

		public Dictionary<string, object> Parameters {
			get; private set;
		}
	}
}
