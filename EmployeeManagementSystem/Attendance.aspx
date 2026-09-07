<%@ Page Title="Attendance"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeFile="Attendance.aspx.cs"
    Inherits="Attendance" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    Runat="Server">

    <style>

        .attendance-container {
            background-color: white;
            padding: 25px;
            border: 1px solid #ddd;
        }
        .attendance-header {
            width: 100%;
            overflow: hidden;
            margin-bottom: 25px;
        }

        .attendance-header h1 {
            margin: 0;
            color: #1f4e79;
        }
        .attendance-form {
            background-color: #f9f9f9;
            border: 1px solid #ddd;
            padding: 25px;
            margin-bottom: 30px;
        }

        .attendance-form h2 {
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
        .check-button-row {
            margin-top: 10px;
            margin-bottom: 20px;
        }

        .check-in-button {
            padding: 11px 22px;
            background-color: #2e7d32;
            color: white;
            border: none;
            cursor: pointer;
            font-weight: bold;
            margin-right: 10px;
        }

        .check-in-button:hover {
            background-color: #256628;
        }

        .check-out-button {
            padding: 11px 22px;
            background-color: #c0392b;
            color: white;
            border: none;
            cursor: pointer;
            font-weight: bold;
        }

        .check-out-button:hover {
            background-color: #992d22;
        }
        .button-row {
            margin-top: 25px;
        }

        .save-button {
            padding: 11px 22px;
            background-color: #1f4e79;
            color: white;
            border: none;
            cursor: pointer;
            font-weight: bold;
        }

        .save-button:hover {
            background-color: #163a5c;
        }

        .cancel-button {
            padding: 11px 22px;
            background-color: #777;
            color: white;
            border: none;
            cursor: pointer;
            margin-left: 10px;
        }
        .message {
            display: block;
            margin-top: 15px;
            font-weight: bold;
        }
        .history-section {
            background-color: white;
            border: 1px solid #ddd;
            padding: 25px;
        }

        .history-section h2 {
            margin-top: 0;
            color: #1f4e79;
            margin-bottom: 20px;
        }
        .search-box {
            margin-bottom: 20px;
        }

        .search-input {
            width: 300px;
            padding: 10px;
            border: 1px solid #ccc;
            font-size: 14px;
        }

        .search-button {
            padding: 10px 18px;
            background-color: #1f4e79;
            color: white;
            border: none;
            cursor: pointer;
            margin-left: 5px;
        }
        .grid {
            width: 100%;
            border-collapse: collapse;
            margin-top: 15px;
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
        .status-present {
            color: #2e7d32;
            font-weight: bold;
        }

        .status-halfday {
            color: #e67e22;
            font-weight: bold;
        }

        .status-absent {
            color: #c0392b;
            font-weight: bold;
        }

    </style>

</asp:Content>


<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    Runat="Server">

    <div class="attendance-container">

        <div class="attendance-header">

            <h1>
                Attendance Management
            </h1>

        </div>
        <div class="attendance-form">

            <h2>
                Mark Attendance
            </h2>

            <div class="form-row">

                <asp:Label ID="lblEmployee"
                    runat="server"
                    Text="Employee">
                </asp:Label>

                <asp:DropDownList ID="ddlEmployee"
                    runat="server"
                    CssClass="form-control">

                    <asp:ListItem
                        Text="Select Employee"
                        Value="">
                    </asp:ListItem>

                </asp:DropDownList>

            </div>
            <div class="form-row">

                <asp:Label ID="lblAttendanceDate"
                    runat="server"
                    Text="Attendance Date">
                </asp:Label>

                <asp:TextBox ID="txtAttendanceDate"
                    runat="server"
                    CssClass="form-control">
                </asp:TextBox>

            </div>
            <div class="form-row">

                <asp:Label ID="lblCheckIn"
                    runat="server"
                    Text="Check In">
                </asp:Label>

                <asp:TextBox ID="txtCheckIn"
                    runat="server"
                    CssClass="form-control"
                    ReadOnly="true">
                </asp:TextBox>

            </div>
            <div class="form-row">

                <asp:Label ID="lblCheckOut"
                    runat="server"
                    Text="Check Out">
                </asp:Label>

                <asp:TextBox ID="txtCheckOut"
                    runat="server"
                    CssClass="form-control"
                    ReadOnly="true">
                </asp:TextBox>

            </div>

            <div class="check-button-row">

                <asp:Button ID="btnCheckIn"
                    runat="server"
                    Text="Check In"
                    CssClass="check-in-button"
                    OnClick="btnCheckIn_Click" />

                <asp:Button ID="btnCheckOut"
                    runat="server"
                    Text="Check Out"
                    CssClass="check-out-button"
                    OnClick="btnCheckOut_Click" />

            </div>

            <div class="form-row">

                <asp:Label ID="lblWorkingHours"
                    runat="server"
                    Text="Working Hours">
                </asp:Label>

                <asp:TextBox ID="txtWorkingHours"
                    runat="server"
                    CssClass="form-control"
                    ReadOnly="true">
                </asp:TextBox>

            </div>

            <div class="form-row">

                <asp:Label ID="lblStatus"
                    runat="server"
                    Text="Status">
                </asp:Label>

                <asp:TextBox ID="txtStatus"
                    runat="server"
                    CssClass="form-control"
                    ReadOnly="true">
                </asp:TextBox>

            </div>


            <div class="button-row">

                <asp:Button ID="btnSave"
                    runat="server"
                    Text="Save Attendance"
                    CssClass="save-button"
                    OnClick="btnSave_Click" />

                <asp:Button ID="btnCancel"
                    runat="server"
                    Text="Clear"
                    CssClass="cancel-button"
                    OnClick="btnCancel_Click" />

            </div>

            <asp:Label ID="lblMessage"
                runat="server"
                CssClass="message">
            </asp:Label>


        </div>

        <div class="history-section">

            <h2>
                Attendance History
            </h2>

            <div class="search-box">

                <asp:TextBox ID="txtSearch"
                    runat="server"
                    CssClass="search-input"
                    placeholder="Search employee">
                </asp:TextBox>

                <asp:Button ID="btnSearch"
                    runat="server"
                    Text="Search"
                    CssClass="search-button"
                    OnClick="btnSearch_Click" />

            </div>

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

        </div>


    </div>

</asp:Content>