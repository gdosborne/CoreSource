namespace GregOsborne.Suite.Extender.UserControls {
	using System.Windows;
	using System.Windows.Controls;
	using GregOsborne.Application.Primitives;

	public partial class LabeledToggleSwitch : UserControl {
		public LabeledToggleSwitch() => this.InitializeComponent();

		public LabeledToggleSwitchView View => this.DataContext.As<LabeledToggleSwitchView>();

		public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register("IsChecked", typeof(bool), typeof(LabeledToggleSwitch), new FrameworkPropertyMetadata(default(bool), OnIsCheckedPropertyChanged));
		public bool IsChecked {
			get => (bool)this.GetValue(IsCheckedProperty);
			set => this.SetValue(IsCheckedProperty, value);
		}
		private static void OnIsCheckedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var obj = (LabeledToggleSwitch)d;
			var val = (bool)e.NewValue;
			obj.View.IsChecked = val;
		}

		public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register("LabelText", typeof(string), typeof(LabeledToggleSwitch), new FrameworkPropertyMetadata("Label goes here", OnLabelTextPropertyChanged));
		public string LabelText {
			get => (string)this.GetValue(LabelTextProperty);
			set => this.SetValue(LabelTextProperty, value);
		}
		private static void OnLabelTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var obj = (LabeledToggleSwitch)d;
			var val = (string)e.NewValue;
			obj.View.LabelText = val;
		}

	}
}
