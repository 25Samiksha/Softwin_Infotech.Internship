<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PublicProducts.aspx.cs" Inherits="PublicProducts" MasterPageFile="~/Public.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="https://fonts.googleapis.com/css2?family=Outfit:wght@400;500;600;700;800&family=Inter:wght@400;500;600&display=swap" rel="stylesheet">
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
.products-page{
    background:var(--paper);
    min-height:100vh;
    padding-bottom:80px;
    font-family:"Inter",Arial,sans-serif;
    color:var(--ink);
}
.products-page h1,
.products-page h2,
.products-page h3,
.products-page h4{
    font-family:"Outfit","Inter",Arial,sans-serif;
}
.products-header{
    position:relative;
    background-image:linear-gradient(100deg,rgba(10,15,20,.92) 0%,rgba(10,15,20,.82) 55%,rgba(10,15,20,.62) 100%),url("https://images.unsplash.com/photo-1748944080268-0968963f250f?w=1800&h=700&fit=crop&auto=format&q=80");
    background-size:cover;
    background-position:center;
    padding:80px 0 110px;
    overflow:hidden;
}
.products-header:before{
    content:"";
    position:absolute;
    inset:0;
    background:rgba(0,0,0,.18);
    z-index:1;
}
.products-header-content{
    position:relative;
    z-index:5;
    text-align:center;
    max-width:640px;
    margin:0 auto;
}
.products-label{
    position:relative;
    z-index:5;
    display:inline-flex;
    align-items:center;
    gap:8px;
    background:rgba(0,0,0,.35);
    color:#ffffff !important;
    border:1px solid rgba(255,255,255,.35);
    padding:7px 18px;
    border-radius:30px;
    font-size:12px;
    font-weight:600;
    text-transform:uppercase;
    letter-spacing:1.2px;
    margin-bottom:18px;
}
.products-label i{
    color:#ffffff;
}
.products-title{
    position:relative;
    z-index:5;
    color:#ffffff !important;
    font-size:42px;
    font-weight:700;
    letter-spacing:-1px;
    margin:0 0 12px;
    text-shadow:0 3px 12px rgba(0,0,0,.55);
}
.products-subtitle{
    position:relative;
    z-index:5;
    color:#ffffff !important;
    font-size:16px;
    line-height:1.6;
    margin:0;
    text-shadow:0 2px 8px rgba(0,0,0,.55);
}
.filter-section{
    margin-top:-56px;
    position:relative;
    z-index:5;
}
.filter-panel{
    background:var(--panel);
    border-radius:14px;
    padding:28px 30px;
    box-shadow:0 20px 45px rgba(20,32,46,.16);
}
.filter-title{
    font-size:15px;
    font-weight:650;
    color:var(--ink);
    margin:0 0 20px;
    display:flex;
    align-items:center;
    gap:8px;
}
.filter-title i{
    color:var(--accent);
}
.filter-label{
    display:block;
    color:var(--ink-soft);
    font-size:12px;
    font-weight:650;
    text-transform:uppercase;
    letter-spacing:.6px;
    margin-bottom:8px;
}
.filter-control{
    width:100%;
    height:44px;
    border:1px solid var(--line);
    border-radius:7px;
    padding:8px 12px;
    color:var(--ink);
    background:var(--paper);
    box-shadow:none;
    transition:all .2s ease;
}
.filter-control:focus{
    border-color:var(--accent);
    box-shadow:0 0 0 3px var(--accent-tint);
    outline:none;
    background:#fff;
}
.search-button{
    width:100%;
    height:44px;
    border:0;
    border-radius:7px;
    background:var(--accent);
    color:#ffffff;
    font-weight:600;
    font-size:13.5px;
    transition:all .2s ease;
}
.search-button:hover{
    background:var(--accent-dark);
}
.clear-button{
    width:100%;
    height:44px;
    margin-top:8px;
    border:1px solid var(--line);
    border-radius:7px;
    background:#ffffff;
    color:var(--ink-soft);
    font-weight:600;
    font-size:13.5px;
    transition:all .2s ease;
}
.clear-button:hover{
    border-color:var(--accent);
    color:var(--accent-dark);
}
.product-section{
    padding-top:55px;
}
.product-count{
    color:var(--ink-soft);
    font-size:13px;
    font-weight:650;
    text-transform:uppercase;
    letter-spacing:.8px;
    margin-bottom:22px;
    display:flex;
    align-items:center;
    gap:7px;
}
.product-count i{
    color:var(--accent);
}
.product-message{
    display:block;
    text-align:center;
    color:var(--ink-soft);
    font-size:15px;
    background:var(--accent-tint);
    border-radius:10px;
    padding:16px;
    margin-bottom:25px;
}
.product-card{
    background:var(--panel);
    border:1px solid var(--line);
    border-radius:14px;
    overflow:hidden;
    height:100%;
    box-shadow:0 2px 10px rgba(24,40,58,.04);
    transition:all .25s ease;
}
.product-card:hover{
    transform:translateY(-6px);
    box-shadow:0 20px 40px rgba(24,40,58,.14);
    border-color:transparent;
}
.product-image-container{
    height:240px;
    background:var(--accent-tint);
    position:relative;
    overflow:hidden;
}
.product-image{
    width:100%;
    height:100%;
    object-fit:cover;
    transition:transform .4s ease;
}
.product-card:hover .product-image{
    transform:scale(1.06);
}
.product-category-badge{
    position:absolute;
    top:14px;
    left:14px;
    background:rgba(255,255,255,.95);
    color:var(--accent-dark);
    padding:6px 12px;
    border-radius:20px;
    font-size:11px;
    font-weight:700;
    text-transform:uppercase;
    letter-spacing:.4px;
    box-shadow:0 3px 10px rgba(24,40,58,.10);
}
.product-content{
    padding:22px;
}
.product-name{
    color:var(--ink);
    font-size:18px;
    font-weight:650;
    margin-bottom:8px;
    min-height:24px;
}
.product-gat{
    color:var(--ink-soft);
    font-size:12.5px;
    margin-bottom:10px;
}
.product-gat i{
    color:var(--accent);
    margin-right:3px;
}
.product-gat strong{
    color:var(--ink);
}
.product-description{
    color:var(--ink-faint);
    font-size:13px;
    line-height:1.6;
    margin-bottom:12px;
    min-height:20px;
}
.product-stock{
    color:var(--ink-soft);
    font-size:12.5px;
    margin-bottom:16px;
}
.product-stock i{
    color:var(--accent);
    margin-right:5px;
}
.product-bottom{
    display:flex;
    align-items:center;
    justify-content:space-between;
    gap:12px;
    padding-top:16px;
    border-top:1px solid var(--line);
}
.product-price{
    color:var(--ink);
    font-size:20px;
    font-weight:750;
    white-space:nowrap;
}
.view-button{
    display:inline-flex;
    align-items:center;
    gap:5px;
    padding:9px 15px;
    background:var(--ink);
    color:#ffffff !important;
    border-radius:7px;
    text-decoration:none !important;
    font-size:13px;
    font-weight:600;
    transition:all .2s ease;
}
.view-button:hover{
    background:var(--accent-dark);
}
@media(max-width:991px){
    .products-title{
        font-size:34px;
    }
    .filter-column{
        margin-bottom:16px;
    }
}
@media(max-width:767px){
    .products-header{
        padding:60px 0 90px;
    }
    .products-title{
        font-size:28px;
    }
    .products-subtitle{
        font-size:14px;
        line-height:1.6;
    }
    .filter-section{
        margin-top:24px;
    }
    .filter-panel{
        padding:22px;
    }
    .product-image-container{
        height:210px;
    }
    .product-section{
        padding-top:36px;
    }
}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="products-page">

