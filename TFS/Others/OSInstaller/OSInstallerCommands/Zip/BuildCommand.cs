namespace OSInstallerCommands.Zip
{
	using OSInstallerCommands.Classes;
	using OSInstallerCommands.Classes.Events;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;

	public class BuildCommand : BaseCommand
	{
		#region Public Constructors
		public BuildCommand(IDictionary<string, object> parameters)
			: base(parameters) { }
		public BuildCommand()
			: base()
		{
			ParameterNames.Add("zipfile");
			ParameterNames.Add("itemlist");
		}
		#endregion Public Constructors

		public override event CommandStatusUpdateHandler CommandStatusUpdate;
		public override void Execute()
		{
#if SLEEP
			System.Threading.Thread.Sleep(1000);
#endif
			Status = CommandStatuses.Failure;
			if (!Parameters.ContainsKey("zipfile"))
			{
				Result = "Zip file parameter not specified";
				Status = CommandStatuses.Failure;
				return;
			}
			if (!Parameters.ContainsKey("itemlist"))
			{
				Result = "Item list not specified";
				Status = CommandStatuses.Failure;
				return;
			}
			if (!Parameters.ContainsKey("tempfilename"))
			{
				Result = "Temp file name not specified";
				Status = CommandStatuses.Failure;
				return;
			}
			var worker = new BuildWorker(Parameters);
			worker.CommandStatusUpdate += worker_CommandStatusUpdate;
			var t = Task.Factory.StartNew(() => worker.Start());

		}

		void worker_CommandStatusUpdate(object sender, CommandStatusUpdateEventArgs e)
		{
			if (CommandStatusUpdate != null)
				CommandStatusUpdate(this, e);
		}
	}
}
