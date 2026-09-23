using System;
using System.Data.SqlClient;
using System.IO;

public partial class ProductDetails : System.Web.UI.Page
{
    private int AvailableQuantity
    {
        get
        {
            if (ViewState["AvailableQuantity"] == null)
            {
                return 0;
            }

            return Convert.ToInt32(ViewState["AvailableQuantity"]);
        }
        set
        {
            ViewState["AvailableQuantity"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadProduct();
        }
    }

    private void LoadProduct()
    {
        int productID;

        if (!int.TryParse(Request.QueryString["ProductID"], out productID))
        {
            lblMessage.Text = "Invalid product.";
            btnBuyNow.Enabled = false;
            btnAddToCart.Enabled = false;
            return;
        }

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    P.ProductName,
                    P.Category,
                    P.Description,
                    P.Quantity,
                    P.Unit,
                    P.SellingPrice,
                    P.ProductImage,
                    B.GatName
                FROM Products P
                INNER JOIN BachatGat B
                    ON P.BachatGatID = B.BachatGatID
                WHERE P.ProductID = @ProductID
                AND P.Status = 'Available'";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ProductID", productID);

            con.Open();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    string productName = dr["ProductName"].ToString();
                    string category = dr["Category"].ToString();
                    string description = dr["Description"].ToString();
                    int quantity = Convert.ToInt32(dr["Quantity"]);
                    string unit = dr["Unit"].ToString();
                    decimal price = Convert.ToDecimal(dr["SellingPrice"]);
                    string imagePath = dr["ProductImage"].ToString();
                    string gatName = dr["GatName"].ToString();

                    lblProductName.Text = productName;
                    lblCategory.Text = category;
                    lblBachatGat.Text = gatName;
                    lblDescription.Text = description;
                    lblAvailableQuantity.Text = quantity.ToString();
                    lblUnit.Text = " " + unit;
                    lblPrice.Text = price.ToString("N2");

                    AvailableQuantity = quantity;
                    txtQuantity.Text = "1";

                    CalculateTotal(price, 1);

                    string imageUrl = GetImageUrl(imagePath);

                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        imgProduct.ImageUrl = imageUrl;
                    }

