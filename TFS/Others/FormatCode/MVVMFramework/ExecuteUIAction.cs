using System;
using System.Collections.Generic;

namespace MVVMFramework
{
	public delegate void ExecuteUIActionHandler(object sender, ExecuteUIActionEventArgs e);

	public class ExecuteUIActionEventArgs : EventArgs
	{
		#region Public Constructors

		public ExecuteUIActionEventArgs(string commandToExecute, Dictionary<string, object> parameters)
		{
			CommandToExecute = commandToExecute;
			Parameters = parameters;
		}

		#endregion

		#region Public Properties
		public string CommandToExecute { get; private set; }
		public Dictionary<string, object> Parameters { get; private set; }
		#endregion
	}
}
