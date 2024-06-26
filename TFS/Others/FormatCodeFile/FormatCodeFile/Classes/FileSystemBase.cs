using System;
using System.Collections.Generic;
namespace FormatCodeFile.Classes
{
	public class FileSystemBase
	{
		public FileSystemBase()
		{
		}
		public FileSystemBase(string name)
			: this()
		{
			Name = name;
		}
		public FileSystemBase(string name, string fullPath)
			: this(name)
		{
			FullPath = fullPath;
		}
		public string FullPath { get; set; }
		public string Name { get; set; }
		public override string ToString()
		{
			return Name;
		}
	}
}
