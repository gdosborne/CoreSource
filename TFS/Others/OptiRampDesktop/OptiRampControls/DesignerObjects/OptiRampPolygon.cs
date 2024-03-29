using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OptiRampControls.Classes;

namespace OptiRampControls.DesignerObjects
{
	public class OptiRampPolygon : ShapeElement
	{
		#region Public Constructors

		public OptiRampPolygon()
		{
			Properties.SetValue<IEnumerable<Point>>("OrderedPoints", new List<Point>());
			Properties.SetValue<Size>("MinimumSize", new Size(20, 20));
		}

		#endregion

		#region Public Properties
		public override bool IsMovable
		{
			get { return true; }
		}
		public override ImageSource ObjectImageSource { get { return new BitmapImage(new Uri("pack://application:,,,/OptiRampControls;component/Images/polygon.png")); } }
		public IEnumerable<Point> OrderedPoints { get { return Properties.GetValue<IEnumerable<Point>>("OrderedPoints"); } set { Properties.SetValue<IEnumerable<Point>>("OrderedPoints", value); } }
		public override ShapeTypes Type
		{
			get { return ShapeTypes.Polygon; }
		}
		public override string TypeName
		{
			get { return this.GetType().Name; }
		}
		#endregion
	}
}