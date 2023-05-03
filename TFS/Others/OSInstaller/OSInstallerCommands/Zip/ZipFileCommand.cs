namespace OSInstallerCommands.Zip
{
	using Classes.Events;
	//using ICSharpCode.SharpZipLib.Zip;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.IO.Compression;
	using System.Threading.Tasks;


	public class ZipFileCommand : BaseCommand
	{
		public override event CommandStatusUpdateHandler CommandStatusUpdate;

		#region Public Constructors
		public ZipFileCommand(IDictionary<string, object> parameters)
			: base(parameters) { }
		public ZipFileCommand()
			: base()
		{
			ParameterNames.Add("zipfile");
			ParameterNames.Add("filename");
			ParameterNames.Add("fileid");
		}
		#endregion Public Constructors
		private object _Locker = new object();
		public override void Execute()
		{
#if SLEEP
			System.Threading.Thread.Sleep(100);
#endif
			if (!Parameters.ContainsKey("zipfile"))
			{
				Result = "Zip file parameter not specified";
				Status = CommandStatuses.Failure;
				return;
			}
			if (!Parameters.ContainsKey("filename"))
			{
				Result = "File name parameter not specified";
				Status = CommandStatuses.Failure;
				return;
			}
			if (!Parameters.ContainsKey("fileid"))
			{
				Result = "File id parameter not specified";
				Status = CommandStatuses.Failure;
				return;
			}
			var zipFile = (string)Parameters["zipfile"];
			var fileName = (string)Parameters["filename"];
			var fileId = (string)Parameters["fileid"];
			if (!File.Exists(fileName))
			{
				Result = "File name does not exist";
				Status = CommandStatuses.Failure;
				return;
			}
			lock (_Locker)
			{
				if (CommandStatusUpdate != null)
					CommandStatusUpdate(this, new CommandStatusUpdateEventArgs(fileName, 0, 0));
				var t = Task.Factory.StartNew(() => CopyFile(zipFile, fileName));
				t.Wait();
			}
			Status = CommandStatuses.Success;
		}

		private void CopyFile(string zipFile, string fileName)
		{
			ZipArchiveMode mode = !File.Exists(zipFile) ? ZipArchiveMode.Create : ZipArchiveMode.Update;
			ZipArchive zip = ZipFile.Open(zipFile, mode);
			zip.CreateEntryFromFile(fileName, Path.GetFileName(fileName), CompressionLevel.Optimal);
			zip.Dispose();
		}
	}
}
