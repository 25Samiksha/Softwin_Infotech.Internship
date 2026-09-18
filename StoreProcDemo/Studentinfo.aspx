<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="Studentinfo.aspx.cs"
    Inherits="Studentinfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">

    <title>Student Information</title>

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
            width: 500px;
            margin: 50px auto;
            background-color: white;
            padding: 30px;
            border-radius: 10px;
        }

        .auto-style1
        {
            width: 100%;
        }

        .auto-style3
        {
            width: 139px;
        }

        .auto-style4
        {
            width: 139px;
            height: 23px;
        }

        .auto-style5
        {
            height: 23px;
        }

        .textbox
        {
            width: 220px;
            padding: 6px;
        }

        .dropdown
        {
            width: 235px;
            padding: 6px;
        }

        /* Buttons */

        .button-area
        {
            text-align: center;
            white-space: nowrap;
            padding-top: 10px;
        }

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
            height: 37px;
        }

        .button:hover
        {
            background-color: #286090;
        }

        .message
        {
            font-weight: bold;
            display: block;
            text-align: center;
            margin-top: 15px;
        }

    </style>

</head>

<body>

<form id="form1" runat="server">

<div class="container">

    <h2>Student Information</h2>

    <table class="auto-style1">

        <!-- Student Name -->

        <tr>

            <td class="auto-style3">

                <asp:Label
                    ID="lblstdname"
                    runat="server"
                    Text="Student Name :">
                </asp:Label>

            </td>

            <td>

                <asp:TextBox
                    ID="txtstdname"
                    runat="server"
                    CssClass="textbox">
                </asp:TextBox>

            </td>

        </tr>


        <!-- Email -->

        <tr>

            <td class="auto-style3">

                <asp:Label
                    ID="lblemail"
                    runat="server"
                    Text="Email :">
                </asp:Label>

            </td>

            <td>

                <asp:TextBox
                    ID="txtemail"
                    runat="server"
                    CssClass="textbox">
                </asp:TextBox>

            </td>

        </tr>


        <!-- Course -->

        <tr>

            <td class="auto-style4">

                <asp:Label
                    ID="lblcourse"
                    runat="server"
                    Text="Course :">
                </asp:Label>

            </td>

            <td class="auto-style5">

                <asp:DropDownList
                    ID="ddlcourse"
                    runat="server"
                    CssClass="dropdown">

                    <asp:ListItem
                        Text="Select"
                        Value="">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="B.Tech"
                        Value="B.Tech">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="BCA"
                        Value="BCA">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="MCA"
                        Value="MCA">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="B.Sc"
                        Value="B.Sc">
                    </asp:ListItem>

                </asp:DropDownList>

            </td>

        </tr>


        <!-- Branch -->

        <tr>

            <td class="auto-style3">

                <asp:Label
                    ID="lblbranch"
                    runat="server"
                    Text="Branch :">
                </asp:Label>

            </td>

            <td>

                <asp:DropDownList
                    ID="ddlbranch"
                    runat="server"
                    CssClass="dropdown">

                    <asp:ListItem
                        Text="Select"
                        Value="">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="CSE"
                        Value="CSE">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="IT"
                        Value="IT">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="ENTC"
                        Value="ENTC">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Mechanical"
                        Value="Mechanical">
                    </asp:ListItem>

                </asp:DropDownList>

            </td>

        </tr>


        <!-- Phone -->

        <tr>

            <td class="auto-style3">

                <asp:Label
                    ID="lblphno"
                    runat="server"
                    Text="Phone No :">
                </asp:Label>

            </td>

            <td>

                <asp:TextBox
                    ID="txtphno"
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
                        Text="Save"
                        CssClass="button"
                        OnClick="btnsave_Click" />

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

</div>

</form>

</body>

</html>