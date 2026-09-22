<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderPlaced.aspx.cs" Inherits="OrderPlaced" MasterPageFile="~/Public.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
.order-success-container {
    max-width: 850px;
    margin: 40px auto;
}

.order-success-panel {
    background: #fff;
    padding: 40px;
    border-radius: 8px;
    box-shadow: 0 2px 10px rgba(0,0,0,0.12);
    text-align: center;
}

.success-icon {
    font-size: 60px;
    color: #5cb85c;
    margin-bottom: 15px;
}

.success-title {
    color: #5cb85c;
    font-weight: 600;
    margin-bottom: 10px;
}

.order-number {
    font-size: 18px;
    margin-bottom: 30px;
}

.details-box {
    text-align: left;
    background: #f8fafc;
    border: 1px solid #e5e9ef;
    border-radius: 6px;
    padding: 25px;
    margin-bottom: 25px;
}

.section-title {
    color: #2f6f9f;
    font-weight: 600;
    margin-top: 0;
    margin-bottom: 20px;
}

.detail-row {
    margin-bottom: 12px;
}

.total-amount {
    font-size: 22px;
    font-weight: 700;
    color: #2f6f9f;
}

.btn-home {
    margin: 5px;
}

.btn-products {
    margin: 5px;
}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="order-success-container">

<div class="order-success-panel">

<div class="success-icon">
<span class="glyphicon glyphicon-ok-circle"></span>
</div>

<h2 class="success-title">Order Placed Successfully</h2>

<p class="order-number">
Order ID:
<strong>
<asp:Label ID="lblOrderID" runat="server"></asp:Label>
</strong>
</p>

<div class="details-box">

<h4 class="section-title">Order Details</h4>

<div class="detail-row">
<strong>Product:</strong>
<asp:Label ID="lblProductName" runat="server"></asp:Label>
</div>

<div class="detail-row">
<strong>Quantity:</strong>
<asp:Label ID="lblQuantity" runat="server"></asp:Label>
</div>

<div class="detail-row">
<strong>Payment Method:</strong>
<asp:Label ID="lblPaymentMode" runat="server"></asp:Label>
</div>

<div class="detail-row">
<strong>Payment Status:</strong>
<asp:Label ID="lblPaymentStatus" runat="server"></asp:Label>
</div>

<div class="detail-row">
<strong>Order Status:</strong>
<asp:Label ID="lblOrderStatus" runat="server"></asp:Label>
</div>

<div class="detail-row">
<strong>Total Amount:</strong>
<span class="total-amount">
₹ <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
</span>
</div>

</div>

<div class="details-box">

<h4 class="section-title">Delivery Address</h4>

<div class="detail-row">
<strong>Name:</strong>
<asp:Label ID="lblFullName" runat="server"></asp:Label>
</div>

<div class="detail-row">
<strong>Mobile:</strong>
<asp:Label ID="lblMobile" runat="server"></asp:Label>
</div>

<div class="detail-row">
<strong>Address:</strong>
<asp:Label ID="lblAddress" runat="server"></asp:Label>
</div>

<div class="detail-row">
<strong>City / Village:</strong>
<asp:Label ID="lblCity" runat="server"></asp:Label>
</div>

<div class="detail-row">
<strong>Taluka:</strong>
<asp:Label ID="lblTaluka" runat="server"></asp:Label>
</div>

<div class="detail-row">
<strong>District:</strong>
<asp:Label ID="lblDistrict" runat="server"></asp:Label>
</div>

<div class="detail-row">
<strong>State:</strong>
<asp:Label ID="lblState" runat="server"></asp:Label>
</div>

<div class="detail-row">
<strong>PIN Code:</strong>
<asp:Label ID="lblPincode" runat="server"></asp:Label>
</div>

</div>

<a href="PublicProducts.aspx" class="btn btn-primary btn-products">
Continue Shopping
</a>

<a href="PublicHome.aspx" class="btn btn-default btn-home">
Home
</a>

</div>

</div>

</asp:Content>