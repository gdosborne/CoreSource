using System;
using System.Collections.Generic;
namespace FormatCodeFile.Classes
{
	public class Folder : FileSystemBase
	{
		public Folder()
			: base()
		{
			Folders = new List<FileSystemBase>();
			Files = new List<FileSystemBase>();
		}
		public Folder(string name)
			: this()
		{
			Name = name;
		}
		public Folder(string name, string fullPath)
			: this(name)
		{
			FullPath = fullPath;
		}
		public IEnumerable<FileSystemBase> Files { get; set; }
		public IEnumerable<FileSystemBase> Folders { get; set; }
	}
}
