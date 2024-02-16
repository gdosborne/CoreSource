using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ManageVersioning.Controls {
    public partial class Toggle : UserControl {
        private const string falseToggleValue = "\uF19E";
        private const string trueToggleValue = "\uF19F";

        public Toggle() {
            InitializeComponent();
            foregroundCharacter.Text = IsChecked ? trueToggleValue : falseToggleValue;
            toggleText.Text = IsChecked ? TrueText : FalseText;
        }

        #region Background Dependency Property
        public static new readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(Toggle), new PropertyMetadata(Brushes.Transparent, new PropertyChangedCallback(OnBackgroundChanged)));
        public new Brush Background {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }
        private static void OnBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Toggle)d;
            var val = (Brush)e.NewValue;
            obj.theGrid.Background = val;
        }
        #endregion

        #region ToggleForeground Dependency Property
        public static readonly DependencyProperty ToggleForegroundProperty = DependencyProperty.Register("ToggleForeground", typeof(Brush), typeof(Toggle), new PropertyMetadata(default(Brush), new PropertyChangedCallback(OnToggleForegroundChanged)));
        public Brush ToggleForeground {
            get { return (Brush)GetValue(ToggleForegroundProperty); }
            set { SetValue(ToggleForegroundProperty, value); }
        }
        private static void OnToggleForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Toggle)d;
            var val = (Brush)e.NewValue;
            obj.foregroundCharacter.Foreground = obj.IsChecked ? val : obj.ToggleOffForeground;
        }
        #endregion

        #region ToggleOffForeground Dependency Property
        public static readonly DependencyProperty ToggleOffForegroundProperty = DependencyProperty.Register("ToggleOffForeground", typeof(Brush), typeof(Toggle), new PropertyMetadata(Brushes.Black, new PropertyChangedCallback(OnToggleOffForegroundChanged)));
        public Brush ToggleOffForeground {
            get { return (Brush)GetValue(ToggleOffForegroundProperty); }
            set { SetValue(ToggleOffForegroundProperty, value); }
        }
        private static void OnToggleOffForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Toggle)d;
            var val = (Brush)e.NewValue;
            obj.foregroundCharacter.Foreground = obj.IsChecked ? obj.ToggleForeground : val;
        }
        #endregion

        #region ToggleOffBackground Dependency Property
        public static readonly DependencyProperty ToggleOffBackgroundProperty = DependencyProperty.Register("ToggleOffBackground", typeof(Brush), typeof(Toggle), new PropertyMetadata(Brushes.Transparent, new PropertyChangedCallback(OnToggleOffBackgroundChanged)));
        public Brush ToggleOffBackground {
            get { return (Brush)GetValue(ToggleOffBackgroundProperty); }
            set { SetValue(ToggleOffBackgroundProperty, value); }
        }
        private static void OnToggleOffBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Toggle)d;
            var val = (Brush)e.NewValue;
            obj.backgroundCharacter.Foreground = obj.IsChecked ? obj.ToggleBackground : val;
        }
        #endregion

        #region ToggleBackground Dependency Property
        public static readonly DependencyProperty ToggleBackgroundProperty = DependencyProperty.Register("ToggleBackground", typeof(Brush), typeof(Toggle), new PropertyMetadata(Brushes.Transparent, new PropertyChangedCallback(OnToggleBackgroundChanged)));
        public Brush ToggleBackground {
            get { return (Brush)GetValue(ToggleBackgroundProperty); }
            set { SetValue(ToggleBackgroundProperty, value); }
        }
        private static void OnToggleBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Toggle)d;
            var val = (Brush)e.NewValue;
            obj.backgroundCharacter.Foreground = obj.IsChecked ? val : obj.ToggleOffBackground;
        }
        #endregion

        #region ToggleSize Dependency Property
        public static readonly DependencyProperty ToggleSizeProperty = DependencyProperty.Register("ToggleSize", typeof(double), typeof(Toggle), new PropertyMetadata(24.0, new PropertyChangedCallback(OnToggleSizeChanged)));
        public double ToggleSize {
            get { return (double)GetValue(ToggleSizeProperty); }
            set { SetValue(ToggleSizeProperty, value); }
        }
        private static void OnToggleSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Toggle)d;
            var val = (double)e.NewValue;
            obj.backgroundCharacter.FontSize = val;
            obj.foregroundCharacter.FontSize = val;
        }
        #endregion

        #region IsChecked Dependency Property
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register("IsChecked", typeof(bool), typeof(Toggle), new PropertyMetadata(false, new PropertyChangedCallback(OnIsCheckedChanged)));
        public bool IsChecked {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }
        private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Toggle)d;
            var val = (bool)e.NewValue;
            obj.backgroundCharacter.Foreground = val ? obj.ToggleBackground : obj.ToggleOffBackground;
            obj.foregroundCharacter.Foreground = val ? obj.Foreground : obj.ToggleOffForeground;
            obj.foregroundCharacter.Text = val ? trueToggleValue : falseToggleValue;
            obj.toggleText.Text = val ? obj.TrueText : obj.FalseText;
        }
        #endregion

        #region TrueText Dependency Property
        public static readonly DependencyProperty TrueTextProperty = DependencyProperty.Register("TrueText", typeof(string), typeof(Toggle), new PropertyMetadata("Yes", new PropertyChangedCallback(OnTrueTextChanged)));
        public string TrueText {
            get { return (string)GetValue(TrueTextProperty); }
            set { SetValue(TrueTextProperty, value); }
        }
        private static void OnTrueTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Toggle)d;
            var val = (string)e.NewValue;
            if(obj.IsChecked) {
                obj.toggleText.Text = val;
            }
        }
        #endregion

        #region FalseText Dependency Property
        public static readonly DependencyProperty FalseTextProperty = DependencyProperty.Register("FalseText", typeof(string), typeof(Toggle), new PropertyMetadata("No", new PropertyChangedCallback(OnFalseTextChanged)));
        public string FalseText {
            get { return (string)GetValue(FalseTextProperty); }
            set { SetValue(FalseTextProperty, value); }
        }

        private static void OnFalseTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Toggle)d;
            var val = (string)e.NewValue;
            if(!obj.IsChecked) {
                obj.toggleText.Text = val;
            }
        }
        #endregion

        private void toggleForeground_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            var tb = sender.As<TextBlock>();
            var fullWidth = tb.ActualWidth;
            if (e.LeftButton == MouseButtonState.Pressed) {
                var pos = e.GetPosition(tb);
                IsChecked = pos.X > fullWidth / 2.0;
                e.Handled = true;
            }
        }
    }
}
