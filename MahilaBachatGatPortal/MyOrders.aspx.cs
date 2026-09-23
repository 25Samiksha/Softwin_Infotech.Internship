using System;
using System.Data;
using System.Data.SqlClient;

public partial class MyOrders : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("User.aspx?mode=login&returnUrl=MyOrders.aspx");
            return;
        }

        if (Session["Role"] == null ||
            !Session["Role"].ToString().Equals("Customer", StringComparison.OrdinalIgnoreCase))
        {
            Response.Redirect("PublicProducts.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadOrders();
        }
    }

    private void LoadOrders()
    {
        int userID = Convert.ToInt32(Session["UserID"]);

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    O.OrderID,
                    O.OrderDate,
                    O.Quantity,
                    O.UnitPrice,
                    O.TotalAmount,
                    O.PaymentMode,
                    O.PaymentStatus,
                    O.OrderStatus,
                    P.ProductName
                FROM Orders O
                INNER JOIN Products P
                    ON O.ProductID = P.ProductID
                WHERE O.UserID = @UserID
                ORDER BY O.OrderDate DESC, O.OrderID DESC";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserID", userID);

            DataTable dt = new DataTable();

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }

            if (dt.Rows.Count == 0)
            {
                pnlOrders.Visible = false;
                pnlEmpty.Visible = true;
                return;
            }

            pnlOrders.Visible = true;
            pnlEmpty.Visible = false;

            rptOrders.DataSource = dt;
            rptOrders.DataBind();
        }
    }

    protected string GetStatusClass(string status)
    {
        if (status == null)
        {
            return "status-placed";
        }

        switch (status.ToLower())
        {
            case "confirmed":
                return "status-confirmed";

            case "processing":
                return "status-processing";

            case "shipped":
                return "status-shipped";

            case "delivered":
                return "status-delivered";

            case "cancelled":
                return "status-cancelled";

            default:
                return "status-placed";
        }
    }

    protected string GetStepClass(string status, int step)
    {
        if (string.IsNullOrEmpty(status))
        {
            return "";
        }

        int currentStep = 1;

        switch (status.ToLower())
        {
            case "placed":
                currentStep = 1;
                break;

            case "confirmed":
                currentStep = 2;
                break;

            case "processing":
                currentStep = 3;
                break;

            case "shipped":
                currentStep = 4;
                break;

            case "delivered":
                currentStep = 5;
                break;

            case "cancelled":
                return "";
        }

        if (step <= currentStep)
        {
            return "active";
        }

        return "";
    }
}