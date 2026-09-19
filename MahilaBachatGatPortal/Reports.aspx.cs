using System;
using System.Data;
using System.Data.SqlClient;

public partial class Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBachatGats();

            txtFromDate.Text =
                new DateTime(
                    DateTime.Today.Year,
                    DateTime.Today.Month,
                    1)
                .ToString("yyyy-MM-dd");

            txtToDate.Text =
                DateTime.Today.ToString("yyyy-MM-dd");
        }
    }

    private void LoadBachatGats()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            SqlDataAdapter da =
                new SqlDataAdapter(
                    "SELECT BachatGatID, GatName " +
                    "FROM BachatGat " +
                    "ORDER BY GatName", con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            ddlBachatGat.DataSource = dt;
            ddlBachatGat.DataTextField = "GatName";
            ddlBachatGat.DataValueField = "BachatGatID";

            ddlBachatGat.DataBind();

            ddlBachatGat.Items.Insert(
                0,
                new System.Web.UI.WebControls.ListItem(
                    "-- All Bachat Gats --", ""));
        }
    }

    protected void btnMemberReport_Click(
        object sender, EventArgs e)
    {
        string query = @"
            SELECT
                M.MemberID,
                M.MemberCode,
                M.MemberName,
                BG.GatName,
                M.Mobile,
                M.Village,
                M.JoinDate,
                M.Status
            FROM Members M
            INNER JOIN BachatGat BG
                ON M.BachatGatID = BG.BachatGatID
            WHERE M.JoinDate BETWEEN @FromDate AND @ToDate";

        if (ddlBachatGat.SelectedValue != "")
        {
            query += " AND M.BachatGatID = @BachatGatID";
        }

        query += " ORDER BY M.MemberID DESC";

        GenerateReport(
            query,
            "Member Report",
            false);
    }

    protected void btnSavingsReport_Click(
        object sender, EventArgs e)
    {
        string query = @"
            SELECT
                S.SavingID,
                BG.GatName,
                M.MemberCode,
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
            WHERE S.PaymentDate BETWEEN @FromDate AND @ToDate";

        if (ddlBachatGat.SelectedValue != "")
        {
            query += " AND S.BachatGatID = @BachatGatID";
        }

        query += " ORDER BY S.PaymentDate DESC";

        GenerateReport(
            query,
            "Savings Report",
            false);
    }

    protected void btnLoanReport_Click(
        object sender, EventArgs e)
    {
        string query = @"
            SELECT
                L.LoanID,
                BG.GatName,
                M.MemberCode,
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
            WHERE L.ApplicationDate BETWEEN @FromDate AND @ToDate";

        if (ddlBachatGat.SelectedValue != "")
        {
            query += " AND L.BachatGatID = @BachatGatID";
        }

        query += " ORDER BY L.LoanID DESC";

        GenerateReport(
            query,
            "Loan Report",
            false);
    }

    protected void btnMeetingReport_Click(
        object sender, EventArgs e)
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
            WHERE M.MeetingDate BETWEEN @FromDate AND @ToDate";

        if (ddlBachatGat.SelectedValue != "")
        {
            query += " AND M.BachatGatID = @BachatGatID";
        }

        query += " ORDER BY M.MeetingDate DESC";

        GenerateReport(
            query,
            "Meeting Report",
            false);
    }

    protected void btnFinancialReport_Click(
        object sender, EventArgs e)
    {
        DateTime fromDate;
        DateTime toDate;

        if (!DateTime.TryParse(
            txtFromDate.Text,
            out fromDate))
        {
            ShowMessage(
                "Invalid From Date.",
                "danger");
            return;
        }

        if (!DateTime.TryParse(
            txtToDate.Text,
            out toDate))
        {
            ShowMessage(
                "Invalid To Date.",
                "danger");
            return;
        }

        if (fromDate > toDate)
        {
            ShowMessage(
                "From Date cannot be greater than To Date.",
                "danger");
            return;
        }

        string query = @"
            SELECT
                BG.GatName,

                ISNULL((
                    SELECT SUM(I.Amount)
                    FROM Income I
                    WHERE I.BachatGatID = BG.BachatGatID
                    AND I.IncomeDate BETWEEN @FromDate AND @ToDate
                ), 0) AS TotalIncome,

                ISNULL((
                    SELECT SUM(E.Amount)
                    FROM Expenses E
                    WHERE E.BachatGatID = BG.BachatGatID
                    AND E.ExpenseDate BETWEEN @FromDate AND @ToDate
                ), 0) AS TotalExpenses,

                ISNULL((
                    SELECT SUM(I.Amount)
                    FROM Income I
                    WHERE I.BachatGatID = BG.BachatGatID
                    AND I.IncomeDate BETWEEN @FromDate AND @ToDate
                ), 0)
                -
                ISNULL((
                    SELECT SUM(E.Amount)
                    FROM Expenses E
                    WHERE E.BachatGatID = BG.BachatGatID
                    AND E.ExpenseDate BETWEEN @FromDate AND @ToDate
                ), 0) AS ProfitLoss

            FROM BachatGat BG
            WHERE 1 = 1";

        if (ddlBachatGat.SelectedValue != "")
        {
            query +=
                " AND BG.BachatGatID = @BachatGatID";
        }

        query += " ORDER BY BG.GatName";

        using (SqlConnection con = DBHelper.GetConnection())
        {
            SqlDataAdapter da =
                new SqlDataAdapter(query, con);

            da.SelectCommand.Parameters.AddWithValue(
                "@FromDate",
                fromDate);

            da.SelectCommand.Parameters.AddWithValue(
                "@ToDate",
                toDate);

            if (ddlBachatGat.SelectedValue != "")
            {
                da.SelectCommand.Parameters.AddWithValue(
                    "@BachatGatID",
                    Convert.ToInt32(
                        ddlBachatGat.SelectedValue));
            }

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

    private void GenerateReport(
        string query,
        string title,
        bool unused)
    {
        DateTime fromDate;
        DateTime toDate;

        if (!DateTime.TryParse(
            txtFromDate.Text,
            out fromDate))
        {
            ShowMessage(
                "Invalid From Date.",
                "danger");
            return;
        }

        if (!DateTime.TryParse(
            txtToDate.Text,
            out toDate))
        {
            ShowMessage(
                "Invalid To Date.",
                "danger");
            return;
        }

        if (fromDate > toDate)
        {
            ShowMessage(
                "From Date cannot be greater than To Date.",
                "danger");
            return;
        }

        using (SqlConnection con = DBHelper.GetConnection())
        {
            SqlDataAdapter da =
                new SqlDataAdapter(query, con);

            da.SelectCommand.Parameters.AddWithValue(
                "@FromDate",
                fromDate);

            da.SelectCommand.Parameters.AddWithValue(
                "@ToDate",
                toDate);

            if (ddlBachatGat.SelectedValue != "")
            {
                da.SelectCommand.Parameters.AddWithValue(
                    "@BachatGatID",
                    Convert.ToInt32(
                        ddlBachatGat.SelectedValue));
            }

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

