using System;
using System.Collections.Generic;
using System.Linq;
namespace ProcessSourceFiles.Classes
{
	public delegate void ReportProgressEventHandler(object sender, ReportProgressEventArgs e);
	public class ReportProgressEventArgs : EventArgs
	{
		public ReportProgressEventArgs(int value, string fileName)
		{
			Value = value;
			FileName = fileName;
		}
		public string FileName { get; private set; }
		public int Value { get; private set; }
	}
}
