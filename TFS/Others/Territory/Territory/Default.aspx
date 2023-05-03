<%@ Page Title="Territories Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Congregation Territories</h1>
        <p class="lead">Use this application to check out/in territories, manage territories and publishers, and report on the status of territories.</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>If your congregation is not already registered, request that a ministerial servant or an elder register 
                your congregation for this application.</p>
            <p>Once your congregation is registered and the congregation receives their unique application and role keys, you can 
                register to check out or manage territories.</p>
            <p>
                <a class="btn btn-default" href="Account/Register.aspx">Register &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Login</h2>
            <p>Login to the application to begin working with territories.</p>
            <p>As a <span style="font-style:italic;">publisher or pioneer</span>, you can see what territories need to be worked, request a territory to work, or 
                just let the system choose a territory for you to work. Don't worry. The application will choose for you 
                a territory that needs working.</p>
            <p>As an <span style="font-style:italic;">elder or ministerial servant</span>, you can see requests, check out a territory to a publisher, and run
                reports to see the status of territories that have been or need to to be worked.
            </p>
            <p>
                <a class="btn btn-default" href="Account/Login.aspx">Login &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Reporting</h2>
            <p>It's all about reporting. Territories needing to be worked, how long it's been since a territory was worked, and who
                was the last publisher to work the territory. You will find all of it here!
            </p>
            <p>
                <a class="btn btn-default" href="Account/Manage.aspx">Reporting &raquo;</a>
            </p>
        </div>
    </div>
</asp:Content>
