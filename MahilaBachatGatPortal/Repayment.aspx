<%@ Page Title="Loan Repayment" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeFile="Repayment.aspx.cs" Inherits="LoanRepayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">

        <h2>Loan Repayment / EMI Management</h2>
        <hr />

        <asp:Label ID="lblMessage"
            runat="server"
            CssClass="alert"
            Visible="false">
        </asp:Label>


        <!-- =====================================================
             LOAN SELECTION
        ====================================================== -->

        <div class="panel panel-primary">

            <div class="panel-heading">

                <h4 class="panel-title">
                    Select Distributed Loan
                </h4>

            </div>

            <div class="panel-body">

                <div class="row">

                    <div class="col-md-8">

                        <div class="form-group">

                            <label>Loan</label>

                            <asp:DropDownList ID="ddlLoan"
                                runat="server"
                                CssClass="form-control"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="ddlLoan_SelectedIndexChanged">
                            </asp:DropDownList>

                        </div>

                    </div>

                </div>

            </div>

        </div>


        <!-- =====================================================
             LOAN INFORMATION
        ====================================================== -->

        <asp:Panel ID="pnlLoanInfo"
            runat="server"
            Visible="false">

            <div class="panel panel-info">

                <div class="panel-heading">

                    <h4 class="panel-title">
                        Loan Information
                    </h4>

                </div>

                <div class="panel-body">

                    <div class="row">

                        <div class="col-md-3">

                            <strong>Member</strong>

                            <br />

                            <asp:Label ID="lblMemberName"
                                runat="server">
                            </asp:Label>

                        </div>


                        <div class="col-md-3">

                            <strong>Loan Amount</strong>

                            <br />

                            ₹
                            <asp:Label ID="lblLoanAmount"
                                runat="server">
                            </asp:Label>

                        </div>


                        <div class="col-md-3">

                            <strong>EMI Amount</strong>

                            <br />

                            ₹
                            <asp:Label ID="lblEMIAmount"
                                runat="server">
                            </asp:Label>

                        </div>


                        <div class="col-md-3">

                            <strong>Outstanding Amount</strong>

                            <br />

                            <span style="font-size:18px;">

                                ₹
                                <asp:Label ID="lblOutstandingAmount"
                                    runat="server">
                                </asp:Label>

                            </span>

                        </div>

                    </div>

                </div>

            </div>

        </asp:Panel>


        <!-- =====================================================
             REPAYMENT ENTRY
        ====================================================== -->

        <asp:Panel ID="pnlRepayment"
            runat="server"
            Visible="false">

            <div class="panel panel-success">

                <div class="panel-heading">

                    <h4 class="panel-title">
                        EMI / Repayment Entry
                    </h4>

                </div>

                <div class="panel-body">


                    <div class="row">

                        <!-- Installment -->

                        <div class="col-md-4">

                            <div class="form-group">

                                <label>Installment No.</label>

                                <asp:TextBox ID="txtInstallmentNo"
                                    runat="server"
                                    CssClass="form-control">
                                </asp:TextBox>

                            </div>

                        </div>


                        <!-- Due Date -->

                        <div class="col-md-4">

                            <div class="form-group">

                                <label>Due Date</label>

                                <asp:TextBox ID="txtDueDate"
                                    runat="server"
                                    CssClass="form-control"
                                    TextMode="SingleLine">
                                </asp:TextBox>

                            </div>

                        </div>


                        <!-- Payment Date -->

                        <div class="col-md-4">

                            <div class="form-group">

                                <label>Payment Date</label>

                                <asp:TextBox ID="txtPaymentDate"
                                    runat="server"
                                    CssClass="form-control"
                                    TextMode="SingleLine">
                                </asp:TextBox>

                            </div>

                        </div>

                    </div>


                    <div class="row">

                        <!-- EMI Amount -->

                        <div class="col-md-3">

                            <div class="form-group">

                                <label>EMI Amount</label>

                                <asp:TextBox ID="txtEMI"
                                    runat="server"
                                    CssClass="form-control">
                                </asp:TextBox>

                            </div>

                        </div>


                        <!-- Principal -->

                        <div class="col-md-3">

                            <div class="form-group">

                                <label>Principal Amount</label>

                                <asp:TextBox ID="txtPrincipal"
                                    runat="server"
                                    CssClass="form-control">
                                </asp:TextBox>

                            </div>

                        </div>


                        <!-- Interest -->

                        <div class="col-md-3">

                            <div class="form-group">

                                <label>Interest Amount</label>

                                <asp:TextBox ID="txtInterest"
                                    runat="server"
                                    CssClass="form-control">
                                </asp:TextBox>

                            </div>

                        </div>


                        <!-- Paid -->

                        <div class="col-md-3">

                            <div class="form-group">

                                <label>Paid Amount</label>

                                <asp:TextBox ID="txtPaidAmount"
                                    runat="server"
                                    CssClass="form-control">
                                </asp:TextBox>

                            </div>

                        </div>

                    </div>


                    <div class="row">

                        <!-- Payment Mode -->

                        <div class="col-md-4">

                            <div class="form-group">

                                <label>Payment Mode</label>

                                <asp:DropDownList ID="ddlPaymentMode"
                                    runat="server"
                                    CssClass="form-control">

                                    <asp:ListItem
                                        Text="Cash"
                                        Value="Cash">
                                    </asp:ListItem>

                                    <asp:ListItem
                                        Text="UPI"
                                        Value="UPI">
                                    </asp:ListItem>

                                    <asp:ListItem
                                        Text="Bank"
                                        Value="Bank">
                                    </asp:ListItem>

                                    <asp:ListItem
                                        Text="Cheque"
                                        Value="Cheque">
                                    </asp:ListItem>

                                </asp:DropDownList>

                            </div>

                        </div>


                        <!-- Receipt -->

                        <div class="col-md-4">

                            <div class="form-group">

                                <label>Receipt Number</label>

                                <asp:TextBox ID="txtReceiptNumber"
                                    runat="server"
                                    CssClass="form-control">
                                </asp:TextBox>

                            </div>

                        </div>


                        <!-- Status -->

                        <div class="col-md-4">

                            <div class="form-group">

                                <label>Status</label>

                                <asp:DropDownList ID="ddlStatus"
                                    runat="server"
                                    CssClass="form-control">

                                    <asp:ListItem
                                        Text="Paid"
                                        Value="Paid">
                                    </asp:ListItem>

                                    <asp:ListItem
                                        Text="Partial"
                                        Value="Partial">
                                    </asp:ListItem>

                                    <asp:ListItem
                                        Text="Pending"
                                        Value="Pending">
                                    </asp:ListItem>

                                    <asp:ListItem
                                        Text="Overdue"
                                        Value="Overdue">
                                    </asp:ListItem>

                                </asp:DropDownList>

                            </div>

                        </div>

                    </div>


                    <!-- Remarks -->

                    <div class="form-group">

                        <label>Remarks</label>

                        <asp:TextBox ID="txtRemarks"
                            runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="2">
                        </asp:TextBox>

                    </div>


                    <asp:Button ID="btnSave"
                        runat="server"
                        Text="Save Repayment"
                        CssClass="btn btn-success"
                        OnClick="btnSave_Click" />

                    <asp:Button ID="btnClear"
                        runat="server"
                        Text="Clear"
                        CssClass="btn btn-default"
                        CausesValidation="false"
                        OnClick="btnClear_Click" />

                </div>

            </div>

        </asp:Panel>


        <!-- =====================================================
             REPAYMENT HISTORY
        ====================================================== -->

        <div class="panel panel-default">

            <div class="panel-heading">

                <h4 class="panel-title">
                    Repayment History
                </h4>

            </div>

            <div class="panel-body">

                <div class="table-responsive">

                    <asp:GridView ID="gvRepayments"
                        runat="server"
                        AutoGenerateColumns="False"
                        CssClass="table table-bordered table-striped table-hover">

                        <Columns>

                            <asp:BoundField
                                DataField="EMIInstallmentNo"
                                HeaderText="Installment" />

                            <asp:BoundField
                                DataField="DueDate"
                                HeaderText="Due Date"
                                DataFormatString="{0:dd-MM-yyyy}" />

                            <asp:BoundField
                                DataField="PaymentDate"
                                HeaderText="Payment Date"
                                DataFormatString="{0:dd-MM-yyyy}" />

                            <asp:BoundField
                                DataField="EMIAmount"
                                HeaderText="EMI"
                                DataFormatString="₹ {0:N2}" />

                            <asp:BoundField
                                DataField="PrincipalAmount"
                                HeaderText="Principal"
                                DataFormatString="₹ {0:N2}" />

                            <asp:BoundField
                                DataField="InterestAmount"
                                HeaderText="Interest"
                                DataFormatString="₹ {0:N2}" />

                            <asp:BoundField
                                DataField="PaidAmount"
                                HeaderText="Paid"
                                DataFormatString="₹ {0:N2}" />

                            <asp:BoundField
                                DataField="PaymentMode"
                                HeaderText="Mode" />

                            <asp:BoundField
                                DataField="ReceiptNumber"
                                HeaderText="Receipt" />

                            <asp:BoundField
                                DataField="Status"
                                HeaderText="Status" />

                        </Columns>

                        <EmptyDataTemplate>

                            <div class="alert alert-info">
                                No repayment records found.
                            </div>

                        </EmptyDataTemplate>

                    </asp:GridView>

                </div>

            </div>

        </div>

    </div>

</asp:Content>