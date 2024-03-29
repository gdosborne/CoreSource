using System;
using System.Collections.Generic;
using System.Windows.Media;
using OptiRampControls.DesignerObjects;

namespace OptiRampControls.Classes
{
	public abstract class ShapeElement : OptiRampElement
	{
		#region Public Constructors

		public ShapeElement()
		{
			Properties.SetValue<Brush>("Stroke", Defaults.Stroke);
			Properties.SetValue<double>("StrokeThickness", Defaults.StrokeThickness);
			Properties.SetValue<Brush>("Fill", Defaults.Fill);
			IsFillable = true;
		}

		#endregion

		#region Public Enums

		public enum ShapeTypes
		{
			Connection,
			Line,
			Rectangle,
			Ellipse,
			Polygon,
			Triangle
		}

		#endregion

		#region Public Properties
		public Brush Fill
		{
			get { return Properties.GetValue<Brush>("Fill"); }
			set { Properties.SetValue<Brush>("Fill", value); }
		}
		public bool IsFillable { get; protected set; }
		public override abstract bool IsMovable { get; }
		public override abstract ImageSource ObjectImageSource { get; }
		public Brush Stroke
		{
			get { return Properties.GetValue<Brush>("Stroke"); }
			set { Properties.SetValue<Brush>("Stroke", value); }
		}
		public double StrokeThickness
		{
			get { return Properties.GetValue<double>("StrokeThickness"); }
			set { Properties.SetValue<double>("StrokeThickness", value); }
		}
		public abstract ShapeTypes Type { get; }
		public override abstract string TypeName { get; }
		#endregion
	}
}