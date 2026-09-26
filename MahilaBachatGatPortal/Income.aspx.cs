using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Income : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtIncomeDate.Attributes["type"] = "date";

        RoleHelper.RequirePresidentSecretary(this);

        if (!IsPostBack)
        {
            LoadBachatGats();

            txtIncomeDate.Text =
                DateTime.Now.ToString("yyyy-MM-dd");

            LoadIncome();
            LoadSummary();
        }
    }

    private bool IsAdmin()
    {
        return Session["Role"] != null &&
               Session["Role"].ToString().Equals(
                   "Admin",
                   StringComparison.OrdinalIgnoreCase
               );
    }

    private int GetBachatGatID()
    {
        return RoleHelper.GetBachatGatID();
    }

    private void LoadBachatGats()
    {
        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query;

            if (IsAdmin())
            {
                query = @"
                    SELECT
                        BachatGatID,
                        GatName
                    FROM BachatGat
                    WHERE Status = 'Active'
                    ORDER BY GatName";
            }
            else
            {
                query = @"
                    SELECT
                        BachatGatID,
                        GatName
                    FROM BachatGat
                    WHERE Status = 'Active'
                    AND BachatGatID = @BachatGatID
                    ORDER BY GatName";
            }

            SqlCommand cmd =
                new SqlCommand(
                    query,
                    con
                );

            if (!IsAdmin())
            {
                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    GetBachatGatID()
                );
            }

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

            if (IsAdmin())
            {
                ddlBachatGat.Items.Insert(
                    0,
                    new ListItem(
                        "-- Select Bachat Gat --",
                        ""
                    )
                );
            }
        }
    }

    private void LoadIncome()
    {
        using (SqlConnection con =
            DBHelper.GetConnection())
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
                    ON I.BachatGatID =
                       B.BachatGatID
                WHERE 1 = 1";

            if (!IsAdmin())
            {
                query +=
                    " AND I.BachatGatID = @BachatGatID";
            }

            query += @"
                ORDER BY
                    I.IncomeDate DESC,
                    I.IncomeID DESC";

            SqlCommand cmd =
                new SqlCommand(
                    query,
                    con
                );

            if (!IsAdmin())
            {
                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    GetBachatGatID()
                );
            }

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            gvIncome.DataSource = dt;

            gvIncome.DataBind();
        }
    }

    protected void btnSave_Click(
        object sender,
        EventArgs e)
    {
        decimal amount;

        if (ddlBachatGat.SelectedValue == "")
        {
            ShowMessage(
                "Please select Bachat Gat.",
                "alert-danger"
            );

            return;
        }

        if (!IsAdmin())
        {
            if (ddlBachatGat.SelectedValue !=
                GetBachatGatID().ToString())
            {
                ShowMessage(
                    "You can manage income only for your Bachat Gat.",
                    "alert-danger"
                );

                return;
            }
        }

        if (ddlIncomeType.SelectedValue == "")
        {
            ShowMessage(
                "Please select income type.",
                "alert-danger"
            );

            return;
        }

        DateTime incomeDate;

        if (!DateTime.TryParse(
            txtIncomeDate.Text,
            out incomeDate))
        {
            ShowMessage(
                "Please enter valid income date.",
                "alert-danger"
            );

            return;
        }

        if (!decimal.TryParse(
            txtAmount.Text,
            out amount) ||
            amount <= 0)
        {
            ShowMessage(
                "Please enter valid amount.",
                "alert-danger"
            );

            return;
        }

        int bachatGatID;

        if (IsAdmin())
        {
            bachatGatID =
                Convert.ToInt32(
                    ddlBachatGat.SelectedValue
                );
        }
        else
        {
            bachatGatID =
                GetBachatGatID();
        }

        using (SqlConnection con =
            DBHelper.GetConnection())
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
                new SqlCommand(
                    query,
                    con
                );

            cmd.Parameters.AddWithValue(
                "@BachatGatID",
                bachatGatID
            );

            cmd.Parameters.AddWithValue(
                "@IncomeDate",
                incomeDate
            );

            cmd.Parameters.AddWithValue(
                "@IncomeType",
                ddlIncomeType.SelectedValue
            );

            cmd.Parameters.AddWithValue(
                "@Amount",
                amount
            );

            cmd.Parameters.AddWithValue(
                "@Description",
                txtDescription.Text.Trim()
            );

            cmd.Parameters.AddWithValue(
                "@ReceivedFrom",
                txtReceivedFrom.Text.Trim()
            );

            cmd.Parameters.AddWithValue(
                "@PaymentMode",
                ddlPaymentMode.SelectedValue
            );

            cmd.Parameters.AddWithValue(
                "@ReceiptNumber",
                txtReceiptNumber.Text.Trim()
            );

            con.Open();

            cmd.ExecuteNonQuery();
        }

        ShowMessage(
            "Income entry saved successfully.",
            "alert-success"
        );

        ClearForm();

        LoadIncome();
        LoadSummary();
    }

    protected void gvIncome_RowCommand(
        object sender,
        GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteIncome")
        {
            int incomeID =
                Convert.ToInt32(
                    e.CommandArgument
                );

            DeleteIncome(incomeID);
        }
    }

    private void DeleteIncome(
        int incomeID)
    {
        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                DELETE FROM Income
                WHERE IncomeID = @IncomeID";

            if (!IsAdmin())
            {
                query +=
                    " AND BachatGatID = @BachatGatID";
            }

            SqlCommand cmd =
                new SqlCommand(
                    query,
                    con
                );

            cmd.Parameters.AddWithValue(
                "@IncomeID",
                incomeID
            );

            if (!IsAdmin())
            {
                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    GetBachatGatID()
                );
            }

            con.Open();

            int rows =
                cmd.ExecuteNonQuery();

            if (rows == 0)
            {
                ShowMessage(
                    "You cannot delete this income record.",
                    "alert-danger"
                );

                return;
            }
        }

        ShowMessage(
            "Income entry deleted successfully.",
            "alert-success"
        );

        LoadIncome();
        LoadSummary();
    }

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
                    ON I.BachatGatID =
                       B.BachatGatID
                WHERE
                    (
                        I.IncomeType LIKE @Search
                        OR I.ReceivedFrom LIKE @Search
                        OR I.ReceiptNumber LIKE @Search
                    )";

            if (!IsAdmin())
            {
                query +=
                    " AND I.BachatGatID = @BachatGatID";
            }

            query += @"
                ORDER BY
                    I.IncomeDate DESC,
                    I.IncomeID DESC";

            SqlCommand cmd =
                new SqlCommand(
                    query,
                    con
                );

            cmd.Parameters.AddWithValue(
                "@Search",
                "%" +
                search +
                "%"
            );

            if (!IsAdmin())
            {
                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    GetBachatGatID()
                );
            }

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            gvIncome.DataSource = dt;

            gvIncome.DataBind();
        }
    }

    protected void btnShowAll_Click(
        object sender,
        EventArgs e)
    {
        txtSearch.Text = "";

        LoadIncome();
    }

    private void LoadSummary()
    {
        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    ISNULL(SUM(Amount), 0)
                        AS TotalIncome,
                    COUNT(*)
                        AS TotalRecords,
                    ISNULL(
                        SUM(
                            CASE
                                WHEN MONTH(IncomeDate)
                                    = MONTH(GETDATE())
                                AND YEAR(IncomeDate)
                                    = YEAR(GETDATE())
                                THEN Amount
                                ELSE 0
                            END
                        ),
                        0
                    )
                        AS CurrentMonthIncome
                FROM Income
                WHERE 1 = 1";

            if (!IsAdmin())
            {
                query +=
                    " AND BachatGatID = @BachatGatID";
            }

            SqlCommand cmd =
                new SqlCommand(
                    query,
                    con
                );

            if (!IsAdmin())
            {
                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    GetBachatGatID()
                );
            }

            con.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();

            if (dr.Read())
            {
                lblTotalIncome.Text =
                    Convert.ToDecimal(
                        dr["TotalIncome"]
                    ).ToString("N2");

                lblTotalRecords.Text =
                    dr["TotalRecords"].ToString();

                lblCurrentMonthIncome.Text =
                    Convert.ToDecimal(
                        dr["CurrentMonthIncome"]
                    ).ToString("N2");
            }

            dr.Close();
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
        hfIncomeID.Value = "";

        if (ddlBachatGat.Items.Count > 0)
        {
            if (IsAdmin())
            {
                ddlBachatGat.SelectedIndex = 0;
            }
            else
            {
                ddlBachatGat.SelectedValue =
                    GetBachatGatID().ToString();
            }
        }

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