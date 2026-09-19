<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Savings.aspx.cs" Inherits="Savings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
.page-title { margin-top: 0; font-weight: bold; }
.form-panel, .grid-panel, .summary-panel { background: #ffffff; padding: 20px; border-radius: 8px; margin-bottom: 25px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); }
.section-title { margin-top: 0; margin-bottom: 20px; font-weight: bold; }
.required { color: red; }
.action-buttons { margin-top: 20px; }
.summary-box { text-align: center; padding: 15px; background: #f5f7fa; border-radius: 8px; }
.summary-box h3 { margin: 5px 0; font-weight: bold; }
.summary-box p { margin: 0; color: #777; }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2 class="page-title">Savings Management</h2>
<p class="text-muted">Record and manage monthly member savings.</p>

<div class="summary-panel">
<div class="row">
<div class="col-md-4">
<div class="summary-box">
<h3>₹<asp:Label ID="lblTotalSavings" runat="server" Text="0"></asp:Label></h3>
<p>Total Savings</p>
</div>
</div>
<div class="col-md-4">
<div class="summary-box">
<h3><asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label></h3>
<p>Total Savings Records</p>
</div>
</div>
<div class="col-md-4">
<div class="summary-box">
<h3>₹<asp:Label ID="lblCurrentMonth" runat="server" Text="0"></asp:Label></h3>
<p>Current Month Collection</p>
</div>
</div>
</div>
</div>

<div class="form-panel">
<h4 class="section-title">Monthly Savings Entry</h4>

<div class="row">
<div class="col-md-4">
<div class="form-group">
<label>Bachat Gat <span class="required">*</span></label>
<asp:DropDownList ID="ddlBachatGat" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBachatGat_SelectedIndexChanged"></asp:DropDownList>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label>Member <span class="required">*</span></label>
<asp:DropDownList ID="ddlMember" runat="server" CssClass="form-control"></asp:DropDownList>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label>Saving Month <span class="required">*</span></label>
<asp:TextBox ID="txtSavingMonth" runat="server" TextMode="SingleLine" CssClass="form-control"></asp:TextBox>
</div>
</div>
</div>

<div class="row">
<div class="col-md-4">
<div class="form-group">
<label>Amount <span class="required">*</span></label>
<asp:TextBox ID="txtAmount" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label>Payment Mode</label>
<asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="form-control">
<asp:ListItem Text="Cash" Value="Cash"></asp:ListItem>
<asp:ListItem Text="UPI" Value="UPI"></asp:ListItem>
<asp:ListItem Text="Bank" Value="Bank"></asp:ListItem>
<asp:ListItem Text="Cheque" Value="Cheque"></asp:ListItem>
</asp:DropDownList>
</div>
</div>
<div class="col-md-4">
<div class="form-group">
<label>Payment Date</label>
<asp:TextBox ID="txtPaymentDate" runat="server" TextMode="SingleLine" CssClass="form-control"></asp:TextBox>
</div>
</div>
</div>

<div class="row">
<div class="col-md-4">
<div class="form-group">
<label>Receipt Number</label>
<asp:TextBox ID="txtReceiptNumber" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div>
<div class="col-md-8">
<div class="form-group">
<label>Remarks</label>
<asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control"></asp:TextBox>
</div>
</div>
</div>

<div class="action-buttons">
<asp:Button ID="btnSave" runat="server" Text="Save Savings" CssClass="btn btn-primary" OnClick="btnSave_Click" />
&nbsp;
<asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-default" OnClick="btnClear_Click" />
</div>

<br />
<asp:Label ID="lblMessage" runat="server" Font-Bold="true"></asp:Label>
</div>

<div class="grid-panel">
<h4 class="section-title">Savings Collection History</h4>

<div class="row">
<div class="col-md-5">
<div class="form-group">
<asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by Member Name, Code or Receipt Number"></asp:TextBox>
</div>
</div>
<div class="col-md-2">
<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" OnClick="btnSearch_Click" />
</div>
<div class="col-md-2">
<asp:Button ID="btnShowAll" runat="server" Text="Show All" CssClass="btn btn-default" OnClick="btnShowAll_Click" />
</div>
</div>

<div class="table-responsive">
<asp:GridView ID="gvSavings" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" DataKeyNames="SavingID" OnRowCommand="gvSavings_RowCommand">
<Columns>
<asp:BoundField DataField="SavingID" HeaderText="ID" />
<asp:BoundField DataField="MemberCode" HeaderText="Member Code" />
<asp:BoundField DataField="MemberName" HeaderText="Member Name" />
<asp:BoundField DataField="GatName" HeaderText="Bachat Gat" />
<asp:BoundField DataField="SavingMonth" HeaderText="Saving Month" DataFormatString="{0:dd-MM-yyyy}" />
<asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="₹{0:N2}" />
<asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:dd-MM-yyyy}" />
<asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode" />
<asp:BoundField DataField="ReceiptNumber" HeaderText="Receipt No." />

<asp:TemplateField HeaderText="Action">
<ItemTemplate>
<asp:LinkButton ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger btn-xs" CommandName="DeleteSaving" CommandArgument='<%# Eval("SavingID") %>' OnClientClick="return confirm('Are you sure you want to delete this savings record?');"></asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>
</Columns>

<EmptyDataTemplate>No savings records found.</EmptyDataTemplate>
</asp:GridView>
</div>
</div>
</asp:Content>