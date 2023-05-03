using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Greg.Osborne.Installer {
	public delegate void TabSelectedHandler(object sender, System.Windows.Input.MouseButtonEventArgs e);

	public partial class TabbItem : UserControl {
		public TabbItem() => this.InitializeComponent();

		public event TabSelectedHandler TabSelected;

		public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(TabbItem), new FrameworkPropertyMetadata(default(bool), OnIsSelectedPropertyChanged));
		public bool IsSelected {
			get => (bool)this.GetValue(IsSelectedProperty);
			set => this.SetValue(IsSelectedProperty, value);
		}
		private static void OnIsSelectedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var obj = (TabbItem)d;
			var val = (bool)e.NewValue;
			obj.selectedBorder.Visibility = val ? Visibility.Visible : Visibility.Collapsed;
		}

		public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register("HeaderText", typeof(string), typeof(TabbItem), new FrameworkPropertyMetadata("Header", OnHeaderTextPropertyChanged));
		public string HeaderText {
			get => (string)this.GetValue(HeaderTextProperty);
			set => this.SetValue(HeaderTextProperty, value);
		}
		private static void OnHeaderTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var obj = (TabbItem)d;
			var val = (string)e.NewValue;
			obj.header.Text = val;
		}

		public static readonly DependencyProperty IsSelectionLeftToClientProperty = DependencyProperty.Register("IsSelectionLeftToClient", typeof(bool), typeof(TabbItem), new FrameworkPropertyMetadata(true, OnIsSelectionLeftToClientPropertyChanged));
		public bool IsSelectionLeftToClient {
			get => (bool)this.GetValue(IsSelectionLeftToClientProperty);
			set => this.SetValue(IsSelectionLeftToClientProperty, value);
		}
		private static void OnIsSelectionLeftToClientPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var obj = (TabbItem)d;
			var val = (bool)e.NewValue;
			//modify the value here, i.e., obj.ctrl.Value = val
		}

		private void UserControl_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
			e.Handled = true;
			if (!this.IsSelectionLeftToClient) {
				this.IsSelected = true;
			}

			TabSelected?.Invoke(this, e);
		}

		public static readonly DependencyProperty SelectedBrushProperty = DependencyProperty.Register("SelectedBrush", typeof(Brush), typeof(TabbItem), new FrameworkPropertyMetadata(default(Brush), OnSelectedBrushPropertyChanged));
		public Brush SelectedBrush {
			get => (Brush)this.GetValue(SelectedBrushProperty);
			set => this.SetValue(SelectedBrushProperty, value);
		}
		private static void OnSelectedBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var obj = (TabbItem)d;
			var val = (Brush)e.NewValue;
			obj.selectedBorder.Background = val;
		}

	}
}
