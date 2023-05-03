namespace SDFManager
{
	using System;
	using System.Linq;
	using System.Windows;

	public delegate void AddControlHandler(object sender, AddControlEventArgs e);

	public class AddControlEventArgs : EventArgs
	{
		#region Public Constructors
		public AddControlEventArgs(UIElement element, Point location, Size size)
		{
			Element = element;
			Location = location;
			Size = size;
		}
		#endregion Public Constructors

		#region Public Properties
		public UIElement Element { get; private set; }
		public Point Location { get; private set; }
		public Size Size { get; set; }
		#endregion Public Properties
	}
}
