namespace OSInstallerCommands.Classes.Events
{
	using System;

	using System.Linq;

	public delegate void CommandStatusUpdateHandler(object sender, CommandStatusUpdateEventArgs e);

	public class CommandStatusUpdateEventArgs : EventArgs
	{
		#region Public Constructors
		public CommandStatusUpdateEventArgs(bool isComplete)
		{
			IsComplete = isComplete;
		}
		public CommandStatusUpdateEventArgs(string message, double max, double value)
		{
			Message = message;
			Max = max;
			Value = value;
		}
		#endregion Public Constructors

		#region Public Properties
		public bool IsComplete { get; private set; }
		public double Max { get; private set; }
		public string Message { get; private set; }
		public double Value { get; private set; }
		#endregion Public Properties
	}
}
