using System;
using System.Data;
using System.Data.SqlClient;

public partial class LoanRepayment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RoleHelper.RequirePresidentSecretary(this);
        if (!IsPostBack)
        {
            LoadLoans();

            txtPaymentDate.Text =
                DateTime.Today.ToString("yyyy-MM-dd");
        }
    }


    // =========================================================
    // LOAD DISTRIBUTED LOANS
    // =========================================================

    private void LoadLoans()
    {
        ddlLoan.Items.Clear();

        ddlLoan.Items.Add(
            new System.Web.UI.WebControls.ListItem(
                "-- Select Distributed Loan --",
                ""));


        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    l.LoanID,
                    m.MemberCode,
                    m.MemberName
                FROM Loans l
                INNER JOIN Members m
                    ON l.MemberID = m.MemberID
                WHERE l.Status = 'Distributed'
                ORDER BY l.LoanID DESC";


            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                con.Open();

                using (SqlDataReader dr =
                    cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ddlLoan.Items.Add(
                            new System.Web.UI.WebControls.ListItem(
                                "Loan #"
                                + dr["LoanID"].ToString()
                                + " - "
                                + dr["MemberCode"].ToString()
                                + " - "
                                + dr["MemberName"].ToString(),

                                dr["LoanID"].ToString()
                            ));
                    }
                }
            }
        }
    }


    // =========================================================
    // LOAN CHANGE
    // =========================================================

    protected void ddlLoan_SelectedIndexChanged(
        object sender,
        EventArgs e)
    {
        if (ddlLoan.SelectedValue == "")
        {
            pnlLoanInfo.Visible = false;
            pnlRepayment.Visible = false;

            return;
        }


        int loanID =
            Convert.ToInt32(
                ddlLoan.SelectedValue);


        LoadLoanInformation(loanID);

        LoadRepaymentHistory(loanID);

        PrepareNextInstallment(loanID);
    }


    // =========================================================
    // LOAD LOAN INFORMATION
    // =========================================================

    private void LoadLoanInformation(int loanID)
    {
        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    l.LoanID,
                    m.MemberName,
                    l.LoanAmount,
                    l.ApprovedAmount,
                    l.EMIAmount
                FROM Loans l
                INNER JOIN Members m
                    ON l.MemberID = m.MemberID
                WHERE l.LoanID = @LoanID";


            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue(
                    "@LoanID",
                    loanID);

                con.Open();

                using (SqlDataReader dr =
                    cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        lblMemberName.Text =
                            dr["MemberName"].ToString();


                        decimal loanAmount = 0;


                        if (dr["ApprovedAmount"] != DBNull.Value)
                        {
                            loanAmount =
                                Convert.ToDecimal(
                                    dr["ApprovedAmount"]);
                        }
                        else
                        {
                            loanAmount =
                                Convert.ToDecimal(
                                    dr["LoanAmount"]);
                        }


                        lblLoanAmount.Text =
                            loanAmount.ToString("N2");


                        lblEMIAmount.Text =
                            Convert.ToDecimal(
                                dr["EMIAmount"])
                                .ToString("N2");
                    }
                }
            }
        }


        decimal outstanding =
            CalculateOutstanding(loanID);


        lblOutstandingAmount.Text =
            outstanding.ToString("N2");


        pnlLoanInfo.Visible = true;

        pnlRepayment.Visible = true;
    }


    // =========================================================
    // CALCULATE OUTSTANDING
    // =========================================================

    private decimal CalculateOutstanding(int loanID)
    {
        decimal loanAmount = 0;

        decimal principalPaid = 0;


        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    ISNULL(
                        ApprovedAmount,
                        LoanAmount
                    ) AS LoanAmount
                FROM Loans
                WHERE LoanID = @LoanID";


            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue(
                    "@LoanID",
                    loanID);

                con.Open();

                object result =
                    cmd.ExecuteScalar();

                if (result != null &&
                    result != DBNull.Value)
                {
                    loanAmount =
                        Convert.ToDecimal(result);
                }
            }
        }


        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    ISNULL(
                        SUM(PrincipalAmount),
                        0
                    )
                FROM LoanRepayments
                WHERE LoanID = @LoanID";


            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue(
                    "@LoanID",
                    loanID);

                con.Open();

                principalPaid =
                    Convert.ToDecimal(
                        cmd.ExecuteScalar());
            }
        }


        decimal outstanding =
            loanAmount - principalPaid;


        if (outstanding < 0)
        {
            outstanding = 0;
        }


        return outstanding;
    }


    // =========================================================
    // PREPARE NEXT INSTALLMENT
    // =========================================================

    private void PrepareNextInstallment(int loanID)
    {
        int nextInstallment = 1;


        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    ISNULL(
                        MAX(EMIInstallmentNo),
                        0
                    ) + 1
                FROM LoanRepayments
                WHERE LoanID = @LoanID";


            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue(
                    "@LoanID",
                    loanID);

                con.Open();

                nextInstallment =
                    Convert.ToInt32(
                        cmd.ExecuteScalar());
            }
        }


        txtInstallmentNo.Text =
            nextInstallment.ToString();


        txtPaymentDate.Text =
            DateTime.Today.ToString("yyyy-MM-dd");


        DateTime dueDate =
            DateTime.Today;


        if (nextInstallment > 1)
        {
            dueDate =
                DateTime.Today.AddMonths(
                    nextInstallment - 1);
        }


        txtDueDate.Text =
            dueDate.ToString("yyyy-MM-dd");


        // Load EMI

        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                SELECT EMIAmount
                FROM Loans
                WHERE LoanID = @LoanID";


            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue(
                    "@LoanID",
                    loanID);

                con.Open();

                object result =
                    cmd.ExecuteScalar();

                if (result != null &&
                    result != DBNull.Value)
                {
                    txtEMI.Text =
                        Convert.ToDecimal(result)
                        .ToString("0.00");
                }
            }
        }


        txtPrincipal.Text = "";

        txtInterest.Text = "";

        txtPaidAmount.Text = "";

        txtReceiptNumber.Text = "";

        txtRemarks.Text = "";

        ddlStatus.SelectedValue =
            "Paid";
    }


    // =========================================================
    // SAVE REPAYMENT
    // =========================================================

    protected void btnSave_Click(
        object sender,
        EventArgs e)
    {
        lblMessage.Visible = false;


        if (ddlLoan.SelectedValue == "")
        {
            ShowMessage(
                "Please select a loan.",
                "alert alert-danger");

            return;
        }


        int loanID =
            Convert.ToInt32(
                ddlLoan.SelectedValue);


        int installmentNo;

        if (!int.TryParse(
            txtInstallmentNo.Text.Trim(),
            out installmentNo) ||
            installmentNo <= 0)
        {
            ShowMessage(
                "Please enter a valid installment number.",
                "alert alert-danger");

            return;
        }


        DateTime dueDate;

        if (!DateTime.TryParse(
            txtDueDate.Text.Trim(),
            out dueDate))
        {
            ShowMessage(
                "Please enter a valid due date.",
                "alert alert-danger");

            return;
        }


        DateTime paymentDate;

        if (!DateTime.TryParse(
            txtPaymentDate.Text.Trim(),
            out paymentDate))
        {
            ShowMessage(
                "Please enter a valid payment date.",
                "alert alert-danger");

            return;
        }


        decimal emiAmount;
        decimal principalAmount;
        decimal interestAmount;
        decimal paidAmount;


        if (!decimal.TryParse(
            txtEMI.Text.Trim(),
            out emiAmount) ||
            emiAmount <= 0)
        {
            ShowMessage(
                "Please enter a valid EMI amount.",
                "alert alert-danger");

            return;
        }


        if (!decimal.TryParse(
            txtPrincipal.Text.Trim(),
            out principalAmount) ||
            principalAmount < 0)
        {
            ShowMessage(
                "Please enter a valid principal amount.",
                "alert alert-danger");

            return;
        }


        if (!decimal.TryParse(
            txtInterest.Text.Trim(),
            out interestAmount) ||
            interestAmount < 0)
        {
            ShowMessage(
                "Please enter a valid interest amount.",
                "alert alert-danger");

            return;
        }


        if (!decimal.TryParse(
            txtPaidAmount.Text.Trim(),
            out paidAmount) ||
            paidAmount <= 0)
        {
            ShowMessage(
                "Please enter a valid paid amount.",
                "alert alert-danger");

            return;
        }


        if (principalAmount > paidAmount)
        {
            ShowMessage(
                "Principal amount cannot be greater than paid amount.",
                "alert alert-danger");

            return;
        }


        // Check outstanding

        decimal outstanding =
            CalculateOutstanding(loanID);


        if (principalAmount > outstanding)
        {
            ShowMessage(
                "Principal amount cannot be greater than outstanding amount.",
                "alert alert-danger");

            return;
        }


        // =====================================================
        // INSERT REPAYMENT
        // =====================================================

        int memberID = 0;


        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                SELECT MemberID
                FROM Loans
                WHERE LoanID = @LoanID";


            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue(
                    "@LoanID",
                    loanID);

                con.Open();

                object result =
                    cmd.ExecuteScalar();

                if (result == null)
                {
                    ShowMessage(
                        "Loan not found.",
                        "alert alert-danger");

                    return;
                }

                memberID =
                    Convert.ToInt32(result);
            }
        }


        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                INSERT INTO LoanRepayments
                (
                    LoanID,
                    MemberID,
                    EMIInstallmentNo,
                    DueDate,
                    PaymentDate,
                    EMIAmount,
                    PrincipalAmount,
                    InterestAmount,
                    PaidAmount,
                    PaymentMode,
                    ReceiptNumber,
                    Status,
                    Remarks,
                    CreatedDate
                )
                VALUES
                (
                    @LoanID,
                    @MemberID,
                    @InstallmentNo,
                    @DueDate,
                    @PaymentDate,
                    @EMIAmount,
                    @PrincipalAmount,
                    @InterestAmount,
                    @PaidAmount,
                    @PaymentMode,
                    @ReceiptNumber,
                    @Status,
                    @Remarks,
                    GETDATE()
                )";


            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue(
                    "@LoanID",
                    loanID);

                cmd.Parameters.AddWithValue(
                    "@MemberID",
                    memberID);

                cmd.Parameters.AddWithValue(
                    "@InstallmentNo",
                    installmentNo);

                cmd.Parameters.AddWithValue(
                    "@DueDate",
                    dueDate);

                cmd.Parameters.AddWithValue(
                    "@PaymentDate",
                    paymentDate);

                cmd.Parameters.AddWithValue(
                    "@EMIAmount",
                    emiAmount);

                cmd.Parameters.AddWithValue(
                    "@PrincipalAmount",
                    principalAmount);

                cmd.Parameters.AddWithValue(
                    "@InterestAmount",
                    interestAmount);

                cmd.Parameters.AddWithValue(
                    "@PaidAmount",
                    paidAmount);

                cmd.Parameters.AddWithValue(
                    "@PaymentMode",
                    ddlPaymentMode.SelectedValue);

                if (string.IsNullOrWhiteSpace(
                    txtReceiptNumber.Text))
                {
                    cmd.Parameters.AddWithValue(
                        "@ReceiptNumber",
                        DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue(
                        "@ReceiptNumber",
                        txtReceiptNumber.Text.Trim());
                }

                cmd.Parameters.AddWithValue(
                    "@Status",
                    ddlStatus.SelectedValue);

                cmd.Parameters.AddWithValue(
                    "@Remarks",
                    txtRemarks.Text.Trim());

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }


        // =====================================================
        // CHECK WHETHER LOAN IS COMPLETED
        // =====================================================

        decimal newOutstanding =
            CalculateOutstanding(loanID);


        if (newOutstanding <= 0)
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query = @"
                    UPDATE Loans
                    SET Status = 'Completed'
                    WHERE LoanID = @LoanID";


                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue(
                        "@LoanID",
                        loanID);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }


        ShowMessage(
            "Repayment saved successfully.",
            "alert alert-success");


        LoadLoanInformation(loanID);

        LoadRepaymentHistory(loanID);

        PrepareNextInstallment(loanID);
    }


    // =========================================================
    // REPAYMENT HISTORY
    // =========================================================

    private void LoadRepaymentHistory(int loanID)
    {
        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    EMIInstallmentNo,
                    DueDate,
                    PaymentDate,
                    EMIAmount,
                    PrincipalAmount,
                    InterestAmount,
                    PaidAmount,
                    PaymentMode,
                    ReceiptNumber,
                    Status
                FROM LoanRepayments
                WHERE LoanID = @LoanID
                ORDER BY EMIInstallmentNo DESC";


            using (SqlDataAdapter da =
                new SqlDataAdapter(query, con))
            {
                da.SelectCommand.Parameters.AddWithValue(
                    "@LoanID",
                    loanID);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                gvRepayments.DataSource =
                    dt;

                gvRepayments.DataBind();
            }
        }
    }


    // =========================================================
    // CLEAR
    // =========================================================

    protected void btnClear_Click(
        object sender,
        EventArgs e)
    {
        ClearForm();

        lblMessage.Visible = false;
    }


    private void ClearForm()
    {
        if (ddlLoan.SelectedValue != "")
        {
            int loanID =
                Convert.ToInt32(
                    ddlLoan.SelectedValue);

            PrepareNextInstallment(loanID);

            return;
        }


        pnlLoanInfo.Visible = false;

        pnlRepayment.Visible = false;

        txtInstallmentNo.Text = "";

        txtDueDate.Text = "";

        txtPaymentDate.Text =
            DateTime.Today.ToString("yyyy-MM-dd");

        txtEMI.Text = "";

        txtPrincipal.Text = "";

        txtInterest.Text = "";

        txtPaidAmount.Text = "";

        txtReceiptNumber.Text = "";

        txtRemarks.Text = "";
    }


    // =========================================================
    // MESSAGE
    // =========================================================

    private void ShowMessage(
        string message,
        string cssClass)
    {
        lblMessage.Text =
            message;

        lblMessage.CssClass =
            cssClass;

        lblMessage.Visible = true;
    }
}