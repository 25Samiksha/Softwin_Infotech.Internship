using System;
using System.Data.SqlClient;

public partial class ProductDetails : System.Web.UI.Page
{
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
            pnlProduct.Visible = false;
            lblMessage.Text = "Invalid product.";
            return;
        }

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    ProductID,
                    ProductName,
                    Category,
                    Description,
                    Unit,
                    Quantity,
                    SellingPrice,
                    ProductImage
                FROM Products
                WHERE ProductID = @ProductID
                AND Status = 'Available'
                AND Quantity > 0";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ProductID", productID);

            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                lblProductName.Text = dr["ProductName"].ToString();
                lblCategory.Text = dr["Category"].ToString();
                lblDescription.Text = dr["Description"].ToString();
                lblQuantity.Text = dr["Quantity"].ToString();
                lblUnit.Text = " " + dr["Unit"].ToString();
                lblSellingPrice.Text = Convert.ToDecimal(dr["SellingPrice"]).ToString("N2");

                if (dr["ProductImage"] != DBNull.Value &&
                    !string.IsNullOrWhiteSpace(dr["ProductImage"].ToString()))
                {
                    string imagePath = dr["ProductImage"].ToString().Trim();

                    if (imagePath.StartsWith("ProductImages/", StringComparison.OrdinalIgnoreCase))
                    {
                        imagePath = "Images/" + imagePath.Substring("ProductImages/".Length);
                    }

                    if (!imagePath.StartsWith("Images/", StringComparison.OrdinalIgnoreCase))
                    {
                        imagePath = "Images/" + System.IO.Path.GetFileName(imagePath);
                    }

                    imgProduct.ImageUrl = ResolveUrl("~/" + imagePath);
                    imgProduct.Visible = true;
                }
                else
                {
                    imgProduct.Visible = false;
                }
            }
            else
            {
                pnlProduct.Visible = false;
                lblMessage.Text = "Product not found.";
            }

            dr.Close();
        }
    }

    protected void btnBuyNow_Click(object sender, EventArgs e)
    {
        string productID = Request.QueryString["ProductID"];

        if (string.IsNullOrEmpty(productID))
        {
            lblMessage.Text = "Invalid product.";
            return;
        }

        if (Session["UserID"] == null)
        {
            string returnUrl = "Checkout.aspx?ProductID=" + productID;

            Response.Redirect("User.aspx?returnUrl=" +
                              Server.UrlEncode(returnUrl));

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
}