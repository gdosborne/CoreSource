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
namespace WpfApplication1
{
	public partial class Designer : UserControl
	{
		public Designer()
		{
			InitializeComponent();
		}
		public new Brush Background
		{
			get { return (Brush)GetValue(BackgroundProperty); }
			set { SetValue(BackgroundProperty, value); }
		}
		public static new readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(Designer), new PropertyMetadata(SystemColors.AppWorkspaceBrush, onBackgroundChanged));
		private static void onBackgroundChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			var src = obj as Designer;
			var val = (Brush)e.NewValue;
			src.MyCanvas.Background = val;
		}
	}
}
