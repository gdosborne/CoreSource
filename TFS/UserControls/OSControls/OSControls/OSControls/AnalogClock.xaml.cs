namespace OSControls
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;
	using System.Windows.Media.Effects;
	using System.Windows.Media.Imaging;
	using System.Windows.Shapes;
	using System.Windows.Threading;

	public partial class AnalogClock : UserControl
	{
		#region Public Constructors
		public AnalogClock()
		{
			InitializeComponent();
			ClockFaceSource = BackgroundImage.Source;
			HubEllipse.Width = 20;
			HubEllipse.Height = 20;
			GlassImage.Width = 20;
			GlassImage.Height = 20;
			DateOffset = 50;
			if (DesignerProperties.GetIsInDesignMode(this))
				return;

			timeTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(50) };
			timeTimer.Tick += controlTimer_Tick;
			timeTimer.Start();
		}
		#endregion Public Constructors

		#region Public Methods
		public void Refresh(DateTime currentTime)
		{
			var centerPoint = new Point(HandsCanvas.ActualWidth / 2, HandsCanvas.ActualHeight / 2);
			TheMoon.CurrentDate = currentTime;
			DrawMinutesHand(currentTime);
			DrawHoursHand(currentTime);
			DrawSecondsHand(currentTime);
		}
		#endregion Public Methods

		#region Internal Methods
		internal ImageSource GetDefaultImageSource()
		{
			return new BitmapImage(new Uri(@"pack://application:,,,/OSControls;component/Themes/DefaultClockFace.png", UriKind.Absolute));
		}
		#endregion Internal Methods

		#region Private Methods
		private static void onActivityBrushChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (Brush)e.NewValue;
			src.MyValueDisplay.ValueIndicator = value;
		}
		private static void onActivityHeightChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			src.MyValueDisplay.Height = value;
		}
		private static void onActivityMarginChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (Thickness)e.NewValue;
			src.MyValueDisplay.Margin = value;
		}
		private static void onActivityValueChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			src.MyValueDisplay.Value = value;
		}
		private static void onActivityVisibilityChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (Visibility)e.NewValue;
			src.MyValueDisplay.Visibility = value;
		}
		private static void onActivityWidthChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			src.MyValueDisplay.Width = value;
		}
		private static void onClockFaceSourceChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (ImageSource)e.NewValue;
			if (value == null)
				value = src.GetDefaultImageSource();
			src.BackgroundImage.Source = value;
		}

		private static void onClockSizeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			src.BackgroundImage.Width = value;
			src.BackgroundImage.Height = value;
		}

		private static void onDateOffsetChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			src.MonthBorder.Margin = new Thickness(value, 0, 0, 0);
			src.DayBorder.Margin = new Thickness(0, 0, value, 0);
		}

		private static void onDateVisibilityChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (Visibility)e.NewValue;
			src.DayBorder.Visibility = value;
			src.MonthBorder.Visibility = value;
		}

		private static void onDropShadowColorChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (Color)e.NewValue;
			src.DropShadowColor = value;
			if (src.SelectedTimeZone != null)
				ResetHands(src);
		}

		private static void onFontFamilyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (FontFamily)e.NewValue;
			src.DayTextBlock.FontFamily = value;
			src.MonthTextBlock.FontFamily = value;
		}

		private static void onFontSizeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			src.DayTextBlock.FontSize = value;
			src.MonthTextBlock.FontSize = value;
		}

		private static void onFontStyleChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (FontStyle)e.NewValue;
			src.DayTextBlock.FontStyle = value;
			src.MonthTextBlock.FontStyle = value;
		}

		private static void onFontWeightChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (FontWeight)e.NewValue;
			src.DayTextBlock.FontWeight = value;
			src.MonthTextBlock.FontWeight = value;
		}

		private static void onForegroundChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (Brush)e.NewValue;
			src.DayTextBlock.Foreground = value;
			src.MonthTextBlock.Foreground = value;
		}

		private static void onHandsBrushChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (Brush)e.NewValue;
			src.HandsBrush = value;
			if (src.hoursHand != null)
				src.hoursHand.Stroke = value;
			if (src.minutesHand != null)
				src.minutesHand.Stroke = value;
			ResetHands(src);
		}

		private static void onHandShortenAmountChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			src.HandShortenAmount = value;
			ResetHands(src);
		}

		private static void onHandThicknessChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			src.HandThickness = value;
			ResetHands(src);
		}

		private static void onHubBrushChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (Brush)e.NewValue;
			src.HubEllipse.Fill = value;
		}

		private static void onHubSizeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			src.HubEllipse.Width = value;
			src.HubEllipse.Height = value;
			src.GlassImage.Width = value;
			src.GlassImage.Height = value;
		}

		private static void onMoonMarginChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (Thickness)e.NewValue;
			src.TheMoon.Margin = value;
		}

		private static void onMoonOpacityChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			src.TheMoon.Opacity = value;
		}

		private static void onMoonPhaseVisibilityChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (Visibility)e.NewValue;
			src.TheMoon.Visibility = value;
		}

		private static void onMoonSizeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			src.TheMoon.Width = value;
			src.TheMoon.Height = value;
		}

		private static void onPaddingChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (Thickness)e.NewValue;
			src.LayoutRoot.Margin = value;
			ResetHands(src);
		}

		private static void onSecondsBrushChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (Brush)e.NewValue;
			src.SecondsBrush = value;
			if (src.secondsHand != null)
				src.secondsHand.Stroke = value;
			ResetHands(src);
		}

		private static void onSecondsVisibilityChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (Visibility)e.NewValue;
			src.SecondsVisibility = value;
			ResetHands(src);
		}

		private static void onSelectedTimeZoneChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (TimeZoneInfo)e.NewValue;
			src.SelectedTimeZone = value;
			src.ToolTip = value.ToString();
			ResetHands(src);
		}

		private static void onShowHandsDropShadowChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (bool)e.NewValue;
			src.ShowHandsDropShadow = value;
			ResetHands(src);
		}

		private static void onSmoothSecondsHandChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (AnalogClock)source;
			if (src == null)
				return;
			var value = (bool)e.NewValue;
			ResetHands(src);
		}

		private static void ResetHands(AnalogClock src)
		{
			if (src.SelectedTimeZone == null)
				return;
			var currentTime = DateTime.Now.ToUniversalTime() + src.SelectedTimeZone.GetUtcOffset(DateTime.Now);
			src.RemoveLine(src.minutesHand);
			src.RemoveLine(src.hoursHand);
			src.RemoveLine(src.secondsHand);
			src.secondsHand = null;
			src.hoursHand = null;
			src.minutesHand = null;
			src.DrawHoursHand(currentTime);
			src.DrawMinutesHand(currentTime);
			src.DrawSecondsHand(currentTime);
		}

		private void controlTimer_Tick(object sender, EventArgs e)
		{
			if (SelectedTimeZone == null)
				return;
			var currentTime = DateTime.Now.ToUniversalTime() + SelectedTimeZone.GetUtcOffset(DateTime.Now);
			MonthTextBlock.Text = MonthAbbreviation(currentTime);
			DayTextBlock.Text = currentTime.Day.ToString();
			Refresh(currentTime);
		}

		private void DrawHoursHand(DateTime currentTime)
		{
			//RemoveLine(hoursHand);
			double angleSize = maxAngle / 12;
			double hour = currentTime.Hour < 13 ? currentTime.Hour : currentTime.Hour - 12;
			hour += currentTime.Minute / 60.0;
			var radius = GetHandRadius(.4);
			var outerPoint = GetPointOnCircle((maxAngle + 90) - (hour * angleSize), radius, CenterPoint);

			if (outerPoint.X == oldOuterPoint.X && outerPoint.Y == oldOuterPoint.Y)
				return;
			if (hoursHand == null)
			{
				if (CenterPoint.X == 0.0 || CenterPoint.Y == 0.0)
					return;
				hoursHand = GetLine(HandsBrush, 3 * HandThickness, outerPoint);
				HandsCanvas.Children.Add(hoursHand);
			}
			else
			{
				hoursHand.Y2 = outerPoint.Y;
				hoursHand.X2 = outerPoint.X;
			}
		}

		private void DrawMinutesHand(DateTime currentTime)
		{
			//RemoveLine(minutesHand);
			double minute = currentTime.Minute + (currentTime.Second / 60.0);
			var radius = GetHandRadius(.2);
			var outerPoint = GetPointOnCircle((maxAngle + 90) - (minute * pointAngle), radius, CenterPoint);

			if (outerPoint.X == oldOuterPoint.X && outerPoint.Y == oldOuterPoint.Y)
				return;
			if (minutesHand == null)
			{
				if (CenterPoint.X == 0.0 || CenterPoint.Y == 0.0)
					return;
				minutesHand = GetLine(HandsBrush, 2 * HandThickness, outerPoint);
				HandsCanvas.Children.Add(minutesHand);
			}
			else
			{
				minutesHand.Y2 = outerPoint.Y;
				minutesHand.X2 = outerPoint.X;
			}
		}

		private void DrawSecondsHand(DateTime currentTime)
		{
			//RemoveLine(secondsHand);
			var radius = GetHandRadius(.1);
			Point outerPoint = new Point(0, 0);
			double angle = (maxAngle + 90) - (currentTime.Second * pointAngle);
			//SecondHandImage.RenderTransform = new RotateTransform(angle);
			if (SmoothSecondsHand)
			{
				var ms = currentTime.Millisecond / 1000.0;
				double second = currentTime.Second + ms;
				outerPoint = GetPointOnCircle((maxAngle + 90) - (second * pointAngle), radius, CenterPoint);
			}
			else
				outerPoint = GetPointOnCircle((maxAngle + 90) - (currentTime.Second * pointAngle), radius, CenterPoint);

			if (outerPoint.X == oldOuterPoint.X && outerPoint.Y == oldOuterPoint.Y)
				return;
			if (secondsHand == null)
			{
				if (CenterPoint.X == 0.0 || CenterPoint.Y == 0.0)
					return;
				secondsHand = GetLine(SecondsBrush, 1 * HandThickness, outerPoint);
				secondsHand.Visibility = SecondsVisibility;
				HandsCanvas.Children.Add(secondsHand);
			}
			else
			{
				secondsHand.Y2 = outerPoint.Y;
				secondsHand.X2 = outerPoint.X;
			}
		}

		private DropShadowEffect GetDropShadow()
		{
			return new DropShadowEffect
			{
				Color = DropShadowColor,
				BlurRadius = 5.0,
				Direction = 0.0,
				ShadowDepth = 4.0
			};
		}

		private double GetHandRadius(double pctOffset)
		{
			var result = HandsCanvas.ActualWidth / 2;
			result = result - (result * pctOffset);
			result = result * (100 - Convert.ToDouble(HandShortenAmount)) / 100;
			result = result <= 0 ? 0 : result;
			return result;
		}

		private Line GetLine(Brush brush, double thickness, Point outerPoint)
		{
			var result = new Line
			{
				X1 = CenterPoint.X,
				X2 = outerPoint.X,
				Y1 = CenterPoint.Y,
				Y2 = outerPoint.Y,
				Stroke = brush,
				StrokeThickness = thickness,
				StrokeEndLineCap = PenLineCap.Triangle
			};
			if (ShowHandsDropShadow)
				result.Effect = GetDropShadow();
			return result;
		}

		private Point GetPointOnCircle(double angle, double radius, Point fromPoint)
		{
			var result = new Point();
			var sinAngle0 = Math.Sin(angle * Math.PI / 180);
			var cosAngle0 = Math.Cos(angle * Math.PI / 180);
			result.X = (cosAngle0 * radius) + fromPoint.X;
			result.Y = fromPoint.Y - (sinAngle0 * radius);
			return result;
		}

		private void HandsCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (DesignerProperties.GetIsInDesignMode(this))
				return;
			ResetHands(this);
		}

		private void LayoutRoot_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			SecondHandImage.Margin = new Thickness(0, (sender as Grid).ActualHeight / 2, 0, 0);
			SecondHandImage.Height = (sender as Grid).ActualHeight / 2;
		}
		private string MonthAbbreviation(DateTime dateTime)
		{
			return dateTime.ToString("MMM");
		}

		private void RemoveLine(Line line)
		{
			if (line != null)
			{
				HandsCanvas.Children.Remove(line);
				line.Effect = null;
				line = null;
			}
		}
		#endregion Private Methods

		#region Public Fields
		public static readonly DependencyProperty ActivityBrushProperty = DependencyProperty.Register("ActivityBrush", typeof(Brush), typeof(AnalogClock), new PropertyMetadata(new SolidColorBrush(Colors.Blue), onActivityBrushChanged));
		public static readonly DependencyProperty ActivityHeightProperty = DependencyProperty.Register("ActivityHeight", typeof(double), typeof(AnalogClock), new PropertyMetadata(20.0, onActivityHeightChanged));
		public static readonly DependencyProperty ActivityMarginProperty = DependencyProperty.Register("ActivityMargin", typeof(Thickness), typeof(AnalogClock), new PropertyMetadata(new Thickness(0, 0, 50, 0), onActivityMarginChanged));
		public static readonly DependencyProperty ActivityValueProperty = DependencyProperty.Register("ActivityValue", typeof(double), typeof(AnalogClock), new PropertyMetadata(0.0, onActivityValueChanged));
		public static readonly DependencyProperty ActivityVisibilityProperty = DependencyProperty.Register("ActivityVisibility", typeof(Visibility), typeof(AnalogClock), new PropertyMetadata(Visibility.Visible, onActivityVisibilityChanged));
		public static readonly DependencyProperty ActivityWidthProperty = DependencyProperty.Register("ActivityWidth", typeof(double), typeof(AnalogClock), new PropertyMetadata(150.0, onActivityWidthChanged));
		public static readonly DependencyProperty ClockFaceSourceProperty = DependencyProperty.Register("ClockFaceSource", typeof(ImageSource), typeof(AnalogClock), new PropertyMetadata(null, onClockFaceSourceChanged));
		public static readonly DependencyProperty ClockSizeProperty = DependencyProperty.Register("ClockSize", typeof(double), typeof(AnalogClock), new PropertyMetadata(300.0, onClockSizeChanged));
		public static readonly DependencyProperty DateOffsetProperty = DependencyProperty.Register("DateOffset", typeof(double), typeof(AnalogClock), new PropertyMetadata(80.0, onDateOffsetChanged));
		public static readonly DependencyProperty DateVisibilityProperty = DependencyProperty.Register("DateVisibility", typeof(Visibility), typeof(AnalogClock), new PropertyMetadata(Visibility.Visible, onDateVisibilityChanged));
		public static readonly DependencyProperty DropShadowColorProperty = DependencyProperty.Register("DropShadowColor", typeof(Color), typeof(AnalogClock), new PropertyMetadata(Colors.Transparent, onDropShadowColorChanged));
		public new static readonly DependencyProperty FontFamilyProperty = DependencyProperty.Register("FontFamily", typeof(FontFamily), typeof(AnalogClock), new PropertyMetadata(null, onFontFamilyChanged));
		public new static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register("FontSize", typeof(double), typeof(AnalogClock), new PropertyMetadata(12.0, onFontSizeChanged));
		public new static readonly DependencyProperty FontStyleProperty = DependencyProperty.Register("FontStyle", typeof(FontStyle), typeof(AnalogClock), new PropertyMetadata(FontStyles.Normal, onFontStyleChanged));
		public new static readonly DependencyProperty FontWeightProperty = DependencyProperty.Register("FontWeight", typeof(FontWeight), typeof(AnalogClock), new PropertyMetadata(FontWeights.Normal, onFontWeightChanged));
		public new static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(AnalogClock), new PropertyMetadata(new SolidColorBrush(Colors.Black), onForegroundChanged));
		public static readonly DependencyProperty HandsBrushProperty = DependencyProperty.Register("HandsBrush", typeof(Brush), typeof(AnalogClock), new PropertyMetadata(Brushes.Transparent, onHandsBrushChanged));
		public static readonly DependencyProperty HandShortenAmountProperty = DependencyProperty.Register("HandShortenAmount", typeof(double), typeof(AnalogClock), new PropertyMetadata(Convert.ToDouble(0), onHandShortenAmountChanged));
		public static readonly DependencyProperty HandThicknessProperty = DependencyProperty.Register("HandThickness", typeof(double), typeof(AnalogClock), new PropertyMetadata(1.0, onHandThicknessChanged));
		public static readonly DependencyProperty HubBrushProperty = DependencyProperty.Register("HubBrush", typeof(Brush), typeof(AnalogClock), new PropertyMetadata(Brushes.Transparent, onHubBrushChanged));
		public static readonly DependencyProperty HubSizeProperty = DependencyProperty.Register("HubSize", typeof(double), typeof(AnalogClock), new PropertyMetadata(20.0, onHubSizeChanged));
		public static readonly DependencyProperty MoonMarginProperty = DependencyProperty.Register("MoonMargin", typeof(Thickness), typeof(AnalogClock), new PropertyMetadata(new Thickness(0), onMoonMarginChanged));
		public static readonly DependencyProperty MoonOpacityProperty = DependencyProperty.Register("MoonOpacity", typeof(double), typeof(AnalogClock), new PropertyMetadata(1.0, onMoonOpacityChanged));
		public static readonly DependencyProperty MoonPhaseVisibilityProperty = DependencyProperty.Register("MoonPhaseVisibility", typeof(Visibility), typeof(AnalogClock), new PropertyMetadata(Visibility.Visible, onMoonPhaseVisibilityChanged));
		public static readonly DependencyProperty MoonSizeProperty = DependencyProperty.Register("MoonSize", typeof(double), typeof(AnalogClock), new PropertyMetadata(30.0, onMoonSizeChanged));
		public static new readonly DependencyProperty PaddingProperty = DependencyProperty.Register("Padding", typeof(Thickness), typeof(AnalogClock), new PropertyMetadata(new Thickness(0), onPaddingChanged));
		public static readonly DependencyProperty SecondsBrushProperty = DependencyProperty.Register("SecondsBrush", typeof(Brush), typeof(AnalogClock), new PropertyMetadata(Brushes.Transparent, onSecondsBrushChanged));
		public static readonly DependencyProperty SecondsVisibilityProperty = DependencyProperty.Register("SecondsVisibility", typeof(Visibility), typeof(AnalogClock), new PropertyMetadata(Visibility.Visible, onSecondsVisibilityChanged));
		public static readonly DependencyProperty SelectedTimeZoneProperty = DependencyProperty.Register("SelectedTimeZone", typeof(TimeZoneInfo), typeof(AnalogClock), new PropertyMetadata(null, onSelectedTimeZoneChanged));
		public static readonly DependencyProperty ShowHandsDropShadowProperty = DependencyProperty.Register("ShowHandsDropShadow", typeof(bool), typeof(AnalogClock), new PropertyMetadata(false, onShowHandsDropShadowChanged));
		public static readonly DependencyProperty SmoothSecondsHandProperty = DependencyProperty.Register("SmoothSecondsHand", typeof(bool), typeof(AnalogClock), new PropertyMetadata(false, onSmoothSecondsHandChanged));
		#endregion Public Fields

		#region Private Fields
		private const double maxAngle = 359.999;
		private Line hoursHand = null;
		private Line minutesHand = null;
		private Point oldOuterPoint = new Point();
		private double pointAngle = maxAngle / 60;
		private Line secondsHand = null;
		private DispatcherTimer timeTimer = null;
		#endregion Private Fields

		#region Public Properties
		public Brush ActivityBrush
		{
			get { return (Brush)GetValue(ActivityBrushProperty); }
			set { SetValue(ActivityBrushProperty, value); }
		}
		public double ActivityHeight
		{
			get { return (double)GetValue(ActivityHeightProperty); }
			set { SetValue(ActivityHeightProperty, value); }
		}
		public Thickness ActivityMargin
		{
			get { return (Thickness)GetValue(ActivityMarginProperty); }
			set { SetValue(ActivityMarginProperty, value); }
		}
		public double ActivityValue
		{
			get { return (double)GetValue(ActivityValueProperty); }
			set { SetValue(ActivityValueProperty, value); }
		}
		public Visibility ActivityVisibility
		{
			get { return (Visibility)GetValue(ActivityVisibilityProperty); }
			set { SetValue(ActivityVisibilityProperty, value); }
		}
		public double ActivityWidth
		{
			get { return (double)GetValue(ActivityWidthProperty); }
			set { SetValue(ActivityWidthProperty, value); }
		}
		public ImageSource ClockFaceSource
		{
			get { return (ImageSource)GetValue(ClockFaceSourceProperty); }
			set { SetValue(ClockFaceSourceProperty, value); }
		}

		public double ClockSize
		{
			get { return (double)GetValue(ClockSizeProperty); }
			set { SetValue(ClockSizeProperty, value); }
		}

		public double DateOffset
		{
			get { return (double)GetValue(DateOffsetProperty); }
			set { SetValue(DateOffsetProperty, value); }
		}

		public Visibility DateVisibility
		{
			get { return (Visibility)GetValue(DateVisibilityProperty); }
			set { SetValue(DateVisibilityProperty, value); }
		}

		public Color DropShadowColor
		{
			get { return (Color)GetValue(DropShadowColorProperty); }
			set { SetValue(DropShadowColorProperty, value); }
		}

		public new FontFamily FontFamily
		{
			get { return (FontFamily)GetValue(FontFamilyProperty); }
			set { SetValue(FontFamilyProperty, value); }
		}

		public new double FontSize
		{
			get { return (double)GetValue(FontSizeProperty); }
			set { SetValue(FontSizeProperty, value); }
		}

		public new FontStyle FontStyle
		{
			get { return (FontStyle)GetValue(FontStyleProperty); }
			set { SetValue(FontStyleProperty, value); }
		}

		public new FontWeight FontWeight
		{
			get { return (FontWeight)GetValue(FontWeightProperty); }
			set { SetValue(FontWeightProperty, value); }
		}

		public new Brush Foreground
		{
			get { return (Brush)GetValue(ForegroundProperty); }
			set { SetValue(ForegroundProperty, value); }
		}

		public Brush HandsBrush
		{
			get { return (Brush)GetValue(HandsBrushProperty); }
			set { SetValue(HandsBrushProperty, value); }
		}

		public double HandShortenAmount
		{
			get { return (double)GetValue(HandShortenAmountProperty); }
			set { SetValue(HandShortenAmountProperty, value); }
		}

		public double HandThickness
		{
			get { return (double)GetValue(HandThicknessProperty); }
			set { SetValue(HandThicknessProperty, value); }
		}

		public Brush HubBrush
		{
			get { return (Brush)GetValue(HubBrushProperty); }
			set { SetValue(HubBrushProperty, value); }
		}

		public double HubSize
		{
			get { return (double)GetValue(HubSizeProperty); }
			set { SetValue(HubSizeProperty, value); }
		}

		public Thickness MoonMargin
		{
			get { return (Thickness)GetValue(MoonMarginProperty); }
			set { SetValue(MoonMarginProperty, value); }
		}

		public double MoonOpacity
		{
			get { return (double)GetValue(MoonOpacityProperty); }
			set { SetValue(MoonOpacityProperty, value); }
		}

		public Visibility MoonPhaseVisibility
		{
			get { return (Visibility)GetValue(MoonPhaseVisibilityProperty); }
			set { SetValue(MoonPhaseVisibilityProperty, value); }
		}

		public double MoonSize
		{
			get { return (double)GetValue(MoonSizeProperty); }
			set { SetValue(MoonSizeProperty, value); }
		}

		public new Thickness Padding
		{
			get { return (Thickness)GetValue(PaddingProperty); }
			set { SetValue(PaddingProperty, value); }
		}

		public Brush SecondsBrush
		{
			get { return (Brush)GetValue(SecondsBrushProperty); }
			set { SetValue(SecondsBrushProperty, value); }
		}

		public Visibility SecondsVisibility
		{
			get { return (Visibility)GetValue(SecondsVisibilityProperty); }
			set { SetValue(SecondsVisibilityProperty, value); }
		}

		public TimeZoneInfo SelectedTimeZone
		{
			get { return (TimeZoneInfo)GetValue(SelectedTimeZoneProperty); }
			set { SetValue(SelectedTimeZoneProperty, value); }
		}

		public bool ShowHandsDropShadow
		{
			get { return (bool)GetValue(ShowHandsDropShadowProperty); }
			set { SetValue(ShowHandsDropShadowProperty, value); }
		}

		public bool SmoothSecondsHand
		{
			get { return (bool)GetValue(SmoothSecondsHandProperty); }
			set { SetValue(SmoothSecondsHandProperty, value); }
		}
		#endregion Public Properties

		#region Private Properties
		private Point CenterPoint
		{
			get { return new Point(HandsCanvas.ActualWidth / 2, HandsCanvas.ActualHeight / 2); }
		}
		#endregion Private Properties
	}
}
