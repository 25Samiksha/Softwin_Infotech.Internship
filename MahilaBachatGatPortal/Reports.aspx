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

            <asp:Button ID="btnFinancialReport"
                runat="server"
                Text="Financial Report"
                CssClass="btn btn-danger"
                OnClick="btnFinancialReport_Click" />

        </div>
    </div>

    <asp:Panel ID="pnlReport"
        runat="server"
        Visible="false"
        ClientIDMode="Static">

        <div class="panel panel-default">

            <div class="panel-heading clearfix">
                <span>Report Result</span>

                <button type="button"
                    class="btn btn-danger btn-sm pull-right"
                    onclick="printReport()">
                    Download PDF
                </button>
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

<script type="text/javascript">

    function printReport() {

        var report = document.getElementById("pnlReport");

        if (!report) {
            alert("Please generate a report first.");
            return;
        }

        var printWindow = window.open(
            "",
            "",
            "width=1000,height=700"
        );

        printWindow.document.write(
            "<html>" +
            "<head>" +
            "<title>Mahila Bachat Gat Report</title>" +
            "<style>" +
            "body {" +
                "font-family: Arial, sans-serif;" +
                "padding: 30px;" +
            "}" +
            "h2 {" +
                "text-align: center;" +
                "margin-bottom: 20px;" +
            "}" +
            "table {" +
                "width: 100%;" +
                "border-collapse: collapse;" +
                "font-size: 12px;" +
            "}" +
            "th, td {" +
                "border: 1px solid #000;" +
                "padding: 6px;" +
                "text-align: left;" +
            "}" +
            "th {" +
                "font-weight: bold;" +
            "}" +
            ".panel-heading {" +
                "display: none;" +
            "}" +
            ".btn {" +
                "display: none;" +
            "}" +
            "</style>" +
            "</head>" +
            "<body>" +
            "<h2>Mahila Bachat Gat Portal</h2>" +
            report.innerHTML +
            "</body>" +
            "</html>"
        );

        printWindow.document.close();

        printWindow.focus();

        setTimeout(function () {
            printWindow.print();
            printWindow.close();
        }, 500);
    }

</script>

</asp:Content>

