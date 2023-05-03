using System;
using System.IO;
using System.Linq;
namespace SharpSounder.Convert
{
	public interface IConverter
	{
		void Convert(string fileName);
		void Convert(string fileName, bool noWait);
		void Convert(FileInfo file);
		void Convert(FileInfo file, bool noWait);
	}
}
