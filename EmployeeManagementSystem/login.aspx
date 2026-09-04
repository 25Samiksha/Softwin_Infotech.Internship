<%@ Page Title="Login"
    Language="C#"
    AutoEventWireup="true"
    CodeFile="Login.aspx.cs"
    Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">

    <title>Employee Management System - Login</title>

    <style type="text/css">

        * {
            box-sizing: border-box;
        }

        body {
            margin: 0;
            padding: 0;
            font-family: Arial, Helvetica, sans-serif;
            background-color: #f2f4f7;
        }

        .login-container {
            width: 100%;
            min-height: 100vh;
            display: table;
        }

        .login-wrapper {
            display: table-cell;
            vertical-align: middle;
            text-align: center;
        }

        .login-box {
            width: 400px;
            margin: 0 auto;
            background-color: white;
            padding: 35px;
            border: 1px solid #ddd;
            box-shadow: 0 3px 12px rgba(0,0,0,0.10);
            text-align: left;
        }

        .login-title {
            text-align: center;
            margin-bottom: 10px;
            color: #1f4e79;
            font-size: 28px;
        }

        .login-subtitle {
            text-align: center;
            color: #777;
            margin-bottom: 30px;
            font-size: 14px;
        }

        .form-row {
            margin-bottom: 20px;
        }

        .form-row label {
            display: block;
            margin-bottom: 7px;
            font-weight: bold;
            color: #333;
        }

        .form-control {
            width: 100%;
            padding: 11px;
            border: 1px solid #ccc;
            font-size: 14px;
        }

        .form-control:focus {
            border-color: #1f4e79;
            outline: none;
        }

        .remember-row {
            margin-bottom: 20px;
            font-size: 14px;
        }

        .remember-row input {
            margin-right: 6px;
        }

        .login-button {
            width: 100%;
            padding: 12px;
            background-color: #1f4e79;
            color: white;
            border: none;
            cursor: pointer;
            font-size: 16px;
            font-weight: bold;
        }

        .login-button:hover {
            background-color: #163a5c;
        }

        .forgot-password {
            text-align: center;
            margin-top: 18px;
        }

        .forgot-password a {
            color: #1f4e79;
            text-decoration: none;
            font-size: 14px;
        }

        .forgot-password a:hover {
            text-decoration: underline;
        }

        .error-message {
            display: block;
            color: #c0392b;
            text-align: center;
            margin-bottom: 15px;
        }

        .login-footer {
            text-align: center;
            color: #888;
            font-size: 12px;
            margin-top: 25px;
        }

    </style>

</head>


<body>

    <form id="form1" runat="server">

        <div class="login-container">

            <div class="login-wrapper">

                <div class="login-box">

                    <div class="login-title">
                        Employee Management System
                    </div>

                    <div class="login-subtitle">
                        Sign in to your account
                    </div>


                    <asp:Label ID="lblMessage"
                        runat="server"
                        CssClass="error-message">
                    </asp:Label>

                    <div class="form-row">

                        <asp:Label ID="lblUsername"
                            runat="server"
                            Text="Username">
                        </asp:Label>

                        <asp:TextBox ID="txtUsername"
                            runat="server"
                            CssClass="form-control">
                        </asp:TextBox>

                    </div>

                    <div class="form-row">

                        <asp:Label ID="lblPassword"
                            runat="server"
                            Text="Password">
                        </asp:Label>

                        <asp:TextBox ID="txtPassword"
                            runat="server"
                            CssClass="form-control"
                            TextMode="Password">
                        </asp:TextBox>

                    </div>

                    <div class="remember-row">

                        <asp:CheckBox ID="chkRemember"
                            runat="server"
                            Text="Remember me" />

                    </div>

                    <asp:Button ID="btnLogin"
                        runat="server"
                        Text="Login"
                        CssClass="login-button"
                        OnClick="btnLogin_Click" />

                    <div class="forgot-password">

                        <a href="#">
                            Forgot Password?
                        </a>

                    </div>


                    <div class="login-footer">

                        Employee Management System

                    </div>

                </div>

            </div>

        </div>

    </form>

</body>

</html>