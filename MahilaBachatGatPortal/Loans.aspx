<%@ Page Title="Loan Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Loans.aspx.cs" Inherits="Loans" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="container-fluid">

<h2>Loan Management</h2>
<hr />

<asp:Label ID="lblMessage" runat="server" CssClass="alert" Visible="false"></asp:Label>

<div class="row">

<div class="col-md-3">
<div class="panel panel-primary">
<div class="panel-heading">Total Loans</div>
<div class="panel-body">
<h3><asp:Label ID="lblTotalLoans" runat="server" Text="0"></asp:Label></h3>
</div>
</div>
</div>

<div class="col-md-3">
<div class="panel panel-warning">
<div class="panel-heading">Pending Loans</div>
<div class="panel-body">
<h3><asp:Label ID="lblPendingLoans" runat="server" Text="0"></asp:Label></h3>
</div>
</div>
</div>

<div class="col-md-3">
<div class="panel panel-success">
<div class="panel-heading">Approved Loans</div>
<div class="panel-body">
<h3><asp:Label ID="lblApprovedLoans" runat="server" Text="0"></asp:Label></h3>
</div>
</div>
</div>

<div class="col-md-3">
<div class="panel panel-info">
<div class="panel-heading">Total Loan Amount</div>
<div class="panel-body">
<h3><asp:Label ID="lblTotalLoanAmount" runat="server" Text="0.00"></asp:Label></h3>
</div>
</div>
</div>

</div>

<div class="panel panel-primary">

<div class="panel-heading">
<h4 class="panel-title">Loan Application</h4>
</div>

<div class="panel-body">

<asp:HiddenField ID="hfLoanID" runat="server" />

<div class="row">

<div class="col-md-6">
<div class="form-group">
<label>Bachat Gat</label>
<asp:DropDownList ID="ddlBachatGat" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBachatGat_SelectedIndexChanged"></asp:DropDownList>
</div>
</div>

<div class="col-md-6">
<div class="form-group">
<label>Member</label>
<asp:DropDownList ID="ddlMember" runat="server" CssClass="form-control"></asp:DropDownList>
</div>
</div>

</div>

<div class="row">

<div class="col-md-4">
<div class="form-group">
<label>Application Date</label>
<asp:TextBox ID="txtApplicationDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label>Loan Amount</label>
<asp:TextBox ID="txtLoanAmount" runat="server" CssClass="form-control" placeholder="Enter loan amount"></asp:TextBox>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label>Interest Rate (%)</label>
<asp:TextBox ID="txtInterestRate" runat="server" CssClass="form-control" placeholder="Enter interest rate"></asp:TextBox>
</div>
</div>

</div>

<div class="row">

<div class="col-md-4">
<div class="form-group">
<label>Loan Term (Months)</label>
<asp:TextBox ID="txtLoanTermMonths" runat="server" CssClass="form-control" placeholder="Enter months"></asp:TextBox>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label>EMI Amount</label>
<asp:TextBox ID="txtEMIAmount" runat="server" CssClass="form-control" placeholder="Enter EMI amount"></asp:TextBox>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label>Status</label>
<asp:DropDownList ID="ddlLoanStatus" runat="server" CssClass="form-control">
<asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
<asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
<asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
<asp:ListItem Text="Distributed" Value="Distributed"></asp:ListItem>
<asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
</asp:DropDownList>
</div>
</div>

</div>

<div class="form-group">
<label>Loan Purpose</label>
<asp:TextBox ID="txtLoanPurpose" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Enter purpose of loan"></asp:TextBox>
</div>

<div class="form-group">
<label>Remarks</label>
<asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" placeholder="Enter remarks"></asp:TextBox>
</div>

<asp:Button ID="btnSave" runat="server" Text="Submit Loan Application" CssClass="btn btn-success" OnClick="btnSave_Click" />

<asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-default" CausesValidation="false" OnClick="btnClear_Click" />

</div>
</div>

<div class="panel panel-info">

<div class="panel-heading">
<h4 class="panel-title">Search Loans</h4>
</div>

<div class="panel-body">

<div class="row">

<div class="col-md-6">
<asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search member name "></asp:TextBox>
</div>

<div class="col-md-3">
<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
</div>

<div class="col-md-3">
<asp:Button ID="btnShowAll" runat="server" Text="Show All" CssClass="btn btn-default" OnClick="btnShowAll_Click" />
</div>

</div>

</div>
</div>

<div class="panel panel-default">

<div class="panel-heading">
<h4 class="panel-title">Loan Applications</h4>
</div>

<div class="panel-body">

<div class="table-responsive">

<asp:GridView ID="gvLoans" runat="server"
AutoGenerateColumns="False"
CssClass="table table-bordered table-striped table-hover"
DataKeyNames="LoanID"
OnRowCommand="gvLoans_RowCommand">

<Columns>

<asp:BoundField DataField="LoanID" HeaderText="ID" />

<asp:BoundField DataField="MemberName" HeaderText="Member Name" />

<asp:BoundField DataField="GatName" HeaderText="Bachat Gat" />

<asp:BoundField DataField="ApplicationDate" HeaderText="Application Date" DataFormatString="{0:dd-MM-yyyy}" />

<asp:BoundField DataField="LoanAmount" HeaderText="Loan Amount" DataFormatString="₹ {0:N2}" />

<asp:BoundField DataField="ApprovedAmount" HeaderText="Approved Amount" DataFormatString="₹ {0:N2}" />

<asp:BoundField DataField="InterestRate" HeaderText="Interest %" />

<asp:BoundField DataField="LoanTermMonths" HeaderText="Term" />

<asp:BoundField DataField="Status" HeaderText="Status" />
</Columns>

<EmptyDataTemplate>
<div class="alert alert-info">No loan records found.</div>
</EmptyDataTemplate>

</asp:GridView>

</div>

</div>
</div>

<asp:Panel ID="pnlLoanDistribution" runat="server" CssClass="panel panel-success">

<div class="panel-heading">
<h4 class="panel-title">Loan Distribution</h4>
</div>

<div class="panel-body">

<asp:HiddenField ID="hfDistributionLoanID" runat="server" />

<div class="row">

<div class="col-md-4">
<div class="form-group">
<label>Loan ID</label>
<asp:TextBox ID="txtDistributionLoanID" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label>Member</label>
<asp:TextBox ID="txtDistributionMember" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label>Approved Amount</label>
<asp:TextBox ID="txtApprovedAmount" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
</div>
</div>

</div>

<div class="row">

<div class="col-md-4">
<div class="form-group">
<label>Distribution Date</label>
<asp:TextBox ID="txtDistributionDate" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label>Distribution Amount</label>
<asp:TextBox ID="txtDistributionAmount" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div>

</div>

<div class="form-group">
<label>Distribution Remarks</label>
<asp:TextBox ID="txtDistributionRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
</div>

<asp:Button ID="btnDistributeLoan"
runat="server"
Text="Confirm Distribution"
CssClass="btn btn-success"
OnClick="btnDistributeLoan_Click" />

<asp:Button ID="btnClearDistribution"
runat="server"
Text="Clear"
CssClass="btn btn-default"
CausesValidation="false"
OnClick="btnClearDistribution_Click" />

</div>

</asp:Panel>

</div>

</asp:Content>