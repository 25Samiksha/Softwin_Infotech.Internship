<%@ Page Title="User Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="container-fluid">
<div class="row">
<div class="col-md-12">
<h2><span class="glyphicon glyphicon-user"></span> User Management</h2>
<hr />
</div>
</div>

<div class="row">
<div class="col-md-12">
<asp:Label ID="lblMessage" runat="server" CssClass="alert" Visible="false"></asp:Label>
</div>
</div>

<div class="row">
<div class="col-md-12">
<div class="panel panel-primary">
<div class="panel-heading"><h4 class="panel-title">Add / Edit User</h4></div>
<div class="panel-body">
<asp:HiddenField ID="hfUserID" runat="server" />

<div class="row">
<div class="col-md-4">
<div class="form-group">
<label>Username</label>
<asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter username"></asp:TextBox>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label>Password</label>
<asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter password"></asp:TextBox>
<small class="text-muted">Leave blank while editing to keep old password.</small>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label>Full Name</label>
<asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Enter full name"></asp:TextBox>
</div>
</div>
</div>

<div class="row">
<div class="col-md-4">
<div class="form-group">
<label>Role</label>
<asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
<asp:ListItem Text="-- Select Role --" Value=""></asp:ListItem>
<asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
<asp:ListItem Text="President" Value="President"></asp:ListItem>
<asp:ListItem Text="Secretary" Value="Secretary"></asp:ListItem>
<asp:ListItem Text="Member" Value="Member"></asp:ListItem>
</asp:DropDownList>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label>Mobile</label>
<asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" MaxLength="15" placeholder="Enter mobile number"></asp:TextBox>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label>Email</label>
<asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter email"></asp:TextBox>
</div>
</div>
</div>

<div class="row">
<div class="col-md-4">
<div class="form-group">
<label>Status</label>
<asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
<asp:ListItem Text="Active" Value="1"></asp:ListItem>
<asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
</asp:DropDownList>
</div>
</div>
</div>

<div class="row">
<div class="col-md-12">
<asp:Button ID="btnSave" runat="server" Text="Save User" CssClass="btn btn-success" OnClick="btnSave_Click" />
&nbsp;
<asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-default" CausesValidation="false" OnClick="btnClear_Click" />
</div>
</div>
</div>
</div>
</div>
</div>

<div class="row">
<div class="col-md-12">
<div class="panel panel-default">
<div class="panel-heading"><h4 class="panel-title">Search Users</h4></div>
<div class="panel-body">
<div class="row">
<div class="col-md-6">
<asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by username, name or role"></asp:TextBox>
</div>
<div class="col-md-6">
<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnSearch_Click" />
&nbsp;
<asp:Button ID="btnShowAll" runat="server" Text="Show All" CssClass="btn btn-default" CausesValidation="false" OnClick="btnShowAll_Click" />
</div>
</div>
</div>
</div>
</div>
</div>

<div class="row">
<div class="col-md-12">
<div class="panel panel-info">
<div class="panel-heading"><h4 class="panel-title">Registered Users</h4></div>
<div class="panel-body">
<div class="table-responsive">
<asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover" DataKeyNames="UserID" OnRowCommand="gvUsers_RowCommand" EmptyDataText="No users found.">
<Columns>
<asp:BoundField DataField="UserID" HeaderText="ID" />
<asp:BoundField DataField="Username" HeaderText="Username" />
<asp:BoundField DataField="FullName" HeaderText="Full Name" />
<asp:BoundField DataField="Role" HeaderText="Role" />
<asp:BoundField DataField="Mobile" HeaderText="Mobile" />
<asp:BoundField DataField="Email" HeaderText="Email" />
<asp:TemplateField HeaderText="Status">
<ItemTemplate>
<asp:Label ID="lblStatus" runat="server" Text='<%# Convert.ToBoolean(Eval("IsActive")) ? "Active" : "Inactive" %>' CssClass='<%# Convert.ToBoolean(Eval("IsActive")) ? "label label-success" : "label label-danger" %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:dd-MM-yyyy}" />
<asp:TemplateField HeaderText="Action">
<ItemTemplate>
<asp:LinkButton ID="btnEdit" runat="server" CommandName="EditUser" CommandArgument='<%# Eval("UserID") %>' CssClass="btn btn-primary btn-xs"><span class="glyphicon glyphicon-edit"></span> Edit</asp:LinkButton>
&nbsp;
<asp:LinkButton ID="btnToggle" runat="server" CommandName="ToggleUser" CommandArgument='<%# Eval("UserID") %>' CssClass="btn btn-warning btn-xs"><span class="glyphicon glyphicon-refresh"></span> Activate / Deactivate</asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>
</div>
</div>
</div>
</div>
</div>
</div>
</div>
</asp:Content>