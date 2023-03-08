using Common.Application.Media;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using media = System.Windows.Media;

namespace Controls.Core {
    public partial class MultiFontIcon : UserControl {
        public MultiFontIcon() {
            InitializeComponent();
        }

        #region PrimaryGlyphProperty
        /// <summary>Gets the PrimaryGlyph dependency property.</summary>
        /// <value>The PrimaryGlyph dependency property.</value>
        public static readonly DependencyProperty PrimaryGlyphProperty = DependencyProperty.Register("PrimaryGlyph", typeof(string), typeof(MultiFontIcon), new PropertyMetadata("", OnPrimaryGlyphPropertyChanged));
        /// <summary>Gets/sets the PrimaryGlyph.</summary>
        /// <value>The PrimaryGlyph.</value>
        public string PrimaryGlyph {
            get => (string)GetValue(PrimaryGlyphProperty);
            set => SetValue(PrimaryGlyphProperty, value);
        }
        private static void OnPrimaryGlyphPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (MultiFontIcon)d;
            var val = (string)e.NewValue;
            obj.MainIcon.Glyph = val;
        }
        #endregion

        #region SecondaryGlyphProperty
        /// <summary>Gets the SecondaryGlyph dependency property.</summary>
        /// <value>The SecondaryGlyph dependency property.</value>
        public static readonly DependencyProperty SecondaryGlyphProperty = DependencyProperty.Register("SecondaryGlyph", typeof(string), typeof(MultiFontIcon), new PropertyMetadata("", OnSecondaryGlyphPropertyChanged));
        /// <summary>Gets/sets the SecondaryGlyph.</summary>
        /// <value>The SecondaryGlyph.</value>
        public string SecondaryGlyph {
            get => (string)GetValue(SecondaryGlyphProperty);
            set => SetValue(SecondaryGlyphProperty, value);
        }
        private static void OnSecondaryGlyphPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (MultiFontIcon)d;
            var val = (string)e.NewValue;
            obj.SecondaryIcon.Glyph = val;
        }
        #endregion

        #region FontSizeProperty
        /// <summary>Gets the FontSize dependency property.</summary>
        /// <value>The FontSize dependency property.</value>
        public static new readonly DependencyProperty FontSizeProperty = DependencyProperty.Register("FontSize", typeof(double), typeof(MultiFontIcon), new PropertyMetadata(160.0, OnFontSizePropertyChanged));
        /// <summary>Gets/sets the FontSize.</summary>
        /// <value>The FontSize.</value>
        public new double FontSize {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }
        private static void OnFontSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (MultiFontIcon)d;
            var val = (double)e.NewValue;
            obj.MainIcon.FontSize = val;
        }
        #endregion

        #region SecondaryFontSizeProperty
        /// <summary>Gets the SecondaryFontSize dependency property.</summary>
        /// <value>The SecondaryFontSize dependency property.</value>
        public static readonly DependencyProperty SecondaryFontSizeProperty = DependencyProperty.Register("SecondaryFontSize", typeof(double), typeof(MultiFontIcon), new PropertyMetadata(80.0, OnSecondaryFontSizePropertyChanged));
        /// <summary>Gets/sets the SecondaryFontSize.</summary>
        /// <value>The SecondaryFontSize.</value>
        public double SecondaryFontSize {
            get => (double)GetValue(SecondaryFontSizeProperty);
            set => SetValue(SecondaryFontSizeProperty, value);
        }
        private static void OnSecondaryFontSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (MultiFontIcon)d;
            var val = (double)e.NewValue;
            obj.SecondaryIcon.FontSize = val;
        }
        #endregion

        #region ForegroundProperty
        /// <summary>Gets the Foreground dependency property.</summary>
        /// <value>The Foreground dependency property.</value>
        public static new readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(media.Brush), typeof(MultiFontIcon), new PropertyMetadata(default(media.Brush), OnForegroundPropertyChanged));
        /// <summary>Gets/sets the Foreground.</summary>
        /// <value>The Foreground.</value>
        public new media.Brush Foreground {
            get => (media.Brush)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }
        private static void OnForegroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (MultiFontIcon)d;
            var val = (media.Brush)e.NewValue;
            obj.MainIcon.Foreground = val;
        }
        #endregion

        #region BackgroundProperty
        /// <summary>Gets the Background dependency property.</summary>
        /// <value>The Background dependency property.</value>
        public static new readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(media.Brush), typeof(MultiFontIcon), new PropertyMetadata(default(media.Brush), OnBackgroundPropertyChanged));
        /// <summary>Gets/sets the Background.</summary>
        /// <value>The Background.</value>
        public new media.Brush Background {
            get => (media.Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }
        private static void OnBackgroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (MultiFontIcon)d;
            var val = (media.Brush)e.NewValue;
            obj.Background = val;
            obj.Blackout.Background = val;
        }
        #endregion

        #region SecondaryForegroundProperty
        /// <summary>Gets the SecondaryForeground dependency property.</summary>
        /// <value>The SecondaryForeground dependency property.</value>
        public static readonly DependencyProperty SecondaryForegroundProperty = DependencyProperty.Register("SecondaryForeground", typeof(media.Brush), typeof(MultiFontIcon), new PropertyMetadata(default(media.Brush), OnSecondaryForegroundPropertyChanged));
        /// <summary>Gets/sets the SecondaryForeground.</summary>
        /// <value>The SecondaryForeground.</value>
        public media.Brush SecondaryForeground {
            get => (media.Brush)GetValue(SecondaryForegroundProperty);
            set => SetValue(SecondaryForegroundProperty, value);
        }
        private static void OnSecondaryForegroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (MultiFontIcon)d;
            var val = (media.Brush)e.NewValue;
            obj.SecondaryIcon.Foreground = val;
        }
        #endregion

        #region FontFamilyProperty
        /// <summary>Gets the FontFamily dependency property.</summary>
        /// <value>The FontFamily dependency property.</value>
        public static new readonly DependencyProperty FontFamilyProperty = DependencyProperty.Register("FontFamily", typeof(System.Windows.Media.FontFamily), typeof(MultiFontIcon), new PropertyMetadata(new System.Windows.Media.FontFamily("Segoe Fluent Icons"), OnFontFamilyPropertyChanged));
        /// <summary>Gets/sets the FontFamily.</summary>
        /// <value>The FontFamily.</value>
        public new System.Windows.Media.FontFamily FontFamily {
            get => (System.Windows.Media.FontFamily)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }
        private static void OnFontFamilyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (MultiFontIcon)d;
            var val = (System.Windows.Media.FontFamily)e.NewValue;
            obj.MainIcon.FontFamily = val;
        }
        #endregion

        #region SecondaryFontFamilyProperty
        /// <summary>Gets the SecondaryFontFamily dependency property.</summary>
        /// <value>The SecondaryFontFamily dependency property.</value>
        public static readonly DependencyProperty SecondaryFontFamilyProperty = DependencyProperty.Register("SecondaryFontFamily", typeof(System.Windows.Media.FontFamily), typeof(MultiFontIcon), new PropertyMetadata(new System.Windows.Media.FontFamily("Segoe Fluent Icons"), OnSecondaryFontFamilyPropertyChanged));
        /// <summary>Gets/sets the SecondaryFontFamily.</summary>
        /// <value>The SecondaryFontFamily.</value>
        public System.Windows.Media.FontFamily SecondaryFontFamily {
            get => (System.Windows.Media.FontFamily)GetValue(SecondaryFontFamilyProperty);
            set => SetValue(SecondaryFontFamilyProperty, value);
        }
        private static void OnSecondaryFontFamilyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (MultiFontIcon)d;
            var val = (System.Windows.Media.FontFamily)e.NewValue;
            obj.SecondaryIcon.FontFamily = val;
        }
        #endregion
    }
}
