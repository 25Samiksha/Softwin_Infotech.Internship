<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sales.aspx.cs" Inherits="Sales" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
.order-container{padding:25px}
.order-title{color:#2f6f9f;margin-bottom:25px}
.order-panel{background:#fff;padding:20px;border-radius:8px;box-shadow:0 2px 8px rgba(0,0,0,.08);margin-bottom:20px}
.order-table{width:100%;border-collapse:collapse}
.order-table th{background:#2f6f9f;color:#fff;padding:12px;text-align:left}
.order-table td{padding:10px;border-bottom:1px solid #e5e5e5;vertical-align:middle}
.payment-proof{width:100px;height:100px;object-fit:cover;border:1px solid #ddd;border-radius:6px}
.btn-verify{margin-right:5px}
.status-pending{color:#d68910;font-weight:600}
.status-paid{color:#198754;font-weight:600}
.status-rejected{color:#dc3545;font-weight:600}
.message{display:block;margin-bottom:15px}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="order-container">
<h2 class="order-title">Order Management</h2>

<div class="order-panel">
<asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>

<div class="table-responsive">
<asp:GridView
    ID="gvOrders"
    runat="server"
    AutoGenerateColumns="False"
    CssClass="table table-bordered table-hover"
    EmptyDataText="No orders found.">

    <Columns>

        <asp:BoundField
            DataField="OrderID"
            HeaderText="Order ID" />

        <asp:BoundField
            DataField="GatName"
            HeaderText="Bachat Gat" />

        <asp:BoundField
            DataField="ProductName"
            HeaderText="Product" />

        <asp:BoundField
            DataField="OrderDate"
            HeaderText="Order Date"
            DataFormatString="{0:dd-MM-yyyy HH:mm}" />

        <asp:BoundField
            DataField="CustomerName"
            HeaderText="Customer" />

        <asp:BoundField
            DataField="Mobile"
            HeaderText="Mobile" />

        <asp:BoundField
            DataField="Quantity"
            HeaderText="Qty" />

        <asp:BoundField
            DataField="UnitPrice"
            HeaderText="Unit Price"
            DataFormatString="₹ {0:N2}" />

        <asp:BoundField
            DataField="TotalAmount"
            HeaderText="Total"
            DataFormatString="₹ {0:N2}" />

        <asp:BoundField
            DataField="PaymentMode"
            HeaderText="Payment Mode" />

        <asp:TemplateField HeaderText="Payment Status">
            <ItemTemplate>
                <asp:Label
                    ID="lblPaymentStatus"
                    runat="server"
                    Text='<%# Eval("PaymentStatus") %>'
                    CssClass='<%# GetPaymentStatusClass(Eval("PaymentStatus")) %>'>
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="UTR Number">
            <ItemTemplate>
                <asp:Label
                    ID="lblUTR"
                    runat="server"
                    Text='<%# Eval("UTRNumber") %>'>
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Payment Screenshot">
            <ItemTemplate>
                <asp:Image
                    ID="imgPaymentScreenshot"
                    runat="server"
                    CssClass="payment-proof"
                    ImageUrl='<%# GetPaymentScreenshotUrl(Eval("PaymentScreenshot")) %>'
                    Visible='<%# HasPaymentScreenshot(Eval("PaymentScreenshot")) %>' />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField
            DataField="OrderStatus"
            HeaderText="Order Status" />

        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>

                <asp:Button
                    ID="btnVerify"
                    runat="server"
                    Text="Verify Payment"
                    CssClass="btn btn-success btn-sm btn-verify"
                    CommandName="VerifyPayment"
                    CommandArgument='<%# Eval("OrderID") %>'
                    OnCommand="OrderCommand" />

                <asp:Button
                    ID="btnReject"
                    runat="server"
                    Text="Reject Payment"
                    CssClass="btn btn-danger btn-sm"
                    CommandName="RejectPayment"
                    CommandArgument='<%# Eval("OrderID") %>'
                    OnCommand="OrderCommand" />

            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
</asp:GridView>
</div>
</div>
</div>
</asp:Content>