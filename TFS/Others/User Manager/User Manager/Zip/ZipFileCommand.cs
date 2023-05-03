using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using ICSharpCode.SharpZipLib.Zip;

namespace SNC.OptiRamp.Services.fSncZip
{
	public class ZipFileCommand : ZipCommand
	{
		public override void Execute(Dictionary<string, object> parameters)
		{
			try
			{
				string fileName = (string)parameters["FileName"];
				string zipFileName = (string)parameters["ZipFileName"];
				string leftShift = null;
				if (parameters.ContainsKey("LeftShift"))
				{
					leftShift = (string)parameters["LeftShift"];
				}
				bool hideFileName = (bool)parameters["HideFileName"];
				bool ignoreController = false;
				if (parameters.ContainsKey("IgnoreController"))
					ignoreController = (bool)parameters["IgnoreController"];
				if (!File.Exists(fileName))
					throw new FileNotFoundException(string.Format("The file {0} does not exist.", fileName));
				ZipFile zipFile = null;
				if (!File.Exists(zipFileName))
					zipFile = ZipFile.Create(zipFileName);
				else
					zipFile = new ZipFile(zipFileName);
				zipFile.UseZip64 = UseZip64.Off;
				zipFile.Password = leftShift;
				zipFile.BeginUpdate();
				var fInfo = new FileInfo(fileName);
				AddFile(zipFile, fInfo, hideFileName);
				var tmpFile = Path.GetTempFileName();
				if (!ignoreController)
				{
					var controllerData = ControllerFileData(zipFileName, leftShift);
					XDocument doc = XDocument.Parse(controllerData);
					AddFilesToController(doc);
					zipFile.Delete(CONTROLLER_FILE_NAME);
					doc.Save(tmpFile);
					zipFile.Add(tmpFile, CONTROLLER_FILE_NAME);
				}
				zipFile.CommitUpdate();
				zipFile.Close();
				File.Delete(tmpFile);
				zipFile.Close();
				Result = CommandResults.Success;
				ZipCommandComplete(this);
			}
			catch (Exception ex)
			{
				Result = CommandResults.Failure;
				Value = ex.Message;
			}
		}
	}
}
