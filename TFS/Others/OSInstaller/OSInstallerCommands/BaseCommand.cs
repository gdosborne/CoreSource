namespace OSInstallerCommands
{
	using OSInstallerCommands.Classes.Events;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public enum CommandStatuses
	{
		None,
		Failure,
		Success
	}

	public abstract class BaseCommand
	{

		#region Public Constructors
		static BaseCommand()
		{
			TempFiles = new List<string>();
		}
		public BaseCommand(IDictionary<string, object> parameters)
			: this()
		{
			Parameters = parameters;
		}
		public BaseCommand()
		{
			Commands = new List<BaseCommand>();
			ParameterNames = new List<string>();
		}
		#endregion Public Constructors

		#region Public Methods
		public virtual void Execute()
		{
		}
		public virtual void Execute(IDictionary<string, object> parameters)
		{
			Parameters = parameters;
			Execute();
		}
		#endregion Public Methods

		#region Public Events
		public abstract event CommandStatusUpdateHandler CommandStatusUpdate;
		#endregion Public Events

		#region Public Properties
		public static IList<string> TempFiles { get; private set; }
		public List<BaseCommand> Commands { get; private set; }
		public string Message { get; set; }
		public IList<string> ParameterNames { get; private set; }
		public IDictionary<string, object> Parameters { get; set; }
		public string Result { get; protected set; }
		public int Sequence { get; set; }
		public CommandStatuses Status { get; protected set; }
		#endregion Public Properties
	}
}
