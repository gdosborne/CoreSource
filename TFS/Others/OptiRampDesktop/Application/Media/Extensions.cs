using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace MyApplication.Media
{
	public static class Extensions
	{
		#region Public Methods

		public static Color ToColor(this string value)
		{
			Color result = Colors.Transparent;
			if (value.StartsWith("#"))
			{
				byte a = 255;
				byte r = 255;
				byte g = 255;
				byte b = 255;
				if (value.Length == 9)
				{
					a = (byte)(Convert.ToUInt32(value.Substring(1, 2), 16));
					r = (byte)(Convert.ToUInt32(value.Substring(3, 2), 16));
					g = (byte)(Convert.ToUInt32(value.Substring(5, 2), 16));
					b = (byte)(Convert.ToUInt32(value.Substring(7, 2), 16));
					result = Color.FromArgb(a, r, g, b);
				}
				else if (value.Length == 7)
				{
					r = (byte)(Convert.ToUInt32(value.Substring(1, 2), 16));
					g = (byte)(Convert.ToUInt32(value.Substring(3, 2), 16));
					b = (byte)(Convert.ToUInt32(value.Substring(5, 2), 16));
					result = Color.FromArgb(a, r, g, b);
				}
			}
			return result;
		}

		#endregion
	}
}