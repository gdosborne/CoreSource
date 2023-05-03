namespace SNC.Applications.Developer.Controls
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Data;
	using System.Windows.Documents;
	using System.Windows.Input;
	using System.Windows.Media;
	using System.Windows.Media.Imaging;
	using System.Windows.Navigation;
	using System.Windows.Shapes;
	using MVVMFramework;

	public partial class TreeViewItemHeader : UserControl
	{
		public TreeViewItemHeader() {
			InitializeComponent();
		}

		public TreeViewItemHeaderView View {
			get {
				return LayoutRoot.GetView<TreeViewItemHeaderView>();
			}
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			View.InitView();
		}


		#region Source
		public ImageSource Source {
			get { return (ImageSource)GetValue(SourceProperty); }
			set { SetValue(SourceProperty, value); }
		}

		public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(TreeViewItemHeader), new PropertyMetadata(null, onSourceChanged));
		private static void onSourceChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (TreeViewItemHeader)source;
			if (src == null)
				return;
			var value = (ImageSource)e.NewValue;
			src.View.HeaderImageSource = value;
		}
		#endregion

		#region Text
		public string Text {
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TreeViewItemHeader), new PropertyMetadata(string.Empty, onTextChanged));
		private static void onTextChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (TreeViewItemHeader)source;
			if (src == null)
				return;
			var value = (string)e.NewValue;
			src.View.HeaderText = value;
		}
		#endregion

	}
}
