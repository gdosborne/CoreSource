namespace GregOsborne.Controls
{
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Threading;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Class TextPath.
    /// </summary>
    public class TextPath : Shape
    {
        /// <summary>
        /// The font family property
        /// </summary>
        public static readonly DependencyProperty FontFamilyProperty = TextElement.FontFamilyProperty.AddOwner(typeof(TextPath), new FrameworkPropertyMetadata(SystemFonts.MessageFontFamily, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits, OnPropertyChanged));

        /// <summary>
        /// The font size property
        /// </summary>
        public static readonly DependencyProperty FontSizeProperty = TextElement.FontSizeProperty.AddOwner(typeof(TextPath), new FrameworkPropertyMetadata(SystemFonts.MessageFontSize, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure, OnPropertyChanged));

        /// <summary>
        /// The font stretch property
        /// </summary>
        public static readonly DependencyProperty FontStretchProperty = TextElement.FontStretchProperty.AddOwner(typeof(TextPath), new FrameworkPropertyMetadata(TextElement.FontStretchProperty.DefaultMetadata.DefaultValue, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits, OnPropertyChanged));

        /// <summary>
        /// The font style property
        /// </summary>
        public static readonly DependencyProperty FontStyleProperty = TextElement.FontStyleProperty.AddOwner(typeof(TextPath), new FrameworkPropertyMetadata(SystemFonts.MessageFontStyle, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits, OnPropertyChanged));

        /// <summary>
        /// The font weight property
        /// </summary>
        public static readonly DependencyProperty FontWeightProperty = TextElement.FontWeightProperty.AddOwner(typeof(TextPath), new FrameworkPropertyMetadata(SystemFonts.MessageFontWeight, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits, OnPropertyChanged));

        /// <summary>
        /// The origin point property
        /// </summary>
        public static readonly DependencyProperty OriginPointProperty = DependencyProperty.Register("Origin", typeof(Point), typeof(TextPath), new FrameworkPropertyMetadata(new Point(0, 0), FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure, OnPropertyChanged));

        /// <summary>
        /// The text property
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TextPath), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure, OnPropertyChanged));

        /// <summary>
        /// The _text geometry
        /// </summary>
        private Geometry _textGeometry;

        /// <summary>
        /// Gets or sets the font family.
        /// </summary>
        /// <value>The font family.</value>
        [Bindable(true), Category("Appearance")]
        [Localizability(LocalizationCategory.Font)]
        [TypeConverter(typeof(FontFamilyConverter))]
        public FontFamily FontFamily {
            get { return (FontFamily)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        /// <summary>
        /// Gets or sets the size of the font.
        /// </summary>
        /// <value>The size of the font.</value>
        [Bindable(true), Category("Appearance")]
        [TypeConverter(typeof(FontSizeConverter))]
        [Localizability(LocalizationCategory.None)]
        public double FontSize {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the font stretch.
        /// </summary>
        /// <value>The font stretch.</value>
        [Bindable(true), Category("Appearance")]
        [TypeConverter(typeof(FontStretchConverter))]
        public FontStretch FontStretch {
            get { return (FontStretch)GetValue(FontStretchProperty); }
            set { SetValue(FontStretchProperty, value); }
        }

        /// <summary>
        /// Gets or sets the font style.
        /// </summary>
        /// <value>The font style.</value>
        [Bindable(true), Category("Appearance")]
        [TypeConverter(typeof(FontStyleConverter))]
        public FontStyle FontStyle {
            get { return (FontStyle)GetValue(FontStyleProperty); }
            set { SetValue(FontStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the font weight.
        /// </summary>
        /// <value>The font weight.</value>
        [Bindable(true), Category("Appearance")]
        [TypeConverter(typeof(FontWeightConverter))]
        public FontWeight FontWeight {
            get { return (FontWeight)GetValue(FontWeightProperty); }
            set { SetValue(FontWeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets the origin.
        /// </summary>
        /// <value>The origin.</value>
        [Bindable(true), Category("Appearance")]
        [TypeConverter(typeof(PointConverter))]
        public Point Origin {
            get { return (Point)GetValue(OriginPointProperty); }
            set { SetValue(OriginPointProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [Bindable(true), Category("Appearance")]
        public string Text {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets a value that represents the <see cref="T:System.Windows.Media.Geometry" /> of the <see cref="T:System.Windows.Shapes.Shape" />.
        /// </summary>
        /// <value>The defining geometry.</value>
        protected override Geometry DefiningGeometry {
            get { return _textGeometry ?? Geometry.Empty; }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            var w = constraint.Width;
            var h = constraint.Height;
            if (double.IsPositiveInfinity(w))
                w = 0;
            if (double.IsPositiveInfinity(h))
                h = 0;
            var desiredSize = new Size();
            //if (DesignerProperties.GetIsInDesignMode(this))
            //    return desiredSize;

            var formattedText = new FormattedText(Text, Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight,
                new Typeface(FontFamily, FontStyle, FontWeight, FontStretch), FontSize, Brushes.Black,
                VisualTreeHelper.GetDpi(this).PixelsPerDip);
            w = System.Math.Min(0, w);
            h = System.Math.Min(0, h);
            desiredSize = new Size(w + formattedText.Width, h + formattedText.Height);
            if (double.IsPositiveInfinity(desiredSize.Width))
                desiredSize.Width = 0;
            if (double.IsPositiveInfinity(desiredSize.Height))
                desiredSize.Height = 0;
            return desiredSize;
        }

        /// <summary>
        /// Handles the <see cref="E:PropertyChanged" /> event.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TextPath)d).CreateTextGeometry();
        }

        /// <summary>
        /// Creates the text geometry.
        /// </summary>
        private void CreateTextGeometry()
        {
            var formattedText = new FormattedText(Text, Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight,
                new Typeface(FontFamily, FontStyle, FontWeight, FontStretch), FontSize, Brushes.Black,
                VisualTreeHelper.GetDpi(this).PixelsPerDip);
            _textGeometry = formattedText.BuildGeometry(Origin);
        }
    }
}