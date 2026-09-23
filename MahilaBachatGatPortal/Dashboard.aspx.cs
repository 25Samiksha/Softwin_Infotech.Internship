using System;
using System.Data;
using System.Data.SqlClient;

public partial class Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RoleHelper.RequireLogin(this);

        if (Session["Role"] != null &&
            Session["Role"].ToString().Equals("Customer", StringComparison.OrdinalIgnoreCase))
        {
            Response.Redirect("PublicHome.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadDashboardData();

            if (RoleHelper.IsPresident())
            {
                LoadPendingOrders();
            }
        }
    }

    private void LoadDashboardData()
    {
        int bachatGatID = RoleHelper.GetBachatGatID();
        int memberID = RoleHelper.GetMemberID();

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query;

            if (RoleHelper.IsAdmin())
            {
                query = @"
                    SELECT
                        (SELECT COUNT(*) FROM BachatGat) AS TotalGats,
                        (SELECT COUNT(*) FROM Members) AS TotalMembers,
                        (SELECT ISNULL(SUM(Amount),0) FROM MemberSavings) AS TotalSavings,
                        (SELECT ISNULL(SUM(LoanAmount),0) FROM Loans) AS TotalLoans,
                        (SELECT COUNT(*) FROM Loans WHERE Status='Pending') AS PendingLoans,
                        (SELECT COUNT(*) FROM GovernmentSchemes WHERE Status='Active') AS ActiveSchemes";
            }
            else if (RoleHelper.IsPresidentOrSecretary())
            {
                if (bachatGatID <= 0)
                {
                    Session.Clear();
                    Response.Redirect("User.aspx");
                    return;
                }

                query = @"
                    SELECT
                        (SELECT COUNT(*)
                         FROM BachatGat
                         WHERE BachatGatID=@BachatGatID) AS TotalGats,

                        (SELECT COUNT(*)
                         FROM Members
                         WHERE BachatGatID=@BachatGatID) AS TotalMembers,

                        (SELECT ISNULL(SUM(Amount),0)
                         FROM MemberSavings
                         WHERE BachatGatID=@BachatGatID) AS TotalSavings,

                        (SELECT ISNULL(SUM(LoanAmount),0)
                         FROM Loans
                         WHERE BachatGatID=@BachatGatID) AS TotalLoans,

                        (SELECT COUNT(*)
                         FROM Loans
                         WHERE BachatGatID=@BachatGatID
                         AND Status='Pending') AS PendingLoans,

                        (SELECT COUNT(*)
                         FROM GovernmentSchemes
                         WHERE Status='Active') AS ActiveSchemes";
            }
            else if (RoleHelper.IsMember())
            {
                if (bachatGatID <= 0 || memberID <= 0)
                {
                    Session.Clear();
                    Response.Redirect("User.aspx");
                    return;
                }

                query = @"
                    SELECT
                        (SELECT COUNT(*)
                         FROM BachatGat
                         WHERE BachatGatID=@BachatGatID) AS TotalGats,

                        (SELECT COUNT(*)
                         FROM Members
                         WHERE MemberID=@MemberID
                         AND BachatGatID=@BachatGatID) AS TotalMembers,

                        (SELECT ISNULL(SUM(Amount),0)
                         FROM MemberSavings
                         WHERE MemberID=@MemberID
                         AND BachatGatID=@BachatGatID) AS TotalSavings,

                        (SELECT ISNULL(SUM(LoanAmount),0)
                         FROM Loans
                         WHERE MemberID=@MemberID
                         AND BachatGatID=@BachatGatID) AS TotalLoans,

                        (SELECT COUNT(*)
                         FROM Loans
                         WHERE MemberID=@MemberID
                         AND BachatGatID=@BachatGatID
                         AND Status='Pending') AS PendingLoans,

                        (SELECT COUNT(*)
                         FROM GovernmentSchemes
                         WHERE Status='Active') AS ActiveSchemes";
            }
            else
            {
                Response.Redirect("PublicHome.aspx");
                return;
            }

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (!RoleHelper.IsAdmin())
                {
                    cmd.Parameters.AddWithValue("@BachatGatID", bachatGatID);
                }

                if (RoleHelper.IsMember())
                {
                    cmd.Parameters.AddWithValue("@MemberID", memberID);
                }

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        lblTotalGats.Text = dr["TotalGats"].ToString();
                        lblTotalMembers.Text = dr["TotalMembers"].ToString();
                        lblTotalSavings.Text = Convert.ToDecimal(dr["TotalSavings"]).ToString("N2");
                        lblTotalLoans.Text = Convert.ToDecimal(dr["TotalLoans"]).ToString("N2");
                        lblPendingLoans.Text = dr["PendingLoans"].ToString();
                        lblActiveSchemes.Text = dr["ActiveSchemes"].ToString();
                    }
                }
            }
        }
    }

    private void LoadPendingOrders()
    {
        int bachatGatID = RoleHelper.GetBachatGatID();

        if (bachatGatID <= 0)
        {
            pnlPendingOrders.Visible = false;
            return;
        }

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    O.OrderID,
                    O.CustomerName,
                    P.ProductName,
                    O.Quantity,
                    O.TotalAmount,
                    O.PaymentMode,
                    O.PaymentStatus
                FROM Orders O
                INNER JOIN Products P
                    ON O.ProductID=P.ProductID
                WHERE O.BachatGatID=@BachatGatID
                AND O.OrderStatus='Placed'
                ORDER BY O.OrderDate DESC,O.OrderID DESC";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@BachatGatID", bachatGatID);

            DataTable dt = new DataTable();

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }

            pnlPendingOrders.Visible = true;
            gvPendingOrders.DataSource = dt;
            gvPendingOrders.DataBind();
        }
    }

    protected void gvPendingOrders_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName != "VerifyOrder")
        {
            return;
        }

        if (!RoleHelper.IsPresident())
        {
            return;
        }

        int orderID = Convert.ToInt32(e.CommandArgument);
        int bachatGatID = RoleHelper.GetBachatGatID();

        if (bachatGatID <= 0)
        {
            return;
        }

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                UPDATE Orders
                SET
                    PaymentStatus='Verified',
                    OrderStatus='Confirmed'
                WHERE OrderID=@OrderID
                AND BachatGatID=@BachatGatID
                AND OrderStatus='Placed'";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@OrderID", orderID);
            cmd.Parameters.AddWithValue("@BachatGatID", bachatGatID);

            con.Open();

            int result = cmd.ExecuteNonQuery();

            if (result > 0)
            {
                lblOrderMessage.Text = "Order verified successfully.";
                lblOrderMessage.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblOrderMessage.Text = "Order could not be verified.";
                lblOrderMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        LoadPendingOrders();
    }
}