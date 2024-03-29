using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
namespace SharpSounder.Convert
{
	public class Mp3Converter : IConverter
	{
		public Mp3Converter(string lamePath)
		{
			LamePath = lamePath;
		}
		public string LamePath { get; set; }
		public void Convert(string fileName)
		{
			Convert(new FileInfo(fileName), false);
		}
		public void Convert(string fileName, bool noWait)
		{
			Convert(new FileInfo(fileName), noWait);
		}
		private string _FinalFileName = null;
		private string _OriginalFileName = null;
		public void Convert(FileInfo file, bool noWait)
		{
			if(!file.Exists)
				throw new FileNotFoundException("Could not find file", file.FullName);
			_OriginalFileName = file.FullName;
			var folderName = file.DirectoryName;
			var fileBase = Path.GetFileNameWithoutExtension(file.FullName);
			_FinalFileName = Path.Combine(folderName, string.Format("{0}.mp3", fileBase));
			var count = 0;
			while(File.Exists(_FinalFileName))
			{
				_FinalFileName = Path.Combine(folderName, string.Format("{0}_{1}.mp3", fileBase, count));
				count++;
			}
			var p = new Process
			{
				EnableRaisingEvents = true,
				StartInfo = new ProcessStartInfo
				{
					FileName = LamePath,
					Arguments = string.Format("--abr 128 \"{0}\" \"{1}\"", file.FullName, _FinalFileName),
					WindowStyle = ProcessWindowStyle.Hidden,
					WorkingDirectory = Path.GetDirectoryName(LamePath)
				}
			};
			p.Start();
			if(noWait)
			{
				p.Exited += new EventHandler(p_Exited);
				return;
			}
			p.WaitForExit();
			file.Delete();
		}
		public void Convert(FileInfo file)
		{
			Convert(file, false);
		}
		private void p_Exited(object sender, EventArgs e)
		{
			var p = sender as Process;
			if(p.ExitCode != 0)
				throw new ApplicationException(string.Format("lame conversion error ({0})", p.ExitCode));
			File.Delete(_OriginalFileName);
		}
	}
}
