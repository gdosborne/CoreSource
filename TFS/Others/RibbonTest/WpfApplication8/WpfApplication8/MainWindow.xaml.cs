using System;
using System.Windows.Controls.Ribbon;

namespace WpfApplication8 {
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

		private void MainWindowView_ExitApp(object sender, EventArgs e)
		{
			App.Current.Shutdown();
		}
    }
}
