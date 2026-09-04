<%@ Page Title="Department Management"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeFile="Department.aspx.cs"
    Inherits="Department" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    Runat="Server">

<style type="text/css">

    .page-title {
        font-size: 28px;
        font-weight: bold;
        margin-bottom: 20px;
    }

    .box {
        background: #fff;
        border: 1px solid #ddd;
        padding: 20px;
        margin-bottom: 20px;
    }

    .search-input {
        width: 350px;
        padding: 10px;
        border: 1px solid #ccc;
    }

    .button {
        padding: 10px 18px;
        border: none;
        cursor: pointer;
        color: white;
        margin-right: 5px;
    }

    .primary {
        background: #1f4e79;
    }

    .success {
        background: #5b8c5a;
    }

    .danger {
        background: #c0392b;
    }

    .gray {
        background: #777;
    }

    .form-label {
        display: block;
        font-weight: bold;
        margin-bottom: 5px;
    }

    .form-input {
        width: 95%;
        padding: 10px;
        border: 1px solid #ccc;
        margin-bottom: 15px;
    }

    .textarea {
        height: 90px;
    }

    .grid {
        width: 100%;
        border-collapse: collapse;
    }

    .grid th {
        background: #1f4e79;
        color: white;
        padding: 12px;
        text-align: left;
    }

    .grid td {
        padding: 10px;
        border-bottom: 1px solid #ddd;
    }

    .message {
        display: block;
        margin-bottom: 15px;
        font-weight: bold;
    }

</style>

</asp:Content>


<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    Runat="Server">

<asp:Label ID="lblMessage"
    runat="server"
    CssClass="message">
</asp:Label>


<asp:Panel ID="pnlDepartmentList"
    runat="server">

    <div class="page-title">
        Department Management
    </div>

    <div class="box">

        <asp:TextBox
            ID="txtSearch"
            runat="server"
            CssClass="search-input">
        </asp:TextBox>

        <asp:Button
            ID="btnSearch"
            runat="server"
            Text="Search"
            CssClass="button primary"
            OnClick="btnSearch_Click" />

        <asp:Button
            ID="btnAddDepartment"
            runat="server"
            Text="+ Add Department"
            CssClass="button success"
            OnClick="btnAddDepartment_Click" />

    </div>


    <div class="box">

        <asp:GridView
            ID="gvDepartments"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="grid"
            OnRowCommand="gvDepartments_RowCommand">

            <Columns>

                <asp:BoundField
                    DataField="DepartmentID"
                    HeaderText="ID" />

                <asp:BoundField
                    DataField="DepartmentName"
                    HeaderText="Department Name" />

                <asp:BoundField
                    DataField="Description"
                    HeaderText="Description" />

                <asp:TemplateField HeaderText="Actions">

                    <ItemTemplate>

                        <asp:Button
                            ID="btnView"
                            runat="server"
                            Text="View"
                            CssClass="button primary"
                            CommandName="ViewDepartment"
                            CommandArgument='<%# Eval("DepartmentID") %>' />

                        <asp:Button
                            ID="btnEdit"
                            runat="server"
                            Text="Edit"
                            CssClass="button success"
                            CommandName="EditDepartment"
                            CommandArgument='<%# Eval("DepartmentID") %>' />

                        <asp:Button
                            ID="btnDelete"
                            runat="server"
                            Text="Delete"
                            CssClass="button danger"
                            CommandName="DeleteDepartment"
                            CommandArgument='<%# Eval("DepartmentID") %>'
                            OnClientClick="return confirm('Are you sure you want to delete this department?');" />

                    </ItemTemplate>

                </asp:TemplateField>

            </Columns>

        </asp:GridView>

    </div>

</asp:Panel>

<asp:Panel ID="pnlDepartmentForm"
    runat="server"
    Visible="false">

    <div class="box">

        <h2>
            <asp:Label
                ID="lblFormTitle"
                runat="server"
                Text="Add Department">
            </asp:Label>
        </h2>

        <asp:HiddenField
            ID="hfDepartmentID"
            runat="server" />


        <asp:Label
            ID="Label1"
            runat="server"
            Text="Department Name"
            CssClass="form-label" />

        <asp:TextBox
            ID="txtDepartmentName"
            runat="server"
            CssClass="form-input" />


        <asp:Label
            ID="Label2"
            runat="server"
            Text="Description"
            CssClass="form-label" />

        <asp:TextBox
            ID="txtDescription"
            runat="server"
            TextMode="MultiLine"
            CssClass="form-input textarea" />


        <asp:Button
            ID="btnSave"
            runat="server"
            Text="Save Department"
            CssClass="button primary"
            OnClick="btnSave_Click" />

        <asp:Button
            ID="btnCancel"
            runat="server"
            Text="Cancel"
            CssClass="button gray"
            OnClick="btnCancel_Click" />

    </div>

</asp:Panel>

<asp:Panel ID="pnlDepartmentDetails"
    runat="server"
    Visible="false">

    <div class="box">

        <h2>Department Details</h2>

        <p>
            <b>ID:</b>
            <asp:Label
                ID="lblDetailsID"
                runat="server" />
        </p>

        <p>
            <b>Name:</b>
            <asp:Label
                ID="lblDetailsName"
                runat="server" />
        </p>

        <p>
            <b>Description:</b>
            <asp:Label
                ID="lblDetailsDescription"
                runat="server" />
        </p>

        <asp:Button
            ID="btnBack"
            runat="server"
            Text="Back"
            CssClass="button gray"
            OnClick="btnBack_Click" />

    </div>

</asp:Panel>

</asp:Content>