<section class="products-header">
<div class="container">
<div class="products-header-content">

<div class="products-label">
<i class="glyphicon glyphicon-shopping-cart"></i>
Mahila Marketplace
</div>

<h1 class="products-title">
Explore Our Products
</h1>

<p class="products-subtitle">
Discover products created and sold by Mahila Bachat Gats
</p>

</div>
</div>
</section>

<section class="filter-section">
<div class="container">
<div class="filter-panel">

<div class="filter-title">
<i class="glyphicon glyphicon-filter"></i>
Find What You're Looking For
</div>

<div class="row">

<div class="col-md-4 filter-column">
<label class="filter-label">
Search Product
</label>

<asp:TextBox
ID="txtSearch"
runat="server"
CssClass="filter-control"
placeholder="Search products...">
</asp:TextBox>
</div>

<div class="col-md-3 filter-column">
<label class="filter-label">
Category
</label>

<asp:DropDownList
ID="ddlCategory"
runat="server"
CssClass="filter-control">
</asp:DropDownList>
</div>

<div class="col-md-3 filter-column">
<label class="filter-label">
Bachat Gat
</label>

<asp:DropDownList
ID="ddlBachatGat"
runat="server"
CssClass="filter-control">
</asp:DropDownList>
</div>

<div class="col-md-2">
<label class="filter-label">
&nbsp;
</label>

<asp:Button
ID="btnSearch"
runat="server"
Text="Search"
CssClass="search-button"
OnClick="btnSearch_Click" />

<asp:Button
ID="btnClear"
runat="server"
Text="Clear"
CssClass="clear-button"
OnClick="btnClear_Click" />
</div>

</div>
</div>
</div>
</section>

<section class="product-section">
<div class="container">

<div class="product-count">
<i class="glyphicon glyphicon-th"></i>
Available Products
</div>

<asp:Label
ID="lblMessage"
runat="server"
CssClass="product-message">
</asp:Label>

<div class="row">

<asp:Repeater ID="rptProducts" runat="server">
<ItemTemplate>

<div class="col-lg-4 col-md-4 col-sm-6" style="margin-bottom:30px;">

<div class="product-card">

<div class="product-image-container">

<asp:Image
ID="imgProduct"
runat="server"
ImageUrl='<%# GetImageUrl(Eval("ProductImage")) %>'
CssClass="product-image"
AlternateText='<%# Eval("ProductName") %>' />

<div class="product-category-badge">
<%# Eval("Category") %>
</div>

</div>

<div class="product-content">

<div class="product-name">
<%# Eval("ProductName") %>
</div>

<div class="product-gat">
<i class="glyphicon glyphicon-home"></i>
&nbsp;
<strong>Bachat Gat:</strong>
<%# Eval("GatName") %>
</div>

<div class="product-description">
<%# Eval("Description") %>
</div>

<div class="product-stock">
<i class="glyphicon glyphicon-check"></i>
<%# Eval("Quantity") %>
&nbsp;<%# Eval("Unit") %> available
</div>

<div class="product-bottom">

<div class="product-price">
₹ <%# Eval("SellingPrice", "{0:N2}") %>
</div>

<a href='<%# "ProductDetails.aspx?ProductID=" + Eval("ProductID") %>'
class="view-button">
View Product
&nbsp;<i class="glyphicon glyphicon-arrow-right"></i>
</a>

</div>
</div>
</div>
</div>

</ItemTemplate>
</asp:Repeater>

</div>
</div>
</section>

</div>
</asp:Content>