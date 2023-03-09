using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Controls.Core {
    public partial class FontIcon : UserControl {
        public FontIcon() {
            InitializeComponent();

        }

        public event EventHandler Clicked;

        #region IsVisibleProperty
        /// <summary>Gets the IsVisible dependency property.</summary>
        /// <value>The IsVisible dependency property.</value>
        public new static readonly DependencyProperty IsVisibleProperty = DependencyProperty.Register("IsVisible", typeof(bool), typeof(FontIcon), new PropertyMetadata(default(bool), OnIsVisiblePropertyChanged));
        /// <summary>Gets/sets the IsVisible.</summary>
        /// <value>The IsVisible.</value>
        public new bool IsVisible {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }
        private static void OnIsVisiblePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (FontIcon)d;
            var val = (bool)e.NewValue;
            //modify the value here, i.e., obj.ctrl.Value = val
        }
        #endregion

        #region BackgroundProperty
        public static new readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(FontIcon), new PropertyMetadata(default(Brush), OnBackgroundPropertyChanged));
        public new Brush Background {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }
        private static void OnBackgroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (FontIcon)d;
            var val = (Brush)e.NewValue;
            obj.MainGrid.Background = val;
        }
        #endregion

        #region FontFamilyProperty
        public static new readonly DependencyProperty FontFamilyProperty = DependencyProperty.Register("FontFamily", typeof(FontFamily), typeof(FontIcon), new PropertyMetadata(new FontFamily("Segoe Fluent Icons"), OnFontFamilyPropertyChanged));
        public new FontFamily FontFamily {
            get => (FontFamily)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }
        private static void OnFontFamilyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (FontIcon)d;
            var val = (FontFamily)e.NewValue;
            if (val != null)
                obj.TheIcon.FontFamily = val;
        }
        #endregion

        #region IsLinkProperty
        public static readonly DependencyProperty IsLinkProperty = DependencyProperty.Register("IsLink", typeof(bool), typeof(FontIcon), new PropertyMetadata(default(bool), OnIsLinkPropertyChanged));
        public bool IsLink {
            get => (bool)GetValue(IsLinkProperty);
            set => SetValue(IsLinkProperty, value);
        }
        private static void OnIsLinkPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (FontIcon)d;
            var val = (bool)e.NewValue;

        }
        #endregion

        #region FontSizeProperty
        public static new readonly DependencyProperty FontSizeProperty = DependencyProperty.Register("FontSize", typeof(double), typeof(FontIcon), new PropertyMetadata(18.0, OnFontSizePropertyChanged));
        public new double FontSize {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }
        private static void OnFontSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (FontIcon)d;
            var val = (double)e.NewValue;
            obj.TheIcon.FontSize = val > 0 ? val : obj.TheIcon.FontSize;
        }
        #endregion

        #region GlyphProperty
        public static readonly DependencyProperty GlyphProperty = DependencyProperty.Register("Glyph", typeof(string), typeof(FontIcon), new PropertyMetadata("&#xE115;", OnGlyphPropertyChanged));
        public string Glyph {
            get => (string)GetValue(GlyphProperty);
            set => SetValue(GlyphProperty, value);
        }
        private static void OnGlyphPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (FontIcon)d;
            var val = (string)e.NewValue;
            //System.Diagnostics.Debug.WriteLine($"Setting Glyph to {val}");
            obj.TheIcon.Text = val;
        }
        #endregion

        #region CursorProperty
        public static new readonly DependencyProperty CursorProperty = DependencyProperty.Register("Cursor", typeof(Cursor), typeof(FontIcon), new PropertyMetadata(default(Cursor), OnCursorPropertyChanged));
        public new Cursor Cursor {
            get => (Cursor)GetValue(CursorProperty);
            set => SetValue(CursorProperty, value);
        }
        private static void OnCursorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (FontIcon)d;
            var val = (Cursor)e.NewValue;
            obj.TheIcon.Cursor = val;
        }
        #endregion

        #region ForegroundProperty
        public static new readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(FontIcon), new PropertyMetadata(default(Brush), OnForegroundPropertyChanged));
        public new Brush Foreground {
            get => (Brush)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }
        private static void OnForegroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (FontIcon)d;
            var val = (Brush)e.NewValue;
            obj.TheIcon.Foreground = val;
        }
        #endregion

        #region IsEnabledProperty
        public static new readonly DependencyProperty IsEnabledProperty = DependencyProperty.Register("IsEnabled", typeof(bool), typeof(FontIcon), new FrameworkPropertyMetadata(true, OnIsEnabledPropertyChanged));
        public new bool IsEnabled {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }
        private static void OnIsEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (FontIcon)d;
            var val = (bool)e.NewValue;
            obj.TheIcon.Opacity = val ? 1.0 : 0.35;
        }
        #endregion

        #region VisibilityProperty
        /// <summary>Gets the Visibility dependency property.</summary>
        /// <value>The Visibility dependency property.</value>
        public new static readonly DependencyProperty VisibilityProperty = DependencyProperty.Register("Visibility", typeof(Visibility), typeof(FontIcon), new PropertyMetadata(default(Visibility), OnVisibilityPropertyChanged));
        /// <summary>Gets/sets the Visibility.</summary>
        /// <value>The Visibility.</value>
        public new Visibility Visibility {
            get => (Visibility)GetValue(VisibilityProperty);
            set => SetValue(VisibilityProperty, value);
        }
        private static void OnVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (FontIcon)d;
            var val = (Visibility)e.NewValue;
            obj.Visibility = val;
            obj.IsVisible = obj.Visibility == Visibility.Visible;
        }
        #endregion

        private bool isMouseDown = default;
        private void UserControl_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            isMouseDown = true;
        }

        private void UserControl_PreviewMouseUp(object sender, MouseButtonEventArgs e) {
            if (isMouseDown) {
                e.Handled = true;
                Clicked?.Invoke(this, EventArgs.Empty);
                isMouseDown = false;
            }
        }
    }
}
