namespace GregOsborne.Controls
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    [DefaultProperty("IsEnabled")]
    public partial class Spinner : UserControl
    {
        public static readonly DependencyProperty BackgroundDependencyProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(Spinner), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(10, 0, 0, 0)), OnBackgroundPropertyChanged, CoerceBackgroundProperty));
        public static readonly DependencyProperty ForegroundDependencyProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(Spinner), new FrameworkPropertyMetadata(Brushes.Black, OnForegroundPropertyChanged, CoerceForegroundProperty));
        public static readonly DependencyProperty InnerIconDependencyProperty = DependencyProperty.Register("InnerIcon", typeof(FontAwesome.WPF.FontAwesomeIcon), typeof(Spinner), new FrameworkPropertyMetadata(FontAwesome.WPF.FontAwesomeIcon.Save, OnInnerIconPropertyChanged, CoerceInnerIconProperty));
        public static readonly DependencyProperty InnerSpinDurationDependencyProperty = DependencyProperty.Register("InnerSpinDuration", typeof(double), typeof(Spinner), new FrameworkPropertyMetadata(2.0, OnInnerSpinDurationPropertyChanged, CoerceInnerSpinDurationProperty));
        public static readonly DependencyProperty IsEnabledDependencyProperty = DependencyProperty.Register("IsEnabled", typeof(bool), typeof(Spinner), new FrameworkPropertyMetadata(true, OnIsEnabledPropertyChanged, CoerceIsEnabledProperty));
        public static readonly DependencyProperty LargeFontFamilyDependencyProperty = DependencyProperty.Register("LargeFontFamily", typeof(FontFamily), typeof(Spinner), new FrameworkPropertyMetadata(new FontFamily("Segoe UI"), OnLargeFontFamilyPropertyChanged, CoerceLargeFontFamilyProperty));
        public static readonly DependencyProperty LargeFontSizeDependencyProperty = DependencyProperty.Register("LargeFontSize", typeof(double), typeof(Spinner), new FrameworkPropertyMetadata(80.0, OnLargeFontSizePropertyChanged, CoerceLargeFontSizeProperty));
        public static readonly DependencyProperty LargeOutlineThicknessDependencyProperty = DependencyProperty.Register("LargeOutlineThickness", typeof(double), typeof(Spinner), new FrameworkPropertyMetadata(2.5, OnLargeOutlineThicknessPropertyChanged, CoerceLargeOutlineThicknessProperty));
        public static readonly DependencyProperty LargeTextDependencyProperty = DependencyProperty.Register("LargeText", typeof(string), typeof(Spinner), new FrameworkPropertyMetadata("Large Text", OnLargeTextPropertyChanged, CoerceLargeTextProperty));
        public static readonly DependencyProperty LargeTextFillDependencyProperty = DependencyProperty.Register("LargeTextFill", typeof(Brush), typeof(Spinner), new FrameworkPropertyMetadata(Brushes.Transparent, OnLargeTextFillPropertyChanged, CoerceLargeTextFillProperty));
        public static readonly DependencyProperty LargeTextOutlineDependencyProperty = DependencyProperty.Register("LargeTextOutline", typeof(Brush), typeof(Spinner), new FrameworkPropertyMetadata(Brushes.Black, OnLargeTextOutlinePropertyChanged, CoerceLargeTextOutlineProperty));
        public static readonly DependencyProperty OuterIconDependencyProperty = DependencyProperty.Register("OuterIcon", typeof(FontAwesome.WPF.FontAwesomeIcon), typeof(Spinner), new FrameworkPropertyMetadata(FontAwesome.WPF.FontAwesomeIcon.Superpowers, OnOuterIconPropertyChanged, CoerceOuterIconProperty));
        public static readonly DependencyProperty OuterSpinDurationDependencyProperty = DependencyProperty.Register("OuterSpinDuration", typeof(double), typeof(Spinner), new FrameworkPropertyMetadata(3.0, OnOuterSpinDurationPropertyChanged, CoerceOuterSpinDurationProperty));
        public static readonly DependencyProperty SmallFontFamilyDependencyProperty = DependencyProperty.Register("SmallFontFamily", typeof(FontFamily), typeof(Spinner), new FrameworkPropertyMetadata(new FontFamily("Times New Roman"), OnSmallFontFamilyPropertyChanged, CoerceSmallFontFamilyProperty));
        public static readonly DependencyProperty SmallFontSizeDependencyProperty = DependencyProperty.Register("SmallFontSize", typeof(double), typeof(Spinner), new FrameworkPropertyMetadata(40.0, OnSmallFontSizePropertyChanged, CoerceSmallFontSizeProperty));
        public static readonly DependencyProperty SmallOutlineThicknessDependencyProperty = DependencyProperty.Register("SmallOutlineThickness", typeof(double), typeof(Spinner), new FrameworkPropertyMetadata(1.5, OnSmallOutlineThicknessPropertyChanged, CoerceSmallOutlineThicknessProperty));
        public static readonly DependencyProperty SmallTextDependencyProperty = DependencyProperty.Register("SmallText", typeof(string), typeof(Spinner), new FrameworkPropertyMetadata("Small Text", OnSmallTextPropertyChanged, CoerceSmallTextProperty));
        public static readonly DependencyProperty SmallTextFillDependencyProperty = DependencyProperty.Register("SmallTextFill", typeof(Brush), typeof(Spinner), new FrameworkPropertyMetadata(Brushes.Transparent, OnSmallTextFillPropertyChanged, CoerceSmallTextFillProperty));
        public static readonly DependencyProperty SmallTextOutlineDependencyProperty = DependencyProperty.Register("SmallTextOutline", typeof(Brush), typeof(Spinner), new FrameworkPropertyMetadata(Brushes.Black, OnSmallTextOutlinePropertyChanged, CoerceSmallTextOutlineProperty));
        public static readonly DependencyProperty SpinnerSizeDependencyProperty = DependencyProperty.Register("SpinnerSize", typeof(double), typeof(Spinner), new FrameworkPropertyMetadata(180.0, OnSpinnerSizePropertyChanged, CoerceSpinnerSizeProperty));

        public Spinner()
        {
            InitializeComponent();
        }

        [Bindable(BindableSupport.Yes)]
        [Browsable(true)]
        [Category("Brush")]
        [Description("Background brush for the control")]
        public new Brush Background {
            get { return (Brush)GetValue(BackgroundDependencyProperty); }
            set { SetValue(BackgroundDependencyProperty, value); }
        }

        [Bindable(BindableSupport.Yes)]
        [Browsable(true)]
        [Category("Brush")]
        [Description("The spinner(s) color")]
        public new Brush Foreground {
            get { return (Brush)GetValue(ForegroundDependencyProperty); }
            set { SetValue(ForegroundDependencyProperty, value); }
        }

        [Bindable(BindableSupport.Yes)]
        [Browsable(true)]
        [Category("Appearance")]
        [Description("The inner (smaller) icon")]
