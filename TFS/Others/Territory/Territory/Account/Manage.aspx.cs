using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using Territory;
public partial class Account_Manage : System.Web.UI.Page
{
    protected string SuccessMessage
    {
        get;
        private set;
    }
    protected bool CanRemoveExternalLogins
    {
        get;
        private set;
    }
    private bool HasPassword(UserManager manager)
    {
		return true;
    }
    protected void Page_Load()
    {
        if (!IsPostBack)
        {
        }
    }
    protected void ChangePassword_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
        }
    }
    protected void SetPassword_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
        }
    }
    public IEnumerable<UserLoginInfo> GetLogins()
    {
		return null;
    }
    public void RemoveLogin(string loginProvider, string providerKey)
    {
    }
    private void AddErrors(IdentityResult result)
    {
    }
}
