using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace KHSound
{
	public partial class SupportRequestWindow : Window
	{
		public SupportRequestWindow(Window owner)
		{
			Owner = owner;
			InitializeComponent();
		}
		public string Subject { get { return SubjectTextBox.Text; } set { SubjectTextBox.Text = value; } }
		public string EmailAddress { get { return EMailTextBox.Text; } set { EMailTextBox.Text = value; } }
		public string Body { get { return RequestTextBox.Text; } set { RequestTextBox.Text = value; } }
		private void OKCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			DialogResult = true;
		}
		private void CancelCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			DialogResult = false;
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
		}
		private void TextBoxGotFocus(object sender, RoutedEventArgs e)
		{
			var tb = sender as TextBox;
			tb.SelectAll();
		}
	}
}
