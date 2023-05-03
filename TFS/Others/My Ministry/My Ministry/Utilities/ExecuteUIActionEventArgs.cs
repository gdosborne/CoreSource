namespace MyMinistry.Utilities
{
	using System.Collections.Generic;

	public delegate void ExecuteUIActionHandler(object sender, ExecuteUIActionEventArgs e);
	public class ExecuteUIActionEventArgs
	{
		public ExecuteUIActionEventArgs(string commandToExecute, Dictionary<string, object> parameters)
		{
			CommandToExecute = commandToExecute;
			Parameters = parameters;
		}

		public string CommandToExecute { get; private set; }
		public Dictionary<string, object> Parameters { get; private set; }
	}
}
