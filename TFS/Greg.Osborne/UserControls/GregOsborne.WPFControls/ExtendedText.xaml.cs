using System.Windows;
using System.Windows.Controls;

namespace GregOsborne.WPFControls {
	public partial class ExtendedText : UserControl {
		public ExtendedText() {
			this.InitializeComponent();
			this.IsExpanded = true;
		}

		public static readonly DependencyProperty InitialTextProperty = DependencyProperty.Register("InitialText", typeof(string), typeof(ExtendedText), new FrameworkPropertyMetadata(default(string), OnInitialTextPropertyChanged));
		public string InitialText {
			get => (string)this.GetValue(InitialTextProperty);
			set => this.SetValue(InitialTextProperty, value);
		}
		private static void OnInitialTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var obj = (ExtendedText)d;
			var val = (string)e.NewValue;
			obj.initialTextBlock.Text = val;
		}

		public static readonly DependencyProperty ContinuingTextProperty = DependencyProperty.Register("ContinuingText", typeof(string), typeof(ExtendedText), new FrameworkPropertyMetadata(default(string), OnContinuingTextPropertyChanged));
		public string ContinuingText {
			get => (string)this.GetValue(ContinuingTextProperty);
			set => this.SetValue(ContinuingTextProperty, value);
		}
		private static void OnContinuingTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var obj = (ExtendedText)d;
			var val = (string)e.NewValue;
			if (obj.isSettingValue) {
				obj.continuationTextBlock.Text = val;
			} else {
				obj.SetContinuationText();
			}
		}

		public static readonly DependencyProperty LinespacingBeforeContinuingTextProperty = DependencyProperty.Register("LinespacingBeforeContinuingText", typeof(int), typeof(ExtendedText), new FrameworkPropertyMetadata(1, OnLinespacingBeforeContinuingTextPropertyChanged));
		public int LinespacingBeforeContinuingText {
			get => (int)this.GetValue(LinespacingBeforeContinuingTextProperty);
			set => this.SetValue(LinespacingBeforeContinuingTextProperty, value);
		}
		private static void OnLinespacingBeforeContinuingTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var obj = (ExtendedText)d;
			var val = (int)e.NewValue;
			if (val < 1) {
				val = 1;
			}
			obj.SetContinuationText();
		}

		private bool isSettingValue = false;
		private void SetContinuationText() {
			var result = default(string);
			if (!string.IsNullOrEmpty(this.ContinuingText)) {
				result = this.ContinuingText.TrimStart('\n');
			} else {
				result = this.ContinuingText;
			}
			for (var i = 1; i < this.LinespacingBeforeContinuingText; i++) {
				result = "\n" + result;
			}
			this.isSettingValue = true;
			this.ContinuingText = result;
			this.isSettingValue = false;
		}

		public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register("IsExpanded", typeof(bool), typeof(ExtendedText), new FrameworkPropertyMetadata(default(bool), OnIsExpandedPropertyChanged));
		public bool IsExpanded {
			get => (bool)this.GetValue(IsExpandedProperty);
			set => this.SetValue(IsExpandedProperty, value);
		}
		private static void OnIsExpandedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var obj = (ExtendedText)d;
			var val = (bool)e.NewValue;
			obj.continuationTextBlock.Visibility = val ? Visibility.Visible : Visibility.Collapsed;
			obj.controllerTextBlock.Text = val ? char.ConvertFromUtf32(60891) : char.ConvertFromUtf32(60892);
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			this.IsExpanded = !this.IsExpanded;
		}
	}
}
