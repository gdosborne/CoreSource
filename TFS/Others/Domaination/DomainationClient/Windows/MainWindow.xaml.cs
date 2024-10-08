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
using DomainationClient.Service;
using System.Windows.Media.Effects;
namespace DomainationClient.Windows
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			bool? result = null;
			if (!App.Settings.ContainsKey("ServerUrl"))
			{
				var settingsWin = new SettingsWindow(this);
				result = settingsWin.ShowDialog();
				if (!result.GetValueOrDefault())
				{
					App.Current.Shutdown(998);
					return;
				}
			}
			if (App.Settings.ContainsKey("CheckConnectionAtStart") && (bool)App.Settings["CheckConnectionAtStart"])
			{
				if (!App.TestServer())
				{
					App.Current.Shutdown(997);
					return;
				}
			}
			var loginWin = new LoginWindow(this);
			result = loginWin.ShowDialog();
			if (!result.GetValueOrDefault())
			{
				App.Current.Shutdown(999);
				return;
			}
			using (var client = References.DomainationClient((string)App.Settings["ServerUrl"]))
			{
				try
				{
					var newUserLogin = false;
					var user = client.Login(loginWin.Username, loginWin.Password, out newUserLogin);
					if(user == null && newUserLogin)
					{
					}
				}
				catch(Exception ex)
				{
					App.DisplayExceptionDetails(ex, true);
				}
			}
		}
		private void SettingsExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			var settingsWin = new SettingsWindow(this);
			settingsWin.ShowDialog();
		}
	}
}
