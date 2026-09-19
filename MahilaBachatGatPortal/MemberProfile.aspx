<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="MemberProfile.aspx.cs" Inherits="MemberProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
.page-title { margin-top: 0; font-weight: bold; }
.page-subtitle { color: #777; margin-bottom: 25px; }
.selection-panel, .profile-panel, .section-panel { background: #ffffff; padding: 20px; margin-bottom: 25px; border-radius: 8px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); }
.section-header { background: #337ab7; color: white; padding: 12px 15px; margin: -20px -20px 20px -20px; border-radius: 8px 8px 0 0; font-size: 18px; font-weight: bold; }
.profile-label { font-weight: bold; color: #555; }
.profile-value { margin-bottom: 15px; color: #222; }
.summary-box { border: 1px solid #ddd; padding: 15px; border-radius: 6px; text-align: center; margin-bottom: 15px; }
.summary-title { font-size: 14px; color: #777; }
.summary-value { font-size: 22px; font-weight: bold; margin-top: 5px; }
.table th { background: #f5f5f5; }
.message { margin-top: 15px; }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2 class="page-title">My Profile</h2>
<p class="page-subtitle">Select your Bachat Gat and name to view your profile details.</p>

<div class="selection-panel">
    <div class="section-header"><span class="glyphicon glyphicon-search"></span> Select Profile</div>

    <div class="row">
        <div class="col-md-5">
            <div class="form-group">
                <label>Bachat Gat Name</label>
                <asp:DropDownList ID="ddlBachatGat" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBachatGat_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>

        <div class="col-md-5">
            <div class="form-group">
                <label>My Name</label>
                <asp:DropDownList ID="ddlMember" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>

        <div class="col-md-2">
            <div class="form-group">
                <label>&nbsp;</label>
                <asp:Button ID="btnViewProfile" runat="server" Text="View Profile" CssClass="btn btn-primary btn-block" OnClick="btnViewProfile_Click" />
            </div>
        </div>
    </div>

    <div class="message">
        <asp:Label ID="lblMessage" runat="server" Font-Bold="true"></asp:Label>
    </div>
</div>

<asp:Panel ID="pnlProfile" runat="server" Visible="false">

    <div class="profile-panel">
        <div class="section-header"><span class="glyphicon glyphicon-user"></span> Member Profile</div>

        <div class="row">
            <div class="col-md-4">
                <div class="profile-label">Member Code</div>
                <div class="profile-value"><asp:Label ID="lblMemberCode" runat="server"></asp:Label></div>
            </div>
            <div class="col-md-4">
                <div class="profile-label">Member Name</div>
                <div class="profile-value"><asp:Label ID="lblMemberName" runat="server"></asp:Label></div>
            </div>
            <div class="col-md-4">
                <div class="profile-label">Father / Husband Name</div>
                <div class="profile-value"><asp:Label ID="lblFatherHusband" runat="server"></asp:Label></div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-4">
                <div class="profile-label">Gender</div>
                <div class="profile-value"><asp:Label ID="lblGender" runat="server"></asp:Label></div>
            </div>
            <div class="col-md-4">
                <div class="profile-label">Date of Birth</div>
                <div class="profile-value"><asp:Label ID="lblDOB" runat="server"></asp:Label></div>
            </div>
            <div class="col-md-4">
                <div class="profile-label">Mobile</div>
                <div class="profile-value"><asp:Label ID="lblMobile" runat="server"></asp:Label></div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-4">
                <div class="profile-label">Email</div>
                <div class="profile-value"><asp:Label ID="lblEmail" runat="server"></asp:Label></div>
            </div>
            <div class="col-md-4">
                <div class="profile-label">Bachat Gat</div>
                <div class="profile-value"><asp:Label ID="lblGatName" runat="server"></asp:Label></div>
            </div>
            <div class="col-md-4">
                <div class="profile-label">Join Date</div>
                <div class="profile-value"><asp:Label ID="lblJoinDate" runat="server"></asp:Label></div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-4">
                <div class="profile-label">Occupation</div>
                <div class="profile-value"><asp:Label ID="lblOccupation" runat="server"></asp:Label></div>
            </div>
            <div class="col-md-4">
                <div class="profile-label">Village</div>
                <div class="profile-value"><asp:Label ID="lblVillage" runat="server"></asp:Label></div>
            </div>
            <div class="col-md-4">
                <div class="profile-label">Taluka</div>
                <div class="profile-value"><asp:Label ID="lblTaluka" runat="server"></asp:Label></div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-4">
                <div class="profile-label">District</div>
                <div class="profile-value"><asp:Label ID="lblDistrict" runat="server"></asp:Label></div>
            </div>
            <div class="col-md-4">
                <div class="profile-label">Bank Name</div>
                <div class="profile-value"><asp:Label ID="lblBankName" runat="server"></asp:Label></div>
            </div>
            <div class="col-md-4">
                <div class="profile-label">Bank Account Number</div>
                <div class="profile-value"><asp:Label ID="lblAccountNumber" runat="server"></asp:Label></div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-4">
                <div class="profile-label">IFSC Code</div>
                <div class="profile-value"><asp:Label ID="lblIFSC" runat="server"></asp:Label></div>
            </div>
            <div class="col-md-4">
                <div class="profile-label">Status</div>
                <div class="profile-value"><asp:Label ID="lblStatus" runat="server"></asp:Label></div>
            </div>
        </div>
    </div>

    <div class="section-panel">
        <div class="section-header"><span class="glyphicon glyphicon-credit-card"></span> My Savings</div>

        <div class="row">
            <div class="col-md-4">
                <div class="summary-box">
                    <div class="summary-title">Total Savings</div>
                    <div class="summary-value">₹ <asp:Label ID="lblTotalSavings" runat="server" Text="0.00"></asp:Label></div>
                </div>
            </div>
        </div>

        <div class="table-responsive">
            <asp:GridView ID="gvSavings" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" EmptyDataText="No savings records found.">
                <Columns>
                    <asp:BoundField DataField="SavingMonth" HeaderText="Saving Month" />
                    <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="₹ {0:N2}" />
                    <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode" />
                    <asp:BoundField DataField="ReceiptNumber" HeaderText="Receipt No." />
                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="section-panel">
        <div class="section-header"><span class="glyphicon glyphicon-usd"></span> My Loans</div>

        <div class="table-responsive">
            <asp:GridView ID="gvLoans" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" EmptyDataText="No loan records found.">
                <Columns>
                    <asp:BoundField DataField="LoanID" HeaderText="Loan ID" />
                    <asp:BoundField DataField="ApplicationDate" HeaderText="Application Date" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="LoanAmount" HeaderText="Loan Amount" DataFormatString="₹ {0:N2}" />
                    <asp:BoundField DataField="ApprovedAmount" HeaderText="Approved Amount" DataFormatString="₹ {0:N2}" />
                    <asp:BoundField DataField="InterestRate" HeaderText="Interest Rate" />
                    <asp:BoundField DataField="LoanTermMonths" HeaderText="Term (Months)" />
                    <asp:BoundField DataField="EMIAmount" HeaderText="EMI Amount" DataFormatString="₹ {0:N2}" />
                    <asp:BoundField DataField="LoanPurpose" HeaderText="Purpose" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:BoundField DataField="ApprovalDate" HeaderText="Approval Date" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="DistributionDate" HeaderText="Distribution Date" DataFormatString="{0:dd-MM-yyyy}" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="section-panel">
        <div class="section-header"><span class="glyphicon glyphicon-refresh"></span> My Repayments</div>

        <div class="table-responsive">
            <asp:GridView ID="gvRepayments" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" EmptyDataText="No repayment records found.">
                <Columns>
                    <asp:BoundField DataField="LoanID" HeaderText="Loan ID" />
                    <asp:BoundField DataField="EMIInstallmentNo" HeaderText="Installment No." />
                    <asp:BoundField DataField="DueDate" HeaderText="Due Date" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="EMIAmount" HeaderText="EMI Amount" DataFormatString="₹ {0:N2}" />
                    <asp:BoundField DataField="PrincipalAmount" HeaderText="Principal" DataFormatString="₹ {0:N2}" />
                    <asp:BoundField DataField="InterestAmount" HeaderText="Interest" DataFormatString="₹ {0:N2}" />
                    <asp:BoundField DataField="PaidAmount" HeaderText="Paid Amount" DataFormatString="₹ {0:N2}" />
                    <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode" />
                    <asp:BoundField DataField="ReceiptNumber" HeaderText="Receipt No." />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Panel>

</asp:Content>