namespace OSControls.Classes.Events
{
	using System;

	using System.Linq;

	public delegate void QueryAddNewItemHandler(object sender, QueryAddNewItemEventArgs e);

	public class QueryAddNewItemEventArgs : EventArgs
	{
		#region Public Properties
		public object TheNewItem { get; set; }
		#endregion Public Properties
	}
}
