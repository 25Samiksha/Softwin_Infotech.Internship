<%@ Page Title="Designation Management"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeFile="Designation.aspx.cs"
    Inherits="Designation" %>

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

<asp:Label
    ID="lblMessage"
    runat="server"
    CssClass="message">
</asp:Label>

<asp:Panel
    ID="pnlDesignationList"
    runat="server">

    <div class="page-title">
        Designation Management
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
            ID="btnAddDesignation"
            runat="server"
            Text="+ Add Designation"
            CssClass="button success"
            OnClick="btnAddDesignation_Click" />

    </div>


    <div class="box">

        <asp:GridView
            ID="gvDesignations"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="grid"
            OnRowCommand="gvDesignations_RowCommand">

            <Columns>

                <asp:BoundField
                    DataField="DesignationID"
                    HeaderText="ID" />

                <asp:BoundField
                    DataField="DesignationName"
                    HeaderText="Designation Name" />

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
                            CommandName="ViewDesignation"
                            CommandArgument='<%# Eval("DesignationID") %>' />

                        <asp:Button
                            ID="btnEdit"
                            runat="server"
                            Text="Edit"
                            CssClass="button success"
                            CommandName="EditDesignation"
                            CommandArgument='<%# Eval("DesignationID") %>' />

                        <asp:Button
                            ID="btnDelete"
                            runat="server"
                            Text="Delete"
                            CssClass="button danger"
                            CommandName="DeleteDesignation"
                            CommandArgument='<%# Eval("DesignationID") %>'
                            OnClientClick="return confirm('Are you sure you want to delete this designation?');" />

                    </ItemTemplate>

                </asp:TemplateField>

            </Columns>

        </asp:GridView>

    </div>

</asp:Panel>

<asp:Panel
    ID="pnlDesignationForm"
    runat="server"
    Visible="false">

    <div class="box">

        <h2>

            <asp:Label
                ID="lblFormTitle"
                runat="server"
                Text="Add Designation">
            </asp:Label>

        </h2>


        <asp:HiddenField
            ID="hfDesignationID"
            runat="server" />


        <asp:Label
            ID="Label1"
            runat="server"
            Text="Designation Name"
            CssClass="form-label" />

        <asp:TextBox
            ID="txtDesignationName"
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
            Text="Save Designation"
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

<asp:Panel
    ID="pnlDesignationDetails"
    runat="server"
    Visible="false">

    <div class="box">

        <h2>Designation Details</h2>

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