namespace SDFManager
{
	using System;
	using System.Windows.Controls;
	using System.Linq;
	using SDFManagerSupport;

	public delegate void TableSelectedHandler(object sender, TableSelectedEventArgs e);
	public class TableSelectedEventArgs : EventArgs
	{
		public TableSelectedEventArgs(TableDefinition table)
		{
			Table = table;
		}
		public TableDefinition Table { get; private set; }
	}
}
