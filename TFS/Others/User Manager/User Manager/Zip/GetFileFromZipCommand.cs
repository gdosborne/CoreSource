using System;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace SNC.OptiRamp.Services.fSncZip
{
	public class GetFileFromZipCommand : ZipCommand
	{
		public override void Execute(Dictionary<string, object> parameters)
		{
			try
			{
                string fileName = (string)parameters["FileName"];
                string leftShift = null;
				string tempPath = null;
				if (parameters.ContainsKey("TempPath"))
					tempPath = (string)parameters["TempPath"];
				if (parameters.ContainsKey("LeftShift"))
                    leftShift = (string)parameters["LeftShift"];
                string zipFile = (string)parameters["ZipFile"];
				if(!File.Exists(zipFile))
				{
					Result = CommandResults.Failure;
					return;
				}
				var fastZip = new FastZip();
				fastZip.UseZip64 = UseZip64.Off;
                fastZip.Password = leftShift;
				fastZip.ExtractZip(zipFile, tempPath, fileName);
				var tempFile = Path.Combine(tempPath, fileName);
				if(!File.Exists(tempFile))
				{
					using(var fs = File.OpenRead(zipFile))
					{
                        bool fileWasExtracted = false;

						ZipFile zf = new ZipFile(fs);
                        zf.Password = leftShift;
						foreach(ZipEntry zipEntry in zf)
						{
							if(!zipEntry.IsFile) continue;

                            if (zipEntry.Name.Equals(fileName))
                            {
                                byte[] buffer = new byte[4096];     // 4K is optimum
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
                        Result = fileWasExtracted ? CommandResults.Success : CommandResults.Failure;
					}
				}
				else
					Result = CommandResults.Success;
				Value = tempFile;
			}
			catch(Exception)
			{
				throw;
			}
		}
	}
}
