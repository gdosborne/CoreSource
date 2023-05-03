namespace OSInstallerCommands.Zip
{
	using ICSharpCode.SharpZipLib.Core;
	using ICSharpCode.SharpZipLib.Zip;
	using OSInstallerCommands.Classes.Events;
	using OSInstallerCommands.IO;
	using System;
	using System.Collections.Generic;
	using System.IO;

	using System.Linq;

	public class GetFileFromZipFileCommand : BaseCommand
	{
		#region Public Constructors
		public GetFileFromZipFileCommand(IDictionary<string, object> parameters)
			: base(parameters) { }
		public GetFileFromZipFileCommand()
			: base()
		{
			ParameterNames.Add("zipfile");
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
			if (!Parameters.ContainsKey("zipfile"))
			{
				Result = "Zip file parameter not specified";
				Status = CommandStatuses.Failure;
				return;
			}
			if (!Parameters.ContainsKey("source"))
			{
				Result = "Source parameter not specified";
				Status = CommandStatuses.Failure;
				return;
			}
			var zipFile = (string)Parameters["zipfile"];
			var fileName = (string)Parameters["source"];
			if (!File.Exists(zipFile))
			{
				Result = "Zip file is missing";
				Status = CommandStatuses.Failure;
				return;
			}
			var tempName = fileName.Replace("\\", "/");
			var tempDir = Path.Combine(Path.GetTempPath(), Path.GetDirectoryName(tempName));
			if (!string.IsNullOrEmpty(tempDir) && !Directory.Exists(tempDir))
				Directory.CreateDirectory(tempDir);
			var fastZip = new FastZip();
			fastZip.UseZip64 = UseZip64.Off;
			fastZip.ExtractZip(zipFile, Path.GetTempPath(), tempName);
			var tempFile = Path.Combine(Path.GetTempPath(), fileName);
			if (!File.Exists(tempFile))
			{
				using (var fs = File.OpenRead(zipFile))
				{
					bool fileWasExtracted = false;

					ZipFile zf = new ZipFile(fs);
					foreach (ZipEntry zipEntry in zf)
					{
						if (!zipEntry.IsFile) continue;

						if (zipEntry.Name.Equals(tempName))
						{
							byte[] buffer = new byte[4096];
							Stream zipStream = zf.GetInputStream(zipEntry);
							using (FileStream streamWriter = File.Create(tempFile))
							{
								StreamUtils.Copy(zipStream, streamWriter, buffer);
							}
							fileWasExtracted = true;
							break;
						}
					}
					zf.IsStreamOwner = true;
					zf.Close();
					Status = fileWasExtracted ? CommandStatuses.Success : CommandStatuses.Failure;
				}
			}
			Parameters["source"] = tempFile;
			Parameters.Add("overwrite", true);
			var cmd = new CopyFileCommand(Parameters);
			cmd.Execute();

			TempFiles.Add(tempFile);

			Status = cmd.Status;
			Result = cmd.Result;
		}
		#endregion Public Methods
	}
}
