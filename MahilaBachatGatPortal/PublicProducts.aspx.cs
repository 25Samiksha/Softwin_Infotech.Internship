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
            LoadCategories();
            LoadBachatGats();
            LoadProducts();
        }
    }

    private void LoadCategories()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT DISTINCT Category
                FROM Products
                WHERE Status = 'Available'
                AND Quantity > 0
                AND Category IS NOT NULL
                AND LTRIM(RTRIM(Category)) <> ''
                ORDER BY Category";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlCategory.Items.Clear();

            ddlCategory.Items.Add(
                new System.Web.UI.WebControls.ListItem(
                    "All Categories",
                    ""
                )
            );

            foreach (DataRow row in dt.Rows)
            {
                string category = row["Category"].ToString().Trim();

                ddlCategory.Items.Add(
                    new System.Web.UI.WebControls.ListItem(
                        category,
                        category
                    )
                );
            }
        }
    }

    private void LoadBachatGats()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT BachatGatID, GatName
                FROM BachatGat
                WHERE Status = 'Active'
                ORDER BY GatName";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlBachatGat.Items.Clear();

            ddlBachatGat.Items.Add(
                new System.Web.UI.WebControls.ListItem(
                    "All Bachat Gats",
                    ""
                )
            );

            foreach (DataRow row in dt.Rows)
            {
                ddlBachatGat.Items.Add(
                    new System.Web.UI.WebControls.ListItem(
                        row["GatName"].ToString(),
                        row["BachatGatID"].ToString()
                    )
                );
            }
        }
    }

    private void LoadProducts()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    P.ProductID,
                    P.BachatGatID,
                    P.ProductName,
                    P.Category,
                    P.Description,
                    P.Unit,
                    P.Quantity,
                    P.SellingPrice,
                    P.ProductImage,
                    B.GatName
                FROM Products P
                INNER JOIN BachatGat B
                    ON P.BachatGatID = B.BachatGatID
                WHERE P.Status = 'Available'
                AND P.Quantity > 0";

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                query += @"
                    AND
                    (
                        P.ProductName LIKE @Search
                        OR P.Description LIKE @Search
                    )";
            }

            if (!string.IsNullOrWhiteSpace(ddlCategory.SelectedValue))
            {
                query += @"
                    AND P.Category = @Category";
            }

            if (!string.IsNullOrWhiteSpace(ddlBachatGat.SelectedValue))
            {
                query += @"
                    AND P.BachatGatID = @BachatGatID";
            }

            query += " ORDER BY P.ProductName";

            SqlCommand cmd = new SqlCommand(query, con);

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                cmd.Parameters.AddWithValue(
                    "@Search",
                    "%" + txtSearch.Text.Trim() + "%"
                );
            }

            if (!string.IsNullOrWhiteSpace(ddlCategory.SelectedValue))
            {
                cmd.Parameters.AddWithValue(
                    "@Category",
                    ddlCategory.SelectedValue.Trim()
                );
            }

            if (!string.IsNullOrWhiteSpace(ddlBachatGat.SelectedValue))
            {
                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    Convert.ToInt32(ddlBachatGat.SelectedValue)
                );
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            rptProducts.DataSource = dt;
            rptProducts.DataBind();

            if (dt.Rows.Count == 0)
            {
                lblMessage.Text = "No products found.";
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Text = "";
                lblMessage.Visible = false;
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadProducts();
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProducts();
    }

    protected void ddlBachatGat_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProducts();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";

        if (ddlCategory.Items.Count > 0)
        {
            ddlCategory.SelectedIndex = 0;
        }

        if (ddlBachatGat.Items.Count > 0)
        {
            ddlBachatGat.SelectedIndex = 0;
        }

        LoadProducts();
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

        if (path.StartsWith(
            "ProductImages/",
            StringComparison.OrdinalIgnoreCase))
        {
            path = "Images/" +
                   path.Substring("ProductImages/".Length);
        }

        if (!path.StartsWith(
            "Images/",
            StringComparison.OrdinalIgnoreCase))
        {
            path = "Images/" + Path.GetFileName(path);
        }

        return ResolveUrl("~/" + path);
    }
}