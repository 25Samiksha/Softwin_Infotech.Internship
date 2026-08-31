<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Library.aspx.cs" Inherits="Library" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">

    <title>Library Management System</title>

    <style type="text/css">

        * {
            box-sizing: border-box;
        }

        body {
            margin: 0;
            padding: 0;
            font-family: Arial, Helvetica, sans-serif;
            background-color: #eef2f7;
            color: #333333;
        }
        .container {
            width: 1000px;
            margin: 40px auto;
            background-color: #ffffff;
            padding: 35px 45px;
            border-radius: 12px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.12);
        }
        .title {
            text-align: center;
            font-size: 30px;
            font-weight: bold;
            color: #1f2937;
            padding-bottom: 20px;
            margin-bottom: 30px;
            border-bottom: 2px solid #e5e7eb;
        }
        .section-title {
            font-size: 23px;
            font-weight: bold;
            color: #1f2937;
            margin-top: 10px;
            margin-bottom: 22px;
        }
        .form-row {
            display: inline-block;
            width: 48%;
            margin-bottom: 20px;
            vertical-align: top;
        }
        .form-row label {
            display: block;
            width: 100%;
            font-weight: bold;
            font-size: 15px;
            margin-bottom: 8px;
            color: #374151;
        }
        .textbox {
            width: 100%;
            height: 40px;
            padding: 8px 12px;
            border: 1px solid #cbd5e1;
            border-radius: 6px;
            font-size: 15px;
            outline: none;
        }

        .textbox:focus {
            border-color: #2563eb;
            box-shadow: 0 0 0 2px rgba(37, 99, 235, 0.15);
        }
        .dropdown {
            width: 100%;
            height: 40px;
            padding: 5px 10px;
            border: 1px solid #cbd5e1;
            border-radius: 6px;
            font-size: 15px;
            background-color: #ffffff;
            outline: none;
        }

        .dropdown:focus {
            border-color: #2563eb;
            box-shadow: 0 0 0 2px rgba(37, 99, 235, 0.15);
        }
        .button {
            padding: 11px 24px;
            margin-right: 10px;
            border: none;
            border-radius: 6px;
            background-color: #2563eb;
            color: white;
            font-size: 15px;
            font-weight: bold;
            cursor: pointer;
        }

        .button:hover {
            background-color: #1d4ed8;
        }
        .message {
            display: block;
            margin-top: 18px;
            padding: 10px 14px;
            font-weight: bold;
            font-size: 15px;
        }
        .grid {
            margin-top: 15px;
            width: 100%;
            border-collapse: collapse;
            border: 1px solid #d1d5db;
            font-size: 14px;
        }
        .grid th {
            background-color: #1f2937;
            color: white;
            padding: 13px 10px;
            text-align: center;
            font-size: 15px;
            border: 1px solid #374151;
        }
        .grid td {
            padding: 12px 10px;
            border: 1px solid #d1d5db;
            text-align: center;
        }
        .grid tr:nth-child(even) td {
            background-color: #f8fafc;
        }
        .grid tr:hover td {
            background-color: #eef4ff;
        }
        .grid input[type="submit"] {
            padding: 6px 14px;
            border: 1px solid #dc2626;
            border-radius: 5px;
            background-color: #ffffff;
            color: #dc2626;
            cursor: pointer;
            font-weight: bold;
        }

        .grid input[type="submit"]:hover {
            background-color: #dc2626;
            color: #ffffff;
        }
        @media screen and (max-width: 1050px) {

            .container {
                width: 94%;
                margin: 20px auto;
                padding: 25px;
            }

            .form-row {
                width: 100%;
            }

        }

    </style>

</head>

<body>

<form id="form1" runat="server">

    <div class="container">

        <div class="title">
            📚 Library Management System
        </div>

        <div class="section-title">
            Add New Book
        </div>

        <div class="form-row">

            <asp:Label ID="lblbname"
                runat="server"
                Text="Book Name :"></asp:Label>

            <asp:TextBox ID="txtbname"
                runat="server"
                CssClass="textbox">
            </asp:TextBox>

        </div>

        <div class="form-row">

            <asp:Label ID="lblauthorname"
                runat="server"
                Text="Author Name :"></asp:Label>

            <asp:TextBox ID="txtaname"
                runat="server"
                CssClass="textbox">
            </asp:TextBox>

        </div>

        <div class="form-row">

            <asp:Label ID="lblcategory"
                runat="server"
                Text="Category :"></asp:Label>

            <asp:DropDownList ID="ddlcategory"
                runat="server"
                CssClass="dropdown">

                <asp:ListItem
                    Text="Select Category"
                    Value="">
                </asp:ListItem>

                <asp:ListItem
                    Text="Programming"
                    Value="Programming">
                </asp:ListItem>

                <asp:ListItem
                    Text="History"
                    Value="History">
                </asp:ListItem>

                <asp:ListItem
                    Text="Biography"
                    Value="Biography">
                </asp:ListItem>

                <asp:ListItem
                    Text="Science"
                    Value="Science">
                </asp:ListItem>

                <asp:ListItem
                    Text="Technology"
                    Value="Technology">
                </asp:ListItem>

                <asp:ListItem
                    Text="Business"
                    Value="Business">
                </asp:ListItem>

                <asp:ListItem
                    Text="Others"
                    Value="Others">
                </asp:ListItem>

            </asp:DropDownList>

        </div>

        <div class="form-row">

            <asp:Label ID="lblprice"
                runat="server"
                Text="Price :"></asp:Label>

            <asp:TextBox ID="txtprice"
                runat="server"
                CssClass="textbox">
            </asp:TextBox>

        </div>

        <div class="form-row">

            <asp:Label ID="lblqty"
                runat="server"
                Text="Quantity :"></asp:Label>

            <asp:TextBox ID="txtqty"
                runat="server"
                CssClass="textbox">
            </asp:TextBox>

        </div>


        <br />

        <asp:Button ID="btnsave"
            runat="server"
            Text="Save Book"
            CssClass="button"
            OnClick="btnsave_Click" />

        <asp:Button ID="btnview"
            runat="server"
            Text="View Books"
            CssClass="button"
            OnClick="btnview_Click" />

        <asp:Label ID="lblmsg"
            runat="server"
            CssClass="message">
        </asp:Label>


        <br />
        <br />

        <div class="section-title">
            Books List
        </div>


        <asp:GridView ID="gvBooks"
            runat="server"
            CssClass="grid"
            AutoGenerateColumns="False"
            DataKeyNames="BookID"
            OnRowDeleting="gvBooks_RowDeleting"
            CellPadding="4"
            GridLines="None" ForeColor="#333333">

            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

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

                <asp:ButtonField
                    Text="Delete"
                    CommandName="Delete"
                    ButtonType="Button" />

            </Columns>


            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />


            <HeaderStyle
                BackColor="#5D7B9D"
                ForeColor="White"
                Font-Bold="True" />

            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />

            <RowStyle
                BackColor="#F7F6F3" ForeColor="#333333" />

            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

        </asp:GridView>

    </div>

</form>

</body>

</html>