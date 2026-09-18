<%@ Page Language="C#" AutoEventWireup="true" CodeFile="validatorDemo.aspx.cs" Inherits="validatorDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1
        {
            width: 100%;
        }
        .auto-style2
        {
            width: 296px;
        }
        .auto-style3
        {
            width: 296px;
            height: 26px;
        }
        .auto-style4
        {
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lblname" runat="server" Text="Customer Name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorname" runat="server" ControlToValidate="txtname" ErrorMessage="Customer Name is required" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lblnum" runat="server" Text="Mobile Number"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtnum" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatornum" runat="server" ControlToValidate="txtnum" ErrorMessage="Enter a valid 10-digit mobile number." ForeColor="Red" ValidationExpression="^[0-9]{10}$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="lblemail" runat="server" Text="Email"></asp:Label>
                </td>
                <td class="auto-style4">
                    <asp:TextBox ID="txtemail" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtemail" ErrorMessage="Enter Valid email" ForeColor="Red" ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="lblmovie" runat="server" Text="Movies"></asp:Label>
                </td>
                <td class="auto-style4">
                    <asp:DropDownList ID="ddmovie" runat="server">
                        <asp:ListItem Value="0">Select Movie</asp:ListItem>
                        <asp:ListItem>Jungle Book</asp:ListItem>
                        <asp:ListItem>HouseFull</asp:ListItem>
                        <asp:ListItem>Dhurandar</asp:ListItem>
                        <asp:ListItem>Taare Zameen Par</asp:ListItem>
                        <asp:ListItem>3 idiot</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddmovie" ErrorMessage="Select Atleast One Movie" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lblticketnum" runat="server" Text="Number of Ticket"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtticketnum" runat="server"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtticketnum" ErrorMessage="Number of tickets must be between 1 and 10." ForeColor="Red" MaximumValue="10" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lbldate" runat="server" Text="Booking Date"></asp:Label>
                    <br />
                </td>
                <td>
                    <asp:TextBox ID="txtdate" runat="server" TextMode="Date"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatordate" runat="server" ControlToValidate="txtdate" ErrorMessage="Booking date is required." ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Button ID="btnbook" runat="server" OnClick="btnbook_Click" Text="Book Ticket" />
                    <br />
                    <br />
                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                    <br />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
