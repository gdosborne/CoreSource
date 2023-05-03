using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.UI;
using Territory;
using Models;
public partial class Account_Register : Page
{
	protected void CreateUser_Click(object sender, EventArgs e)
	{
		if (NewCongregation.Checked)
		{
			return;
		}
		var dataContext = new TerritoryDataContext();
		try
		{
			var manager = new UserManager(dataContext, CongregationKey.Text.ToUpper(), RoleKey.Text.ToUpper(), Password.Text, FirstName.Text, LastName.Text);
			var user = new ApplicationUser() { Email = UserName.Text };
			IdentityResult result = manager.Create(user, Password.Text);
			if (result.Succeeded)
			{
				IdentityHelper.SignIn(manager, user, isPersistent: false);
				Session.Add("user", user.DBUser);
				if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
					IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
				else
					Response.Redirect("~/Site/Default.aspx");
			}
			else
			{
				ErrorMessage.Text = result.Errors.FirstOrDefault();
			}
		}
		catch (Exception ex)
		{
		}
		finally
		{
			dataContext.Connection.Dispose();
		}
	}
	protected void NewCongregation_CheckedChanged(object sender, EventArgs e)
	{
		var cb = (sender as ICheckBoxControl);
		CongregationNamePanel.Visible = cb.Checked;
		CongregationNumberPanel.Visible = cb.Checked;
		CongregationCityStatePanel.Visible = cb.Checked;
		ExplanationPanel.Visible = cb.Checked;
		CongregationKeyPanel.Visible = !cb.Checked;
		RoleKeyPanel.Visible = !cb.Checked;
		KeyExplanationPanel.Visible = !cb.Checked;
	}
}
