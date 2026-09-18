<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="UserDashboard.aspx.cs"
    Inherits="UserDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">

    <title>User Dashboard</title>

    <link href="../CSS/Style.css"
          rel="stylesheet"
          type="text/css" />

</head>

<body>

<form id="form1" runat="server">

<div class="user-page">

    <div class="user-container">


        <!-- WELCOME -->

        <div class="user-header">

            <a href="../Logout.aspx"
               class="user-logout">
                Logout
            </a>

            <h1>
                Welcome 👋
            </h1>

            <p>

                <strong>

                    <asp:Label ID="lblName"
                        runat="server">
                    </asp:Label>

                </strong>

            </p>

            <p>
                Your personal security dashboard
            </p>

        </div>


        <!-- PROFILE -->

        <div class="user-profile">

            <h2 class="user-profile-title">
                👤 My Account
            </h2>


            <div class="user-data">


                <div class="user-data-row">

                    <span class="user-data-label">
                        User ID
                    </span>

                    <span class="user-data-value">

                        <asp:Label ID="lblUserID"
                            runat="server">
                        </asp:Label>

                    </span>

                </div>


                <div class="user-data-row">

                    <span class="user-data-label">
                        Username
                    </span>

                    <span class="user-data-value">

                        <asp:Label ID="lblUsername"
                            runat="server">
                        </asp:Label>

                    </span>

                </div>


                <div class="user-data-row">

                    <span class="user-data-label">
                        Full Name
                    </span>

                    <span class="user-data-value">

                        <asp:Label ID="lblFullName"
                            runat="server">
                        </asp:Label>

                    </span>

                </div>


                <div class="user-data-row">

                    <span class="user-data-label">
                        Email
                    </span>

                    <span class="user-data-value">

                        <asp:Label ID="lblEmail"
                            runat="server">
                        </asp:Label>

                    </span>

                </div>


                <div class="user-data-row">

                    <span class="user-data-label">
                        Phone
                    </span>

                    <span class="user-data-value">

                        <asp:Label ID="lblPhone"
                            runat="server">
                        </asp:Label>

                    </span>

                </div>


            </div>

        </div>

        </div>

</div>

</form>

</body>

</html>