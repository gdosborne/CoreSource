namespace GregOsborne.Application.Windows.Media {
	using System;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;

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
			this.Font = font;
			this.txtblk = new TextBlock {
				FontFamily = this.Font.FontFamily,
				FontStyle = this.Font.FontStyle,
				FontWeight = this.Font.FontWeight,
				FontSize = EmSize,
				Text = " "
			};
			this.height = this.txtblk.ActualHeight / EmSize;
		}

		public Font Font { protected set; get; }

		public double this[char ch] {
			get {
				var upper = ch >> 8;
				var lower = ch & 0xFF;
				if (this.charWidths[upper] == null) {
					this.charWidths[upper] = new double[256];
					for (var i = 0; i < 256; i++) {
						this.charWidths[upper][i] = -1;
					}
				}
				if (Math.Abs(this.charWidths[upper][lower] - -1) > 0.0) {
					return this.charWidths[upper][lower];
				}

				this.txtblk.Text = ch.ToString();
				this.charWidths[upper][lower] = this.txtblk.ActualWidth / EmSize;
				return this.charWidths[upper][lower];
			}
		}

		public Size MeasureText(string text) {
			var accumWidth = text.Sum(ch => this[ch]);
			return new Size(accumWidth, this.height);
		}

		public Size MeasureText(string text, int startIndex, int length) {
			double accumWidth = 0;
			for (var index = startIndex; index < startIndex + length; index++) {
				accumWidth += this[text[index]];
			}

			return new Size(accumWidth, this.height);
		}
	}
}