<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="Employee.aspx.cs"
    Inherits="EmployeePage" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">

    <title>Employee Portal</title>

    <link href="../CSS/style.css"
          rel="stylesheet"
          type="text/css" />

</head>

<body>

<form id="form1" runat="server">

    <div style="text-align:center;
                margin-top:100px;">

        <h1>👨‍💻 Employee Portal</h1>

        <p>
            Welcome,
            <asp:Label ID="lblName"
                runat="server">
            </asp:Label>
        </p>

        <p>
            This area is restricted to employees.
        </p>

        <br />

        <asp:Button ID="btnDashboard"
            runat="server"
            Text="Open Employee Dashboard"
            CssClass="login-button"
            Width="300px"
            OnClick="btnDashboard_Click" />

        <br /><br />

        <asp:Button ID="btnLogout"
            runat="server"
            Text="Logout"
            CssClass="login-button"
            Width="300px"
            OnClick="btnLogout_Click" />

    </div>

</form>

</body>
</html>