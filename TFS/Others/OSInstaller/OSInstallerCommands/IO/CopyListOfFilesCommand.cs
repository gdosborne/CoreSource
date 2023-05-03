namespace OSInstallerCommands.IO
{
	using OSInstallerCommands.Classes.Events;
	using System;
	using System.Collections.Generic;
	using System.IO;

	using System.Linq;

	public class CopyListOfFilesCommand : BaseCommand
	{
		public CopyListOfFilesCommand(IDictionary<string, object> parameters)
			: base(parameters) { }
		public CopyListOfFilesCommand()
			: base()
		{
			ParameterNames.Add("filelist");
			ParameterNames.Add("outputdirectory");
		}
		public override event CommandStatusUpdateHandler CommandStatusUpdate;
		public override void Execute()
		{
#if SLEEP
			System.Threading.Thread.Sleep(1000);
#endif
			if (!Parameters.ContainsKey("filelist"))
			{
				Result = "List of files not specified";
				Status = CommandStatuses.Failure;
				return;
			}
			if (!Parameters.ContainsKey("outputdirectory"))
			{
				Result = "Output directory not specified";
				Status = CommandStatuses.Failure;
				return;
			}
			var listOfFiles = Parameters["filelist"] as List<string>;
			var parms = new Dictionary<string, object>
			{
				{ "source ", string.Empty },
				{ "destination", string.Empty },
				{ "overwrite", true }
			};
			Status = CommandStatuses.Success;
			foreach (var item in listOfFiles)
			{
				parms["source"] = item;
				parms["destination"] = Path.Combine((string)Parameters["outputdirectory"], Path.GetFileName(item));
				var cmd = new CopyFileCommand(null);
				cmd.Execute(parms);
				if (cmd.Status == CommandStatuses.Success)
					continue;
				Result = cmd.Result;
				Status = cmd.Status;
				break;
			}
		}
	}
}
