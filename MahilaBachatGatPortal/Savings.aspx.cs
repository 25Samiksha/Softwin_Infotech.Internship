using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Savings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RoleHelper.RequirePresidentSecretary(this);
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
                string query =
                    "SELECT BachatGatID, GatName FROM BachatGat WHERE Status = 'Active' ORDER BY GatName";

                SqlDataAdapter da =
                    new SqlDataAdapter(query, con);

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

            ddlBachatGat.Items.Insert(
                0,
                new ListItem(
                    "-- Select Bachat Gat --",
                    ""
                )
            );
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
    // LOAD ALL ACTIVE MEMBERS
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
                        MemberCode + ' - ' + MemberName AS MemberDisplay
                    FROM Members
                    WHERE Status = 'Active'
                    ORDER BY MemberName";

                SqlDataAdapter da =
                    new SqlDataAdapter(query, con);

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
    // LOAD MEMBERS BY BACHAT GAT
    // ==========================================

    protected void ddlBachatGat_SelectedIndexChanged(
        object sender,
        EventArgs e)
    {
        if (ddlBachatGat.SelectedValue == "")
        {
            LoadMembers();

            return;
        }


        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query = @"
                    SELECT
                        MemberID,
                        MemberCode + ' - ' + MemberName AS MemberDisplay
                    FROM Members
                    WHERE
                        BachatGatID = @BachatGatID
                        AND Status = 'Active'
                    ORDER BY MemberName";

                SqlCommand cmd =
                    new SqlCommand(query, con);

                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    ddlBachatGat.SelectedValue
                );

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
    // SAVE SAVINGS
    // ==========================================

    protected void btnSave_Click(
        object sender,
        EventArgs e)
    {
        if (ddlBachatGat.SelectedValue == "")
        {
            ShowMessage(
                "Please select Bachat Gat.",
                System.Drawing.Color.Red
            );

            return;
        }


        if (ddlMember.SelectedValue == "")
        {
            ShowMessage(
                "Please select Member.",
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


        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
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
                    ddlMember.SelectedValue
                );

                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    ddlBachatGat.SelectedValue
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


                con.Open();

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
    // LOAD SAVINGS LIST
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
                    ORDER BY S.SavingID DESC";


                SqlDataAdapter da =
                    new SqlDataAdapter(
                        query,
                        con
                    );


                DataTable dt =
                    new DataTable();


                da.Fill(dt);


                gvSavings.DataSource = dt;

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
                        M.MemberName LIKE @Search
                        OR M.MemberCode LIKE @Search
                        OR S.ReceiptNumber LIKE @Search
                    ORDER BY S.SavingID DESC";


                SqlCommand cmd =
                    new SqlCommand(query, con);


                cmd.Parameters.AddWithValue(
                    "@Search",
                    "%" + txtSearch.Text.Trim() + "%"
                );


                SqlDataAdapter da =
                    new SqlDataAdapter(cmd);


                DataTable dt =
                    new DataTable();


                da.Fill(dt);


                gvSavings.DataSource = dt;

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
    // DELETE SAVINGS
    // ==========================================

    protected void gvSavings_RowCommand(
        object sender,
        GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteSaving")
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
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query =
                    "DELETE FROM MemberSavings WHERE SavingID = @SavingID";


                SqlCommand cmd =
                    new SqlCommand(query, con);


                cmd.Parameters.AddWithValue(
                    "@SavingID",
                    savingID
                );


                con.Open();

                cmd.ExecuteNonQuery();
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
    // LOAD SUMMARY
    // ==========================================

    private void LoadSummary()
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string totalQuery =
                    "SELECT ISNULL(SUM(Amount),0) FROM MemberSavings";


                SqlCommand totalCmd =
                    new SqlCommand(
                        totalQuery,
                        con
                    );


                string countQuery =
                    "SELECT COUNT(*) FROM MemberSavings";


                SqlCommand countCmd =
                    new SqlCommand(
                        countQuery,
                        con
                    );


                string monthQuery = @"
                    SELECT ISNULL(SUM(Amount),0)
                    FROM MemberSavings
                    WHERE
                        MONTH(SavingMonth) = MONTH(GETDATE())
                        AND YEAR(SavingMonth) = YEAR(GETDATE())";


                SqlCommand monthCmd =
                    new SqlCommand(
                        monthQuery,
                        con
                    );


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
    // CLEAR FORM
    // ==========================================

    protected void btnClear_Click(
        object sender,
        EventArgs e)
    {
        ClearForm();
    }


    private void ClearForm()
    {
        ddlBachatGat.SelectedIndex = 0;

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