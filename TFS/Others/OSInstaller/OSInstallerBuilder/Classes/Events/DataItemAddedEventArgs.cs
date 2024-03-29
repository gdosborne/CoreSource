namespace OSInstallerBuilder.Classes.Events
{
	using System;
	using System.Linq;

	public delegate void DataItemAddedHandler(object sender, DataItemAddedEventArgs e);

	public class DataItemAddedEventArgs : EventArgs
	{
		#region Public Constructors
		public DataItemAddedEventArgs(object item)
		{
			Item = item;
		}
		#endregion Public Constructors

		#region Public Properties
		public object Item { get; private set; }
		#endregion Public Properties
	}
}
