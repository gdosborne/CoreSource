namespace OSInstallerCommands.IO
{
	using OSInstallerCommands.Classes.Events;
	using System;
	using System.Collections.Generic;
	using System.IO;

	using System.Linq;

	public class CreateOutputDirectoryCommand : BaseCommand
	{
		public CreateOutputDirectoryCommand(IDictionary<string, object> parameters)
			: base(parameters) { }
		public CreateOutputDirectoryCommand()
			: base()
		{
			ParameterNames.Add("outputdirectory");
		}
		public override event CommandStatusUpdateHandler CommandStatusUpdate;
		public override void Execute()
		{
#if SLEEP
			System.Threading.Thread.Sleep(1000);
#endif
			Status = CommandStatuses.Success;
			if (!Parameters.ContainsKey("outputdirectory"))
			{
				Result = "Output directory not specified";
				Status = CommandStatuses.Failure;
				return;
			}
			if (!Directory.Exists((string)Parameters["outputdirectory"]))
			{
				try
				{
					Directory.CreateDirectory((string)Parameters["outputdirectory"]);
					Status = CommandStatuses.Success;
				}
				catch (Exception ex)
				{
					Status = CommandStatuses.Failure;
					Result = ex.Message;
				}
			}
		}
	}
}
