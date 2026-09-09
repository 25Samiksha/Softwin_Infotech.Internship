<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="container">

<div class="page-header">
<h2>Dashboard</h2>
<ol class="breadcrumb">
<li class="active">Home</li>
</ol>
</div>

<div class="row">

<div class="col-md-3 col-sm-6">
<div class="panel panel-primary">
<div class="panel-heading">Total Employees</div>
<div class="panel-body text-center">
<h2><asp:Label ID="lblTotalEmployees" runat="server" Text="0"></asp:Label></h2>
</div>
</div>
</div>

<div class="col-md-3 col-sm-6">
<div class="panel panel-success">
<div class="panel-heading">Active Employees</div>
<div class="panel-body text-center">
<h2><asp:Label ID="lblActiveEmployees" runat="server" Text="0"></asp:Label></h2>
</div>
</div>
</div>

<div class="col-md-3 col-sm-6">
<div class="panel panel-danger">
<div class="panel-heading">Inactive Employees</div>
<div class="panel-body text-center">
<h2><asp:Label ID="lblInactiveEmployees" runat="server" Text="0"></asp:Label></h2>
</div>
</div>
</div>

<div class="col-md-3 col-sm-6">
<div class="panel panel-info">
<div class="panel-heading">Departments</div>
<div class="panel-body text-center">
<h2><asp:Label ID="lblDepartments" runat="server" Text="0"></asp:Label></h2>
</div>
</div>
</div>

</div>

</div>

</asp:Content>