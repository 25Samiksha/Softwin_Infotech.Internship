<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="LoginDashboard.aspx.cs"
    Inherits="LoginDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">

    <title>Security Portal - Dashboard</title>

    <meta name="viewport"
        content="width=device-width, initial-scale=1.0" />

    <link href="CSS/Style.css"
        rel="stylesheet"
        type="text/css" />

</head>

<body>

<form id="form1" runat="server">

    <header class="top-navbar">

        <div class="brand">

            <div class="brand-icon">
                🔐
            </div>

            <span>Security Portal</span>

        </div>


        <div class="nav-right">

            <span class="welcome-text">

                Welcome,
                <strong>
                    <asp:Label ID="lblFullName"
                        runat="server">
                    </asp:Label>
                </strong>

                <span class="separator">|</span>

                <asp:Label ID="lblRole"
                    runat="server">
                </asp:Label>

            </span>


            <a href="Logout.aspx"
                class="logout-button">

                Logout

            </a>

        </div>

    </header>


    <main class="dashboard-container">


        <section class="welcome-card">

            <div class="welcome-content">

                <div class="welcome-icon">
                    👋
                </div>

                <div>

                    <h1>
                        Welcome to Security Portal
                    </h1>

                    <p>
                        You are successfully logged in.
                        Choose your available dashboard below.
                    </p>

                </div>

            </div>


            <div class="role-badge">

                Role:

                <asp:Label ID="lblRoleBadge"
                    runat="server">
                </asp:Label>

            </div>

        </section>


        <section class="dashboard-section">

            <div class="section-heading">

                <h2>
                    Your Available Dashboard
                </h2>

                <p>
                    Select a dashboard to continue.
                </p>

            </div>



            <div class="dashboard-grid">


                <asp:Panel ID="pnlAdmin"
                    runat="server"
                    CssClass="dashboard-card admin-card">

                    <div class="card-icon admin-icon">
                        👨‍💼
                    </div>

                    <div class="card-content">

                        <span class="card-label">
                            ADMIN
                        </span>

                        <h3>
                            Admin Dashboard
                        </h3>

                        <p>
                            Manage administrators,
                            employees, users and
                            confidential system information.
                        </p>

                        <a href="Admin/AdminDashboard.aspx"
                            class="card-button admin-button">

                            Open Dashboard
                            <span>→</span>

                        </a>

                    </div>

                </asp:Panel>

                <asp:Panel ID="pnlEmployee"
                    runat="server"
                    CssClass="dashboard-card employee-card">

                    <div class="card-icon employee-icon">
                        👨‍💻
                    </div>

                    <div class="card-content">

                        <span class="card-label">
                            EMPLOYEE
                        </span>

                        <h3>
                            Employee Dashboard
                        </h3>

                        <p>
                            View your employee information,
                            department, designation and
                            secure work details.
                        </p>

                        <a href="Employee/EmployeeDashboard.aspx"
                            class="card-button employee-button">

                            Open Dashboard
                            <span>→</span>

                        </a>

                    </div>

                </asp:Panel>


                <asp:Panel ID="pnlUser"
                    runat="server"
                    CssClass="dashboard-card user-card">

                    <div class="card-icon user-icon">
                        👤
                    </div>

                    <div class="card-content">

                        <span class="card-label">
                            USER
                        </span>

                        <h3>
                            User Dashboard
                        </h3>

                        <p>
                            View your profile,
                            registered information
                            and account details.
                        </p>

                        <a href="User/UserDashboard.aspx"
                            class="card-button user-button">

                            Open Dashboard
                            <span>→</span>

                        </a>

                    </div>

                </asp:Panel>


            </div>

        </section>


        <section class="security-info">

            <div class="security-icon">
                🛡️
            </div>

            <div class="security-content">

                <h3>
                    Security Protected
                </h3>

                <p>
                    Your access is controlled using
                    authentication and authorization.
                    Only dashboards permitted for your
                    role are displayed.
                </p>

            </div>

            <div class="security-status">

                <span class="status-dot">
                </span>

                Secure Session

            </div>

        </section>


    </main>

    <footer class="footer">

        <p>
            © 2026 Security Portal
            <span>|</span>
            Secure Access System
        </p>

    </footer>


</form>

</body>

</html>