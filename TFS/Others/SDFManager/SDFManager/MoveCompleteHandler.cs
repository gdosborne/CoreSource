namespace SDFManager
{
	using SDFManagerSupport;
	using System;
	using System.Linq;
	using System.Windows;

	public delegate void MoveCompleteHandler(object sender, MoveCompleteEventArgs e);

	public class MoveCompleteEventArgs : EventArgs
	{
		#region Public Constructors
		public MoveCompleteEventArgs(TableDefinition tableDef, Point location)
		{
			TableDef = tableDef;
			Location = location;
		}
		#endregion Public Constructors

		#region Public Properties
		public Point Location { get; private set; }
		public TableDefinition TableDef { get; private set; }
		#endregion Public Properties
	}
}
