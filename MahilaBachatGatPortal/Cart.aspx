<%@ Page Title="Cart" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeFile="Cart.aspx.cs" Inherits="Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="https://fonts.googleapis.com/css2?family=Outfit:wght@500;600;700;800&family=Inter:wght@400;500;600&display=swap" rel="stylesheet">
<style>
:root{
    --ink:#1c2530;
    --ink-soft:#5b6672;
    --ink-faint:#93a0ad;
    --paper:#fbfaf8;
    --panel:#ffffff;
    --line:#e8e4dd;
    --accent:#c1652f;
    --accent-dark:#a2521f;
    --accent-tint:#fbeee5;
}
.cart-page{
    background:var(--paper);
    min-height:100vh;
    padding-bottom:80px;
    font-family:"Inter",Arial,sans-serif;
    color:var(--ink);
}
.cart-page h1,
.cart-page h2,
.cart-page h3,
.cart-page h4{
    font-family:"Outfit","Inter",Arial,sans-serif;
}
.cart-hero{
    position:relative;
    background-image:linear-gradient(100deg,rgba(15,25,37,.88) 0%,rgba(15,25,37,.72) 55%,rgba(15,25,37,.4) 100%),url("https://images.unsplash.com/photo-1748944084924-bef8de7650f5?w=1800&h=650&fit=crop&auto=format&q=80");
    background-size:cover;
    background-position:center;
    margin-bottom:40px;
    padding:65px 15px;
}
.cart-heading{
    position:relative;
    z-index:2;
    text-align:center;
}
.cart-label{
    display:inline-flex;
    align-items:center;
    gap:8px;
    background:rgba(255,255,255,.12);
    border:1px solid rgba(255,255,255,.2);
    color:#f3ded2;
    padding:7px 17px;
    border-radius:30px;
    font-size:12px;
    font-weight:600;
    text-transform:uppercase;
    letter-spacing:1px;
    margin-bottom:16px;
}
.cart-title{
    color:#ffffff;
    font-size:40px;
    font-weight:700;
    letter-spacing:-1px;
    margin:0 0 10px;
}
.cart-subtitle{
    color:#dfe4e8;
    font-size:15px;
    margin:0;
}
.cart-container{
    max-width:1100px;
    margin:0 auto;
    padding:0 15px;
}
.cart-panel{
    background:var(--panel);
    border:1px solid var(--line);
    border-radius:14px;
    padding:25px;
    box-shadow:0 2px 10px rgba(24,40,58,.04);
}
.cart-image{
    width:80px;
    height:80px;
    object-fit:cover;
    border-radius:8px;
}
.cart-table{
    width:100%;
    border-collapse:separate;
    border-spacing:0;
}
.cart-table th{
    background:var(--ink);
    color:#ffffff;
    padding:13px;
    border:0;
    font-size:12.5px;
    font-weight:650;
    text-transform:uppercase;
    letter-spacing:.5px;
}
.cart-table th:first-child{
    border-top-left-radius:8px;
}
.cart-table th:last-child{
    border-top-right-radius:8px;
}
.cart-table td{
    padding:14px 13px;
    vertical-align:middle;
    color:var(--ink);
    border-bottom:1px solid var(--line);
    font-size:14px;
}
.cart-table tr:hover td{
    background:var(--accent-tint);
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
    border:1px solid var(--line);
    background:#ffffff;
    color:var(--ink);
    font-weight:700;
    transition:all .2s ease;
}
.quantity-button:hover{
    border-color:var(--accent);
    color:var(--accent-dark);
}
.quantity-text{
    width:45px;
    text-align:center;
    color:var(--ink);
    font-weight:650;
}
.cart-total{
    text-align:right;
    color:var(--ink);
    font-size:20px;
    font-weight:700;
    margin-top:22px;
    padding-top:18px;
    border-top:1px solid var(--line);
}
.cart-actions{
    text-align:right;
    margin-top:22px;
}
.cart-actions .btn{
    border-radius:7px;
    padding:10px 20px;
    font-weight:600;
    margin-left:8px;
    font-size:13.5px;
}
.cart-actions .btn-default{
    border-color:var(--line);
    color:var(--ink-soft);
    background:#ffffff;
}
.cart-actions .btn-default:hover{
    border-color:var(--accent);
    color:var(--accent-dark);
}
.cart-actions .btn-primary{
    background:var(--accent);
    border-color:var(--accent);
}
.cart-actions .btn-primary:hover{
    background:var(--accent-dark);
    border-color:var(--accent-dark);
}
.message{
    display:block;
    margin-bottom:15px;
}
.empty-cart{
    background:var(--panel);
    text-align:center;
    padding:55px 30px;
    color:var(--ink-soft);
}
.empty-cart h4{
    color:var(--ink);
    font-size:23px;
    font-weight:700;
    margin:10px 0;
}
.empty-cart p{
    color:var(--ink-soft);
    font-size:14px;
    margin-bottom:20px;
}
.empty-cart .btn{
    background:var(--accent);
    border-color:var(--accent);
    border-radius:7px;
    padding:10px 22px;
    font-weight:600;
}
.empty-cart .btn:hover{
    background:var(--accent-dark);
    border-color:var(--accent-dark);
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
Shopping Cart
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