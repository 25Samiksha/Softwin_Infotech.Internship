<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PublicHome.aspx.cs" Inherits="PublicHome" MasterPageFile="~/Public.Master" %>

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
body{
    background:var(--paper);
    color:var(--ink);
    font-family:"Inter",Arial,sans-serif;
    font-size:15px;
}
h1,h2,h3,h4{
    font-family:"Outfit","Inter",Arial,sans-serif;
}
.home-wrapper{
    overflow:hidden;
}
.hero-section{
    position:relative;
    min-height:640px;
    display:flex;
    align-items:center;
    background-image:linear-gradient(100deg,rgba(15,25,37,.88) 0%,rgba(15,25,37,.72) 45%,rgba(15,25,37,.35) 100%),url("https://images.unsplash.com/photo-1748944076823-9991da0a0634?w=1800&h=1100&fit=crop&auto=format&q=80");
    background-size:cover;
    background-position:center;
    padding:60px 0;
    overflow:hidden;
}
.hero-section:before{
    content:"";
    position:absolute;
    inset:0;
    background:rgba(0,0,0,.15);
    z-index:1;
}
.hero-content{
    position:relative;
    z-index:5;
    max-width:600px;
}
.hero-badge{
    position:relative;
    z-index:5;
    display:inline-flex;
    align-items:center;
    gap:8px;
    background:rgba(0,0,0,.35);
    color:#ffffff;
    padding:7px 16px 7px 6px;
    border-radius:30px;
    font-size:12px;
    font-weight:600;
    letter-spacing:.4px;
    margin-bottom:24px;
    border:1px solid rgba(255,255,255,.3);
}
.hero-badge i{
    width:22px;
    height:22px;
    border-radius:50%;
    background:var(--accent);
    color:#fff;
    display:flex;
    align-items:center;
    justify-content:center;
    font-size:11px;
}
.hero-title{
    position:relative;
    z-index:5;
    font-size:54px;
    line-height:1.1;
    font-weight:700;
    color:#ffffff !important;
    margin:0 0 20px;
    letter-spacing:-1px;
    text-shadow:0 3px 12px rgba(0,0,0,.45);
}
.hero-title span{
    color:#f0a477 !important;
}
.hero-subtitle{
    position:relative;
    z-index:5;
    font-size:19px;
    color:#ffffff !important;
    line-height:1.6;
    max-width:480px;
    margin-bottom:12px;
    font-weight:500;
    text-shadow:0 2px 8px rgba(0,0,0,.45);
}
.hero-description{
    position:relative;
    z-index:5;
    font-size:15px;
    color:#f0f0f0 !important;
    max-width:460px;
    line-height:1.75;
    margin-bottom:34px;
    text-shadow:0 2px 8px rgba(0,0,0,.45);
}
.hero-buttons{
    position:relative;
    z-index:5;
    display:flex;
    gap:16px;
    flex-wrap:wrap;
    align-items:center;
    margin-bottom:44px;
}
.btn-shop{
    display:inline-flex;
    align-items:center;
    padding:15px 30px;
    background:var(--accent);
    color:#fff !important;
    border-radius:6px;
    font-size:14px;
    font-weight:600;
    letter-spacing:.2px;
    text-decoration:none !important;
    box-shadow:0 12px 28px rgba(193,101,47,.35);
    transition:all .2s ease;
}
.btn-shop:hover{
    background:var(--accent-dark);
    transform:translateY(-2px);
    box-shadow:0 16px 32px rgba(193,101,47,.4);
}
.btn-explore{
    display:inline-flex;
    align-items:center;
    padding:15px 26px;
    background:rgba(255,255,255,.08);
    color:#fff !important;
    border:1px solid rgba(255,255,255,.35);
    border-radius:6px;
    font-size:14px;
    font-weight:600;
    text-decoration:none !important;
    transition:all .2s ease;
    backdrop-filter:blur(4px);
}
.btn-explore:hover{
    background:rgba(255,255,255,.16);
    border-color:rgba(255,255,255,.6);
}
.hero-stats{
    position:relative;
    z-index:5;
    display:flex;
    gap:36px;
    flex-wrap:wrap;
}
.hero-stat{
    padding-left:16px;
    border-left:2px solid var(--accent);
}
.hero-stat strong{
    display:block;
    color:#ffffff !important;
    font-size:15px;
    font-weight:700;
}
.hero-stat span{
    display:block;
    color:#e0e0e0 !important;
    font-size:12px;
    margin-top:2px;
}
.section{
    padding:90px 0;
}
.section-header{
    text-align:center;
    margin-bottom:50px;
    max-width:600px;
    margin-left:auto;
    margin-right:auto;
}
.section-label{
    color:var(--accent-dark);
    font-size:12px;
    text-transform:uppercase;
    letter-spacing:1.8px;
    font-weight:700;
    margin-bottom:10px;
}
.section-title{
    font-size:32px;
    font-weight:700;
    color:var(--ink);
    margin:0 0 12px;
    letter-spacing:-.5px;
}
.section-description{
    color:var(--ink-soft);
    font-size:15px;
    line-height:1.7;
    margin:0;
}
.product-slider-section{
    padding:55px 0;
    background:var(--paper);
    overflow:hidden;
}
.product-slider{
    width:100%;
    overflow:hidden;
    position:relative;
}
.product-slider-track{
    display:flex;
    width:max-content;
    animation:productRunning 20s linear infinite;
}
.slider-product{
    width:260px;
    height:210px;
    margin-right:22px;
    border-radius:14px;
    overflow:hidden;
    background:#ffffff;
    box-shadow:0 8px 25px rgba(24,40,58,.12);
    flex-shrink:0;
}
.slider-product img{
    width:100%;
    height:100%;
    object-fit:cover;
    display:block;
    transition:transform .4s ease;
}
.slider-product:hover img{
    transform:scale(1.08);
}
.product-slider:hover .product-slider-track{
    animation-play-state:paused;
}
@keyframes productRunning{
    from{
        transform:translateX(0);
    }
    to{
        transform:translateX(-50%);
    }
}
.category-card{
    display:block;
    text-decoration:none !important;
    position:relative;
    border-radius:12px;
    overflow:hidden;
    height:220px;
    box-shadow:0 10px 26px rgba(24,40,58,.10);
    transition:transform .25s ease,box-shadow .25s ease;
}
.category-card:hover{
    transform:translateY(-6px);
    box-shadow:0 20px 40px rgba(24,40,58,.18);
}
.category-card .cat-bg{
    position:absolute;
    inset:0;
    background-size:cover;
    background-position:center;
    transition:transform .4s ease;
}
.category-card:hover .cat-bg{
    transform:scale(1.08);
}
.category-card .cat-overlay{
    position:absolute;
    inset:0;
    background:linear-gradient(0deg,rgba(15,25,37,.82) 0%,rgba(15,25,37,.15) 60%,rgba(15,25,37,.05) 100%);
}
.category-card .cat-body{
    position:relative;
    z-index:2;
    height:100%;
    display:flex;
    flex-direction:column;
    justify-content:flex-end;
    padding:20px;
}
.category-icon{
    width:38px;
    height:38px;
    border-radius:8px;
    background:var(--accent);
    color:#fff;
    display:flex;
    align-items:center;
    justify-content:center;
    font-size:16px;
    margin-bottom:12px;
}
.category-card h4{
    color:#fff;
    font-size:17px;
    font-weight:650;
    margin:0;
}
.category-card p{
    color:#dfe4e8;
    font-size:12px;
    margin:4px 0 0;
}
.products-section{
    background:var(--panel);
    border-top:1px solid var(--line);
    border-bottom:1px solid var(--line);
}
.product-card{
    background:var(--panel);
    border:1px solid var(--line);
    border-radius:12px;
    overflow:hidden;
    height:100%;
    box-shadow:0 2px 10px rgba(24,40,58,.04);
    transition:all .25s ease;
}
.product-card:hover{
    transform:translateY(-6px);
    box-shadow:0 18px 34px rgba(24,40,58,.14);
    border-color:transparent;
}
.product-image-box{
    height:230px;
    background:var(--accent-tint);
    overflow:hidden;
    position:relative;
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
.product-info{
    padding:20px 22px 22px;
}
.product-category{
    color:var(--accent-dark);
    font-size:11px;
    font-weight:700;
    text-transform:uppercase;
    letter-spacing:1px;
}
.product-name{
    color:var(--ink);
    font-size:17px;
    font-weight:650;
    margin:8px 0 4px;
}
.product-gat{
    color:var(--ink-faint);
    font-size:12px;
    margin-bottom:14px;
}
.product-price{
    color:var(--ink);
    font-size:20px;
    font-weight:750;
    margin-bottom:16px;
}
.product-button{
    display:block;
    text-align:center;
    padding:11px;
    border-radius:6px;
    background:var(--ink);
    color:#fff !important;
    text-decoration:none !important;
    font-size:13px;
    font-weight:600;
    transition:all .2s ease;
}
.product-button:hover{
    background:var(--accent-dark);
}
.cta-banner{
    background:linear-gradient(120deg,#1c2530 0%,#2b3a4d 100%);
    border-radius:16px;
    padding:56px 50px;
    display:flex;
    align-items:center;
    justify-content:space-between;
    flex-wrap:wrap;
    gap:24px;
}
.cta-banner h3{
    color:#fff;
    font-size:26px;
    font-weight:700;
    margin:0 0 8px;
}
.cta-banner p{
    color:#c3cad1;
    font-size:14px;
    margin:0;
    max-width:440px;
}
@media(max-width:991px){
    .hero-title{
        font-size:40px;
    }
    .hero-section{
        min-height:auto;
        padding:70px 0 90px;
    }
    .slider-product{
        width:230px;
        height:190px;
    }
}
@media(max-width:767px){
    .hero-title{
        font-size:33px;
    }
    .hero-subtitle{
        font-size:17px;
    }
    .section{
        padding:60px 0;
    }
    .section-title{
        font-size:26px;
    }
    .product-slider-section{
        padding:40px 0;
    }
    .slider-product{
        width:210px;
        height:170px;
        margin-right:15px;
    }
    .product-slider-track{
        animation-duration:16s;
    }
    .product-image-box{
        height:200px;
    }
    .category-card{
        height:170px;
        margin-bottom:16px;
    }
    .cta-banner{
        padding:40px 26px;
    }
}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="home-wrapper">

<section class="hero-section">
<div class="container">
<div class="row">
<div class="col-md-8">
<div class="hero-content">

<div class="hero-badge">
<i class="glyphicon glyphicon-heart"></i>
Supporting Women Entrepreneurs
</div>

<h1 class="hero-title">
Products Made by <span>Women</span>,<br />
Supporting Women
</h1>

<p class="hero-subtitle">
Discover authentic products created with skill, care and dedication by Mahila Bachat Gats.
</p>

<p class="hero-description">
Shop handmade and locally produced goods while supporting women-led businesses and their communities.
</p>

<div class="hero-buttons">
<a href="PublicProducts.aspx" class="btn-shop">
Shop Products
&nbsp; <i class="glyphicon glyphicon-arrow-right"></i>
</a>

<a href="#categories" class="btn-explore">
Explore Categories
</a>
</div>

<div class="hero-stats">

<div class="hero-stat">
<strong>Handmade</strong>
<span>Every item crafted locally</span>
</div>

<div class="hero-stat">
<strong>Women-Led</strong>
<span>Run by Bachat Gat groups</span>
</div>

<div class="hero-stat">
<strong>Community First</strong>
<span>Proceeds stay local</span>
</div>

</div>
</div>
</div>
</div>
</div>
</section>

<section class="product-slider-section">
<div class="container">
<div class="product-slider">
<div class="product-slider-track">

<div class="slider-product">
<img src="Images/homemade bowl.jpg" alt="Homemade Bowl" />
</div>

<div class="slider-product">
<img src="Images/Homemade Mango Pickle.jpg" alt="Homemade Mango Pickle" />
</div>

<div class="slider-product">
<img src="Images/Homemade Papad.jpg" alt="Homemade Papad" />
</div>

<div class="slider-product">
<img src="Images/homemade bowl.jpg" alt="Homemade Bowl" />
</div>

<div class="slider-product">
<img src="Images/Homemade Mango Pickle.jpg" alt="Homemade Mango Pickle" />
</div>

<div class="slider-product">
<img src="Images/Homemade Papad.jpg" alt="Homemade Papad" />
</div>

</div>
</div>
</div>
</section>

<section class="section" id="categories">
<div class="container">

<div class="section-header">
<div class="section-label">
Explore
</div>

<h2 class="section-title">
Shop by Category
</h2>

<p class="section-description">
Explore products created by talented women from Mahila Bachat Gats.
</p>
</div>

<div class="row">

<div class="col-md-3 col-sm-6" style="margin-bottom:20px;">
<a href="PublicProducts.aspx?Category=Food" class="category-card">
<div class="cat-bg" style="background-image:url('https://images.unsplash.com/photo-1634588478809-22e8edbfc85a?w=500&h=400&fit=crop&auto=format&q=80');"></div>
<div class="cat-overlay"></div>
<div class="cat-body">

<div class="category-icon">
<i class="glyphicon glyphicon-cutlery"></i>
</div>

<h4>
Food Products
</h4>

<p>
Homemade &amp; traditional foods
</p>

</div>
</a>
</div>

<div class="col-md-3 col-sm-6" style="margin-bottom:20px;">
<a href="PublicProducts.aspx?Category=Handmade" class="category-card">
<div class="cat-bg" style="background-image:url('https://images.unsplash.com/photo-1748944076900-65700f687edf?w=500&h=400&fit=crop&auto=format&q=80');"></div>
<div class="cat-overlay"></div>
<div class="cat-body">

<div class="category-icon">
<i class="glyphicon glyphicon-heart"></i>
</div>

<h4>
Handmade Products
</h4>

<p>
Unique handmade creations
</p>

</div>
</a>
</div>

<div class="col-md-3 col-sm-6" style="margin-bottom:20px;">
<a href="PublicProducts.aspx?Category=Home" class="category-card">
<div class="cat-bg" style="background-image:url('https://images.unsplash.com/photo-1748944078380-3d12ffee7378?w=500&h=400&fit=crop&auto=format&q=80');"></div>
<div class="cat-overlay"></div>
<div class="cat-body">

<div class="category-icon">
<i class="glyphicon glyphicon-home"></i>
</div>

<h4>
Home Products
</h4>

<p>
Useful products for your home
</p>

</div>
</a>
</div>

<div class="col-md-3 col-sm-6" style="margin-bottom:20px;">
<a href="PublicProducts.aspx" class="category-card">
<div class="cat-bg" style="background-image:url('https://images.unsplash.com/photo-1748944080331-30fe7088ff11?w=500&h=400&fit=crop&auto=format&q=80');"></div>
<div class="cat-overlay"></div>
<div class="cat-body">

<div class="category-icon">
<i class="glyphicon glyphicon-th"></i>
</div>

<h4>
View All
</h4>

<p>
Explore all available products
</p>

</div>
</a>
</div>

</div>
</div>
</section>

<section class="section products-section">
<div class="container">

<div class="section-header">

<div class="section-label">
Our Products
</div>

<h2 class="section-title">
Featured Products
</h2>

<p class="section-description">
Discover some of the latest products available from our Mahila Bachat Gats.
</p>

</div>

<div class="row">

<asp:Repeater ID="rptProducts" runat="server">

<ItemTemplate>

<div class="col-md-3 col-sm-6" style="margin-bottom:30px;">

<div class="product-card">

<div class="product-image-box">

<asp:Image
ID="imgProduct"
runat="server"
ImageUrl='<%# GetImageUrl(Eval("ProductImage")) %>'
CssClass="product-image" />

</div>

<div class="product-info">

<div class="product-category">
<%# Eval("Category") %>
</div>

<div class="product-name">
<%# Eval("ProductName") %>
</div>

<div class="product-gat">
<i class="glyphicon glyphicon-map-marker"></i>
&nbsp; Mahila Bachat Gat
</div>

<div class="product-price">
₹ <%# Eval("SellingPrice", "{0:N2}") %>
</div>

<a href='<%# "ProductDetails.aspx?ProductID=" + Eval("ProductID") %>' class="product-button">
View Product
&nbsp; <i class="glyphicon glyphicon-arrow-right"></i>
</a>

</div>
</div>
</div>

</ItemTemplate>

</asp:Repeater>

</div>

<div class="text-center" style="margin-top:10px;">

<a href="PublicProducts.aspx" class="btn-shop">
View All Products
&nbsp; <i class="glyphicon glyphicon-arrow-right"></i>
</a>

</div>

</div>
</section>

<section class="section" style="padding-top:0;">
<div class="container">

<div class="cta-banner">

<div>

<h3>
Ready to shop with purpose?
</h3>

<p>
Every order supports a woman-led self-help group and helps strengthen local communities.
</p>

</div>

<a href="PublicProducts.aspx" class="btn-shop">
Browse All Products
&nbsp; <i class="glyphicon glyphicon-arrow-right"></i>
</a>

</div>

</div>
</section>

</div>
</asp:Content>