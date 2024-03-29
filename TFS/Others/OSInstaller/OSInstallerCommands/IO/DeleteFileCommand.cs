namespace OSInstallerCommands.IO
{
	using OSInstallerCommands.Classes.Events;
	using System;
	using System.Collections.Generic;
	using System.IO;

	using System.Linq;

	public sealed class DeleteFileCommand : BaseCommand
	{
		#region Public Constructors
		public DeleteFileCommand(IDictionary<string, object> parameters)
			: base(parameters) { }
		public DeleteFileCommand()
			: base()
		{
			ParameterNames.Add("source");
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
			else if (!File.Exists((string)Parameters["source"]))
			{
				Result = "Source file is missing";
				Status = CommandStatuses.Failure;
				return;
			}
			File.Delete((string)Parameters["source"]);
			Status = CommandStatuses.Success;
		}
		#endregion Public Methods
	}
}
