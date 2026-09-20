<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Members.aspx.cs" Inherits="Members" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
.page-title { margin-top: 0; font-weight: bold; }
.form-panel, .grid-panel { background: #fff; padding: 20px; border-radius: 8px; margin-bottom: 25px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); }
.section-title { margin-top: 0; margin-bottom: 20px; font-weight: bold; }
.required { color: red; }
.action-buttons { margin-top: 25px; }
.search-box { margin-bottom: 20px; }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2 class="page-title">Member Management</h2>
<p class="text-muted">Register and manage Bachat Gat members.</p>

<div class="form-panel">

<h4 class="section-title">Member Registration</h4>

<asp:HiddenField ID="hfMemberID" runat="server" />

<div class="row">

<div class="col-md-4">
<div class="form-group">
<label>Bachat Gat <span class="required">*</span></label>
<asp:DropDownList ID="ddlBachatGat" runat="server" CssClass="form-control"></asp:DropDownList>
</div>
</div>

<div class="col-md-8">
<div class="form-group">
<label>Member Name <span class="required">*</span></label>
<asp:TextBox ID="txtMemberName" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div>

</div>

<div class="row">

<div class="col-md-4">
<div class="form-group">
<label>Father / Husband Name</label>
<asp:TextBox ID="txtFatherOrHusbandName" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label>Date of Birth</label>
<asp:TextBox ID="txtDateOfBirth" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label>Mobile</label>
<asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div>

</div>

<div class="row">

<div class="col-md-4">
<div class="form-group">
<label>Email</label>
<asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label>Village</label>
<asp:TextBox ID="txtVillage" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label>Taluka</label>
<asp:TextBox ID="txtTaluka" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div>

</div>

<div class="row">

<div class="col-md-4">
<div class="form-group">
<label>District</label>
<asp:TextBox ID="txtDistrict" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label>Join Date <span class="required">*</span></label>
<asp:TextBox ID="txtJoinDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label>Occupation</label>
<asp:TextBox ID="txtOccupation" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div>

</div>

<div class="row">

<div class="col-md-12">
<div class="form-group">
<label>Address</label>
<asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
</div>
</div>

</div>

<div class="row">

<div class="col-md-4">
<div class="form-group">
<label>Status</label>
<asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
<asp:ListItem Text="Active" Value="Active"></asp:ListItem>
<asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
</asp:DropDownList>
</div>
</div>

</div>

<div class="action-buttons">

<asp:Button ID="btnSave" runat="server" Text="Save Member" CssClass="btn btn-primary" OnClick="btnSave_Click" />

<asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-default" OnClick="btnClear_Click" />

<asp:Label ID="lblMessage" runat="server" CssClass="text-success" Style="margin-left:15px;"></asp:Label>

</div>

</div>

<div class="grid-panel">

<h4 class="section-title">Member List</h4>

<div class="search-box">

<div class="row">

<div class="col-md-4">
<div class="form-group">
<label>Search Member</label>
<asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search member name, mobile or village"></asp:TextBox>
</div>
</div>

<div class="col-md-3">
<div class="form-group">
<label>Filter by Bachat Gat</label>
<asp:DropDownList ID="ddlFilterBachatGat" runat="server" CssClass="form-control">
</asp:DropDownList>
</div>
</div>

<div class="col-md-3">
<div class="form-group">
<label>Filter by Village</label>
<asp:DropDownList ID="ddlFilterVillage" runat="server" CssClass="form-control">
</asp:DropDownList>
</div>
</div>

<div class="col-md-2">
<div class="form-group">
<label>&nbsp;</label>
<div>

<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" OnClick="btnSearch_Click" />

<asp:Button ID="btnShowAll" runat="server" Text="Clear" CssClass="btn btn-default" OnClick="btnShowAll_Click" />

</div>
</div>
</div>

</div>

</div>

<asp:GridView ID="gvMembers" runat="server"
AutoGenerateColumns="False"
CssClass="table table-bordered table-striped"
DataKeyNames="MemberID"
OnRowCommand="gvMembers_RowCommand">

<Columns>

<asp:BoundField DataField="MemberID" HeaderText="ID" />

<asp:BoundField DataField="MemberName" HeaderText="Member Name" />

<asp:BoundField DataField="GatName" HeaderText="Bachat Gat" />

<asp:BoundField DataField="Mobile" HeaderText="Mobile" />

<asp:BoundField DataField="Village" HeaderText="Village" />

<asp:BoundField DataField="Status" HeaderText="Status" />

<asp:TemplateField HeaderText="Action">

<ItemTemplate>

<asp:LinkButton ID="btnEdit" runat="server"
CommandName="EditMember"
CommandArgument='<%# Eval("MemberID") %>'
CssClass="btn btn-primary btn-sm">
Edit
</asp:LinkButton>

<asp:LinkButton ID="btnDelete" runat="server"
CommandName="DeleteMember"
CommandArgument='<%# Eval("MemberID") %>'
CssClass="btn btn-danger btn-sm"
OnClientClick="return confirm('Are you sure you want to deactivate this member?');">
Delete
</asp:LinkButton>

</ItemTemplate>

</asp:TemplateField>

</Columns>

<EmptyDataTemplate>
<div class="alert alert-info">
No members found.
</div>
</EmptyDataTemplate>

</asp:GridView>

</div>

</asp:Content>