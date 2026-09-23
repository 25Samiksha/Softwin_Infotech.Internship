<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Payment.aspx.cs" Inherits="Payment" MasterPageFile="~/Public.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
.payment-container{max-width:800px;margin:30px auto}
.payment-panel{background:#fff;padding:30px;border-radius:8px;box-shadow:0 2px 10px rgba(0,0,0,.12)}
.payment-title{text-align:center;color:#2f6f9f;margin-bottom:30px}
.amount-box{background:#f8fafc;border:1px solid #e5e9ef;border-radius:6px;padding:20px;text-align:center;margin-bottom:30px}
.amount-label{font-size:18px;font-weight:600}
.amount{font-size:26px;font-weight:700;color:#2f6f9f}
.payment-option{border:1px solid #ddd;border-radius:6px;padding:15px;margin-bottom:12px}
.online-payment-box{margin-top:20px;padding:25px;border:1px solid #e1e5ea;border-radius:8px;background:#fafcff;text-align:center}
.qr-title{color:#2f6f9f;margin-bottom:10px}
.qr-text{color:#666;margin-bottom:20px}
.qr-image{width:280px;height:280px;max-width:100%;object-fit:contain;border:1px solid #ddd;border-radius:8px;padding:5px;background:#fff}
.qr-amount{font-size:18px;font-weight:600;margin-top:15px}
.payment-confirm{margin-top:15px;display:block}
.payment-field{text-align:left;margin-top:20px}
.payment-field label{font-weight:600}
.payment-field .form-control{margin-top:6px}
.btn-pay{width:100%;margin-top:20px}
.message{display:block;text-align:center;margin-top:15px}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="payment-container">
<div class="payment-panel">
<h2 class="payment-title">Payment</h2>

<div class="amount-box">
<div class="amount-label">Total Amount</div>
<div class="amount">₹ <asp:Label ID="lblTotalAmount" runat="server"></asp:Label></div>
</div>

<h4>Choose Payment Method</h4>

<div class="payment-option">
<asp:RadioButton ID="rbCOD" runat="server" GroupName="PaymentMethod" Text=" Cash on Delivery" Checked="true" AutoPostBack="true" OnCheckedChanged="rbCOD_CheckedChanged" />
</div>

<div class="payment-option">
<asp:RadioButton ID="rbOnline" runat="server" GroupName="PaymentMethod" Text=" Online Payment" AutoPostBack="true" OnCheckedChanged="rbOnline_CheckedChanged" />
</div>

<asp:Panel ID="pnlOnlinePayment" runat="server" Visible="false" CssClass="online-payment-box">

<h4 class="qr-title">Scan & Pay with PhonePe</h4>

<p class="qr-text">Scan the QR code using PhonePe and pay the exact amount shown above.</p>

<asp:Image ID="imgPhonePeQR" runat="server" ImageUrl="~/Images/PhonePeQR.jpeg" CssClass="qr-image" AlternateText="PhonePe QR Code" />

<div class="qr-amount">
Amount to Pay: ₹ <asp:Label ID="lblQRAmount" runat="server"></asp:Label>
</div>

<div class="payment-field">
<label>UTR Number</label>
<asp:TextBox ID="txtUTRNumber" runat="server" CssClass="form-control" placeholder="Enter UTR / Transaction Number"></asp:TextBox>
</div>

<div class="payment-field">
<label>Payment Screenshot</label>
<asp:FileUpload ID="fuPaymentScreenshot" runat="server" CssClass="form-control" />
</div>

<asp:CheckBox ID="chkPaymentCompleted" runat="server" Text=" I have completed the payment" CssClass="payment-confirm" />

</asp:Panel>

<asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" CssClass="btn btn-primary btn-pay" OnClick="btnPlaceOrder_Click" />

<asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>

</div>
</div>
</asp:Content>