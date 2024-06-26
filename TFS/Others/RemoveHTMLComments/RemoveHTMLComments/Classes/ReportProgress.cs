namespace ProcessSourceFiles.Classes
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public delegate void ReportProgressEventHandler(object sender, ReportProgressEventArgs e);

	public class ReportProgressEventArgs : EventArgs
	{
		#region Public Constructors
		public ReportProgressEventArgs(int value, string fileName)
		{
			Value = value;
			FileName = fileName;
		}
		#endregion Public Constructors

		#region Public Properties
		public string FileName { get; private set; }
		public int Value { get; private set; }
		#endregion Public Properties
	}
}
