<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="EmployeeDashboard.aspx.cs"
    Inherits="EmployeeDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">

    <title>Employee Dashboard</title>

    <link href="../CSS/Style.css"
          rel="stylesheet"
          type="text/css" />

</head>

<body>

<form id="form1" runat="server">

<div class="employee-page">

    <div class="employee-container">

        <div class="employee-header">

            <a href="../Logout.aspx"
               class="employee-logout">
                Logout
            </a>

            <h1>
                👨‍💻 Employee Dashboard
            </h1>

            <p>
                Welcome,
                <strong>
                    <asp:Label ID="lblName"
                        runat="server">
                    </asp:Label>
                </strong>
            </p>

        </div>


        <div class="employee-profile">

            <div>

                <div class="employee-avatar">
                    👨‍💻
                </div>

                <div class="employee-name">

                    <h2>
                        <asp:Label ID="lblFullName"
                            runat="server">
                        </asp:Label>
                    </h2>

                    <p>
                        Employee Profile
                    </p>

                </div>

            </div>


            <div class="employee-info">

                <div class="employee-info-box">

                    <span class="employee-label">
                        Employee ID
                    </span>

                    <span class="employee-value">

                        <asp:Label ID="lblEmployeeID"
                            runat="server">
                        </asp:Label>

                    </span>

                </div>


                <div class="employee-info-box">

                    <span class="employee-label">
                        Username
                    </span>

                    <span class="employee-value">

                        <asp:Label ID="lblUsername"
                            runat="server">
                        </asp:Label>

                    </span>

                </div>


                <div class="employee-info-box">

                    <span class="employee-label">
                        Email
                    </span>

                    <span class="employee-value">

                        <asp:Label ID="lblEmail"
                            runat="server">
                        </asp:Label>

                    </span>

                </div>


                <div class="employee-info-box">

                    <span class="employee-label">
                        Phone
                    </span>

                    <span class="employee-value">

                        <asp:Label ID="lblPhone"
                            runat="server">
                        </asp:Label>

                    </span>

                </div>


                <div class="employee-info-box">

                    <span class="employee-label">
                        Department
                    </span>

                    <span class="employee-value">

                        <asp:Label ID="lblDepartment"
                            runat="server">
                        </asp:Label>

                    </span>

                </div>


                <div class="employee-info-box">

                    <span class="employee-label">
                        Designation
                    </span>

                    <span class="employee-value">

                        <asp:Label ID="lblDesignation"
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