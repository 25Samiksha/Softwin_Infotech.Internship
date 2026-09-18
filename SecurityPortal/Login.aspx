<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="Login.aspx.cs"
    Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">

    <title>Security Portal - Login</title>

    <link href="CSS/Style.css"
          rel="stylesheet"
          type="text/css" />

</head>

<body>

<form id="form1" runat="server">

    <div class="login-page">

        <div class="login-card">

            <div class="login-icon">
                🔐
            </div>
            <h1 class="login-title">
                Security Portal
            </h1>

            <p class="login-subtitle">
                Login to access your dashboard
            </p>

            <div class="login-form-group">

                <asp:Label
                    ID="lblUsername"
                    runat="server"
                    Text="Username"
                    CssClass="login-label">
                </asp:Label>

                <asp:TextBox
                    ID="txtUsername"
                    runat="server"
                    CssClass="login-input">
                </asp:TextBox>

            </div>

            <div class="login-form-group">

                <asp:Label
                    ID="lblPassword"
                    runat="server"
                    Text="Password"
                    CssClass="login-label">
                </asp:Label>

                <asp:TextBox
                    ID="txtPassword"
                    runat="server"
                    TextMode="Password"
                    CssClass="login-input">
                </asp:TextBox>

            </div>

            <asp:Button
                ID="btnLogin"
                runat="server"
                Text="LOGIN"
                CssClass="login-button"
                OnClick="btnLogin_Click" />

            <asp:Label
                ID="lblMessage"
                runat="server"
                CssClass="login-message">
            </asp:Label>

        </div>

    </div>

</form>

</body>

</html>