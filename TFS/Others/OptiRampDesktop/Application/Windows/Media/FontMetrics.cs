using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyApplication.Windows.Media
{
	public class Font
	{
		#region Public Properties
		public FontFamily FontFamily { get; set; }
		public FontStyle FontStyle { get; set; }
		public FontWeight FontWeight { get; set; }
		#endregion
	}

	public class FontMetrics
	{
		#region Private Fields
		private const int EmSize = 2048;
		private double[][] charWidths = new double[256][];
		private double height;
		private TextBlock txtblk;
		#endregion

		#region Public Constructors

		public FontMetrics(Font font)
		{
			this.Font = font;
			txtblk = new TextBlock
			{
				FontFamily = this.Font.FontFamily,
				FontStyle = this.Font.FontStyle,
				FontWeight = this.Font.FontWeight,
				FontSize = EmSize
			};
			txtblk.Text = " ";
			height = txtblk.ActualHeight / EmSize;
		}

		#endregion

		#region Public Properties
		public Font Font { protected set; get; }
		#endregion

		#region Public Indexers
		public double this[char ch]
		{
			get
			{
				int upper = (ushort)ch >> 8;
				int lower = (ushort)ch & 0xFF;
				if (charWidths[upper] == null)
				{
					charWidths[upper] = new double[256];
					for (int i = 0; i < 256; i++)
						charWidths[upper][i] = -1;
				}
				if (charWidths[upper][lower] == -1)
				{
					txtblk.Text = ch.ToString();
					charWidths[upper][lower] = txtblk.ActualWidth / EmSize;
				}
				return charWidths[upper][lower];
			}
		}
		#endregion

		#region Public Methods

		public Size MeasureText(string text)
		{
			double accumWidth = 0;
			foreach (char ch in text)
				accumWidth += this[ch];
			return new Size(accumWidth, height);
		}

		public Size MeasureText(string text, int startIndex, int length)
		{
			double accumWidth = 0;
			for (int index = startIndex; index < startIndex + length; index++)
				accumWidth += this[text[index]];
			return new Size(accumWidth, height);
		}

		#endregion
	}
}