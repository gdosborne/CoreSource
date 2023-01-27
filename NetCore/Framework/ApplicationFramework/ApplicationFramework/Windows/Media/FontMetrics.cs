using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Common.Applicationn.Windows.Media {
    public class Font {
        public FontFamily FontFamily { get; set; }

        public FontStyle FontStyle { get; set; }

        public FontWeight FontWeight { get; set; }
    }

    public class FontMetrics {
        private const int EmSize = 2048;

        private readonly double[][] charWidths = new double[256][];

        private readonly double height;

        private readonly TextBlock txtblk;

        public FontMetrics(Font font) {
            Font = font;
            txtblk = new TextBlock {
                FontFamily = Font.FontFamily,
                FontStyle = Font.FontStyle,
                FontWeight = Font.FontWeight,
                FontSize = EmSize,
                Text = " "
            };
            height = txtblk.ActualHeight / EmSize;
        }

        public Font Font { protected set; get; }

        public double this[char ch] {
            get {
                var upper = ch >> 8;
                var lower = ch & 0xFF;
                if (charWidths[upper] == null) {
                    charWidths[upper] = new double[256];
                    for (var i = 0; i < 256; i++) {
                        charWidths[upper][i] = -1;
                    }
                }
                if (Math.Abs(charWidths[upper][lower] - -1) > 0.0) {
                    return charWidths[upper][lower];
                }

                txtblk.Text = ch.ToString();
                charWidths[upper][lower] = txtblk.ActualWidth / EmSize;
                return charWidths[upper][lower];
            }
        }

        public Size MeasureText(string text) {
            var accumWidth = text.Sum(ch => this[ch]);
            return new Size(accumWidth, height);
        }

        public Size MeasureText(string text, int startIndex, int length) {
            double accumWidth = 0;
            for (var index = startIndex; index < startIndex + length; index++) {
                accumWidth += this[text[index]];
            }

            return new Size(accumWidth, height);
        }
    }
}