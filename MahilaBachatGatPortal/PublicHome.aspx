<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PublicHome.aspx.cs" Inherits="PublicHome" MasterPageFile="~/Public.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link href="https://fonts.googleapis.com/css2?family=Outfit:wght@400;500;600;700;800&family=Inter:wght@400;500;600&display=swap" rel="stylesheet">

<style>
:root{
    --green:#4f8f68;
    --green-dark:#3f7756;
    --green-light:#eef7f0;
    --orange:#c96d35;
    --orange-dark:#ad5726;
    --dark:#26352d;
    --text:#52605a;
    --muted:#89938d;
    --light:#f8faf7;
    --white:#fff;
    --line:#e6ebe6;
}

body{
    background:#fff;
    color:var(--dark);
    font-family:"Inter",Arial,sans-serif;
}

h1,h2,h3,h4{
    font-family:"Outfit","Inter",Arial,sans-serif;
}

.home-wrapper{
    width:100%;
    max-width:100%;
    margin:0;
    overflow:hidden;
}

.hero-section{
    position:relative;
    width:100%;
    min-height:590px;
    display:flex;
    align-items:center;
    background:
        linear-gradient(
            100deg,
            rgba(24,45,34,.88) 0%,
            rgba(24,45,34,.70) 48%,
            rgba(24,45,34,.25) 100%
        ),
        url("https://images.unsplash.com/photo-1748944076823-9991da0a0634?w=1800&h=1100&fit=crop&auto=format&q=80");
    background-size:cover;
    background-position:center;
    padding:70px 0;
    box-sizing:border-box;
}

.hero-content{
    max-width:650px;
    position:relative;
    z-index:2;
}

.hero-badge{
    display:inline-flex;
    align-items:center;
    gap:9px;
    background:rgba(255,255,255,.12);
    color:#fff;
    padding:7px 17px 7px 7px;
    border:1px solid rgba(255,255,255,.25);
    border-radius:30px;
    font-size:12px;
    font-weight:600;
    margin-bottom:22px;
}

.hero-badge i{
    width:23px;
    height:23px;
    display:flex;
    align-items:center;
    justify-content:center;
    border-radius:50%;
    background:var(--orange);
    color:#fff;
    font-size:11px;
}

.hero-title{
    font-size:54px;
    line-height:1.08;
    font-weight:700;
    color:#fff!important;
    margin:0 0 20px;
    letter-spacing:-1px;
    text-shadow:0 3px 12px rgba(0,0,0,.35);
}

.hero-title span{
    color:#f2a476!important;
}

.hero-subtitle{
    max-width:550px;
    color:#fff!important;
    font-size:19px;
    line-height:1.6;
    margin:0 0 10px;
    font-weight:500;
}

.hero-description{
    max-width:510px;
    color:#e8eeea!important;
    font-size:14px;
    line-height:1.8;
    margin:0 0 30px;
}

.hero-buttons{
    display:flex;
    align-items:center;
    gap:13px;
    flex-wrap:wrap;
    margin-bottom:38px;
}

.btn-shop{
    display:inline-flex;
    align-items:center;
    justify-content:center;
    min-height:46px;
    padding:0 25px;
    background:var(--orange);
    color:#fff!important;
    border-radius:5px;
    font-size:14px;
    font-weight:600;
    text-decoration:none!important;
    transition:all .2s ease;
}

.btn-shop:hover{
    background:var(--orange-dark);
    color:#fff!important;
    text-decoration:none!important;
    transform:translateY(-2px);
}

.btn-explore{
    display:inline-flex;
    align-items:center;
    justify-content:center;
    min-height:46px;
    padding:0 23px;
    background:rgba(255,255,255,.08);
    color:#fff!important;
    border:1px solid rgba(255,255,255,.35);
    border-radius:5px;
    font-size:14px;
    font-weight:600;
    text-decoration:none!important;
    transition:all .2s ease;
}

