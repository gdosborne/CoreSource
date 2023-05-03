using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
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
using GregOsborne.Application.Media;

namespace ControlTester
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
            MyTitlebar.AddContextMenuItem("Some menu item", Assembly.GetExecutingAssembly().GetImageSourceByName("attachment.png", 10));
            MyTitlebar.AddContextMenuItem("Another menu item", Assembly.GetExecutingAssembly().GetImageSourceByName("addthin.png", 10));
            var item = new MenuItem {
                Header = "Has sub menu"
            };
            item.Items.Add(new MenuItem { Header = "Menu 1" });
            item.Items.Add(new MenuItem { Header = "Menu 2" });
            MyTitlebar.AddContextMenuItem(item);
        }
	}
}
