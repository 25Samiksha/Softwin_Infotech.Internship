<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="User" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta charset="utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1" />
<title>Login | Mahila Bachat Gat</title>
<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" rel="stylesheet" />
<link href="https://fonts.googleapis.com/css2?family=Outfit:wght@500;600;700;800&family=Inter:wght@400;500;600&display=swap" rel="stylesheet" />
<style>
:root{
    --ink:#1c2530;
    --ink-soft:#5b6672;
    --ink-faint:#93a0ad;
    --line:#e8e4dd;
    --accent:#c1652f;
    --accent-dark:#a2521f;
    --accent-tint:#fbeee5;
}
*{
    box-sizing:border-box;
}
body{
    margin:0;
    min-height:100vh;
    background-image:linear-gradient(100deg,rgba(15,25,37,.82) 0%,rgba(15,25,37,.6) 55%,rgba(15,25,37,.4) 100%),url("https://images.unsplash.com/photo-1748944080331-30fe7088ff11?w=1800&h=1200&fit=crop&auto=format&q=80");
    background-size:cover;
    background-position:center;
    background-repeat:no-repeat;
    background-attachment:fixed;
    font-family:"Inter",Arial,sans-serif;
    display:flex;
    align-items:center;
    justify-content:center;
    padding:40px 18px;
}
.login-container{
    width:410px;
}
.brand-header{
    text-align:center;
    margin-bottom:20px;
}
.brand-badge{
    display:inline-flex;
    align-items:center;
    gap:8px;
    background:rgba(255,255,255,.14);
    color:#f3ded2;
    border:1px solid rgba(255,255,255,.25);
    padding:7px 16px;
    border-radius:30px;
    font-size:11.5px;
    font-weight:600;
    text-transform:uppercase;
    letter-spacing:1px;
    margin-bottom:14px;
    backdrop-filter:blur(6px);
}

/* ---------- Glass panel ---------- */
.login-panel{
    padding:34px 32px;
    background:rgba(255,255,255,.72);
    backdrop-filter:blur(18px) saturate(160%);
    -webkit-backdrop-filter:blur(18px) saturate(160%);
    border-radius:18px;
    box-shadow:0 25px 60px rgba(10,18,28,.35);
    border:1px solid rgba(255,255,255,.55);
}
.login-title{
    text-align:center;
    margin:0 0 6px;
    color:var(--ink);
    font-family:"Outfit","Inter",Arial,sans-serif;
    font-size:25px;
    font-weight:700;
    letter-spacing:-.3px;
}
.text-center{
    text-align:center;
    color:var(--ink-soft);
    font-weight:600;
    font-size:13.5px;
    margin-bottom:16px;
}
hr{
    border:0;
    border-top:1px solid rgba(28,37,48,.12);
    margin:16px 0 24px;
}
.form-group{
    margin-bottom:16px;
}
.form-group label{
    display:block;
    color:var(--ink-soft);
    font-size:11.5px;
    font-weight:650;
    text-transform:uppercase;
    letter-spacing:.5px;
    margin-bottom:7px;
}
.form-control{
    height:44px;
    border:1px solid rgba(28,37,48,.16);
    border-radius:8px;
    box-shadow:none;
    color:var(--ink);
    background:rgba(255,255,255,.75);
    transition:all .2s ease;
}
.form-control:focus{
    border-color:var(--accent);
    box-shadow:0 0 0 3px rgba(193,101,47,.14);
    background:rgba(255,255,255,.95);
}
.btn-login{
    width:100%;
    height:46px;
    border:0;
    border-radius:8px;
    background:var(--accent);
    color:#ffffff;
    font-weight:650;
    font-size:14px;
    letter-spacing:.2px;
    box-shadow:0 10px 24px rgba(193,101,47,.3);
    transition:all .2s ease;
}
.btn-login:hover{
    background:var(--accent-dark);
    transform:translateY(-1px);
}
.message{
    display:block;
    margin-top:15px;
    text-align:center;
    font-size:13px;
}
.signup-link{
    text-align:center;
    margin-top:22px;
    color:var(--ink-soft);
    font-size:13px;
}
.signup-link a{
    color:var(--accent-dark);
    font-weight:650;
    text-decoration:none;
}
.signup-link a:hover{
    color:var(--accent);
    text-decoration:underline;
}
.signup-panel{
    display:none;
}
@media(max-width:767px){
    body{
        background-attachment:scroll;
        padding:30px 16px;
    }
    .login-container{
        width:100%;
        max-width:400px;
    }
    .login-panel{
        padding:26px 22px;
    }
    .login-title{
        font-size:22px;
    }
}
</style>
</head>
<body>
<form id="form1" runat="server">
<div class="login-container">

<div class="brand-header">
<div class="brand-badge">
<span class="glyphicon glyphicon-heart"></span>
Mahila Bachat Gat
</div>
</div>

<div class="login-panel">

<div id="loginPanel">

<h2 class="login-title">
Welcome Back
</h2>

<h4 class="text-center">
Sign in to continue shopping
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
Join the Mahila Marketplace
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