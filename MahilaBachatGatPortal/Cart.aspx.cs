using System;
using System.Data.SqlClient;
using System.IO;

public partial class Cart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("User.aspx?returnUrl=" + Server.UrlEncode("Cart.aspx"));
            return;
        }

        if (Session["Role"] == null || !Session["Role"].ToString().Equals("Customer", StringComparison.OrdinalIgnoreCase))
        {
            Response.Redirect("Dashboard.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadCart();
        }
    }

    private void LoadCart()
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
                    P.ProductImage,
                    P.Quantity AS AvailableQuantity,
                    C.Quantity * P.SellingPrice AS ItemTotal
                FROM Cart C
                INNER JOIN Products P ON C.ProductID = P.ProductID
                WHERE C.UserID = @UserID
                AND P.Status = 'Available'
                ORDER BY C.CartID DESC";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserID", userID);

            con.Open();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                gvCart.DataSource = dr;
                gvCart.DataBind();
            }
        }

        LoadCartTotal();
        CheckCart();
    }

    private void LoadCartTotal()
    {
        int userID = Convert.ToInt32(Session["UserID"]);

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT ISNULL(SUM(C.Quantity * P.SellingPrice),0)
                FROM Cart C
                INNER JOIN Products P ON C.ProductID = P.ProductID
                WHERE C.UserID = @UserID
                AND P.Status = 'Available'";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserID", userID);

            con.Open();

            decimal total = Convert.ToDecimal(cmd.ExecuteScalar());

            lblCartTotal.Text = total.ToString("N2");
        }
    }

    private void CheckCart()
    {
        int userID = Convert.ToInt32(Session["UserID"]);

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT COUNT(*)
                FROM Cart C
                INNER JOIN Products P ON C.ProductID = P.ProductID
                WHERE C.UserID = @UserID
                AND P.Status = 'Available'";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserID", userID);

            con.Open();

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            if (count == 0)
            {
                pnlCart.Visible = false;
                pnlEmptyCart.Visible = true;
                return;
            }

            pnlCart.Visible = true;
            pnlEmptyCart.Visible = false;
        }
    }

    protected void gvCart_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int cartID;

        if (!int.TryParse(e.CommandArgument.ToString(), out cartID))
        {
            return;
        }

        if (e.CommandName == "Increase")
        {
            UpdateQuantity(cartID, 1);
        }
        else if (e.CommandName == "Decrease")
        {
            UpdateQuantity(cartID, -1);
        }
        else if (e.CommandName == "RemoveItem")
        {
            RemoveItem(cartID);
        }

        LoadCart();
    }

    private void UpdateQuantity(int cartID, int change)
    {
        int userID = Convert.ToInt32(Session["UserID"]);

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    C.Quantity,
                    P.Quantity AS AvailableQuantity
                FROM Cart C
                INNER JOIN Products P ON C.ProductID = P.ProductID
                WHERE C.CartID = @CartID
                AND C.UserID = @UserID
                AND P.Status = 'Available'";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@CartID", cartID);
            cmd.Parameters.AddWithValue("@UserID", userID);

            con.Open();

            int currentQuantity = 0;
            int availableQuantity = 0;

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (!dr.Read())
                {
                    lblMessage.Text = "Cart item not found.";
                    return;
                }

                currentQuantity = Convert.ToInt32(dr["Quantity"]);
                availableQuantity = Convert.ToInt32(dr["AvailableQuantity"]);
            }

            int newQuantity = currentQuantity + change;

            if (newQuantity <= 0)
            {
                RemoveItem(cartID);
                return;
            }

            if (newQuantity > availableQuantity)
            {
                lblMessage.Text = "Maximum available quantity is " + availableQuantity + ".";
                return;
            }

            string updateQuery = @"
                UPDATE Cart
                SET Quantity = @Quantity
                WHERE CartID = @CartID
                AND UserID = @UserID";

            SqlCommand updateCmd = new SqlCommand(updateQuery, con);
            updateCmd.Parameters.AddWithValue("@Quantity", newQuantity);
            updateCmd.Parameters.AddWithValue("@CartID", cartID);
            updateCmd.Parameters.AddWithValue("@UserID", userID);

            updateCmd.ExecuteNonQuery();
        }
    }

    private void RemoveItem(int cartID)
    {
        int userID = Convert.ToInt32(Session["UserID"]);

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                DELETE FROM Cart
                WHERE CartID = @CartID
                AND UserID = @UserID";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@CartID", cartID);
            cmd.Parameters.AddWithValue("@UserID", userID);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        lblMessage.Text = "";
    }

    protected void btnCheckout_Click(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("User.aspx?returnUrl=" + Server.UrlEncode("Cart.aspx"));
            return;
        }

        if (Session["Role"] == null || !Session["Role"].ToString().Equals("Customer", StringComparison.OrdinalIgnoreCase))
        {
            Response.Redirect("Dashboard.aspx");
            return;
        }

        int userID = Convert.ToInt32(Session["UserID"]);

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT COUNT(*)
                FROM Cart C
                INNER JOIN Products P ON C.ProductID = P.ProductID
                WHERE C.UserID = @UserID
                AND P.Status = 'Available'
                AND P.Quantity >= C.Quantity";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserID", userID);

            con.Open();

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            if (count == 0)
            {
                lblMessage.Text = "Your cart is empty or some products are unavailable.";
                return;
            }
        }

        Response.Redirect("Checkout.aspx");
    }

    protected void btnContinueShopping_Click(object sender, EventArgs e)
    {
        Response.Redirect("PublicProducts.aspx");
    }

    protected void btnShopNow_Click(object sender, EventArgs e)
    {
        Response.Redirect("PublicProducts.aspx");
    }

    public string GetImageUrl(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return "";
        }

        string path = value.ToString().Trim().Replace("\\", "/");

        if (path == "")
        {
            return "";
        }

        if (path.StartsWith("ProductImages/", StringComparison.OrdinalIgnoreCase))
        {
            path = "Images/" + path.Substring("ProductImages/".Length);
        }

        if (!path.StartsWith("Images/", StringComparison.OrdinalIgnoreCase))
        {
            path = "Images/" + Path.GetFileName(path);
        }

        return ResolveUrl("~/" + path);
    }
}