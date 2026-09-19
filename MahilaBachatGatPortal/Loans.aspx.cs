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
    // LOAD BACHAT GAT
    // =========================================================

    private void LoadBachatGats()
    {
        ddlBachatGat.Items.Clear();

        int bachatGatID =
            RoleHelper.GetBachatGatID();

        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    BachatGatID,
                    GatName
                FROM BachatGat
                WHERE
                    BachatGatID = @BachatGatID
                    AND Status = 'Active'
                ORDER BY GatName";

            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    bachatGatID);

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

        // Automatically select the user's Bachat Gat
        if (ddlBachatGat.Items.Count > 0)
        {
            ddlBachatGat.SelectedValue =
                bachatGatID.ToString();

            LoadMembers();
        }
    }


    // =========================================================
    // BACHAT GAT CHANGE
    // =========================================================

    protected void ddlBachatGat_SelectedIndexChanged(
        object sender,
        EventArgs e)
    {
        int assignedBachatGatID =
            RoleHelper.GetBachatGatID();

        // Prevent President/Secretary from selecting
        // another Bachat Gat.
        if (ddlBachatGat.SelectedValue !=
            assignedBachatGatID.ToString())
        {
            ShowMessage(
                "You cannot select another Bachat Gat.",
                "alert alert-danger");

            ddlBachatGat.SelectedValue =
                assignedBachatGatID.ToString();
        }

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

        int bachatGatID =
            RoleHelper.GetBachatGatID();

        if (ddlBachatGat.SelectedValue == "")
        {
            return;
        }

        // Extra server-side protection
        if (Convert.ToInt32(
                ddlBachatGat.SelectedValue) !=
            bachatGatID)
        {
            return;
        }

        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    MemberID,
                    MemberCode,
                    MemberName
                FROM Members
                WHERE
                    BachatGatID = @BachatGatID
                    AND Status = 'Active'
                ORDER BY MemberName";

            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    bachatGatID);

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

        int bachatGatID =
            RoleHelper.GetBachatGatID();

        if (ddlBachatGat.SelectedValue == "")
        {
            ShowMessage(
                "Please select Bachat Gat.",
                "alert alert-danger");

            return;
        }

        // Make sure selected Gat is user's Gat
        if (Convert.ToInt32(
                ddlBachatGat.SelectedValue) !=
            bachatGatID)
        {
            ShowMessage(
                "You cannot create a loan for another Bachat Gat.",
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


        int memberID =
            Convert.ToInt32(
                ddlMember.SelectedValue);


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


        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            con.Open();


            // =================================================
            // VERIFY MEMBER BELONGS TO CURRENT BACHAT GAT
            // =================================================

            string memberCheckQuery = @"
                SELECT COUNT(*)
                FROM Members
                WHERE
                    MemberID = @MemberID
                    AND BachatGatID = @BachatGatID
                    AND Status = 'Active'";


            using (SqlCommand memberCheckCmd =
                new SqlCommand(
                    memberCheckQuery,
                    con))
            {
                memberCheckCmd.Parameters.AddWithValue(
                    "@MemberID",
                    memberID);

                memberCheckCmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    bachatGatID);


                int memberExists =
                    Convert.ToInt32(
                        memberCheckCmd.ExecuteScalar());


                if (memberExists == 0)
                {
                    ShowMessage(
                        "Invalid member selection.",
                        "alert alert-danger");

                    return;
                }
            }


            // =================================================
            // INSERT LOAN
            // =================================================

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
                    memberID);

                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    bachatGatID);

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
        int bachatGatID =
            RoleHelper.GetBachatGatID();

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
                l.BachatGatID = @BachatGatID
            ORDER BY l.LoanID DESC";


        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            using (SqlDataAdapter da =
                new SqlDataAdapter(query, con))
            {
                da.SelectCommand.Parameters.AddWithValue(
                    "@BachatGatID",
                    bachatGatID);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                gvLoans.DataSource =
                    dt;

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

        int bachatGatID =
            RoleHelper.GetBachatGatID();


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
                    l.BachatGatID = @BachatGatID
                    AND
                    (
                        m.MemberName LIKE @Search
                        OR m.MemberCode LIKE @Search
                        OR l.Status LIKE @Search
                    )
                ORDER BY l.LoanID DESC";


            using (SqlDataAdapter da =
                new SqlDataAdapter(
                    query,
                    con))
            {
                da.SelectCommand.Parameters.AddWithValue(
                    "@Search",
                    "%" + search + "%");

                da.SelectCommand.Parameters.AddWithValue(
                    "@BachatGatID",
                    bachatGatID);


                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                gvLoans.DataSource =
                    dt;

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
        int bachatGatID =
            RoleHelper.GetBachatGatID();


        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                UPDATE Loans
                SET
                    Status = 'Approved',
                    ApprovalDate = GETDATE(),
                    ApprovedBy = @ApprovedBy
                WHERE
                    LoanID = @LoanID
                    AND BachatGatID = @BachatGatID
                    AND Status = 'Pending'";


            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue(
                    "@LoanID",
                    loanID);

                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    bachatGatID);

                cmd.Parameters.AddWithValue(
                    "@ApprovedBy",
                    RoleHelper.GetUserID());

                con.Open();

                int rows =
                    cmd.ExecuteNonQuery();


                if (rows == 0)
                {
                    ShowMessage(
                        "Only pending loans from your Bachat Gat can be approved.",
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
        int bachatGatID =
            RoleHelper.GetBachatGatID();


        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                UPDATE Loans
                SET
                    Status = 'Rejected'
                WHERE
                    LoanID = @LoanID
                    AND BachatGatID = @BachatGatID
                    AND Status = 'Pending'";


            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue(
                    "@LoanID",
                    loanID);

                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    bachatGatID);

                con.Open();


                int rows =
                    cmd.ExecuteNonQuery();


                if (rows == 0)
                {
                    ShowMessage(
                        "Only pending loans from your Bachat Gat can be rejected.",
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
        int bachatGatID =
            RoleHelper.GetBachatGatID();


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
                WHERE
                    l.LoanID = @LoanID
                    AND l.BachatGatID = @BachatGatID";


            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue(
                    "@LoanID",
                    loanID);

                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    bachatGatID);

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


                        if (dr["ApprovedAmount"] !=
                            DBNull.Value)
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
                            DateTime.Today.ToString(
                                "yyyy-MM-dd");

                        txtDistributionRemarks.Text =
                            "";
                    }
                    else
                    {
                        ShowMessage(
                            "Loan not found or you do not have permission to access it.",
                            "alert alert-danger");
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


        if (distributionAmount >
            approvedAmount)
        {
            ShowMessage(
                "Distribution amount cannot be greater than approved amount.",
                "alert alert-danger");

            return;
        }


        int loanID =
            Convert.ToInt32(
                hfDistributionLoanID.Value);


        int bachatGatID =
            RoleHelper.GetBachatGatID();


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
                WHERE
                    LoanID = @LoanID
                    AND BachatGatID = @BachatGatID
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

                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    bachatGatID);


                con.Open();


                int rows =
                    cmd.ExecuteNonQuery();


                if (rows == 0)
                {
                    ShowMessage(
                        "Loan could not be distributed. Check the loan status and Bachat Gat.",
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
        int bachatGatID =
            RoleHelper.GetBachatGatID();


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

                FROM Loans

                WHERE BachatGatID = @BachatGatID";


            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    bachatGatID);

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

        // Keep the President/Secretary on
        // their own Bachat Gat.
        if (ddlBachatGat.Items.Count > 0)
        {
            ddlBachatGat.SelectedValue =
                RoleHelper.GetBachatGatID().ToString();
        }


        LoadMembers();


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