namespace OSInstallerCommands.Zip
{
	using ICSharpCode.SharpZipLib.Zip;
	using OSInstallerCommands.Classes.Events;
	using System;
	using System.Collections.Generic;
	using System.IO;

	using System.Linq;

	public class CopyInstallationFilesCommand : BaseCommand
	{
		#region Public Constructors
		public CopyInstallationFilesCommand(IDictionary<string, object> parameters)
			: base(parameters) { }
		public CopyInstallationFilesCommand()
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
			Status = CommandStatuses.Failure;
			var items = (IDictionary<string, string>)Parameters["itemlist"];
			var zipFile = (string)Parameters["zipfile"];
			int itemNumber = 0;
			using (var fs = File.OpenRead(zipFile))
			{
				var zf = new ZipFile(fs);
				foreach (ZipEntry zipEntry in zf)
				{
					if (CommandStatusUpdate != null)
						CommandStatusUpdate(this, new CommandStatusUpdateEventArgs(string.Format("Installing {0}", zipEntry.Name), items.Count, Convert.ToDouble(itemNumber)));

					itemNumber++;
				}
				zf.IsStreamOwner = true;
				zf.Close();
			}
			Status = CommandStatuses.Success;
		}
	}
}
