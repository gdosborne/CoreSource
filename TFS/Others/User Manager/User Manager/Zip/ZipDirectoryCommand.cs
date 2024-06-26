using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using ICSharpCode.SharpZipLib.Zip;

namespace SNC.OptiRamp.Services.fSncZip
{
	public class ZipDirectoryCommand : ZipCommand
	{
		public override void Execute(Dictionary<string, object> parameters)
		{
			try
			{
				string folderName = (string)parameters["FolderName"];
                string zipFileName = (string)parameters["ZipFileName"];
                string leftShift = null;
                if (parameters.ContainsKey("LeftShift"))
                {
                    leftShift = (string)parameters["LeftShift"];
                }
                bool hideFileName = (bool)parameters["HideFileName"];
				if(!Directory.Exists(folderName))
					throw new DirectoryNotFoundException(string.Format("The folder {0} does not exist.", folderName));
                var controllerData = ControllerFileData(zipFileName, leftShift);
				using(var zipFile = new ZipFile(zipFileName))
				{
					zipFile.UseZip64 = UseZip64.Off;
                    zipFile.Password = leftShift;
					zipFile.BeginUpdate();
					var dInfo = new DirectoryInfo(folderName);
					AddDirectory(zipFile, dInfo, hideFileName);
					var doc = XDocument.Parse(controllerData);
					AddFilesToController(doc);
                    zipFile.Delete(CONTROLLER_FILE_NAME);
					var tmpFile = Path.GetTempFileName();
					doc.Save(tmpFile);
                    zipFile.Add(tmpFile, CONTROLLER_FILE_NAME);
					zipFile.CommitUpdate();
					zipFile.Close();
					File.Delete(tmpFile);
				}
				Result = CommandResults.Success;
				ZipCommandComplete(this);
			}
			catch(Exception ex)
			{
				Result = CommandResults.Failure;
				Value = ex.Message;
			}
		}
	}
}
