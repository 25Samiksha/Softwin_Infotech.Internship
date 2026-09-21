<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PublicProducts.aspx.cs" Inherits="PublicProducts" MasterPageFile="~/Public.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="container">
    <div class="text-center" style="padding:40px 20px 30px 20px;">
        <h2 style="font-size:32px;font-weight:700;color:#2f6f9f;">Products</h2>
        <p style="font-size:16px;color:#777;">Explore products created and sold by Mahila Bachat Gats</p>
    </div>

    <asp:Label ID="lblMessage" runat="server" style="display:block;text-align:center;font-size:16px;"></asp:Label>

    <div class="row">
        <asp:Repeater ID="rptProducts" runat="server">
            <ItemTemplate>
                <div class="col-md-4">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div style="height:180px;text-align:center;margin-bottom:15px;">
                                <asp:Image ID="imgProduct"
                                    runat="server"
                                    ImageUrl='<%# GetImageUrl(Eval("ProductImage")) %>'
                                    AlternateText='<%# Eval("ProductName") %>'
                                    style="max-width:100%;max-height:180px;" />
                            </div>

                            <h3 style="color:#2f6f9f;"><%# Eval("ProductName") %></h3>

                            <p><strong>Category:</strong> <%# Eval("Category") %></p>

                            <p><strong>Description:</strong> <%# Eval("Description") %></p>

                            <p>
                                <strong>Available Quantity:</strong>
                                <%# Eval("Quantity") %> <%# Eval("Unit") %>
                            </p>

                            <p style="font-size:18px;">
                                <strong>Selling Price:</strong>
                                ₹ <%# Eval("SellingPrice", "{0:N2}") %>
                            </p>

                            <a href='<%# "ProductDetails.aspx?ProductID=" + Eval("ProductID") %>' class="btn btn-primary">View Product</a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
</asp:Content>