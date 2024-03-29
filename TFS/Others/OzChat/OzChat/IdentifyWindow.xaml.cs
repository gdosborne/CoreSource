namespace OzChat
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
	using MVVMFramework;
	using GregOsborne.Application.Primitives;
	using System.Windows.Shapes;
	using OzChat.Views;

	internal partial class IdentifyWindow : Window
	{
		public IdentifyWindow()
		{
			InitializeComponent();
		}
		public IdentifyWindowView View
		{
			get { return LayoutRoot.GetView<IdentifyWindowView>(); }
		}

		private void TextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			sender.As<TextBox>().SelectAll();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			View.InitView();
		}

		private void IdentifyWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			switch(e.PropertyName)
			{
				case "DialogResult":
					DialogResult = View.DialogResult;
				break;
			}
		}
	}
}
