<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="Library.aspx.cs"
    Inherits="Library" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">

    <title>Library Management System</title>

    <style type="text/css">

        body
        {
            margin: 0;
            padding: 0;
            font-family: Arial, Helvetica, sans-serif;
            background-color: #f2f2f2;
        }

        .container
        {
            width: 90%;
            margin: 30px auto;
            background-color: white;
            padding: 25px;
            border-radius: 8px;
            box-shadow: 0px 0px 10px #cccccc;
        }

        .heading
        {
            text-align: center;
            color: #333333;
            margin-bottom: 25px;
        }

        .form-table
        {
            width: 600px;
            margin: auto;
        }

        .form-table td
        {
            padding: 8px;
        }

        .label
        {
            font-weight: bold;
            color: #333333;
        }

        .textbox,
        .dropdown
        {
            width: 250px;
            height: 30px;
            padding: 4px;
            border: 1px solid #cccccc;
            border-radius: 4px;
        }

        /* Button styling */

        .button
        {
            padding: 9px 18px;
            margin: 5px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            background-color: #337ab7;
            color: white;
            font-weight: bold;
            height: 38px;
        }

        .button:hover
        {
            background-color: #286090;
        }

        /* Keep all buttons on one line */

        .button-area
        {
            text-align: center;
            white-space: nowrap;
        }

        .message
        {
            display: block;
            text-align: center;
            margin: 15px;
            font-weight: bold;
        }

        .grid
        {
            width: 100%;
            margin-top: 25px;
            border-collapse: collapse;
        }

        .grid th
        {
            background-color: #337ab7;
            color: white;
            padding: 10px;
        }

        .grid td
        {
            padding: 8px;
            text-align: center;
            border: 1px solid #dddddd;
        }

        .grid tr:nth-child(even)
        {
            background-color: #f9f9f9;
        }

    </style>

</head>

<body>

<form id="form1" runat="server">

<div class="container">

    <h1 class="heading">
        Library Management System
    </h1>


    <table class="form-table">

        <!-- Book Name -->

        <tr>

            <td class="label">
                Book Name
            </td>

            <td>

                <asp:TextBox
                    ID="txtbname"
                    runat="server"
                    CssClass="textbox">
                </asp:TextBox>

            </td>

        </tr>


        <!-- Author Name -->

        <tr>

            <td class="label">
                Author Name
            </td>

            <td>

                <asp:TextBox
                    ID="txtaname"
                    runat="server"
                    CssClass="textbox">
                </asp:TextBox>

            </td>

        </tr>


        <!-- Category -->

        <tr>

            <td class="label">
                Category
            </td>

            <td>

                <asp:DropDownList
                    ID="ddlcategory"
                    runat="server"
                    CssClass="dropdown">

                    <asp:ListItem Value="">
                        Select Category
                    </asp:ListItem>

                    <asp:ListItem Value="Technology">
                        Technology
                    </asp:ListItem>

                    <asp:ListItem Value="Programing">
                        Programing
                    </asp:ListItem>

                    <asp:ListItem Value="Bussiness">
                        Bussiness
                    </asp:ListItem>

                    <asp:ListItem Value="Science">
                        Science
                    </asp:ListItem>

                    <asp:ListItem Value="Biography">
                        Biography
                    </asp:ListItem>

                    <asp:ListItem Value="Novel">
                        Novel
                    </asp:ListItem>

                    <asp:ListItem Value="Other">
                        Other
                    </asp:ListItem>

                </asp:DropDownList>

            </td>

        </tr>


        <!-- Price -->

        <tr>

            <td class="label">
                Price
            </td>

            <td>

                <asp:TextBox
                    ID="txtprice"
                    runat="server"
                    CssClass="textbox">
                </asp:TextBox>

            </td>

        </tr>


        <!-- Quantity -->

        <tr>

            <td class="label">
                Quantity
            </td>

            <td>

                <asp:TextBox
                    ID="txtqty"
                    runat="server"
                    CssClass="textbox">
                </asp:TextBox>

            </td>

        </tr>


        <!-- Buttons -->

        <tr>

            <td colspan="2">

                <div class="button-area">

                    <asp:Button
                        ID="btnsave"
                        runat="server"
                        Text="Save Book"
                        CssClass="button"
                        OnClick="btnsave_Click" />

                    <asp:Button
                        ID="btnupdate"
                        runat="server"
                        Text="Update Book"
                        CssClass="button"
                        OnClick="btnupdate_Click" />

                    <asp:Button
                        ID="btnview"
                        runat="server"
                        Text="View Books"
                        CssClass="button"
                        OnClick="btnview_Click" />

                    <asp:Button
                        ID="btnclear"
                        runat="server"
                        Text="Clear"
                        CssClass="button"
                        OnClick="btnclear_Click" />

                    <asp:Button
                        ID="btnback"
                        runat="server"
                        Text="Back"
                        CssClass="button"
                        OnClick="btnback_Click" />

                </div>

            </td>

        </tr>

    </table>


    <!-- Message -->

    <asp:Label
        ID="lblmsg"
        runat="server"
        CssClass="message">
    </asp:Label>


    <!-- Books Grid -->

    <asp:GridView
        ID="gvBooks"
        runat="server"
        CssClass="grid"
        AutoGenerateColumns="False"
        DataKeyNames="BookID"
        OnRowDeleting="gvBooks_RowDeleting"
        OnSelectedIndexChanged="gvBooks_SelectedIndexChanged">

        <Columns>

            <asp:BoundField
                DataField="BookID"
                HeaderText="Book ID" />

            <asp:BoundField
                DataField="BookName"
                HeaderText="Book Name" />

            <asp:BoundField
                DataField="AuthorName"
                HeaderText="Author Name" />

            <asp:BoundField
                DataField="Category"
                HeaderText="Category" />

            <asp:BoundField
                DataField="Price"
                HeaderText="Price" />

            <asp:BoundField
                DataField="Quantity"
                HeaderText="Quantity" />

            <asp:CommandField
                ShowSelectButton="True"
                ShowDeleteButton="True"
                HeaderText="Action" />

        </Columns>

    </asp:GridView>

</div>

</form>

</body>

</html>