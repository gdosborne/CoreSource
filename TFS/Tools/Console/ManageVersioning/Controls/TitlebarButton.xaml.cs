using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManageVersioning.Controls {
    public partial class TitlebarButton : UserControl {
        public TitlebarButton() {
            InitializeComponent();
            ButtonType = ButtonTypes.Close;
        }

        public enum ButtonTypes {
            Minimize,
            Maximize,
            Close,
            Custom
        }

        #region ButtonType Dependency Property
        public static readonly DependencyProperty ButtonTypeProperty = DependencyProperty.Register("ButtonType", typeof(ButtonTypes), typeof(TitlebarButton), new PropertyMetadata(ButtonTypes.Close, new PropertyChangedCallback(OnButtonTypeChanged)));
        public ButtonTypes ButtonType {
            get { return (ButtonTypes)GetValue(ButtonTypeProperty); }
            set { SetValue(ButtonTypeProperty, value); }
        }
        private static void OnButtonTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitlebarButton)d;
            var val = (ButtonTypes)e.NewValue;
            obj.CharTextBlock.Text = val == ButtonTypes.Close ? "\uE8BB" : val == ButtonTypes.Minimize ? "\uE921" : val == ButtonTypes.Maximize ? "\uE922" : obj.CustomCharacter;
        }
        #endregion

        #region Background Dependency Property
        public static new readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(TitlebarButton), new PropertyMetadata(default(Brush), new PropertyChangedCallback(OnBackgroundChanged)));
        public new Brush Background {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }
        private static void OnBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitlebarButton)d;
            var val = (Brush)e.NewValue;
            obj.MainBorder.Background = val;
        }
        #endregion

        #region CornerRadius Dependency Property
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(TitlebarButton), new PropertyMetadata(default(CornerRadius), new PropertyChangedCallback(OnCornerRadiusChanged)));
        public CornerRadius CornerRadius {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitlebarButton)d;
            var val = (CornerRadius)e.NewValue;
            obj.MainBorder.CornerRadius = val;
        }
        #endregion

        #region Foreground Dependency Property
        public static new readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(TitlebarButton), new PropertyMetadata(default(Brush), new PropertyChangedCallback(OnForegroundChanged)));
        public new Brush Foreground {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }
        private static void OnForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitlebarButton)d;
            var val = (Brush)e.NewValue;
            obj.CharTextBlock.Foreground = val;
        }
        #endregion

        #region MouseOverBackground Dependency Property
        public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.Register("MouseOverBackground", typeof(Brush), typeof(TitlebarButton), new PropertyMetadata(default(Brush), new PropertyChangedCallback(OnMouseOverBackgroundChanged)));
        public Brush MouseOverBackground {
            get { return (Brush)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }
        private static void OnMouseOverBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitlebarButton)d;
            var val = (Brush)e.NewValue;
            //set value here
        }
        #endregion

        #region MouseOverForeground Dependency Property
        public static readonly DependencyProperty MouseOverForegroundProperty = DependencyProperty.Register("MouseOverForeground", typeof(Brush), typeof(TitlebarButton), new PropertyMetadata(default(Brush), new PropertyChangedCallback(OnMouseOverForegroundChanged)));
        public Brush MouseOverForeground {
            get { return (Brush)GetValue(MouseOverForegroundProperty); }
            set { SetValue(MouseOverForegroundProperty, value); }
        }
        private static void OnMouseOverForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitlebarButton)d;
            var val = (Brush)e.NewValue;
            //set value here
        }
        #endregion

        #region FontSize Dependency Property
        public static new readonly DependencyProperty FontSizeProperty = DependencyProperty.Register("FontSize", typeof(double), typeof(TitlebarButton), new PropertyMetadata(14.0, new PropertyChangedCallback(OnFontSizeChanged)));
        public new double FontSize {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }
        private static void OnFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitlebarButton)d;
            var val = (double)e.NewValue;
            obj.CharTextBlock.FontSize = val;
        }
        #endregion

        #region Command Dependency Property
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(TitlebarButton), new PropertyMetadata(default(ICommand), new PropertyChangedCallback(OnCommandChanged)));
        public ICommand Command {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitlebarButton)d;
            var val = (ICommand)e.NewValue;
            if (val != null) {
                obj.MainBorder.PreviewMouseUp += (s, e) => {
                    obj.Command.Execute(null);
                };
                val.CanExecuteChanged += (s, e) => {
                    obj.IsEnabled = s.As<ICommand>().CanExecute(null);
                };
            }
        }
        #endregion

        #region Padding Dependency Property
        public static new readonly DependencyProperty PaddingProperty = DependencyProperty.Register("Padding", typeof(Thickness), typeof(TitlebarButton), new PropertyMetadata(new Thickness(2), new PropertyChangedCallback(OnPaddingChanged)));
        public new Thickness Padding {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }
        private static void OnPaddingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitlebarButton)d;
            var val = (Thickness)e.NewValue;
            obj.Padding = val;
        }
        #endregion

        #region CustomCharacter Dependency Property
        public static readonly DependencyProperty CustomCharacterProperty = DependencyProperty.Register("CustomCharacter", typeof(string), typeof(TitlebarButton), new PropertyMetadata("\uE00A", new PropertyChangedCallback(OnCustomCharacterChanged)));
        public string CustomCharacter {
            get { return (string)GetValue(CustomCharacterProperty); }
            set { SetValue(CustomCharacterProperty, value); }
        }
        private static void OnCustomCharacterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitlebarButton)d;
            var val = (string)e.NewValue;
            if (obj.ButtonType == ButtonTypes.Custom) {
                obj.CharTextBlock.Text = val;
            }
        }
        #endregion

        #region IsEnabled Dependency Property
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.Register("IsEnabled", typeof(bool), typeof(TitlebarButton), new PropertyMetadata(true, new PropertyChangedCallback(OnIsEnabledChanged)));
        public bool IsEnabled {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }
        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitlebarButton)d;
            var val = (bool)e.NewValue;
            obj.MainBorder.IsEnabled = val;
            obj.MainBorder.Opacity = !val ? 0.5 : 1.0;
        }
        #endregion

        #region BorderThickness Dependency Property
        public static new readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(TitlebarButton), new PropertyMetadata(new Thickness(0), new PropertyChangedCallback(OnBorderThicknessChanged)));
        public new Thickness BorderThickness {
            get { return (Thickness)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }
        private static void OnBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitlebarButton)d;
            var val = (Thickness)e.NewValue;
            obj.MainBorder.BorderThickness = obj.IsBorderShownOnlyOnMouseOver ? new Thickness(0) : val;
        }
        #endregion

        #region BorderBrush Dependency Property
        public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(TitlebarButton), new PropertyMetadata(default(Brush), new PropertyChangedCallback(OnBorderBrushChanged)));
        public Brush BorderBrush {
            get { return (Brush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }
        private static void OnBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitlebarButton)d;
            var val = (Brush)e.NewValue;
            obj.MainBorder.BorderBrush = val;
        }
        #endregion

        #region IsBorderShownOnlyOnMouseOver Dependency Property
        public static readonly DependencyProperty IsBorderShownOnlyOnMouseOverProperty = DependencyProperty.Register("IsBorderShownOnlyOnMouseOver", typeof(bool), typeof(TitlebarButton), new PropertyMetadata(default(bool), new PropertyChangedCallback(OnIsBorderShownOnlyOnMouseOverChanged)));
        public bool IsBorderShownOnlyOnMouseOver {
            get { return (bool)GetValue(IsBorderShownOnlyOnMouseOverProperty); }
            set { SetValue(IsBorderShownOnlyOnMouseOverProperty, value); }
        }
        private static void OnIsBorderShownOnlyOnMouseOverChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitlebarButton)d;
            var val = (bool)e.NewValue;
            obj.MainBorder.BorderThickness = val ? new Thickness(0) : obj.BorderThickness;
        }
        #endregion

        private void MainBorder_MouseEnter(object sender, MouseEventArgs e) {
            MainBorder.Background = MouseOverBackground;
            CharTextBlock.Foreground = MouseOverForeground;
            if(IsBorderShownOnlyOnMouseOver) {
                MainBorder.BorderThickness = BorderThickness;
            }
        }

        private void MainBorder_MouseLeave(object sender, MouseEventArgs e) {
            MainBorder.Background = Background;
            CharTextBlock.Foreground = Foreground;
            if (IsBorderShownOnlyOnMouseOver) {
                MainBorder.BorderThickness = new Thickness(0);
            }
        }
    }
}
