using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using ICSharpCode.SharpZipLib.Zip;

namespace SNC.OptiRamp.Services.fSncZip
{
	public class InitializeZipCommand : ZipCommand
	{
		public override void Execute(Dictionary<string, object> parameters)
		{
			string zipFileName = (string)parameters["ZipFileName"];
            string leftShift = null;
            if (parameters.ContainsKey("LeftShift"))
            {
                leftShift = (string)parameters["LeftShift"];
            }

            var controllerData = ControllerFileData(zipFileName, leftShift);

            using (var stream = new FileStream(zipFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var zipFile = ZipFile.Create(stream))
                {
                    zipFile.UseZip64 = UseZip64.Off;
                    zipFile.Password = leftShift;
                    zipFile.BeginUpdate();
                    XDocument doc = XDocument.Parse(controllerData);
                    var tmpFile = Path.GetTempFileName();
                    doc.Save(tmpFile);
                    zipFile.Add(tmpFile, CONTROLLER_FILE_NAME);
                    zipFile.CommitUpdate();
                    zipFile.Close();
                    File.Delete(tmpFile);
                }
            }
        }
	}
}
