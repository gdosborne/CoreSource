namespace MyMinistry
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
	using GregOsborne.Application.Windows;
	using MVVMFramework;

	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			View.PropertyChanged += View_PropertyChanged;
			View.ExecuteUIAction += View_ExecuteUIAction;
		}

		void View_ExecuteUIAction(object sender, ExecuteUIActionEventArgs e)
		{
			
		}

		void View_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			this.HideMinimizeAndMaximizeButtons();
		}

		public MainWindowView View { get { return LayoutRoot.GetView<MainWindowView>(); } }

		private void TextBlock_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			this.Close();
		}

		private void TextBlock_PreviewTouchDown(object sender, TouchEventArgs e)
		{
			this.Close();
		}
	}
}
