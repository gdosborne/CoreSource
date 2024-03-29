using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OptiRampControls.Classes;

namespace OptiRampControls.DesignerObjects
{
	public class OptiRampLine : ShapeElement
	{
		#region Public Constructors

		public OptiRampLine()
		{
			IsFillable = false;
			Properties.SetValue<Size>("MinimumSize", new Size(10, 3));
		}

		#endregion

		#region Public Properties
		public override bool IsMovable
		{
			get { return true; }
		}
		public override ImageSource ObjectImageSource { get { return new BitmapImage(new Uri("pack://application:,,,/OptiRampControls;component/Images/line.png")); } }
		public override ShapeTypes Type
		{
			get { return ShapeTypes.Line; }
		}
		public override string TypeName
		{
			get { return this.GetType().Name; }
		}
		#endregion
	}
}