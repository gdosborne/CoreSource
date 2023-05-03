namespace GregOsborne.Developers.Controls {
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;

	public partial class LabelAndToggleSwitch : UserControl {
		public LabelAndToggleSwitch() => this.InitializeComponent();

		public enum LabelAlignments {
			None,
			LeftSide,
			RightSide
		}

		public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register("IsChecked", typeof(bool), typeof(LabelAndToggleSwitch), new FrameworkPropertyMetadata(default(bool), OnIsCheckedPropertyChanged));
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(LabelAndToggleSwitch), new FrameworkPropertyMetadata(default(string), OnTextPropertyChanged));
		public static new readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(LabelAndToggleSwitch), new FrameworkPropertyMetadata(Brushes.Transparent, OnBackgroundPropertyChanged));
		public static new readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(LabelAndToggleSwitch), new FrameworkPropertyMetadata(Brushes.Black, OnForegroundPropertyChanged));
		public static readonly DependencyProperty SwitchBackgroundProperty = DependencyProperty.Register("SwitchBackground", typeof(Brush), typeof(LabelAndToggleSwitch), new FrameworkPropertyMetadata(Brushes.Black, OnSwitchBackgroundPropertyChanged));
		public static readonly DependencyProperty SwitchForegroundProperty = DependencyProperty.Register("SwitchForeground", typeof(Brush), typeof(LabelAndToggleSwitch), new FrameworkPropertyMetadata(Brushes.White, OnSwitchForegroundPropertyChanged));
		public static readonly DependencyProperty LabelAlignmentProperty = DependencyProperty.Register("LabelAlignment", typeof(LabelAlignments), typeof(LabelAndToggleSwitch), new FrameworkPropertyMetadata(LabelAlignments.RightSide, OnLabelAlignmentPropertyChanged));

		private static void OnIsCheckedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var obj = (LabelAndToggleSwitch)d;
			var val = (bool)e.NewValue;
			obj.theSwitch.IsChecked = val;
		}

		private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var obj = (LabelAndToggleSwitch)d;
			var val = (string)e.NewValue;
			if (obj.theLabelLeft != null) {
				obj.theLabelLeft.Text = val;
			}

			if (obj.theLabelRight != null) {
				obj.theLabelRight.Text = val;
			}
		}

		private static bool isChangingBackground = false;
		private static void OnBackgroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			if (isChangingBackground) {
				return;
			}

			var obj = (LabelAndToggleSwitch)d;
			var val = (Brush)e.NewValue;
			isChangingBackground = true;
			obj.theControl.Background = val;
			isChangingBackground = false;
			if (obj.theSwitch != null) {
				obj.theSwitch.Background = val;
			}
		}

		private static void OnForegroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var obj = (LabelAndToggleSwitch)d;
			var val = (Brush)e.NewValue;
			obj.theControl.Foreground = val;
			if (obj.theSwitch != null) {
				obj.theSwitch.Foreground = val;
			}

			if (obj.theLabelLeft != null) {
				obj.theLabelLeft.Foreground = val;
			}

			if (obj.theLabelRight != null) {
				obj.theLabelRight.Foreground = val;
			}
		}

		private static void OnSwitchBackgroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var obj = (LabelAndToggleSwitch)d;
			var val = (Brush)e.NewValue;
			if (obj.theSwitch != null) {
				obj.theSwitch.ToggleBackground = val;
			}
		}

		private static void OnSwitchForegroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var obj = (LabelAndToggleSwitch)d;
			var val = (Brush)e.NewValue;
			if (obj.theSwitch != null) {
				obj.theSwitch.ToggleForeground = val;
			}
		}

		private static void OnLabelAlignmentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var obj = (LabelAndToggleSwitch)d;
			var val = (LabelAlignments)e.NewValue;
			if (obj.theLabelLeft != null) {
				obj.theLabelLeft.Visibility = val == LabelAlignments.None
				? Visibility.Collapsed
				: val == LabelAlignments.LeftSide
					? Visibility.Visible
					: Visibility.Collapsed;
			}

			if (obj.theLabelRight != null) {
				obj.theLabelRight.Visibility = val == LabelAlignments.None
				? Visibility.Collapsed
				: val == LabelAlignments.RightSide
					? Visibility.Visible
					: Visibility.Collapsed;
			}
		}

		public bool IsChecked {
			get => (bool)this.GetValue(IsCheckedProperty);
			set => this.SetValue(IsCheckedProperty, value);
		}

		public string Text {
			get => (string)this.GetValue(TextProperty);
			set => this.SetValue(TextProperty, value);
		}

		public new Brush Background {
			get => (Brush)this.GetValue(BackgroundProperty);
			set => this.SetValue(BackgroundProperty, value);
		}

		public new Brush Foreground {
			get => (Brush)this.GetValue(ForegroundProperty);
			set => this.SetValue(ForegroundProperty, value);
		}

		public Brush SwitchBackground {
			get => (Brush)this.GetValue(SwitchBackgroundProperty);
			set => this.SetValue(SwitchBackgroundProperty, value);
		}

		public Brush SwitchForeground {
			get => (Brush)this.GetValue(SwitchForegroundProperty);
			set => this.SetValue(SwitchForegroundProperty, value);
		}

		public LabelAlignments LabelAlignment {
			get => (LabelAlignments)this.GetValue(LabelAlignmentProperty);
			set => this.SetValue(LabelAlignmentProperty, value);
		}
	}
}
