using Common.Applicationn.Primitives;
using Common.Applicationn.Windows.Expressions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace Controls {
    public partial class ControlButton : UserControl {
        public ControlButton() {
            InitializeComponent();
            MouseEnter += ControlButton_MouseEnter;
            MouseLeave += ControlButton_MouseLeave;
            MouseDown += ControlButton_MouseDown;
        }

        private void ControlButton_MouseDown(object sender, MouseButtonEventArgs e) {
            Click?.Invoke(this, EventArgs.Empty);
            Command?.Execute(CommandParameter);
        }

        private void ControlButton_MouseLeave(object sender, MouseEventArgs e) {
            OuterBorder.Background = Background;
            ControlIcon.Foreground = Foreground;
        }

        private void ControlButton_MouseEnter(object sender, MouseEventArgs e) {
            OuterBorder.Background = MouseOverBackground;
            ControlIcon.Foreground = MouseOverForeground;
        }

        public event EventHandler Click;

        #region CornerRadiusProperty
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ControlButton), new FrameworkPropertyMetadata(default(CornerRadius), OnCornerRadiusPropertyChanged));
        public CornerRadius CornerRadius {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        private static void OnCornerRadiusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ControlButton)d;
            var val = (CornerRadius)e.NewValue;
            obj.OuterBorder.CornerRadius = val;
        }
        #endregion

        #region BackgroundProperty
        public new static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(ControlButton), new FrameworkPropertyMetadata(default(Brush), OnBackgroundPropertyChanged));
        public new Brush Background {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }
        private static void OnBackgroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ControlButton)d;
            var val = (Brush)e.NewValue;
            obj.OuterBorder.Background = val;
        }
        #endregion

        #region ForegroundProperty
        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(ControlButton), new FrameworkPropertyMetadata(default(Brush), OnForegroundPropertyChanged));
        public Brush Foreground {
            get => (Brush)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }
        private static void OnForegroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ControlButton)d;
            var val = (Brush)e.NewValue;
            obj.ControlIcon.Foreground = val;
        }
        #endregion

        #region MouseOverBackgroundProperty
        public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.Register("MouseOverBackground", typeof(Brush), typeof(ControlButton), new FrameworkPropertyMetadata(default(Brush), OnMouseOverBackgroundPropertyChanged));
        public Brush MouseOverBackground {
            get => (Brush)GetValue(MouseOverBackgroundProperty);
            set => SetValue(MouseOverBackgroundProperty, value);
        }
        private static void OnMouseOverBackgroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ControlButton)d;
            var val = (Brush)e.NewValue;
            //modify the value here, i.e., obj.ctrl.Value = val
        }
        #endregion

        #region MouseOverForegroundProperty
        public static readonly DependencyProperty MouseOverForegroundProperty = DependencyProperty.Register("MouseOverForeground", typeof(Brush), typeof(ControlButton), new FrameworkPropertyMetadata(default(Brush), OnMouseOverForegroundPropertyChanged));
        public Brush MouseOverForeground {
            get => (Brush)GetValue(MouseOverForegroundProperty);
            set => SetValue(MouseOverForegroundProperty, value);
        }
        private static void OnMouseOverForegroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ControlButton)d;
            var val = (Brush)e.NewValue;
            //modify the value here, i.e., obj.ctrl.Value = val
        }
        #endregion

        #region GlyphProperty
        public static readonly DependencyProperty GlyphProperty = DependencyProperty.Register("Glyph", typeof(string), typeof(ControlButton), new PropertyMetadata("&#xE115;", OnGlyphPropertyChanged));
        public string Glyph {
            get => (string)GetValue(GlyphProperty);
            set => SetValue(GlyphProperty, value);
        }
        private static void OnGlyphPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ControlButton)d;
            var val = (string)e.NewValue;
            obj.ControlIcon.Glyph = val;
        }
        #endregion

        #region GlyphSizeProperty
        public static readonly DependencyProperty GlyphSizeProperty = DependencyProperty.Register("GlyphSize", typeof(double), typeof(ControlButton), new FrameworkPropertyMetadata(10.0, OnGlyphSizePropertyChanged));
        public double GlyphSize {
            get => (double)GetValue(GlyphSizeProperty);
            set => SetValue(GlyphSizeProperty, value);
        }
        private static void OnGlyphSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ControlButton)d;
            var val = 0.0;
            if (e.NewValue.Is<Calculator>()) {
                val = e.NewValue.As<Calculator>().Expression.CalculateValue();
            }
            else {
                val = (double)e.NewValue;
            }
            obj.ControlIcon.FontSize = val;
        }
        #endregion

        #region GlyphFontFamilyProperty
        public static readonly DependencyProperty GlyphFontFamilyProperty = DependencyProperty.Register("GlyphFontFamily", typeof(FontFamily), typeof(ControlButton), new FrameworkPropertyMetadata(new FontFamily("Segoe Fluent Icons"), OnGlyphFontFamilyPropertyChanged));
        public FontFamily GlyphFontFamily {
            get => (FontFamily)GetValue(GlyphFontFamilyProperty);
            set => SetValue(GlyphFontFamilyProperty, value);
        }
        private static void OnGlyphFontFamilyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ControlButton)d;
            var val = (FontFamily)e.NewValue;
            obj.ControlIcon.FontFamily = val;
        }
        #endregion

        #region CommandProperty
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(ControlButton), new FrameworkPropertyMetadata(default(ICommand), OnCommandPropertyChanged));
        public ICommand Command {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        private static void OnCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ControlButton)d;
            var val = (ICommand)e.NewValue;
            //modify the value here, i.e., obj.ctrl.Value = val
        }
        #endregion

        #region CommandParameterProperty
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(string), typeof(ControlButton), new FrameworkPropertyMetadata(default(string), OnCommandParameterPropertyChanged));
        public string CommandParameter {
            get => (string)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
        private static void OnCommandParameterPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ControlButton)d;
            var val = (string)e.NewValue;
            //modify the value here, i.e., obj.ctrl.Value = val
        }
        #endregion

        #region PaddingProperty
        public new static readonly DependencyProperty PaddingProperty = DependencyProperty.Register("Padding", typeof(Thickness), typeof(ControlButton), new FrameworkPropertyMetadata(new Thickness(5), OnPaddingPropertyChanged));
        public new Thickness Padding {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }
        private static void OnPaddingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ControlButton)d;
            var val = (Thickness)e.NewValue;
            obj.ControlIcon.Margin = val;
        }
        #endregion

        #region BorderBrushProperty
        /// <summary>Gets the BorderBrush dependency property.</summary>
        /// <value>The BorderBrush dependency property.</value>
        public static new readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(ControlButton), new PropertyMetadata(default(Brush), OnBorderBrushPropertyChanged));
        /// <summary>Gets/sets the BorderBrush.</summary>
        /// <value>The BorderBrush.</value>
        public new Brush BorderBrush {
            get => (Brush)GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }
        private static void OnBorderBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ControlButton)d;
            var val = (Brush)e.NewValue;
            obj.OuterBorder.BorderBrush = val;
        }
        #endregion

        #region BorderThicknessProperty
        /// <summary>Gets the BorderThickness dependency property.</summary>
        /// <value>The BorderThickness dependency property.</value>
        public static new readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(ControlButton), new PropertyMetadata(default(Thickness), OnBorderThicknessPropertyChanged));
        /// <summary>Gets/sets the BorderThickness.</summary>
        /// <value>The BorderThickness.</value>
        public new Thickness BorderThickness {
            get => (Thickness)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }
        private static void OnBorderThicknessPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (ControlButton)d;
            var val = (Thickness)e.NewValue;
            obj.OuterBorder.BorderThickness = val;
        }
        #endregion

    }
}
