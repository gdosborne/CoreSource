using System;
using System.Collections.Generic;
namespace FormatCodeFile.Classes
{
	public class File : FileSystemBase
	{
		public File()
			: base()
		{
		}
		public File(string name)
			: this()
		{
			Name = name;
		}
		public File(string name, string fullPath)
			: this(name)
		{
			FullPath = fullPath;
		}
	}
}
