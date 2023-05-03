using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using MyApplication.Primitives;
using OptiRampControls.DesignerObjects;

namespace OptiRampControls.Classes.ElementHelpers
{
	public static class ShapeHelper
	{
		#region Public Methods

		public static FrameworkElement CreateShape(ShapeElement element)
		{
			FrameworkElement result = null;
			switch (element.Type)
			{
				case ShapeElement.ShapeTypes.Line:
					result = GetLine(element);
					break;
				case ShapeElement.ShapeTypes.Ellipse:
					result = GetEllipse(element);
					break;
				case ShapeElement.ShapeTypes.Rectangle:
					result = GetRectangle(element);
					break;
				case ShapeElement.ShapeTypes.Triangle:
					result = GetTriangle(element);
					break;
				case ShapeElement.ShapeTypes.Polygon:
					result = GetPolygon(element);
					break;
			}
			return result;
		}

		public static Ellipse GetEllipse(ShapeElement element)
		{
			var result = new Ellipse
			{
				Width = element.Size.Width == 0 ? element.StrokeThickness : element.Size.Width,
				Height = element.Size.Height == 0 ? element.StrokeThickness : element.Size.Height,
				Stroke = element.Stroke,
				StrokeThickness = element.StrokeThickness
			};
			if (element.IsFillable)
				result.Fill = element.Fill;
			result.RenderTransform = new RotateTransform { Angle = element.Rotation, CenterX = element.Size.Width / 2, CenterY = element.Size.Height / 2 };
			if (element.As<OptiRampElement>().HasDropShadow)
				result.Effect = GetDropShadow(element.Rotation);
			result.SetValue(Canvas.LeftProperty, element.Location.X);
			result.SetValue(Canvas.TopProperty, element.Location.Y);
			return result;
		}

		public static Line GetLine(ShapeElement element)
		{
			var result = new Line
			{
				Width = element.Size.Width == 0 ? element.StrokeThickness : element.Size.Width,
				Height = element.Size.Height == 0 ? element.StrokeThickness : element.Size.Height,
				Stroke = element.Stroke,
				StrokeThickness = element.StrokeThickness,
				X1 = 0,
				Y1 = 0,
				X2 = element.Size.Width,
				Y2 = element.Size.Height
			};
			result.RenderTransform = new RotateTransform { Angle = element.Rotation, CenterX = element.Size.Width / 2, CenterY = element.Size.Height / 2 };
			if (element.As<OptiRampElement>().HasDropShadow)
				result.Effect = GetDropShadow(element.Rotation);
			result.SetValue(Canvas.LeftProperty, element.Location.X);
			result.SetValue(Canvas.TopProperty, element.Location.Y);
			return result;
		}

		public static Polygon GetPolygon(ShapeElement element)
		{
			double offset = 0;
			var result = new Polygon
			{
				Stroke = element.Stroke,
				StrokeThickness = element.StrokeThickness,
			};
			if (element.IsFillable)
				result.Fill = element.Fill;
			element.As<OptiRampPolygon>().OrderedPoints.ToList().ForEach(x => result.Points.Add(x));
			if (element.As<OptiRampElement>().HasDropShadow)
				result.Effect = GetDropShadow(element.Rotation);
			var minWidth = element.As<OptiRampPolygon>().OrderedPoints.Min(x => x.X) + (2 * element.StrokeThickness);
			var minHeight = element.As<OptiRampPolygon>().OrderedPoints.Min(x => x.Y) + (2 * element.StrokeThickness);
			var maxWidth = element.As<OptiRampPolygon>().OrderedPoints.Max(x => x.X) + (2 * element.StrokeThickness);
			var maxHeight = element.As<OptiRampPolygon>().OrderedPoints.Max(x => x.Y) + (2 * element.StrokeThickness);
			result.Width = maxWidth + minWidth;
			result.Height = maxHeight + minHeight;
			if (element.As<OptiRampElement>().HasDropShadow)
			{
				result.Width += 10;
				result.Height += 10;
			}
			element.Size = new Size(result.Width, result.Height);
			result.RenderTransform = new RotateTransform { Angle = element.Rotation, CenterX = element.Size.Width / 2, CenterY = element.Size.Height / 2 };
			result.SetValue(Canvas.LeftProperty, element.Location.X);
			result.SetValue(Canvas.TopProperty, element.Location.Y);
			return result;
		}

		public static Rectangle GetRectangle(ShapeElement element)
		{
			var result = new Rectangle
			{
				Width = element.Size.Width == 0 ? element.StrokeThickness : element.Size.Width,
				Height = element.Size.Height == 0 ? element.StrokeThickness : element.Size.Height,
				Stroke = element.Stroke,
				StrokeThickness = element.StrokeThickness,
				RadiusX = element.As<OptiRampRectangle>().CornerRadius,
				RadiusY = element.As<OptiRampRectangle>().CornerRadius
			};
			if (element.IsFillable)
				result.Fill = element.Fill;
			result.RenderTransform = new RotateTransform { Angle = element.Rotation, CenterX = element.Size.Width / 2, CenterY = element.Size.Height / 2 };
			if (element.As<OptiRampElement>().HasDropShadow)
				result.Effect = GetDropShadow(element.Rotation);
			result.SetValue(Canvas.LeftProperty, element.Location.X);
			result.SetValue(Canvas.TopProperty, element.Location.Y);
			return result;
		}

		public static Polygon GetTriangle(ShapeElement element)
		{
			double offset = 0;
			if (element.As<OptiRampElement>().HasDropShadow)
			{
				element.Size = new Size(element.Size.Width + 20, element.Size.Height + 20);
				offset = 10;
			}
			var result = new Polygon
			{
				Width = element.Size.Width == 0 ? element.StrokeThickness : element.Size.Width,
				Height = element.Size.Height == 0 ? element.StrokeThickness : element.Size.Height,
				Stroke = element.Stroke,
				StrokeThickness = element.StrokeThickness,
			};
			if (element.IsFillable)
				result.Fill = element.Fill;
			result.RenderTransform = new RotateTransform { Angle = element.Rotation, CenterX = element.Size.Width / 2, CenterY = element.Size.Height / 2 };
			result.Points.Add(new Point(offset, offset));
			var midPoint = new Point(element.Size.Width - offset, (element.Size.Height - offset) / 2);
			result.Points.Add(midPoint);
			var endPoint = new Point(offset, element.Size.Height - offset);
			result.Points.Add(endPoint);
			if (element.As<OptiRampElement>().HasDropShadow)
				result.Effect = GetDropShadow(element.Rotation);
			result.SetValue(Canvas.LeftProperty, element.Location.X);
			result.SetValue(Canvas.TopProperty, element.Location.Y);
			return result;
		}

		#endregion

		#region Private Methods

		private static DropShadowEffect GetDropShadow(double rotation)
		{
			var result = new DropShadowEffect
			{
				BlurRadius = 5,
				Color = Colors.Black,
				ShadowDepth = 5,
				Direction = 315 + rotation
			};
			return result;
		}

		#endregion
	}
}