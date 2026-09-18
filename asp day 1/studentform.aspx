<%@ Page Language="C#" AutoEventWireup="true" CodeFile="studentform.aspx.cs" Inherits="studentform" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Form</title>
    <style type="text/css">
        .auto-style1
        {
            width: 100%;
            height: 204px;
        }
        .auto-style2
        {
            width: 170px;
        }
        .auto-style3
        {
            width: 170px;
            height: 26px;
        }
        .auto-style4
        {
            height: 26px;
        }
        .auto-style5
        {
            width: 170px;
            height: 23px;
        }
        .auto-style6
        {
            height: 23px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    &nbsp;<table class="auto-style1">
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label1" runat="server" Text="StudentName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label2" runat="server" Text="Email"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtemail" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label3" runat="server" Text="Phone_No"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="phno" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="Label4" runat="server" Text="Address"></asp:Label>
                </td>
                <td class="auto-style4">
                    <asp:TextBox ID="txtaddress" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label5" runat="server" Text="Course"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkcsharp" runat="server" Text="C#" />
                    <br />
                    <asp:CheckBox ID="chkdotnet" runat="server" Text=".NET" />
                    <br />
                    <asp:CheckBox ID="chkwebdp" runat="server" Text="Web Devlopment" />
                    <br />
                    <asp:CheckBox ID="chkpython" runat="server" Text="Python" />
                </td>
            </tr>
            <tr>
                <td class="auto-style5">
                    <asp:Button ID="btnregister" runat="server" Text="Register" Width="129px" OnClick="btnregister_Click1" />
                </td>
                <td class="auto-style6"></td>
            </tr>
        </table>
        <asp:Panel ID="pnlDetails" runat="server" CssClass="details-container" Visible="false">
            <h2>Student Details</h2>
            Student Name;<asp:Label ID="lblOutName" runat="server"></asp:Label><br />
            Email: <asp:Label ID="lblOutEmail" runat="server"></asp:Label><br />
            Phone No: <asp:Label ID="lblOutPhone" runat="server"></asp:Label><br />
            Address: <asp:Label ID="lblOutAddress" runat="server"></asp:Label><br />
            Course: <asp:Label ID="lblOutCourse" runat="server"></asp:Label>
        </asp:Panel>
    </form>
</body>
</html>
