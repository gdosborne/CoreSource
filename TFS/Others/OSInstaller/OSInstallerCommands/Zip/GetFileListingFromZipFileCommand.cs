namespace OSInstallerCommands.Zip
{
	using ICSharpCode.SharpZipLib.Zip;
	using OSInstallerCommands.Classes.Events;
	using System;
	using System.Collections.Generic;
	using System.IO;

	using System.Linq;

	public class GetFileListingFromZipFileCommand : BaseCommand
	{
		#region Public Constructors
		public GetFileListingFromZipFileCommand(IDictionary<string, object> parameters)
			: base(parameters) { }
		public GetFileListingFromZipFileCommand()
			: base()
		{
			ParameterNames.Add("zipfile");
			ParameterNames.Add("result");
		}
		#endregion Public Constructors

		public override event CommandStatusUpdateHandler CommandStatusUpdate;
		public override void Execute()
		{
#if SLEEP
			System.Threading.Thread.Sleep(1000);
#endif
			if (!Parameters.ContainsKey("zipfile"))
			{
				Result = "Zip file parameter not specified";
				Status = CommandStatuses.Failure;
				return;
			}
			if (!Parameters.ContainsKey("result") || Parameters["result"].GetType() != typeof(Dictionary<string, bool>))
			{
				Result = "Result parameter not specified or result type is invalid";
				Status = CommandStatuses.Failure;
				return;
			}
			Status = CommandStatuses.Failure;
			var temp = new Dictionary<string, bool>();
			var zipFile = (string)Parameters["zipfile"];
			using (var fs = File.OpenRead(zipFile))
			{
				var zf = new ZipFile(fs);
				foreach (ZipEntry zipEntry in zf)
				{
					temp.Add(zipEntry.Name, zipEntry.IsDirectory);
				}
				zf.IsStreamOwner = true;
				zf.Close();
				Parameters["result"] = temp;
			}
			Status = CommandStatuses.Success;
		}
	}
}
