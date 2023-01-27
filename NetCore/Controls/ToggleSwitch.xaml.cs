using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Controls {
    public partial class ToggleSwitch : UserControl {
        public ToggleSwitch() {
            InitializeComponent();
            SetToggleProperties(null, IsOn);
            TheValue.Foreground = Foreground;
        }

        #region FillBrushProperty
        public static readonly DependencyProperty FillBrushProperty = DependencyProperty.Register("FillBrush", typeof(Brush), typeof(ToggleSwitch), new FrameworkPropertyMetadata(Brushes.Transparent, OnFillBrushPropertyChanged));
        public Brush FillBrush {
            get => (Brush)GetValue(FillBrushProperty);
            set => SetValue(FillBrushProperty, value);
        }
        private static void OnFillBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ToggleSwitch)d;
            var val = (Brush)e.NewValue;
            obj.TheBackground.Foreground = val;
        }
        #endregion

        #region OnThumbBrushProperty
        public static readonly DependencyProperty OnThumbBrushProperty = DependencyProperty.Register("OnThumbBrush", typeof(Brush), typeof(ToggleSwitch), new FrameworkPropertyMetadata(default(Brush), OnOnThumbBrushPropertyChanged));
        public Brush OnThumbBrush {
            get => (Brush)GetValue(OnThumbBrushProperty);
            set => SetValue(OnThumbBrushProperty, value);
        }
        private static void OnOnThumbBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ToggleSwitch)d;
            var val = (Brush)e.NewValue;
            obj.SetToggleProperties(obj, obj.IsOn);
        }
        #endregion

        #region OnFillBrushProperty
        /// <summary>Gets the OnFillBrush dependency property.</summary>
        /// <value>The OnFillBrush dependency property.</value>
        public static readonly DependencyProperty OnFillBrushProperty = DependencyProperty.Register("OnFillBrush", typeof(Brush), typeof(ToggleSwitch), new PropertyMetadata(default(Brush), OnOnFillBrushPropertyChanged));
        /// <summary>Gets/sets the OnFillBrush.</summary>
        /// <value>The OnFillBrush.</value>
        public Brush OnFillBrush {
            get => (Brush)GetValue(OnFillBrushProperty);
            set => SetValue(OnFillBrushProperty, value);
        }
        private static void OnOnFillBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ToggleSwitch)d;
            var val = (Brush)e.NewValue;
            
        }
        #endregion

        #region FontSizeProperty
        public static new readonly DependencyProperty FontSizeProperty = DependencyProperty.Register("FontSize", typeof(double), typeof(ToggleSwitch), new PropertyMetadata(32.0, OnFontSizePropertyChanged));
        public new double FontSize {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }
        private static void OnFontSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ToggleSwitch)d;
            var val = (double)e.NewValue;
            obj.TheBackground.FontSize = val * 3;
            obj.TheBorder.FontSize = val * 3;
            obj.TheThumb.FontSize = (val * 3) * 0.28;
            obj.TheValue.FontSize = val;
        }
        #endregion

        #region IsOnProperty
        public static readonly DependencyProperty IsOnProperty = DependencyProperty.Register("IsOn", typeof(bool), typeof(ToggleSwitch), new PropertyMetadata(default(bool), OnIsOnPropertyChanged));
        public bool IsOn {
            get => (bool)GetValue(IsOnProperty);
            set => SetValue(IsOnProperty, value);
        }
        private static void OnIsOnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ToggleSwitch)d;
            var val = (bool)e.NewValue;
            obj.SetToggleProperties(obj, obj.IsOn);
        }
        #endregion

        #region OnContentProperty
        public static readonly DependencyProperty OnContentProperty = DependencyProperty.Register("OnContent", typeof(string), typeof(ToggleSwitch), new PropertyMetadata("On", OnOnContentPropertyChanged));
        public string OnContent {
            get => (string)GetValue(OnContentProperty);
            set => SetValue(OnContentProperty, value);
        }
        private static void OnOnContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ToggleSwitch)d;
            var val = (string)e.NewValue;
            obj.TheValue.Text = obj.IsOn ? val : obj.OffContent;
        }
        #endregion

        #region OffContentProperty
        public static readonly DependencyProperty OffContentProperty = DependencyProperty.Register("OffContent", typeof(string), typeof(ToggleSwitch), new PropertyMetadata("Off", OnOffContentPropertyChanged));
        public string OffContent {
            get => (string)GetValue(OffContentProperty);
            set => SetValue(OffContentProperty, value);
        }
        private static void OnOffContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ToggleSwitch)d;
            var val = (string)e.NewValue;
            obj.TheValue.Text = obj.IsOn ? obj.OnContent : val;
        }
        #endregion

        #region TextFontFamilyProperty
        public static readonly DependencyProperty TextFontFamilyProperty = DependencyProperty.Register("TextFontFamily", typeof(FontFamily), typeof(ToggleSwitch), new PropertyMetadata(default(FontFamily), OnTextFontFamilyPropertyChanged));
        public FontFamily TextFontFamily {
            get => (FontFamily)GetValue(TextFontFamilyProperty);
            set => SetValue(TextFontFamilyProperty, value);
        }
        private static void OnTextFontFamilyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ToggleSwitch)d;
            var val = (FontFamily)e.NewValue;
            obj.TheValue.FontFamily = val;
        }
        #endregion

        private void TheBorder_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                IsOn = e.GetPosition(TheBorder).X > (TheBorder.ActualWidth / 2.0);
                SetToggleProperties(null, IsOn);
            }
        }

        private void TheValue_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                IsOn = !IsOn;
                SetToggleProperties(null, IsOn);
            }
        }

        private void SetToggleProperties(ToggleSwitch ts = default, bool isOn = false) {
            if (ts == null) {
                ts = this;
            }
            if (ts == null) {
                return;
            }
            ts.TheThumb.HorizontalAlignment = isOn ? HorizontalAlignment.Right : HorizontalAlignment.Left;
            ts.TheValue.Foreground = ts.Foreground;
            ts.TheThumb.Foreground = isOn ? OnThumbBrush : ts.Foreground;
            ts.TheValue.Text = isOn ? ts.OnContent : ts.OffContent;
            ts.TheBackground.Foreground = isOn ? ts.OnFillBrush : Brushes.Transparent;
        }
    }
}
