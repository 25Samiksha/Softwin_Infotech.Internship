<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="User" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta charset="utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1" />
<title>Login | Mahila Bachat Gat</title>
<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" rel="stylesheet" />
<style>
body {
    background-color: #f5f7fa;
}
.login-container {
    width: 400px;
    margin: 100px auto;
}
.login-panel {
    padding: 30px;
    background: white;
    border-radius: 8px;
    box-shadow: 0 2px 10px rgba(0,0,0,0.15);
}
.login-title {
    text-align: center;
    margin-bottom: 25px;
}
.btn-login {
    width: 100%;
}
.message {
    display: block;
    margin-top: 15px;
    text-align: center;
}
.signup-link {
    text-align: center;
    margin-top: 18px;
}
.signup-panel {
    display: none;
}
</style>
</head>
<body>
<form id="form1" runat="server">
<div class="login-container">
<div class="login-panel">

<div id="loginPanel">
<h2 class="login-title">Mahila Bachat Gat</h2>
<h4 class="text-center">Sign In</h4>
<hr />

<div class="form-group">
<label>Username</label>
<asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter username"></asp:TextBox>
</div>

<div class="form-group">
<label>Password</label>
<asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter password"></asp:TextBox>
</div>

<asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary btn-login" OnClick="btnLogin_Click" />

<asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>

<div class="signup-link">
<span>New customer?</span>
<a href="javascript:void(0);" onclick="showSignup();">Create your account</a>
</div>
</div>

<div id="signupPanel" class="signup-panel">
<h2 class="login-title">Create Account</h2>
<h4 class="text-center">New Customer</h4>
<hr />

<div class="form-group">
<label>Full Name</label>
<asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Enter your name"></asp:TextBox>
</div>

<div class="form-group">
<label>Mobile Number</label>
<asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="Enter mobile number"></asp:TextBox>
</div>

<div class="form-group">
<label>Email</label>
<asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter email"></asp:TextBox>
</div>

<div class="form-group">
<label>Username</label>
<asp:TextBox ID="txtNewUsername" runat="server" CssClass="form-control" placeholder="Create username"></asp:TextBox>
</div>

<div class="form-group">
<label>Password</label>
<asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Create password"></asp:TextBox>
</div>

<div class="form-group">
<label>Confirm Password</label>
<asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Confirm password"></asp:TextBox>
</div>

<asp:Button ID="btnCreateAccount" runat="server" Text="Create Account" CssClass="btn btn-primary btn-login" OnClick="btnCreateAccount_Click" />

<asp:Label ID="lblSignupMessage" runat="server" CssClass="message"></asp:Label>

<div class="signup-link">
<span>Already have an account?</span>
<a href="javascript:void(0);" onclick="showLogin();">Sign In</a>
</div>
</div>

</div>
</div>
</form>

<script type="text/javascript">
    function showSignup() {
        document.getElementById("loginPanel").style.display = "none";
        document.getElementById("signupPanel").style.display = "block";
    }

    function showLogin() {
        document.getElementById("signupPanel").style.display = "none";
        document.getElementById("loginPanel").style.display = "block";
    }
</script>

</body>
</html>