.btn-explore:hover{
    background:rgba(255,255,255,.16);
    color:#fff!important;
    text-decoration:none!important;
}

.hero-stats{
    display:flex;
    gap:35px;
    flex-wrap:wrap;
}

.hero-stat{
    padding-left:14px;
    border-left:2px solid var(--orange);
}

.hero-stat strong{
    display:block;
    color:#fff!important;
    font-size:14px;
    font-weight:700;
}

.hero-stat span{
    display:block;
    color:#d9e2dc!important;
    font-size:11px;
    margin-top:3px;
}

.product-slider-section{
    padding:42px 0;
    background:#fff;
    overflow:hidden;
    border-bottom:1px solid var(--line);
}

.product-slider{
    width:100%;
    overflow:hidden;
    position:relative;
}

.product-slider-track{
    display:flex;
    width:max-content;
    animation:productRunning 30s linear infinite;
}

.slider-product{
    width:245px;
    height:190px;
    margin-right:18px;
    border-radius:7px;
    overflow:hidden;
    background:#f7f9f6;
    border:1px solid var(--line);
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
    transform:scale(1.06);
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

.section{
    padding:75px 0;
}

.section-header{
    max-width:650px;
    margin:0 auto 42px;
    text-align:center;
}

.section-label{
    color:var(--green);
    font-size:11px;
    font-weight:700;
    text-transform:uppercase;
    letter-spacing:1.7px;
    margin-bottom:9px;
}

.section-title{
    color:var(--dark);
    font-size:34px;
    font-weight:700;
    margin:0 0 12px;
    letter-spacing:-.5px;
}

.section-description{
    color:var(--text);
    font-size:14px;
    line-height:1.8;
    margin:0;
}

.category-card{
    display:block;
    position:relative;
    height:220px;
    border-radius:7px;
    overflow:hidden;
    text-decoration:none!important;
    box-shadow:0 8px 22px rgba(38,53,45,.10);
    transition:all .25s ease;
}

.category-card:hover{
    transform:translateY(-5px);
    box-shadow:0 16px 30px rgba(38,53,45,.15);
}

.category-card .cat-bg{
    position:absolute;
    inset:0;
    background-size:cover;
    background-position:center;
    transition:transform .4s ease;
}

.category-card:hover .cat-bg{
    transform:scale(1.06);
}

.category-card .cat-overlay{
    position:absolute;
    inset:0;
    background:
        linear-gradient(
            0deg,
            rgba(24,45,34,.85) 0%,
            rgba(24,45,34,.20) 65%,
            rgba(24,45,34,.05) 100%
        );
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
    display:flex;
    align-items:center;
    justify-content:center;
    background:var(--orange);
    color:#fff;
    border-radius:5px;
    font-size:15px;
    margin-bottom:11px;
}

.category-card h4{
    color:#fff;
    font-size:17px;
    font-weight:600;
    margin:0;
}

.category-card p{
    color:#e3ebe5;
    font-size:12px;
    margin:4px 0 0;
}

.products-section{
    background:#f8faf7;
    border-top:1px solid var(--line);
    border-bottom:1px solid var(--line);
}

.product-card{
    background:#fff;
    border:1px solid var(--line);
    border-radius:7px;
    overflow:hidden;
    height:100%;
    box-shadow:0 2px 10px rgba(38,53,45,.04);
    transition:all .25s ease;
}

.product-card:hover{
    transform:translateY(-5px);
    box-shadow:0 14px 28px rgba(38,53,45,.11);
    border-color:#d7e2da;
}

.product-image-box{
    height:225px;
    background:#f3f7f3;
    overflow:hidden;
}

.product-image{
    width:100%;
    height:100%;
    object-fit:cover;
    transition:transform .4s ease;
}

.product-card:hover .product-image{
    transform:scale(1.05);
}

.product-info{
    padding:18px 20px 20px;
}

.product-category{
    color:var(--green);
    font-size:10px;
    font-weight:700;
    text-transform:uppercase;
    letter-spacing:1px;
}

.product-name{
    color:var(--dark);
    font-size:17px;
    font-weight:600;
    line-height:1.35;
    margin:7px 0 4px;
}

.product-gat{
    color:var(--muted);
    font-size:11px;
    margin-bottom:13px;
}

.product-price{
    color:var(--orange);
    font-size:19px;
    font-weight:700;
    margin-bottom:15px;
}

.product-button{
    display:flex;
    align-items:center;
    justify-content:center;
    min-height:38px;
    background:var(--green);
    color:#fff!important;
    border-radius:4px;
    font-size:12px;
    font-weight:600;
    text-decoration:none!important;
    transition:all .2s ease;
}

.product-button:hover{
    background:var(--green-dark);
    color:#fff!important;
    text-decoration:none!important;
}

.cta-banner{
    background:#26352d;
    border-radius:8px;
    padding:48px 45px;
    display:flex;
    align-items:center;
    justify-content:space-between;
    flex-wrap:wrap;
    gap:25px;
}

.cta-banner h3{
    color:#fff;
    font-size:26px;
    font-weight:700;
    margin:0 0 8px;
}

.cta-banner p{
    color:#bdc9c1;
    font-size:13px;
    line-height:1.7;
    margin:0;
    max-width:500px;
}

@media(max-width:991px){
    .hero-title{
        font-size:43px;
    }

    .hero-section{
        min-height:540px;
    }

    .slider-product{
        width:220px;
        height:175px;
    }
}

@media(max-width:767px){
    .hero-section{
        min-height:520px;
        padding:60px 0;
    }

    .hero-title{
        font-size:35px;
    }

    .hero-subtitle{
        font-size:17px;
    }

    .hero-description{
        font-size:13px;
    }

    .hero-stats{
        gap:20px;
    }

    .section{
        padding:55px 0;
    }

    .section-title{
        font-size:28px;
    }

    .product-slider-section{
        padding:35px 0;
    }

    .slider-product{
        width:205px;
        height:165px;
        margin-right:13px;
    }

    .product-slider-track{
        animation-duration:24s;
    }

    .category-card{
        height:190px;
        margin-bottom:18px;
    }

    .product-image-box{
        height:210px;
    }

    .cta-banner{
        padding:38px 25px;
    }

    .cta-banner h3{
        font-size:23px;
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
&nbsp;
<i class="glyphicon glyphicon-arrow-right"></i>
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

<asp:Repeater ID="rptSliderProducts" runat="server">
<ItemTemplate>
<div class="slider-product">
<img src='<%# GetImageUrl(Eval("ImagePath")) %>' alt='<%# Eval("ImageName") %>' />
</div>
</ItemTemplate>
</asp:Repeater>

<asp:Repeater ID="rptSliderProductsDuplicate" runat="server">
<ItemTemplate>
<div class="slider-product">
<img src='<%# GetImageUrl(Eval("ImagePath")) %>' alt='<%# Eval("ImageName") %>' />
</div>
</ItemTemplate>
</asp:Repeater>

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
<h4>Food Products</h4>
<p>Homemade &amp; traditional foods</p>
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
<h4>Handmade Products</h4>
<p>Unique handmade creations</p>
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
<h4>Home Products</h4>
<p>Useful products for your home</p>
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
<h4>View All</h4>
<p>Explore all available products</p>
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
&nbsp;
<i class="glyphicon glyphicon-arrow-right"></i>
</a>

</div>
</div>

</div>

</ItemTemplate>
</asp:Repeater>

</div>

<div class="text-center" style="margin-top:5px;">

<a href="PublicProducts.aspx" class="btn-shop">
View All Products
&nbsp;
<i class="glyphicon glyphicon-arrow-right"></i>
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
&nbsp;
<i class="glyphicon glyphicon-arrow-right"></i>
</a>

</div>

</div>
</section>

</div>

</asp:Content>