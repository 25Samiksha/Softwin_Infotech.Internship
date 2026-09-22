<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Checkout.aspx.cs" Inherits="Checkout" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
.checkout-container {
    max-width: 900px;
    margin: 30px auto;
}

.checkout-panel {
    background: #fff;
    padding: 30px;
    border-radius: 8px;
    box-shadow: 0 2px 10px rgba(0,0,0,0.12);
}

.checkout-title {
    text-align: center;
    margin-bottom: 30px;
    color: #2f6f9f;
    font-weight: 600;
}

.product-box {
    background: #f8fafc;
    border: 1px solid #e5e9ef;
    border-radius: 6px;
    padding: 20px;
    margin-bottom: 30px;
}

.section-title {
    color: #2f6f9f;
    font-weight: 600;
    margin-bottom: 20px;
}

.btn-continue {
    width: 100%;
    margin-top: 15px;
}

.message {
    display: block;
    margin-top: 15px;
    text-align: center;
}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="checkout-container">

<div class="checkout-panel">

<h2 class="checkout-title">Checkout</h2>

<div class="product-box">

<h4 class="section-title">Product Details</h4>

<div class="row">

<div class="col-md-6">
<strong>Product Name</strong>
<br />
<asp:Label ID="lblProductName" runat="server"></asp:Label>
</div>

<div class="col-md-3">
<strong>Price</strong>
<br />
₹ <asp:Label ID="lblPrice" runat="server"></asp:Label>
</div>

<div class="col-md-3">
<strong>Available Quantity</strong>
<br />
<asp:Label ID="lblQuantity" runat="server"></asp:Label>
</div>

</div>

</div>

<h4 class="section-title">Delivery Address</h4>

<div class="row">

<div class="col-md-6">

<div class="form-group">

<label>Full Name</label>

<asp:TextBox
    ID="txtFullName"
    runat="server"
    CssClass="form-control"
    placeholder="Enter full name">
</asp:TextBox>

</div>

</div>

<div class="col-md-6">

<div class="form-group">

<label>Mobile Number</label>

<asp:TextBox
    ID="txtMobile"
    runat="server"
    CssClass="form-control"
    placeholder="Enter mobile number">
</asp:TextBox>

</div>

</div>

</div>

<div class="form-group">

<label>Address</label>

<asp:TextBox
    ID="txtAddress"
    runat="server"
    CssClass="form-control"
    TextMode="MultiLine"
    Rows="3"
    placeholder="House number, street, area">
</asp:TextBox>

</div>

<div class="row">

<div class="col-md-4">

<div class="form-group">

<label>Village / City</label>

<asp:TextBox
    ID="txtCity"
    runat="server"
    CssClass="form-control"
    placeholder="Enter village or city">
</asp:TextBox>

</div>

</div>

<div class="col-md-4">

<div class="form-group">

<label>Taluka</label>

<asp:TextBox
    ID="txtTaluka"
    runat="server"
    CssClass="form-control"
    placeholder="Enter taluka">
</asp:TextBox>

</div>

</div>

<div class="col-md-4">

<div class="form-group">

<label>District</label>

<asp:TextBox
    ID="txtDistrict"
    runat="server"
    CssClass="form-control"
    placeholder="Enter district">
</asp:TextBox>

</div>

</div>

</div>

<div class="row">

<div class="col-md-6">

<div class="form-group">

<label>State</label>

<asp:TextBox
    ID="txtState"
    runat="server"
    CssClass="form-control"
    placeholder="Enter state">
</asp:TextBox>

</div>

</div>

<div class="col-md-6">

<div class="form-group">

<label>PIN Code</label>

<asp:TextBox
    ID="txtPincode"
    runat="server"
    CssClass="form-control"
    placeholder="Enter PIN code">
</asp:TextBox>

</div>

</div>

</div>

<asp:Button
    ID="btnContinue"
    runat="server"
    Text="Continue to Order Review"
    CssClass="btn btn-primary btn-continue"
    OnClick="btnContinue_Click" />

<asp:Label
    ID="lblMessage"
    runat="server"
    CssClass="message">
</asp:Label>

</div>

</div>

</asp:Content>