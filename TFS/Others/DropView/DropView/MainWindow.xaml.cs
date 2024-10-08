using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using GregOsborne.MVVMFramework;

namespace DropView
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		public MainWindowView View
		{
			get
			{
				return this.GetView<MainWindowView>();
			}
		}

		private void MainWindowView_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			View.UpdateInterface();
		}
	}
}
