<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PublicHome.aspx.cs" Inherits="PublicHome" MasterPageFile="~/Public.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="container">
    <div class="text-center" style="padding:60px 20px 40px 20px;">
        <h1 style="font-size:38px;font-weight:700;color:#2f6f9f;">Mahila Bachat Gat Portal</h1>
        <p style="font-size:18px;color:#666;margin-top:15px;">Empowering Women Through Self Help Groups</p>
        <p style="font-size:15px;color:#777;max-width:700px;margin:20px auto;">
            Explore products created and sold through Mahila Bachat Gats.
        </p>
    </div>

    <div class="row">
        <div class="col-md-6 col-md-offset-3">
            <div class="panel panel-default text-center">
                <div class="panel-body" style="padding:30px;">
                    <span class="glyphicon glyphicon-shopping-cart" style="font-size:45px;color:#2f6f9f;"></span>
                    <h3>Products</h3>
                    <p>
                        Explore products created and sold by Mahila Bachat Gats.
                    </p>
                    <a href="PublicProducts.aspx" class="btn btn-primary">Explore Products</a>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>