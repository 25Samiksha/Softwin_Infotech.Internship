<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
.dashboard-title{
    margin-top:0;
    margin-bottom:5px;
    font-weight:700;
    color:#3D1F2D;
}
.dashboard-subtitle{
    color:#6F5A65;
    margin-bottom:25px;
}
.dashboard-card{
    background:#FFFFFF;
    border:1px solid #EDDBD2;
    border-radius:12px;
    padding:20px;
    margin-bottom:20px;
    box-shadow:0 3px 14px rgba(61,31,45,0.07);
    min-height:130px;
    position:relative;
    overflow:hidden;
    transition:all .2s ease;
}
.dashboard-card:hover{
    transform:translateY(-2px);
    box-shadow:0 6px 18px rgba(61,31,45,0.10);
}
.dashboard-card .icon{
    font-size:35px;
    margin-bottom:10px;
}
.dashboard-card h3{
    margin:5px 0;
    font-size:28px;
    font-weight:700;
    color:#3D1F2D;
}
.dashboard-card p{
    margin:0;
    color:#6F5A65;
    font-size:14px;
}
.card-blue{
    border-left:4px solid #C0395B;
}
.card-blue .icon{
    color:#C0395B;
}
.card-green{
    border-left:4px solid #67B56B;
}
.card-green .icon{
    color:#67B56B;
}
.card-orange{
    border-left:4px solid #E07A38;
}
.card-orange .icon{
    color:#E07A38;
}
.card-red{
    border-left:4px solid #C0395B;
}
.card-red .icon{
    color:#C0395B;
}
.card-purple{
    border-left:4px solid #9B59B6;
}
.card-purple .icon{
    color:#9B59B6;
}
.card-teal{
    border-left:4px solid #20A58A;
}
.card-teal .icon{
    color:#20A58A;
}
.dashboard-section{
    background:#FFFFFF;
    border:1px solid #EDDBD2;
    border-radius:12px;
    padding:20px;
    margin-top:10px;
    margin-bottom:20px;
    box-shadow:0 3px 14px rgba(61,31,45,0.07);
}
.dashboard-section h4{
    margin-top:0;
    color:#3D1F2D;
    font-weight:700;
    border-bottom:1px solid #EDDBD2;
    padding-bottom:12px;
}
.quick-link{
    display:block;
    padding:14px 15px;
    margin-bottom:10px;
    border:1px solid #EDDBD2;
    border-radius:8px;
    color:#5C3D4A;
    background:#FFFFFF;
    text-decoration:none;
    transition:all .2s ease;
}
.quick-link:hover{
    background:#FCEEF4;
    border-color:#E8B7C7;
    color:#C0395B;
    text-decoration:none;
}
.quick-link .glyphicon{
    color:#C0395B;
}
.order-table{
    width:100%;
    border-collapse:collapse;
    background:#FFFFFF;
}
.order-table th{
    background:#C0395B;
    color:#FFFFFF;
    padding:12px;
    text-align:left;
    font-weight:600;
}
.order-table td{
    padding:12px;
    border-bottom:1px solid #EDDBD2;
    color:#5C3D4A;
}
.order-table tr:hover{
    background:#FCEEF4;
}
.verify-button{
    background:#67B56B;
    color:#FFFFFF;
    border:none;
    padding:7px 14px;
    border-radius:5px;
    transition:all .2s ease;
}
.verify-button:hover{
    background:#559A59;
    color:#FFFFFF;
}
.order-status{
    font-weight:bold;
}
.pending-status{
    color:#E07A38;
}
.verified-status{
    color:#67B56B;
}
#<%= pnlPendingOrders.ClientID %>{
    margin-top:5px;
}
@media(max-width:767px){
    .dashboard-card{
        min-height:120px;
    }
    .dashboard-card h3{
        font-size:25px;
    }
    .dashboard-section{
        padding:16px;
    }
    .order-table{
        display:block;
        overflow-x:auto;
        white-space:nowrap;
    }
}
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

<a href="Members.aspx" class="quick-link">
<span class="glyphicon glyphicon-user"></span>&nbsp; Add / Manage Members
</a>

<a href="Savings.aspx" class="quick-link">
<span class="glyphicon glyphicon-credit-card"></span>&nbsp; Record Monthly Savings
</a>

<a href="Loans.aspx" class="quick-link">
<span class="glyphicon glyphicon-usd"></span>&nbsp; Manage Loans
</a>

<a href="Meetings.aspx" class="quick-link">
<span class="glyphicon glyphicon-calendar"></span>&nbsp; Manage Meetings
</a>

</div>
</div>

<div class="col-md-6">
<div class="dashboard-section">

<h4>Recent Activities</h4>

<p>
<span class="glyphicon glyphicon-ok" style="color:#C0395B;"></span>
&nbsp; New member registered
</p>

<hr />

<p>
<span class="glyphicon glyphicon-credit-card" style="color:#E07A38;"></span>
&nbsp; Monthly savings collected
</p>

<hr />

<p>
<span class="glyphicon glyphicon-usd" style="color:#67B56B;"></span>
&nbsp; New loan application received
</p>

<hr />

<p>
<span class="glyphicon glyphicon-calendar" style="color:#9B59B6;"></span>
&nbsp; Monthly meeting recorded
</p>

</div>
</div>

</div>

<asp:Panel ID="pnlPendingOrders" runat="server" Visible="false">

<div class="dashboard-section">

<h4>Pending Customer Orders</h4>

<asp:GridView ID="gvPendingOrders" runat="server"
    AutoGenerateColumns="False"
    CssClass="order-table"
    GridLines="None"
    OnRowCommand="gvPendingOrders_RowCommand"
    EmptyDataText="No pending orders found.">

<Columns>

<asp:BoundField DataField="OrderID" HeaderText="Order ID" />

<asp:BoundField DataField="CustomerName" HeaderText="Customer" />

<asp:BoundField DataField="ProductName" HeaderText="Product" />

<asp:BoundField DataField="Quantity" HeaderText="Quantity" />

<asp:BoundField
    DataField="TotalAmount"
    HeaderText="Amount"
    DataFormatString="₹ {0:N2}" />

<asp:BoundField
    DataField="PaymentMode"
    HeaderText="Payment" />

<asp:BoundField
    DataField="PaymentStatus"
    HeaderText="Payment Status" />

<asp:TemplateField HeaderText="Action">

<ItemTemplate>

<asp:Button ID="btnVerify"
    runat="server"
    Text="Verify Order"
    CommandName="VerifyOrder"
    CommandArgument='<%# Eval("OrderID") %>'
    CssClass="verify-button"
    OnClientClick="return confirm('Are you sure you want to verify this order?');" />

</ItemTemplate>

</asp:TemplateField>

</Columns>

</asp:GridView>

<asp:Label ID="lblOrderMessage" runat="server"></asp:Label>

</div>

</asp:Panel>

</asp:Content>