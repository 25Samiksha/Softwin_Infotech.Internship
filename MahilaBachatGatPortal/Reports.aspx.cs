using System;
using System.Data;
using System.Data.SqlClient;

public partial class Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtFromDate.Attributes["type"] = "date";
        txtToDate.Attributes["type"] = "date";

        if (!IsPostBack)
        {
            LoadBachatGats();

            txtFromDate.Text = new DateTime(
                DateTime.Today.Year,
                DateTime.Today.Month,
                1
            ).ToString("yyyy-MM-dd");

            txtToDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }
    }

    private void LoadBachatGats()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query;

            if (RoleHelper.IsAdmin())
            {
                query = @"
                    SELECT BachatGatID, GatName
                    FROM BachatGat
                    ORDER BY GatName";
            }
            else
            {
                query = @"
                    SELECT BachatGatID, GatName
                    FROM BachatGat
                    WHERE BachatGatID = @BachatGatID
                    ORDER BY GatName";
            }

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (!RoleHelper.IsAdmin())
                {
                    cmd.Parameters.AddWithValue(
                        "@BachatGatID",
                        RoleHelper.GetBachatGatID()
                    );
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

            if (RoleHelper.IsAdmin())
            {
                ddlBachatGat.Items.Insert(
                    0,
                    new System.Web.UI.WebControls.ListItem(
                        "-- All Bachat Gats --",
                        ""
                    )
                );
            }
            else if (ddlBachatGat.Items.Count > 0)
            {
                ddlBachatGat.SelectedValue =
                    RoleHelper.GetBachatGatID().ToString();
            }
        }
    }

    protected void btnMemberReport_Click(object sender, EventArgs e)
    {
        string query = @"
            SELECT
                M.MemberID,
                M.MemberName,
                BG.GatName,
                M.Mobile,
                M.Village,
                M.JoinDate,
                M.Status
            FROM Members M
            INNER JOIN BachatGat BG
                ON M.BachatGatID = BG.BachatGatID
            WHERE M.JoinDate >= @FromDate
            AND M.JoinDate < DATEADD(DAY,1,@ToDate)";

        AddBachatGatFilter(ref query, "M.BachatGatID");

        query += " ORDER BY M.MemberID DESC";

        GenerateReport(query, "Member Report");
    }

    protected void btnSavingsReport_Click(object sender, EventArgs e)
    {
        string query = @"
            SELECT
                S.SavingID,
                BG.GatName,
                M.MemberName,
                S.SavingMonth,
                S.Amount,
                S.PaymentDate,
                S.PaymentMode,
                S.ReceiptNumber
            FROM MemberSavings S
            INNER JOIN Members M
                ON S.MemberID = M.MemberID
            INNER JOIN BachatGat BG
                ON S.BachatGatID = BG.BachatGatID
            WHERE S.PaymentDate >= @FromDate
            AND S.PaymentDate < DATEADD(DAY,1,@ToDate)";

        AddBachatGatFilter(ref query, "S.BachatGatID");

        query += " ORDER BY S.PaymentDate DESC";

        GenerateReport(query, "Savings Report");
    }

    protected void btnLoanReport_Click(object sender, EventArgs e)
    {
        string query = @"
            SELECT
                L.LoanID,
                BG.GatName,
                M.MemberName,
                L.ApplicationDate,
                L.LoanAmount,
                L.ApprovedAmount,
                L.InterestRate,
                L.LoanTermMonths,
                L.EMIAmount,
                L.Status
            FROM Loans L
            INNER JOIN Members M
                ON L.MemberID = M.MemberID
            INNER JOIN BachatGat BG
                ON L.BachatGatID = BG.BachatGatID
            WHERE L.ApplicationDate >= @FromDate
            AND L.ApplicationDate < DATEADD(DAY,1,@ToDate)";

        AddBachatGatFilter(ref query, "L.BachatGatID");

        query += " ORDER BY L.LoanID DESC";

        GenerateReport(query, "Loan Report");
    }

    protected void btnMeetingReport_Click(object sender, EventArgs e)
    {
        string query = @"
            SELECT
                M.MeetingID,
                BG.GatName,
                M.MeetingDate,
                M.MeetingTime,
                M.MeetingPlace,
                M.MeetingType,
                M.Agenda,
                M.Decisions,
                M.NextMeetingDate
            FROM Meetings M
            INNER JOIN BachatGat BG
                ON M.BachatGatID = BG.BachatGatID
            WHERE M.MeetingDate >= @FromDate
            AND M.MeetingDate < DATEADD(DAY,1,@ToDate)";

        AddBachatGatFilter(ref query, "M.BachatGatID");

        query += " ORDER BY M.MeetingDate DESC";

        GenerateReport(query, "Meeting Report");
    }

    protected void btnFinancialReport_Click(object sender, EventArgs e)
    {
        DateTime fromDate;
        DateTime toDate;

        if (!ValidateDates(out fromDate, out toDate))
        {
            return;
        }

        string query = @"
            SELECT
                BG.GatName,
                ISNULL((
                    SELECT SUM(I.Amount)
                    FROM Income I
                    WHERE I.BachatGatID = BG.BachatGatID
                    AND I.IncomeDate >= @FromDate
                    AND I.IncomeDate < DATEADD(DAY,1,@ToDate)
                ),0) AS TotalIncome,
                ISNULL((
                    SELECT SUM(E.Amount)
                    FROM Expenses E
                    WHERE E.BachatGatID = BG.BachatGatID
                    AND E.ExpenseDate >= @FromDate
                    AND E.ExpenseDate < DATEADD(DAY,1,@ToDate)
                ),0) AS TotalExpenses,
                ISNULL((
                    SELECT SUM(I.Amount)
                    FROM Income I
                    WHERE I.BachatGatID = BG.BachatGatID
                    AND I.IncomeDate >= @FromDate
                    AND I.IncomeDate < DATEADD(DAY,1,@ToDate)
                ),0)
                -
                ISNULL((
                    SELECT SUM(E.Amount)
                    FROM Expenses E
                    WHERE E.BachatGatID = BG.BachatGatID
                    AND E.ExpenseDate >= @FromDate
                    AND E.ExpenseDate < DATEADD(DAY,1,@ToDate)
                ),0) AS ProfitLoss
            FROM BachatGat BG
            WHERE 1=1";

        AddBachatGatFilter(ref query, "BG.BachatGatID");

        query += " ORDER BY BG.GatName";

        using (SqlConnection con = DBHelper.GetConnection())
        {
            SqlDataAdapter da = new SqlDataAdapter(query, con);

            da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate.Date);
            da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate.Date);

            AddBachatGatParameter(da.SelectCommand);

            DataTable dt = new DataTable();
            da.Fill(dt);

            gvReport.DataSource = dt;
            gvReport.DataBind();

            lblReportTitle.Text =
                "Financial Report (" +
                dt.Rows.Count +
                " records)";

            pnlReport.Visible = true;
        }
    }

    protected void btnOrderReport_Click(object sender, EventArgs e)
    {
        string query = @"
            SELECT
                O.OrderID,
                BG.GatName,
                P.ProductName,
                O.OrderDate,
                O.CustomerName,
                O.Mobile,
                O.Quantity,
                O.UnitPrice,
                O.TotalAmount,
                O.PaymentMode,
                O.PaymentStatus,
                O.OrderStatus,
                O.UTRNumber
            FROM Orders O
            INNER JOIN BachatGat BG
                ON O.BachatGatID = BG.BachatGatID
            INNER JOIN Products P
                ON O.ProductID = P.ProductID
            WHERE O.OrderDate >= @FromDate
            AND O.OrderDate < DATEADD(DAY,1,@ToDate)";

        AddBachatGatFilter(ref query, "O.BachatGatID");

        query += " ORDER BY O.OrderID DESC";

        GenerateReport(query, "Order Report");
    }

    private void GenerateReport(string query, string title)
    {
        DateTime fromDate;
        DateTime toDate;

        if (!ValidateDates(out fromDate, out toDate))
        {
            return;
        }

        using (SqlConnection con = DBHelper.GetConnection())
        {
            SqlDataAdapter da = new SqlDataAdapter(query, con);

            da.SelectCommand.Parameters.AddWithValue(
                "@FromDate",
                fromDate.Date
            );

            da.SelectCommand.Parameters.AddWithValue(
                "@ToDate",
                toDate.Date
            );

            AddBachatGatParameter(da.SelectCommand);

            DataTable dt = new DataTable();
            da.Fill(dt);

            gvReport.DataSource = dt;
            gvReport.DataBind();

            lblReportTitle.Text =
                title +
                " (" +
                dt.Rows.Count +
                " records)";

            pnlReport.Visible = true;
        }
    }

    private void AddBachatGatFilter(
        ref string query,
        string columnName)
    {
        if (RoleHelper.IsAdmin())
        {
            if (ddlBachatGat.SelectedValue != "")
            {
                query += " AND " + columnName + " = @BachatGatID";
            }
        }
        else
        {
            query += " AND " + columnName + " = @BachatGatID";
        }
    }

    private void AddBachatGatParameter(SqlCommand cmd)
    {
        if (RoleHelper.IsAdmin())
        {
            if (ddlBachatGat.SelectedValue != "")
            {
                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    Convert.ToInt32(ddlBachatGat.SelectedValue)
                );
            }
        }
        else
        {
            cmd.Parameters.AddWithValue(
                "@BachatGatID",
                RoleHelper.GetBachatGatID()
            );
        }
    }

    private bool ValidateDates(
        out DateTime fromDate,
        out DateTime toDate)
    {
        fromDate = DateTime.MinValue;
        toDate = DateTime.MinValue;

        if (!DateTime.TryParse(
            txtFromDate.Text,
            out fromDate))
        {
            ShowMessage(
                "Invalid From Date.",
                "danger"
            );
            return false;
        }

        if (!DateTime.TryParse(
            txtToDate.Text,
            out toDate))
        {
            ShowMessage(
                "Invalid To Date.",
                "danger"
            );
            return false;
        }

        if (fromDate > toDate)
        {
            ShowMessage(
                "From Date cannot be greater than To Date.",
                "danger"
            );
            return false;
        }

        return true;
    }

    private void ShowMessage(
        string message,
        string type)
    {
        lblMessage.Text =
            "<div class='alert alert-" +
            type +
            "'>" +
            message +
            "</div>";
    }
}