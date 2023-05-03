namespace Counties.MVVM {
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public delegate void ExecuteUiActionHandler(object sender, ExecuteUiActionEventArgs e);
	public class ExecuteUiActionEventArgs : EventArgs {
		public ExecuteUiActionEventArgs(string commandToExecute) {
			this.CommandToExecute = commandToExecute;
			this.Parameters = new Dictionary<string, object>();
		}

		public ExecuteUiActionEventArgs(string commandToExecute, Dictionary<string, object> parameters)
			: this(commandToExecute) {
			this.Parameters = parameters;
		}

		public ExecuteUiActionEventArgs(string commandToExecute, params KeyValuePair<string, object>[] parameters)
			: this(commandToExecute) {
			this.Parameters = new Dictionary<string, object>();
			parameters.ToList().ForEach(x => this.Parameters.Add(x.Key, x.Value));
		}

		public ExecuteUiActionEventArgs(UiActionParameters uiParams) {
			this.UiParameters = uiParams;
		}

		public string CommandToExecute {
			get; private set;
		}

		public Dictionary<string, object> Parameters {
			get; private set;
		}

		public UiActionParameters UiParameters { get; private set; } = default(UiActionParameters);
	}
}
