using System;
using System.Windows.Controls;
namespace ProcessSourceFiles.Classes
{
	public delegate void AddControlEventHandler(object sender, AddControlEventArgs e);
	public class AddControlEventArgs : EventArgs
	{
		public AddControlEventArgs(Control control, string tabName, string parentName)
		{
			Control = control;
			TabName = tabName;
			ParentName = parentName;
		}
		public Control Control { get; private set; }
		public string ParentName { get; private set; }
		public string TabName { get; private set; }
	}
}
