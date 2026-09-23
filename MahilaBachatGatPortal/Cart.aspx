<%@ Page Title="Cart" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeFile="Cart.aspx.cs" Inherits="Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
.cart-page{
    background:#f4f8fc;
    min-height:100vh;
    padding-bottom:80px;
}
.cart-hero{
    position:relative;
    background-image:linear-gradient(rgba(24,59,91,.78),rgba(24,59,91,.78)),url("images/cart-bg.jpg");
    background-size:cover;
    background-position:center;
    background-repeat:no-repeat;
    margin-bottom:40px;
    padding:65px 15px;
    overflow:hidden;
}
.cart-hero:before{
    content:"";
    position:absolute;
    width:320px;
    height:320px;
    border-radius:50%;
    background:rgba(255,255,255,.06);
    top:-150px;
    right:-70px;
}
.cart-hero:after{
    content:"";
    position:absolute;
    width:220px;
    height:220px;
    border-radius:50%;
    background:rgba(120,185,225,.08);
    bottom:-120px;
    left:-60px;
}
.cart-heading{
    position:relative;
    z-index:2;
    text-align:center;
}
.cart-label{
    display:inline-block;
    background:rgba(255,255,255,.13);
    border:1px solid rgba(255,255,255,.22);
    color:#ffffff;
    padding:8px 17px;
    border-radius:30px;
    font-size:12px;
    font-weight:700;
    text-transform:uppercase;
    letter-spacing:1px;
    margin-bottom:13px;
}
.cart-title{
    color:#ffffff;
    font-size:40px;
    font-weight:750;
    margin:0 0 10px;
}
.cart-subtitle{
    color:#e1edf5;
    font-size:15px;
    margin:0;
}
.cart-container{
    max-width:1100px;
    margin:0 auto;
    padding:0 15px;
}
.cart-panel{
    background:#ffffff;
    border:1px solid #dce7f1;
    border-radius:16px;
    padding:25px;
    box-shadow:0 10px 30px rgba(24,59,91,.07);
}
.cart-image{
    width:80px;
    height:80px;
    object-fit:cover;
    border-radius:8px;
}
.cart-table{
    width:100%;
}
.cart-table th{
    background:#2f78b7;
    color:#ffffff;
    padding:13px;
    border:0;
}
.cart-table td{
    padding:13px;
    vertical-align:middle;
    color:#40566b;
    border-bottom:1px solid #e7eef5;
}
.cart-table tr:hover td{
    background:#f8fbfe;
}
.quantity-box{
    display:flex;
    align-items:center;
    gap:8px;
}
.quantity-button{
    width:32px;
    height:32px;
    padding:0;
    border-radius:6px;
}
.quantity-text{
    width:45px;
    text-align:center;
    color:#183b5b;
    font-weight:600;
}
.cart-total{
    text-align:right;
    color:#183b5b;
    font-size:21px;
    font-weight:700;
    margin-top:22px;
    padding-top:18px;
    border-top:1px solid #e7eef5;
}
.cart-actions{
    text-align:right;
    margin-top:22px;
}
.cart-actions .btn{
    border-radius:7px;
    padding:10px 18px;
    font-weight:600;
    margin-left:7px;
}
.cart-actions .btn-default{
    border-color:#cfdde8;
    color:#526b80;
    background:#ffffff;
}
.cart-actions .btn-default:hover{
    border-color:#2f78b7;
    color:#2f78b7;
}
.cart-actions .btn-primary{
    background:#2f78b7;
    border-color:#2f78b7;
}
.cart-actions .btn-primary:hover{
    background:#245f91;
    border-color:#245f91;
}
.message{
    display:block;
    margin-bottom:15px;
}
.empty-cart{
    background:#ffffff;
    text-align:center;
    padding:55px 30px;
    color:#718397;
}
.empty-cart h4{
    color:#183b5b;
    font-size:24px;
    font-weight:700;
    margin:10px 0;
}
.empty-cart p{
    color:#718397;
    font-size:14px;
    margin-bottom:20px;
}
.empty-cart .btn{
    background:#2f78b7;
    border-color:#2f78b7;
    border-radius:7px;
    padding:10px 22px;
    font-weight:600;
}
.empty-cart .btn:hover{
    background:#245f91;
    border-color:#245f91;
}
@media(max-width:767px){
    .cart-hero{
        padding:50px 15px;
    }
    .cart-title{
        font-size:30px;
    }
    .cart-subtitle{
        font-size:14px;
        line-height:1.6;
    }
    .cart-container{
        padding:0 10px;
    }
    .cart-panel{
        padding:15px;
        overflow-x:auto;
    }
    .cart-table{
        min-width:700px;
    }
    .cart-total{
        min-width:700px;
    }
    .cart-actions{
        min-width:700px;
    }
}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="cart-page">

