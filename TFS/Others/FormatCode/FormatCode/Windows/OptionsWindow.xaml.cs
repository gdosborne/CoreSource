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
using Application.Windows;
using Application.Primitives;
using FormatCode.Classes;
namespace FormatCode.Windows
{
	public partial class OptionsWindow : Window
	{
		public OptionsWindow()
		{
			InitializeComponent();
		}
		public OptionsWindowView View
		{
			get
			{
				if (DesignerProperties.GetIsInDesignMode(this))
					return null;
				return LayoutRoot.DataContext.As<OptionsWindowView>();
			}
		}
		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			this.HideControlBox();
			View.OptionFlags = (FormatCode.Classes.Enumerations.OptionFlags)Registry.GetValue<long>("Options", 0);
			View.InitializeOptions();
		}
		private void OptionsWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
		}
		private void OptionsWindowView_CloseRequest(object sender, CloseEventArgs e)
		{
			DialogResult = e.DialogResult;
		}
		private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}
	}
}
