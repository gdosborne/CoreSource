using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WPFControls {
    public partial class ToggleSwitch : UserControl {
        public ToggleSwitch() {
            InitializeComponent();
            IsChecked = true;
        }

        public event EventHandler ToggleIsChecked;
        public event EventHandler ToggleIsUnchecked;

        private readonly double iconRatio = 2.5;

        #region FontSize Dependency Property
        public static new readonly DependencyProperty FontSizeProperty = DependencyProperty.Register("FontSize", typeof(double), typeof(ToggleSwitch), new PropertyMetadata(12.0, new PropertyChangedCallback(OnFontSizeChanged)));
        public new double FontSize {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }
        private static void OnFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ToggleSwitch)d;
            var val = (double)e.NewValue;
            obj.OnOffText.FontSize = val;
            obj.ToggleBackground.FontSize = val * obj.iconRatio;
            obj.ToggleLeft.FontSize = val * obj.iconRatio;
            obj.ToggleRight.FontSize = val * obj.iconRatio;
        }
        #endregion

        #region Foreground Dependency Property
        public static new readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(ToggleSwitch), new PropertyMetadata(Brushes.Black, new PropertyChangedCallback(OnForegroundChanged)));
        public new Brush Foreground {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }
        private static void OnForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ToggleSwitch)d;
            var val = (Brush)e.NewValue;
            obj.OnOffText.Foreground = val;
            obj.ToggleLeft.Foreground = val;
            obj.ToggleRight.Foreground = val;
        }
        #endregion

        #region OnBackground Dependency Property
        public static readonly DependencyProperty OnBackgroundProperty = DependencyProperty.Register("OnBackground", typeof(Brush), typeof(ToggleSwitch), new PropertyMetadata(Brushes.LightBlue, new PropertyChangedCallback(OnOnBackgroundChanged)));
        public Brush OnBackground {
            get { return (Brush)GetValue(OnBackgroundProperty); }
            set { SetValue(OnBackgroundProperty, value); }
        }
        private static void OnOnBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ToggleSwitch)d;
            var val = (Brush)e.NewValue;
            obj.ToggleBackground.Foreground = val;
        }
        #endregion

        #region OnText Dependency Property
        public static readonly DependencyProperty OnTextProperty = DependencyProperty.Register("OnText", typeof(string), typeof(ToggleSwitch), new PropertyMetadata("On", new PropertyChangedCallback(OnOnTextChanged)));
        public string OnText {
            get { return (string)GetValue(OnTextProperty); }
            set { SetValue(OnTextProperty, value); }
        }
        private static void OnOnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ToggleSwitch)d;
            var val = (string)e.NewValue;
            if (obj.IsChecked) {
                obj.OnOffText.Text = obj.OnText;
            }
        }
        #endregion

        #region OffText Dependency Property
        public static readonly DependencyProperty OffTextProperty = DependencyProperty.Register("OffText", typeof(string), typeof(ToggleSwitch), new PropertyMetadata("Off", new PropertyChangedCallback(OnOffTextChanged)));
        public string OffText {
            get { return (string)GetValue(OffTextProperty); }
            set { SetValue(OffTextProperty, value); }
        }
        private static void OnOffTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ToggleSwitch)d;
            var val = (string)e.NewValue;
            if (!obj.IsChecked) {
                obj.OnOffText.Text = obj.OffText;
            }
        }
        #endregion

        #region IsChecked Dependency Property
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register("IsChecked", typeof(bool), typeof(ToggleSwitch), new PropertyMetadata(false, new PropertyChangedCallback(OnIsCheckedChanged)));
        public bool IsChecked {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }
        private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ToggleSwitch)d;
            var val = (bool)e.NewValue;
            obj.ToggleBackground.Foreground = val ? obj.OnBackground : obj.Background;
            obj.OnOffText.Text = val ? obj.OnText : obj.OffText;
            obj.ToggleLeft.Visibility = val ? Visibility.Collapsed : Visibility.Visible;
            obj.ToggleRight.Visibility = val ? Visibility.Visible : Visibility.Collapsed;
            if (val) {
                obj.ToggleIsChecked?.Invoke(obj, EventArgs.Empty);
            }
            else {
                obj.ToggleIsUnchecked?.Invoke(obj, EventArgs.Empty);
            }
        }
        #endregion

        #region IsEnabled Dependency Property
        public static new readonly DependencyProperty IsEnabledProperty = DependencyProperty.Register("IsEnabled", typeof(bool), typeof(ToggleSwitch), new PropertyMetadata(true, new PropertyChangedCallback(OnIsEnabledChanged)));
        public new bool IsEnabled {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }
        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ToggleSwitch)d;
            var val = (bool)e.NewValue;
            obj.Opacity = val ? 1.0 : 0.4;
        }
        #endregion

        private void OnOffText_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            IsChecked = !IsChecked;
        }

        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            var fullWidth = ((Grid)sender).ActualWidth;
            var pos = e.GetPosition((Grid)sender);
            var breakPoint = fullWidth / 2.0;
            IsChecked = pos.X > breakPoint;
        }
    }
}
