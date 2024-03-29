namespace OSInstallerCommands.IO
{
	using OSInstallerCommands.Classes.Events;
	using System;
	using System.Collections.Generic;
	using System.IO;

	using System.Linq;

	public sealed class DeleteDirectoryCommand : BaseCommand
	{
		#region Public Constructors
		public DeleteDirectoryCommand(IDictionary<string, object> parameters)
			: base(parameters) { }
		public DeleteDirectoryCommand()
			: base()
		{
			ParameterNames.Add("source");
			ParameterNames.Add("recursive");
		}
		#endregion Public Constructors

		public override event CommandStatusUpdateHandler CommandStatusUpdate;

		#region Public Methods
		public override void Execute()
		{
#if SLEEP
			System.Threading.Thread.Sleep(1000);
#endif
			if (!Parameters.ContainsKey("source"))
			{
				Result = "Source parameter not specified";
				Status = CommandStatuses.Failure;
				return;
			}
			else if (!Directory.Exists((string)Parameters["source"]))
			{
				Result = "Source directory is missing";
				Status = CommandStatuses.Failure;
				return;
			}
			bool recursive = !Parameters.ContainsKey("recursive") || (Parameters.ContainsKey("recursive") && (bool)Parameters["recursive"]);
			Directory.Delete((string)Parameters["source"], recursive);
			Status = CommandStatuses.Success;
		}
		#endregion Public Methods
	}
}
