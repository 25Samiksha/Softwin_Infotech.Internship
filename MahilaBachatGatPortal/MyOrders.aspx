<%@ Page Title="My Orders" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeFile="MyOrders.aspx.cs" Inherits="MyOrders" %>

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
.orders-page{
    background:var(--paper);
    min-height:100vh;
    padding:0 15px 80px;
    font-family:"Inter",Arial,sans-serif;
    color:var(--ink);
}
.orders-page h1,
.orders-page h2,
.orders-page h3,
.orders-page h4{
    font-family:"Outfit","Inter",Arial,sans-serif;
}
.orders-hero{
    position:relative;
    background-image:linear-gradient(100deg,rgba(15,25,37,.88) 0%,rgba(15,25,37,.72) 55%,rgba(15,25,37,.4) 100%),url("https://images.unsplash.com/photo-1748944077011-7bd68d84150c?w=1800&h=650&fit=crop&auto=format&q=80");
    background-size:cover;
    background-position:center;
    margin:0 -15px 45px;
    padding:70px 15px;
}
.orders-heading{
    position:relative;
    z-index:2;
    text-align:center;
}
.orders-label{
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
.orders-title{
    font-size:40px;
    font-weight:700;
    letter-spacing:-1px;
    color:#ffffff;
    margin:0 0 10px;
}
.orders-subtitle{
    color:#dfe4e8;
    font-size:15px;
    margin:0;
}
.order-card{
    background:var(--panel);
    border:1px solid var(--line);
    border-radius:14px;
    margin-bottom:25px;
    padding:26px;
    box-shadow:0 2px 10px rgba(24,40,58,.04);
    transition:all .25s ease;
}
.order-card:hover{
    transform:translateY(-3px);
    box-shadow:0 16px 34px rgba(24,40,58,.12);
    border-color:transparent;
}
.order-header{
    display:flex;
    justify-content:space-between;
    align-items:center;
    border-bottom:1px solid var(--line);
    padding-bottom:18px;
    margin-bottom:22px;
}
.order-id{
    font-size:18px;
    font-weight:650;
    color:var(--ink);
}
.order-id:before{
    content:"";
    display:inline-block;
    width:4px;
    height:20px;
    background:var(--accent);
    border-radius:4px;
    vertical-align:-4px;
    margin-right:10px;
}
.order-date{
    color:var(--ink-faint);
    font-size:13px;
}
.order-info{
    margin-bottom:25px;
}
.order-info > div{
    margin-bottom:15px;
    color:var(--ink);
    font-size:14px;
}
.order-info strong{
    display:inline-block;
    color:var(--accent-dark);
    font-size:11.5px;
    text-transform:uppercase;
    letter-spacing:.6px;
    margin-bottom:6px;
}
.status{
    display:inline-block;
    padding:6px 13px;
    border-radius:20px;
    font-size:12px;
    font-weight:700;
    margin-top:2px;
}
.status-placed{
    background:#fdf1dd;
    color:#946c00;
}
.status-confirmed{
    background:#e7f3ea;
    color:#36734a;
}
.status-processing{
    background:var(--accent-tint);
    color:var(--accent-dark);
}
.status-shipped{
    background:#eaf0f7;
    color:#2f4d6e;
}
.status-delivered{
    background:#e7f3ea;
    color:#39744d;
}
.status-cancelled{
    background:#fbe8e8;
    color:#9b4d4d;
}
.track-title{
    color:var(--ink);
    font-size:15px;
    font-weight:650;
    margin-bottom:20px;
    padding-top:20px;
    border-top:1px solid var(--line);
    display:flex;
    align-items:center;
    gap:7px;
}
.track-title .glyphicon{
    color:var(--accent);
}
.tracking{
    display:flex;
    justify-content:space-between;
    position:relative;
    margin:25px 0 5px;
}
.tracking:before{
    content:"";
    position:absolute;
    top:15px;
    left:8%;
    right:8%;
    height:2px;
    background:var(--line);
}
.track-step{
    position:relative;
    text-align:center;
    width:20%;
    z-index:1;
}
.track-circle{
    width:31px;
    height:31px;
    line-height:31px;
    border-radius:50%;
    background:#eef0f2;
    color:var(--ink-faint);
    margin:0 auto 9px;
    font-size:12px;
    font-weight:700;
    border:3px solid var(--paper);
}
.track-step.active .track-circle{
    background:var(--accent);
    color:#ffffff;
    box-shadow:0 0 0 3px var(--accent-tint);
}
.track-label{
    font-size:12px;
    color:var(--ink-faint);
}
.track-step.active .track-label{
    color:var(--ink);
    font-weight:700;
}
.empty-orders{
    background:var(--panel);
    border:1px solid var(--line);
    border-radius:14px;
    padding:60px 30px;
    text-align:center;
    color:var(--ink-soft);
    box-shadow:0 2px 10px rgba(24,40,58,.04);
}
.empty-orders .glyphicon{
    display:inline-block;
    font-size:40px;
    color:var(--accent);
    margin-bottom:15px;
}
.empty-orders h3{
    color:var(--ink);
    font-size:22px;
    font-weight:700;
    margin:5px 0 10px;
}
.empty-orders p{
    color:var(--ink-soft);
    font-size:14px;
    margin-bottom:0;
}
.shop-button{
    margin-top:22px;
    background:var(--accent) !important;
    border-color:var(--accent) !important;
    border-radius:7px;
    padding:10px 22px;
    font-weight:600;
    transition:all .2s ease;
}
.shop-button:hover{
    background:var(--accent-dark) !important;
    border-color:var(--accent-dark) !important;
}
@media(max-width:767px){
    .orders-page{
        padding:0 12px 60px;
    }
    .orders-hero{
        margin:0 -12px 35px;
        padding:55px 12px;
    }
    .orders-title{
        font-size:30px;
    }
    .orders-subtitle{
        font-size:14px;
        line-height:1.6;
    }
    .order-card{
        padding:20px;
    }
    .order-header{
        display:block;
    }
    .order-date{
        display:block;
        margin-top:8px;
    }
    .tracking:before{
        display:none;
    }
    .tracking{
        display:block;
        margin-top:20px;
    }
    .track-step{
        width:100%;
        display:flex;
        align-items:center;
        text-align:left;
        margin-bottom:14px;
    }
    .track-circle{
        margin:0 12px 0 0;
        flex-shrink:0;
    }
}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="orders-page">

<div class="orders-hero">
<div class="container">
<div class="orders-heading">

<div class="orders-label">
<span class="glyphicon glyphicon-list-alt"></span>
Order History
</div>

<div class="orders-title">
My Orders
</div>

<p class="orders-subtitle">
View your purchases and track your order status
</p>

</div>
</div>
</div>

<div class="container">

<asp:Panel ID="pnlOrders" runat="server">

<asp:Repeater ID="rptOrders" runat="server">
<ItemTemplate>

<div class="order-card">

<div class="order-header">

<div class="order-id">
Order #<%# Eval("OrderID") %>
</div>

<div class="order-date">
<span class="glyphicon glyphicon-calendar"></span>
&nbsp;
<%# Eval("OrderDate", "{0:dd MMM yyyy, hh:mm tt}") %>
</div>

</div>

<div class="row order-info">

<div class="col-md-3">
<strong>Product</strong><br />
<%# Eval("ProductName") %>
</div>

<div class="col-md-2">
<strong>Quantity</strong><br />
<%# Eval("Quantity") %>
</div>

<div class="col-md-2">
<strong>Amount</strong><br />
₹ <%# Eval("TotalAmount", "{0:N2}") %>
</div>

<div class="col-md-2">
<strong>Payment</strong><br />
<%# Eval("PaymentStatus") %>
</div>

<div class="col-md-3">
<strong>Status</strong><br />

<span class='status <%# GetStatusClass(Eval("OrderStatus").ToString()) %>'>
<%# Eval("OrderStatus") %>
</span>

</div>

</div>

<div class="track-title">
<span class="glyphicon glyphicon-road"></span>
Track Order
</div>

<div class="tracking">

<div class='track-step <%# GetStepClass(Eval("OrderStatus").ToString(), 1) %>'>
<div class="track-circle">1</div>
<div class="track-label">Placed</div>
</div>

<div class='track-step <%# GetStepClass(Eval("OrderStatus").ToString(), 2) %>'>
<div class="track-circle">2</div>
<div class="track-label">Confirmed</div>
</div>

<div class='track-step <%# GetStepClass(Eval("OrderStatus").ToString(), 3) %>'>
<div class="track-circle">3</div>
<div class="track-label">Processing</div>
</div>

<div class='track-step <%# GetStepClass(Eval("OrderStatus").ToString(), 4) %>'>
<div class="track-circle">4</div>
<div class="track-label">Shipped</div>
</div>

<div class='track-step <%# GetStepClass(Eval("OrderStatus").ToString(), 5) %>'>
<div class="track-circle">5</div>
<div class="track-label">Delivered</div>
</div>

</div>

</div>

</ItemTemplate>
</asp:Repeater>

</asp:Panel>

<asp:Panel ID="pnlEmpty" runat="server" Visible="false">

<div class="empty-orders">

<span class="glyphicon glyphicon-list-alt"></span>

<h3>
No Orders Found
</h3>

<p>
You have not placed any orders yet.
</p>

<a href="PublicProducts.aspx" class="btn btn-primary shop-button">
Shop Products
</a>

</div>

</asp:Panel>

</div>

</div>
</asp:Content>