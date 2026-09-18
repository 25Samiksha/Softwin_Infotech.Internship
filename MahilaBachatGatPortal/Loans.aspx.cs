using System;
using System.Data;
using System.Data.SqlClient;

public partial class Loans : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RoleHelper.RequirePresidentSecretary(this);
        if (!IsPostBack)
        {
            LoadBachatGats();

            txtApplicationDate.Text =
                DateTime.Today.ToString("yyyy-MM-dd");

            txtDistributionDate.Text =
                DateTime.Today.ToString("yyyy-MM-dd");

            LoadLoans();

            LoadSummary();
        }
    }


    // =========================================================
    // LOAD BACHAT GATS
    // =========================================================

    private void LoadBachatGats()
    {
        ddlBachatGat.Items.Clear();

        ddlBachatGat.Items.Add(
            new System.Web.UI.WebControls.ListItem(
                "-- Select Bachat Gat --",
                ""));

        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                SELECT BachatGatID, GatName
                FROM BachatGat
                WHERE Status = 'Active'
                ORDER BY GatName";

            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                con.Open();

                using (SqlDataReader dr =
                    cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ddlBachatGat.Items.Add(
                            new System.Web.UI.WebControls.ListItem(
                                dr["GatName"].ToString(),
                                dr["BachatGatID"].ToString()));
                    }
                }
            }
        }
    }


    // =========================================================
    // BACHAT GAT CHANGE
    // =========================================================

    protected void ddlBachatGat_SelectedIndexChanged(
        object sender,
        EventArgs e)
    {
        LoadMembers();
    }


    // =========================================================
    // LOAD MEMBERS
    // =========================================================

    private void LoadMembers()
    {
        ddlMember.Items.Clear();

        ddlMember.Items.Add(
            new System.Web.UI.WebControls.ListItem(
                "-- Select Member --",
                ""));

        if (ddlBachatGat.SelectedValue == "")
        {
            return;
        }

        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                SELECT MemberID, MemberCode, MemberName
                FROM Members
                WHERE BachatGatID = @BachatGatID
                AND Status = 'Active'
                ORDER BY MemberName";

            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    Convert.ToInt32(
                        ddlBachatGat.SelectedValue));

                con.Open();

                using (SqlDataReader dr =
                    cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ddlMember.Items.Add(
                            new System.Web.UI.WebControls.ListItem(
                                dr["MemberCode"].ToString()
                                + " - "
                                + dr["MemberName"].ToString(),
                                dr["MemberID"].ToString()));
                    }
                }
            }
        }
    }


    // =========================================================
    // SAVE LOAN
    // =========================================================

    protected void btnSave_Click(
        object sender,
        EventArgs e)
    {
        lblMessage.Visible = false;

        if (ddlBachatGat.SelectedValue == "")
        {
            ShowMessage(
                "Please select Bachat Gat.",
                "alert alert-danger");

            return;
        }

        if (ddlMember.SelectedValue == "")
        {
            ShowMessage(
                "Please select member.",
                "alert alert-danger");

            return;
        }


        DateTime applicationDate;

        if (!DateTime.TryParse(
            txtApplicationDate.Text.Trim(),
            out applicationDate))
        {
            ShowMessage(
                "Please enter a valid application date.",
                "alert alert-danger");

            return;
        }


        decimal loanAmount;
        decimal interestRate;
        int loanTerm;
        decimal emiAmount;


        if (!decimal.TryParse(
            txtLoanAmount.Text.Trim(),
            out loanAmount) ||
            loanAmount <= 0)
        {
            ShowMessage(
                "Please enter a valid loan amount.",
                "alert alert-danger");

            return;
        }


        if (!decimal.TryParse(
            txtInterestRate.Text.Trim(),
            out interestRate) ||
            interestRate < 0)
        {
            ShowMessage(
                "Please enter a valid interest rate.",
                "alert alert-danger");

            return;
        }


        if (!int.TryParse(
            txtLoanTermMonths.Text.Trim(),
            out loanTerm) ||
            loanTerm <= 0)
        {
            ShowMessage(
                "Please enter a valid loan term.",
                "alert alert-danger");

            return;
        }


        if (!decimal.TryParse(
            txtEMIAmount.Text.Trim(),
            out emiAmount) ||
            emiAmount <= 0)
        {
            ShowMessage(
                "Please enter a valid EMI amount.",
                "alert alert-danger");

            return;
        }


        // =====================================================
        // INSERT
        // =====================================================

        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                INSERT INTO Loans
                (
                    MemberID,
                    BachatGatID,
                    ApplicationDate,
                    LoanAmount,
                    InterestRate,
                    LoanPurpose,
                    LoanTermMonths,
                    EMIAmount,
                    Status,
                    Remarks,
                    CreatedDate
                )
                VALUES
                (
                    @MemberID,
                    @BachatGatID,
                    @ApplicationDate,
                    @LoanAmount,
                    @InterestRate,
                    @LoanPurpose,
                    @LoanTermMonths,
                    @EMIAmount,
                    'Pending',
                    @Remarks,
                    GETDATE()
                )";

            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue(
                    "@MemberID",
                    Convert.ToInt32(
                        ddlMember.SelectedValue));

                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    Convert.ToInt32(
                        ddlBachatGat.SelectedValue));

                cmd.Parameters.AddWithValue(
                    "@ApplicationDate",
                    applicationDate);

                cmd.Parameters.AddWithValue(
                    "@LoanAmount",
                    loanAmount);

                cmd.Parameters.AddWithValue(
                    "@InterestRate",
                    interestRate);

                cmd.Parameters.AddWithValue(
                    "@LoanPurpose",
                    txtLoanPurpose.Text.Trim());

                cmd.Parameters.AddWithValue(
                    "@LoanTermMonths",
                    loanTerm);

                cmd.Parameters.AddWithValue(
                    "@EMIAmount",
                    emiAmount);

                cmd.Parameters.AddWithValue(
                    "@Remarks",
                    txtRemarks.Text.Trim());

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }


        ShowMessage(
            "Loan application submitted successfully.",
            "alert alert-success");

        ClearLoanForm();

        LoadLoans();

        LoadSummary();
    }


    // =========================================================
    // LOAD LOANS
    // =========================================================

    private void LoadLoans()
    {
        string query = @"
            SELECT
                l.LoanID,
                m.MemberCode,
                m.MemberName,
                b.GatName,
                l.ApplicationDate,
                l.LoanAmount,
                l.ApprovedAmount,
                l.InterestRate,
                l.LoanTermMonths,
                l.Status
            FROM Loans l
            INNER JOIN Members m
                ON l.MemberID = m.MemberID
            INNER JOIN BachatGat b
                ON l.BachatGatID = b.BachatGatID
            ORDER BY l.LoanID DESC";


        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            using (SqlDataAdapter da =
                new SqlDataAdapter(query, con))
            {
                DataTable dt = new DataTable();

                da.Fill(dt);

                gvLoans.DataSource = dt;

                gvLoans.DataBind();
            }
        }
    }


    // =========================================================
    // SEARCH
    // =========================================================

    protected void btnSearch_Click(
        object sender,
        EventArgs e)
    {
        string search =
            txtSearch.Text.Trim();


        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    l.LoanID,
                    m.MemberCode,
                    m.MemberName,
                    b.GatName,
                    l.ApplicationDate,
                    l.LoanAmount,
                    l.ApprovedAmount,
                    l.InterestRate,
                    l.LoanTermMonths,
                    l.Status
                FROM Loans l
                INNER JOIN Members m
                    ON l.MemberID = m.MemberID
                INNER JOIN BachatGat b
                    ON l.BachatGatID = b.BachatGatID
                WHERE
                    m.MemberName LIKE @Search
                    OR m.MemberCode LIKE @Search
                    OR l.Status LIKE @Search
                ORDER BY l.LoanID DESC";


            using (SqlDataAdapter da =
                new SqlDataAdapter(query, con))
            {
                da.SelectCommand.Parameters.AddWithValue(
                    "@Search",
                    "%" + search + "%");

                DataTable dt = new DataTable();

                da.Fill(dt);

                gvLoans.DataSource = dt;

                gvLoans.DataBind();
            }
        }
    }


    // =========================================================
    // SHOW ALL
    // =========================================================

    protected void btnShowAll_Click(
        object sender,
        EventArgs e)
    {
        txtSearch.Text = "";

        LoadLoans();

        LoadSummary();
    }


    // =========================================================
    // ROW COMMAND
    // =========================================================

    protected void gvLoans_RowCommand(
        object sender,
        System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int loanID =
            Convert.ToInt32(
                e.CommandArgument);


        if (e.CommandName == "ApproveLoan")
        {
            ApproveLoan(loanID);
        }

        else if (e.CommandName == "RejectLoan")
        {
            RejectLoan(loanID);
        }

        else if (e.CommandName == "SelectDistribution")
        {
            LoadDistributionDetails(loanID);
        }
    }


    // =========================================================
    // APPROVE LOAN
    // =========================================================

    private void ApproveLoan(int loanID)
    {
        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                UPDATE Loans
                SET
                    Status = 'Approved',
                    ApprovalDate = GETDATE()
                WHERE LoanID = @LoanID
                AND Status = 'Pending'";


            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue(
                    "@LoanID",
                    loanID);

                con.Open();

                int rows =
                    cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    ShowMessage(
                        "Only pending loans can be approved.",
                        "alert alert-danger");

                    return;
                }
            }
        }


        ShowMessage(
            "Loan approved successfully.",
            "alert alert-success");

        LoadLoans();

        LoadSummary();
    }


    // =========================================================
    // REJECT LOAN
    // =========================================================

    private void RejectLoan(int loanID)
    {
        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                UPDATE Loans
                SET
                    Status = 'Rejected'
                WHERE LoanID = @LoanID
                AND Status = 'Pending'";


            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue(
                    "@LoanID",
                    loanID);

                con.Open();

                int rows =
                    cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    ShowMessage(
                        "Only pending loans can be rejected.",
                        "alert alert-danger");

                    return;
                }
            }
        }


        ShowMessage(
            "Loan rejected.",
            "alert alert-warning");

        LoadLoans();

        LoadSummary();
    }


    // =========================================================
    // LOAD DISTRIBUTION DETAILS
    // =========================================================

    private void LoadDistributionDetails(int loanID)
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
                    l.Status
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
                        string status =
                            dr["Status"].ToString();

                        if (status != "Approved")
                        {
                            ShowMessage(
                                "Only approved loans can be distributed.",
                                "alert alert-danger");

                            return;
                        }


                        hfDistributionLoanID.Value =
                            dr["LoanID"].ToString();

                        txtDistributionLoanID.Text =
                            dr["LoanID"].ToString();

                        txtDistributionMember.Text =
                            dr["MemberName"].ToString();


                        decimal approvedAmount = 0;


                        if (dr["ApprovedAmount"] != DBNull.Value)
                        {
                            approvedAmount =
                                Convert.ToDecimal(
                                    dr["ApprovedAmount"]);
                        }
                        else
                        {
                            approvedAmount =
                                Convert.ToDecimal(
                                    dr["LoanAmount"]);
                        }


                        txtApprovedAmount.Text =
                            approvedAmount.ToString("0.00");

                        txtDistributionAmount.Text =
                            approvedAmount.ToString("0.00");

                        txtDistributionDate.Text =
                            DateTime.Today.ToString("yyyy-MM-dd");

                        txtDistributionRemarks.Text = "";
                    }
                }
            }
        }
    }


    // =========================================================
    // DISTRIBUTE LOAN
    // =========================================================

    protected void btnDistributeLoan_Click(
        object sender,
        EventArgs e)
    {
        lblMessage.Visible = false;


        if (hfDistributionLoanID.Value == "")
        {
            ShowMessage(
                "Please select an approved loan first.",
                "alert alert-danger");

            return;
        }


        DateTime distributionDate;

        if (!DateTime.TryParse(
            txtDistributionDate.Text.Trim(),
            out distributionDate))
        {
            ShowMessage(
                "Please enter a valid distribution date.",
                "alert alert-danger");

            return;
        }


        decimal approvedAmount;

        if (!decimal.TryParse(
            txtApprovedAmount.Text.Trim(),
            out approvedAmount))
        {
            ShowMessage(
                "Invalid approved amount.",
                "alert alert-danger");

            return;
        }


        decimal distributionAmount;

        if (!decimal.TryParse(
            txtDistributionAmount.Text.Trim(),
            out distributionAmount) ||
            distributionAmount <= 0)
        {
            ShowMessage(
                "Please enter a valid distribution amount.",
                "alert alert-danger");

            return;
        }


        if (distributionAmount > approvedAmount)
        {
            ShowMessage(
                "Distribution amount cannot be greater than approved amount.",
                "alert alert-danger");

            return;
        }


        int loanID =
            Convert.ToInt32(
                hfDistributionLoanID.Value);


        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                UPDATE Loans
                SET
                    DistributionDate = @DistributionDate,
                    ApprovedAmount = @DistributionAmount,
                    Status = 'Distributed',
                    Remarks = @Remarks
                WHERE LoanID = @LoanID
                AND Status = 'Approved'";


            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue(
                    "@DistributionDate",
                    distributionDate);

                cmd.Parameters.AddWithValue(
                    "@DistributionAmount",
                    distributionAmount);

                cmd.Parameters.AddWithValue(
                    "@Remarks",
                    txtDistributionRemarks.Text.Trim());

                cmd.Parameters.AddWithValue(
                    "@LoanID",
                    loanID);

                con.Open();

                int rows =
                    cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    ShowMessage(
                        "Loan could not be distributed. Check its status.",
                        "alert alert-danger");

                    return;
                }
            }
        }


        ShowMessage(
            "Loan distributed successfully.",
            "alert alert-success");

        ClearDistributionForm();

        LoadLoans();

        LoadSummary();
    }


    // =========================================================
    // CLEAR DISTRIBUTION
    // =========================================================

    protected void btnClearDistribution_Click(
        object sender,
        EventArgs e)
    {
        ClearDistributionForm();

        lblMessage.Visible = false;
    }


    private void ClearDistributionForm()
    {
        hfDistributionLoanID.Value = "";

        txtDistributionLoanID.Text = "";

        txtDistributionMember.Text = "";

        txtApprovedAmount.Text = "";

        txtDistributionAmount.Text = "";

        txtDistributionDate.Text =
            DateTime.Today.ToString("yyyy-MM-dd");

        txtDistributionRemarks.Text = "";
    }


    // =========================================================
    // SUMMARY
    // =========================================================

    private void LoadSummary()
    {
        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    COUNT(*) AS TotalLoans,

                    SUM(
                        CASE
                            WHEN Status = 'Pending'
                            THEN 1
                            ELSE 0
                        END
                    ) AS PendingLoans,

                    SUM(
                        CASE
                            WHEN Status = 'Approved'
                            THEN 1
                            ELSE 0
                        END
                    ) AS ApprovedLoans,

                    ISNULL(
                        SUM(LoanAmount),
                        0
                    ) AS TotalLoanAmount

                FROM Loans";


            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                con.Open();

                using (SqlDataReader dr =
                    cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        lblTotalLoans.Text =
                            dr["TotalLoans"].ToString();

                        lblPendingLoans.Text =
                            dr["PendingLoans"].ToString();

                        lblApprovedLoans.Text =
                            dr["ApprovedLoans"].ToString();

                        lblTotalLoanAmount.Text =
                            "₹ "
                            + Convert.ToDecimal(
                                dr["TotalLoanAmount"])
                                .ToString("N2");
                    }
                }
            }
        }
    }


    // =========================================================
    // CLEAR LOAN FORM
    // =========================================================

    protected void btnClear_Click(
        object sender,
        EventArgs e)
    {
        ClearLoanForm();

        lblMessage.Visible = false;
    }


    private void ClearLoanForm()
    {
        hfLoanID.Value = "";

        ddlBachatGat.SelectedIndex = 0;

        ddlMember.Items.Clear();

        ddlMember.Items.Add(
            new System.Web.UI.WebControls.ListItem(
                "-- Select Member --",
                ""));

        txtApplicationDate.Text =
            DateTime.Today.ToString("yyyy-MM-dd");

        txtLoanAmount.Text = "";

        txtInterestRate.Text = "";

        txtLoanTermMonths.Text = "";

        txtEMIAmount.Text = "";

        txtLoanPurpose.Text = "";

        txtRemarks.Text = "";

        ddlLoanStatus.SelectedValue =
            "Pending";

        btnSave.Text =
            "Submit Loan Application";
    }


    // =========================================================
    // MESSAGE
    // =========================================================

    private void ShowMessage(
        string message,
        string cssClass)
    {
        lblMessage.Text = message;

        lblMessage.CssClass =
            cssClass;

        lblMessage.Visible = true;
    }
}