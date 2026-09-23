<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderReview.aspx.cs" Inherits="OrderReview" MasterPageFile="~/Public.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
.order-container {
    max-width: 900px;
    margin: 30px auto;
}
.order-panel {
    background: #fff;
    padding: 30px;
    border-radius: 8px;
    box-shadow: 0 2px 10px rgba(0,0,0,0.12);
}
.order-title {
    text-align: center;
    color: #2f6f9f;
    margin-bottom: 30px;
}
.section-box {
    border: 1px solid #e5e9ef;
    border-radius: 6px;
    padding: 20px;
    margin-bottom: 25px;
}
.section-title {
    color: #2f6f9f;
    font-weight: 600;
    margin-top: 0;
    margin-bottom: 20px;
}
.order-table {
    width: 100%;
    border-collapse: collapse;
}
.order-table th {
    background: #f8fafc;
    padding: 12px;
    border-bottom: 1px solid #e5e9ef;
    text-align: left;
}
.order-table td {
    padding: 12px;
    border-bottom: 1px solid #e5e9ef;
}
.total-box {
    background: #f8fafc;
    padding: 20px;
    border-radius: 6px;
    margin-bottom: 20px;
}
.total-label {
    font-size: 20px;
    font-weight: 600;
}
.total-amount {
    font-size: 22px;
    font-weight: 700;
    color: #2f6f9f;
}
.btn-payment {
    width: 100%;
    margin-top: 10px;
}
.message {
    display: block;
    text-align: center;
    margin-top: 15px;
}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="order-container">
<div class="order-panel">

<h2 class="order-title">Order Review</h2>

<div class="section-box">
<h4 class="section-title">Product Details</h4>

<asp:GridView
    ID="gvOrderItems"
    runat="server"
    AutoGenerateColumns="False"
    CssClass="order-table"
    GridLines="None">
    <Columns>
        <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
        <asp:BoundField DataField="GatName" HeaderText="Bachat Gat" />
        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
        <asp:BoundField DataField="SellingPrice" HeaderText="Price" DataFormatString="₹ {0:N2}" />
        <asp:BoundField DataField="ItemTotal" HeaderText="Total" DataFormatString="₹ {0:N2}" />
    </Columns>
</asp:GridView>

</div>

<div class="section-box">
<h4 class="section-title">Delivery Address</h4>

<p>
<strong>Name:</strong>
<asp:Label ID="lblFullName" runat="server"></asp:Label>
</p>

<p>
<strong>Mobile:</strong>
<asp:Label ID="lblMobile" runat="server"></asp:Label>
</p>

<p>
<strong>Address:</strong>
<asp:Label ID="lblAddress" runat="server"></asp:Label>
</p>

<p>
<strong>City / Village:</strong>
<asp:Label ID="lblCity" runat="server"></asp:Label>
</p>

<p>
<strong>Taluka:</strong>
<asp:Label ID="lblTaluka" runat="server"></asp:Label>
</p>

<p>
<strong>District:</strong>
<asp:Label ID="lblDistrict" runat="server"></asp:Label>
</p>

<p>
<strong>State:</strong>
<asp:Label ID="lblState" runat="server"></asp:Label>
</p>

<p>
<strong>PIN Code:</strong>
<asp:Label ID="lblPincode" runat="server"></asp:Label>
</p>

</div>

<div class="total-box">
<div class="row">

<div class="col-md-6">
<span class="total-label">Total Amount</span>
</div>

<div class="col-md-6 text-right">
<span class="total-amount">
₹ <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
</span>
</div>

</div>
</div>

<asp:Button
    ID="btnContinuePayment"
    runat="server"
    Text="Continue to Payment"
    CssClass="btn btn-primary btn-payment"
    OnClick="btnContinuePayment_Click" />

<asp:Label
    ID="lblMessage"
    runat="server"
    CssClass="message">
</asp:Label>

</div>
</div>
</asp:Content>