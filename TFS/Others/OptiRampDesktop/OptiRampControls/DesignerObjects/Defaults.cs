using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace OptiRampControls.DesignerObjects
{
	internal static class Defaults
	{
		#region Public Fields
		public static double CornerRadius = 0.0;
		public static Brush Fill = new SolidColorBrush(Colors.Black);
		public static bool HasDropShadow = false;
		public static Point Location = new Point(0, 0);
		public static double Rotation = 0.0;
		public static Size Size = new Size(100, 100);
		public static Brush Stroke = new SolidColorBrush(Colors.Black);
		public static double StrokeThickness = 0.0;
		#endregion
	}
}