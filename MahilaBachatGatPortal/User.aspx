<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeFile="User.aspx.cs"
    Inherits="User" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <meta charset="utf-8" />

    <meta name="viewport"
          content="width=device-width, initial-scale=1" />

    <title>Login | Mahila Bachat Gat</title>

    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css"
          rel="stylesheet" />

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
    </style>

</head>

<body>

<form id="form1" runat="server">

    <div class="login-container">

        <div class="login-panel">

            <h2 class="login-title">
                Mahila Bachat Gat
            </h2>

            <h4 class="text-center">
                User Login
            </h4>

            <hr />

            <div class="form-group">

                <label>Username</label>

                <asp:TextBox ID="txtUsername"
                    runat="server"
                    CssClass="form-control"
                    placeholder="Enter username">
                </asp:TextBox>

            </div>

            <div class="form-group">

                <label>Password</label>

                <asp:TextBox ID="txtPassword"
                    runat="server"
                    CssClass="form-control"
                    TextMode="Password"
                    placeholder="Enter password">
                </asp:TextBox>

            </div>

            <asp:Button ID="btnLogin"
                runat="server"
                Text="Login"
                CssClass="btn btn-primary btn-login"
                OnClick="btnLogin_Click" />

            <asp:Label ID="lblMessage"
                runat="server"
                CssClass="message">
            </asp:Label>

        </div>

    </div>

</form>

</body>
</html>