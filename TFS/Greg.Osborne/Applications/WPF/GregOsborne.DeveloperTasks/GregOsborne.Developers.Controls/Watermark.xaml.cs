namespace GregOsborne.Developers.Controls {
	using System.Windows;
	using System.Windows.Controls;

	public partial class Watermark : UserControl {
		public Watermark() => this.InitializeComponent();

		public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(int), typeof(Watermark), new FrameworkPropertyMetadata(63547, OnIconPropertyChanged));
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(Watermark), new FrameworkPropertyMetadata("Watermark", OnTextPropertyChanged));
		public static new readonly DependencyProperty FontSizeProperty = DependencyProperty.Register("FontSize", typeof(double), typeof(Watermark), new FrameworkPropertyMetadata(60.0, OnFontSizePropertyChanged));

		public int Icon {
			get => (int)this.GetValue(IconProperty);
			set => this.SetValue(IconProperty, value);
		}

		public string Text {
			get => (string)this.GetValue(TextProperty);
			set => this.SetValue(TextProperty, value);
		}

		public new double FontSize {
			get => (double)this.GetValue(FontSizeProperty);
			set => this.SetValue(FontSizeProperty, value);
		}

		private static void OnIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var obj = (Watermark)d;
			var val = (int)e.NewValue;
			obj.theIcon.Text = char.ConvertFromUtf32(val);
		}

		private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var obj = (Watermark)d;
			var val = (string)e.NewValue;
			obj.theText.Text = val;
		}

		private static void OnFontSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var obj = (Watermark)d;
			var val = (double)e.NewValue;
			obj.theText.FontSize = val;
			obj.theIcon.FontSize = val * 2;
		}

	}
}
