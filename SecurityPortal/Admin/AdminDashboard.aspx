<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="AdminDashboard.aspx.cs"
    Inherits="Admin_AdminDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">

    <title>Admin Dashboard - Security Portal</title>

    <link href="../CSS/Style.css"
        rel="stylesheet"
        type="text/css" />

</head>

<body>

<form id="form1" runat="server">

<div class="admin-page">
 

    <div class="admin-sidebar">

        <div class="admin-logo">
            Security Portal
        </div>

        <div class="admin-line"></div>

        <div class="admin-menu-title">
            ADMINISTRATION
        </div>

        <div class="admin-menu">

            <a href="AdminDashboard.aspx">
                Dashboard
            </a>

            <a href="Admin.aspx">
                Admin
            </a>

            <a href="../Employee/Employee.aspx">
                Employees
            </a>

            <a href="../User/User.aspx">
                Users
            </a>

        </div>

        <div class="admin-menu-title">
            SECURITY
        </div>

        <div class="admin-menu">

            <a href="#">
                Confidentiality
            </a>

            <a href="#">
                Integrity
            </a>

        </div>


        <div class="admin-logout">

            <a href="../Logout.aspx">
                Logout
            </a>

        </div>

    </div>

    <div class="admin-main">

        <div class="admin-top">

            <h1>
                Admin Dashboard
            </h1>

            <p>
                Welcome,
                <strong>
                    <asp:Label
                        ID="lblAdminName"
                        runat="server">
                    </asp:Label>
                </strong>
            </p>

        </div>

        <h2 class="admin-section-title">
            System Management
        </h2>

        <div class="admin-cards">

            <div class="admin-card">

                <div class="admin-card-icon">
                    ADMIN
                </div>

                <h3>
                    Admin
                </h3>

                <p>
                    Manage administrator information
                    and system settings.
                </p>

                <a href="Admin.aspx">
                    Open Admin
                </a>

            </div>
            <div class="admin-card">

                <div class="admin-card-icon">
                    EMPLOYEE
                </div>

                <h3>
                    Employees
                </h3>

                <p>
                    View and manage employee
                    information.
                </p>

                <a href="../Employee/Employee.aspx">
                    Open Employees
                </a>

            </div>

            <div class="admin-card">

                <div class="admin-card-icon">
                    USER
                </div>

                <h3>
                    Users
                </h3>

                <p>
                    View registered user
                    information.
                </p>

                <a href="ManageUsers.aspx" class="admin-card-button">
    Manage Users
</a>

            </div>

        </div>
        <div class="admin-security">

            <h2>
                Security Status
            </h2>

            <p>
                Your administrator account has
                access to confidential system information.
            </p>

            <span class="security-good">
                Security Active
            </span>

        </div>

    </div>

</div>

</form>

</body>

</html>