<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="ManageUsers.aspx.cs"
    Inherits="Admin_ManageUsers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">

    <title>Manage Users - Security Portal</title>

    <link href="../CSS/Style.css"
          rel="stylesheet"
          type="text/css" />

    <style type="text/css">
        .manage-input
        {
            margin-left: 22px;
        }
        .form-control
        {}
    </style>

</head>

<body>

<form id="form1" runat="server">

<div class="manage-page">

    <div class="manage-container">

        <!-- HEADER -->

        <div class="manage-header">

            <div>

                <h1>Manage Users</h1>

                <p>
                    Add and manage registered users
                </p>

            </div>

            <div>

                <a href="AdminDashboard.aspx"
                   class="back-button">
                    ← Dashboard
                </a>

            </div>

        </div>

        <div class="manage-card">

            <h2>Add New User</h2>

            <p class="form-description">
                Enter the user's information below.
            </p>

            <div class="input-group">

                <asp:Label
                    ID="lblFullName"
                    runat="server"
                    Text="Full Name">
                </asp:Label>

                <asp:TextBox
                    ID="txtFullName"
                    runat="server"
                    CssClass="manage-input">
                </asp:TextBox>

            </div>

            <div class="input-group">

                <asp:Label
                    ID="lblUsername"
                    runat="server"
                    Text="Username">
                </asp:Label>

                <asp:TextBox
                    ID="txtUsername"
                    runat="server"
                    CssClass="manage-input">
                </asp:TextBox>

            </div>

            <div class="input-group">

                <asp:Label
                    ID="lblPassword"
                    runat="server"
                    Text="Password">
                </asp:Label>

                <asp:TextBox
                    ID="txtPassword"
                    runat="server"
                    TextMode="Password"
                    CssClass="manage-input">
                </asp:TextBox>

            </div>


            <div class="input-group">

                <asp:Label
                    ID="lblEmail"
                    runat="server"
                    Text="Email">
                </asp:Label>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;

                <asp:TextBox
                    ID="txtEmail"
                    runat="server"
                    CssClass="form-control"  Width="128px"></asp:TextBox>

            </div>

            <div class="input-group">

                <asp:Label
                    ID="lblPhone"
                    runat="server"
                    Text="Phone">
                </asp:Label>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                <asp:TextBox
                    ID="txtPhone"
                    runat="server"
                    CssClass="manage-input">
                </asp:TextBox>

            </div>

            <asp:Button
                ID="btnAddUser"
                runat="server"
                Text="Add User"
                CssClass="add-user-button"
                OnClick="btnAddUser_Click" />


            <asp:Label
                ID="lblMessage"
                runat="server"
                CssClass="manage-message">
            </asp:Label>

        </div>

        <div class="manage-card">

            <h2>Registered Users</h2>

            <asp:GridView
                ID="gvUsers"
                runat="server"
                AutoGenerateColumns="true"
                CssClass="user-grid">

            </asp:GridView>

        </div>

    </div>

</div>

</form>

</body>

</html>