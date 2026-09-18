<%@ Page Title="Reports" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="container-fluid">

    <h2 class="page-title">Reports</h2>

    <asp:Label ID="lblMessage" runat="server"></asp:Label>

    <div class="panel panel-primary">

        <div class="panel-heading">
            Report Filters
        </div>

        <div class="panel-body">

            <div class="row">

                <div class="col-md-4">

                    <label>Bachat Gat</label>

                    <asp:DropDownList ID="ddlBachatGat"
                        runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                </div>

                <div class="col-md-4">

                    <label>From Date</label>

                    <asp:TextBox ID="txtFromDate"
                        runat="server"
                        CssClass="form-control"
                        TextMode="SingleLine">
                    </asp:TextBox>

                </div>

                <div class="col-md-4">

                    <label>To Date</label>

                    <asp:TextBox ID="txtToDate"
                        runat="server"
                        CssClass="form-control"
                        TextMode="SingleLine">
                    </asp:TextBox>

                </div>

            </div>

            <br />

            <asp:Button ID="btnMemberReport"
                runat="server"
                Text="Member Report"
                CssClass="btn btn-primary"
                OnClick="btnMemberReport_Click" />

            <asp:Button ID="btnSavingsReport"
                runat="server"
                Text="Savings Report"
                CssClass="btn btn-success"
                OnClick="btnSavingsReport_Click" />

            <asp:Button ID="btnLoanReport"
                runat="server"
                Text="Loan Report"
                CssClass="btn btn-warning"
                OnClick="btnLoanReport_Click" />

            <asp:Button ID="btnMeetingReport"
                runat="server"
                Text="Meeting Report"
                CssClass="btn btn-info"
                OnClick="btnMeetingReport_Click" />

            <a href="FinancialReport.aspx"
               class="btn btn-danger">
                Financial Report
            </a>

        </div>
    </div>


    <asp:Panel ID="pnlReport"
        runat="server"
        Visible="false">

        <div class="panel panel-default">

            <div class="panel-heading">
                Report Result
            </div>

            <div class="panel-body">

                <asp:Label ID="lblReportTitle"
                    runat="server"
                    Font-Bold="true">
                </asp:Label>

                <br /><br />

                <asp:GridView ID="gvReport"
                    runat="server"
                    AutoGenerateColumns="true"
                    CssClass="table table-bordered table-striped">
                </asp:GridView>

            </div>

        </div>

    </asp:Panel>

</div>

</asp:Content>