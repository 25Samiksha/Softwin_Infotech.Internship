<%@ Page Title="Government Schemes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Schemes.aspx.cs" Inherits="Schemes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="container-fluid">
<h2 class="page-title">Government Schemes</h2>
<asp:Label ID="lblMessage" runat="server"></asp:Label>
<asp:HiddenField ID="hfSchemeID" runat="server" />

<div class="panel panel-primary">
<div class="panel-heading">Scheme Information</div>
<div class="panel-body">
<div class="row">
<div class="col-md-6">
<label>Scheme Name</label>
<asp:TextBox ID="txtSchemeName" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="col-md-6">
<label>Department</label>
<asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div>
<br />
<div class="row">
<div class="col-md-4">
<label>Start Date</label>
<asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
</div>
<div class="col-md-4">
<label>End Date</label>
<asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
</div>
<div class="col-md-4">
<label>Status</label>
<asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
<asp:ListItem>Active</asp:ListItem>
<asp:ListItem>Inactive</asp:ListItem>
<asp:ListItem>Expired</asp:ListItem>
</asp:DropDownList>
</div>
</div>
<br />
<div class="row">
<div class="col-md-12">
<label>Description</label>
<asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
</div>
</div>
<br />
<div class="row">
<div class="col-md-12">
<label>Eligibility</label>
<asp:TextBox ID="txtEligibility" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
</div>
</div>
<br />
<div class="row">
<div class="col-md-12">
<label>Benefits</label>
<asp:TextBox ID="txtBenefits" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
</div>
</div>
<br />
<div class="row">
<div class="col-md-12">
<label>Required Documents</label>
<asp:TextBox ID="txtRequiredDocuments" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
</div>
</div>
<br />
<div class="row">
<div class="col-md-12">
<label>Application Process</label>
<asp:TextBox ID="txtApplicationProcess" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
</div>
</div>
<br />
<div class="row">
<div class="col-md-12">
<label>Official Website</label>
<asp:TextBox ID="txtOfficialWebsite" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div>
<br />
<asp:Button ID="btnSave" runat="server" Text="Save Scheme" CssClass="btn btn-success" OnClick="btnSave_Click" />
<asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-default" OnClick="btnClear_Click" />
</div>
</div>

<div class="panel panel-default">
<div class="panel-heading">Search Schemes</div>
<div class="panel-body">
<div class="row">
<div class="col-md-10">
<asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by scheme name or department"></asp:TextBox>
</div>
<div class="col-md-2">
<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
</div>
</div>
<br />
<asp:Button ID="btnShowAll" runat="server" Text="Show All" CssClass="btn btn-info" OnClick="btnShowAll_Click" />
</div>
</div>

<asp:GridView ID="gvSchemes" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" DataKeyNames="SchemeID" OnRowCommand="gvSchemes_RowCommand">
<Columns>
<asp:BoundField DataField="SchemeID" HeaderText="ID" />
<asp:BoundField DataField="SchemeName" HeaderText="Scheme Name" />
<asp:BoundField DataField="Department" HeaderText="Department" />
<asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:dd-MM-yyyy}" />
<asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:dd-MM-yyyy}" />
<asp:BoundField DataField="Status" HeaderText="Status" />
<asp:ButtonField ButtonType="Button" CommandName="EditScheme" Text="Edit" ControlStyle-CssClass="btn btn-warning btn-xs" />
<asp:ButtonField ButtonType="Button" CommandName="DeleteScheme" Text="Delete" ControlStyle-CssClass="btn btn-danger btn-xs" />
</Columns>
</asp:GridView>
</div>
</asp:Content>