namespace OSInstallerCommands.IO
{
	using OSInstallerCommands.Classes.Events;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	public sealed class CopyDirectoryCommand : BaseCommand
	{
		#region Public Constructors
		public CopyDirectoryCommand(IDictionary<string, object> parameters)
			: base(parameters) { }
		public CopyDirectoryCommand()
			: base()
		{
			ParameterNames.Add("source");
			ParameterNames.Add("destination");
			ParameterNames.Add("overwrite");
		}
		#endregion Public Constructors

		public override event CommandStatusUpdateHandler CommandStatusUpdate;

		#region Public Methods
		public override void Execute()
		{
#if SLEEP
			System.Threading.Thread.Sleep(1000);
#endif
			if (!Parameters.ContainsKey("source") || !Parameters.ContainsKey("destination"))
			{
				Result = "Source and/or destination parameter not specified";
				Status = CommandStatuses.Failure;
				return;
			}
			var source = (string)Parameters["source"];
			var destination = (string)Parameters["destination"];
			if (!Directory.Exists(source))
			{
				Result = "Source directory is missing";
				Status = CommandStatuses.Failure;
				return;
			}
			bool overwrite = Parameters.ContainsKey("overwrite") && (bool)Parameters["overwrite"];
			var dInfo = new DirectoryInfo(source);
			ProcessDirectory(dInfo, destination);
			Status = CommandStatuses.Success;
		}
		#endregion Public Methods

		#region Private Methods
		private void ProcessDirectory(DirectoryInfo d, string destination)
		{
			if (d.Exists)
			{
				if (!Directory.Exists(destination))
					Directory.CreateDirectory(destination);
				d.GetFiles().ToList().ForEach(x =>
				{
					var c = new CopyFileCommand(new Dictionary<string, object>
					{
						{ "source", x.FullName },
						{ "destination", Path.Combine(Path.Combine(destination, d.Name), x.Name) },
						{ "overwrite", true }
					});
					c.Execute();
				});
				d.GetDirectories().ToList().ForEach(x =>
				{
					ProcessDirectory(x, Path.Combine(destination, x.Name));
				});
			}
		}
		#endregion Private Methods
	}
}
