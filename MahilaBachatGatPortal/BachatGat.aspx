<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeFile="BachatGat.aspx.cs"
    Inherits="BachatGat" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">

    <style>

        .page-title {
            margin-top: 0;
            font-weight: bold;
        }

        .form-panel,
        .grid-panel {
            background: #ffffff;
            padding: 20px;
            border-radius: 8px;
            margin-bottom: 25px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.08);
        }

        .section-title {
            margin-top: 0;
            margin-bottom: 20px;
            font-weight: bold;
        }

        .required {
            color: red;
        }

        .action-buttons {
            margin-top: 20px;
        }

        .search-box {
            margin-bottom: 20px;
        }

    </style>

</asp:Content>


<asp:Content ID="Content2"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2 class="page-title">
        Bachat Gat Management
    </h2>

    <p class="text-muted">
        Register and manage Mahila Bachat Gat groups.
    </p>


    <!-- REGISTRATION FORM -->

    <div class="form-panel">

        <h4 class="section-title">
            Bachat Gat Registration
        </h4>

        <asp:HiddenField ID="hfBachatGatID"
            runat="server" />


        <!-- ROW 1 -->

        <div class="row">

            <div class="col-md-6">

                <div class="form-group">

                    <label>
                        Gat Name
                        <span class="required">*</span>
                    </label>

                    <asp:TextBox ID="txtGatName"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>

                </div>

            </div>


            <div class="col-md-6">

                <div class="form-group">

                    <label>
                        Registration Number
                    </label>

                    <asp:TextBox ID="txtRegistrationNumber"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>

                </div>

            </div>

        </div>


        <!-- ROW 2 -->

        <div class="row">

            <div class="col-md-4">

                <div class="form-group">

                    <label>
                        Formation Date
                        <span class="required">*</span>
                    </label>

                    <asp:TextBox ID="txtFormationDate"
                        runat="server"
                        TextMode="SingleLine"
                        CssClass="form-control">
                    </asp:TextBox>

                </div>

            </div>


            <div class="col-md-4">

                <div class="form-group">

                    <label>
                        Village
                    </label>

                    <asp:TextBox ID="txtVillage"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>

                </div>

            </div>


            <div class="col-md-4">

                <div class="form-group">

                    <label>
                        Taluka
                    </label>

                    <asp:TextBox ID="txtTaluka"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>

                </div>

            </div>

        </div>


        <!-- ROW 3 -->

        <div class="row">

            <div class="col-md-4">

                <div class="form-group">

                    <label>
                        District
                    </label>

                    <asp:TextBox ID="txtDistrict"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>

                </div>

            </div>


            <div class="col-md-8">

                <div class="form-group">

                    <label>
                        Address
                    </label>

                    <asp:TextBox ID="txtAddress"
                        runat="server"
                        TextMode="MultiLine"
                        Rows="3"
                        CssClass="form-control">
                    </asp:TextBox>

                </div>

            </div>

        </div>


        <!-- ROW 4 -->

        <div class="row">

            <div class="col-md-6">

                <div class="form-group">

                    <label>
                        President Name
                    </label>

                    <asp:TextBox ID="txtPresidentName"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>

                </div>

            </div>


            <div class="col-md-6">

                <div class="form-group">

                    <label>
                        Secretary Name
                    </label>

                    <asp:TextBox ID="txtSecretaryName"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>

                </div>

            </div>

        </div>


        <!-- ROW 5 -->

        <div class="row">

            <div class="col-md-4">

                <div class="form-group">

                    <label>
                        Bank Name
                    </label>

                    <asp:TextBox ID="txtBankName"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>

                </div>

            </div>


            <div class="col-md-4">

                <div class="form-group">

                    <label>
                        Bank Account Number
                    </label>

                    <asp:TextBox ID="txtBankAccount"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>

                </div>

            </div>


            <div class="col-md-4">

                <div class="form-group">

                    <label>
                        IFSC Code
                    </label>

                    <asp:TextBox ID="txtIFSC"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>

                </div>

            </div>

        </div>


        <!-- ROW 6 -->

        <div class="row">

            <div class="col-md-4">

                <div class="form-group">

                    <label>
                        Monthly Saving Amount
                    </label>

                    <asp:TextBox ID="txtMonthlySaving"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>

                </div>

            </div>


            <div class="col-md-4">

                <div class="form-group">

                    <label>
                        Status
                    </label>

                    <asp:DropDownList ID="ddlStatus"
                        runat="server"
                        CssClass="form-control">

                        <asp:ListItem Text="Active"
                            Value="Active">
                        </asp:ListItem>

                        <asp:ListItem Text="Inactive"
                            Value="Inactive">
                        </asp:ListItem>

                    </asp:DropDownList>

                </div>

            </div>

        </div>


        <!-- BUTTONS -->

        <div class="action-buttons">

            <asp:Button ID="btnSave"
                runat="server"
                Text="Save Bachat Gat"
                CssClass="btn btn-primary"
                OnClick="btnSave_Click" />

            &nbsp;

            <asp:Button ID="btnClear"
                runat="server"
                Text="Clear"
                CssClass="btn btn-default"
                OnClick="btnClear_Click" />

        </div>


        <br />

        <asp:Label ID="lblMessage"
            runat="server"
            Font-Bold="true">
        </asp:Label>

    </div>


    <!-- BACHAT GAT LIST -->

    <div class="grid-panel">

        <h4 class="section-title">
            Bachat Gat List
        </h4>


        <!-- SEARCH -->

        <div class="row search-box">

            <div class="col-md-5">

                <asp:TextBox ID="txtSearch"
                    runat="server"
                    CssClass="form-control"
                    placeholder="Search by Gat Name, Registration No. or Village">
                </asp:TextBox>

            </div>


            <div class="col-md-2">

                <asp:Button ID="btnSearch"
                    runat="server"
                    Text="Search"
                    CssClass="btn btn-info"
                    OnClick="btnSearch_Click" />

            </div>

        </div>


        <!-- GRID -->

        <div class="table-responsive">

            <asp:GridView ID="gvBachatGat"
                runat="server"
                AutoGenerateColumns="False"
                CssClass="table table-bordered table-hover"
                DataKeyNames="BachatGatID"
                OnRowCommand="gvBachatGat_RowCommand">

                <Columns>

                    <asp:BoundField
                        DataField="BachatGatID"
                        HeaderText="ID" />

                    <asp:BoundField
                        DataField="GatName"
                        HeaderText="Gat Name" />

                    <asp:BoundField
                        DataField="RegistrationNumber"
                        HeaderText="Registration No." />

                    <asp:BoundField
                        DataField="Village"
                        HeaderText="Village" />

                    <asp:BoundField
                        DataField="Taluka"
                        HeaderText="Taluka" />

                    <asp:BoundField
                        DataField="District"
                        HeaderText="District" />

                    <asp:BoundField
                        DataField="PresidentName"
                        HeaderText="President" />

                    <asp:BoundField
                        DataField="TotalMembers"
                        HeaderText="Members" />

                    <asp:BoundField
                        DataField="MonthlySavingAmount"
                        HeaderText="Monthly Saving" />

                    <asp:BoundField
                        DataField="Status"
                        HeaderText="Status" />


                    <asp:TemplateField HeaderText="Action">

                        <ItemTemplate>

                            <asp:LinkButton
                                ID="btnEdit"
                                runat="server"
                                Text="Edit"
                                CssClass="btn btn-warning btn-xs"
                                CommandName="EditGat"
                                CommandArgument='<%# Eval("BachatGatID") %>'>
                            </asp:LinkButton>

                            &nbsp;

                            <asp:LinkButton
                                ID="btnDelete"
                                runat="server"
                                Text="Delete"
                                CssClass="btn btn-danger btn-xs"
                                CommandName="DeleteGat"
                                CommandArgument='<%# Eval("BachatGatID") %>'
                                OnClientClick="return confirm('Are you sure you want to delete this Bachat Gat?');">
                            </asp:LinkButton>

                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>


                <EmptyDataTemplate>
                    No Bachat Gat records found.
                </EmptyDataTemplate>

            </asp:GridView>

        </div>

    </div>

</asp:Content>