#pragma warning disable CS3003 // Type is not CLS-compliant
        public FontAwesome.WPF.FontAwesomeIcon InnerIcon {
#pragma warning restore CS3003 // Type is not CLS-compliant
            get { return (FontAwesome.WPF.FontAwesomeIcon)GetValue(InnerIconDependencyProperty); }
            set { SetValue(InnerIconDependencyProperty, value); }
        }

        [Bindable(BindableSupport.Yes)]
        [Browsable(true)]
        [Category("Functionality")]
        [Description("The speed at which the inner (smaller) icon spins")]
        public double InnerSpinDuration {
            get { return (double)GetValue(InnerSpinDurationDependencyProperty); }
            set { SetValue(InnerSpinDurationDependencyProperty, value); }
        }

        [Bindable(BindableSupport.Yes)]
        [Browsable(true)]
        [Category("Common")]
        [Description("Determines if the spinners spin")]
        public new bool IsEnabled {
            get {
                return (bool)GetValue(IsEnabledDependencyProperty);
            }
            set {
                SetValue(IsEnabledDependencyProperty, value);
            }
        }

        [Bindable(BindableSupport.Yes)]
        [Browsable(true)]
        [Category("Appearance")]
        [Description("The font for the large (upper) text")]
        public FontFamily LargeFontFamily {
            get {
                return (FontFamily)GetValue(LargeFontFamilyDependencyProperty);
            }
            set {
                SetValue(LargeFontFamilyDependencyProperty, value);
            }
        }

        [Bindable(BindableSupport.Yes)]
        [Browsable(true)]
        [Category("Appearance")]
        [Description("The font size for the large (upper) text")]
        public double LargeFontSize {
            get {
                return (double)GetValue(LargeFontSizeDependencyProperty);
            }
            set {
                SetValue(LargeFontSizeDependencyProperty, value);
            }
        }

        [Bindable(BindableSupport.Yes)]
        [Browsable(true)]
        [Category("Appearance")]
        [Description("The outline thickness of the large (upper) text")]
        public double LargeOutlineThickness {
            get {
                return (double)GetValue(LargeOutlineThicknessDependencyProperty);
            }
            set {
                SetValue(LargeOutlineThicknessDependencyProperty, value);
            }
        }

        [Bindable(BindableSupport.Yes)]
        [Browsable(true)]
        [Category("Text")]
        [Description("The large (upper) text")]
        public string LargeText {
            get {
                return (string)GetValue(LargeTextDependencyProperty);
            }
            set {
                SetValue(LargeTextDependencyProperty, value);
            }
        }

        [Bindable(BindableSupport.Yes)]
        [Browsable(true)]
        [Category("Brush")]
        [Description("The fill brush for the large (upper) text")]
        public Brush LargeTextFill {
            get {
                return (Brush)GetValue(LargeTextFillDependencyProperty);
            }
            set {
                SetValue(LargeTextFillDependencyProperty, value);
            }
        }

        [Bindable(BindableSupport.Yes)]
        [Browsable(true)]
        [Category("Brush")]
        [Description("The outline brush for the large (upper) text")]
        public Brush LargeTextOutline {
            get {
                return (Brush)GetValue(LargeTextOutlineDependencyProperty);
            }
            set {
                SetValue(LargeTextOutlineDependencyProperty, value);
            }
        }

        [Bindable(BindableSupport.Yes)]
        [Browsable(true)]
        [Category("Appearance")]
        [Description("The outer (larger) icon")]
