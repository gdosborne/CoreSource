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
using DomainationData.Entities;
using DomainationClient.Service;
using MyApplication.Security;
namespace DomainationClient.Windows
{
	public partial class UserWindow : Window
	{
		public UserWindow(Window owner)
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
			e.CanExecute = !string.IsNullOrEmpty(TheUserName.Text)
				&& !string.IsNullOrEmpty(TheFirstName.Text)
				&& !string.IsNullOrEmpty(TheLastName.Text)
				&& !string.IsNullOrEmpty(ThePassword1.Password)
				&& !string.IsNullOrEmpty(ThePassword2.Password)
				&& ThePassword1.Password == ThePassword2.Password;
		}
		private void OKExecute(object sender, ExecutedRoutedEventArgs e)
		{
			var user = new User
			{
				EMailAddress = TheUserName.Text,
				FirstName = TheFirstName.Text,
				LastName = TheLastName.Text
			};
			using (var client = References.DomainationClient((string)App.Settings["ServerUrl"]))
			{
				var userExists = client.CreateUser(user, ThePassword1.Password.Encrypt(App.Password));
				if (userExists)
					App.DisplayExceptionDetails(new Exception("The user already exists"), false);
			}
			DialogResult = true;
		}
		private void CancelExecute(object sender, ExecutedRoutedEventArgs e)
		{
			DialogResult = false;
		}
	}
}
