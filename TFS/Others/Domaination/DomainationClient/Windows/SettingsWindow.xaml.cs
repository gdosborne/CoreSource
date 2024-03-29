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
using Configuration;
using MyApplication.Windows;
using DomainationClient.Service;
using Sid.Windows.Controls;
namespace DomainationClient.Windows
{
	public partial class SettingsWindow : Window
	{
		public SettingsWindow(Window owner)
		{
			Owner = owner;
			InitializeComponent();
		}
		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			this.HideControlBox();
		}
		private void OKCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}
		private void OKExecute(object sender, ExecutedRoutedEventArgs e)
		{
			App.SetSetting("ServerUrl", ServerUrl.Text);
			App.SetSetting("CheckConnectionAtStart", CheckConnectionAtStart.IsChecked.GetValueOrDefault());
			DialogResult = true;
		}
		private void CancelExecute(object sender, ExecutedRoutedEventArgs e)
		{
			DialogResult = false;
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (App.Settings.ContainsKey("ServerUrl"))
				ServerUrl.Text = (string)App.Settings["ServerUrl"];
			if (App.Settings.ContainsKey("CheckConnectionAtStart"))
				CheckConnectionAtStart.IsChecked = (bool)App.Settings["CheckConnectionAtStart"];
		}
		private void TestCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = !string.IsNullOrEmpty(ServerUrl.Text);
		}
		private void TestExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			App.TestServer();
		}
	}
}