#pragma warning disable CS3003 // Type is not CLS-compliant
        public FontAwesome.WPF.FontAwesomeIcon OuterIcon {
#pragma warning restore CS3003 // Type is not CLS-compliant
            get {
                return (FontAwesome.WPF.FontAwesomeIcon)GetValue(OuterIconDependencyProperty);
            }
            set {
                SetValue(OuterIconDependencyProperty, value);
            }
        }

        [Bindable(BindableSupport.Yes)]
        [Browsable(true)]
        [Category("Functionality")]
        [Description("The speed at which the outer (larger) icon spins")]
        public double OuterSpinDuration {
            get {
                return (double)GetValue(OuterSpinDurationDependencyProperty);
            }
            set {
                SetValue(OuterSpinDurationDependencyProperty, value);
            }
        }

        [Bindable(BindableSupport.Yes)]
        [Browsable(true)]
        [Category("Appearance")]
        [Description("The font for the small (lower) text")]
        public FontFamily SmallFontFamily {
            get {
                return (FontFamily)GetValue(SmallFontFamilyDependencyProperty);
            }
            set {
                SetValue(SmallFontFamilyDependencyProperty, value);
            }
        }

        [Bindable(BindableSupport.Yes)]
        [Browsable(true)]
        [Category("Appearance")]
        [Description("The font size for the small (lower) text")]
        public double SmallFontSize {
            get {
                return (double)GetValue(SmallFontSizeDependencyProperty);
            }
            set {
                SetValue(SmallFontSizeDependencyProperty, value);
            }
        }

        [Bindable(BindableSupport.Yes)]
        [Browsable(true)]
        [Category("Appearance")]
        [Description("The outline thickness for the small (lower) text")]
        public double SmallOutlineThickness {
            get {
                return (double)GetValue(SmallOutlineThicknessDependencyProperty);
            }
            set {
                SetValue(SmallOutlineThicknessDependencyProperty, value);
            }
        }

        [Bindable(BindableSupport.Yes)]
        [Browsable(true)]
        [Category("Text")]
        [Description("The small (lower) text")]
        public string SmallText {
            get {
                return (string)GetValue(SmallTextDependencyProperty);
            }
            set {
                SetValue(SmallTextDependencyProperty, value);
            }
        }

        [Bindable(BindableSupport.Yes)]
        [Browsable(true)]
        [Category("Brush")]
        [Description("The fill brush for the small (lower) text")]
        public Brush SmallTextFill {
            get {
                return (Brush)GetValue(SmallTextFillDependencyProperty);
            }
            set {
                SetValue(SmallTextFillDependencyProperty, value);
            }
        }

        [Bindable(BindableSupport.Yes)]
        [Browsable(true)]
        [Category("Brush")]
        [Description("The outline brush for the small (lower) text")]
        public Brush SmallTextOutline {
            get {
                return (Brush)GetValue(SmallTextOutlineDependencyProperty);
            }
            set {
                SetValue(SmallTextOutlineDependencyProperty, value);
            }
        }

        [Bindable(BindableSupport.Yes)]
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Size of the outer icon (smaller size is set as a percentage of larger size)")]
        public double SpinnerSize {
            get {
                return (double)GetValue(SpinnerSizeDependencyProperty);
            }
            set {
                SetValue(SpinnerSizeDependencyProperty, value);
            }
        }

        public void AddToGrid(Grid grid)
        {
            SetValue(Grid.RowProperty, 0);
            SetValue(Grid.ColumnProperty, 0);
            SetValue(Grid.RowSpanProperty, 99);
            SetValue(Grid.ColumnSpanProperty, 99);
            grid.Children.Add(this);
        }

        public void BindToView(object view, DependencyProperty property, string propertyName)
        {
            var myBinding = new Binding(propertyName)
            {
                Source = view
            };
            SetBinding(property, myBinding);
        }

        private static object CoerceBackgroundProperty(DependencyObject d, object value)
        {
            var val = (Brush)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceForegroundProperty(DependencyObject d, object value)
        {
            var val = (Brush)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceInnerIconProperty(DependencyObject d, object value)
        {
            var val = (FontAwesome.WPF.FontAwesomeIcon)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceInnerSpinDurationProperty(DependencyObject d, object value)
        {
            var val = (double)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceIsEnabledProperty(DependencyObject d, object value)
        {
            var val = (bool)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceLargeFontFamilyProperty(DependencyObject d, object value)
        {
            var val = (FontFamily)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceLargeFontSizeProperty(DependencyObject d, object value)
        {
            var val = (double)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceLargeOutlineThicknessProperty(DependencyObject d, object value)
        {
            var val = (double)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceLargeTextFillProperty(DependencyObject d, object value)
        {
            var val = (Brush)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceLargeTextOutlineProperty(DependencyObject d, object value)
        {
            var val = (Brush)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceLargeTextProperty(DependencyObject d, object value)
        {
            var val = (string)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceOuterIconProperty(DependencyObject d, object value)
        {
            var val = (FontAwesome.WPF.FontAwesomeIcon)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceOuterSpinDurationProperty(DependencyObject d, object value)
        {
            var val = (double)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceSmallFontFamilyProperty(DependencyObject d, object value)
        {
            var val = (FontFamily)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceSmallFontSizeProperty(DependencyObject d, object value)
        {
            var val = (double)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceSmallOutlineThicknessProperty(DependencyObject d, object value)
        {
            var val = (double)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceSmallTextFillProperty(DependencyObject d, object value)
        {
            var val = (Brush)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceSmallTextOutlineProperty(DependencyObject d, object value)
        {
            var val = (Brush)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceSmallTextProperty(DependencyObject d, object value)
        {
            var val = (string)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceSpinnerSizeProperty(DependencyObject d, object value)
        {
            var val = (double)value;
            //coerce value here if necessary
            return val;
        }

        private static void OnBackgroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Spinner)d;
            var val = (Brush)e.NewValue;
            obj._backGrid.Background = val;
        }

        private static void OnForegroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Spinner)d;
            var val = (Brush)e.NewValue;
            obj._inner.Foreground = val;
            obj._outer.Foreground = val;
        }

        private static void OnInnerIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Spinner)d;
            var val = (FontAwesome.WPF.FontAwesomeIcon)e.NewValue;
            obj._inner.Icon = val;
        }

        private static void OnInnerSpinDurationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Spinner)d;
            var val = (double)e.NewValue;
            obj._inner.SpinDuration = val;
        }

        private static void OnIsEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Spinner)d;
            var val = (bool)e.NewValue;
            obj._outer.Spin = val;
            obj._inner.Spin = val;
        }

        private static void OnLargeFontFamilyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Spinner)d;
            var val = (FontFamily)e.NewValue;
            obj._largeText.FontFamily = val;
        }

        private static void OnLargeFontSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Spinner)d;
            var val = (double)e.NewValue;
            obj._largeText.FontSize = val;
        }

        private static void OnLargeOutlineThicknessPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Spinner)d;
            var val = (double)e.NewValue;
            obj._largeText.StrokeThickness = val;
        }

        private static void OnLargeTextFillPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Spinner)d;
            var val = (Brush)e.NewValue;
            obj._largeText.Fill = val;
        }

        private static void OnLargeTextOutlinePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Spinner)d;
            var val = (Brush)e.NewValue;
            obj._largeText.Stroke = val;
        }

        private static void OnLargeTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Spinner)d;
            var val = (string)e.NewValue;
            obj._largeText.Text = val;
        }

        private static void OnOuterIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Spinner)d;
            var val = (FontAwesome.WPF.FontAwesomeIcon)e.NewValue;
            obj._outer.Icon = val;
        }

        private static void OnOuterSpinDurationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Spinner)d;
            var val = (double)e.NewValue;
            obj._outer.SpinDuration = val;
        }

        private static void OnSmallFontFamilyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Spinner)d;
            var val = (FontFamily)e.NewValue;
            obj._smallText.FontFamily = val;
        }

        private static void OnSmallFontSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Spinner)d;
            var val = (double)e.NewValue;
            obj._smallText.FontSize = val;
        }

        private static void OnSmallOutlineThicknessPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Spinner)d;
            var val = (double)e.NewValue;
            System.Diagnostics.Debug.WriteLine(val);
            obj._smallText.StrokeThickness = val;
        }

        private static void OnSmallTextFillPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Spinner)d;
            var val = (Brush)e.NewValue;
            obj._smallText.Fill = val;
        }

        private static void OnSmallTextOutlinePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Spinner)d;
            var val = (Brush)e.NewValue;
            obj._smallText.Stroke = val;
        }

        private static void OnSmallTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Spinner)d;
            var val = (string)e.NewValue;
            obj._smallText.Text = val;
        }

        private static void OnSpinnerSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Spinner)d;
            var val = (double)e.NewValue;
            obj._outer.Height = val;
            obj._outer.Width = val;
            obj._inner.Height = val * .45;
            obj._inner.Width = val * .45;
        }
    }
}