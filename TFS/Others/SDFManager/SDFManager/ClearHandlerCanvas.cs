namespace SDFManager
{
	using System;

	using System.Linq;

	public delegate void ClearCanvasHandler(object sender, ClearCanvasEventArgs e);

	public class ClearCanvasEventArgs : EventArgs
	{
		#region Public Constructors
		public ClearCanvasEventArgs()
		{
		}
		#endregion Public Constructors
	}
}
