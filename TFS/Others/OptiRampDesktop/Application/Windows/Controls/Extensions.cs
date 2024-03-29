using System;
using System.Collections.Generic;
using System.Windows;

namespace MyApplication.Windows.Controls
{
	public static class Extensions
	{
		#region Public Methods

		public static void RemoveOverflow(this System.Windows.Controls.ToolBar value)
		{
			var overflowGrid = value.Template.FindName("OverflowGrid", value) as FrameworkElement;
			if (overflowGrid != null)
				overflowGrid.Visibility = Visibility.Collapsed;
		}

		#endregion
	}
}