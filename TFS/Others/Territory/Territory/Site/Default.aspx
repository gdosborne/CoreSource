<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInternal.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Site_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="minitron">
        <h2>What would you like to do</h2>
    </div>
    <div style="padding-top: 30px;padding-left:30px;font-size:16pt;">
        <a href="CheckOut.aspx">I would like to check out a territory</a>
    </div>
    <div style="padding-top: 30px;padding-left:30px;font-size:16pt;">
        <a href="CheckIn.aspx">I would like to check in a territory</a>
    </div>
    <div style="padding-top: 30px;padding-left:30px;font-size:16pt;">
        <a href="NeedsCheckout.aspx">I would like to see territories that need to be checked out</a>
    </div>
</asp:Content>

