using System;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace SNC.OptiRamp.Services.fSncZip
{
	public class UnzipAllCommand : ZipCommand
	{
		public override event CommandStatusChangedEventHandler CommandStatusChanged;
		public override void Execute(Dictionary<string, object> parameters)
		{
			Result = CommandResults.Failure;
			try
			{
                string zipFile = (string)parameters["ZipFile"];
                string leftShift = null;
                if (parameters.ContainsKey("LeftShift"))
                    leftShift = (string)parameters["LeftShift"];
				if (!File.Exists(zipFile))
					return;
				var fileData = GetFileData(zipFile, leftShift);
				var directoryName = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
				if(CommandStatusChanged != null)
					CommandStatusChanged(this, new CommandStatusChangedEventArgs(string.Format("Temp folder = {0}", directoryName)));
				if(!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				using(var fs = File.OpenRead(zipFile))
				{
					ZipFile zf = new ZipFile(fs);
                    zf.Password = leftShift;
					foreach(ZipEntry zipEntry in zf)
					{
						if(!zipEntry.IsFile) continue;
						UnzipFile(zf, zipEntry, directoryName);
					}
					zf.Close();
					Value = directoryName;
					Result = CommandResults.Success;
				}
			}
			catch(Exception ex)
			{
				Value = ex.Message;
				return;
			}
		}
		private void UnzipFile(ZipFile zf, ZipEntry zipEntry, string directoryName)
		{
			var entryFileName = zipEntry.Name;
			byte[] buffer = new byte[4096];
			var zipStream = zf.GetInputStream(zipEntry);
			var tempFile = Path.Combine(directoryName, entryFileName);
			using(var streamWriter = File.Create(tempFile))
			{
				StreamUtils.Copy(zipStream, streamWriter, buffer);
				streamWriter.Close();
			}
		}
	}
}
