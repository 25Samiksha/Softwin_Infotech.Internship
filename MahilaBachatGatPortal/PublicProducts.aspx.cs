using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class PublicProducts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadProducts();
        }
    }

    private void LoadProducts()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    ProductID,
                    BachatGatID,
                    ProductName,
                    Category,
                    Description,
                    Unit,
                    Quantity,
                    SellingPrice,
                    ProductImage
                FROM Products
                WHERE Status = 'Available'
                AND Quantity > 0
                ORDER BY ProductName";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            rptProducts.DataSource = dt;
            rptProducts.DataBind();

            if (dt.Rows.Count == 0)
            {
                lblMessage.Text = "No products are currently available.";
            }
            else
            {
                lblMessage.Text = "";
            }
        }
    }

    public string GetImageUrl(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return "";
        }

        string path = value.ToString().Trim();

        if (path == "")
        {
            return "";
        }

        path = path.Replace("\\", "/");

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