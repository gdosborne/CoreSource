using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GregOsborne.Application.Windows.Media {
    public class Font {
        public FontFamily FontFamily { get; set; }

        public FontStyle FontStyle { get; set; }

        public FontWeight FontWeight { get; set; }
    }

    public class FontMetrics {
        private const int EmSize = 2048;

        private readonly double[][] _charWidths = new double[256][];

        private readonly double _height;

        private readonly TextBlock _txtblk;

        public FontMetrics(Font font) {
            Font = font;
            _txtblk = new TextBlock {
                FontFamily = Font.FontFamily,
                FontStyle = Font.FontStyle,
                FontWeight = Font.FontWeight,
                FontSize = EmSize,
                Text = " "
            };
            _height = _txtblk.ActualHeight / EmSize;
        }

        public Font Font { protected set; get; }

        public double this[char ch] {
            get {
                var upper = ch >> 8;
                var lower = ch & 0xFF;
                if (_charWidths[upper] == null) {
                    _charWidths[upper] = new double[256];
                    for (var i = 0; i < 256; i++)
                        _charWidths[upper][i] = -1;
                }
                if (Math.Abs(_charWidths[upper][lower] - -1) > 0.0) return _charWidths[upper][lower];
                _txtblk.Text = ch.ToString();
                _charWidths[upper][lower] = _txtblk.ActualWidth / EmSize;
                return _charWidths[upper][lower];
            }
        }

        public Size MeasureText(string text) {
            var accumWidth = text.Sum(ch => this[ch]);
            return new Size(accumWidth, _height);
        }

        public Size MeasureText(string text, int startIndex, int length) {
            double accumWidth = 0;
            for (var index = startIndex; index < startIndex + length; index++)
                accumWidth += this[text[index]];
            return new Size(accumWidth, _height);
        }
    }
}