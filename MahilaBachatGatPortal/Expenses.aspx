<%@ Page Title="Expense Management"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeFile="Expenses.aspx.cs"
    Inherits="Expenses" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">

    <style>
        .summary-card {
            padding: 20px;
            border-radius: 6px;
            margin-bottom: 20px;
            color: white;
        }

        .summary-expense {
            background: #d9534f;
        }

        .summary-records {
            background: #337ab7;
        }

        .summary-month {
            background: #f0ad4e;
        }

        .form-section {
            background: #ffffff;
            padding: 20px;
            border-radius: 6px;
            margin-bottom: 20px;
            box-shadow: 0 1px 5px rgba(0,0,0,0.1);
        }

        .table-section {
            background: #ffffff;
            padding: 20px;
            border-radius: 6px;
            box-shadow: 0 1px 5px rgba(0,0,0,0.1);
        }

        .amount {
            font-size: 24px;
            font-weight: bold;
        }
    </style>

</asp:Content>


<asp:Content ID="Content2"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>
        <span class="glyphicon glyphicon-minus"></span>
        Expense Management
    </h2>

    <hr />


    <!-- SUMMARY -->

    <div class="row">

        <div class="col-md-4">

            <div class="summary-card summary-expense">

                <h4>Total Expenses</h4>

                <div class="amount">

                    ₹ <asp:Label ID="lblTotalExpenses"
                        runat="server"
                        Text="0.00">
                    </asp:Label>

                </div>

            </div>

        </div>


        <div class="col-md-4">

            <div class="summary-card summary-records">

                <h4>Total Records</h4>

                <div class="amount">

                    <asp:Label ID="lblTotalRecords"
                        runat="server"
                        Text="0">
                    </asp:Label>

                </div>

            </div>

        </div>


        <div class="col-md-4">

            <div class="summary-card summary-month">

                <h4>Current Month Expenses</h4>

                <div class="amount">

                    ₹ <asp:Label ID="lblCurrentMonthExpenses"
                        runat="server"
                        Text="0.00">
                    </asp:Label>

                </div>

            </div>

        </div>

    </div>


    <!-- MESSAGE -->

    <asp:Label ID="lblMessage"
        runat="server"
        CssClass="alert"
        Visible="false">
    </asp:Label>


    <!-- FORM -->

    <div class="form-section">

        <h3>Expense Entry</h3>

        <hr />

        <asp:HiddenField ID="hfExpenseID"
            runat="server" />


        <div class="row">

            <!-- Bachat Gat -->

            <div class="col-md-6">

                <div class="form-group">

                    <label>Bachat Gat</label>

                    <asp:DropDownList ID="ddlBachatGat"
                        runat="server"
                        CssClass="form-control">

                    </asp:DropDownList>

                </div>

            </div>


            <!-- Expense Date -->

            <div class="col-md-6">

                <div class="form-group">

                    <label>Expense Date</label>

                    <asp:TextBox ID="txtExpenseDate"
                        runat="server"
                        CssClass="form-control"
                        TextMode="SingleLine">
                    </asp:TextBox>

                </div>

            </div>

        </div>


        <div class="row">

            <!-- Expense Type -->

            <div class="col-md-6">

                <div class="form-group">

                    <label>Expense Type</label>

                    <asp:DropDownList ID="ddlExpenseType"
                        runat="server"
                        CssClass="form-control">

                        <asp:ListItem Text="-- Select Expense Type --"
                            Value="">
                        </asp:ListItem>

                        <asp:ListItem Text="Office Expenses"
                            Value="Office Expenses">
                        </asp:ListItem>

                        <asp:ListItem Text="Meeting Expenses"
                            Value="Meeting Expenses">
                        </asp:ListItem>

                        <asp:ListItem Text="Travel Expenses"
                            Value="Travel Expenses">
                        </asp:ListItem>

                        <asp:ListItem Text="Raw Material"
                            Value="Raw Material">
                        </asp:ListItem>

                        <asp:ListItem Text="Electricity"
                            Value="Electricity">
                        </asp:ListItem>

                        <asp:ListItem Text="Maintenance"
                            Value="Maintenance">
                        </asp:ListItem>

                        <asp:ListItem Text="Bank Charges"
                            Value="Bank Charges">
                        </asp:ListItem>

                        <asp:ListItem Text="Other"
                            Value="Other">
                        </asp:ListItem>

                    </asp:DropDownList>

                </div>

            </div>


            <!-- Amount -->

            <div class="col-md-6">

                <div class="form-group">

                    <label>Amount</label>

                    <asp:TextBox ID="txtAmount"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>

                </div>

            </div>

        </div>


        <div class="row">

            <!-- Description -->

            <div class="col-md-6">

                <div class="form-group">

                    <label>Description</label>

                    <asp:TextBox ID="txtDescription"
                        runat="server"
                        CssClass="form-control"
                        TextMode="MultiLine"
                        Rows="3">
                    </asp:TextBox>

                </div>

            </div>


            <!-- Paid To -->

            <div class="col-md-6">

                <div class="form-group">

                    <label>Paid To</label>

                    <asp:TextBox ID="txtPaidTo"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>

                </div>

            </div>

        </div>


        <div class="row">

            <!-- Payment Mode -->

            <div class="col-md-6">

                <div class="form-group">

                    <label>Payment Mode</label>

                    <asp:DropDownList ID="ddlPaymentMode"
                        runat="server"
                        CssClass="form-control">

                        <asp:ListItem Text="-- Select Payment Mode --"
                            Value="">
                        </asp:ListItem>

                        <asp:ListItem Text="Cash"
                            Value="Cash">
                        </asp:ListItem>

                        <asp:ListItem Text="UPI"
                            Value="UPI">
                        </asp:ListItem>

                        <asp:ListItem Text="Bank"
                            Value="Bank">
                        </asp:ListItem>

                        <asp:ListItem Text="Cheque"
                            Value="Cheque">
                        </asp:ListItem>

                    </asp:DropDownList>

                </div>

            </div>


            <!-- Receipt Number -->

            <div class="col-md-6">

                <div class="form-group">

                    <label>Receipt Number</label>

                    <asp:TextBox ID="txtReceiptNumber"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>

                </div>

            </div>

        </div>


        <!-- REMARKS -->

        <div class="form-group">

            <label>Remarks</label>

            <asp:TextBox ID="txtRemarks"
                runat="server"
                CssClass="form-control"
                TextMode="MultiLine"
                Rows="2">
            </asp:TextBox>

        </div>


        <!-- BUTTONS -->

        <div class="form-group">

            <asp:Button ID="btnSave"
                runat="server"
                Text="Save Expense"
                CssClass="btn btn-danger"
                OnClick="btnSave_Click" />

            &nbsp;

            <asp:Button ID="btnClear"
                runat="server"
                Text="Clear"
                CssClass="btn btn-default"
                OnClick="btnClear_Click" />

        </div>

    </div>


    <!-- SEARCH -->

    <div class="form-section">

        <h3>Search Expenses</h3>

        <div class="row">

            <div class="col-md-8">

                <asp:TextBox ID="txtSearch"
                    runat="server"
                    CssClass="form-control"
                    placeholder="Search by expense type, paid to or receipt number">
                </asp:TextBox>

            </div>


            <div class="col-md-2">

                <asp:Button ID="btnSearch"
                    runat="server"
                    Text="Search"
                    CssClass="btn btn-primary btn-block"
                    OnClick="btnSearch_Click" />

            </div>


            <div class="col-md-2">

                <asp:Button ID="btnShowAll"
                    runat="server"
                    Text="Show All"
                    CssClass="btn btn-default btn-block"
                    OnClick="btnShowAll_Click" />

            </div>

        </div>

    </div>


    <!-- GRID -->

    <div class="table-section">

        <h3>Expense History</h3>

        <hr />

        <div class="table-responsive">

            <asp:GridView ID="gvExpenses"
                runat="server"
                AutoGenerateColumns="False"
                CssClass="table table-bordered table-striped table-hover"
                DataKeyNames="ExpenseID"
                OnRowCommand="gvExpenses_RowCommand">

                <Columns>

                    <asp:BoundField
                        DataField="ExpenseID"
                        HeaderText="ID" />

                    <asp:BoundField
                        DataField="GatName"
                        HeaderText="Bachat Gat" />

                    <asp:BoundField
                        DataField="ExpenseDate"
                        HeaderText="Date"
                        DataFormatString="{0:dd-MM-yyyy}" />

                    <asp:BoundField
                        DataField="ExpenseType"
                        HeaderText="Expense Type" />

                    <asp:BoundField
                        DataField="Amount"
                        HeaderText="Amount"
                        DataFormatString="₹ {0:N2}" />

                    <asp:BoundField
                        DataField="PaidTo"
                        HeaderText="Paid To" />

                    <asp:BoundField
                        DataField="PaymentMode"
                        HeaderText="Payment Mode" />

                    <asp:BoundField
                        DataField="ReceiptNumber"
                        HeaderText="Receipt No." />

                    <asp:TemplateField HeaderText="Action">

                        <ItemTemplate>

                            <asp:LinkButton ID="btnDelete"
                                runat="server"
                                CommandName="DeleteExpense"
                                CommandArgument='<%# Eval("ExpenseID") %>'
                                CssClass="btn btn-danger btn-xs"
                                OnClientClick="return confirm('Are you sure you want to delete this expense entry?');">

                                <span class="glyphicon glyphicon-trash"></span>
                                Delete

                            </asp:LinkButton>

                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>

            </asp:GridView>

        </div>

    </div>

</asp:Content>