namespace XPad
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
	using System.Windows.Shapes;
	using XPad.Views;
	using MVVMFramework;

	public partial class OptionsWindow : Window
	{
		public OptionsWindow()
		{
			InitializeComponent();
		}

		public OptionsWindowView View { get { return this.GetView<OptionsWindowView>(); } }

		private void OptionsWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "DialogResult":
					DialogResult = View.DialogResult;
					break;
			}
		}
	}
}
