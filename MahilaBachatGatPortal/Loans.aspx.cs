using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Loans : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RoleHelper.RequireLogin(this);

        pnlLoanDistribution.Visible = IsManagementUser();

        if (!IsPostBack)
        {
            LoadBachatGats();
            LoadMembers();
            LoadLoans();
            LoadSummary();
            SetDefaultDates();
        }
    }

    protected bool IsManagementUser()
    {
        return RoleHelper.IsAdmin() || RoleHelper.IsPresidentOrSecretary();
    }

    private void SetDefaultDates()
    {
        if (string.IsNullOrWhiteSpace(txtApplicationDate.Text))
        {
            txtApplicationDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        if (string.IsNullOrWhiteSpace(txtDistributionDate.Text))
        {
            txtDistributionDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    private void LoadBachatGats()
    {
        try
        {
            ddlBachatGat.Items.Clear();

            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query;

                if (RoleHelper.IsAdmin())
                {
                    query = @"
                        SELECT BachatGatID, GatName
                        FROM BachatGat
                        WHERE Status = 'Active'
                        ORDER BY GatName";
                }
                else
                {
                    query = @"
                        SELECT BachatGatID, GatName
                        FROM BachatGat
                        WHERE Status = 'Active'
                        AND BachatGatID = @BachatGatID
                        ORDER BY GatName";
                }

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (!RoleHelper.IsAdmin())
                    {
                        cmd.Parameters.AddWithValue("@BachatGatID", RoleHelper.GetBachatGatID());
                    }

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        ddlBachatGat.DataSource = dr;
                        ddlBachatGat.DataTextField = "GatName";
                        ddlBachatGat.DataValueField = "BachatGatID";
                        ddlBachatGat.DataBind();
                    }
                }
            }

            if (RoleHelper.IsAdmin())
            {
                ddlBachatGat.Items.Insert(
                    0,
                    new ListItem("-- Select Bachat Gat --", "")
                );
            }
            else if (ddlBachatGat.Items.Count > 0)
            {
                ddlBachatGat.SelectedValue =
                    RoleHelper.GetBachatGatID().ToString();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error loading Bachat Gat: " + ex.Message,
                true
            );
        }
    }

    private void LoadMembers()
    {
        try
        {
            ddlMember.Items.Clear();

            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = @"
                    SELECT
                        MemberID,
                        MemberName
                    FROM Members
                    WHERE Status = 'Active'
                    AND BachatGatID = @BachatGatID";

                if (RoleHelper.IsMember())
                {
                    query += @"
                        AND MemberID = @MemberID";
                }

                query += " ORDER BY MemberName";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue(
                        "@BachatGatID",
                        RoleHelper.GetBachatGatID()
                    );

                    if (RoleHelper.IsMember())
                    {
                        cmd.Parameters.AddWithValue(
                            "@MemberID",
                            RoleHelper.GetMemberID()
                        );
                    }

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        ddlMember.DataSource = dr;
                        ddlMember.DataTextField = "MemberName";
                        ddlMember.DataValueField = "MemberID";
                        ddlMember.DataBind();
                    }
                }
            }

            if (RoleHelper.IsMember() && ddlMember.Items.Count > 0)
            {
                ddlMember.SelectedValue =
                    RoleHelper.GetMemberID().ToString();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error loading members: " + ex.Message,
                true
            );
        }
    }

    private void LoadLoans()
    {
        try
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = @"
                    SELECT
                        L.LoanID,
                        M.MemberName,
                        B.GatName,
                        L.ApplicationDate,
                        L.LoanAmount,
                        L.ApprovedAmount,
                        L.InterestRate,
                        L.LoanTermMonths,
                        L.Status
                    FROM Loans L
                    INNER JOIN Members M
                        ON L.MemberID = M.MemberID
                    INNER JOIN BachatGat B
                        ON L.BachatGatID = B.BachatGatID
                    WHERE L.BachatGatID = @BachatGatID";

                if (RoleHelper.IsMember())
                {
                    query += @"
                        AND L.MemberID = @MemberID";
                }

                query += " ORDER BY L.LoanID DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue(
                        "@BachatGatID",
                        RoleHelper.GetBachatGatID()
                    );

                    if (RoleHelper.IsMember())
                    {
                        cmd.Parameters.AddWithValue(
                            "@MemberID",
                            RoleHelper.GetMemberID()
                        );
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    gvLoans.DataSource = dt;
                    gvLoans.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error loading loans: " + ex.Message,
                true
            );
        }
    }

    private void LoadSummary()
    {
        try
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = @"
                    SELECT
                        COUNT(*) AS TotalLoans,
                        SUM(CASE WHEN Status = 'Pending' THEN 1 ELSE 0 END) AS PendingLoans,
                        SUM(CASE WHEN Status = 'Approved' THEN 1 ELSE 0 END) AS ApprovedLoans,
                        ISNULL(SUM(LoanAmount), 0) AS TotalLoanAmount
                    FROM Loans
                    WHERE BachatGatID = @BachatGatID";

                if (RoleHelper.IsMember())
                {
                    query += " AND MemberID = @MemberID";
                }

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue(
                        "@BachatGatID",
                        RoleHelper.GetBachatGatID()
                    );

                    if (RoleHelper.IsMember())
                    {
                        cmd.Parameters.AddWithValue(
                            "@MemberID",
                            RoleHelper.GetMemberID()
                        );
                    }

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
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
                                Convert.ToDecimal(
                                    dr["TotalLoanAmount"]
                                ).ToString("N2");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error loading loan summary: " + ex.Message,
                true
            );
        }
    }

    protected void ddlBachatGat_SelectedIndexChanged(
        object sender,
        EventArgs e)
    {
        if (!RoleHelper.IsAdmin())
        {
            ddlBachatGat.SelectedValue =
                RoleHelper.GetBachatGatID().ToString();
        }

        LoadMembers();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlBachatGat.SelectedValue == "")
        {
            ShowMessage("Please select Bachat Gat.", true);
            return;
        }

        if (ddlMember.SelectedValue == "")
        {
            ShowMessage("Please select member.", true);
            return;
        }

        int selectedBachatGatID =
            Convert.ToInt32(ddlBachatGat.SelectedValue);

        int selectedMemberID =
            Convert.ToInt32(ddlMember.SelectedValue);

        if (!RoleHelper.IsAdmin() &&
            selectedBachatGatID != RoleHelper.GetBachatGatID())
        {
            ShowMessage(
                "You cannot select another Bachat Gat.",
                true
            );
            return;
        }

        if (RoleHelper.IsMember() &&
            selectedMemberID != RoleHelper.GetMemberID())
        {
            ShowMessage(
                "You can apply only for yourself.",
                true
            );
            return;
        }

        decimal loanAmount;
        decimal interestRate;
        decimal emiAmount;
        int loanTermMonths;
        DateTime applicationDate;

        if (!decimal.TryParse(
            txtLoanAmount.Text.Trim(),
            out loanAmount))
        {
            ShowMessage(
                "Please enter valid loan amount.",
                true
            );
            return;
        }

        if (!decimal.TryParse(
            txtInterestRate.Text.Trim(),
            out interestRate))
        {
            ShowMessage(
                "Please enter valid interest rate.",
                true
            );
            return;
        }

        if (!int.TryParse(
            txtLoanTermMonths.Text.Trim(),
            out loanTermMonths))
        {
            ShowMessage(
                "Please enter valid loan term.",
                true
            );
            return;
        }

        if (!decimal.TryParse(
            txtEMIAmount.Text.Trim(),
            out emiAmount))
        {
            ShowMessage(
                "Please enter valid EMI amount.",
                true
            );
            return;
        }

        if (!DateTime.TryParse(
            txtApplicationDate.Text.Trim(),
            out applicationDate))
        {
            ShowMessage(
                "Please enter valid application date.",
                true
            );
            return;
        }

        try
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();

                if (!string.IsNullOrEmpty(hfLoanID.Value))
                {
                    int loanID =
                        Convert.ToInt32(hfLoanID.Value);

                    string query = @"
                        UPDATE Loans
                        SET
                            MemberID = @MemberID,
                            BachatGatID = @BachatGatID,
                            ApplicationDate = @ApplicationDate,
                            LoanAmount = @LoanAmount,
                            InterestRate = @InterestRate,
                            LoanPurpose = @LoanPurpose,
                            LoanTermMonths = @LoanTermMonths,
                            EMIAmount = @EMIAmount,
                            Status = @Status,
                            Remarks = @Remarks
                        WHERE LoanID = @LoanID
                        AND BachatGatID = @BachatGatID";

                    if (RoleHelper.IsMember())
                    {
                        query +=
                            " AND MemberID = @CurrentMemberID";
                    }

                    using (SqlCommand cmd =
                        new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue(
                            "@LoanID",
                            loanID
                        );

                        cmd.Parameters.AddWithValue(
                            "@MemberID",
                            selectedMemberID
                        );

                        cmd.Parameters.AddWithValue(
                            "@BachatGatID",
                            selectedBachatGatID
                        );

                        cmd.Parameters.AddWithValue(
                            "@ApplicationDate",
                            applicationDate
                        );

                        cmd.Parameters.AddWithValue(
                            "@LoanAmount",
                            loanAmount
                        );

                        cmd.Parameters.AddWithValue(
                            "@InterestRate",
                            interestRate
                        );

                        cmd.Parameters.AddWithValue(
                            "@LoanPurpose",
                            txtLoanPurpose.Text.Trim()
                        );

                        cmd.Parameters.AddWithValue(
                            "@LoanTermMonths",
                            loanTermMonths
                        );

                        cmd.Parameters.AddWithValue(
                            "@EMIAmount",
                            emiAmount
                        );

                        cmd.Parameters.AddWithValue(
                            "@Status",
                            RoleHelper.IsMember()
                                ? "Pending"
                                : ddlLoanStatus.SelectedValue
                        );

                        cmd.Parameters.AddWithValue(
                            "@Remarks",
                            txtRemarks.Text.Trim()
                        );

                        if (RoleHelper.IsMember())
                        {
                            cmd.Parameters.AddWithValue(
                                "@CurrentMemberID",
                                RoleHelper.GetMemberID()
                            );
                        }

                        int rows =
                            cmd.ExecuteNonQuery();

                        if (rows == 0)
                        {
                            ShowMessage(
                                "Loan not found or you do not have permission to update it.",
                                true
                            );
                            return;
                        }
                    }

                    ShowMessage(
                        "Loan application updated successfully.",
                        false
                    );
                }
                else
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
                            selectedMemberID
                        );

                        cmd.Parameters.AddWithValue(
                            "@BachatGatID",
                            selectedBachatGatID
                        );

                        cmd.Parameters.AddWithValue(
                            "@ApplicationDate",
                            applicationDate
                        );

                        cmd.Parameters.AddWithValue(
                            "@LoanAmount",
                            loanAmount
                        );

                        cmd.Parameters.AddWithValue(
                            "@InterestRate",
                            interestRate
                        );

                        cmd.Parameters.AddWithValue(
                            "@LoanPurpose",
                            txtLoanPurpose.Text.Trim()
                        );

                        cmd.Parameters.AddWithValue(
                            "@LoanTermMonths",
                            loanTermMonths
                        );

                        cmd.Parameters.AddWithValue(
                            "@EMIAmount",
                            emiAmount
                        );

                        cmd.Parameters.AddWithValue(
                            "@Remarks",
                            txtRemarks.Text.Trim()
                        );

                        cmd.ExecuteNonQuery();
                    }

                    ShowMessage(
                        "Loan application submitted successfully.",
                        false
                    );
                }
            }

            ClearForm();
            LoadLoans();
            LoadSummary();
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error saving loan: " + ex.Message,
                true
            );
        }
    }

    protected void btnClear_Click(
        object sender,
        EventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    {
        hfLoanID.Value = "";

        txtApplicationDate.Text =
            DateTime.Now.ToString("yyyy-MM-dd");

        txtLoanAmount.Text = "";
        txtInterestRate.Text = "";
        txtLoanTermMonths.Text = "";
        txtEMIAmount.Text = "";
        txtLoanPurpose.Text = "";
        txtRemarks.Text = "";

        ddlLoanStatus.SelectedValue = "Pending";

        LoadBachatGats();
        LoadMembers();

        if (RoleHelper.IsMember())
        {
            if (ddlBachatGat.Items.Count > 0)
            {
                ddlBachatGat.SelectedValue =
                    RoleHelper.GetBachatGatID().ToString();
            }

            if (ddlMember.Items.Count > 0)
            {
                ddlMember.SelectedValue =
                    RoleHelper.GetMemberID().ToString();
            }
        }
    }

    protected void btnSearch_Click(
        object sender,
        EventArgs e)
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query = @"
                    SELECT
                        L.LoanID,
                        M.MemberName,
                        B.GatName,
                        L.ApplicationDate,
                        L.LoanAmount,
                        L.ApprovedAmount,
                        L.InterestRate,
                        L.LoanTermMonths,
                        L.Status
                    FROM Loans L
                    INNER JOIN Members M
                        ON L.MemberID = M.MemberID
                    INNER JOIN BachatGat B
                        ON L.BachatGatID = B.BachatGatID
                    WHERE L.BachatGatID = @BachatGatID
                    AND
                    (
                        M.MemberName LIKE @Search
                        OR L.Status LIKE @Search
                    )";

                if (RoleHelper.IsMember())
                {
                    query +=
                        " AND L.MemberID = @MemberID";
                }

                query +=
                    " ORDER BY L.LoanID DESC";

                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue(
                        "@BachatGatID",
                        RoleHelper.GetBachatGatID()
                    );

                    cmd.Parameters.AddWithValue(
                        "@Search",
                        "%" + txtSearch.Text.Trim() + "%"
                    );

                    if (RoleHelper.IsMember())
                    {
                        cmd.Parameters.AddWithValue(
                            "@MemberID",
                            RoleHelper.GetMemberID()
                        );
                    }

                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    DataTable dt =
                        new DataTable();

                    da.Fill(dt);

                    gvLoans.DataSource = dt;
                    gvLoans.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Search error: " + ex.Message,
                true
            );
        }
    }

    protected void btnShowAll_Click(
        object sender,
        EventArgs e)
    {
        txtSearch.Text = "";

        LoadLoans();
        LoadSummary();
    }

    protected void gvLoans_RowCommand(
        object sender,
        GridViewCommandEventArgs e)
    {
        int loanID;

        if (!int.TryParse(
            e.CommandArgument.ToString(),
            out loanID))
        {
            ShowMessage(
                "Invalid loan ID.",
                true
            );
            return;
        }

        if (e.CommandName == "ApproveLoan")
        {
            if (!IsManagementUser())
            {
                Response.Redirect("Dashboard.aspx");
                return;
            }

            ApproveLoan(loanID);
        }
        else if (e.CommandName == "RejectLoan")
        {
            if (!IsManagementUser())
            {
                Response.Redirect("Dashboard.aspx");
                return;
            }

            RejectLoan(loanID);
        }
        else if (e.CommandName == "SelectDistribution")
        {
            if (!IsManagementUser())
            {
                Response.Redirect("Dashboard.aspx");
                return;
            }

            LoadDistributionDetails(loanID);
        }
    }

    private void ApproveLoan(int loanID)
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query = @"
                    UPDATE Loans
                    SET
                        Status = 'Approved',
                        ApprovalDate = GETDATE(),
                        ApprovedAmount = LoanAmount,
                        ApprovedBy = @ApprovedBy
                    WHERE LoanID = @LoanID
                    AND BachatGatID = @BachatGatID
                    AND Status = 'Pending'";

                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue(
                        "@LoanID",
                        loanID
                    );

                    cmd.Parameters.AddWithValue(
                        "@BachatGatID",
                        RoleHelper.GetBachatGatID()
                    );

                    cmd.Parameters.AddWithValue(
                        "@ApprovedBy",
                        RoleHelper.GetUserID()
                    );

                    con.Open();

                    int rows =
                        cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        ShowMessage(
                            "Loan approved successfully.",
                            false
                        );
                    }
                    else
                    {
                        ShowMessage(
                            "Loan could not be approved.",
                            true
                        );
                    }
                }
            }

            LoadLoans();
            LoadSummary();
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error approving loan: " + ex.Message,
                true
            );
        }
    }

    private void RejectLoan(int loanID)
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query = @"
                    UPDATE Loans
                    SET Status = 'Rejected'
                    WHERE LoanID = @LoanID
                    AND BachatGatID = @BachatGatID
                    AND Status = 'Pending'";

                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue(
                        "@LoanID",
                        loanID
                    );

                    cmd.Parameters.AddWithValue(
                        "@BachatGatID",
                        RoleHelper.GetBachatGatID()
                    );

                    con.Open();

                    int rows =
                        cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        ShowMessage(
                            "Loan rejected successfully.",
                            false
                        );
                    }
                    else
                    {
                        ShowMessage(
                            "Loan could not be rejected.",
                            true
                        );
                    }
                }
            }

            LoadLoans();
            LoadSummary();
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error rejecting loan: " + ex.Message,
                true
            );
        }
    }

    private void LoadDistributionDetails(
        int loanID)
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query = @"
                    SELECT
                        L.LoanID,
                        M.MemberName,
                        L.ApprovedAmount,
                        L.DistributionDate,
                        L.DistributionAmount,
                        L.Remarks
                    FROM Loans L
                    INNER JOIN Members M
                        ON L.MemberID = M.MemberID
                    WHERE L.LoanID = @LoanID
                    AND L.BachatGatID = @BachatGatID
                    AND L.Status = 'Approved'";

                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue(
                        "@LoanID",
                        loanID
                    );

                    cmd.Parameters.AddWithValue(
                        "@BachatGatID",
                        RoleHelper.GetBachatGatID()
                    );

                    con.Open();

                    using (SqlDataReader dr =
                        cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            hfDistributionLoanID.Value =
                                dr["LoanID"].ToString();

                            txtDistributionLoanID.Text =
                                dr["LoanID"].ToString();

                            txtDistributionMember.Text =
                                dr["MemberName"].ToString();

                            txtApprovedAmount.Text =
                                dr["ApprovedAmount"].ToString();

                            if (dr["DistributionDate"] !=
                                DBNull.Value)
                            {
                                txtDistributionDate.Text =
                                    Convert.ToDateTime(
                                        dr["DistributionDate"]
                                    ).ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                txtDistributionDate.Text =
                                    DateTime.Now.ToString("yyyy-MM-dd");
                            }

                            if (dr["DistributionAmount"] !=
                                DBNull.Value)
                            {
                                txtDistributionAmount.Text =
                                    dr["DistributionAmount"].ToString();
                            }
                            else
                            {
                                txtDistributionAmount.Text =
                                    dr["ApprovedAmount"].ToString();
                            }

                            txtDistributionRemarks.Text =
                                dr["Remarks"].ToString();
                        }
                        else
                        {
                            ShowMessage(
                                "Only approved loans can be distributed.",
                                true
                            );
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error loading distribution details: " +
                ex.Message,
                true
            );
        }
    }

    protected void btnDistributeLoan_Click(
        object sender,
        EventArgs e)
    {
        if (!IsManagementUser())
        {
            Response.Redirect("Dashboard.aspx");
            return;
        }

        int loanID;

        if (!int.TryParse(
            hfDistributionLoanID.Value,
            out loanID))
        {
            ShowMessage(
                "Please select an approved loan.",
                true
            );
            return;
        }

        DateTime distributionDate;

        if (!DateTime.TryParse(
            txtDistributionDate.Text.Trim(),
            out distributionDate))
        {
            ShowMessage(
                "Please enter valid distribution date.",
                true
            );
            return;
        }

        decimal distributionAmount;

        if (!decimal.TryParse(
            txtDistributionAmount.Text.Trim(),
            out distributionAmount))
        {
            ShowMessage(
                "Please enter valid distribution amount.",
                true
            );
            return;
        }

        decimal approvedAmount;

        if (!decimal.TryParse(
            txtApprovedAmount.Text.Trim(),
            out approvedAmount))
        {
            ShowMessage(
                "Invalid approved amount.",
                true
            );
            return;
        }

        if (distributionAmount > approvedAmount)
        {
            ShowMessage(
                "Distribution amount cannot be greater than approved amount.",
                true
            );
            return;
        }

        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query = @"
                    UPDATE Loans
                    SET
                        DistributionDate = @DistributionDate,
                        DistributionAmount = @DistributionAmount,
                        Remarks = @Remarks,
                        Status = 'Distributed'
                    WHERE LoanID = @LoanID
                    AND BachatGatID = @BachatGatID
                    AND Status = 'Approved'";

                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue(
                        "@LoanID",
                        loanID
                    );

                    cmd.Parameters.AddWithValue(
                        "@BachatGatID",
                        RoleHelper.GetBachatGatID()
                    );

                    cmd.Parameters.AddWithValue(
                        "@DistributionDate",
                        distributionDate
                    );

                    cmd.Parameters.AddWithValue(
                        "@DistributionAmount",
                        distributionAmount
                    );

                    cmd.Parameters.AddWithValue(
                        "@Remarks",
                        txtDistributionRemarks.Text.Trim()
                    );

                    con.Open();

                    int rows =
                        cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        ShowMessage(
                            "Loan distributed successfully.",
                            false
                        );
                    }
                    else
                    {
                        ShowMessage(
                            "Loan could not be distributed.",
                            true
                        );
                    }
                }
            }

            ClearDistribution();
            LoadLoans();
            LoadSummary();
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error distributing loan: " + ex.Message,
                true
            );
        }
    }

    protected void btnClearDistribution_Click(
        object sender,
        EventArgs e)
    {
        ClearDistribution();
    }

    private void ClearDistribution()
    {
        hfDistributionLoanID.Value = "";
        txtDistributionLoanID.Text = "";
        txtDistributionMember.Text = "";
        txtApprovedAmount.Text = "";
        txtDistributionDate.Text =
            DateTime.Now.ToString("yyyy-MM-dd");
        txtDistributionAmount.Text = "";
        txtDistributionRemarks.Text = "";
    }

    private void ShowMessage(
        string message,
        bool error)
    {
        lblMessage.Text = message;
        lblMessage.Visible = true;

        lblMessage.CssClass =
            error
                ? "alert alert-danger"
                : "alert alert-success";
    }
}