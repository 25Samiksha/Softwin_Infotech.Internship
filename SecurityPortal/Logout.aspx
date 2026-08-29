<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="Logout.aspx.cs"
    Inherits="Logout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>Logout</title>

    <style>
        body {
            font-family: Arial, Helvetica, sans-serif;
            background: #f3f4f6;
            text-align: center;
            margin-top: 150px;
        }

        .logout-box {
            width: 400px;
            margin: auto;
            background: white;
            padding: 40px;
            border-radius: 10px;
            box-shadow: 0 5px 20px rgba(0,0,0,0.15);
        }

        .logout-box h2 {
            color: #1f2937;
        }

        .logout-box p {
            color: #6b7280;
        }
    </style>

</head>

<body>

<form id="form1" runat="server">

    <div class="logout-box">

        <h2>🔒 Logging Out...</h2>

        <p>
            Please wait while we securely log you out.
        </p>

    </div>

</form>

</body>

</html>