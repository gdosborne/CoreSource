namespace OSInstallerCommands.Zip
{
	using Classes.Events;
	using ICSharpCode.SharpZipLib.Zip;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	public class InitializeZipFileCommand : BaseCommand
	{
		public override event CommandStatusUpdateHandler CommandStatusUpdate;

		#region Public Constructors
		public InitializeZipFileCommand(IDictionary<string, object> parameters)
			: base(parameters) { }
		public InitializeZipFileCommand()
			: base()
		{
			ParameterNames.Add("zipfile");
		}
		#endregion Public Constructors

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

			var zipFile = (string)Parameters["zipfile"];
			using (var stream = new FileStream(zipFile, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				using (var zip = ZipFile.Create(stream))
				{
					zip.UseZip64 = UseZip64.Off;
					zip.BeginUpdate();
					var tmpFile = Path.GetTempFileName();
					zip.CommitUpdate();
					zip.Close();
					if (File.Exists(tmpFile))
						File.Delete(tmpFile);
				}
			}
			Status = CommandStatuses.Success;
		}
	}
}
