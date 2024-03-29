#pragma warning disable 0067, 0108
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ICSharpCode.SharpZipLib.Zip;

namespace SNC.OptiRamp.Services.fSncZip
{
	public abstract class ZipCommand : BaseCommand
	{
        public static readonly string CONTROLLER_FILE_NAME = "Controller.xml";
		protected List<FileData> Files = null;
		private static List<FileData> _FileData = null;
		public ZipCommand()
		{
			if(Files == null)
				Files = new List<FileData>();
		}
		public override event CommandCompleteHandler CommandComplete;
		public override event CommandStartedHandler CommandStarted;
		public override event CommandStatusChangedEventHandler CommandStatusChanged;
		public override event InitializeProgressHandler InitializeProgress;
		public override event UpdateProgressHandler UpdateProgress;
		public CommandResults Result { get; protected set; }
		public object Value { get; protected set; }
		public override abstract void Execute(Dictionary<string, object> parameters);
		public List<FileData> GetFileData()
		{
			return _FileData;
		}
		public List<FileData> GetFileData(string zipFile, string leftShift)
		{
			if(_FileData != null) return _FileData;
			_FileData = new List<FileData>();
            var cData = ControllerFileData(zipFile, leftShift);
			var doc = XDocument.Parse(cData);
			foreach(var item in doc.Root.Elements())
			{
				_FileData.Add(new FileData
				{
					OriginalSource = item.Attribute("originalname").Value,
					EntryName = item.Attribute("entryname").Value
				});
			}
			return _FileData;
		}
		protected void AddDirectory(ZipFile zipFile, DirectoryInfo dInfo, bool hideFileName)
		{
			var files = dInfo.GetFiles().ToList();
			files.ForEach(x => AddFile(zipFile, x, hideFileName));
			var folders = dInfo.GetDirectories().ToList();
			folders.ForEach(x => AddDirectory(zipFile, x, hideFileName));
			ChangeStatus(this, string.Format("Folder added to package - {0}.", dInfo.Name));
		}
        protected void AddFile(ZipFile zipFile, FileInfo fInfo, bool hideFileName)
		{
			var g = Guid.NewGuid();
			var entryName = hideFileName ? g.ToString() : fInfo.Name;
			zipFile.Add(fInfo.FullName, entryName);
			Files.Add(new FileData
			{
				OriginalSource = fInfo.FullName,
				Guid = g,
				EntryName = entryName
			});
			ChangeStatus(this, string.Format("File added to package - {0}.", fInfo.Name));
		}
		protected void AddFilesToController(XDocument doc)
		{
			var root = doc.Root;
			Files.ForEach(x => root.Add(GetFileElement(x)));
		}
		protected void ChangeStatus(ZipCommand cmd, string message)
		{
			if(CommandStatusChanged != null)
				CommandStatusChanged(cmd, new CommandStatusChangedEventArgs(message));
		}
		protected string ControllerFileData(string zipFile, string leftShift)
		{
			var cmd = new GetFileFromZipCommand();
			var p = new Dictionary<string, object>();
			p.Add("ZipFile", zipFile);
            p.Add("FileName", CONTROLLER_FILE_NAME);
            p.Add("LeftShift", leftShift);
			cmd.Execute(p);
			XDocument doc = null;
			if(cmd.Result == CommandResults.Success)
			{
				using(var sr = new StreamReader((string)cmd.Value))
				{
					doc = XDocument.Parse(sr.ReadToEnd());
					sr.Close();
				}
				File.Delete((string)cmd.Value);
				return doc.ToString();
			}
			doc = new XDocument();
			var root = new XElement("files");
            doc.Add(root);
            AddFilesToController(doc);
			return doc.ToString();
		}
		protected void ZipCommandComplete(ZipCommand cmd)
		{
			if(CommandComplete != null)
				CommandComplete(cmd, EventArgs.Empty);
		}
		private XElement GetFileElement(FileData fd)
		{
			return new XElement("file",
				new XAttribute("id", fd.Guid.ToString()),
				new XAttribute("entryname", string.IsNullOrEmpty(fd.EntryName) ? string.Empty : fd.EntryName),
				new XAttribute("originalname", fd.OriginalSource));
		}
		public struct FileData
		{
			public string EntryName;
			public Guid Guid;
			public string OriginalSource;
		}
	}
}
