namespace GregOsborne.WPFControls {
	using System.ComponentModel;
	using System.Threading;
	using System.Windows;
	using System.Windows.Documents;
	using System.Windows.Media;
	using System.Windows.Shapes;

	public class TextPath : Shape {
		public static readonly DependencyProperty FontFamilyProperty = TextElement.FontFamilyProperty.AddOwner(typeof(TextPath), new FrameworkPropertyMetadata(SystemFonts.MessageFontFamily, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits, OnPropertyChanged));
		public static readonly DependencyProperty FontSizeProperty = TextElement.FontSizeProperty.AddOwner(typeof(TextPath), new FrameworkPropertyMetadata(SystemFonts.MessageFontSize, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure, OnPropertyChanged));
		public static readonly DependencyProperty FontStretchProperty = TextElement.FontStretchProperty.AddOwner(typeof(TextPath), new FrameworkPropertyMetadata(TextElement.FontStretchProperty.DefaultMetadata.DefaultValue, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits, OnPropertyChanged));
		public static readonly DependencyProperty FontStyleProperty = TextElement.FontStyleProperty.AddOwner(typeof(TextPath), new FrameworkPropertyMetadata(SystemFonts.MessageFontStyle, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits, OnPropertyChanged));
		public static readonly DependencyProperty FontWeightProperty = TextElement.FontWeightProperty.AddOwner(typeof(TextPath), new FrameworkPropertyMetadata(SystemFonts.MessageFontWeight, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits, OnPropertyChanged));
		public static readonly DependencyProperty OriginPointProperty = DependencyProperty.Register("Origin", typeof(Point), typeof(TextPath), new FrameworkPropertyMetadata(new Point(0, 0), FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure, OnPropertyChanged));
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TextPath), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure, OnPropertyChanged));
		public static readonly DependencyProperty TextDecorationsProperty = DependencyProperty.Register("TextDecorations", typeof(TextDecorationCollection), typeof(TextPath), new FrameworkPropertyMetadata(default(TextDecorationCollection), OnPropertyChanged));

		private Geometry textGeometry;

		[Bindable(true), Category("Appearance")]
		[Localizability(LocalizationCategory.Font)]
		[TypeConverter(typeof(FontFamilyConverter))]
		public FontFamily FontFamily {
			get => (FontFamily)this.GetValue(FontFamilyProperty);
			set => this.SetValue(FontFamilyProperty, value);
		}

		[Bindable(true), Category("Appearance")]
		[TypeConverter(typeof(FontSizeConverter))]
		[Localizability(LocalizationCategory.None)]
		public double FontSize {
			get => (double)this.GetValue(FontSizeProperty);
			set => this.SetValue(FontSizeProperty, value);
		}

		[Bindable(true), Category("Appearance")]
		[TypeConverter(typeof(FontStretchConverter))]
		public FontStretch FontStretch {
			get => (FontStretch)this.GetValue(FontStretchProperty);
			set => this.SetValue(FontStretchProperty, value);
		}

		[Bindable(true), Category("Appearance")]
		[TypeConverter(typeof(FontStyleConverter))]
		public FontStyle FontStyle {
			get => (FontStyle)this.GetValue(FontStyleProperty);
			set => this.SetValue(FontStyleProperty, value);
		}

		[Bindable(true), Category("Appearance")]
		[TypeConverter(typeof(FontWeightConverter))]
		public FontWeight FontWeight {
			get => (FontWeight)this.GetValue(FontWeightProperty);
			set => this.SetValue(FontWeightProperty, value);
		}

		[Bindable(true), Category("Appearance")]
		[TypeConverter(typeof(PointConverter))]
		public Point Origin {
			get => (Point)this.GetValue(OriginPointProperty);
			set => this.SetValue(OriginPointProperty, value);
		}

		[Bindable(true), Category("Appearance")]
		public string Text {
			get => (string)this.GetValue(TextProperty);
			set => this.SetValue(TextProperty, value);
		}

		protected override Geometry DefiningGeometry => this.textGeometry ?? Geometry.Empty;

		protected override Size MeasureOverride(Size constraint) {
			var w = constraint.Width;
			var h = constraint.Height;
			if (double.IsPositiveInfinity(w)) {
				w = 0;
			}

			if (double.IsPositiveInfinity(h)) {
				h = 0;
			}

			var formattedText = this.GetFormattedText();
			w = System.Math.Min(0, w);
			h = System.Math.Min(0, h);
			var desiredSize = new Size(w + formattedText.Width, h + formattedText.Height);
			if (double.IsPositiveInfinity(desiredSize.Width)) {
				desiredSize.Width = 0;
			}

			if (double.IsPositiveInfinity(desiredSize.Height)) {
				desiredSize.Height = 0;
			}

			return desiredSize;
		}

		private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((TextPath)d).CreateTextGeometry();

		private FormattedText GetFormattedText() {
			var td = new FormattedText(this.Text, Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight,
				new Typeface(this.FontFamily, this.FontStyle, this.FontWeight, this.FontStretch), this.FontSize, Brushes.Transparent,
				VisualTreeHelper.GetDpi(this).PixelsPerDip);
			td.SetTextDecorations(this.TextDecorations);
			return td;
		}

		private void CreateTextGeometry() {
			var t = this.GetFormattedText();
			t.SetTextDecorations(this.TextDecorations);
			this.textGeometry = t.BuildGeometry(this.Origin);
		}

		public TextDecorationCollection TextDecorations {
			get => (TextDecorationCollection)this.GetValue(TextDecorationsProperty);
			set => this.SetValue(TextDecorationsProperty, value);
		}
	}
}
