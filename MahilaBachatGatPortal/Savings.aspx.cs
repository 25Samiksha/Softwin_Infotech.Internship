using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Savings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RoleHelper.RequireLogin(this);
        if (!RoleHelper.IsPresidentOrSecretary() && !RoleHelper.IsMember())
        {
            Response.Redirect("Dashboard.aspx");
            return;
        }

        if (!IsPostBack)
        {
            SetDefaultDates();

            LoadBachatGat();

            LoadMembers();

            LoadSavings();

            LoadSummary();
        }
    }


    // ==========================================
    // DEFAULT DATES
    // ==========================================

    private void SetDefaultDates()
    {
        txtSavingMonth.Text =
            DateTime.Today.ToString("yyyy-MM-dd");

        txtPaymentDate.Text =
            DateTime.Today.ToString("yyyy-MM-dd");
    }


    // ==========================================
    // LOAD BACHAT GAT
    // ==========================================

    private void LoadBachatGat()
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query = @"
                    SELECT
                        BachatGatID,
                        GatName
                    FROM BachatGat
                    WHERE Status = 'Active'
                    AND BachatGatID = @BachatGatID
                    ORDER BY GatName";

                SqlCommand cmd =
                    new SqlCommand(query, con);

                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    RoleHelper.GetBachatGatID()
                );

                SqlDataAdapter da =
                    new SqlDataAdapter(cmd);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                ddlBachatGat.DataSource = dt;

                ddlBachatGat.DataTextField =
                    "GatName";

                ddlBachatGat.DataValueField =
                    "BachatGatID";

                ddlBachatGat.DataBind();
            }

            // President/Secretary have only one
            // Bachat Gat, so no "Select" option.
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error loading Bachat Gat: " + ex.Message,
                System.Drawing.Color.Red
            );
        }
    }


    // ==========================================
    // LOAD MEMBERS
    // ==========================================

    private void LoadMembers()
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query = @"
                    SELECT
                        MemberID,
                        MemberCode + ' - ' + MemberName
                        AS MemberDisplay
                    FROM Members
                    WHERE
                        BachatGatID = @BachatGatID
                        AND Status = 'Active'";

                if (RoleHelper.IsMember())
                {
                    query += " AND MemberID = @MemberID";
                }

                query += " ORDER BY MemberName";

                SqlCommand cmd =
                    new SqlCommand(query, con);

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

                SqlDataAdapter da =
                    new SqlDataAdapter(cmd);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                ddlMember.DataSource = dt;

                ddlMember.DataTextField =
                    "MemberDisplay";

                ddlMember.DataValueField =
                    "MemberID";

                ddlMember.DataBind();
            }

            ddlMember.Items.Insert(
                0,
                new ListItem(
                    "-- Select Member --",
                    ""
                )
            );
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error loading members: " + ex.Message,
                System.Drawing.Color.Red
            );
        }
    }


    // ==========================================
    // BACHAT GAT CHANGED
    // ==========================================

    protected void ddlBachatGat_SelectedIndexChanged(
        object sender,
        EventArgs e)
    {
        // President / Secretary cannot switch
        // to another Bachat Gat.

        if (ddlBachatGat.SelectedValue !=
            RoleHelper.GetBachatGatID().ToString())
        {
            ShowMessage(
                "You cannot select another Bachat Gat.",
                System.Drawing.Color.Red
            );

            ddlBachatGat.SelectedValue =
                RoleHelper.GetBachatGatID().ToString();

            LoadMembers();

            return;
        }

        LoadMembers();
    }


    // ==========================================
    // SAVE SAVINGS
    // ==========================================

    protected void btnSave_Click(
        object sender,
        EventArgs e)
    {
        if (ddlMember.SelectedValue == "")
        {
            ShowMessage(
                "Please select Member.",
                System.Drawing.Color.Red
            );

            return;
        }

        if (RoleHelper.IsMember() && ddlMember.SelectedValue != RoleHelper.GetMemberID().ToString())
        {
            ShowMessage(
                "You can only manage your own savings.",
                System.Drawing.Color.Red
            );

            return;
        }

        if (string.IsNullOrWhiteSpace(
            txtSavingMonth.Text))
        {
            ShowMessage(
                "Please select Saving Month.",
                System.Drawing.Color.Red
            );

            return;
        }

        decimal amount;

        if (!decimal.TryParse(
            txtAmount.Text.Trim(),
            out amount))
        {
            ShowMessage(
                "Please enter a valid amount.",
                System.Drawing.Color.Red
            );

            return;
        }

        if (amount <= 0)
        {
            ShowMessage(
                "Amount must be greater than zero.",
                System.Drawing.Color.Red
            );

            return;
        }


        int bachatGatID =
            RoleHelper.GetBachatGatID();

        int memberID =
            Convert.ToInt32(
                ddlMember.SelectedValue
            );


        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                con.Open();


                // ==================================
                // VERIFY MEMBER BELONGS TO GAT
                // ==================================

                string checkQuery = @"
                    SELECT COUNT(*)
                    FROM Members
                    WHERE
                        MemberID = @MemberID
                        AND BachatGatID = @BachatGatID
                        AND Status = 'Active'";

                SqlCommand checkCmd =
                    new SqlCommand(
                        checkQuery,
                        con
                    );

                checkCmd.Parameters.AddWithValue(
                    "@MemberID",
                    memberID
                );

                checkCmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    bachatGatID
                );

                int memberExists =
                    Convert.ToInt32(
                        checkCmd.ExecuteScalar()
                    );


                if (memberExists == 0)
                {
                    ShowMessage(
                        "Invalid member selection.",
                        System.Drawing.Color.Red
                    );

                    return;
                }


                // ==================================
                // INSERT SAVINGS
                // ==================================

                string query = @"
                    INSERT INTO MemberSavings
                    (
                        MemberID,
                        BachatGatID,
                        SavingMonth,
                        Amount,
                        PaymentDate,
                        PaymentMode,
                        ReceiptNumber,
                        Remarks
                    )
                    VALUES
                    (
                        @MemberID,
                        @BachatGatID,
                        @SavingMonth,
                        @Amount,
                        @PaymentDate,
                        @PaymentMode,
                        @ReceiptNumber,
                        @Remarks
                    )";


                SqlCommand cmd =
                    new SqlCommand(query, con);


                cmd.Parameters.AddWithValue(
                    "@MemberID",
                    memberID
                );

                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    bachatGatID
                );

                cmd.Parameters.AddWithValue(
                    "@SavingMonth",
                    Convert.ToDateTime(
                        txtSavingMonth.Text
                    )
                );

                cmd.Parameters.AddWithValue(
                    "@Amount",
                    amount
                );


                if (string.IsNullOrWhiteSpace(
                    txtPaymentDate.Text))
                {
                    cmd.Parameters.AddWithValue(
                        "@PaymentDate",
                        DBNull.Value
                    );
                }
                else
                {
                    cmd.Parameters.AddWithValue(
                        "@PaymentDate",
                        Convert.ToDateTime(
                            txtPaymentDate.Text
                        )
                    );
                }


                cmd.Parameters.AddWithValue(
                    "@PaymentMode",
                    ddlPaymentMode.SelectedValue
                );

                cmd.Parameters.AddWithValue(
                    "@ReceiptNumber",
                    txtReceiptNumber.Text.Trim()
                );

                cmd.Parameters.AddWithValue(
                    "@Remarks",
                    txtRemarks.Text.Trim()
                );


                cmd.ExecuteNonQuery();
            }


            ShowMessage(
                "Savings recorded successfully!",
                System.Drawing.Color.Green
            );


            ClearForm();

            LoadSavings();

            LoadSummary();
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error saving savings: " + ex.Message,
                System.Drawing.Color.Red
            );
        }
    }


    // ==========================================
    // LOAD SAVINGS
    // ==========================================

    private void LoadSavings()
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query = @"
                    SELECT
                        S.SavingID,
                        M.MemberCode,
                        M.MemberName,
                        B.GatName,
                        S.SavingMonth,
                        S.Amount,
                        S.PaymentDate,
                        S.PaymentMode,
                        S.ReceiptNumber
                    FROM MemberSavings S
                    INNER JOIN Members M
                        ON S.MemberID = M.MemberID
                    INNER JOIN BachatGat B
                        ON S.BachatGatID = B.BachatGatID
                    WHERE
                        S.BachatGatID = @BachatGatID";

                if (RoleHelper.IsMember())
                {
                    query += " AND S.MemberID = @MemberID";
                }

                query += " ORDER BY S.SavingID DESC";


                SqlCommand cmd =
                    new SqlCommand(
                        query,
                        con
                    );

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


                SqlDataAdapter da =
                    new SqlDataAdapter(cmd);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);


                gvSavings.DataSource =
                    dt;

                gvSavings.DataBind();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error loading savings: " + ex.Message,
                System.Drawing.Color.Red
            );
        }
    }


    // ==========================================
    // SEARCH
    // ==========================================

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
                        S.SavingID,
                        M.MemberCode,
                        M.MemberName,
                        B.GatName,
                        S.SavingMonth,
                        S.Amount,
                        S.PaymentDate,
                        S.PaymentMode,
                        S.ReceiptNumber
                    FROM MemberSavings S
                    INNER JOIN Members M
                        ON S.MemberID = M.MemberID
                    INNER JOIN BachatGat B
                        ON S.BachatGatID = B.BachatGatID
                    WHERE
                        S.BachatGatID = @BachatGatID
                        AND
                        (
                            M.MemberName LIKE @Search
                            OR M.MemberCode LIKE @Search
                            OR S.ReceiptNumber LIKE @Search
                        )";

                if (RoleHelper.IsMember())
                {
                    query += " AND S.MemberID = @MemberID";
                }

                query += " ORDER BY S.SavingID DESC";


                SqlCommand cmd =
                    new SqlCommand(
                        query,
                        con
                    );


                cmd.Parameters.AddWithValue(
                    "@Search",
                    "%" + txtSearch.Text.Trim() + "%"
                );


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


                SqlDataAdapter da =
                    new SqlDataAdapter(cmd);


                DataTable dt =
                    new DataTable();


                da.Fill(dt);


                gvSavings.DataSource =
                    dt;

                gvSavings.DataBind();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Search error: " + ex.Message,
                System.Drawing.Color.Red
            );
        }
    }


    // ==========================================
    // SHOW ALL
    // ==========================================

    protected void btnShowAll_Click(
        object sender,
        EventArgs e)
    {
        txtSearch.Text = "";

        LoadSavings();
    }


    // ==========================================
    // DELETE
    // ==========================================

    protected void gvSavings_RowCommand(
        object sender,
        GridViewCommandEventArgs e)
    {
        if (e.CommandName ==
            "DeleteSaving")
        {
            int savingID =
                Convert.ToInt32(
                    e.CommandArgument
                );

            DeleteSaving(savingID);
        }
    }


    private void DeleteSaving(int savingID)
    {
        if (RoleHelper.IsMember())
        {
            ShowMessage("Members cannot delete savings.", System.Drawing.Color.Red);
            return;
        }

        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query = @"
                    DELETE FROM MemberSavings
                    WHERE
                        SavingID = @SavingID
                        AND BachatGatID = @BachatGatID";


                SqlCommand cmd =
                    new SqlCommand(
                        query,
                        con
                    );


                cmd.Parameters.AddWithValue(
                    "@SavingID",
                    savingID
                );


                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    RoleHelper.GetBachatGatID()
                );


                con.Open();


                int rows =
                    cmd.ExecuteNonQuery();


                if (rows == 0)
                {
                    ShowMessage(
                        "Savings record not found or you do not have permission to delete it.",
                        System.Drawing.Color.Red
                    );

                    return;
                }
            }


            ShowMessage(
                "Savings record deleted successfully!",
                System.Drawing.Color.Green
            );


            LoadSavings();

            LoadSummary();
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error deleting savings: " + ex.Message,
                System.Drawing.Color.Red
            );
        }
    }


    // ==========================================
    // SUMMARY
    // ==========================================

    private void LoadSummary()
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string totalQuery = @"
                    SELECT ISNULL(SUM(Amount), 0)
                    FROM MemberSavings
                    WHERE BachatGatID = @BachatGatID";

                string countQuery = @"
                    SELECT COUNT(*)
                    FROM MemberSavings
                    WHERE BachatGatID = @BachatGatID";

                string monthQuery = @"
                    SELECT ISNULL(SUM(Amount), 0)
                    FROM MemberSavings
                    WHERE
                        BachatGatID = @BachatGatID
                        AND MONTH(SavingMonth) =
                            MONTH(GETDATE())
                        AND YEAR(SavingMonth) =
                            YEAR(GETDATE())";

                if (RoleHelper.IsMember())
                {
                    totalQuery += " AND MemberID = @MemberID";
                    countQuery += " AND MemberID = @MemberID";
                    monthQuery += " AND MemberID = @MemberID";
                }

                SqlCommand totalCmd =
                    new SqlCommand(
                        totalQuery,
                        con
                    );

                SqlCommand countCmd =
                    new SqlCommand(
                        countQuery,
                        con
                    );

                SqlCommand monthCmd =
                    new SqlCommand(
                        monthQuery,
                        con
                    );

                totalCmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    RoleHelper.GetBachatGatID()
                );

                countCmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    RoleHelper.GetBachatGatID()
                );

                monthCmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    RoleHelper.GetBachatGatID()
                );

                if (RoleHelper.IsMember())
                {
                    totalCmd.Parameters.AddWithValue("@MemberID", RoleHelper.GetMemberID());
                    countCmd.Parameters.AddWithValue("@MemberID", RoleHelper.GetMemberID());
                    monthCmd.Parameters.AddWithValue("@MemberID", RoleHelper.GetMemberID());
                }


                con.Open();


                decimal total =
                    Convert.ToDecimal(
                        totalCmd.ExecuteScalar()
                    );


                int count =
                    Convert.ToInt32(
                        countCmd.ExecuteScalar()
                    );


                decimal currentMonth =
                    Convert.ToDecimal(
                        monthCmd.ExecuteScalar()
                    );


                lblTotalSavings.Text =
                    total.ToString("N2");


                lblTotalRecords.Text =
                    count.ToString();


                lblCurrentMonth.Text =
                    currentMonth.ToString("N2");
            }
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error loading summary: " + ex.Message,
                System.Drawing.Color.Red
            );
        }
    }


    // ==========================================
    // CLEAR BUTTON
    // ==========================================

    protected void btnClear_Click(
        object sender,
        EventArgs e)
    {
        ClearForm();
    }


    private void ClearForm()
    {
        // Do NOT allow changing Bachat Gat.
        ddlBachatGat.SelectedValue =
            RoleHelper.GetBachatGatID().ToString();

        LoadMembers();

        ddlMember.SelectedIndex = 0;

        txtSavingMonth.Text =
            DateTime.Today.ToString("yyyy-MM-dd");

        txtAmount.Text = "";

        txtPaymentDate.Text =
            DateTime.Today.ToString("yyyy-MM-dd");

        ddlPaymentMode.SelectedIndex = 0;

        txtReceiptNumber.Text = "";

        txtRemarks.Text = "";
    }


    // ==========================================
    // MESSAGE
    // ==========================================

    private void ShowMessage(
        string message,
        System.Drawing.Color color)
    {
        lblMessage.Text = message;

        lblMessage.ForeColor = color;
    }
}