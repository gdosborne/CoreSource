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
using MyApplication.Windows;
using MyApplication.Security;
using Configuration;
namespace DomainationClient.Windows
{
	public partial class LoginWindow : Window
	{
		public LoginWindow(Window owner)
		{
			Owner = owner;
			InitializeComponent();
		}
		public string Username { get; set; }
		public string Password { get; set; }
		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			this.HideControlBox();
		}
		private void OKCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = !string.IsNullOrEmpty(TheUserName.Text)
				&& !string.IsNullOrEmpty(ThePassword.Password);
		}
		private void OKExecute(object sender, ExecutedRoutedEventArgs e)
		{
			App.SetSetting("SaveUsername", RememberUserNameCheckBox.IsChecked.GetValueOrDefault());
			App.SetSetting("SavePassword", RememberPasswordCheckBox.IsChecked.GetValueOrDefault());
			App.SetSetting("Username", (bool)App.Settings["SaveUsername"] ? TheUserName.Text : string.Empty);
			App.SetSetting("Password", (bool)App.Settings["SavePassword"] ? ThePassword.Password.Encrypt(App.Password) : string.Empty);
			Username = TheUserName.Text;
			Password = ThePassword.Password.Encrypt(App.Password);
			DialogResult = true;
		}
		private void CancelExecute(object sender, ExecutedRoutedEventArgs e)
		{
			DialogResult = false;
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (App.Settings.ContainsKey("SaveUsername"))
				RememberUserNameCheckBox.IsChecked = (bool)App.Settings["SaveUsername"];
			if (App.Settings.ContainsKey("SavePassword"))
				RememberPasswordCheckBox.IsChecked = (bool)App.Settings["SavePassword"];
			if (RememberUserNameCheckBox.IsChecked.GetValueOrDefault() && App.Settings.ContainsKey("Username"))
				TheUserName.Text = (string)App.Settings["Username"];
			if (RememberPasswordCheckBox.IsChecked.GetValueOrDefault() && App.Settings.ContainsKey("Password"))
				ThePassword.Password = ((string)App.Settings["Password"]).Decrypt(App.Password);
		}
		private void NewUserExecute(object sender, ExecutedRoutedEventArgs e)
		{
			var userWin = new UserWindow(this);
			var result = userWin.ShowDialog();
			if (!result.GetValueOrDefault())
				return;
		}
	}
}
