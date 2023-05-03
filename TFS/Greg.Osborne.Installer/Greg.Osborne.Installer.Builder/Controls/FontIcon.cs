namespace Greg.Osborne.Installer.Builder.Controls {
	using System.Windows;
	using System.Windows.Controls;

	public sealed class FontIcon : TextBlock {

		public static readonly DependencyProperty GlyphProperty = DependencyProperty.Register("Glyph", typeof(string), typeof(FontIcon), new FrameworkPropertyMetadata(default(string), OnGlyphPropertyChanged));
		public string Glyph {
			get => (string)this.GetValue(GlyphProperty);
			set => this.SetValue(GlyphProperty, value);
		}

		private static void OnGlyphPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var obj = (FontIcon)d;
			var val = (string)e.NewValue;
			obj.Text = val;
		}
	}
}
