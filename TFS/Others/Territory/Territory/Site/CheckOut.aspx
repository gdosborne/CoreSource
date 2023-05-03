<%@ Page Title="Check out territory" Language="C#" MasterPageFile="~/SiteInternal.master" AutoEventWireup="true" CodeFile="CheckOut.aspx.cs" Inherits="Site_CheckOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="minitron">
        <h2>Check out territory</h2>
    </div>
    <div style="padding-top: 30px;padding-left:30px;font-size:16pt">
        <asp:Label runat="server" ID="AreaLabel" AssociatedControlID="AreaRadioButtonList">Area</asp:Label>        
        <asp:RadioButtonList runat="server" ID="AreaRadioButtonList" AutoPostBack="true" />
        <asp:Label runat="server" ID="TerritoryLabel" AssociatedControlID="TerritoryRadioButtonList" Visible="false">Territory</asp:Label>
        <asp:RadioButtonList runat="server" ID="TerritoryRadioButtonList" AutoPostBack="true" Visible="false" />
        <asp:Label runat="server" ID="PublishersLabel" AssociatedControlID="PublisherRadioButtonList" Visible="false">Publisher</asp:Label>
        <asp:RadioButtonList runat="server" ID="PublisherRadioButtonList" AutoPostBack="true" Visible="false" />
        <asp:Label runat="server" ID="EMailAddressLabel" AssociatedControlID="EMailAddressTextBox" Visible="false">EMail Address</asp:Label>
        <asp:TextBox runat="server" ID="EMailAddressTextBox" Visible="false" />
        <asp:Button runat="server" Text="Check Out" ID="CheckOutButton" Visible="false" OnClick="CheckOutButton_Click" /><br />
        <asp:Image runat="server" ID="TerritoryImage" Visible="false" AlternateText="Territory Image" />
    </div>
</asp:Content>

