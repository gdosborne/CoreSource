using System;
using System.Linq;
namespace KHSound
{
	internal class LocalFileInfo
	{
		public System.IO.FileInfo FileInfo { get; set; }
		public string Path { get { return FileInfo.FullName; } }
		public bool Selected { get; set; }
	}
}
