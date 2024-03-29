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
using System.Windows.Shapes;
using MVVMFramework;

namespace User_Manager
{
	public partial class EditWebConfigWindow : Window
	{
		public EditWebConfigWindow()
		{
			InitializeComponent();
		}

		public EditWebConfigWindowView View
		{
			get
			{
				if (DesignerProperties.GetIsInDesignMode(this))
					return default(EditWebConfigWindowView);
				return LayoutRoot.DataContext as EditWebConfigWindowView;
			}
		}

		private void EditWebConfigWindowView_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{

		}

		private void EditWebConfigWindowView_ExecuteCommand(object sender, ExecuteUIActionEventArgs e)
		{
			switch (e.CommandToExecute)
			{
				case "CloseWindow":
					DialogResult = (bool)e.Parameters["result"];
					break;
			}
		}
	}
}
