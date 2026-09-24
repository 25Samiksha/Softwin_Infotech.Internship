using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class PublicHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadSliderProducts();
            LoadProducts();
        }
    }

    private void LoadSliderProducts()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("ImagePath");
        dt.Columns.Add("ImageName");

        DataRow row;

        row = dt.NewRow();
        row["ImagePath"] = "Images/homemade bowl.jpg";
        row["ImageName"] = "Homemade Bowl";
        dt.Rows.Add(row);

        row = dt.NewRow();
        row["ImagePath"] = "Images/Homemade Mango Pickle.jpg";
        row["ImageName"] = "Homemade Mango Pickle";
        dt.Rows.Add(row);

        row = dt.NewRow();
        row["ImagePath"] = "Images/Homemade Papad.jpg";
        row["ImageName"] = "Homemade Papad";
        dt.Rows.Add(row);

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    ProductImage,
                    ProductName
                FROM Products
                WHERE Status = 'Available'
                AND Quantity > 0
                AND ProductImage IS NOT NULL
                AND LTRIM(RTRIM(ProductImage)) <> ''
                ORDER BY CreatedDate DESC";

            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    string image = dr["ProductImage"].ToString().Trim();

                    if (image != "")
                    {
                        row = dt.NewRow();
                        row["ImagePath"] = image;
                        row["ImageName"] = dr["ProductName"].ToString();
                        dt.Rows.Add(row);
                    }
                }
            }
        }

        rptSliderProducts.DataSource = dt;
        rptSliderProducts.DataBind();

        rptSliderProductsDuplicate.DataSource = dt;
        rptSliderProductsDuplicate.DataBind();
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