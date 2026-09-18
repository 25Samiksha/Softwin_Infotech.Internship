<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="IssueBook.aspx.cs"
    Inherits="IssueBook" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">

    <title>Issue Book</title>

    <style type="text/css">

        body
        {
            font-family: Arial, sans-serif;
            background-color: #f2f2f2;
            margin: 0;
            padding: 0;
        }

        .container
        {
            width: 850px;
            margin: 40px auto;
            background-color: white;
            padding: 30px;
            border-radius: 10px;
        }

        h2
        {
            text-align: center;
            margin-bottom: 30px;
        }

        .form-table
        {
            width: 500px;
            margin: auto;
        }

        .label-cell
        {
            width: 150px;
            padding: 10px;
            font-weight: bold;
        }

        .input-cell
        {
            padding: 10px;
        }

        .textbox
        {
            width: 250px;
            height: 25px;
        }

        .dropdown
        {
            width: 260px;
            height: 32px;
        }

        /* Button container */

        .button-area
        {
            text-align: center;
            padding: 20px;
            white-space: nowrap;
        }

        /* All buttons including Back button */

        .button
        {
            padding: 10px 20px;
            margin: 5px;
            cursor: pointer;
            font-size: 15px;
            background-color: #337ab7;
            color: white;
            border: none;
            border-radius: 4px;
            height: 43px;
        }

        .button:hover
        {
            background-color: #286090;
        }

        .message
        {
            display: block;
            text-align: center;
            font-weight: bold;
            margin: 15px;
        }

        .grid
        {
            width: 100%;
            margin-top: 25px;
        }

    </style>

</head>

<body>

<form id="form1" runat="server">

<div class="container">

    <h2>Issue Book</h2>

    <table class="form-table">

        <!-- Student -->

        <tr>

            <td class="label-cell">

                <asp:Label
                    ID="lblStudent"
                    runat="server"
                    Text="Student Name :">
                </asp:Label>

            </td>

            <td class="input-cell">

                <asp:DropDownList
                    ID="ddlStudent"
                    runat="server"
                    CssClass="dropdown">
                </asp:DropDownList>

            </td>

        </tr>


        <!-- Book -->

        <tr>

            <td class="label-cell">

                <asp:Label
                    ID="lblBook"
                    runat="server"
                    Text="Book Name :">
                </asp:Label>

            </td>

            <td class="input-cell">

                <asp:DropDownList
                    ID="ddlBook"
                    runat="server"
                    CssClass="dropdown">
                </asp:DropDownList>

            </td>

        </tr>


        <!-- Issue Date -->

        <tr>

            <td class="label-cell">

                <asp:Label
                    ID="lblIssueDate"
                    runat="server"
                    Text="Issue Date :">
                </asp:Label>

            </td>

            <td class="input-cell">

                <asp:TextBox
                    ID="txtIssueDate"
                    runat="server"
                    CssClass="textbox">
                </asp:TextBox>

            </td>

        </tr>

    </table>


    <!-- Buttons -->

    <div class="button-area">

        <asp:Button
            ID="btnIssue"
            runat="server"
            Text="Issue Book"
            CssClass="button"
            OnClick="btnIssue_Click" />

        <asp:Button
            ID="btnView"
            runat="server"
            Text="View Issued Books"
            CssClass="button"
            OnClick="btnView_Click" />

        <asp:Button
            ID="btnback"
            runat="server"
            Text="Back"
            CssClass="button"
            OnClick="btnback_Click" />

    </div>


    <!-- Message -->

    <asp:Label
        ID="lblmsg"
        runat="server"
        CssClass="message">
    </asp:Label>


    <!-- Issued Books Grid -->

    <asp:GridView
        ID="gvIssuedBooks"
        runat="server"
        AutoGenerateColumns="False"
        CssClass="grid"
        BorderWidth="1px"
        CellPadding="8"
        GridLines="Both">

        <Columns>

            <asp:BoundField
                DataField="StudentName"
                HeaderText="Student Name" />

            <asp:BoundField
                DataField="Email"
                HeaderText="Email" />

            <asp:BoundField
                DataField="Course"
                HeaderText="Course" />

            <asp:BoundField
                DataField="Branch"
                HeaderText="Branch" />

            <asp:BoundField
                DataField="Phone"
                HeaderText="Phone" />

            <asp:BoundField
                DataField="BookName"
                HeaderText="Book Name" />

            <asp:BoundField
                DataField="AuthorName"
                HeaderText="Author" />

            <asp:BoundField
                DataField="Category"
                HeaderText="Category" />

            <asp:BoundField
                DataField="IssueDate"
                HeaderText="Issue Date"
                DataFormatString="{0:dd-MM-yyyy}" />

        </Columns>

    </asp:GridView>

</div>

</form>

</body>

</html>