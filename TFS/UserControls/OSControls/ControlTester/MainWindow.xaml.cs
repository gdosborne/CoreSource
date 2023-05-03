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
using System.Windows.Threading;

namespace ControlTester
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			MyValueDisplay.Minimum = 145;
			MyValueDisplay.Maximum = 522;
			MyValueDisplay.Value = MyValueDisplay.Minimum;
			timer1.Tick += timer1_Tick;
			timer1.Start();
		}

		private Random r = new Random();
		void timer1_Tick(object sender, EventArgs e)
		{
			MyValueDisplay.Value = Convert.ToDouble(r.Next(Convert.ToInt32(MyValueDisplay.Minimum), Convert.ToInt32(MyValueDisplay.Maximum)));
		}
		private DispatcherTimer timer1 = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
	}

}
