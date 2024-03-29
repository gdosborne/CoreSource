using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OptiRampControls.Classes;

namespace OptiRampControls.DesignerObjects
{
	public class OptiRampConnection : ShapeElement, IConnectable
	{
		#region Public Constructors

		public OptiRampConnection()
		{
			IsFillable = false;
			Properties.SetValue<Point>("Element1ConnectionPoint", new Point(0, 0));
			Properties.SetValue<Point>("Element2ConnectionPoint", new Point(0, 0));
			Properties.SetValue<Size>("MinimumSize", new Size(10, 3));
		}

		#endregion

		#region Public Properties
		public Point Element1ConnectionPoint { get { return Properties.GetValue<Point>("Element1ConnectionPoint"); } set { Properties.SetValue<Point>("Element1ConnectionPoint", value); } }
		public Point Element2ConnectionPoint { get { return Properties.GetValue<Point>("Element2ConnectionPoint"); } set { Properties.SetValue<Point>("Element2ConnectionPoint", value); } }
		public override bool IsMovable
		{
			get { return false; }
		}
		public override ImageSource ObjectImageSource { get { return new BitmapImage(new Uri("pack://application:,,,/OptiRampControls;component/Images/connector.png")); } }
		public override ShapeTypes Type
		{
			get { return ShapeTypes.Connection; }
		}
		public override string TypeName
		{
			get { return this.GetType().Name; }
		}
		#endregion
	}
}