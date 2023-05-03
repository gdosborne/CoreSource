namespace OSControls {
	using System;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;
	using System.Windows.Shapes;
	using GregOsborne.Application.Windows;

	public partial class ValueDisplay : UserControl {
		#region Public Constructors
		public ValueDisplay() => this.InitializeComponent();
		#endregion Public Constructors

		#region Public Methods
		public void Refresh() {
			try {
				if (this.ActualWidth == 0.0 || double.IsNaN(this.ActualWidth)) {
					return;
				}

				this.HGrid.Children.Clear();
				if (this.IndicatorOrientation == IndicatorOrientations.Horizontal) {
					this.HorizontalGrid.Background = this.Background;
					var partSize = this.ActualWidth / 100;
					var valueSize = (this.Maximum - this.Minimum) / 100.0;
					var lineCount = 0;
					var majorLinePosition = 0;
					var showMajorTicks = this.MajorTicksAtPercent > 0 && this.MajorTicksAtPercent < 100;
					if (showMajorTicks) {
						majorLinePosition = Convert.ToInt32(100 * (this.MajorTicksAtPercent / 100));
					}

					for (var i = 1; i < 100; i++) {
						lineCount++;
						var currentPosition = i * partSize;
						if (this.MarkerVisibility == System.Windows.Visibility.Visible) {
							var size = 0.0;
							if (showMajorTicks && (lineCount % majorLinePosition) == 0) {
								size = this.MajorTickThickness;
							} else if (this.ShowMinorTicks) {
								size = this.MinorTickThickness;
							}

							if (size > 0.0) {
								this.HGrid.Children.Add
								(
									new Line {
										X1 = currentPosition,
										Y1 = 0,
										X2 = currentPosition,
										Y2 = this.ActualHeight,
										Stroke = Foreground,
										StrokeThickness = size
									}
								);
							}
						}
						if (this.ScaleVisibility == System.Windows.Visibility.Visible && (showMajorTicks && (lineCount % majorLinePosition) == 0)) {
							var currentValue = valueSize * i;
							var tb = new TextBlock {
								Text = currentValue.ToString("G6"),
								VerticalAlignment = VerticalAlignment.Bottom,
								FontSize = FontSize,
								Foreground = ScaleForeground
							};
							var tbSize = currentValue.ToString("G6").Measure(this.FontFamily, this.FontSize, this.FontStyle, this.FontWeight);
							tb.Margin = new Thickness(currentPosition - (tbSize.Width / 2), 0, 0, 0);
							this.HGrid.Children.Add(tb);
						}
					}
				}
			}
			catch (Exception ex) {
				throw new ApplicationException("testing", ex);
			}
		}
		#endregion Public Methods

		#region Private Methods
		private static void onBackgroundChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ValueDisplay)source;
			if (src == null) {
				return;
			}

			var value = (Brush)e.NewValue;
			src.Refresh();
		}
		private static void onFontSizeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ValueDisplay)source;
			if (src == null) {
				return;
			}

			var value = (double)e.NewValue;
			src.Refresh();
		}
		private static void onForegroundChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ValueDisplay)source;
			if (src == null) {
				return;
			}

			var value = (Brush)e.NewValue;
			src.Refresh();
		}
		private static void onGlassVisibilityChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ValueDisplay)source;
			if (src == null) {
				return;
			}

			var value = (Visibility)e.NewValue;
			src.UpdateValue();
		}
		private static void onIndicatorOrientationChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ValueDisplay)source;
			if (src == null) {
				return;
			}

			var value = (IndicatorOrientations)e.NewValue;
			src.UpdateValue();
		}
		private static void onIndicatorTypeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ValueDisplay)source;
			if (src == null) {
				return;
			}

			var value = (IndicatorTypes)e.NewValue;
			src.UpdateValue();
		}
		private static void onLineIndicatorSizeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ValueDisplay)source;
			if (src == null) {
				return;
			}

			var value = (double)e.NewValue;
			src.UpdateValue();
		}
		private static void onMajorTicksAtPercentChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ValueDisplay)source;
			if (src == null) {
				return;
			}

			var value = (double)e.NewValue;
			src.Refresh();
		}
		private static void onMajorTickThicknessChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ValueDisplay)source;
			if (src == null) {
				return;
			}

			var value = (double)e.NewValue;
			src.Refresh();
		}
		private static void onMarkerVisibilityChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ValueDisplay)source;
			if (src == null) {
				return;
			}

			var value = (Visibility)e.NewValue;
			src.Refresh();
		}
		private static void onMaximumChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ValueDisplay)source;
			if (src == null) {
				return;
			}

			var value = (double)e.NewValue;
			src.Refresh();
		}
		private static void onMinimumChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ValueDisplay)source;
			if (src == null) {
				return;
			}

			var value = (double)e.NewValue;
			src.Refresh();
		}
		private static void onMinorTickThicknessChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ValueDisplay)source;
			if (src == null) {
				return;
			}

			var value = (double)e.NewValue;
			src.Refresh();
		}
		private static void onScaleForegroundChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ValueDisplay)source;
			if (src == null) {
				return;
			}

			var value = (Brush)e.NewValue;
			src.Refresh();
		}
		private static void onScaleVisibilityChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ValueDisplay)source;
			if (src == null) {
				return;
			}

			var value = (Visibility)e.NewValue;
			src.Refresh();
		}
		private static void onShowMinorTicksChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ValueDisplay)source;
			if (src == null) {
				return;
			}

			var value = (bool)e.NewValue;
			src.Refresh();
		}
		private static void onValueChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ValueDisplay)source;
			if (src == null) {
				return;
			}

			var value = (double)e.NewValue;
			src.UpdateValue();
		}
		private static void onValueIndicatorChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ValueDisplay)source;
			if (src == null) {
				return;
			}

			var value = (Brush)e.NewValue;
			src.UpdateValue();
		}
		private void UpdateValue() {
			if (this.ActualWidth == 0.0 || double.IsNaN(this.ActualWidth)) {
				return;
			}

			var pctPosition = this.Value / (this.Maximum - this.Minimum);
			if (this.IndicatorOrientation == IndicatorOrientations.Horizontal) {
				this.HorizontalGrid.Visibility = Visibility.Visible;
				this.VerticalGrid.Visibility = Visibility.Collapsed;
				var valuePosition = this.ActualWidth * pctPosition;
				if (this.IndicatorType == IndicatorTypes.Line) {
					if (this.HRectangleMarker.Visibility == Visibility.Visible) {
						this.HRectangleMarker.Visibility = Visibility.Collapsed;
						this.HRectangleGlass.Visibility = Visibility.Collapsed;
					}
					if (this.HLineMarker.Visibility == Visibility.Collapsed) {
						this.HLineMarker.Visibility = Visibility.Visible;
					}

					this.HLineMarker.X1 = valuePosition;
					this.HLineMarker.X2 = valuePosition;
					this.HLineMarker.StrokeThickness = this.LineIndicatorSize;
					this.HLineMarker.Stroke = this.ValueIndicator;
				} else if (this.IndicatorType == IndicatorTypes.Solid) {
					if (this.HLineMarker.Visibility == Visibility.Visible) {
						this.HLineMarker.Visibility = Visibility.Collapsed;
					}

					if (this.HRectangleMarker.Visibility == Visibility.Collapsed) {
						this.HRectangleMarker.Visibility = Visibility.Visible;
					}

					this.HRectangleMarker.Width = valuePosition;
					if (this.HRectangleGlass.Visibility != this.GlassVisibility) {
						this.HRectangleGlass.Visibility = this.GlassVisibility;
					}

					this.HRectangleGlass.Visibility = this.GlassVisibility;
					this.HRectangleGlass.Width = valuePosition;
					this.HRectangleMarker.Fill = this.ValueIndicator;
				}
			} else {
				this.HorizontalGrid.Visibility = Visibility.Collapsed;
				this.VerticalGrid.Visibility = Visibility.Visible;
			}
		}

		private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e) {
			this.Refresh();
			this.UpdateValue();
		}
		#endregion Private Methods

		#region Public Fields
		public static new readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(ValueDisplay), new PropertyMetadata(new SolidColorBrush(Colors.Transparent), onBackgroundChanged));
		public static new readonly DependencyProperty FontSizeProperty = DependencyProperty.Register("FontSize", typeof(double), typeof(ValueDisplay), new PropertyMetadata(6.0, onFontSizeChanged));
		public static new readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(ValueDisplay), new PropertyMetadata(new SolidColorBrush(Colors.Black), onForegroundChanged));
		public static readonly DependencyProperty GlassVisibilityProperty = DependencyProperty.Register("GlassVisibility", typeof(Visibility), typeof(ValueDisplay), new PropertyMetadata(Visibility.Visible, onGlassVisibilityChanged));
		public static readonly DependencyProperty IndicatorOrientationProperty = DependencyProperty.Register("IndicatorOrientation", typeof(IndicatorOrientations), typeof(ValueDisplay), new PropertyMetadata(IndicatorOrientations.Vertical, onIndicatorOrientationChanged));
		public static readonly DependencyProperty IndicatorTypeProperty = DependencyProperty.Register("IndicatorType", typeof(IndicatorTypes), typeof(ValueDisplay), new PropertyMetadata(IndicatorTypes.Line, onIndicatorTypeChanged));
		public static readonly DependencyProperty LineIndicatorSizeProperty = DependencyProperty.Register("LineIndicatorSize", typeof(double), typeof(ValueDisplay), new PropertyMetadata(2.0, onLineIndicatorSizeChanged));
		public static readonly DependencyProperty MajorTicksAtPercentProperty = DependencyProperty.Register("MajorTicksAtPercent", typeof(double), typeof(ValueDisplay), new PropertyMetadata(10.0, onMajorTicksAtPercentChanged));
		public static readonly DependencyProperty MajorTickThicknessProperty = DependencyProperty.Register("MajorTickThickness", typeof(double), typeof(ValueDisplay), new PropertyMetadata(1.0, onMajorTickThicknessChanged));
		public static readonly DependencyProperty MarkerVisibilityProperty = DependencyProperty.Register("MarkerVisibility", typeof(Visibility), typeof(ValueDisplay), new PropertyMetadata(Visibility.Visible, onMarkerVisibilityChanged));
		public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(double), typeof(ValueDisplay), new PropertyMetadata(100.0, onMaximumChanged));
		public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(double), typeof(ValueDisplay), new PropertyMetadata(0.0, onMinimumChanged));
		public static readonly DependencyProperty MinorTickThicknessProperty = DependencyProperty.Register("MinorTickThickness", typeof(double), typeof(ValueDisplay), new PropertyMetadata(0.5, onMinorTickThicknessChanged));
		public static readonly DependencyProperty ScaleForegroundProperty = DependencyProperty.Register("ScaleForeground", typeof(Brush), typeof(ValueDisplay), new PropertyMetadata(new SolidColorBrush(Colors.Black), onScaleForegroundChanged));
		public static readonly DependencyProperty ScaleVisibilityProperty = DependencyProperty.Register("ScaleVisibility", typeof(Visibility), typeof(ValueDisplay), new PropertyMetadata(Visibility.Visible, onScaleVisibilityChanged));
		public static readonly DependencyProperty ShowMinorTicksProperty = DependencyProperty.Register("ShowMinorTicks", typeof(bool), typeof(ValueDisplay), new PropertyMetadata(false, onShowMinorTicksChanged));
		public static readonly DependencyProperty ValueIndicatorProperty = DependencyProperty.Register("ValueIndicator", typeof(Brush), typeof(ValueDisplay), new PropertyMetadata(new SolidColorBrush(Colors.Red), onValueIndicatorChanged));
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(ValueDisplay), new PropertyMetadata(50.0, onValueChanged));
		#endregion Public Fields

		#region Public Enums
		public enum IndicatorOrientations {
			Horizontal,
			Vertical
		}
		public enum IndicatorTypes {
			Line,
			Solid
		}
		#endregion Public Enums

		#region Public Properties
		public new Brush Background {
			get => (Brush)this.GetValue(BackgroundProperty);
			set => this.SetValue(BackgroundProperty, value);
		}
		public new double FontSize {
			get => (double)this.GetValue(FontSizeProperty);
			set => this.SetValue(FontSizeProperty, value);
		}
		public new Brush Foreground {
			get => (Brush)this.GetValue(ForegroundProperty);
			set => this.SetValue(ForegroundProperty, value);
		}
		public Visibility GlassVisibility {
			get => (Visibility)this.GetValue(GlassVisibilityProperty);
			set => this.SetValue(GlassVisibilityProperty, value);
		}
		public IndicatorOrientations IndicatorOrientation {
			get => (IndicatorOrientations)this.GetValue(IndicatorOrientationProperty);
			set => this.SetValue(IndicatorOrientationProperty, value);
		}
		public IndicatorTypes IndicatorType {
			get => (IndicatorTypes)this.GetValue(IndicatorTypeProperty);
			set => this.SetValue(IndicatorTypeProperty, value);
		}
		public double LineIndicatorSize {
			get => (double)this.GetValue(LineIndicatorSizeProperty);
			set => this.SetValue(LineIndicatorSizeProperty, value);
		}
		public double MajorTicksAtPercent {
			get => (double)this.GetValue(MajorTicksAtPercentProperty);
			set => this.SetValue(MajorTicksAtPercentProperty, value);
		}
		public double MajorTickThickness {
			get => (double)this.GetValue(MajorTickThicknessProperty);
			set => this.SetValue(MajorTickThicknessProperty, value);
		}
		public Visibility MarkerVisibility {
			get => (Visibility)this.GetValue(MarkerVisibilityProperty);
			set => this.SetValue(MarkerVisibilityProperty, value);
		}
		public double Maximum {
			get => (double)this.GetValue(MaximumProperty);
			set => this.SetValue(MaximumProperty, value);
		}
		public double Minimum {
			get => (double)this.GetValue(MinimumProperty);
			set => this.SetValue(MinimumProperty, value);
		}
		public double MinorTickThickness {
			get => (double)this.GetValue(MinorTickThicknessProperty);
			set => this.SetValue(MinorTickThicknessProperty, value);
		}
		public Brush ScaleForeground {
			get => (Brush)this.GetValue(ScaleForegroundProperty);
			set => this.SetValue(ScaleForegroundProperty, value);
		}
		public Visibility ScaleVisibility {
			get => (Visibility)this.GetValue(ScaleVisibilityProperty);
			set => this.SetValue(ScaleVisibilityProperty, value);
		}
		public bool ShowMinorTicks {
			get => (bool)this.GetValue(ShowMinorTicksProperty);
			set => this.SetValue(ShowMinorTicksProperty, value);
		}
		public double Value {
			get => (double)this.GetValue(ValueProperty);
			set => this.SetValue(ValueProperty, value);
		}
		public Brush ValueIndicator {
			get => (Brush)this.GetValue(ValueIndicatorProperty);
			set => this.SetValue(ValueIndicatorProperty, value);
		}
		#endregion Public Properties
	}
}
