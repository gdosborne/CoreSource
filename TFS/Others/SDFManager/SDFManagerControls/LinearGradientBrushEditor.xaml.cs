namespace SDFManagerControls
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Data;
	using System.Windows.Documents;
	using System.Windows.Input;
	using System.Windows.Media;
	using System.Windows.Media.Imaging;
	using System.Windows.Navigation;
	using System.Windows.Shapes;
    using GregOsborne.Application.Primitives;

    public partial class LinearGradientBrushEditor : UserControl
	{
		public LinearGradientBrushEditor()
		{
			InitializeComponent();
		}

		#region Label
		public string Label
		{
			get { return (string)GetValue(LabelProperty); }
			set { SetValue(LabelProperty, value); }
		}

		public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(LinearGradientBrushEditor), new PropertyMetadata("Title", onLabelChanged));
		private static void onLabelChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (LinearGradientBrushEditor)source;
			if (src == null)
				return;
			var value = (string)e.NewValue;
			src.TitleLabel.Content = value;
		}
		#endregion

		#region ValueBrush
		public Brush ValueBrush
		{
			get { return (Brush)GetValue(ValueBrushProperty); }
			set { SetValue(ValueBrushProperty, value); }
		}

		public static readonly DependencyProperty ValueBrushProperty = DependencyProperty.Register("ValueBrush", typeof(Brush), typeof(LinearGradientBrushEditor), new PropertyMetadata(new SolidColorBrush(Colors.Black), onValueBrushChanged));
		private static void onValueBrushChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (LinearGradientBrushEditor)source;
			if (src == null)
				return;
			var value = (Brush)e.NewValue;
			src.ValueRectangle.Fill = value;
			src.SetColorSlider();
		}
		#endregion
		private void SetColorSlider()
		{
			LinearGradientBrush v = new LinearGradientBrush(ValueBrush.As<LinearGradientBrush>().GradientStops, ValueBrush.As<LinearGradientBrush>().StartPoint, ValueBrush.As<LinearGradientBrush>().EndPoint);
			if (v.StartPoint.Y < v.EndPoint.Y)
			{
				v.StartPoint = new Point(0, 0.5);
				v.EndPoint = new Point(1, 0.5);
			}
			ColorSlider.Background = v;
		}
		#region Orientation
		public Orientation Orientation
		{
			get { return (Orientation)GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value); }
		}

		public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(LinearGradientBrushEditor), new PropertyMetadata(Orientation.Vertical, onOrientationChanged));
		private static void onOrientationChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (LinearGradientBrushEditor)source;
			if (src == null)
				return;
			var value = (Orientation)e.NewValue;
			src.VertRadioButton.IsChecked = value == Orientation.Vertical;
			src.HorzRadioButton.IsChecked = value == Orientation.Horizontal;
			src.SetColorSlider();
		}
		#endregion

	}
}
