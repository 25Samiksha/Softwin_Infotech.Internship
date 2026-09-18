<%@ Page Title="Scheme Applications" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeFile="SchemeApplication.aspx.cs"
    Inherits="SchemeApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">

        <h2 class="page-title">Scheme Applications</h2>

        <asp:Label ID="lblMessage" runat="server"></asp:Label>

        <asp:HiddenField ID="hfApplicationID" runat="server" />

        <div class="panel panel-primary">

            <div class="panel-heading">
                Scheme Application
            </div>

            <div class="panel-body">

                <div class="row">

                    <div class="col-md-4">
                        <label>Government Scheme</label>

                        <asp:DropDownList ID="ddlScheme"
                            runat="server"
                            CssClass="form-control">
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label>Bachat Gat</label>

                        <asp:DropDownList ID="ddlBachatGat"
                            runat="server"
                            CssClass="form-control"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlBachatGat_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label>Member</label>

                        <asp:DropDownList ID="ddlMember"
                            runat="server"
                            CssClass="form-control">
                        </asp:DropDownList>
                    </div>

                </div>

                <br />

                <div class="row">

                    <div class="col-md-4">
                        <label>Application Date</label>

                        <asp:TextBox ID="txtApplicationDate"
                            runat="server"
                            CssClass="form-control"
                            TextMode="SingleLine">
                        </asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label>Application Number</label>

                        <asp:TextBox ID="txtApplicationNumber"
                            runat="server"
                            CssClass="form-control">
                        </asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label>Status</label>

                        <asp:DropDownList ID="ddlStatus"
                            runat="server"
                            CssClass="form-control">

                            <asp:ListItem>Pending</asp:ListItem>
                            <asp:ListItem>Submitted</asp:ListItem>
                            <asp:ListItem>Approved</asp:ListItem>
                            <asp:ListItem>Rejected</asp:ListItem>
                            <asp:ListItem>Completed</asp:ListItem>

                        </asp:DropDownList>
                    </div>

                </div>

                <br />

                <div class="row">

                    <div class="col-md-6">

                        <label>Documents</label>

                        <asp:TextBox ID="txtDocuments"
                            runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="3">
                        </asp:TextBox>

                    </div>

                    <div class="col-md-6">

                        <label>Remarks</label>

                        <asp:TextBox ID="txtRemarks"
                            runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="3">
                        </asp:TextBox>

                    </div>

                </div>

                <br />

                <div class="row">

                    <div class="col-md-6">

                        <label>Approved Date</label>

                        <asp:TextBox ID="txtApprovedDate"
                            runat="server"
                            CssClass="form-control"
                            TextMode="SingleLine">
                        </asp:TextBox>

                    </div>

                    <div class="col-md-6">

                        <label>Rejection Reason</label>

                        <asp:TextBox ID="txtRejectionReason"
                            runat="server"
                            CssClass="form-control">
                        </asp:TextBox>

                    </div>

                </div>

                <br />

                <asp:Button ID="btnSave"
                    runat="server"
                    Text="Save Application"
                    CssClass="btn btn-success"
                    OnClick="btnSave_Click" />

                <asp:Button ID="btnClear"
                    runat="server"
                    Text="Clear"
                    CssClass="btn btn-default"
                    OnClick="btnClear_Click" />

            </div>
        </div>


        <div class="panel panel-default">

            <div class="panel-heading">
                Search Applications
            </div>

            <div class="panel-body">

                <div class="row">

                    <div class="col-md-10">

                        <asp:TextBox ID="txtSearch"
                            runat="server"
                            CssClass="form-control"
                            placeholder="Search member, scheme or application number">
                        </asp:TextBox>

                    </div>

                    <div class="col-md-2">

                        <asp:Button ID="btnSearch"
                            runat="server"
                            Text="Search"
                            CssClass="btn btn-primary"
                            OnClick="btnSearch_Click" />

                    </div>

                </div>

                <br />

                <asp:Button ID="btnShowAll"
                    runat="server"
                    Text="Show All"
                    CssClass="btn btn-info"
                    OnClick="btnShowAll_Click" />

            </div>
        </div>


        <asp:GridView ID="gvApplications"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-bordered table-striped"
            DataKeyNames="ApplicationID"
            OnRowCommand="gvApplications_RowCommand">

            <Columns>

                <asp:BoundField DataField="ApplicationID"
                    HeaderText="ID" />

                <asp:BoundField DataField="SchemeName"
                    HeaderText="Scheme" />

                <asp:BoundField DataField="GatName"
                    HeaderText="Bachat Gat" />

                <asp:BoundField DataField="MemberName"
                    HeaderText="Member" />

                <asp:BoundField DataField="ApplicationDate"
                    HeaderText="Application Date"
                    DataFormatString="{0:dd-MM-yyyy}" />

                <asp:BoundField DataField="ApplicationNumber"
                    HeaderText="Application No." />

                <asp:BoundField DataField="Status"
                    HeaderText="Status" />

                <asp:BoundField DataField="ApprovedDate"
                    HeaderText="Approved Date"
                    DataFormatString="{0:dd-MM-yyyy}" />

                <asp:ButtonField
                    ButtonType="Button"
                    CommandName="EditApplication"
                    Text="Edit"
                    ControlStyle-CssClass="btn btn-warning btn-xs" />

                <asp:ButtonField
                    ButtonType="Button"
                    CommandName="DeleteApplication"
                    Text="Delete"
                    ControlStyle-CssClass="btn btn-danger btn-xs" />

            </Columns>

        </asp:GridView>

    </div>

</asp:Content>