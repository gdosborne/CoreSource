using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OptiRampControls.Classes;

namespace OptiRampControls.DesignerObjects
{
	public class OptiRampRectangle : ShapeElement
	{
		#region Public Constructors

		public OptiRampRectangle()
		{
			Properties.SetValue<double>("CornerRadius", 0.0);
			Properties.SetValue<Size>("MinimumSize", new Size(20, 20));
		}

		#endregion

		#region Public Properties
		public double CornerRadius { get { return Properties.GetValue<double>("CornerRadius"); } set { Properties.SetValue<double>("CornerRadius", value); } }
		public override bool IsMovable
		{
			get { return true; }
		}
		public override ImageSource ObjectImageSource { get { return new BitmapImage(new Uri("pack://application:,,,/OptiRampControls;component/Images/rectangle.png")); } }
		public override ShapeTypes Type
		{
			get { return ShapeTypes.Rectangle; }
		}
		public override string TypeName
		{
			get { return this.GetType().Name; }
		}
		#endregion
	}
}