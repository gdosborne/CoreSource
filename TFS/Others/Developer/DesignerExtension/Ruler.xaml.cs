// -----------------------------------------------------------------------
// Copyright© 2016 Statistcs & Controls, Inc.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Ruler.xaml.cs
//
namespace SNC.OptiRamp.Application.Developer.Extensions.DesignerExtension {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;
	using System.Windows.Shapes;

	public partial class Ruler : UserControl {

		#region Public Constructors
		public Ruler() {
			InitializeComponent();
			Refresh();
		}
		#endregion Public Constructors

		#region Private Methods
		private static void onForegroundChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (Ruler)source;
			if (src == null)
				return;
			var value = (Brush)e.NewValue;
			src.Refresh();
		}
		private static void onOrientationChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (Ruler)source;
			if (src == null)
				return;
			var value = (Orientation)e.NewValue;
			src.Refresh();
		}
		private void Refresh() {
			RulerCanvas.Children.Clear();
			if (Orientation == Orientation.Horizontal) {
				for (int i = 10; i < 10000; i += 10) {
					var line = new Line {
						X1 = i,
						X2 = i,
						Y1 = -2.0,
						Y2 = 5.0,
						Stroke = Foreground,
						StrokeThickness = 0.5
					};
					if ((i % 100) == 0)
						line.Y2 = 10.0;
					else if ((i % 50) == 0)
						line.Y2 = 7.5;
					RulerCanvas.Children.Add(line);
				}
			}
			else {
				for (int i = 10; i < 10000; i += 10) {
					var line = new Line {
						X1 = -2.0,
						X2 = 5.0,
						Y1 = i,
						Y2 = i,
						Stroke = Foreground,
						StrokeThickness = 0.5
					};
					if ((i % 100) == 0)
						line.X2 = 10.0;
					else if ((i % 50) == 0)
						line.X2 = 7.5;
					RulerCanvas.Children.Add(line);
				}
			}
		}
		#endregion Private Methods

		#region Public Fields
		public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(Ruler), new PropertyMetadata(new SolidColorBrush(Colors.Black), onForegroundChanged));
		public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(Ruler), new PropertyMetadata(Orientation.Horizontal, onOrientationChanged));
		#endregion Public Fields

		#region Public Properties
		public Brush Foreground {
			get {
				return (Brush)GetValue(ForegroundProperty);
			}
			set {
				SetValue(ForegroundProperty, value);
			}
		}
		public Orientation Orientation {
			get {
				return (Orientation)GetValue(OrientationProperty);
			}
			set {
				SetValue(OrientationProperty, value);
			}
		}
		#endregion Public Properties
	}
}
