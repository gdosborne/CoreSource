namespace XPad.Controls
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
	using XPadLib;

	public partial class TreeViewItemHeader : UserControl
	{
		public TreeViewItemHeader()
		{
			InitializeComponent();
		}

		public class RenameCompleteEventArgs : EventArgs
		{
			public RenameCompleteEventArgs(string newName, string oldName)
			{
				NewName = newName;
				OldName = oldName;
			}
			public string NewName { get; private set; }
			public string OldName { get; private set; }
		}
		public delegate void RenameCompleteEventHandler(object sender, RenameCompleteEventArgs e);
		public event RenameCompleteEventHandler RenameComplete;

		#region Icon
		public int Icon
		{
			get { return (int)GetValue(IconProperty); }
			set { SetValue(IconProperty, value); }
		}

		public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(int), typeof(TreeViewItemHeader), new PropertyMetadata(0xEC51, onIconChanged));
		private static void onIconChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (TreeViewItemHeader)source;
			if (src == null)
				return;
			var value = (int)e.NewValue;
			src.IconTBlock.Text = Char.ConvertFromUtf32(value);
		}
		#endregion

		#region Title
		public string Title
		{
			get { return (string)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}

		public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(TreeViewItemHeader), new PropertyMetadata("Name", onTitleChanged));
		private static void onTitleChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (TreeViewItemHeader)source;
			if (src == null)
				return;
			var value = (string)e.NewValue;
			src.NameTBlock.Text = value;
			src.RenameTextBox.Text = value;
		}
		#endregion

		#region RenameVisibility
		public Visibility RenameVisibility
		{
			get { return (Visibility)GetValue(RenameVisibilityProperty); }
			set { SetValue(RenameVisibilityProperty, value); }
		}

		public static readonly DependencyProperty RenameVisibilityProperty = DependencyProperty.Register("RenameVisibility", typeof(Visibility), typeof(TreeViewItemHeader), new PropertyMetadata(Visibility.Collapsed, onRenameVisibilityChanged));
		private static void onRenameVisibilityChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (TreeViewItemHeader)source;
			if (src == null)
				return;
			var value = (Visibility)e.NewValue;
			src.RenameTextBox.Visibility = value;
			src.NameTBlock.Visibility = value == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			if (value == Visibility.Visible)
			{
				src.RenameTextBox.Focus();
				src.RenameTextBox.SelectAll();
			}
		}
		#endregion

		#region Element
		public XPadElement Element
		{
			get { return (XPadElement)GetValue(ElementProperty); }
			set { SetValue(ElementProperty, value); }
		}

		public static readonly DependencyProperty ElementProperty = DependencyProperty.Register("Element", typeof(XPadElement), typeof(TreeViewItemHeader), new PropertyMetadata(null, onElementChanged));
		private static void onElementChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (TreeViewItemHeader)source;
			if (src == null)
				return;
			var value = (XPadElement)e.NewValue;			
		}
		#endregion

		private void OnComplete()
		{
			if (RenameComplete != null)
				RenameComplete(this, new RenameCompleteEventArgs(RenameTextBox.Text, Element.Name));
			Element.Name = RenameTextBox.Text;
			NameTBlock.Text = RenameTextBox.Text;
			RenameVisibility = Visibility.Collapsed;
		}
		private void RenameTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			OnComplete();
		}

		private void RenameTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				OnComplete();
			else if (e.Key == Key.Escape)
			{
				RenameTextBox.Text = Element.Name;
				RenameVisibility = Visibility.Collapsed;
			}
		}

	}
}
