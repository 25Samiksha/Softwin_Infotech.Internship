<%@ Page Title="Reports"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeFile="Reports.aspx.cs"
    Inherits="Reports" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    Runat="Server">

    <style type="text/css">

        .reports-container {
            background-color: white;
            padding: 25px;
            border: 1px solid #ddd;
        }

        .reports-header {
            margin-bottom: 25px;
        }

        .reports-header h1 {
            margin: 0;
            color: #1f4e79;
        }

        .filter-section {
            background-color: #f9f9f9;
            border: 1px solid #ddd;
            padding: 25px;
            margin-bottom: 25px;
        }

        .filter-section h2 {
            margin-top: 0;
            color: #1f4e79;
            margin-bottom: 25px;
        }

        .form-row {
            margin-bottom: 18px;
        }

        .form-row label {
            display: block;
            margin-bottom: 7px;
            font-weight: bold;
            color: #333;
        }

        .form-control {
            width: 95%;
            padding: 10px;
            border: 1px solid #ccc;
            font-size: 14px;
        }

        .search-input {
            width: 300px;
            padding: 10px;
            border: 1px solid #ccc;
            font-size: 14px;
        }

        .button-row {
            margin-top: 25px;
        }

        .search-button {
            padding: 11px 22px;
            background-color: #1f4e79;
            color: white;
            border: none;
            cursor: pointer;
            font-weight: bold;
        }

        .clear-button {
            padding: 11px 22px;
            background-color: #777;
            color: white;
            border: none;
            cursor: pointer;
            margin-left: 8px;
            font-weight: bold;
        }

        .export-button {
            padding: 11px 22px;
            background-color: #2e7d32;
            color: white;
            border: none;
            cursor: pointer;
            font-weight: bold;
            margin-right: 8px;
        }

        .print-button {
            padding: 11px 22px;
            background-color: #5b5b5b;
            color: white;
            border: none;
            cursor: pointer;
            font-weight: bold;
        }

        .report-section {
            background-color: white;
            border: 1px solid #ddd;
            padding: 25px;
        }

        .report-section h2 {
            margin-top: 0;
            color: #1f4e79;
            margin-bottom: 20px;
        }

        .grid {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        .grid th {
            background-color: #1f4e79;
            color: white;
            padding: 12px;
            text-align: left;
        }

        .grid td {
            padding: 11px;
            border-bottom: 1px solid #ddd;
        }

        .grid tr:hover {
            background-color: #f5f5f5;
        }

        .report-buttons {
            margin-top: 20px;
        }

        .message {
            display: block;
            margin-top: 15px;
            font-weight: bold;
        }

        @media print {

            .reports-header {
                margin-bottom: 10px;
            }

            .filter-section {
                display: none;
            }

            .report-buttons {
                display: none;
            }

            .message {
                display: none;
            }

            .reports-container {
                border: none;
            }
        }

    </style>

</asp:Content>


<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    Runat="Server">

    <div class="reports-container">


        <div class="reports-header">

            <h1>
                Reports
            </h1>

        </div>
        <div class="filter-section">

            <h2>
                Search and Filter Reports
            </h2>


            <div class="form-row">

                <asp:Label ID="lblReportType"
                    runat="server"
                    Text="Report Type">
                </asp:Label>

                <asp:DropDownList ID="ddlReportType"
                    runat="server"
                    CssClass="form-control"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">

                    <asp:ListItem
                        Text="Select Report"
                        Value="">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Attendance Report"
                        Value="Attendance">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Leave Report"
                        Value="Leave">
                    </asp:ListItem>

                </asp:DropDownList>

            </div>
            <div class="form-row">

                <asp:Label ID="lblEmployee"
                    runat="server"
                    Text="Employee">
                </asp:Label>

                <asp:DropDownList ID="ddlEmployee"
                    runat="server"
                    CssClass="form-control">

                    <asp:ListItem
                        Text="All Employees"
                        Value="">
                    </asp:ListItem>

                </asp:DropDownList>

            </div>

            <div class="form-row">

                <asp:Label ID="lblFromDate"
                    runat="server"
                    Text="From Date">
                </asp:Label>

                <asp:TextBox ID="txtFromDate"
                    runat="server"
                    CssClass="form-control">
                </asp:TextBox>

            </div>
            <div class="form-row">

                <asp:Label ID="lblToDate"
                    runat="server"
                    Text="To Date">
                </asp:Label>

                <asp:TextBox ID="txtToDate"
                    runat="server"
                    CssClass="form-control">
                </asp:TextBox>

            </div>

            <div class="form-row">

                <asp:Label ID="lblStatus"
                    runat="server"
                    Text="Status">
                </asp:Label>

                <asp:DropDownList ID="ddlStatus"
                    runat="server"
                    CssClass="form-control">

                    <asp:ListItem
                        Text="All Status"
                        Value="">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Present"
                        Value="Present">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Half Day"
                        Value="Half Day">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Absent"
                        Value="Absent">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Pending"
                        Value="Pending">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Approved"
                        Value="Approved">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Rejected"
                        Value="Rejected">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Cancelled"
                        Value="Cancelled">
                    </asp:ListItem>

                </asp:DropDownList>

            </div>

            <div class="form-row">

                <asp:Label ID="lblSearch"
                    runat="server"
                    Text="Search Employee">
                </asp:Label>

                <asp:TextBox ID="txtSearch"
                    runat="server"
                    CssClass="search-input">
                </asp:TextBox>

            </div>

            <div class="button-row">

                <asp:Button ID="btnSearch"
                    runat="server"
                    Text="Search / Filter"
                    CssClass="search-button"
                    OnClick="btnSearch_Click" />

                <asp:Button ID="btnClear"
                    runat="server"
                    Text="Clear"
                    CssClass="clear-button"
                    OnClick="btnClear_Click" />

            </div>
            <asp:Label ID="lblMessage"
                runat="server"
                CssClass="message">
            </asp:Label>

        </div>
        <asp:Panel ID="pnlReport"
            runat="server"
            CssClass="report-section"
            Visible="true">

            <h2>

                <asp:Label ID="lblReportTitle"
                    runat="server"
                    Text="Report Results">
                </asp:Label>

            </h2>

            <asp:Panel ID="pnlAttendanceReport"
                runat="server"
                Visible="false">

                <asp:GridView ID="gvAttendance"
                    runat="server"
                    AutoGenerateColumns="False"
                    CssClass="grid">

                    <Columns>

                        <asp:BoundField
                            DataField="AttendanceDate"
                            HeaderText="Date" />

                        <asp:BoundField
                            DataField="EmployeeName"
                            HeaderText="Employee" />

                        <asp:BoundField
                            DataField="CheckIn"
                            HeaderText="Check In" />

                        <asp:BoundField
                            DataField="CheckOut"
                            HeaderText="Check Out" />

                        <asp:BoundField
                            DataField="WorkingHours"
                            HeaderText="Working Hours" />

                        <asp:BoundField
                            DataField="Status"
                            HeaderText="Status" />

                    </Columns>

                </asp:GridView>

            </asp:Panel>
            <asp:Panel ID="pnlLeaveReport"
                runat="server"
                Visible="false">

                <asp:GridView ID="gvLeave"
                    runat="server"
                    AutoGenerateColumns="False"
                    CssClass="grid">

                    <Columns>

                        <asp:BoundField
                            DataField="LeaveID"
                            HeaderText="ID" />

                        <asp:BoundField
                            DataField="EmployeeName"
                            HeaderText="Employee" />

                        <asp:BoundField
                            DataField="LeaveType"
                            HeaderText="Leave Type" />

                        <asp:BoundField
                            DataField="FromDate"
                            HeaderText="From Date" />

                        <asp:BoundField
                            DataField="ToDate"
                            HeaderText="To Date" />

                        <asp:BoundField
                            DataField="LeaveDays"
                            HeaderText="Days" />

                        <asp:BoundField
                            DataField="Reason"
                            HeaderText="Reason" />

                        <asp:BoundField
                            DataField="Status"
                            HeaderText="Status" />

                    </Columns>

                </asp:GridView>

            </asp:Panel>

            <div class="report-buttons">

                <asp:Button ID="btnExport"
                    runat="server"
                    Text="Export CSV"
                    CssClass="export-button"
                    OnClick="btnExport_Click" />

                <asp:Button ID="btnPrint"
                    runat="server"
                    Text="Print Report"
                    CssClass="print-button"
                    OnClientClick="window.print(); return false;" />

            </div>

        </asp:Panel>

    </div>

</asp:Content>