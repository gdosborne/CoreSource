using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFControls {
    public partial class FontIcon : UserControl {
        public FontIcon() {
            InitializeComponent();
        }

        #region Icon Dependency Property
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(FontIcon), new PropertyMetadata("", new PropertyChangedCallback(OnIconChanged)));
        public string Icon {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (FontIcon)d;
            var val = (string)e.NewValue;
            obj.TheIcon.Text = val;
        }
        #endregion

        #region FontFamily Dependency Property
        public static new readonly DependencyProperty FontFamilyProperty = DependencyProperty.Register("FontFamily", typeof(FontFamily), typeof(FontIcon), new PropertyMetadata(new FontFamily("Segoe Fluent Icons"), new PropertyChangedCallback(OnFontFamilyChanged)));
        public new FontFamily FontFamily {
            get { return (FontFamily)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }
        private static void OnFontFamilyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (FontIcon)d;
            var val = (FontFamily)e.NewValue;
            obj.TheIcon.FontFamily = val;
        }
        #endregion

        #region FontSize Dependency Property
        public static new readonly DependencyProperty FontSizeProperty = DependencyProperty.Register("FontSize", typeof(double), typeof(FontIcon), new PropertyMetadata(24.0, new PropertyChangedCallback(OnFontSizeChanged)));
        public new double FontSize {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }
        private static void OnFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (FontIcon)d;
            var val = (double)e.NewValue;
            obj.TheIcon.FontSize = val;
        }
        #endregion

        #region Foreground Dependency Property
        public static new readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(FontIcon), new PropertyMetadata(Brushes.Black, new PropertyChangedCallback(OnForegroundChanged)));
        public new Brush Foreground {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }
        private static void OnForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (FontIcon)d;
            var val = (Brush)e.NewValue;
            obj.TheIcon.Foreground = val;
        }
        #endregion

        #region IsEnabled Dependency Property
        public static new readonly DependencyProperty IsEnabledProperty = DependencyProperty.Register("IsEnabled", typeof(bool), typeof(FontIcon), new PropertyMetadata(default(bool), new PropertyChangedCallback(OnIsEnabledChanged)));
        public new bool IsEnabled {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }
        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (FontIcon)d;
            var val = (bool)e.NewValue;
            obj.TheIcon.IsEnabled = val;
        }
        #endregion    }
    }
}