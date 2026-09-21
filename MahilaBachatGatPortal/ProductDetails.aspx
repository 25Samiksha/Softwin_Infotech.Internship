<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductDetails.aspx.cs" Inherits="ProductDetails" MasterPageFile="~/Public.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="container">

    <asp:Label ID="lblMessage"
        runat="server"
        style="display:block;text-align:center;font-size:16px;">
    </asp:Label>

    <asp:Panel ID="pnlProduct" runat="server">

        <div class="row" style="padding:40px 20px;">

            <div class="col-md-5 text-center">

                <asp:Image
                    ID="imgProduct"
                    runat="server"
                    style="max-width:100%;max-height:400px;" />

            </div>

            <div class="col-md-7">

                <h1 style="color:#2f6f9f;">
                    <asp:Label ID="lblProductName" runat="server"></asp:Label>
                </h1>

                <hr />

                <p>
                    <strong>Category:</strong>
                    <asp:Label ID="lblCategory" runat="server"></asp:Label>
                </p>

                <p>
                    <strong>Description:</strong>
                </p>

                <p>
                    <asp:Label ID="lblDescription" runat="server"></asp:Label>
                </p>

                <p>
                    <strong>Available Quantity:</strong>
                    <asp:Label ID="lblQuantity" runat="server"></asp:Label>
                    <asp:Label ID="lblUnit" runat="server"></asp:Label>
                </p>

                <p style="font-size:24px;color:#2f6f9f;">
                    <strong>
                        ₹ <asp:Label ID="lblSellingPrice" runat="server"></asp:Label>
                    </strong>
                </p>

                <div style="margin-top:30px;">

                    <asp:Button
                        ID="btnBuyNow"
                        runat="server"
                        Text="Buy Now"
                        CssClass="btn btn-primary btn-lg"
                        OnClick="btnBuyNow_Click" />

                </div>

            </div>

        </div>

    </asp:Panel>

</div>

</asp:Content>