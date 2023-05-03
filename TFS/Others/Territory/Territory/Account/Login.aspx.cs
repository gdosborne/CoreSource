using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Web;
using System.Web.UI;
using Territory;
using System.Linq;
using Models;
public partial class Account_Login : Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		RegisterHyperLink.NavigateUrl = "Register";
		var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
		if (!String.IsNullOrEmpty(returnUrl))
		{
			RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
		}
	}
	protected void LogIn(object sender, EventArgs e)
	{
		if (IsValid)
		{
			var dataContext = new TerritoryDataContext();
			var manager = new UserManager(dataContext);
			ApplicationUser user = manager.Find(UserName.Text, Password.Text);
			if (user != null)
			{
				IdentityHelper.SignIn(manager, user, RememberMe.Checked);
				Session.Add("user", user.DBUser);
				if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
					IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
				else
					Response.Redirect("~/Site/Default.aspx");
			}
			else
			{
				FailureText.Text = "Invalid username or password.";
				ErrorMessage.Visible = true;
			}
		}
	}
}
