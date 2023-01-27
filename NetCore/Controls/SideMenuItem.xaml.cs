using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Controls {
    public partial class SideMenuItem : UserControl {
        public SideMenuItem() {
            InitializeComponent();

            TheIcon.IsLink = false;
        }

        public event EventHandler Clicked;

        #region GlyphProperty
        public static readonly DependencyProperty GlyphProperty = DependencyProperty.Register("Glyph", typeof(string), typeof(SideMenuItem), new PropertyMetadata("&#xE115;", OnGlyphPropertyChanged));
        public string Glyph {
            get => (string)GetValue(GlyphProperty);
            set => SetValue(GlyphProperty, value);
        }
        private static void OnGlyphPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (SideMenuItem)d;
            var val = (string)e.NewValue;
            obj.TheIcon.Glyph = val;
        }
        #endregion

        #region GlyphFontFamilyProperty
        public static readonly DependencyProperty GlyphFontFamilyProperty = DependencyProperty.Register("GlyphFontFamily", typeof(FontFamily), typeof(SideMenuItem), new PropertyMetadata(new FontFamily("Segoe Fluent Icons"), OnGlyphFontFamilyPropertyChanged));
        public FontFamily GlyphFontFamily {
            get => (FontFamily)GetValue(GlyphFontFamilyProperty);
            set => SetValue(GlyphFontFamilyProperty, value);
        }
        private static void OnGlyphFontFamilyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (SideMenuItem)d;
            var val = (FontFamily)e.NewValue;
            obj.TheIcon.FontFamily = val;
        }
        #endregion

        #region GlyphFontSizeProperty
        public static readonly DependencyProperty GlyphFontSizeProperty = DependencyProperty.Register("GlyphFontSize", typeof(double), typeof(SideMenuItem), new PropertyMetadata(18.0, OnGlyphFontSizePropertyChanged));
        public double GlyphFontSize {
            get => (double)GetValue(GlyphFontSizeProperty);
            set => SetValue(GlyphFontSizeProperty, value);
        }
        private static void OnGlyphFontSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (SideMenuItem)d;
            var val = (double)e.NewValue;
            obj.TheIcon.FontSize = val;
        }
        #endregion

        #region TextProperty
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(SideMenuItem), new PropertyMetadata("Menu Text", OnTextPropertyChanged));
        public string Text {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (SideMenuItem)d;
            var val = (string)e.NewValue;
            obj.TheText.Text = val;
        }
        #endregion

        #region TextVisibilityProperty
        public static readonly DependencyProperty TextVisibilityProperty = DependencyProperty.Register("TextVisibility", typeof(Visibility), typeof(SideMenuItem), new PropertyMetadata(default(Visibility), OnTextVisibilityPropertyChanged));
        public Visibility TextVisibility {
            get => (Visibility)GetValue(TextVisibilityProperty);
            set => SetValue(TextVisibilityProperty, value);
        }
        private static void OnTextVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (SideMenuItem)d;
            var val = (Visibility)e.NewValue;
            obj.TheText.Visibility = val;
        }
        #endregion

        #region CursorProperty
        public static new readonly DependencyProperty CursorProperty = DependencyProperty.Register("Cursor", typeof(Cursor), typeof(SideMenuItem), new PropertyMetadata(default(Cursor), OnCursorPropertyChanged));
        public new Cursor Cursor {
            get => (Cursor)GetValue(CursorProperty);
            set => SetValue(CursorProperty, value);
        }
        private static void OnCursorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (SideMenuItem)d;
            var val = (Cursor)e.NewValue;
            obj.TheIcon.Cursor = obj.TheText.Cursor = val;
        }
        #endregion

        private void TheIcon_Clicked(object sender, EventArgs e) {
            if (Command != null) {
                Command.Execute(CommandParameter);
            }
            Clicked?.Invoke(this, EventArgs.Empty);
        }

        private bool isMouseDown = default;
        private void TheText_MouseDown(object sender, MouseButtonEventArgs e) {
            isMouseDown = true;
        }

        private void TheText_MouseUp(object sender, MouseButtonEventArgs e) {
            if (isMouseDown) {
                e.Handled = true;
                if (Command != null) {
                    Command.Execute(CommandParameter);
                }
                Clicked?.Invoke(this, EventArgs.Empty);
                isMouseDown = false;
            }
        }

        #region CommandProperty
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(SideMenuItem), new PropertyMetadata(default(ICommand), OnCommandPropertyChanged));
        public ICommand Command {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        private static void OnCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (SideMenuItem)d;
            var val = (ICommand)e.NewValue;
            //modify the value here, i.e., obj.ctrl.Value = val
        }
        #endregion

        #region CommandParameterProperty
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(SideMenuItem), new PropertyMetadata(default(object), OnCommandParameterPropertyChanged));
        public object CommandParameter {
            get => (object)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
        private static void OnCommandParameterPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (SideMenuItem)d;
            var val = (object)e.NewValue;
            //modify the value here, i.e., obj.ctrl.Value = val
        }
        #endregion

        #region ForegroundProperty
        public static new readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(SideMenuItem), new PropertyMetadata(default(Brush), OnForegroundPropertyChanged));
        public new Brush Foreground {
            get => (Brush)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }
        private static void OnForegroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (SideMenuItem)d;
            var val = (Brush)e.NewValue;
            obj.TheIcon.Foreground = val;
            obj.TheText.Foreground = val;
        }
        #endregion

        private Brush savedBg = default;
        private void UserControl_MouseEnter(object sender, MouseEventArgs e) {
            savedBg = Background;
            Background = HighlightBrush;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e) {
            Background = savedBg;
        }

        #region HighlightBrushProperty
        public static readonly DependencyProperty HighlightBrushProperty = DependencyProperty.Register("HighlightBrush", typeof(Brush), typeof(SideMenuItem), new PropertyMetadata(default(Brush), OnHighlightBrushPropertyChanged));
        public Brush HighlightBrush {
            get => (Brush)GetValue(HighlightBrushProperty);
            set => SetValue(HighlightBrushProperty, value);
        }
        private static void OnHighlightBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (SideMenuItem)d;
            var val = (Brush)e.NewValue;
            //modify the value here, i.e., obj.ctrl.Value = val
        }
        #endregion

    }
}
