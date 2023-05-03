<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Account_Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Create a new account.</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <asp:Panel runat="server" ID="NewCongregationPanel" Visible="false" CssClass="form-group">
            <div class="col-md-offset-2 col-md-10">
                <div class="checkbox">
                    <asp:CheckBox runat="server" ID="NewCongregation" OnCheckedChanged="NewCongregation_CheckedChanged" AutoPostBack="true" />
                    <asp:Label runat="server" AssociatedControlID="NewCongregation">I am requesting setup of a new congregation</asp:Label>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="CongregationNamePanel" Visible="false" CssClass="form-group">
            <asp:Label runat="server" AssociatedControlID="CongregationName" CssClass="col-md-2 control-label">Congregation name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="CongregationName" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="CongregationName"
                    CssClass="text-danger" ErrorMessage="The congregation name field is required." />
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="CongregationNumberPanel" Visible="false" CssClass="form-group">
            <asp:Label runat="server" AssociatedControlID="CongregationNumber" CssClass="col-md-2 control-label">Congregation number</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="CongregationNumber" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="CongregationNumber"
                    CssClass="text-danger" ErrorMessage="The congregation number field is required." />
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="CongregationCityStatePanel" Visible="false" CssClass="form-group">
            <asp:Label runat="server" AssociatedControlID="CongregationCityState" CssClass="col-md-2 control-label">City and state</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="CongregationCityState" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="CongregationCityState"
                    CssClass="text-danger" ErrorMessage="The city and state field is required." />
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="ExplanationPanel" Visible="false" CssClass="form-group">
            <p>Your user will not created immediately. Your congregation will need to be verified before this congregation
                and user will be created. As requester, you will automatically be assigned the role of coordinator once the congregation
                has been set up.
            </p>
        </asp:Panel>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-2 control-label">Email address</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="UserName" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                    CssClass="text-danger" ErrorMessage="The user name field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="FirstName" CssClass="col-md-2 control-label">First name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="FirstName" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="FirstName"
                    CssClass="text-danger" ErrorMessage="The first name field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="LastName" CssClass="col-md-2 control-label">Last name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="LastName" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="LastName"
                    CssClass="text-danger" ErrorMessage="The last name field is required." />
            </div>
        </div>
        <asp:Panel runat="server" ID="CongregationKeyPanel" Visible="true" CssClass="form-group">
            <asp:Label runat="server" AssociatedControlID="CongregationKey" CssClass="col-md-2 control-label">Congregation Key</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="CongregationKey" CssClass="form-control" style="text-transform:uppercase" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="CongregationKey"
                    CssClass="text-danger" ErrorMessage="The congregation key field is required." />
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="RoleKeyPanel" Visible="true" CssClass="form-group">
            <asp:Label runat="server" AssociatedControlID="RoleKey" CssClass="col-md-2 control-label">Role Key</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="RoleKey" CssClass="form-control" style="text-transform:uppercase" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="RoleKey"
                    CssClass="text-danger" ErrorMessage="The role key field is required." />
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="KeyExplanationPanel" Visible="true" CssClass="form-group">
            <p>The congregation and role keys should have been given to you by the brother who set up the congregation. They 
                are required for registration.</p>
        </asp:Panel>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    CssClass="text-danger" ErrorMessage="The password field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Confirm password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>

