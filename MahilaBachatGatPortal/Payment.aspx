<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Payment.aspx.cs" Inherits="Payment" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
.payment-container {
    max-width: 800px;
    margin: 30px auto;
}

.payment-panel {
    background: #fff;
    padding: 30px;
    border-radius: 8px;
    box-shadow: 0 2px 10px rgba(0,0,0,0.12);
}

.payment-title {
    text-align: center;
    color: #2f6f9f;
    margin-bottom: 30px;
}

.amount-box {
    background: #f8fafc;
    border: 1px solid #e5e9ef;
    border-radius: 6px;
    padding: 20px;
    text-align: center;
    margin-bottom: 30px;
}

.amount-label {
    font-size: 18px;
    font-weight: 600;
}

.amount {
    font-size: 26px;
    font-weight: 700;
    color: #2f6f9f;
}

.payment-option {
    border: 1px solid #ddd;
    border-radius: 6px;
    padding: 15px;
    margin-bottom: 12px;
}

.btn-pay {
    width: 100%;
    margin-top: 20px;
}

.message {
    display: block;
    text-align: center;
    margin-top: 15px;
}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="payment-container">

<div class="payment-panel">

<h2 class="payment-title">Payment</h2>

<div class="amount-box">

<div class="amount-label">
Total Amount
</div>

<div class="amount">
₹ <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
</div>

</div>

<h4>Choose Payment Method</h4>

<div class="payment-option">
<asp:RadioButton
    ID="rbCOD"
    runat="server"
    GroupName="PaymentMethod"
    Text=" Cash on Delivery"
    Checked="true" />
</div>

<div class="payment-option">
<asp:RadioButton
    ID="rbOnline"
    runat="server"
    GroupName="PaymentMethod"
    Text=" Online Payment" />
</div>

<asp:Button
    ID="btnPlaceOrder"
    runat="server"
    Text="Place Order"
    CssClass="btn btn-primary btn-pay"
    OnClick="btnPlaceOrder_Click" />

<asp:Label
    ID="lblMessage"
    runat="server"
    CssClass="message">
</asp:Label>

</div>

</div>

</asp:Content>