                    if (quantity <= 0)
                    {
                        lblMessage.Text = "Product is currently out of stock.";
                        btnBuyNow.Enabled = false;
                        btnAddToCart.Enabled = false;
                        btnPlus.Enabled = false;
                        btnMinus.Enabled = false;
                    }
                }
                else
                {
                    lblMessage.Text = "Product not found.";
                    btnBuyNow.Enabled = false;
                    btnAddToCart.Enabled = false;
                }
            }
        }
    }

    protected void btnMinus_Click(object sender, EventArgs e)
    {
        int quantity = GetSelectedQuantity();

        if (quantity > 1)
        {
            quantity--;
        }

        txtQuantity.Text = quantity.ToString();
        lblMessage.Text = "";

        UpdateTotal(quantity);
    }

    protected void btnPlus_Click(object sender, EventArgs e)
    {
        int quantity = GetSelectedQuantity();

        if (quantity < AvailableQuantity)
        {
            quantity++;
            txtQuantity.Text = quantity.ToString();
            lblMessage.Text = "";
            UpdateTotal(quantity);
        }
        else
        {
            lblMessage.Text = "Maximum available quantity is " + AvailableQuantity + ".";
        }
    }

    private int GetSelectedQuantity()
    {
        int quantity;

        if (!int.TryParse(txtQuantity.Text, out quantity))
        {
            quantity = 1;
        }

        if (quantity < 1)
        {
            quantity = 1;
        }

        if (AvailableQuantity > 0 && quantity > AvailableQuantity)
        {
            quantity = AvailableQuantity;
        }

        return quantity;
    }

    private void UpdateTotal(int quantity)
    {
        int productID;

        if (!int.TryParse(Request.QueryString["ProductID"], out productID))
        {
            return;
        }

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT SellingPrice
                FROM Products
                WHERE ProductID = @ProductID
                AND Status = 'Available'
                AND Quantity > 0";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ProductID", productID);

            con.Open();

            object result = cmd.ExecuteScalar();

            if (result != null)
            {
                decimal price = Convert.ToDecimal(result);
                CalculateTotal(price, quantity);
            }
        }
    }

    private void CalculateTotal(decimal price, int quantity)
    {
        decimal total = price * quantity;
        lblTotalAmount.Text = total.ToString("N2");
    }

    protected void btnAddToCart_Click(object sender, EventArgs e)
    {
        int quantity = GetSelectedQuantity();

        if (quantity <= 0)
        {
            lblMessage.Text = "Please select a valid quantity.";
            return;
        }

        if (quantity > AvailableQuantity)
        {
            lblMessage.Text = "Selected quantity is not available.";
            return;
        }

        string productID = Request.QueryString["ProductID"];

        if (string.IsNullOrEmpty(productID))
        {
            lblMessage.Text = "Invalid product.";
            return;
        }

        if (Session["UserID"] == null)
        {
            string returnUrl = "ProductDetails.aspx?ProductID=" + productID;
            Response.Redirect("User.aspx?returnUrl=" + Server.UrlEncode(returnUrl));
            return;
        }

        if (Session["Role"] == null ||
            !Session["Role"].ToString().Equals("Customer", StringComparison.OrdinalIgnoreCase))
        {
            lblMessage.Text = "Please login using a customer account to add products to cart.";
            return;
        }

        int productIDValue = Convert.ToInt32(productID);
        int userID = Convert.ToInt32(Session["UserID"]);

        using (SqlConnection con = DBHelper.GetConnection())
        {
            con.Open();

            string productQuery = @"
                SELECT BachatGatID, Quantity
                FROM Products
                WHERE ProductID = @ProductID
                AND Status = 'Available'";

            SqlCommand productCmd = new SqlCommand(productQuery, con);
            productCmd.Parameters.AddWithValue("@ProductID", productIDValue);

            int bachatGatID;
            int availableQuantity;

            using (SqlDataReader dr = productCmd.ExecuteReader())
            {
                if (!dr.Read())
                {
                    lblMessage.Text = "Product is no longer available.";
                    return;
                }

                bachatGatID = Convert.ToInt32(dr["BachatGatID"]);
                availableQuantity = Convert.ToInt32(dr["Quantity"]);
            }

            if (quantity > availableQuantity)
            {
                lblMessage.Text = "Selected quantity is not available.";
                return;
            }

            string cartQuery = @"
                SELECT CartID, Quantity
                FROM Cart
                WHERE UserID = @UserID
                AND ProductID = @ProductID";

            SqlCommand cartCmd = new SqlCommand(cartQuery, con);
            cartCmd.Parameters.AddWithValue("@UserID", userID);
            cartCmd.Parameters.AddWithValue("@ProductID", productIDValue);

            int cartID = 0;
            int existingQuantity = 0;

            using (SqlDataReader dr = cartCmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    cartID = Convert.ToInt32(dr["CartID"]);
                    existingQuantity = Convert.ToInt32(dr["Quantity"]);
                }
            }

            int newQuantity = existingQuantity + quantity;

            if (newQuantity > availableQuantity)
            {
                lblMessage.Text = "Cart quantity cannot exceed available stock.";
                return;
            }

            if (cartID > 0)
            {
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
            else
            {
                string insertQuery = @"
                    INSERT INTO Cart
                    (
                        UserID,
                        ProductID,
                        BachatGatID,
                        Quantity
                    )
                    VALUES
                    (
                        @UserID,
                        @ProductID,
                        @BachatGatID,
                        @Quantity
                    )";

                SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                insertCmd.Parameters.AddWithValue("@UserID", userID);
                insertCmd.Parameters.AddWithValue("@ProductID", productIDValue);
                insertCmd.Parameters.AddWithValue("@BachatGatID", bachatGatID);
                insertCmd.Parameters.AddWithValue("@Quantity", quantity);

                insertCmd.ExecuteNonQuery();
            }
        }

        Response.Redirect("Cart.aspx");
    }

    protected void btnBuyNow_Click(object sender, EventArgs e)
    {
        int quantity = GetSelectedQuantity();

        if (quantity <= 0)
        {
            lblMessage.Text = "Please select a valid quantity.";
            return;
        }

        if (quantity > AvailableQuantity)
        {
            lblMessage.Text = "Selected quantity is not available.";
            return;
        }

        string productID = Request.QueryString["ProductID"];

        if (string.IsNullOrEmpty(productID))
        {
            lblMessage.Text = "Invalid product.";
            return;
        }

        Session["OrderQuantity"] = quantity;

        if (Session["UserID"] == null)
        {
            string returnUrl = "Checkout.aspx?ProductID=" + productID;
            Response.Redirect("User.aspx?returnUrl=" + Server.UrlEncode(returnUrl));
            return;
        }

        if (Session["Role"] != null &&
            Session["Role"].ToString().Equals("Customer", StringComparison.OrdinalIgnoreCase))
        {
            Response.Redirect("Checkout.aspx?ProductID=" + productID);
            return;
        }

        Response.Redirect("Dashboard.aspx");
    }

    private string GetImageUrl(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return "";
        }

        string path = value.Trim().Replace("\\", "/");

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