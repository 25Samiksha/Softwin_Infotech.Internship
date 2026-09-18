<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="container-fluid">

    <h2 class="page-title">Products / Business</h2>

    <asp:Label ID="lblMessage" runat="server"></asp:Label>

    <asp:HiddenField ID="hfProductID" runat="server" />

    <div class="panel panel-primary">

        <div class="panel-heading">
            Product Information
        </div>

        <div class="panel-body">

            <div class="row">

                <div class="col-md-4">
                    <label>Bachat Gat</label>

                    <asp:DropDownList ID="ddlBachatGat"
                        runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>
                </div>

                <div class="col-md-4">
                    <label>Product Name</label>

                    <asp:TextBox ID="txtProductName"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>

                <div class="col-md-4">
                    <label>Category</label>

                    <asp:TextBox ID="txtCategory"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>

            </div>

            <br />

            <div class="row">

                <div class="col-md-4">
                    <label>Unit</label>

                    <asp:TextBox ID="txtUnit"
                        runat="server"
                        CssClass="form-control"
                        placeholder="Kg / Piece / Packet">
                    </asp:TextBox>
                </div>

                <div class="col-md-4">
                    <label>Quantity</label>

                    <asp:TextBox ID="txtQuantity"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>

                <div class="col-md-4">
                    <label>Status</label>

                    <asp:DropDownList ID="ddlStatus"
                        runat="server"
                        CssClass="form-control">

                        <asp:ListItem>Available</asp:ListItem>
                        <asp:ListItem>Out of Stock</asp:ListItem>
                        <asp:ListItem>Inactive</asp:ListItem>

                    </asp:DropDownList>
                </div>

            </div>

            <br />

            <div class="row">

                <div class="col-md-6">
                    <label>Cost Price</label>

                    <asp:TextBox ID="txtCostPrice"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>

                <div class="col-md-6">
                    <label>Selling Price</label>

                    <asp:TextBox ID="txtSellingPrice"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>

            </div>

            <br />

            <label>Description</label>

            <asp:TextBox ID="txtDescription"
                runat="server"
                CssClass="form-control"
                TextMode="MultiLine"
                Rows="3">
            </asp:TextBox>

            <br />

            <asp:Button ID="btnSave"
                runat="server"
                Text="Save Product"
                CssClass="btn btn-success"
                OnClick="btnSave_Click" />

            <asp:Button ID="btnClear"
                runat="server"
                Text="Clear"
                CssClass="btn btn-default"
                OnClick="btnClear_Click" />

        </div>
    </div>


    <div class="panel panel-default">

        <div class="panel-heading">
            Search Products
        </div>

        <div class="panel-body">

            <div class="row">

                <div class="col-md-10">

                    <asp:TextBox ID="txtSearch"
                        runat="server"
                        CssClass="form-control"
                        placeholder="Search product or category">
                    </asp:TextBox>

                </div>

                <div class="col-md-2">

                    <asp:Button ID="btnSearch"
                        runat="server"
                        Text="Search"
                        CssClass="btn btn-primary"
                        OnClick="btnSearch_Click" />

                </div>

            </div>

            <br />

            <asp:Button ID="btnShowAll"
                runat="server"
                Text="Show All"
                CssClass="btn btn-info"
                OnClick="btnShowAll_Click" />

        </div>
    </div>


    <asp:GridView ID="gvProducts"
        runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        DataKeyNames="ProductID"
        OnRowCommand="gvProducts_RowCommand">

        <Columns>

            <asp:BoundField DataField="ProductID"
                HeaderText="ID" />

            <asp:BoundField DataField="GatName"
                HeaderText="Bachat Gat" />

            <asp:BoundField DataField="ProductName"
                HeaderText="Product" />

            <asp:BoundField DataField="Category"
                HeaderText="Category" />

            <asp:BoundField DataField="Unit"
                HeaderText="Unit" />

            <asp:BoundField DataField="Quantity"
                HeaderText="Quantity" />

            <asp:BoundField DataField="CostPrice"
                HeaderText="Cost Price"
                DataFormatString="{0:N2}" />

            <asp:BoundField DataField="SellingPrice"
                HeaderText="Selling Price"
                DataFormatString="{0:N2}" />

            <asp:BoundField DataField="Status"
                HeaderText="Status" />

            <asp:ButtonField
                ButtonType="Button"
                CommandName="EditProduct"
                Text="Edit"
                ControlStyle-CssClass="btn btn-warning btn-xs" />

            <asp:ButtonField
                ButtonType="Button"
                CommandName="DeleteProduct"
                Text="Delete"
                ControlStyle-CssClass="btn btn-danger btn-xs" />

        </Columns>

    </asp:GridView>

</div>

</asp:Content>