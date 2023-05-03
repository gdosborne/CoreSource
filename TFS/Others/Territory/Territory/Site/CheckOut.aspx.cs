using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
public partial class Site_CheckOut : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (Session["user"] == null)
			Response.Redirect("~/Default.aspx");
		var areas = Query.GetAreas().ToList();
		areas.ForEach(x =>
		{
			AreaRadioButtonList.Items.Add(new ListItem("&nbsp;" + x.Value, x.ID.ToString()));
		});
		AreaRadioButtonList.SelectedIndexChanged += AreaRadioButtonList_SelectedIndexChanged;
		PublisherRadioButtonList.SelectedIndexChanged += PublisherRadioButtonList_SelectedIndexChanged;
		TerritoryRadioButtonList.SelectedIndexChanged += TerritoryRadioButtonList_SelectedIndexChanged;
	}
	void TerritoryRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
	{
		var publishers = Query.GetPublishers().OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
		publishers.ForEach(x =>
		{
			PublisherRadioButtonList.Items.Add(new ListItem("&nbsp;" + x.LastName + ",&nbsp;" + x.FirstName, x.ID.ToString()));
		});
		TerritoryLabel.Visible = false;
		TerritoryRadioButtonList.Visible = false;
		PublishersLabel.Visible = true;
		PublisherRadioButtonList.Visible = true;
	}
	void PublisherRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
	{
		var id = TerritoryRadioButtonList.SelectedValue;
		TerritoryImage.AlternateText = string.Format("Territory {0} image missing", id);
		TerritoryImage.ImageUrl = string.Format("~/territories/{0}.png", id);
		PublishersLabel.Visible = false;
		PublisherRadioButtonList.Visible = false;
		EMailAddressLabel.Visible = true;
		EMailAddressTextBox.Visible = true;
		CheckOutButton.Visible = true;
		TerritoryImage.Visible = true;
	}
	protected void AreaRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
	{
		var territories = Query.GetTerritoriesNeedingWorkForArea(int.Parse(AreaRadioButtonList.SelectedValue))
			.OrderByDescending(x => x.NumberOfDaysSinceWorked).ToList();
		territories.ForEach(x =>
		{
			TerritoryRadioButtonList.Items.Add(new ListItem("&nbsp;" + x.Number + "&nbsp;(" + x.NumberOfDaysSinceWorked.ToString() + "&nbsp;days since worked)", x.ID.ToString()));
		});
		AreaLabel.Visible = false;
		AreaRadioButtonList.Visible = false;
		TerritoryLabel.Visible = true;
		TerritoryRadioButtonList.Visible = true;
	}
	protected void CheckOutButton_Click(object sender, EventArgs e)
	{
	}
}
