namespace GregOsborne.Dialog
{
	//using CredentialManagement;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net;
	using System.Windows;

	public sealed class CredentialDialog
	{
		#region Public Methods
		public void ConfirmCredentials(bool save)
		{
			IsConfirmed = true;
			if (save)
				SaveCredentials();
		}
		public void DeleteCredentials()
		{
			//var cm = new Credential();
			//cm.Target = ApplicationName;
			//cm.Load();
			//cm.Delete();
		}
		public void SaveCredentials()
		{
			if (!IsSaveChecked || Credentials == null)
				return;

			//var cm = new Credential();
			//cm.Target = ApplicationName;
			//cm.Load();
			//cm.Username = Credentials.UserName;
			//cm.Password = Credentials.Password;
			//cm.Save();
		}
		public void Show()
		{
			//var dlg = new CredentialsDialog
			//{
			//	WindowStartupLocation = WindowStartupLocation.CenterScreen,
			//	Title = Title,
			//	Topmost = TopMost
			//};
			//var creds = GetSavedCredentials();
			//dlg.View.Credentials = creds;
			//dlg.View.IsSaveChecked = IsSaveChecked;
			//dlg.View.SaveVisibility = ShowSaveCheckBox ? Visibility.Visible : Visibility.Collapsed;
			//dlg.View.Instructions = Instructions;
			//dlg.Show();
		}
		public bool? ShowDialog(Window owner)
		{
            //var dlg = new CredentialsDialog
            //{
            //	WindowStartupLocation = WindowStartupLocation.CenterScreen,
            //	Owner = owner,
            //	Title = Title,
            //	Topmost = TopMost
            //};
            //var creds = GetSavedCredentials();
            //dlg.View.Credentials = creds;
            //dlg.View.IsSaveChecked = IsSaveChecked;
            //dlg.View.SaveVisibility = ShowSaveCheckBox ? Visibility.Visible : Visibility.Collapsed;
            //dlg.View.Instructions = Instructions;
            //var result = dlg.ShowDialog();
            //if (!result.GetValueOrDefault())
            //	return false;
            //IsSaveChecked = dlg.View.IsSaveChecked;
            //Credentials = new NetworkCredential(dlg.View.UserName, dlg.View.Password);
            //return result;
            return null;
		}
		#endregion Public Methods

		#region Private Methods
		private NetworkCredential GetSavedCredentials()
		{
			var result = new NetworkCredential();

			//var cm = new Credential();
			//cm.Target = ApplicationName;
			//if (!cm.Exists())
			//	return result;
			//cm.Load();
			//result.UserName = cm.Username;
			//result.Password = cm.Password;
			//IsSaveChecked = true;
			return result;
		}
		#endregion Private Methods

		#region Public Properties
		public string ApplicationName { get; set; }
		public NetworkCredential Credentials { get; set; }
		public string Instructions { get; set; }
		public bool IsConfirmed { get; private set; }
		public bool IsSaveChecked { get; private set; }
		public bool ShowSaveCheckBox { get; set; }
		public bool ShowUIForSavedCredentials { get; set; }
		public string Title { get; set; }
		public bool TopMost { get; set; }
		#endregion Public Properties
	}
}
