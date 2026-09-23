using System;
using System.Data.SqlClient;

public partial class Checkout : System.Web.UI.Page
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
            LoadCustomerDetails();
            LoadCartItems();
        }
    }

    private void LoadCustomerDetails()
    {
        txtFullName.Text = Session["FullName"] != null
            ? Session["FullName"].ToString()
            : "";

        int userID = Convert.ToInt32(Session["UserID"]);

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT Mobile, Email
                FROM Users
                WHERE UserID = @UserID";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserID", userID);

            con.Open();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    if (dr["Mobile"] != DBNull.Value)
                    {
                        txtMobile.Text = dr["Mobile"].ToString();
                    }
                }
            }
        }
    }

    private void LoadCartItems()
    {
        int userID = Convert.ToInt32(Session["UserID"]);

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    C.CartID,
                    C.ProductID,
                    C.Quantity,
                    P.ProductName,
                    P.SellingPrice,
                    P.Quantity AS AvailableQuantity,
                    B.GatName,
                    C.Quantity * P.SellingPrice AS ItemTotal
                FROM Cart C
                INNER JOIN Products P
                    ON C.ProductID = P.ProductID
                INNER JOIN BachatGat B
                    ON P.BachatGatID = B.BachatGatID
                WHERE C.UserID = @UserID
                AND P.Status = 'Available'
                AND P.Quantity > 0
                ORDER BY C.CartID DESC";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserID", userID);

            con.Open();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                gvCartItems.DataSource = dr;
                gvCartItems.DataBind();
            }
        }

        LoadGrandTotal();
        ValidateCart();
    }

    private void LoadGrandTotal()
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

            decimal total = Convert.ToDecimal(cmd.ExecuteScalar());

            lblGrandTotal.Text = total.ToString("N2");
        }
    }

    private void ValidateCart()
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
                AND P.Status = 'Available'
                AND P.Quantity > 0
                AND P.Quantity >= C.Quantity";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserID", userID);

            con.Open();

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            if (count == 0)
            {
                lblMessage.Text = "Your cart is empty or some products are unavailable.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                btnContinue.Enabled = false;
            }
        }
    }

    protected void btnContinue_Click(object sender, EventArgs e)
    {
        if (txtFullName.Text.Trim() == "" ||
            txtMobile.Text.Trim() == "" ||
            txtAddress.Text.Trim() == "" ||
            txtCity.Text.Trim() == "" ||
            txtTaluka.Text.Trim() == "" ||
            txtDistrict.Text.Trim() == "" ||
            txtState.Text.Trim() == "" ||
            txtPincode.Text.Trim() == "")
        {
            lblMessage.Text = "Please fill all delivery address details.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }

        int userID = Convert.ToInt32(Session["UserID"]);

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT COUNT(*)
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

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            if (count == 0)
            {
                lblMessage.Text = "Your cart is empty or some products are unavailable.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        Session["DeliveryFullName"] = txtFullName.Text.Trim();
        Session["DeliveryMobile"] = txtMobile.Text.Trim();
        Session["DeliveryAddress"] = txtAddress.Text.Trim();
        Session["DeliveryCity"] = txtCity.Text.Trim();
        Session["DeliveryTaluka"] = txtTaluka.Text.Trim();
        Session["DeliveryDistrict"] = txtDistrict.Text.Trim();
        Session["DeliveryState"] = txtState.Text.Trim();
        Session["DeliveryPincode"] = txtPincode.Text.Trim();

        Response.Redirect("OrderReview.aspx");
    }
}