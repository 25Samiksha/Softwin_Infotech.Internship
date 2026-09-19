<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
.dashboard-title { margin-top: 0; margin-bottom: 5px; font-weight: bold; color: #333; }
.dashboard-subtitle { color: #777; margin-bottom: 25px; }
.dashboard-card { background: #ffffff; border-radius: 10px; padding: 20px; margin-bottom: 20px; box-shadow: 0 2px 10px rgba(0,0,0,0.08); min-height: 130px; }
.dashboard-card .icon { font-size: 35px; margin-bottom: 10px; }
.dashboard-card h3 { margin: 5px 0; font-size: 28px; font-weight: bold; }
.dashboard-card p { margin: 0; color: #777; }
.card-blue .icon { color: #337ab7; }
.card-green .icon { color: #5cb85c; }
.card-orange .icon { color: #f0ad4e; }
.card-red .icon { color: #d9534f; }
.card-purple .icon { color: #8e44ad; }
.card-teal .icon { color: #16a085; }
.dashboard-section { background: #ffffff; border-radius: 10px; padding: 20px; margin-top: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.08); }
.dashboard-section h4 { margin-top: 0; font-weight: bold; border-bottom: 1px solid #eeeeee; padding-bottom: 12px; }
.quick-link { display: block; padding: 15px; margin-bottom: 10px; border: 1px solid #eeeeee; border-radius: 7px; color: #444444; text-decoration: none; }
.quick-link:hover { background: #f7f9fb; text-decoration: none; }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2 class="dashboard-title">Dashboard</h2>
<p class="dashboard-subtitle">Welcome to Mahila Bachat Gat Management Portal</p>

<div class="row">
<div class="col-md-4 col-sm-6">
<div class="dashboard-card card-blue">
<div class="icon"><span class="glyphicon glyphicon-home"></span></div>
<h3><asp:Label ID="lblTotalGats" runat="server" Text="0"></asp:Label></h3>
<p>Total Bachat Gat</p>
</div>
</div>

<div class="col-md-4 col-sm-6">
<div class="dashboard-card card-green">
<div class="icon"><span class="glyphicon glyphicon-user"></span></div>
<h3><asp:Label ID="lblTotalMembers" runat="server" Text="0"></asp:Label></h3>
<p>Total Members</p>
</div>
</div>

<div class="col-md-4 col-sm-6">
<div class="dashboard-card card-orange">
<div class="icon"><span class="glyphicon glyphicon-credit-card"></span></div>
<h3>₹<asp:Label ID="lblTotalSavings" runat="server" Text="0.00"></asp:Label></h3>
<p>Total Savings</p>
</div>
</div>

<div class="col-md-4 col-sm-6">
<div class="dashboard-card card-red">
<div class="icon"><span class="glyphicon glyphicon-usd"></span></div>
<h3>₹<asp:Label ID="lblTotalLoans" runat="server" Text="0.00"></asp:Label></h3>
<p>Total Loans</p>
</div>
</div>

<div class="col-md-4 col-sm-6">
<div class="dashboard-card card-purple">
<div class="icon"><span class="glyphicon glyphicon-time"></span></div>
<h3><asp:Label ID="lblPendingLoans" runat="server" Text="0"></asp:Label></h3>
<p>Pending Loans</p>
</div>
</div>

<div class="col-md-4 col-sm-6">
<div class="dashboard-card card-teal">
<div class="icon"><span class="glyphicon glyphicon-list-alt"></span></div>
<h3><asp:Label ID="lblActiveSchemes" runat="server" Text="0"></asp:Label></h3>
<p>Active Schemes</p>
</div>
</div>
</div>

<div class="row">
<div class="col-md-6">
<div class="dashboard-section">
<h4>Quick Actions</h4>
<a href="Members.aspx" class="quick-link"><span class="glyphicon glyphicon-user"></span>&nbsp; Add / Manage Members</a>
<a href="Savings.aspx" class="quick-link"><span class="glyphicon glyphicon-credit-card"></span>&nbsp; Record Monthly Savings</a>
<a href="Loans.aspx" class="quick-link"><span class="glyphicon glyphicon-usd"></span>&nbsp; Manage Loans</a>
<a href="Meetings.aspx" class="quick-link"><span class="glyphicon glyphicon-calendar"></span>&nbsp; Manage Meetings</a>
</div>
</div>

<div class="col-md-6">
<div class="dashboard-section">
<h4>Recent Activities</h4>
<p><span class="glyphicon glyphicon-ok"></span>&nbsp; New member registered</p>
<hr />
<p><span class="glyphicon glyphicon-credit-card"></span>&nbsp; Monthly savings collected</p>
<hr />
<p><span class="glyphicon glyphicon-usd"></span>&nbsp; New loan application received</p>
<hr />
<p><span class="glyphicon glyphicon-calendar"></span>&nbsp; Monthly meeting recorded</p>
</div>
</div>
</div>
</asp:Content>