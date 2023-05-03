using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using GregOsborne.Application.Primitives;

namespace GregOsborne.Controls {
    public partial class TitleBar {
        public static readonly DependencyProperty TitleDependencyProperty = DependencyProperty.Register("Title", typeof(string), typeof(TitleBar), new FrameworkPropertyMetadata("Titlebar", MeasurablePropertyOptions, OnTitlePropertyChanged, CoerceTitleProperty));
        public new static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(TitleBar), new FrameworkPropertyMetadata(Brushes.AntiqueWhite, FrameworkPropertyMetadataOptions.None, OnBackgroundPropertyChanged, CoerceBackgroundProperty));
        public new static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(TitleBar), new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.None, OnForegroundPropertyChanged, CoerceForegroundProperty));
        public static readonly DependencyProperty ButtonForegroundProperty = DependencyProperty.Register("ButtonForeground", typeof(Brush), typeof(TitleBar), new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.None, OnButtonForegroundChanged, CoerceButtonForeground));
        public static readonly DependencyProperty MinimizeVisibilityProperty = DependencyProperty.Register("MinimizeVisibility", typeof(Visibility), typeof(TitleBar), new FrameworkPropertyMetadata(Visibility.Visible, MeasurablePropertyOptions, OnMinimizeVisibilityChanged, CoerceMinimizeVisibility));
        public static readonly DependencyProperty MaximizeVisibilityProperty = DependencyProperty.Register("MaximizeVisibility", typeof(Visibility), typeof(TitleBar), new FrameworkPropertyMetadata(Visibility.Visible, MeasurablePropertyOptions, OnMaximizeVisibilityChanged, CoerceMaximizeVisibility));
        public static readonly DependencyProperty CloseVisibilityProperty = DependencyProperty.Register("CloseVisibility", typeof(Visibility), typeof(TitleBar), new FrameworkPropertyMetadata(Visibility.Visible, MeasurablePropertyOptions, OnCloseVisibilityChanged, CoerceCloseVisibility));
        public new static readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(TitleBar), new FrameworkPropertyMetadata(new Thickness(0), MeasurablePropertyOptions, OnBorderThicknessChanged, CoerceBorderThickness));
        public new static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(TitleBar), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, OnBorderBrushChanged, CoerceBorderBrush));
        public new static readonly DependencyProperty FontFamilyProperty = DependencyProperty.Register("FontFamily", typeof(FontFamily), typeof(TitleBar), new FrameworkPropertyMetadata(new FontFamily("Segoe UI"), MeasurablePropertyOptions, OnFontFamilyChanged, CoerceFontFamily));
        public new static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register("FontSize", typeof(double), typeof(TitleBar), new FrameworkPropertyMetadata(12.0, MeasurablePropertyOptions, OnFontSizeChanged, CoerceFontSize));
        public new static readonly DependencyProperty FontWeightProperty = DependencyProperty.Register("FontWeight", typeof(FontWeight), typeof(TitleBar), new FrameworkPropertyMetadata(FontWeights.Normal, MeasurablePropertyOptions, OnFontWeightChanged, CoerceFontWeight));
        public new static readonly DependencyProperty FontStretchProperty = DependencyProperty.Register("FontStretch", typeof(FontStretch), typeof(TitleBar), new FrameworkPropertyMetadata(FontStretches.Normal, MeasurablePropertyOptions, OnFontStretchChanged, CoerceFontStretch));
        public new static readonly DependencyProperty FontStyleProperty = DependencyProperty.Register("FontStyle", typeof(FontStyle), typeof(TitleBar), new FrameworkPropertyMetadata(FontStyles.Normal, MeasurablePropertyOptions, OnFontStyleChanged, CoerceFontStyle));
        public new static readonly DependencyProperty EffectProperty = DependencyProperty.Register("Effect", typeof(Effect), typeof(TitleBar), new FrameworkPropertyMetadata(null, MeasurablePropertyOptions, OnEffectChanged, CoerceEffect));
        public static readonly DependencyProperty CloseCommandProperty = DependencyProperty.Register("CloseCommand", typeof(ICommand), typeof(TitleBar), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, OnCloseCommandChanged, CoerceCloseCommand));
        public static readonly DependencyProperty CloseCommandParameterProperty = DependencyProperty.Register("CloseCommandParameter", typeof(object), typeof(TitleBar), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, OnCloseCommandParameterChanged, CoerceCloseCommandParameter));
        public static readonly DependencyProperty MinimizeCommandProperty = DependencyProperty.Register("MinimizeCommand", typeof(ICommand), typeof(TitleBar), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, OnMinimizeCommandChanged, CoerceMinimizeCommand));
        public static readonly DependencyProperty MinimizeCommandParameterProperty = DependencyProperty.Register("MinimizeCommandParameter", typeof(object), typeof(TitleBar), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, OnMinimizeCommandParameterChanged, CoerceMinimizeCommandParameter));
        public static readonly DependencyProperty MaximizeCommandProperty = DependencyProperty.Register("MaximizeCommand", typeof(ICommand), typeof(TitleBar), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, OnMaximizeCommandChanged, CoerceMaximizeCommand));
        public static readonly DependencyProperty MaximizeCommandParameterProperty = DependencyProperty.Register("MaximizeCommandParameter", typeof(object), typeof(TitleBar), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, OnMaximizeCommandParameterChanged, CoerceMaximizeCommandParameter));
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(TitleBar), new FrameworkPropertyMetadata(null, MeasurablePropertyOptions, OnImageSourceChanged, CoerceImageSource));
        public static readonly DependencyProperty ImageSizeProperty = DependencyProperty.Register("ImageSize", typeof(double), typeof(TitleBar), new FrameworkPropertyMetadata(16.0, MeasurablePropertyOptions, OnImageSizeChanged, CoerceImageSize));
        public static readonly DependencyProperty ContextMenuVisibilityProperty = DependencyProperty.Register("ContextMenuVisibility", typeof(Visibility), typeof(TitleBar), new FrameworkPropertyMetadata(Visibility.Collapsed, MeasurablePropertyOptions, OnContextMenuVisibilityChanged, CoerceContextMenuVisibility));
        public static readonly DependencyProperty HelpMenuItemVisibilityProperty = DependencyProperty.Register("HelpMenuItemVisibility", typeof(Visibility), typeof(TitleBar), new FrameworkPropertyMetadata(Visibility.Collapsed, MeasurablePropertyOptions, OnHelpMenuItemVisibilityChanged, CoerceHelpMenuItemVisibility));
        public static readonly DependencyProperty HelpShowTextProperty = DependencyProperty.Register("HelpShowText", typeof(string), typeof(TitleBar), new FrameworkPropertyMetadata("Show", MeasurablePropertyOptions, OnHelpShowTextChanged, CoerceHelpShowText));
        public static readonly DependencyProperty HelpAboutTextProperty = DependencyProperty.Register("HelpAboutText", typeof(string), typeof(TitleBar), new FrameworkPropertyMetadata("About", MeasurablePropertyOptions, OnHelpAboutTextChanged, CoerceHelpAboutText));
        public static readonly DependencyProperty ShowHelpCommandProperty = DependencyProperty.Register("ShowHelpCommand", typeof(ICommand), typeof(TitleBar), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, OnShowHelpCommandChanged, CoerceShowHelpCommand));
        public static readonly DependencyProperty AboutCommandProperty = DependencyProperty.Register("AboutCommand", typeof(ICommand), typeof(TitleBar), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, OnAboutCommandChanged, CoerceAboutCommand));
        public static readonly DependencyProperty HowHelpCommandParameterProperty = DependencyProperty.Register("HowHelpCommandParameter", typeof(object), typeof(TitleBar), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, OnHowHelpCommandParameterChanged, CoerceHowHelpCommandParameter));
        public static readonly DependencyProperty HelpAboutCommandParameterProperty = DependencyProperty.Register("HelpAboutCommandParameter", typeof(object), typeof(TitleBar), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, OnHelpAboutCommandParameterChanged, CoerceHelpAboutCommandParameter));
        public static readonly DependencyProperty ButtonMouseOverBackgroundProperty = DependencyProperty.Register("ButtonMouseOverBackground", typeof(Brush), typeof(TitleBar), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.LightGray), FrameworkPropertyMetadataOptions.None, OnButtonMouseOverBackgroundChanged, CoerceButtonMouseOverBackground));
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(TitleBar), new FrameworkPropertyMetadata(new CornerRadius(5, 5, 0, 0), MeasurablePropertyOptions, OnCornerRadiusChanged, CoerceCornerRadius));
        public static readonly DependencyProperty ContentOffsetProperty = DependencyProperty.Register("ContentOffset", typeof(double), typeof(TitleBar), new FrameworkPropertyMetadata(0.0, MeasurablePropertyOptions, OnContentOffsetChanged, CoerceContentOffset));
        public static readonly DependencyProperty ImageVisibilityProperty = DependencyProperty.Register("ImageVisibility", typeof(Visibility), typeof(TitleBar), new FrameworkPropertyMetadata(Visibility.Visible, MeasurablePropertyOptions, OnImageVisibilityChanged, CoerceImageVisibility));
        public static readonly DependencyProperty ApplicationMenuProperty = DependencyProperty.Register("ApplicationMenu", typeof(Menu), typeof(TitleBar), new FrameworkPropertyMetadata(null, MeasurablePropertyOptions, OnApplicationMenuChanged, CoerceApplicationMenu));
        public static readonly DependencyProperty ApplicationMenuOffsetProperty = DependencyProperty.Register("ApplicationMenuOffset", typeof(double), typeof(TitleBar), new FrameworkPropertyMetadata(0.0, MeasurablePropertyOptions, OnApplicationMenuOffsetChanged, CoerceApplicationMenuOffset));

        private const FrameworkPropertyMetadataOptions MeasurablePropertyOptions = FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange | FrameworkPropertyMetadataOptions.AffectsParentMeasure;
        private bool _hasPersonalSeparator;
        private int _lastIndex;

        public TitleBar() {
            InitializeComponent();
        }

        public string Title {
            get => (string) GetValue(TitleDependencyProperty);
            set => SetValue(TitleDependencyProperty, value);
        }

        public new Brush Background {
            get => (Brush) GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public new Brush Foreground {
            get => (Brush) GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }

        public Brush ButtonForeground {
            get => (Brush) GetValue(ButtonForegroundProperty);
            set => SetValue(ButtonForegroundProperty, value);
        }

        public Visibility MinimizeVisibility {
            get => (Visibility) GetValue(MinimizeVisibilityProperty);
            set => SetValue(MinimizeVisibilityProperty, value);
        }

        public Visibility MaximizeVisibility {
            get => (Visibility) GetValue(MaximizeVisibilityProperty);
            set => SetValue(MaximizeVisibilityProperty, value);
        }

        public Visibility CloseVisibility {
            get => (Visibility) GetValue(CloseVisibilityProperty);
            set => SetValue(CloseVisibilityProperty, value);
        }

        public new Thickness BorderThickness {
            get => (Thickness) GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        public new Brush BorderBrush {
            get => (Brush) GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }

        public new FontFamily FontFamily {
            get => (FontFamily) GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public new double FontSize {
            get => (double) GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public new FontWeight FontWeight {
            get => (FontWeight) GetValue(FontWeightProperty);
            set => SetValue(FontWeightProperty, value);
        }

        public new FontStretch FontStretch {
            get => (FontStretch) GetValue(FontStretchProperty);
            set => SetValue(FontStretchProperty, value);
        }

        public new FontStyle FontStyle {
            get => (FontStyle) GetValue(FontStyleProperty);
            set => SetValue(FontStyleProperty, value);
        }

        public new Effect Effect {
            get => (Effect) GetValue(EffectProperty);
            set => SetValue(EffectProperty, value);
        }

        public ICommand CloseCommand {
            get => (ICommand) GetValue(CloseCommandProperty);
            set => SetValue(CloseCommandProperty, value);
        }

        public object CloseCommandParameter {
            get => GetValue(CloseCommandParameterProperty);
            set => SetValue(CloseCommandParameterProperty, value);
        }

        public ICommand MinimizeCommand {
            get => (ICommand) GetValue(MinimizeCommandProperty);
            set => SetValue(MinimizeCommandProperty, value);
        }

        public object MinimizeCommandParameter {
            get => GetValue(MinimizeCommandParameterProperty);
            set => SetValue(MinimizeCommandParameterProperty, value);
        }

        public ICommand MaximizeCommand {
            get => (ICommand) GetValue(MaximizeCommandProperty);
            set => SetValue(MaximizeCommandProperty, value);
        }

        public object MaximizeCommandParameter {
            get => GetValue(MaximizeCommandParameterProperty);
            set => SetValue(MaximizeCommandParameterProperty, value);
        }

        public ImageSource ImageSource {
            get => (ImageSource) GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public double ImageSize {
            get => (double) GetValue(ImageSizeProperty);
            set => SetValue(ImageSizeProperty, value);
        }

        public Visibility ContextMenuVisibility {
            get => (Visibility) GetValue(ContextMenuVisibilityProperty);
            set => SetValue(ContextMenuVisibilityProperty, value);
        }

        public Visibility HelpMenuItemVisibility {
            get => (Visibility) GetValue(HelpMenuItemVisibilityProperty);
            set => SetValue(HelpMenuItemVisibilityProperty, value);
        }

        public string HelpShowText {
            get => (string) GetValue(HelpShowTextProperty);
            set => SetValue(HelpShowTextProperty, value);
        }

        public string HelpAboutText {
            get => (string) GetValue(HelpAboutTextProperty);
            set => SetValue(HelpAboutTextProperty, value);
        }

        public ICommand ShowHelpCommand {
            get => (ICommand) GetValue(ShowHelpCommandProperty);
            set => SetValue(ShowHelpCommandProperty, value);
        }

        public ICommand AboutCommand {
            get => (ICommand) GetValue(AboutCommandProperty);
            set => SetValue(AboutCommandProperty, value);
        }

        public object HowHelpCommandParameter {
            get => GetValue(HowHelpCommandParameterProperty);
            set => SetValue(HowHelpCommandParameterProperty, value);
        }

        public object HelpAboutCommandParameter {
            get => GetValue(HelpAboutCommandParameterProperty);
            set => SetValue(HelpAboutCommandParameterProperty, value);
        }

        public Brush ButtonMouseOverBackground {
            get => (Brush) GetValue(ButtonMouseOverBackgroundProperty);
            set => SetValue(ButtonMouseOverBackgroundProperty, value);
        }

        public CornerRadius CornerRadius {
            get => (CornerRadius) GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public double ContentOffset {
            get => (double) GetValue(ContentOffsetProperty);
            set => SetValue(ContentOffsetProperty, value);
        }

        public Visibility ImageVisibility {
            get => (Visibility) GetValue(ImageVisibilityProperty);
            set => SetValue(ImageVisibilityProperty, value);
        }

        public Menu ApplicationMenu {
            get => (Menu) GetValue(ApplicationMenuProperty);
            set => SetValue(ApplicationMenuProperty, value);
        }

        public double ApplicationMenuOffset {
            get => (double) GetValue(ApplicationMenuOffsetProperty);
            set => SetValue(ApplicationMenuOffsetProperty, value);
        }

        private void _TitleBorder_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            switch (e.ClickCount) {
                case 2:
                    MaximizeCommand?.Execute(MaximizeCommandParameter);
                    e.Handled = true;
                    break;
                case 1:
                    var window = Window.GetWindow(this);
                    window?.DragMove();
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }

        private static void OnTitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (string) e.NewValue;
            obj.TitleBarText.Content = val;
        }

        private static object CoerceTitleProperty(DependencyObject d, object value) {
            var val = (string) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnBackgroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (Brush) e.NewValue;
            //val.Freeze();
            obj.Background = Brushes.Transparent;
            obj.TitleBorder.Background = val;
        }

        private static object CoerceBackgroundProperty(DependencyObject d, object value) {
            var val = (Brush) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnForegroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (Brush) e.NewValue;
            obj.TitleBarText.Foreground = val;
        }

        private static object CoerceForegroundProperty(DependencyObject d, object value) {
            var val = (Brush) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnButtonForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (Brush) e.NewValue;
            obj.Resources["ButtonForeground"] = val;
            //obj.MinimizeButton.Foreground = val;
            //obj.MaximizeButton.Foreground = val;
            //obj.CloseButton.Foreground = val;
        }

        private static object CoerceButtonForeground(DependencyObject d, object value) {
            var val = (Brush) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnMinimizeVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (Visibility) e.NewValue;
            obj.MinimizeButton.Visibility = val;
            obj.MinimizeMenuItem.Visibility = val;
        }

        private static object CoerceMinimizeVisibility(DependencyObject d, object value) {
            var val = (Visibility) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnMaximizeVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (Visibility) e.NewValue;
            obj.MaximizeButton.Visibility = val;
            obj.MaximizeMenuItem.Visibility = val;
        }

        private static object CoerceMaximizeVisibility(DependencyObject d, object value) {
            var val = (Visibility) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnCloseVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (Visibility) e.NewValue;
            obj.CloseButton.Visibility = val;
            obj.ExitMenuItem.Visibility = val;
        }

        private static object CoerceCloseVisibility(DependencyObject d, object value) {
            var val = (Visibility) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (Thickness) e.NewValue;
            obj.BorderThickness = new Thickness(0);
            obj.TitleBorder.BorderThickness = val;
        }

        private static object CoerceBorderThickness(DependencyObject d, object value) {
            var val = (Thickness) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (Brush) e.NewValue;
            obj.TitleBorder.BorderBrush = val;
        }

        private static object CoerceBorderBrush(DependencyObject d, object value) {
            var val = (Brush) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnFontFamilyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (FontFamily) e.NewValue;
            obj.TitleBarText.FontFamily = val;
        }

        private static object CoerceFontFamily(DependencyObject d, object value) {
            var val = (FontFamily) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (double) e.NewValue;
            obj.TitleBarText.FontSize = val;
            obj.MinimizeButton.FontSize = val;
            obj.MaximizeButton.FontSize = val;
            obj.CloseButton.FontSize = val;
        }

        private static object CoerceFontSize(DependencyObject d, object value) {
            var val = (double) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnFontWeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (FontWeight) e.NewValue;
            obj.TitleBarText.FontWeight = val;
        }

        private static object CoerceFontWeight(DependencyObject d, object value) {
            var val = (FontWeight) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnFontStretchChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (FontStretch) e.NewValue;
            obj.TitleBarText.FontStretch = val;
        }

        private static object CoerceFontStretch(DependencyObject d, object value) {
            var val = (FontStretch) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnFontStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (FontStyle) e.NewValue;
            obj.TitleBarText.FontStyle = val;
        }

        private static object CoerceFontStyle(DependencyObject d, object value) {
            var val = (FontStyle) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnEffectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (Effect) e.NewValue;
            obj.TitleBarText.Effect = val;
            obj.MinimizeButton.Effect = val;
            obj.MaximizeButton.Effect = val;
            obj.CloseButton.Effect = val;
            obj.IconImage.Effect = val;
        }

        private static object CoerceEffect(DependencyObject d, object value) {
            var val = (Effect) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnCloseCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (ICommand) e.NewValue;
            obj.CloseButton.Command = val;
            obj.ExitMenuItem.Command = val;
        }

        private static object CoerceCloseCommand(DependencyObject d, object value) {
            var val = (ICommand) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnCloseCommandParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = e.NewValue;
            obj.CloseButton.CommandParameter = val;
            obj.ExitMenuItem.CommandParameter = val;
        }

        private static object CoerceCloseCommandParameter(DependencyObject d, object value) {
            var val = value;
            //coerce value here if necessary
            return val;
        }

        private static void OnMinimizeCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (ICommand) e.NewValue;
            obj.MinimizeButton.Command = val;
            obj.MinimizeMenuItem.Command = val;
        }

        private static object CoerceMinimizeCommand(DependencyObject d, object value) {
            var val = (ICommand) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnMinimizeCommandParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = e.NewValue;
            obj.MinimizeButton.CommandParameter = val;
            obj.MinimizeMenuItem.CommandParameter = val;
        }

        private static object CoerceMinimizeCommandParameter(DependencyObject d, object value) {
            var val = value;
            //coerce value here if necessary
            return val;
        }

        private static void OnMaximizeCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (ICommand) e.NewValue;
            obj.MaximizeButton.Command = val;
            obj.MaximizeMenuItem.Command = val;
        }

        private static object CoerceMaximizeCommand(DependencyObject d, object value) {
            var val = (ICommand) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnMaximizeCommandParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = e.NewValue;
            obj.MaximizeButton.CommandParameter = val;
            obj.MaximizeMenuItem.CommandParameter = val;
        }

        private static object CoerceMaximizeCommandParameter(DependencyObject d, object value) {
            var val = value;
            //coerce value here if necessary
            return val;
        }

        private static void OnImageSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (ImageSource) e.NewValue;
            obj.IconImage.Source = val;
            obj.TitleBarText.SetValue(Grid.ColumnProperty, val == null ? 0 : 1);
        }

        private static object CoerceImageSource(DependencyObject d, object value) {
            var val = (ImageSource) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnImageSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (double) e.NewValue;
            obj.IconImage.Width = val;
            obj.IconImage.Height = val;
        }

        private static object CoerceImageSize(DependencyObject d, object value) {
            var val = (double) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnContextMenuVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (Visibility) e.NewValue;
            obj.TitlebarContextMenu.Visibility = val;
        }

        private static object CoerceContextMenuVisibility(DependencyObject d, object value) {
            var val = (Visibility) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnHelpMenuItemVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (Visibility) e.NewValue;
            obj.HelpMenuItem.Visibility = val;
            obj.FirstSeperator.Visibility = val;
        }

        private static object CoerceHelpMenuItemVisibility(DependencyObject d, object value) {
            var val = (Visibility) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnHelpShowTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (string) e.NewValue;
            obj.HelpShowMenuItem.Header = val;
        }

        private static object CoerceHelpShowText(DependencyObject d, object value) {
            var val = (string) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnHelpAboutTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (string) e.NewValue;
            obj.HelpAboutMenuItem.Header = val;
        }

        private static object CoerceHelpAboutText(DependencyObject d, object value) {
            var val = (string) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnShowHelpCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (ICommand) e.NewValue;
            obj.HelpShowMenuItem.Command = val;
        }

        private static object CoerceShowHelpCommand(DependencyObject d, object value) {
            var val = (ICommand) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnAboutCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (ICommand) e.NewValue;
            obj.HelpAboutMenuItem.Command = val;
        }

        private static object CoerceAboutCommand(DependencyObject d, object value) {
            var val = (ICommand) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnHowHelpCommandParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = e.NewValue;
            obj.HelpShowMenuItem.CommandParameter = val;
        }

        private static object CoerceHowHelpCommandParameter(DependencyObject d, object value) {
            var val = value;
            //coerce value here if necessary
            return val;
        }

        private static void OnHelpAboutCommandParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = e.NewValue;
            obj.HelpAboutMenuItem.CommandParameter = val;
        }

        private static object CoerceHelpAboutCommandParameter(DependencyObject d, object value) {
            var val = value;
            //coerce value here if necessary
            return val;
        }

        private static void OnButtonMouseOverBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (Brush) e.NewValue;
            var resources = obj.Resources;
            resources["MouseOverBackground"] = val;
        }

        private static object CoerceButtonMouseOverBackground(DependencyObject d, object value) {
            var val = (Brush) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (CornerRadius) e.NewValue;
            obj.TitleBorder.CornerRadius = val;
        }

        private static object CoerceCornerRadius(DependencyObject d, object value) {
            var val = (CornerRadius) value;
            //coerce value here if necessary
            return val;
        }

        private void TitleBar_OnLoaded(object sender, RoutedEventArgs e) {
            IconBorder.Margin = new Thickness(ContentOffset, 8, 2, 0);
            TitleBarText.Margin = new Thickness(0);
        }

        private static void OnContentOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (double) e.NewValue;
            obj.IconBorder.Margin = new Thickness(val, 8, 2, 0);
        }

        private static object CoerceContentOffset(DependencyObject d, object value) {
            var val = (double) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnImageVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (Visibility) e.NewValue;
            obj.IconBorder.Visibility = val;
        }

        private static object CoerceImageVisibility(DependencyObject d, object value) {
            var val = (Visibility) value;
            //coerce value here if necessary
            return val;
        }

        public void AddContextMenuItem(MenuItem item) {
            if (item == null) return;
            TitlebarContextMenu.Items.Insert(_lastIndex, item);
            _lastIndex++;
            if (_hasPersonalSeparator) return;
            TitlebarContextMenu.Items.Insert(1, new Separator());
            _hasPersonalSeparator = true;
        }

        public void AddContextMenuItem(object header) {
            AddContextMenuItem(header, null);
        }

        public void AddContextMenuItem(object header, ImageSource image) {
            AddContextMenuItem(header, image, null);
        }

        public void AddContextMenuItem(object header, ImageSource image, ICommand command) {
            AddContextMenuItem(header, image, command, null);
        }

        public void AddContextMenuItem(object header, ImageSource image, ICommand command, object commandParameter) {
            var item = new MenuItem {
                Header = header,
                Command = command,
                CommandParameter = commandParameter,
                Icon = new Image {
                    Source = image,
                    Width = image.Width,
                    Height = image.Height
                }
            };
            AddContextMenuItem(item);
        }

        private static void OnApplicationMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (Menu) e.NewValue;
            if (obj.ApplicationMenu != null) obj.OuterGrid.Children.Remove(obj.ApplicationMenu);
            if (val == null) return;
            val.Margin = new Thickness(obj.ApplicationMenuOffset, 0, 0, 0);
            val.HorizontalAlignment = HorizontalAlignment.Left;
            val.SetValue(Grid.ColumnProperty, 2);
            obj.OuterGrid.Children.Add(val);
        }

        private static object CoerceApplicationMenu(DependencyObject d, object value) {
            var val = (Menu) value;
            //coerce value here if necessary
            return val;
        }

        private static void OnApplicationMenuOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (TitleBar) d;
            var val = (double) e.NewValue;
            if (obj.ApplicationMenu != null)
                obj.ApplicationMenu.Margin = new Thickness(val, 0, 0, 0);
        }

        private static object CoerceApplicationMenuOffset(DependencyObject d, object value) {
            var val = (double) value;
            //coerce value here if necessary
            return val;
        }
    }
}