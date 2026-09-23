<%@ Page Title="My Orders" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeFile="MyOrders.aspx.cs" Inherits="MyOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
.orders-page{
    background:#f4f8fc;
    min-height:100vh;
    padding:0 15px 80px;
}
.orders-hero{
    position:relative;
    background-image:linear-gradient(rgba(24,59,91,.78),rgba(24,59,91,.78)),url("images/my-orders-bg.jpg");
    background-size:cover;
    background-position:center;
    background-repeat:no-repeat;
    margin:0 -15px 45px;
    padding:70px 15px;
    overflow:hidden;
}
.orders-hero:before{
    content:"";
    position:absolute;
    width:320px;
    height:320px;
    border-radius:50%;
    background:rgba(255,255,255,.06);
    top:-150px;
    right:-70px;
}
.orders-hero:after{
    content:"";
    position:absolute;
    width:220px;
    height:220px;
    border-radius:50%;
    background:rgba(120,185,225,.08);
    bottom:-120px;
    left:-60px;
}
.orders-heading{
    position:relative;
    z-index:2;
    text-align:center;
}
.orders-label{
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
.orders-title{
    font-size:40px;
    font-weight:750;
    color:#ffffff;
    margin:0 0 10px;
}
.orders-subtitle{
    color:#e1edf5;
    font-size:15px;
    margin:0;
}
.order-card{
    background:#ffffff;
    border:1px solid #dce7f1;
    border-radius:16px;
    margin-bottom:25px;
    padding:26px;
    box-shadow:0 10px 30px rgba(24,59,91,.07);
    transition:all .25s ease;
}
.order-card:hover{
    transform:translateY(-3px);
    box-shadow:0 16px 38px rgba(24,59,91,.12);
    border-color:#c5d9ea;
}
.order-header{
    display:flex;
    justify-content:space-between;
    align-items:center;
    border-bottom:1px solid #e7eef5;
    padding-bottom:18px;
    margin-bottom:22px;
}
.order-id{
    font-size:19px;
    font-weight:700;
    color:#183b5b;
}
.order-id:before{
    content:"";
    display:inline-block;
    width:4px;
    height:20px;
    background:#2f78b7;
    border-radius:4px;
    vertical-align:-4px;
    margin-right:10px;
}
.order-date{
    color:#718397;
    font-size:13px;
}
.order-info{
    margin-bottom:25px;
}
.order-info > div{
    margin-bottom:15px;
    color:#40566b;
    font-size:14px;
}
.order-info strong{
    display:inline-block;
    color:#2f78b7;
    font-size:12px;
    text-transform:uppercase;
    letter-spacing:.5px;
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
    background:#fff4d6;
    color:#946c00;
}
.status-confirmed{
    background:#e3f3e8;
    color:#36734a;
}
.status-processing{
    background:#e3f0f8;
    color:#326582;
}
.status-shipped{
    background:#dcecff;
    color:#245b91;
}
.status-delivered{
    background:#e2f3e8;
    color:#39744d;
}
.status-cancelled{
    background:#f9e3e5;
    color:#9b4d55;
}
.track-title{
    color:#183b5b;
    font-size:16px;
    font-weight:700;
    margin-bottom:20px;
    padding-top:20px;
    border-top:1px solid #e7eef5;
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
    background:#dce7f1;
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
    background:#e2eaf2;
    color:#718397;
    margin:0 auto 9px;
    font-size:12px;
    font-weight:700;
    border:3px solid #f4f8fc;
}
.track-step.active .track-circle{
    background:#2f78b7;
    color:#ffffff;
    box-shadow:0 0 0 3px #dcecff;
}
.track-label{
    font-size:12px;
    color:#7b8d9e;
}
.track-step.active .track-label{
    color:#244b6c;
    font-weight:700;
}
.empty-orders{
    background:#ffffff;
    border:1px solid #dce7f1;
    border-radius:16px;
    padding:60px 30px;
    text-align:center;
    color:#718397;
    box-shadow:0 10px 30px rgba(24,59,91,.06);
}
.empty-orders .glyphicon{
    display:inline-block;
    font-size:42px;
    color:#8db3d3;
    margin-bottom:15px;
}
.empty-orders h3{
    color:#183b5b;
    font-size:23px;
    font-weight:700;
    margin:5px 0 10px;
}
.empty-orders p{
    color:#718397;
    font-size:14px;
    margin-bottom:0;
}
.shop-button{
    margin-top:22px;
    background:#2f78b7 !important;
    border-color:#2f78b7 !important;
    border-radius:7px;
    padding:10px 20px;
    font-weight:600;
    transition:all .2s ease;
}
.shop-button:hover{
    background:#245f91 !important;
    border-color:#245f91 !important;
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
&nbsp; Order History
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
&nbsp; Track Order
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