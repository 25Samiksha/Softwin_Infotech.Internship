using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class MemberProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RoleHelper.RequireLogin(this);

        if (!IsPostBack)
        {
            if (RoleHelper.IsMember())
            {
                LoadMemberProfileForLoggedInMember();
            }
            else
            {
                LoadBachatGat();
            }
        }
    }

    private void LoadBachatGat()
    {
        try
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
                    WHERE BachatGatID = @BachatGatID
                    AND Status = 'Active'
                    ORDER BY GatName";
            }

            using (SqlConnection con = DBHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (!RoleHelper.IsAdmin())
                {
                    cmd.Parameters.AddWithValue(
                        "@BachatGatID",
                        RoleHelper.GetBachatGatID());
                }

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    ddlBachatGat.Items.Clear();
                    ddlBachatGat.Items.Add(
                        new ListItem("-- Select Bachat Gat --", ""));

                    while (dr.Read())
                    {
                        ddlBachatGat.Items.Add(
                            new ListItem(
                                dr["GatName"].ToString(),
                                dr["BachatGatID"].ToString()));
                    }
                }
            }

            ddlMember.Items.Clear();
            ddlMember.Items.Add(
                new ListItem("-- Select Member --", ""));

            if (!RoleHelper.IsAdmin() &&
                ddlBachatGat.Items.Count > 1)
            {
                ddlBachatGat.SelectedValue =
                    RoleHelper.GetBachatGatID().ToString();

                LoadMembers();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error loading Bachat Gat: " + ex.Message,
                System.Drawing.Color.Red);
        }
    }

    protected void ddlBachatGat_SelectedIndexChanged(
        object sender,
        EventArgs e)
    {
        ddlMember.Items.Clear();

        ddlMember.Items.Add(
            new ListItem("-- Select Member --", ""));

        pnlProfile.Visible = false;
        lblMessage.Text = "";

        if (ddlBachatGat.SelectedValue == "")
        {
            return;
        }

        if (!RoleHelper.IsAdmin() &&
            Convert.ToInt32(ddlBachatGat.SelectedValue) !=
            RoleHelper.GetBachatGatID())
        {
            ShowMessage(
                "You cannot view members of another Bachat Gat.",
                System.Drawing.Color.Red);

            ddlBachatGat.SelectedValue =
                RoleHelper.GetBachatGatID().ToString();

            LoadMembers();
            return;
        }

        LoadMembers();
    }

    private void LoadMembers()
    {
        if (ddlBachatGat.SelectedValue == "")
        {
            return;
        }

        int bachatGatID =
            Convert.ToInt32(ddlBachatGat.SelectedValue);

        if (!RoleHelper.IsAdmin() &&
            bachatGatID != RoleHelper.GetBachatGatID())
        {
            return;
        }

        string query = @"
            SELECT MemberID, MemberName
            FROM Members
            WHERE BachatGatID = @BachatGatID
            AND Status = 'Active'
            ORDER BY MemberName";

        using (SqlConnection con = DBHelper.GetConnection())
        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue(
                "@BachatGatID",
                bachatGatID);

            con.Open();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    ddlMember.Items.Add(
                        new ListItem(
                            dr["MemberName"].ToString(),
                            dr["MemberID"].ToString()));
                }
            }
        }
    }

    protected void btnViewProfile_Click(
        object sender,
        EventArgs e)
    {
        lblMessage.Text = "";
        pnlProfile.Visible = false;

        if (RoleHelper.IsMember())
        {
            LoadMemberProfileForLoggedInMember();
            return;
        }

        if (ddlBachatGat.SelectedValue == "")
        {
            ShowMessage(
                "Please select Bachat Gat.",
                System.Drawing.Color.Red);
            return;
        }

        if (ddlMember.SelectedValue == "")
        {
            ShowMessage(
                "Please select Member.",
                System.Drawing.Color.Red);
            return;
        }

        int memberID =
            Convert.ToInt32(ddlMember.SelectedValue);

        int bachatGatID =
            Convert.ToInt32(ddlBachatGat.SelectedValue);

        if (!RoleHelper.IsAdmin() &&
            bachatGatID != RoleHelper.GetBachatGatID())
        {
            ShowMessage(
                "You cannot view members of another Bachat Gat.",
                System.Drawing.Color.Red);
            return;
        }

        if (!IsMemberInSelectedGat(
            memberID,
            bachatGatID))
        {
            ShowMessage(
                "Invalid member selection.",
                System.Drawing.Color.Red);
            return;
        }

        LoadProfile(
            memberID,
            bachatGatID);
    }

    private void LoadMemberProfileForLoggedInMember()
    {
        int memberID = RoleHelper.GetMemberID();
        int bachatGatID = RoleHelper.GetBachatGatID();

        if (memberID <= 0 || bachatGatID <= 0)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
            return;
        }

        ddlBachatGat.Items.Clear();

        string gatQuery = @"
            SELECT BachatGatID, GatName
            FROM BachatGat
            WHERE BachatGatID = @BachatGatID
            AND Status = 'Active'";

        using (SqlConnection con = DBHelper.GetConnection())
        using (SqlCommand cmd = new SqlCommand(gatQuery, con))
        {
            cmd.Parameters.AddWithValue(
                "@BachatGatID",
                bachatGatID);

            con.Open();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    ddlBachatGat.Items.Add(
                        new ListItem(
                            dr["GatName"].ToString(),
                            dr["BachatGatID"].ToString()));
                }
            }
        }

        ddlMember.Items.Clear();

        string memberQuery = @"
            SELECT MemberID, MemberName
            FROM Members
            WHERE MemberID = @MemberID
            AND BachatGatID = @BachatGatID
            AND UserID = @UserID
            AND Status = 'Active'";

        using (SqlConnection con = DBHelper.GetConnection())
        using (SqlCommand cmd = new SqlCommand(memberQuery, con))
        {
            cmd.Parameters.AddWithValue(
                "@MemberID",
                memberID);

            cmd.Parameters.AddWithValue(
                "@BachatGatID",
                bachatGatID);

            cmd.Parameters.AddWithValue(
                "@UserID",
                RoleHelper.GetUserID());

            con.Open();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    ddlMember.Items.Add(
                        new ListItem(
                            dr["MemberName"].ToString(),
                            dr["MemberID"].ToString()));
                }
                else
                {
                    ShowMessage(
                        "Your member profile is not properly assigned.",
                        System.Drawing.Color.Red);
                    return;
                }
            }
        }

        LoadProfile(
            memberID,
            bachatGatID);
    }

    private bool IsMemberInSelectedGat(
        int memberID,
        int bachatGatID)
    {
        string query;

        if (RoleHelper.IsAdmin())
        {
            query = @"
                SELECT COUNT(*)
                FROM Members
                WHERE MemberID = @MemberID
                AND BachatGatID = @BachatGatID";
        }
        else
        {
            query = @"
                SELECT COUNT(*)
                FROM Members
                WHERE MemberID = @MemberID
                AND BachatGatID = @BachatGatID";
        }

        using (SqlConnection con = DBHelper.GetConnection())
        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue(
                "@MemberID",
                memberID);

            cmd.Parameters.AddWithValue(
                "@BachatGatID",
                bachatGatID);

            con.Open();

            return Convert.ToInt32(
                cmd.ExecuteScalar()) > 0;
        }
    }

    private void LoadProfile(
        int memberID,
        int bachatGatID)
    {
        string query = @"
            SELECT
                M.MemberID,
                M.MemberCode,
                M.MemberName,
                M.FatherOrHusbandName,
                M.Gender,
                M.DateOfBirth,
                M.Mobile,
                M.Email,
                M.Address,
                M.Village,
                M.Taluka,
                M.District,
                M.JoinDate,
                M.Occupation,
                M.BankName,
                M.BankAccountNumber,
                M.IFSCCode,
                M.Status,
                B.GatName
            FROM Members M
            LEFT JOIN BachatGat B
                ON M.BachatGatID = B.BachatGatID
            WHERE M.MemberID = @MemberID
            AND M.BachatGatID = @BachatGatID";

        if (RoleHelper.IsMember())
        {
            query += @"
                AND M.UserID = @UserID";
        }

        using (SqlConnection con = DBHelper.GetConnection())
        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue(
                "@MemberID",
                memberID);

            cmd.Parameters.AddWithValue(
                "@BachatGatID",
                bachatGatID);

            if (RoleHelper.IsMember())
            {
                cmd.Parameters.AddWithValue(
                    "@UserID",
                    RoleHelper.GetUserID());
            }

            con.Open();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    lblMemberCode.Text =
                        GetValue(dr["MemberCode"]);

                    lblMemberName.Text =
                        GetValue(dr["MemberName"]);

                    lblFatherHusband.Text =
                        GetValue(dr["FatherOrHusbandName"]);

                    lblGender.Text =
                        GetValue(dr["Gender"]);

                    lblDOB.Text =
                        FormatDate(dr["DateOfBirth"]);

                    lblMobile.Text =
                        GetValue(dr["Mobile"]);

                    lblEmail.Text =
                        GetValue(dr["Email"]);

                    lblGatName.Text =
                        GetValue(dr["GatName"]);

                    lblJoinDate.Text =
                        FormatDate(dr["JoinDate"]);

                    lblOccupation.Text =
                        GetValue(dr["Occupation"]);

                    lblVillage.Text =
                        GetValue(dr["Village"]);

                    lblTaluka.Text =
                        GetValue(dr["Taluka"]);

                    lblDistrict.Text =
                        GetValue(dr["District"]);

                    lblBankName.Text =
                        GetValue(dr["BankName"]);

                    lblAccountNumber.Text =
                        GetValue(dr["BankAccountNumber"]);

                    lblIFSC.Text =
                        GetValue(dr["IFSCCode"]);

                    lblStatus.Text =
                        GetValue(dr["Status"]);

                    LoadSavings(
                        memberID,
                        bachatGatID);

                    LoadLoans(
                        memberID,
                        bachatGatID);

                    LoadRepayments(
                        memberID,
                        bachatGatID);

                    pnlProfile.Visible = true;
                }
                else
                {
                    ShowMessage(
                        "Profile not found.",
                        System.Drawing.Color.Red);

                    pnlProfile.Visible = false;
                }
            }
        }
    }

    private void LoadSavings(
        int memberID,
        int bachatGatID)
    {
        string query = @"
            SELECT
                SavingID,
                SavingMonth,
                Amount,
                PaymentDate,
                PaymentMode,
                ReceiptNumber,
                Remarks
            FROM MemberSavings
            WHERE MemberID = @MemberID
            AND BachatGatID = @BachatGatID
            ORDER BY PaymentDate DESC";

        using (SqlConnection con = DBHelper.GetConnection())
        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue(
                "@MemberID",
                memberID);

            cmd.Parameters.AddWithValue(
                "@BachatGatID",
                bachatGatID);

            using (SqlDataAdapter da =
                new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();

                da.Fill(dt);

                gvSavings.DataSource = dt;
                gvSavings.DataBind();

                decimal totalSavings = 0;

                foreach (DataRow row in dt.Rows)
                {
                    if (row["Amount"] != DBNull.Value)
                    {
                        totalSavings +=
                            Convert.ToDecimal(
                                row["Amount"]);
                    }
                }

                lblTotalSavings.Text =
                    totalSavings.ToString("N2");
            }
        }
    }

    private void LoadLoans(
        int memberID,
        int bachatGatID)
    {
        string query = @"
            SELECT
                LoanID,
                ApplicationDate,
                LoanAmount,
                ApprovedAmount,
                InterestRate,
                LoanPurpose,
                LoanTermMonths,
                EMIAmount,
                ApprovalDate,
                DistributionDate,
                Status
            FROM Loans
            WHERE MemberID = @MemberID
            AND BachatGatID = @BachatGatID
            ORDER BY LoanID DESC";

        using (SqlConnection con = DBHelper.GetConnection())
        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue(
                "@MemberID",
                memberID);

            cmd.Parameters.AddWithValue(
                "@BachatGatID",
                bachatGatID);

            using (SqlDataAdapter da =
                new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();

                da.Fill(dt);

                gvLoans.DataSource = dt;
                gvLoans.DataBind();
            }
        }
    }

    private void LoadRepayments(
        int memberID,
        int bachatGatID)
    {
        string query = @"
            SELECT
                LR.LoanID,
                LR.EMIInstallmentNo,
                LR.DueDate,
                LR.PaymentDate,
                LR.EMIAmount,
                LR.PrincipalAmount,
                LR.InterestAmount,
                LR.PaidAmount,
                LR.PaymentMode,
                LR.ReceiptNumber,
                LR.Status
            FROM LoanRepayments LR
            INNER JOIN Loans L
                ON LR.LoanID = L.LoanID
            WHERE L.MemberID = @MemberID
            AND L.BachatGatID = @BachatGatID
            ORDER BY LR.PaymentDate DESC";

        using (SqlConnection con = DBHelper.GetConnection())
        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue(
                "@MemberID",
                memberID);

            cmd.Parameters.AddWithValue(
                "@BachatGatID",
                bachatGatID);

            using (SqlDataAdapter da =
                new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();

                da.Fill(dt);

                gvRepayments.DataSource = dt;
                gvRepayments.DataBind();
            }
        }
    }

    private string GetValue(object value)
    {
        if (value == null ||
            value == DBNull.Value)
        {
            return "-";
        }

        string text =
            value.ToString().Trim();

        if (text == "")
        {
            return "-";
        }

        return text;
    }

    private string FormatDate(object value)
    {
        if (value == null ||
            value == DBNull.Value)
        {
            return "-";
        }

        return Convert.ToDateTime(value)
            .ToString("dd-MM-yyyy");
    }

    private void ShowMessage(
        string message,
        System.Drawing.Color color)
    {
        lblMessage.Text = message;
        lblMessage.ForeColor = color;
    }
}