<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="Home.aspx.cs"
    Inherits="Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">

    <title>Library Management System</title>

    <style type="text/css">

        body
        {
            margin: 0;
            padding: 0;
            font-family: Arial, sans-serif;
            background-color: #f2f2f2;
        }

        .header
        {
            background-color: #333333;
            color: white;
            text-align: center;
            padding: 25px;
        }

        .header h1
        {
            margin: 0;
            font-size: 32px;
        }

        .header p
        {
            margin-top: 8px;
            font-size: 16px;
        }

        .container
        {
            width: 900px;
            margin: 50px auto;
            text-align: center;
        }

        .welcome
        {
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 35px;
        }

        .cards
        {
            width: 100%;
            text-align: center;
        }

        .card
        {
            display: inline-block;
            width: 240px;
            min-height: 180px;
            margin: 15px;
            padding: 25px;
            background-color: white;
            border: 1px solid #dddddd;
            border-radius: 10px;
            vertical-align: top;
        }

        .card h2
        {
            font-size: 21px;
            margin-bottom: 15px;
        }

        .card p
        {
            font-size: 14px;
            color: #666666;
            min-height: 45px;
        }

        .button
        {
            width: 180px;
            padding: 10px;
            margin-top: 15px;
            cursor: pointer;
            font-size: 15px;
        }

        .footer
        {
            text-align: center;
            margin-top: 50px;
            color: #777777;
            font-size: 14px;
        }

    </style>

</head>

<body>

<form id="form1" runat="server">

    <div class="header">

        <h1>Library Management System</h1>

        <p>Manage Books, Students and Issued Books</p>

    </div>

    <div class="container">

        <div class="welcome">
            Welcome to Library Management System
        </div>


        <div class="cards">


            <div class="card">

                <h2>📚 Manage Books</h2>

                <p>
                    Add, view, update and delete books
                    from the library.
                </p>

                <asp:Button ID="btnBooks"
                    runat="server"
                    Text="Manage Books"
                    CssClass="button"
                    OnClick="btnBooks_Click" />

            </div>

            <div class="card">

                <h2>👨‍🎓 Students</h2>

                <p>
                    Register and manage student
                    information.
                </p>

                <asp:Button ID="btnStudents"
                    runat="server"
                    Text="Add Student"
                    CssClass="button"
                    OnClick="btnStudents_Click" />

            </div>


            <div class="card">

                <h2>📖 Issue Books</h2>

                <p>
                    Issue books to students and
                    view issued books.
                </p>

                <asp:Button ID="btnIssueBook"
                    runat="server"
                    Text="Issue Book"
                    CssClass="button"
                    OnClick="btnIssueBook_Click" />

            </div>


        </div>


        <div class="footer">

            Library Management System

        </div>

    </div>

</form>

</body>

</html>