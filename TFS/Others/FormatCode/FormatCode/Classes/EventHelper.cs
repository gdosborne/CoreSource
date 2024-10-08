using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FormatCode.Classes
{
	public delegate void AskHandler(object sender, AskEventArgs e);
	public delegate void CloseHandler(object sender, CloseEventArgs e);
	public delegate void DisplayExceptionHandler(object sender, DisplayExceptionEventArgs e);
	public class CloseEventArgs : EventArgs
	{
		public CloseEventArgs(bool? dialogResult)
		{
			DialogResult = dialogResult;
		}
		public bool? DialogResult { get; private set; }
	}
	public class AskEventArgs : EventArgs
	{
		public bool? Answer { get; set; }
	}
	public class DisplayExceptionEventArgs : EventArgs
	{
		public DisplayExceptionEventArgs(Exception ex)
		{
			Ex = ex;
		}
		public Exception Ex { get; private set; }
	}
}
