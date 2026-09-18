<%@ Page Title="Sales Management" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeFile="Sales.aspx.cs" Inherits="Sales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="container-fluid">

    <h2 class="page-title">Sales Management</h2>

    <asp:Label ID="lblMessage" runat="server"></asp:Label>

    <div class="panel panel-primary">

        <div class="panel-heading">
            New Sale
        </div>

        <div class="panel-body">

            <div class="row">

                <div class="col-md-4">
                    <label>Bachat Gat</label>

                    <asp:DropDownList ID="ddlBachatGat"
                        runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlBachatGat_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <div class="col-md-4">
                    <label>Product</label>

                    <asp:DropDownList ID="ddlProduct"
                        runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <div class="col-md-4">
                    <label>Sale Date</label>

                    <asp:TextBox ID="txtSaleDate"
                        runat="server"
                        CssClass="form-control"
                        TextMode="SingleLine">
                    </asp:TextBox>
                </div>

            </div>

            <br />

            <div class="row">

                <div class="col-md-4">
                    <label>Customer Name</label>

                    <asp:TextBox ID="txtCustomerName"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>

                <div class="col-md-4">
                    <label>Customer Mobile</label>

                    <asp:TextBox ID="txtCustomerMobile"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>

                <div class="col-md-4">
                    <label>Available Stock</label>

                    <asp:TextBox ID="txtStock"
                        runat="server"
                        CssClass="form-control"
                        ReadOnly="true">
                    </asp:TextBox>
                </div>

            </div>

            <br />

            <div class="row">

                <div class="col-md-4">
                    <label>Quantity</label>

                    <asp:TextBox ID="txtQuantity"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>

                <div class="col-md-4">
                    <label>Unit Price</label>

                    <asp:TextBox ID="txtUnitPrice"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>

                <div class="col-md-4">
                    <label>Total Amount</label>

                    <asp:TextBox ID="txtTotalAmount"
                        runat="server"
                        CssClass="form-control"
                        ReadOnly="true">
                    </asp:TextBox>
                </div>

            </div>

            <br />

            <div class="row">

                <div class="col-md-4">
                    <label>Payment Mode</label>

                    <asp:DropDownList ID="ddlPaymentMode"
                        runat="server"
                        CssClass="form-control">

                        <asp:ListItem>Cash</asp:ListItem>
                        <asp:ListItem>UPI</asp:ListItem>
                        <asp:ListItem>Bank</asp:ListItem>
                        <asp:ListItem>Cheque</asp:ListItem>

                    </asp:DropDownList>
                </div>

                <div class="col-md-4">
                    <label>Receipt Number</label>

                    <asp:TextBox ID="txtReceiptNumber"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>

                <div class="col-md-4">
                    <label>Sold By User ID</label>

                    <asp:TextBox ID="txtSoldBy"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>

            </div>

            <br />

            <label>Remarks</label>

            <asp:TextBox ID="txtRemarks"
                runat="server"
                CssClass="form-control"
                TextMode="MultiLine"
                Rows="3">
            </asp:TextBox>

            <br />

            <asp:Button ID="btnSave"
                runat="server"
                Text="Save Sale"
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
            Sales History
        </div>

        <div class="panel-body">

            <asp:TextBox ID="txtSearch"
                runat="server"
                CssClass="form-control"
                placeholder="Search customer or product">
            </asp:TextBox>

            <br />

            <asp:Button ID="btnSearch"
                runat="server"
                Text="Search"
                CssClass="btn btn-primary"
                OnClick="btnSearch_Click" />

            <asp:Button ID="btnShowAll"
                runat="server"
                Text="Show All"
                CssClass="btn btn-info"
                OnClick="btnShowAll_Click" />

        </div>

    </div>


    <asp:GridView ID="gvSales"
        runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        DataKeyNames="SaleID">

        <Columns>

            <asp:BoundField DataField="SaleID"
                HeaderText="ID" />

            <asp:BoundField DataField="GatName"
                HeaderText="Bachat Gat" />

            <asp:BoundField DataField="ProductName"
                HeaderText="Product" />

            <asp:BoundField DataField="SaleDate"
                HeaderText="Date"
                DataFormatString="{0:dd-MM-yyyy}" />

            <asp:BoundField DataField="CustomerName"
                HeaderText="Customer" />

            <asp:BoundField DataField="Quantity"
                HeaderText="Quantity" />

            <asp:BoundField DataField="UnitPrice"
                HeaderText="Unit Price"
                DataFormatString="{0:N2}" />

            <asp:BoundField DataField="TotalAmount"
                HeaderText="Total"
                DataFormatString="{0:N2}" />

            <asp:BoundField DataField="PaymentMode"
                HeaderText="Payment" />

            <asp:BoundField DataField="ReceiptNumber"
                HeaderText="Receipt" />

        </Columns>

    </asp:GridView>

</div>

</asp:Content>