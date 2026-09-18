<%@ Page Title="Leave Management"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeFile="Leave.aspx.cs"
    Inherits="Leave" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    Runat="Server">

    <style>

        .leave-container {
            background-color: white;
            padding: 25px;
            border: 1px solid #ddd;
        }
        .leave-header {
            margin-bottom: 25px;
        }

        .leave-header h1 {
            margin: 0;
            color: #1f4e79;
        }
        .leave-form {
            background-color: #f9f9f9;
            border: 1px solid #ddd;
            padding: 25px;
            margin-bottom: 30px;
        }

        .leave-form h2 {
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
        .button-row {
            margin-top: 25px;
        }

        .apply-button {
            padding: 11px 22px;
            background-color: #1f4e79;
            color: white;
            border: none;
            cursor: pointer;
            font-weight: bold;
        }

        .apply-button:hover {
            background-color: #163a5c;
        }

        .clear-button {
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

        .action-button {
            padding: 6px 10px;
            margin-right: 4px;
            border: none;
            cursor: pointer;
            color: white;
            font-weight: bold;
        }

        .view-button {
            background-color: #1f4e79;
        }

        .cancel-button {
            background-color: #c0392b;
        }

        .approve-button {
            background-color: #2e7d32;
        }

        .reject-button {
            background-color: #e67e22;
        }
        .details-panel {
            margin-top: 25px;
            padding: 25px;
            background-color: #f9f9f9;
            border: 1px solid #ddd;
        }

        .details-panel h2 {
            margin-top: 0;
            color: #1f4e79;
            margin-bottom: 20px;
        }

        .detail-row {
            margin-bottom: 12px;
        }

        .detail-label {
            display: inline-block;
            width: 150px;
            font-weight: bold;
        }

        .close-button {
            padding: 10px 20px;
            background-color: #777;
            color: white;
            border: none;
            cursor: pointer;
            margin-top: 10px;
        }

        .status {
            font-weight: bold;
        }

    </style>

</asp:Content>


<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    Runat="Server">

    <div class="leave-container">


        <div class="leave-header">

            <h1>
                Leave Management
            </h1>

        </div>


        <div class="leave-form">

            <h2>
                Apply Leave
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

                <asp:Label ID="lblLeaveType"
                    runat="server"
                    Text="Leave Type">
                </asp:Label>

                <asp:DropDownList ID="ddlLeaveType"
                    runat="server"
                    CssClass="form-control">

                    <asp:ListItem
                        Text="Select Leave Type"
                        Value="">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Casual Leave"
                        Value="Casual Leave">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Sick Leave"
                        Value="Sick Leave">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Earned Leave"
                        Value="Earned Leave">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Emergency Leave"
                        Value="Emergency Leave">
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

                <asp:Label ID="lblLeaveDays"
                    runat="server"
                    Text="Leave Days">
                </asp:Label>

                <asp:TextBox ID="txtLeaveDays"
                    runat="server"
                    CssClass="form-control"
                    ReadOnly="true">
                </asp:TextBox>

            </div>

            <div class="form-row">

                <asp:Label ID="lblReason"
                    runat="server"
                    Text="Reason">
                </asp:Label>

                <asp:TextBox ID="txtReason"
                    runat="server"
                    CssClass="form-control"
                    TextMode="MultiLine"
                    Rows="4">
                </asp:TextBox>

            </div>

            <div class="button-row">

                <asp:Button ID="btnApply"
                    runat="server"
                    Text="Apply Leave"
                    CssClass="apply-button"
                    OnClick="btnApply_Click" />

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


        <div class="history-section">

            <h2>
                Leave Requests
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
            <asp:GridView ID="gvLeaves"
                runat="server"
                AutoGenerateColumns="False"
                CssClass="grid"
                OnRowCommand="gvLeaves_RowCommand">

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
                        HeaderText="From" />

                    <asp:BoundField
                        DataField="ToDate"
                        HeaderText="To" />

                    <asp:BoundField
                        DataField="LeaveDays"
                        HeaderText="Days" />

                    <asp:BoundField
                        DataField="Status"
                        HeaderText="Status" />

                    <asp:TemplateField
                        HeaderText="Actions">

                        <ItemTemplate>

                            <asp:Button ID="btnView"
                                runat="server"
                                Text="View"
                                CommandName="ViewLeave"
                                CommandArgument='<%# Eval("LeaveID") %>'
                                CssClass="action-button view-button" />


                            <asp:Button ID="btnCancelLeave"
                                runat="server"
                                Text="Cancel"
                                CommandName="CancelLeave"
                                CommandArgument='<%# Eval("LeaveID") %>'
                                CssClass="action-button cancel-button" />


                            <asp:Button ID="btnApprove"
                                runat="server"
                                Text="Approve"
                                CommandName="ApproveLeave"
                                CommandArgument='<%# Eval("LeaveID") %>'
                                CssClass="action-button approve-button" />


                            <asp:Button ID="btnReject"
                                runat="server"
                                Text="Reject"
                                CommandName="RejectLeave"
                                CommandArgument='<%# Eval("LeaveID") %>'
                                CssClass="action-button reject-button" />

                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>

            </asp:GridView>


        </div>

        <asp:Panel ID="pnlDetails"
            runat="server"
            CssClass="details-panel"
            Visible="false">

            <h2>
                Leave Details
            </h2>


            <div class="detail-row">

                <span class="detail-label">
                    Leave ID:
                </span>

                <asp:Label ID="lblDetailsID"
                    runat="server">
                </asp:Label>

            </div>


            <div class="detail-row">

                <span class="detail-label">
                    Employee:
                </span>

                <asp:Label ID="lblDetailsEmployee"
                    runat="server">
                </asp:Label>

            </div>


            <div class="detail-row">

                <span class="detail-label">
                    Leave Type:
                </span>

                <asp:Label ID="lblDetailsLeaveType"
                    runat="server">
                </asp:Label>

            </div>


            <div class="detail-row">

                <span class="detail-label">
                    From Date:
                </span>

                <asp:Label ID="lblDetailsFromDate"
                    runat="server">
                </asp:Label>

            </div>


            <div class="detail-row">

                <span class="detail-label">
                    To Date:
                </span>

                <asp:Label ID="lblDetailsToDate"
                    runat="server">
                </asp:Label>

            </div>


            <div class="detail-row">

                <span class="detail-label">
                    Leave Days:
                </span>

                <asp:Label ID="lblDetailsDays"
                    runat="server">
                </asp:Label>

            </div>


            <div class="detail-row">

                <span class="detail-label">
                    Reason:
                </span>

                <asp:Label ID="lblDetailsReason"
                    runat="server">
                </asp:Label>

            </div>


            <div class="detail-row">

                <span class="detail-label">
                    Status:
                </span>

                <asp:Label ID="lblDetailsStatus"
                    runat="server">
                </asp:Label>

            </div>


            <asp:Button ID="btnCloseDetails"
                runat="server"
                Text="Close"
                CssClass="close-button"
                OnClick="btnCloseDetails_Click" />

        </asp:Panel>


    </div>

</asp:Content>