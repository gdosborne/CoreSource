using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OSControls
{
    public partial class Glyph : UserControl
    {
        public Glyph() => this.InitializeComponent();

        public static readonly DependencyProperty CharacterProperty = DependencyProperty.Register("Character", typeof(int), typeof(Glyph), new FrameworkPropertyMetadata(default(int), OnCharacterPropertyChanged));
        public int Character {
            get => (int)this.GetValue(CharacterProperty);
            set => this.SetValue(CharacterProperty, value);
        }
        private static void OnCharacterPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Glyph)d;
            var val = (int)e.NewValue;
            obj.theGlyph.Text = char.ConvertFromUtf32(val);
        }

        public static new readonly DependencyProperty FontFamilyProperty = DependencyProperty.Register("FontFamily", typeof(FontFamily), typeof(Glyph), new FrameworkPropertyMetadata(new FontFamily("Segoe MDL2 Assets"), OnFontFamilyPropertyChanged));
        public new FontFamily FontFamily {
            get => (FontFamily)this.GetValue(FontFamilyProperty);
            set => this.SetValue(FontFamilyProperty, value);
        }
        private static void OnFontFamilyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Glyph)d;
            var val = (FontFamily)e.NewValue;
            obj.theGlyph.FontFamily = val;
        }

        public static new readonly DependencyProperty FontSizeProperty = DependencyProperty.Register("FontSize", typeof(double), typeof(Glyph), new FrameworkPropertyMetadata(75.0, OnFontSizePropertyChanged));
        public new double FontSize {
            get => (double)this.GetValue(FontSizeProperty);
            set => this.SetValue(FontSizeProperty, value);
        }
        private static void OnFontSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Glyph)d;
            var val = (double)e.NewValue;
            obj.theGlyph.FontSize = val;
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(Glyph), new FrameworkPropertyMetadata(default(CornerRadius), OnCornerRadiusPropertyChanged));
        public CornerRadius CornerRadius {
            get => (CornerRadius)this.GetValue(CornerRadiusProperty);
            set => this.SetValue(CornerRadiusProperty, value);
        }
        private static void OnCornerRadiusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Glyph)d;
            var val = (CornerRadius)e.NewValue;

            obj.theBorder.CornerRadius = val;
            obj.theLeftSideBorder.CornerRadius = new CornerRadius(val.TopLeft, 0, 0, val.BottomLeft);
            obj.theTopSideBorder.CornerRadius = new CornerRadius(0, val.TopRight, 0, 0);
            obj.theRightSideBorder.CornerRadius = new CornerRadius(0, val.TopRight, val.BottomRight, 0);
            obj.theBottomSideBorder.CornerRadius = new CornerRadius(0, 0, val.BottomLeft, val.BottomLeft);
        }

        public static new readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(Glyph), new FrameworkPropertyMetadata(default(Thickness), OnBorderThicknessPropertyChanged));
        public new Thickness BorderThickness {
            get => (Thickness)this.GetValue(BorderThicknessProperty);
            set => this.SetValue(BorderThicknessProperty, value);
        }
        private static void OnBorderThicknessPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Glyph)d;
            var val = (Thickness)e.NewValue;
            obj.theBorder.BorderThickness = val;
        }

        public static new readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(Glyph), new FrameworkPropertyMetadata(SystemColors.ActiveBorderBrush, OnBorderBrushPropertyChanged));
        public new Brush BorderBrush {
            get => (Brush)this.GetValue(BorderBrushProperty);
            set => this.SetValue(BorderBrushProperty, value);
        }
        private static void OnBorderBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Glyph)d;
            var val = (Brush)e.NewValue;
            obj.theBorder.BorderBrush = val;
        }

        public static new readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(Glyph), new FrameworkPropertyMetadata(Brushes.Transparent, OnBackgroundPropertyChanged));
        public new Brush Background {
            get => (Brush)this.GetValue(BackgroundProperty);
            set => this.SetValue(BackgroundProperty, value);
        }
        private static void OnBackgroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Glyph)d;
            var val = (Brush)e.NewValue;
            obj.theBorder.Background = val;
        }

        public static readonly DependencyProperty HasDepthProperty = DependencyProperty.Register("HasDepth", typeof(bool), typeof(Glyph), new FrameworkPropertyMetadata(default(bool), OnHasDepthPropertyChanged));
        public bool HasDepth {
            get => (bool)this.GetValue(HasDepthProperty);
            set => this.SetValue(HasDepthProperty, value);
        }
        private static void OnHasDepthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Glyph)d;
            var val = (bool)e.NewValue;
            obj.theGlassGrid.Visibility = val ? Visibility.Visible : Visibility.Hidden;
        }

        public static readonly DependencyProperty BevelHeightProperty = DependencyProperty.Register("BevelHeight", typeof(double), typeof(Glyph), new FrameworkPropertyMetadata(7.5, OnBevelHeightPropertyChanged));
        public double BevelHeight {
            get => (double)this.GetValue(BevelHeightProperty);
            set => this.SetValue(BevelHeightProperty, value);
        }
        private static void OnBevelHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Glyph)d;
            var val = (double)e.NewValue;
            if (val < 0 || val > 50)
                throw new ArgumentOutOfRangeException("BevelHeight","Value must be betwwen 0 and 50");
            obj.row1.Height = new GridLength((val * 2) / 100, GridUnitType.Star);
            obj.row3.Height = new GridLength((val * 2) / 100, GridUnitType.Star);
            obj.col1.Width = new GridLength((val * 2) / 100, GridUnitType.Star);
            obj.col3.Width = new GridLength((val * 2) / 100, GridUnitType.Star);
        }

    }
}
