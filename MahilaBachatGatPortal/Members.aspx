<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeFile="Members.aspx.cs"
    Inherits="Members" %>

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
            background: #fff;
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
            margin-top: 25px;
        }

        .search-box {
            margin-bottom: 20px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2 class="page-title">Member Management</h2>
    <p class="text-muted">
        Register and manage Bachat Gat members.
    </p>

    <div class="form-panel">

        <h4 class="section-title">Member Registration</h4>

        <asp:HiddenField ID="hfMemberID" runat="server" />

        <div class="row">

            <div class="col-md-4">
                <div class="form-group">
                    <label>Bachat Gat <span class="required">*</span></label>
                    <asp:DropDownList ID="ddlBachatGat"
                        runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="col-md-4">
                <div class="form-group">
                    <label>Member Code <span class="required">*</span></label>
                    <asp:TextBox ID="txtMemberCode"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>
            </div>

            <div class="col-md-4">
                <div class="form-group">
                    <label>Member Name <span class="required">*</span></label>
                    <asp:TextBox ID="txtMemberName"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>
            </div>

        </div>

        <div class="row">

            <div class="col-md-4">
                <div class="form-group">
                    <label>Father / Husband Name</label>
                    <asp:TextBox ID="txtFatherHusband"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>
            </div>

            <div class="col-md-4">
                <div class="form-group">
                    <label>Date of Birth</label>
                    <asp:TextBox ID="txtDOB"
                        runat="server"
                        TextMode="SingleLine"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>
            </div>

            <div class="col-md-4">
                <div class="form-group">
                    <label>Mobile</label>
                    <asp:TextBox ID="txtMobile"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>
            </div>

        </div>

        <div class="row">

            <div class="col-md-4">
                <div class="form-group">
                    <label>Email</label>
                    <asp:TextBox ID="txtEmail"
                        runat="server"
                        TextMode="SingleLine"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>
            </div>

            <div class="col-md-4">
                <div class="form-group">
                    <label>Village</label>
                    <asp:TextBox ID="txtVillage"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>
            </div>

            <div class="col-md-4">
                <div class="form-group">
                    <label>Taluka</label>
                    <asp:TextBox ID="txtTaluka"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>
            </div>

        </div>

        <div class="row">

            <div class="col-md-4">
                <div class="form-group">
                    <label>District</label>
                    <asp:TextBox ID="txtDistrict"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>
            </div>

            <div class="col-md-4">
                <div class="form-group">
                    <label>Join Date <span class="required">*</span></label>
                    <asp:TextBox ID="txtJoinDate"
                        runat="server"
                        TextMode="SingleLine"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>
            </div>

            <div class="col-md-4">
                <div class="form-group">
                    <label>Occupation</label>
                    <asp:TextBox ID="txtOccupation"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>
            </div>

        </div>

        <div class="row">

            <div class="col-md-8">
                <div class="form-group">
                    <label>Address</label>
                    <asp:TextBox ID="txtAddress"
                        runat="server"
                        TextMode="MultiLine"
                        Rows="3"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>
            </div>

            <div class="col-md-4">
                <div class="form-group">
                    <label>Status</label>
                    <asp:DropDownList ID="ddlStatus"
                        runat="server"
                        CssClass="form-control">

                        <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                        <asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>

                    </asp:DropDownList>
                </div>
            </div>

        </div>

        <div class="action-buttons">

            <asp:Button ID="btnSave"
                runat="server"
                Text="Save Member"
                CssClass="btn btn-primary"
                OnClick="btnSave_Click" />

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

    <!-- MEMBER LIST -->

    <div class="grid-panel">

        <h4 class="section-title">Member List</h4>

        <div class="row search-box">

            <div class="col-md-4">

                <asp:TextBox ID="txtSearch"
                    runat="server"
                    CssClass="form-control"
                    placeholder="Search by Member Name or Mobile">
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

        <div class="table-responsive">

            <asp:GridView ID="gvMembers"
                runat="server"
                AutoGenerateColumns="False"
                CssClass="table table-bordered table-hover"
                DataKeyNames="MemberID"
                OnRowCommand="gvMembers_RowCommand">

                <Columns>

                    <asp:BoundField DataField="MemberID"
                        HeaderText="ID" />

                    <asp:BoundField DataField="MemberCode"
                        HeaderText="Code" />

                    <asp:BoundField DataField="MemberName"
                        HeaderText="Member Name" />

                    <asp:BoundField DataField="GatName"
                        HeaderText="Bachat Gat" />

                    <asp:BoundField DataField="Mobile"
                        HeaderText="Mobile" />

                    <asp:BoundField DataField="Village"
                        HeaderText="Village" />

                    <asp:BoundField DataField="Status"
                        HeaderText="Status" />

                    <asp:TemplateField HeaderText="Action">

                        <ItemTemplate>

                            <asp:LinkButton ID="btnEdit"
                                runat="server"
                                Text="Edit"
                                CssClass="btn btn-warning btn-xs"
                                CommandName="EditMember"
                                CommandArgument='<%# Eval("MemberID") %>'>
                            </asp:LinkButton>

                            &nbsp;

                            <asp:LinkButton ID="btnDelete"
                                runat="server"
                                Text="Delete"
                                CssClass="btn btn-danger btn-xs"
                                CommandName="DeleteMember"
                                CommandArgument='<%# Eval("MemberID") %>'
                                OnClientClick="return confirm('Are you sure you want to delete this member?');">
                            </asp:LinkButton>

                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>

                <EmptyDataTemplate>
                    No members found.
                </EmptyDataTemplate>

            </asp:GridView>

        </div>

    </div>

</asp:Content>