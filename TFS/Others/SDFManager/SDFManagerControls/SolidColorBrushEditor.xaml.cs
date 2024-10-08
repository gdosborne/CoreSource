namespace SDFManagerControls
{
	using GregOsborne.Application.Primitives;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;

	public delegate void BrushChangedHandler(object sender, BrushChangedEventArgs e);

	public class BrushChangedEventArgs : EventArgs
	{
		#region Public Constructors
		public BrushChangedEventArgs(Brush brush)
		{
			Brush = brush;
		}
		#endregion

		#region Public Properties
		public Brush Brush { get; private set; }
		#endregion
	}

	public partial class SolidColorBrushEditor : UserControl
	{
		#region Public Constructors
		public SolidColorBrushEditor()
		{
			InitializeComponent();
		}
		#endregion

		#region Private Methods
		private static void onLabelChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (SolidColorBrushEditor)source;
			if (src == null)
				return;
			var value = (string)e.NewValue;
			src.TitleLabel.Content = value;
		}
		private static void onValueBrushChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (SolidColorBrushEditor)source;
			if (src == null)
				return;
			var value = (Brush)e.NewValue;
			src.ValueRectangle.Fill = value;
			src.TheColorPicker.SelectedColor = value.As<SolidColorBrush>().Color;
		}
		private void TheColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
		{
			ValueRectangle.Fill = new SolidColorBrush(e.NewValue.Value);
			if (BrushChanged != null)
				BrushChanged(this, new BrushChangedEventArgs(new SolidColorBrush(e.NewValue.Value)));
		}
		#endregion

		#region Public Events
		public event BrushChangedHandler BrushChanged;
		#endregion

		#region Public Fields
		public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(SolidColorBrushEditor), new PropertyMetadata("Title", onLabelChanged));
		public static readonly DependencyProperty ValueBrushProperty = DependencyProperty.Register("ValueBrush", typeof(Brush), typeof(SolidColorBrushEditor), new PropertyMetadata(new SolidColorBrush(Colors.Transparent), onValueBrushChanged));
		#endregion

		#region Public Properties
		public string Label
		{
			get { return (string)GetValue(LabelProperty); }
			set { SetValue(LabelProperty, value); }
		}
		public Brush ValueBrush
		{
			get { return (Brush)GetValue(ValueBrushProperty); }
			set { SetValue(ValueBrushProperty, value); }
		}
		#endregion

		

	}
}
