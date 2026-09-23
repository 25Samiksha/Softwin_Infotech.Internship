using System;
using System.Data.SqlClient;
using System.IO;

public partial class PublicHome : System.Web.UI.Page
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
                SELECT TOP 8
                    ProductID,
                    ProductName,
                    Category,
                    SellingPrice,
                    ProductImage
                FROM Products
                WHERE Status = 'Available'
                AND Quantity > 0
                ORDER BY CreatedDate DESC";

            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                rptProducts.DataSource = dr;
                rptProducts.DataBind();
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