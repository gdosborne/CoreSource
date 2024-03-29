namespace OSInstallerCommands.IO
{
	using OSInstallerCommands.Classes.Events;
	using System;
	using System.Collections.Generic;
	using System.IO;

	using System.Linq;

	public sealed class CopyFileCommand : BaseCommand
	{
		#region Public Constructors
		public CopyFileCommand(IDictionary<string, object> parameters)
			: base(parameters) { }
		public CopyFileCommand()
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
			if (!File.Exists(source))
			{
				Result = "Source file is missing";
				Status = CommandStatuses.Failure;
				return;
			}
			var dir = Path.GetDirectoryName(destination);
			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);
			bool overwrite = Parameters.ContainsKey("overwrite") && (bool)Parameters["overwrite"];
			File.Copy(source, destination, overwrite);
			Status = CommandStatuses.Success;
		}
		#endregion Public Methods
	}
}