<div class="cart-hero">
<div class="cart-heading">

<div class="cart-label">
<span class="glyphicon glyphicon-shopping-cart"></span>
&nbsp; Shopping Cart
</div>

<div class="cart-title">
Your Cart
</div>

<p class="cart-subtitle">
Review your selected products before proceeding to checkout
</p>

</div>
</div>

<div class="cart-container">

<div class="cart-panel">

<asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>

<asp:Panel ID="pnlCart" runat="server">

<asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" CssClass="cart-table" GridLines="None" OnRowCommand="gvCart_RowCommand">

<Columns>

<asp:TemplateField HeaderText="Product">
<ItemTemplate>

<asp:Image
    ID="imgProduct"
    runat="server"
    ImageUrl='<%# GetImageUrl(Eval("ProductImage")) %>'
    CssClass="cart-image" />

</ItemTemplate>
</asp:TemplateField>

<asp:BoundField
    DataField="ProductName"
    HeaderText="Product Name" />

<asp:BoundField
    DataField="SellingPrice"
    HeaderText="Price"
    DataFormatString="₹ {0:N2}" />

<asp:TemplateField HeaderText="Quantity">
<ItemTemplate>

<div class="quantity-box">

<asp:Button
    ID="btnMinus"
    runat="server"
    Text="−"
    CommandName="Decrease"
    CommandArgument='<%# Eval("CartID") %>'
    CssClass="btn btn-default quantity-button"
    CausesValidation="false" />

<asp:Label
    ID="lblQuantity"
    runat="server"
    Text='<%# Eval("Quantity") %>'
    CssClass="quantity-text">
</asp:Label>

<asp:Button
    ID="btnPlus"
    runat="server"
    Text="+"
    CommandName="Increase"
    CommandArgument='<%# Eval("CartID") %>'
    CssClass="btn btn-default quantity-button"
    CausesValidation="false" />

</div>

</ItemTemplate>
</asp:TemplateField>

<asp:BoundField
    DataField="ItemTotal"
    HeaderText="Total"
    DataFormatString="₹ {0:N2}" />

<asp:TemplateField HeaderText="Action">
<ItemTemplate>

<asp:Button
    ID="btnRemove"
    runat="server"
    Text="Remove"
    CommandName="RemoveItem"
    CommandArgument='<%# Eval("CartID") %>'
    CssClass="btn btn-danger btn-sm"
    CausesValidation="false" />

</ItemTemplate>
</asp:TemplateField>

</Columns>

</asp:GridView>

<div class="cart-total">
Cart Total: ₹
<asp:Label ID="lblCartTotal" runat="server"></asp:Label>
</div>

<div class="cart-actions">

<asp:Button
    ID="btnContinueShopping"
    runat="server"
    Text="Continue Shopping"
    CssClass="btn btn-default"
    OnClick="btnContinueShopping_Click"
    CausesValidation="false" />

<asp:Button
    ID="btnCheckout"
    runat="server"
    Text="Proceed to Checkout"
    CssClass="btn btn-primary"
    OnClick="btnCheckout_Click"
    CausesValidation="false" />

</div>

</asp:Panel>

<asp:Panel
    ID="pnlEmptyCart"
    runat="server"
    Visible="false"
    CssClass="empty-cart">

<h4>
Your cart is empty.
</h4>

<p>
Add some products to your cart and come back here.
</p>

<asp:Button
    ID="btnShopNow"
    runat="server"
    Text="Shop Now"
    CssClass="btn btn-primary"
    OnClick="btnShopNow_Click"
    CausesValidation="false" />

</asp:Panel>

</div>
</div>

</div>
</asp:Content>