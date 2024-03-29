namespace Imaginator.Data
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class FileItem
	{
		public string Name { get; set; }
		public string FullPath { get; set; }
		public static FileItem Create(string path)
		{
			if (!System.IO.File.Exists(path))
				return null;
			var result = new FileItem
			{
				FullPath = path,
				Name = System.IO.Path.GetFileName(path)
			};
			return result;
		}
	}
}
