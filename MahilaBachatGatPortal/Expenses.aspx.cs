using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Expenses : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtExpenseDate.Attributes["type"] = "date";

        RoleHelper.RequirePresidentSecretary(this);

        if (!IsPostBack)
        {
            LoadBachatGats();

            txtExpenseDate.Text =
                DateTime.Now.ToString("yyyy-MM-dd");

            LoadExpenses();
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
        using (SqlConnection con = DBHelper.GetConnection())
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
                new SqlCommand(query, con);

            if (!IsAdmin())
            {
                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    GetBachatGatID());
            }

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            ddlBachatGat.DataSource = dt;
            ddlBachatGat.DataTextField = "GatName";
            ddlBachatGat.DataValueField = "BachatGatID";
            ddlBachatGat.DataBind();

            if (IsAdmin())
            {
                ddlBachatGat.Items.Insert(
                    0,
                    new ListItem(
                        " Select Bachat Gat ",
                        "")
                );
            }
        }
    }

    private void LoadExpenses()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    E.ExpenseID,
                    B.GatName,
                    E.ExpenseDate,
                    E.ExpenseType,
                    E.Amount,
                    E.PaidTo,
                    E.PaymentMode,
                    E.ReceiptNumber
                FROM Expenses E
                INNER JOIN BachatGat B
                    ON E.BachatGatID = B.BachatGatID
                WHERE 1 = 1";

            if (!IsAdmin())
            {
                query +=
                    " AND E.BachatGatID = @BachatGatID";
            }

            query += @"
                ORDER BY
                    E.ExpenseDate DESC,
                    E.ExpenseID DESC";

            SqlCommand cmd =
                new SqlCommand(query, con);

            if (!IsAdmin())
            {
                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    GetBachatGatID());
            }

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            gvExpenses.DataSource = dt;
            gvExpenses.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        decimal amount;

        if (ddlBachatGat.SelectedValue == "")
        {
            ShowMessage(
                "Please select Bachat Gat.",
                "alert-danger");

            return;
        }

        if (!IsAdmin())
        {
            if (ddlBachatGat.SelectedValue !=
                GetBachatGatID().ToString())
            {
                ShowMessage(
                    "You can manage expenses only for your Bachat Gat.",
                    "alert-danger");

                return;
            }
        }

        if (ddlExpenseType.SelectedValue == "")
        {
            ShowMessage(
                "Please select expense type.",
                "alert-danger");

            return;
        }

        DateTime expenseDate;

        if (!DateTime.TryParse(
            txtExpenseDate.Text,
            out expenseDate))
        {
            ShowMessage(
                "Please enter valid expense date.",
                "alert-danger");

            return;
        }

        if (!decimal.TryParse(
            txtAmount.Text,
            out amount) || amount <= 0)
        {
            ShowMessage(
                "Please enter valid amount.",
                "alert-danger");

            return;
        }

        int bachatGatID;

        if (IsAdmin())
        {
            bachatGatID =
                Convert.ToInt32(
                    ddlBachatGat.SelectedValue);
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
                INSERT INTO Expenses
                (
                    BachatGatID,
                    ExpenseDate,
                    ExpenseType,
                    Amount,
                    Description,
                    PaidTo,
                    PaymentMode,
                    ReceiptNumber
                )
                VALUES
                (
                    @BachatGatID,
                    @ExpenseDate,
                    @ExpenseType,
                    @Amount,
                    @Description,
                    @PaidTo,
                    @PaymentMode,
                    @ReceiptNumber
                )";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@BachatGatID",
                bachatGatID);

            cmd.Parameters.AddWithValue(
                "@ExpenseDate",
                expenseDate);

            cmd.Parameters.AddWithValue(
                "@ExpenseType",
                ddlExpenseType.SelectedValue);

            cmd.Parameters.AddWithValue(
                "@Amount",
                amount);

            cmd.Parameters.AddWithValue(
                "@Description",
                txtDescription.Text.Trim());

            cmd.Parameters.AddWithValue(
                "@PaidTo",
                txtPaidTo.Text.Trim());

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
            "Expense entry saved successfully.",
            "alert-success");

        ClearForm();

        LoadExpenses();
        LoadSummary();
    }

    protected void gvExpenses_RowCommand(
        object sender,
        GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteExpense")
        {
            int expenseID =
                Convert.ToInt32(
                    e.CommandArgument);

            DeleteExpense(expenseID);
        }
    }

    private void DeleteExpense(int expenseID)
    {
        using (SqlConnection con =
               DBHelper.GetConnection())
        {
            string query = @"
                DELETE FROM Expenses
                WHERE ExpenseID = @ExpenseID";

            if (!IsAdmin())
            {
                query +=
                    " AND BachatGatID = @BachatGatID";
            }

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@ExpenseID",
                expenseID);

            if (!IsAdmin())
            {
                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    GetBachatGatID());
            }

            con.Open();

            int rows =
                cmd.ExecuteNonQuery();

            if (rows == 0)
            {
                ShowMessage(
                    "You cannot delete this expense record.",
                    "alert-danger");

                return;
            }
        }

        ShowMessage(
            "Expense entry deleted successfully.",
            "alert-success");

        LoadExpenses();
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
                    E.ExpenseID,
                    B.GatName,
                    E.ExpenseDate,
                    E.ExpenseType,
                    E.Amount,
                    E.PaidTo,
                    E.PaymentMode,
                    E.ReceiptNumber
                FROM Expenses E
                INNER JOIN BachatGat B
                    ON E.BachatGatID = B.BachatGatID
                WHERE
                    (
                        E.ExpenseType LIKE @Search
                        OR E.PaidTo LIKE @Search
                        OR E.ReceiptNumber LIKE @Search
                    )";

            if (!IsAdmin())
            {
                query +=
                    " AND E.BachatGatID = @BachatGatID";
            }

            query += @"
                ORDER BY
                    E.ExpenseDate DESC,
                    E.ExpenseID DESC";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@Search",
                "%" + search + "%");

            if (!IsAdmin())
            {
                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    GetBachatGatID());
            }

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            gvExpenses.DataSource = dt;
            gvExpenses.DataBind();
        }
    }

    protected void btnShowAll_Click(
        object sender,
        EventArgs e)
    {
        txtSearch.Text = "";

        LoadExpenses();
    }

    private void LoadSummary()
    {
        using (SqlConnection con =
               DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    ISNULL(
                        SUM(Amount),
                        0
                    ) AS TotalExpenses,

                    COUNT(*) AS TotalRecords,

                    ISNULL(
                        SUM(
                            CASE
                                WHEN MONTH(ExpenseDate)
                                    = MONTH(GETDATE())
                                AND YEAR(ExpenseDate)
                                    = YEAR(GETDATE())
                                THEN Amount
                                ELSE 0
                            END
                        ),
                        0
                    ) AS CurrentMonthExpenses

                FROM Expenses
                WHERE 1 = 1";

            if (!IsAdmin())
            {
                query +=
                    " AND BachatGatID = @BachatGatID";
            }

            SqlCommand cmd =
                new SqlCommand(query, con);

            if (!IsAdmin())
            {
                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    GetBachatGatID());
            }

            con.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();

            if (dr.Read())
            {
                lblTotalExpenses.Text =
                    Convert.ToDecimal(
                        dr["TotalExpenses"])
                    .ToString("N2");

                lblTotalRecords.Text =
                    dr["TotalRecords"]
                    .ToString();

                lblCurrentMonthExpenses.Text =
                    Convert.ToDecimal(
                        dr["CurrentMonthExpenses"])
                    .ToString("N2");
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
        hfExpenseID.Value = "";

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

        ddlExpenseType.SelectedIndex = 0;

        txtExpenseDate.Text =
            DateTime.Now.ToString("yyyy-MM-dd");

        txtAmount.Text = "";
        txtDescription.Text = "";
        txtPaidTo.Text = "";

        ddlPaymentMode.SelectedIndex = 0;

        txtReceiptNumber.Text = "";
        txtRemarks.Text = "";

        btnSave.Text = "Save Expense";
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