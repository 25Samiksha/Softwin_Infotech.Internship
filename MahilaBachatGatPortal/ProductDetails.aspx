<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductDetails.aspx.cs" Inherits="ProductDetails" MasterPageFile="~/Public.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
.product-page{max-width:1080px;margin:35px auto;padding:0 20px}
.product-card{background:#fff;border-radius:18px;padding:28px;border:1px solid #e5ebf0;box-shadow:0 8px 30px rgba(30,60,90,.10)}
.product-image-box{background:#f7f9fb;border-radius:14px;padding:18px;text-align:center}
.product-image{width:100%;max-width:430px;height:390px;object-fit:cover;border-radius:12px}
.product-info{padding:5px 10px 10px 25px}
.product-category{display:inline-block;background:#edf6ff;color:#2874a6;font-size:13px;font-weight:700;padding:7px 14px;border-radius:20px;margin-bottom:15px;text-transform:uppercase}
.product-name{font-size:34px;font-weight:700;color:#172b4d;margin:0 0 18px}
.info-line{border-top:1px solid #edf0f3;padding-top:15px;margin-top:10px}
.info-label{display:block;font-weight:700;color:#172b4d;margin-bottom:5px}
.info-value{color:#60758a;line-height:1.6}
.price{font-size:30px;font-weight:700;color:#2874a6;margin-top:22px}
.stock{display:inline-block;margin-top:10px;background:#eaf8f0;color:#16834a;padding:7px 13px;border-radius:20px;font-size:13px;font-weight:600}
.quantity-title{font-weight:700;color:#172b4d;margin-top:20px;margin-bottom:8px}
.quantity-control{display:flex;align-items:center;width:150px;height:44px;border:1px solid #cfd9e2;border-radius:8px;overflow:hidden;background:#fff}
.quantity-button{flex:0 0 48px!important;width:48px!important;min-width:48px!important;max-width:48px!important;height:44px!important;min-height:44px!important;padding:0!important;margin:0!important;border:none!important;border-radius:0!important;background:#f2f6f9!important;color:#2874a6!important;font-size:22px!important;font-weight:700!important;line-height:44px!important;text-align:center!important;cursor:pointer;box-shadow:none!important}
.quantity-button:hover{background:#e2edf5!important;color:#1f5f8d!important}
.quantity-value{flex:1;width:54px;height:44px;padding:0;margin:0;border:none;border-left:1px solid #e0e6eb;border-right:1px solid #e0e6eb;border-radius:0;text-align:center;font-size:16px;font-weight:600;color:#172b4d;background:#fff;outline:none}
.total-box{margin-top:16px;color:#60758a;font-size:15px}
.total-amount{font-size:22px;font-weight:700;color:#172b4d}
.product-actions{display:flex;gap:12px;margin-top:20px}
.action-button{width:100%;max-width:180px;height:48px;border-radius:8px!important;font-size:16px;font-weight:600}
.message{display:block;margin-top:15px;color:#dc3545;font-weight:600}
@media(max-width:767px){
.product-page{margin:20px auto;padding:0 12px}
.product-card{padding:18px}
.product-image{height:300px}
.product-info{padding:20px 5px 5px}
.product-name{font-size:28px}
.product-actions{flex-direction:column}
.action-button{max-width:100%}
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="product-page">
<div class="product-card">
<div class="row">
<div class="col-md-6">
<div class="product-image-box">
<asp:Image ID="imgProduct" runat="server" CssClass="product-image" />
</div>
</div>
<div class="col-md-6">
<div class="product-info">
<asp:Label ID="lblCategory" runat="server" CssClass="product-category"></asp:Label>
<h1 class="product-name">
<asp:Label ID="lblProductName" runat="server"></asp:Label>
</h1>
<div class="info-line">
<span class="info-label">Bachat Gat</span>
<span class="info-value">
<asp:Label ID="lblBachatGat" runat="server"></asp:Label>
</span>
</div>
<div class="info-line">
<span class="info-label">Description</span>
<span class="info-value">
<asp:Label ID="lblDescription" runat="server"></asp:Label>
</span>
</div>
<div class="info-line">
<span class="info-label">Available Quantity</span>
<span class="info-value">
<asp:Label ID="lblAvailableQuantity" runat="server"></asp:Label>
<asp:Label ID="lblUnit" runat="server"></asp:Label>
</span>
</div>
<div class="quantity-title">Quantity</div>
<div class="quantity-control">
<asp:Button ID="btnMinus" runat="server" Text="−" CssClass="quantity-button" OnClick="btnMinus_Click" CausesValidation="false" />
<asp:TextBox ID="txtQuantity" runat="server" Text="1" CssClass="quantity-value" ReadOnly="true"></asp:TextBox>
<asp:Button ID="btnPlus" runat="server" Text="+" CssClass="quantity-button" OnClick="btnPlus_Click" CausesValidation="false" />
</div>
<div class="price">
₹ <asp:Label ID="lblPrice" runat="server"></asp:Label>
</div>
<div class="total-box">
Total:
<span class="total-amount">
₹ <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
</span>
</div>
<div class="product-actions">
<asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CssClass="btn btn-success action-button" OnClick="btnAddToCart_Click" CausesValidation="false" />
<asp:Button ID="btnBuyNow" runat="server" Text="Buy Now" CssClass="btn btn-primary action-button" OnClick="btnBuyNow_Click" CausesValidation="false" />
</div>
<asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
</div>
</div>
</div>
</div>
</div>
</asp:Content>