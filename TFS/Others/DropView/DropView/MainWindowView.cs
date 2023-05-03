using MVVMFramework;
using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace DropView
{
	public class MainWindowView : ViewModelBase, INotifyPropertyChanged
	{
		public override event PropertyChangedEventHandler PropertyChanged;
		public override void UpdateInterface()
		{
			base.UpdateInterface();
		}
	}
}
