using System;
using System.Data;
using System.Data.SqlClient;

public partial class Income : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RoleHelper.RequirePresidentSecretary(this);
        if (!IsPostBack)
        {
            LoadBachatGats();

            txtIncomeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            LoadIncome();
            LoadSummary();
        }
    }


    // =====================================================
    // LOAD BACHAT GATS
    // =====================================================

    private void LoadBachatGats()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT BachatGatID, GatName
                FROM BachatGat
                WHERE Status = 'Active'
                ORDER BY GatName";

            SqlDataAdapter da = new SqlDataAdapter(query, con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            ddlBachatGat.DataSource = dt;
            ddlBachatGat.DataTextField = "GatName";
            ddlBachatGat.DataValueField = "BachatGatID";
            ddlBachatGat.DataBind();

            ddlBachatGat.Items.Insert(
                0,
                new System.Web.UI.WebControls.ListItem(
                    "-- Select Bachat Gat --",
                    "")
            );
        }
    }


    // =====================================================
    // LOAD INCOME
    // =====================================================

    private void LoadIncome()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    I.IncomeID,
                    B.GatName,
                    I.IncomeDate,
                    I.IncomeType,
                    I.Amount,
                    I.ReceivedFrom,
                    I.PaymentMode,
                    I.ReceiptNumber
                FROM Income I
                INNER JOIN BachatGat B
                    ON I.BachatGatID = B.BachatGatID
                ORDER BY I.IncomeDate DESC, I.IncomeID DESC";

            SqlDataAdapter da =
                new SqlDataAdapter(query, con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvIncome.DataSource = dt;
            gvIncome.DataBind();
        }
    }


    // =====================================================
    // SAVE INCOME
    // =====================================================

    protected void btnSave_Click(object sender, EventArgs e)
    {
        decimal amount;

        if (ddlBachatGat.SelectedValue == "")
        {
            ShowMessage("Please select Bachat Gat.", "alert-danger");
            return;
        }

        if (ddlIncomeType.SelectedValue == "")
        {
            ShowMessage("Please select income type.", "alert-danger");
            return;
        }
        DateTime incomeDate;

        if (!DateTime.TryParse(
            txtIncomeDate.Text,
            out incomeDate))
        {
            ShowMessage("Please enter valid income date.", "alert-danger");
            return;
        }

        if (!decimal.TryParse(
            txtAmount.Text,
            out amount) || amount <= 0)
        {
            ShowMessage("Please enter valid amount.", "alert-danger");
            return;
        }


        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                INSERT INTO Income
                (
                    BachatGatID,
                    IncomeDate,
                    IncomeType,
                    Amount,
                    Description,
                    ReceivedFrom,
                    PaymentMode,
                    ReceiptNumber
                    
                )
                VALUES
                (
                    @BachatGatID,
                    @IncomeDate,
                    @IncomeType,
                    @Amount,
                    @Description,
                    @ReceivedFrom,
                    @PaymentMode,
                    @ReceiptNumber
                    
                )";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@BachatGatID",
                ddlBachatGat.SelectedValue);

            cmd.Parameters.AddWithValue(
                "@IncomeDate",
                incomeDate);

            cmd.Parameters.AddWithValue(
                "@IncomeType",
                ddlIncomeType.SelectedValue);

            cmd.Parameters.AddWithValue(
                "@Amount",
                amount);

            cmd.Parameters.AddWithValue(
                "@Description",
                txtDescription.Text.Trim());

            cmd.Parameters.AddWithValue(
                "@ReceivedFrom",
                txtReceivedFrom.Text.Trim());

            cmd.Parameters.AddWithValue(
                "@PaymentMode",
                ddlPaymentMode.SelectedValue);

            cmd.Parameters.AddWithValue(
                "@ReceiptNumber",
                txtReceiptNumber.Text.Trim());

           

            con.Open();

            cmd.ExecuteNonQuery();
        }


        ShowMessage(
            "Income entry saved successfully.",
            "alert-success");

        ClearForm();

        LoadIncome();
        LoadSummary();
    }


    // =====================================================
    // DELETE
    // =====================================================

    protected void gvIncome_RowCommand(
        object sender,
        System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteIncome")
        {
            int incomeID =
                Convert.ToInt32(e.CommandArgument);

            DeleteIncome(incomeID);
        }
    }


    private void DeleteIncome(int incomeID)
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                DELETE FROM Income
                WHERE IncomeID = @IncomeID";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@IncomeID",
                incomeID);

            con.Open();

            cmd.ExecuteNonQuery();
        }

        ShowMessage(
            "Income entry deleted successfully.",
            "alert-success");

        LoadIncome();
        LoadSummary();
    }


    // =====================================================
    // SEARCH
    // =====================================================

    protected void btnSearch_Click(
        object sender,
        EventArgs e)
    {
        string search =
            txtSearch.Text.Trim();

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    I.IncomeID,
                    B.GatName,
                    I.IncomeDate,
                    I.IncomeType,
                    I.Amount,
                    I.ReceivedFrom,
                    I.PaymentMode,
                    I.ReceiptNumber
                FROM Income I
                INNER JOIN BachatGat B
                    ON I.BachatGatID = B.BachatGatID
                WHERE
                    I.IncomeType LIKE @Search
                    OR I.ReceivedFrom LIKE @Search
                    OR I.ReceiptNumber LIKE @Search
                ORDER BY
                    I.IncomeDate DESC,
                    I.IncomeID DESC";

            SqlDataAdapter da =
                new SqlDataAdapter(query, con);

            da.SelectCommand.Parameters.AddWithValue(
                "@Search",
                "%" + search + "%");

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvIncome.DataSource = dt;
            gvIncome.DataBind();
        }
    }


    // =====================================================
    // SHOW ALL
    // =====================================================

    protected void btnShowAll_Click(
        object sender,
        EventArgs e)
    {
        txtSearch.Text = "";

        LoadIncome();
    }


    // =====================================================
    // SUMMARY
    // =====================================================

    private void LoadSummary()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    ISNULL(SUM(Amount), 0) AS TotalIncome,
                    COUNT(*) AS TotalRecords,
                    ISNULL(
                        SUM(
                            CASE
                                WHEN MONTH(IncomeDate) = MONTH(GETDATE())
                                AND YEAR(IncomeDate) = YEAR(GETDATE())
                                THEN Amount
                                ELSE 0
                            END
                        ), 0
                    ) AS CurrentMonthIncome
                FROM Income";

            SqlCommand cmd =
                new SqlCommand(query, con);

            con.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();

            if (dr.Read())
            {
                lblTotalIncome.Text =
                    Convert.ToDecimal(
                        dr["TotalIncome"])
                    .ToString("N2");

                lblTotalRecords.Text =
                    dr["TotalRecords"].ToString();

                lblCurrentMonthIncome.Text =
                    Convert.ToDecimal(
                        dr["CurrentMonthIncome"])
                    .ToString("N2");
            }
        }
    }


    // =====================================================
    // CLEAR FORM
    // =====================================================

    protected void btnClear_Click(
        object sender,
        EventArgs e)
    {
        ClearForm();
    }


    private void ClearForm()
    {
        hfIncomeID.Value = "";

        ddlBachatGat.SelectedIndex = 0;

        ddlIncomeType.SelectedIndex = 0;

        txtIncomeDate.Text =
            DateTime.Now.ToString("yyyy-MM-dd");

        txtAmount.Text = "";

        txtDescription.Text = "";

        txtReceivedFrom.Text = "";

        ddlPaymentMode.SelectedIndex = 0;

        txtReceiptNumber.Text = "";

        txtRemarks.Text = "";

        btnSave.Text = "Save Income";
    }


    // =====================================================
    // MESSAGE
    // =====================================================

    private void ShowMessage(
        string message,
        string cssClass)
    {
        lblMessage.Text = message;

        lblMessage.CssClass =
            "alert " + cssClass;

        lblMessage.Visible = true;
    }
}