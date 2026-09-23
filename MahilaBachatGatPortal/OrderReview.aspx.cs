using System;
using System.Data.SqlClient;

public partial class OrderReview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("User.aspx?returnUrl=" + Server.UrlEncode(Request.RawUrl));
            return;
        }

        if (Session["Role"] == null ||
            !Session["Role"].ToString().Equals("Customer", StringComparison.OrdinalIgnoreCase))
        {
            Response.Redirect("Dashboard.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadOrderDetails();
            LoadDeliveryAddress();
        }
    }

    private void LoadOrderDetails()
    {
        int userID = Convert.ToInt32(Session["UserID"]);

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    C.CartID,
                    P.ProductName,
                    B.GatName,
                    C.Quantity,
                    P.SellingPrice,
                    C.Quantity * P.SellingPrice AS ItemTotal,
                    P.Quantity AS AvailableQuantity
                FROM Cart C
                INNER JOIN Products P
                    ON C.ProductID = P.ProductID
                INNER JOIN BachatGat B
                    ON P.BachatGatID = B.BachatGatID
                WHERE C.UserID = @UserID
                AND P.Status = 'Available'
                AND P.Quantity > 0
                AND P.Quantity >= C.Quantity
                ORDER BY C.CartID DESC";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserID", userID);

            con.Open();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                gvOrderItems.DataSource = dr;
                gvOrderItems.DataBind();
            }
        }

        LoadTotalAmount();
        ValidateCart();
    }

    private void LoadTotalAmount()
    {
        int userID = Convert.ToInt32(Session["UserID"]);

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT ISNULL(SUM(C.Quantity * P.SellingPrice), 0)
                FROM Cart C
                INNER JOIN Products P
                    ON C.ProductID = P.ProductID
                WHERE C.UserID = @UserID
                AND P.Status = 'Available'
                AND P.Quantity > 0
                AND P.Quantity >= C.Quantity";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserID", userID);

            con.Open();

            decimal totalAmount = Convert.ToDecimal(cmd.ExecuteScalar());

            lblTotalAmount.Text = totalAmount.ToString("N2");
        }
    }

    private void ValidateCart()
    {
        int userID = Convert.ToInt32(Session["UserID"]);

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT COUNT(*)
                FROM Cart
                WHERE UserID = @UserID";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserID", userID);

            con.Open();

            int cartCount = Convert.ToInt32(cmd.ExecuteScalar());

            if (cartCount == 0)
            {
                lblMessage.Text = "Your cart is empty.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                btnContinuePayment.Enabled = false;
                return;
            }
        }

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT COUNT(*)
                FROM Cart C
                INNER JOIN Products P
                    ON C.ProductID = P.ProductID
                WHERE C.UserID = @UserID
                AND (
                    P.Status <> 'Available'
                    OR P.Quantity <= 0
                    OR P.Quantity < C.Quantity
                )";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserID", userID);

            con.Open();

            int invalidCount = Convert.ToInt32(cmd.ExecuteScalar());

            if (invalidCount > 0)
            {
                lblMessage.Text = "Some products in your cart are unavailable or have insufficient stock.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                btnContinuePayment.Enabled = false;
            }
        }
    }

    private void LoadDeliveryAddress()
    {
        lblFullName.Text = Session["DeliveryFullName"] != null
            ? Session["DeliveryFullName"].ToString()
            : "";

        lblMobile.Text = Session["DeliveryMobile"] != null
            ? Session["DeliveryMobile"].ToString()
            : "";

        lblAddress.Text = Session["DeliveryAddress"] != null
            ? Session["DeliveryAddress"].ToString()
            : "";

        lblCity.Text = Session["DeliveryCity"] != null
            ? Session["DeliveryCity"].ToString()
            : "";

        lblTaluka.Text = Session["DeliveryTaluka"] != null
            ? Session["DeliveryTaluka"].ToString()
            : "";

        lblDistrict.Text = Session["DeliveryDistrict"] != null
            ? Session["DeliveryDistrict"].ToString()
            : "";

        lblState.Text = Session["DeliveryState"] != null
            ? Session["DeliveryState"].ToString()
            : "";

        lblPincode.Text = Session["DeliveryPincode"] != null
            ? Session["DeliveryPincode"].ToString()
            : "";
    }

    protected void btnContinuePayment_Click(object sender, EventArgs e)
    {
        int userID = Convert.ToInt32(Session["UserID"]);

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT COUNT(*)
                FROM Cart C
                INNER JOIN Products P
                    ON C.ProductID = P.ProductID
                WHERE C.UserID = @UserID
                AND (
                    P.Status <> 'Available'
                    OR P.Quantity <= 0
                    OR P.Quantity < C.Quantity
                )";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserID", userID);

            con.Open();

            int invalidCount = Convert.ToInt32(cmd.ExecuteScalar());

            if (invalidCount > 0)
            {
                lblMessage.Text = "Some products in your cart are unavailable or have insufficient stock.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        Response.Redirect("Payment.aspx");
    }
}