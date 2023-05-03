using System.Net;
using System.Windows;
//using CredentialManagement;

namespace GregOsborne.Dialog {
    //public sealed class CredentialDialog {
    //    public string ApplicationName { get; set; }
    //    public NetworkCredential Credentials { get; set; }
    //    public string Instructions { get; set; }
    //    public bool IsConfirmed { get; private set; }
    //    public bool IsSaveChecked { get; private set; }
    //    public bool ShowSaveCheckBox { get; set; }
    //    public bool ShowUiForSavedCredentials { get; set; }
    //    public string Title { get; set; }
    //    public bool TopMost { get; set; }

    //    public void ConfirmCredentials(bool save) {
    //        IsConfirmed = true;
    //        if (save)
    //            SaveCredentials();
    //    }

        //public void DeleteCredentials() {
        //    var credential = new Credential {
        //        Target = ApplicationName
        //    };
        //    credential.Load();
        //    credential.Delete();
        //}

        //public void SaveCredentials() {
        //    if (!IsSaveChecked || Credentials == null)
        //        return;

        //    var credential = new Credential {
        //        Target = ApplicationName
        //    };
        //    credential.Load();
        //    credential.Username = Credentials.UserName;
        //    credential.Password = Credentials.Password;
        //    credential.Save();
        //}
        //public void RemoveSavedCredentials() {
        //    var credential = new Credential {
        //        Target = ApplicationName
        //    };
        //    credential.Load();
        //    credential.Delete();
        //    credential.Save();
        //}
        //public void Show() {
        //    var dlg = new CredentialsDialog {
        //        WindowStartupLocation = WindowStartupLocation.CenterScreen,
        //        Title = Title,
        //        Topmost = TopMost
        //    };
        //    var creds = GetSavedCredentials();
        //    dlg.View.Credentials = creds;
        //    dlg.View.IsSaveChecked = IsSaveChecked;
        //    dlg.View.SaveVisibility = ShowSaveCheckBox ? Visibility.Visible : Visibility.Collapsed;
        //    dlg.View.Instructions = Instructions;
        //    if (!string.IsNullOrEmpty(creds?.UserName) && !string.IsNullOrEmpty(creds.Password) && !ShowUiForSavedCredentials)
        //        return;
        //    dlg.Show();
        //}

        //public bool? ShowDialog(Window owner) {
        //    var dlg = new CredentialsDialog {
        //        WindowStartupLocation = WindowStartupLocation.CenterScreen,
        //        Owner = owner,
        //        Title = Title,
        //        Topmost = TopMost
        //    };
        //    var creds = GetSavedCredentials();
        //    dlg.View.Credentials = creds;
        //    dlg.View.IsSaveChecked = IsSaveChecked;
        //    dlg.View.SaveVisibility = ShowSaveCheckBox ? Visibility.Visible : Visibility.Collapsed;
        //    dlg.View.Instructions = Instructions;
        //    if (!string.IsNullOrEmpty(creds?.UserName) && !string.IsNullOrEmpty(creds.Password) && !ShowUiForSavedCredentials) {
        //        Credentials = new NetworkCredential(dlg.View.UserName, dlg.View.Password);
        //        return true;
        //    }
        //    var result = dlg.ShowDialog();
        //    if (!result.GetValueOrDefault())
        //        return false;
        //    Credentials = new NetworkCredential(dlg.View.UserName, dlg.View.Password);
        //    IsSaveChecked = dlg.View.IsSaveChecked;
        //    return result;
        //}

        //private NetworkCredential GetSavedCredentials() {
        //    var result = new NetworkCredential();
        //    var credential = new Credential {
        //        Target = ApplicationName
        //    };
        //    if (!credential.Exists())
        //        return result;
        //    credential.Load();
        //    result.UserName = credential.Username;
        //    result.Password = credential.Password;
        //    IsSaveChecked = true;
        //    return result;
        //}
    //}
}