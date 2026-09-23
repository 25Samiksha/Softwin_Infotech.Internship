<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="User" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta charset="utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1" />
<title>Login | Mahila Bachat Gat</title>
<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" rel="stylesheet" />
<style>
body{
    margin:0;
    min-height:100vh;
    background-image:linear-gradient(rgba(17,47,72,.72),rgba(17,47,72,.72)),url("images/login-bg.jpg");
    background-size:cover;
    background-position:center;
    background-repeat:no-repeat;
    background-attachment:fixed;
    font-family:"Segoe UI",Arial,sans-serif;
}
.login-container{
    width:400px;
    margin:70px auto;
}
.login-panel{
    padding:32px;
    background:rgba(255,255,255,.97);
    border-radius:16px;
    box-shadow:0 18px 50px rgba(0,0,0,.22);
    border:1px solid rgba(255,255,255,.7);
}
.login-title{
    text-align:center;
    margin:0 0 12px;
    color:#183b5b;
    font-size:27px;
    font-weight:700;
}
.text-center{
    color:#6b7f92;
    font-weight:500;
    margin-bottom:18px;
}
hr{
    border-top:1px solid #e1e9f0;
    margin:18px 0 25px;
}
.form-group{
    margin-bottom:18px;
}
.form-group label{
    color:#40566b;
    font-size:13px;
    font-weight:600;
    margin-bottom:7px;
}
.form-control{
    height:44px;
    border:1px solid #cfdae4;
    border-radius:7px;
    box-shadow:none;
    color:#34495e;
}
.form-control:focus{
    border-color:#2f78b7;
    box-shadow:0 0 0 3px rgba(47,120,183,.10);
}
.btn-login{
    width:100%;
    height:44px;
    border:0;
    border-radius:7px;
    background:#2f78b7;
    font-weight:600;
    font-size:14px;
    transition:all .2s ease;
}
.btn-login:hover{
    background:#245f91;
}
.message{
    display:block;
    margin-top:15px;
    text-align:center;
}
.signup-link{
    text-align:center;
    margin-top:20px;
    color:#718397;
    font-size:13px;
}
.signup-link a{
    color:#2f78b7;
    font-weight:600;
    text-decoration:none;
}
.signup-link a:hover{
    color:#245f91;
    text-decoration:none;
}
.signup-panel{
    display:none;
}
@media(max-width:767px){
    body{
        background-attachment:scroll;
    }
    .login-container{
        width:auto;
        max-width:400px;
        margin:35px 18px;
    }
    .login-panel{
        padding:25px 22px;
    }
    .login-title{
        font-size:24px;
    }
}
</style>
</head>
<body>
<form id="form1" runat="server">
<div class="login-container">
<div class="login-panel">

<div id="loginPanel">

<h2 class="login-title">
Mahila Bachat Gat
</h2>

<h4 class="text-center">
Sign In
</h4>

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

<h2 class="login-title">
Create Account
</h2>

<h4 class="text-center">
New Customer
</h4>

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