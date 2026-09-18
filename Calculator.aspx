<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="Calculator.aspx.cs"
    Inherits="calculatorDemo.Calculator"
    MasterPageFile="~/Site1.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style type="text/css">
        .calculator-table {
            width: 100%;
            max-width: 500px;
            margin-top: 20px;
            border-collapse: separate;
            border-spacing: 10px;
        }
        .calc-row {
            padding: 8px;
            display: block;
        }
    </style>

    <table class="calculator-table">
        <tr class="calc-row">
            <td>
                <asp:Label ID="lblnum1" runat="server" Text="Enter Number: "></asp:Label>
                <asp:TextBox ID="txtnum1" runat="server"></asp:TextBox>
            </td>
        </tr>

        <tr class="calc-row">
            <td>
                <asp:Label ID="lblnum2" runat="server" Text="Enter Number: "></asp:Label>
                <asp:TextBox ID="txtnum2" runat="server"></asp:TextBox>
            </td>
        </tr>

        <tr class="calc-row">
            <td>
                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" Width="80px" />
                &nbsp;&nbsp;
                <asp:Button ID="btnSub" runat="server" OnClick="btnSub_Click" Text="Sub" Width="80px" />
                &nbsp;&nbsp;
                <asp:Button ID="btnMul" runat="server" OnClick="btnMul_Click" Text="Multiply" Width="80px" />
                &nbsp;&nbsp;
                <asp:Button ID="btnDiv" runat="server" OnClick="btnDiv_Click" Text="Div" Width="80px" />
            </td>
        </tr>

        <tr class="calc-row">
            <td>
                <asp:Label ID="lblResult" runat="server" Text="Result: "></asp:Label>
                <asp:TextBox ID="txtResult" runat="server" Width="150px"></asp:TextBox>
            </td>
        </tr>
    </table>

</asp:Content>
