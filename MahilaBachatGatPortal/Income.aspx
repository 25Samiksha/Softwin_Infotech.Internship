<%@ Page Title="Income Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Income.aspx.cs" Inherits="Income" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
.summary-card { padding: 20px; border-radius: 6px; margin-bottom: 20px; color: white; }
.summary-income { background: #28a745; }
.summary-records { background: #337ab7; }
.summary-month { background: #f0ad4e; }
.form-section { background: #ffffff; padding: 20px; border-radius: 6px; margin-bottom: 20px; box-shadow: 0 1px 5px rgba(0,0,0,0.1); }
.table-section { background: #ffffff; padding: 20px; border-radius: 6px; box-shadow: 0 1px 5px rgba(0,0,0,0.1); }
.amount { font-size: 24px; font-weight: bold; }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2><span class="glyphicon glyphicon-plus"></span> Income Management</h2>
<hr />

<div class="row">
<div class="col-md-4">
<div class="summary-card summary-income">
<h4>Total Income</h4>
<div class="amount">₹ <asp:Label ID="lblTotalIncome" runat="server" Text="0.00"></asp:Label></div>
</div>
</div>

<div class="col-md-4">
<div class="summary-card summary-records">
<h4>Total Records</h4>
<div class="amount"><asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label></div>
</div>
</div>

<div class="col-md-4">
<div class="summary-card summary-month">
<h4>Current Month Income</h4>
<div class="amount">₹ <asp:Label ID="lblCurrentMonthIncome" runat="server" Text="0.00"></asp:Label></div>
</div>
</div>
</div>

<asp:Label ID="lblMessage" runat="server" CssClass="alert" Visible="false"></asp:Label>

<div class="form-section">
<h3>Income Entry</h3>
<hr />
<asp:HiddenField ID="hfIncomeID" runat="server" />

<div class="row">
<div class="col-md-6">
<div class="form-group">
<label>Bachat Gat</label>
<asp:DropDownList ID="ddlBachatGat" runat="server" CssClass="form-control"></asp:DropDownList>
</div>
</div>

<div class="col-md-6">
<div class="form-group">
<label>Income Date</label>
<asp:TextBox ID="txtIncomeDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
</div>
</div>
</div>

<div class="row">
<div class="col-md-6">
<div class="form-group">
<label>Income Type</label>
<asp:DropDownList ID="ddlIncomeType" runat="server" CssClass="form-control">
<asp:ListItem Text="-- Select Income Type --" Value=""></asp:ListItem>
<asp:ListItem Text="Product Sales" Value="Product Sales"></asp:ListItem>
<asp:ListItem Text="Membership Fee" Value="Membership Fee"></asp:ListItem>
<asp:ListItem Text="Interest Received" Value="Interest Received"></asp:ListItem>
<asp:ListItem Text="Government Grant" Value="Government Grant"></asp:ListItem>
<asp:ListItem Text="Donation" Value="Donation"></asp:ListItem>
<asp:ListItem Text="Other" Value="Other"></asp:ListItem>
</asp:DropDownList>
</div>
</div>

<div class="col-md-6">
<div class="form-group">
<label>Amount</label>
<asp:TextBox ID="txtAmount" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div>
</div>

<div class="row">
<div class="col-md-6">
<div class="form-group">
<label>Description</label>
<asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
</div>
</div>

<div class="col-md-6">
<div class="form-group">
<label>Received From</label>
<asp:TextBox ID="txtReceivedFrom" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div>
</div>

<div class="row">
<div class="col-md-6">
<div class="form-group">
<label>Payment Mode</label>
<asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="form-control">
<asp:ListItem Text="-- Select Payment Mode --" Value=""></asp:ListItem>
<asp:ListItem Text="Cash" Value="Cash"></asp:ListItem>
<asp:ListItem Text="UPI" Value="UPI"></asp:ListItem>
<asp:ListItem Text="Bank" Value="Bank"></asp:ListItem>
<asp:ListItem Text="Cheque" Value="Cheque"></asp:ListItem>
</asp:DropDownList>
</div>
</div>

<div class="col-md-6">
<div class="form-group">
<label>Receipt Number</label>
<asp:TextBox ID="txtReceiptNumber" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div>
</div>

<div class="form-group">
<label>Remarks</label>
<asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
</div>

<div class="form-group">
<asp:Button ID="btnSave" runat="server" Text="Save Income" CssClass="btn btn-success" OnClick="btnSave_Click" />
&nbsp;
<asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-default" OnClick="btnClear_Click" />
</div>
</div>

<div class="form-section">
<h3>Search Income</h3>
<div class="row">
<div class="col-md-8">
<asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by income type, received from or receipt number"></asp:TextBox>
</div>

<div class="col-md-2">
<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary btn-block" OnClick="btnSearch_Click" />
</div>

<div class="col-md-2">
<asp:Button ID="btnShowAll" runat="server" Text="Show All" CssClass="btn btn-default btn-block" OnClick="btnShowAll_Click" />
</div>
</div>
</div>

<div class="table-section">
<h3>Income History</h3>
<hr />
<div class="table-responsive">
<asp:GridView ID="gvIncome" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover" DataKeyNames="IncomeID" OnRowCommand="gvIncome_RowCommand">
<Columns>
<asp:BoundField DataField="IncomeID" HeaderText="ID" />
<asp:BoundField DataField="GatName" HeaderText="Bachat Gat" />
<asp:BoundField DataField="IncomeDate" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" />
<asp:BoundField DataField="IncomeType" HeaderText="Income Type" />
<asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="₹ {0:N2}" />
<asp:BoundField DataField="ReceivedFrom" HeaderText="Received From" />
<asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode" />
<asp:BoundField DataField="ReceiptNumber" HeaderText="Receipt No." />
<asp:TemplateField HeaderText="Action">
<ItemTemplate>
<asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteIncome" CommandArgument='<%# Eval("IncomeID") %>' CssClass="btn btn-danger btn-xs" OnClientClick="return confirm('Are you sure you want to delete this income entry?');"><span class="glyphicon glyphicon-trash"></span> Delete</asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>
</div>
</div>
</asp:Content>