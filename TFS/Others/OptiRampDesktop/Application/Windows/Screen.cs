using System;
using System.Collections.Generic;
using System.Linq;

namespace MyApplication.Windows
{
	public static class Screen
	{
		#region Public Properties
		public static bool HasMultipleScreens
		{
			get
			{
				return System.Windows.Forms.Screen.AllScreens.Count() > 1;
			}
		}
		public static double PrimaryScreenWidth
		{
			get
			{
				return System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
			}
		}
		#endregion
	}
}