namespace GregOsborne.Controls {
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public partial class ToggleSwitch : UserControl {
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register("IsChecked", typeof(bool?), typeof(ToggleSwitch), new FrameworkPropertyMetadata(null, OnIsCheckedPropertyChanged, CoerceIsCheckedProperty));
        public static readonly DependencyProperty OffTextProperty = DependencyProperty.Register("OffText", typeof(string), typeof(ToggleSwitch), new FrameworkPropertyMetadata("Off", OnOffTextPropertyChanged, CoerceOffTextProperty));
        public static readonly DependencyProperty OnTextProperty = DependencyProperty.Register("OnText", typeof(string), typeof(ToggleSwitch), new FrameworkPropertyMetadata("on", OnOnTextPropertyChanged, CoerceOnTextProperty));
        public static readonly DependencyProperty ToggleBackgroundProperty = DependencyProperty.Register("ToggleBackground", typeof(Brush), typeof(ToggleSwitch), new FrameworkPropertyMetadata(Brushes.Black, OnToggleBackgroundPropertyChanged, CoerceTaggleBackgroundProperty));
        public static readonly DependencyProperty ToggleButtonSizeProperty = DependencyProperty.Register("ToggleButtonSize", typeof(double), typeof(ToggleSwitch), new FrameworkPropertyMetadata(40.0, OnToggleButtonSizePropertyChanged, CoerceToggleButtonSizeProperty));
        public static readonly DependencyProperty ToggleForegroundProperty = DependencyProperty.Register("ToggleForeground", typeof(Brush), typeof(ToggleSwitch), new FrameworkPropertyMetadata(Brushes.White, OnToggleForegroundPropertyChanged, CoerceToggleForegroundProperty));

        public ToggleSwitch() {
            this.InitializeComponent();
            this.IsChecked = null;
        }

        public event EventHandler Checked;

        public event EventHandler Unchecked;

        public bool? IsChecked {
            get => (bool?)this.GetValue(IsCheckedProperty);
            set => this.SetValue(IsCheckedProperty, value);
        }

        public string OffText {
            get => (string)this.GetValue(OffTextProperty);
            set => this.SetValue(OffTextProperty, value);
        }

        public string OnText {
            get => (string)this.GetValue(OnTextProperty);
            set => this.SetValue(OnTextProperty, value);
        }

        public Brush ToggleBackground {
            get => (Brush)this.GetValue(ToggleBackgroundProperty);
            set => this.SetValue(ToggleBackgroundProperty, value);
        }

        public double ToggleButtonSize {
            get => (double)this.GetValue(ToggleButtonSizeProperty);
            set => this.SetValue(ToggleButtonSizeProperty, value);
        }

        public Brush ToggleForeground {
            get => (Brush)this.GetValue(ToggleForegroundProperty);
            set => this.SetValue(ToggleForegroundProperty, value);
        }

        private static object CoerceIsCheckedProperty(DependencyObject d, object value) {
            var val = (bool?)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceOffTextProperty(DependencyObject d, object value) {
            var val = (string)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceOnTextProperty(DependencyObject d, object value) {
            var val = (string)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceTaggleBackgroundProperty(DependencyObject d, object value) {
            var val = (Brush)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceToggleButtonSizeProperty(DependencyObject d, object value) {
            var val = (double)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceToggleForegroundProperty(DependencyObject d, object value) {
            var val = (Brush)value;
            //coerce value here if necessary
            return val;
        }

        private static void OnIsCheckedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ToggleSwitch)d;
            var val = (bool?)e.NewValue;
            obj.IsChecked = val;
            var margin = -(obj.ToggleButtonSize / 4);
            obj.tbTab.HorizontalAlignment = obj.IsChecked.HasValue && obj.IsChecked.Value ? HorizontalAlignment.Left : HorizontalAlignment.Right;
            obj.tbTab.Margin = new Thickness(margin, 0, margin, 0);
        }

        private static void OnOffTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ToggleSwitch)d;
            var val = (string)e.NewValue;
            obj.tbOffText.Text = val;
        }

        private static void OnOnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ToggleSwitch)d;
            var val = (string)e.NewValue;
            obj.tbOnText.Text = val;
        }

        private static void OnToggleBackgroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ToggleSwitch)d;
            var val = (Brush)e.NewValue;
            obj.tbBack.Foreground = val;
        }

        private static void OnToggleButtonSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ToggleSwitch)d;
            var val = (double)e.NewValue;
            obj.tbBack.FontSize = val;
            obj.tbTab.FontSize = val;
            var margin = -(val / 4);
            obj.tbTab.HorizontalAlignment = obj.IsChecked.HasValue && obj.IsChecked.Value ? HorizontalAlignment.Left : HorizontalAlignment.Right;
            obj.tbTab.Margin = new Thickness(margin, 0, margin, 0);
        }

        private static void OnToggleForegroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ToggleSwitch)d;
            var val = (Brush)e.NewValue;
            obj.tbTab.Foreground = val;
            //obj.tbOffText.Foreground = val;
            //obj.tbOnText.Foreground = val;
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            var grid = (Grid)sender;
            var midpoint = grid.ActualWidth / 2;
            var x = e.MouseDevice.GetPosition((Grid)sender).X;
            if (x < midpoint) {
                this.IsChecked = true;
                Checked?.Invoke(this, EventArgs.Empty);
            } else {
                this.IsChecked = false;
                Unchecked?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
