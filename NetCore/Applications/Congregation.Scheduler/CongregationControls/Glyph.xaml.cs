using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CongregationControls {
    public partial class Glyph : UserControl {
        public Glyph () {
            InitializeComponent();
        }

        #region FontFamily Dependency Property
        public static new readonly DependencyProperty FontFamilyProperty = DependencyProperty.Register("FontFamily", typeof(FontFamily), typeof(Glyph), new PropertyMetadata(default(FontFamily), new PropertyChangedCallback(OnFontFamilyChanged)));
        public new FontFamily FontFamily {
            get { return (FontFamily)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }
        private static void OnFontFamilyChanged (DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Glyph)d;
            var val = (FontFamily)e.NewValue;
            obj.GlyphTextBlock.FontFamily = val;
        }
        #endregion

        #region FontSize Dependency Property
        public static new readonly DependencyProperty FontSizeProperty = DependencyProperty.Register("FontSize", typeof(double), typeof(Glyph), new PropertyMetadata(default(double), new PropertyChangedCallback(OnFontSizeChanged)));
        public new double FontSize {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }
        private static void OnFontSizeChanged (DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Glyph)d;
            var val = (double)e.NewValue;
            obj.GlyphTextBlock.FontSize = val;
        }
        #endregion

        #region Character Dependency Property
        public static readonly DependencyProperty CharacterProperty = DependencyProperty.Register("Character", typeof(char), typeof(Glyph), new PropertyMetadata(default(char), new PropertyChangedCallback(OnCharacterChanged)));
        public char Character {
            get { return (char)GetValue(CharacterProperty); }
            set { SetValue(CharacterProperty, value); }
        }
        private static void OnCharacterChanged (DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (Glyph)d;
            var val = (char)e.NewValue;
            obj.GlyphTextBlock.Text = val.ToString();
        }
        #endregion

    }

}
