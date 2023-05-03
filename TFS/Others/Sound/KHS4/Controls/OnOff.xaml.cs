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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KHS4.Controls
{
	public partial class OnOff : UserControl
	{
		public event EventHandler ValueChanged;
		public OnOff()
		{
			InitializeComponent();
		}

		private bool _Value = false;
		public bool Value 
		{ 
			get 
			{ 
				return _Value; 
			} 
			set 
			{ 
				_Value = value; 
				SetControl(value); 
			} 
		}

		private void NameBorder_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			var bdr = sender as Border;
			Value = (int)bdr.GetValue(Grid.ColumnProperty) == 1;
			if (ValueChanged != null)
				ValueChanged(this, EventArgs.Empty);
		}

		private void SetControl(bool value)
		{
			ThumbBorder.SetValue(Grid.ColumnProperty, value ? 1 : 0);
			NameBorder.SetValue(Grid.ColumnProperty, value ? 0 : 1);
			NameTextBlock.Text = value ? "On" : "Off";
		}
	}
}
