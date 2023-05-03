// <copyright file="Titlebar.xaml.cs" company="">
// Copyright (c) 2019 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>12/3/2019</date>

namespace GregOsborne.Controls {
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public partial class Titlebar : UserControl {
        public static new readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(Titlebar), new FrameworkPropertyMetadata(Brushes.Black, OnBackgroundPropertyChanged));
        public static new readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(Titlebar), new FrameworkPropertyMetadata(Brushes.Black, OnBorderBrushPropertyChanged));
        public static readonly DependencyProperty CloseCommandProperty = DependencyProperty.Register("CloseCommand", typeof(ICommand), typeof(Titlebar), new FrameworkPropertyMetadata(default(ICommand), OnCloseCommandPropertyChanged));
        public static readonly DependencyProperty CloseVisibilityProperty = DependencyProperty.Register("CloseVisibility", typeof(Visibility), typeof(Titlebar), new FrameworkPropertyMetadata(Visibility.Visible, OnCloseVisibilityPropertyChanged));
        public static readonly DependencyProperty ControlAreaBackgroundProperty = DependencyProperty.Register("ControlAreaBackground", typeof(Brush), typeof(Titlebar), new FrameworkPropertyMetadata(Brushes.White, OnControlAreaBackgroundPropertyChanged));
        public static readonly DependencyProperty ControlAreaVisibilityProperty = DependencyProperty.Register("ControlAreaVisibility", typeof(Visibility), typeof(Titlebar), new FrameworkPropertyMetadata(default(Visibility), OnControlAreaVisibilityPropertyChanged));
        public static readonly DependencyProperty ControlsForegroundProperty = DependencyProperty.Register("ControlsForeground", typeof(Brush), typeof(Titlebar), new FrameworkPropertyMetadata(Brushes.Black, OnControlsForegroundPropertyChanged));
        public static readonly DependencyProperty ControlsMouseOverBrushProperty = DependencyProperty.Register("ControlsMouseOverBrush", typeof(Brush), typeof(Titlebar), new FrameworkPropertyMetadata(Brushes.Black, OnControlsMouseOverBrushPropertyChanged));
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(Titlebar), new FrameworkPropertyMetadata(new CornerRadius(8, 8, 0, 0), OnCornerRadiusPropertyChanged));
        public static new readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(Titlebar), new FrameworkPropertyMetadata(Brushes.White, OnForegroundPropertyChanged));
        public static readonly DependencyProperty GradientVisibilityProperty = DependencyProperty.Register("GradientVisibility", typeof(Visibility), typeof(Titlebar), new FrameworkPropertyMetadata(default(Visibility), OnGradientVisibilityPropertyChanged));
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(ImageSource), typeof(Titlebar), new FrameworkPropertyMetadata(default(ImageSource), OnIconPropertyChanged));
        public static readonly DependencyProperty IconVisibilityProperty = DependencyProperty.Register("IconVisibility", typeof(Visibility), typeof(Titlebar), new FrameworkPropertyMetadata(Visibility.Visible, OnIconVisibilityPropertyChanged));
        public static readonly DependencyProperty IsCloseEnabledProperty = DependencyProperty.Register("IsCloseEnabled", typeof(bool), typeof(Titlebar), new FrameworkPropertyMetadata(true, OnIsCloseEnabledPropertyChanged));
        public static readonly DependencyProperty IsDockTitlebarProperty = DependencyProperty.Register("IsDockTitlebar", typeof(bool), typeof(Titlebar), new FrameworkPropertyMetadata(false, OnIsDockTitlebarPropertyChanged));
        public static readonly DependencyProperty IsMaximizeRestoreEnabledProperty = DependencyProperty.Register("IsMaximizeRestoreEnabled", typeof(bool), typeof(Titlebar), new FrameworkPropertyMetadata(true, OnIsMaximizeRestoreEnabledPropertyChanged));
        public static readonly DependencyProperty IsMinimizeEnabledProperty = DependencyProperty.Register("IsMinimizeEnabled", typeof(bool), typeof(Titlebar), new FrameworkPropertyMetadata(true, OnIsMinimizeEnabledPropertyChanged));
        public static readonly DependencyProperty MaximizeRestoreCommandProperty = DependencyProperty.Register("MaximizeRestoreCommand", typeof(ICommand), typeof(Titlebar), new FrameworkPropertyMetadata(default(ICommand), OnMaximizeRestoreCommandPropertyChanged));
        public static readonly DependencyProperty MaximizeRestoreVisibilityProperty = DependencyProperty.Register("MaximizeRestoreVisibility", typeof(Visibility), typeof(Titlebar), new FrameworkPropertyMetadata(Visibility.Visible, OnMaximizeRestoreVisibilityPropertyChanged));
        public static readonly DependencyProperty MinimizeCommandProperty = DependencyProperty.Register("MinimizeCommand", typeof(ICommand), typeof(Titlebar), new FrameworkPropertyMetadata(default(ICommand), OnMinimizeCommandPropertyChanged));
        public static readonly DependencyProperty MinimizeVisibilityProperty = DependencyProperty.Register("MinimizeVisibility", typeof(Visibility), typeof(Titlebar), new FrameworkPropertyMetadata(Visibility.Visible, OnMinimizeVisibilityPropertyChanged));
        public static readonly DependencyProperty WindowTitleProperty = DependencyProperty.Register("WindowTitle", typeof(string), typeof(Titlebar), new FrameworkPropertyMetadata("Title", OnWindowTitlePropertyChanged));
        private Brush disabledBrush = SystemColors.ControlLightBrush;
        private Brush savedBackgroundBrush = null;
        public Titlebar() {
            this.InitializeComponent();
        }

        public event EventHandler CloseClicked;
        public event EventHandler MaximizeRestoreClicked;
        public event EventHandler MinimizeClicked;
        public new Brush Background {
            get => (Brush)this.GetValue(BackgroundProperty); set => this.SetValue(BackgroundProperty, value);
        }

        public new Brush BorderBrush {
            get => (Brush)this.GetValue(BorderBrushProperty); set => this.SetValue(BorderBrushProperty, value);
        }

        public ICommand CloseCommand {
            get => (ICommand)this.GetValue(CloseCommandProperty); set => this.SetValue(CloseCommandProperty, value);
        }

        public Visibility CloseVisibility {
            get => (Visibility)this.GetValue(CloseVisibilityProperty); set => this.SetValue(CloseVisibilityProperty, value);
        }

        public Brush ControlAreaBackground {
            get => (Brush)this.GetValue(ControlAreaBackgroundProperty); set => this.SetValue(ControlAreaBackgroundProperty, value);
        }

        public Visibility ControlAreaVisibility {
            get => (Visibility)this.GetValue(ControlAreaVisibilityProperty); set => this.SetValue(ControlAreaVisibilityProperty, value);
        }

        public Brush ControlsForeground {
            get => (Brush)this.GetValue(ControlsForegroundProperty); set => this.SetValue(ControlsForegroundProperty, value);
        }

        public Brush ControlsMouseOverBrush {
            get => (Brush)this.GetValue(ControlsMouseOverBrushProperty); set => this.SetValue(ControlsMouseOverBrushProperty, value);
        }

        public CornerRadius CornerRadius {
            get => (CornerRadius)this.GetValue(CornerRadiusProperty); set => this.SetValue(CornerRadiusProperty, value);
        }

        public new Brush Foreground {
            get => (Brush)this.GetValue(ForegroundProperty); set => this.SetValue(ForegroundProperty, value);
        }

        public Visibility GradientVisibility {
            get => (Visibility)this.GetValue(GradientVisibilityProperty); set => this.SetValue(GradientVisibilityProperty, value);
        }

        public ImageSource Icon {
            get => (ImageSource)this.GetValue(IconProperty); set => this.SetValue(IconProperty, value);
        }

        public Visibility IconVisibility {
            get => (Visibility)this.GetValue(IconVisibilityProperty); set => this.SetValue(IconVisibilityProperty, value);
        }

        public bool IsCloseEnabled {
            get => (bool)this.GetValue(IsCloseEnabledProperty); set => this.SetValue(IsCloseEnabledProperty, value);
        }

        public bool IsDockTitlebar {
            get => (bool)this.GetValue(IsDockTitlebarProperty); set => this.SetValue(IsDockTitlebarProperty, value);
        }

        public bool IsMaximizeRestoreEnabled {
            get => (bool)this.GetValue(IsMaximizeRestoreEnabledProperty); set => this.SetValue(IsMaximizeRestoreEnabledProperty, value);
        }

        public bool IsMinimizeEnabled {
            get => (bool)this.GetValue(IsMinimizeEnabledProperty); set => this.SetValue(IsMinimizeEnabledProperty, value);
        }

        public ICommand MaximizeRestoreCommand {
            get => (ICommand)this.GetValue(MaximizeRestoreCommandProperty); set => this.SetValue(MaximizeRestoreCommandProperty, value);
        }

        public Visibility MaximizeRestoreVisibility {
            get => (Visibility)this.GetValue(MaximizeRestoreVisibilityProperty); set => this.SetValue(MaximizeRestoreVisibilityProperty, value);
        }

        public ICommand MinimizeCommand {
            get => (ICommand)this.GetValue(MinimizeCommandProperty); set => this.SetValue(MinimizeCommandProperty, value);
        }

        public Visibility MinimizeVisibility {
            get => (Visibility)this.GetValue(MinimizeVisibilityProperty); set => this.SetValue(MinimizeVisibilityProperty, value);
        }

        public string WindowTitle {
            get => (string)this.GetValue(WindowTitleProperty); set => this.SetValue(WindowTitleProperty, value);
        }

        private static void OnBackgroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (Brush)e.NewValue;
            obj.outerBorder.Background = val;
        }

        private static void OnBorderBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (Brush)e.NewValue;
            obj.controlsBorder.BorderBrush = val;
            obj.outerBorder.BorderBrush = val;
        }

        private static void OnCloseCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (ICommand)e.NewValue;
            obj.closeHyperlink.Command = val;
        }

        private static void OnCloseVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (Visibility)e.NewValue;
            obj.closeTextBlock.Visibility = val;
        }

        private static void OnControlAreaBackgroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (Brush)e.NewValue;
            if (obj.savedBackgroundBrush == null) {
                obj.savedBackgroundBrush = val;
            }

            obj.controlsBorder.Background = val;
        }

        private static void OnControlAreaVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (Visibility)e.NewValue;
            obj.controlsBorder.Visibility = val;
        }

        private static void OnControlsForegroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (Brush)e.NewValue;
            obj.minimizeTextBlock.Foreground = val;
            obj.maximizeRestoreTextBlock.Foreground = val;
            obj.closeTextBlock.Foreground = val;
            obj.closeHyperlink.Foreground = val;
            obj.maximizeRestoreHyperlink.Foreground = val;
            obj.minimizeHyperlink.Foreground = val;
        }

        private static void OnControlsMouseOverBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (Brush)e.NewValue;
        }

        private static void OnCornerRadiusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (CornerRadius)e.NewValue;
            obj.outerBorder.CornerRadius = val;
            obj.gradientBorder.CornerRadius = val;
            if (val.TopRight > 0) {
                obj.controlsBorder.CornerRadius = new CornerRadius(0, val.TopRight * .75, 0, 0);
            } else {
                obj.controlsBorder.CornerRadius = new CornerRadius(0, 0, 0, 0);
            }
        }

        private static void OnForegroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (Brush)e.NewValue;
            obj.titleTextBlock.Foreground = val;
        }

        private static void OnGradientVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (Visibility)e.NewValue;
            obj.gradientBorder.Visibility = val;
        }

        private static void OnIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (ImageSource)e.NewValue;
            obj.iconImage.Source = val;
        }

        private static void OnIconVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (Visibility)e.NewValue;
            obj.iconImage.Visibility = val;
        }

        private static void OnIsCloseEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (bool)e.NewValue;
            obj.closeHyperlink.IsEnabled = val;
            obj.closeTextBlock.Opacity = val ? 1.0 : 0.5;
        }

        private static void OnIsDockTitlebarPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (bool)e.NewValue;
            obj.outerBorder.CornerRadius = new CornerRadius(8, (val ? 0 : 8), 0, 0);
            obj.gradientBorder.CornerRadius = new CornerRadius(8, (val ? 0 : 8), 0, 0);
        }

        private static void OnIsMaximizeRestoreEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (bool)e.NewValue;
            obj.maximizeRestoreHyperlink.IsEnabled = val;
            obj.maximizeRestoreTextBlock.Opacity = val ? 1.0 : 0.5;
        }

        private static void OnIsMinimizeEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (bool)e.NewValue;
            obj.minimizeHyperlink.IsEnabled = val;
            obj.minimizeTextBlock.Opacity = val ? 1.0 : 0.5;
        }

        private static void OnMaximizeRestoreCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (ICommand)e.NewValue;
            obj.maximizeRestoreHyperlink.Command = val;
        }

        private static void OnMaximizeRestoreVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (Visibility)e.NewValue;
            obj.maximizeRestoreTextBlock.Visibility = val;
        }

        private static void OnMinimizeCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (ICommand)e.NewValue;
            obj.minimizeHyperlink.Command = val;
        }

        private static void OnMinimizeVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (Visibility)e.NewValue;
            obj.minimizeTextBlock.Visibility = val;
        }

        private static void OnWindowTitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (string)e.NewValue;
            obj.titleTextBlock.Text = val;
        }

        private void Border_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            if (this.IsDragEnabled && e.LeftButton == MouseButtonState.Pressed) {
                Window.GetWindow(this).DragMove();
            }
        }

        private void closePreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) => CloseClicked?.Invoke(this, EventArgs.Empty);

        private void maximizePreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) => MaximizeRestoreClicked?.Invoke(this, EventArgs.Empty);

        private void minimizePreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) => MinimizeClicked?.Invoke(this, EventArgs.Empty);

        private void UserControl_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e) => this.controlsBorder.Background = !(bool)e.NewValue ? this.disabledBrush : this.savedBackgroundBrush;

        public static readonly DependencyProperty IsDragEnabledProperty = DependencyProperty.Register("IsDragEnabled", typeof(bool), typeof(Titlebar), new FrameworkPropertyMetadata(true, OnIsDragEnabledPropertyChanged));
        public bool IsDragEnabled {
            get => (bool)this.GetValue(IsDragEnabledProperty);
            set => this.SetValue(IsDragEnabledProperty, value);
        }
        private static void OnIsDragEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Titlebar)d;
            var val = (bool)e.NewValue;
            //modify the value here, i.e., obj.ctrl.Value = val
        }

    }
}
