using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace KHSound.Controls
{
	public partial class TitleBar : UserControl
	{
		public event EventHandler CloseButtonClicked;
		public event EventHandler MaximizeButtonClicked;
		public event EventHandler MinimizeButtonClicked;
		public TitleBar()
		{
			InitializeComponent();
		}
		public Visibility CloseVisibilty { get { return CloseImage.Visibility; } set { CloseImage.Visibility = value; } }
		public Visibility MaximizeVisibilty { get { return MaximizeImage.Visibility; } set { MaximizeImage.Visibility = value; } }
		public Visibility MinimizeVisibilty { get { return MinimizeImage.Visibility; } set { MinimizeImage.Visibility = value; } }
		public string Text { get { return TitleTextBlock.Text; } set { TitleTextBlock.Text = value; } }
		public Brush TitleBackground { get { return LayoutBorder.Background; } set { LayoutBorder.Background = value; } }
		private void CloseImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if(CloseButtonClicked != null)
				CloseButtonClicked(this, EventArgs.Empty);
		}
		private void MaximizeImage_MouseEnter(object sender, MouseEventArgs e)
		{
			var img = sender as Image;
			if(Window.GetWindow(this).WindowState == WindowState.Maximized)
				img.ToolTip = "Restore";
			else
				img.ToolTip = "Maximize";
		}
		private void MaximizeImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if(MaximizeButtonClicked != null)
				MaximizeButtonClicked(this, EventArgs.Empty);
		}
		private void MinimizeImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if(MinimizeButtonClicked != null)
				MinimizeButtonClicked(this, EventArgs.Empty);
		}
		private void ThisTitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
			var win = Window.GetWindow(this);
			if(win == null)
				return;
			win.DragMove();
		}
	}
}
