using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class Products : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RoleHelper.RequirePresidentSecretary(this);

        if (!IsPostBack)
        {
            fuProductImage.Attributes["accept"] = "image/*";
            LoadBachatGats();
            LoadProducts();
        }
    }

    private void LoadBachatGats()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"SELECT BachatGatID,GatName
                             FROM BachatGat
                             WHERE BachatGatID=@BachatGatID
                             AND Status='Active'
                             ORDER BY GatName";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.SelectCommand.Parameters.AddWithValue("@BachatGatID", RoleHelper.GetBachatGatID());

            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlBachatGat.DataSource = dt;
            ddlBachatGat.DataTextField = "GatName";
            ddlBachatGat.DataValueField = "BachatGatID";
            ddlBachatGat.DataBind();

            if (dt.Rows.Count == 1)
            {
                ddlBachatGat.SelectedValue = dt.Rows[0]["BachatGatID"].ToString();
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int bachatGatID = RoleHelper.GetBachatGatID();
        int productID = 0;
        decimal quantity;
        decimal costPrice;
        decimal sellingPrice;

        int.TryParse(hfProductID.Value, out productID);

        if (bachatGatID == 0)
        {
            ShowMessage("Bachat Gat is not assigned.", "danger");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtProductName.Text))
        {
            ShowMessage("Enter product name.", "danger");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtCategory.Text))
        {
            ShowMessage("Enter category.", "danger");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtUnit.Text))
        {
            ShowMessage("Enter unit.", "danger");
            return;
        }

        if (!decimal.TryParse(txtQuantity.Text, out quantity) || quantity < 0)
        {
            ShowMessage("Enter valid quantity.", "danger");
            return;
        }

        if (!decimal.TryParse(txtCostPrice.Text, out costPrice) || costPrice < 0)
        {
            ShowMessage("Enter valid cost price.", "danger");
            return;
        }

        if (!decimal.TryParse(txtSellingPrice.Text, out sellingPrice) || sellingPrice < 0)
        {
            ShowMessage("Enter valid selling price.", "danger");
            return;
        }

        try
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();

                string imagePath = "";

                if (productID > 0)
                {
                    SqlCommand oldImageCmd = new SqlCommand(
                        @"SELECT ProductImage
                          FROM Products
                          WHERE ProductID=@ProductID
                          AND BachatGatID=@BachatGatID", con);

                    oldImageCmd.Parameters.AddWithValue("@ProductID", productID);
                    oldImageCmd.Parameters.AddWithValue("@BachatGatID", bachatGatID);

                    object oldImage = oldImageCmd.ExecuteScalar();

                    if (oldImage == null)
                    {
                        ShowMessage("Product not found.", "danger");
                        return;
                    }

                    if (oldImage != DBNull.Value)
                    {
                        imagePath = oldImage.ToString();
                    }
                }

                if (fuProductImage.HasFile)
                {
                    string extension = Path.GetExtension(fuProductImage.FileName).ToLower();

                    if (extension != ".jpg" &&
                        extension != ".jpeg" &&
                        extension != ".png" &&
                        extension != ".gif" &&
                        extension != ".webp")
                    {
                        ShowMessage("Only JPG, JPEG, PNG, GIF and WEBP images are allowed.", "danger");
                        return;
                    }

                    string folderPath = Server.MapPath("~/Images/");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string fileName = Guid.NewGuid().ToString() + extension;
                    string filePath = Path.Combine(folderPath, fileName);

                    fuProductImage.SaveAs(filePath);

                    imagePath = "Images/" + fileName;
                }

                if (productID > 0)
                {
                    string query = @"
                        UPDATE Products
                        SET ProductName=@ProductName,
                            Category=@Category,
                            Description=@Description,
                            Unit=@Unit,
                            Quantity=@Quantity,
                            CostPrice=@CostPrice,
                            SellingPrice=@SellingPrice,
                            ProductImage=@ProductImage,
                            Status=@Status
                        WHERE ProductID=@ProductID
                        AND BachatGatID=@BachatGatID";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Category", txtCategory.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@Unit", txtUnit.Text.Trim());
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@CostPrice", costPrice);
                    cmd.Parameters.AddWithValue("@SellingPrice", sellingPrice);
                    cmd.Parameters.AddWithValue("@ProductImage", imagePath);
                    cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@ProductID", productID);
                    cmd.Parameters.AddWithValue("@BachatGatID", bachatGatID);

                    cmd.ExecuteNonQuery();

                    ShowMessage("Product updated successfully.", "success");
                }
                else
                {
                    string query = @"
                        INSERT INTO Products
                        (
                            BachatGatID,
                            ProductName,
                            Category,
                            Description,
                            Unit,
                            Quantity,
                            CostPrice,
                            SellingPrice,
                            ProductImage,
                            Status,
                            CreatedDate
                        )
                        VALUES
                        (
                            @BachatGatID,
                            @ProductName,
                            @Category,
                            @Description,
                            @Unit,
                            @Quantity,
                            @CostPrice,
                            @SellingPrice,
                            @ProductImage,
                            @Status,
                            GETDATE()
                        )";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@BachatGatID", bachatGatID);
                    cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Category", txtCategory.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@Unit", txtUnit.Text.Trim());
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@CostPrice", costPrice);
                    cmd.Parameters.AddWithValue("@SellingPrice", sellingPrice);
                    cmd.Parameters.AddWithValue("@ProductImage", imagePath);
                    cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);

                    cmd.ExecuteNonQuery();

                    ShowMessage("Product added successfully.", "success");
                }
            }

            ClearForm();
            LoadBachatGats();
            LoadProducts();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, "danger");
        }
    }

    private void LoadProducts()
    {
        int bachatGatID = RoleHelper.GetBachatGatID();

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    P.ProductID,
                    BG.GatName,
                    P.ProductName,
                    P.ProductImage,
                    P.Category,
                    P.Unit,
                    P.Quantity,
                    P.CostPrice,
                    P.SellingPrice,
                    P.Status
                FROM Products P
                INNER JOIN BachatGat BG
                    ON P.BachatGatID=BG.BachatGatID
                WHERE P.BachatGatID=@BachatGatID
                ORDER BY P.ProductID DESC";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.SelectCommand.Parameters.AddWithValue("@BachatGatID", bachatGatID);

            DataTable dt = new DataTable();
            da.Fill(dt);

            gvProducts.DataSource = dt;
            gvProducts.DataBind();
        }
    }

    protected void gvProducts_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);

        if (rowIndex < 0 || rowIndex >= gvProducts.Rows.Count)
        {
            return;
        }

        int productID = Convert.ToInt32(gvProducts.DataKeys[rowIndex].Value);

        if (e.CommandName == "EditProduct")
        {
            LoadProduct(productID);
        }
        else if (e.CommandName == "DeleteProduct")
        {
            DeleteProduct(productID);
        }
    }

    private void LoadProduct(int productID)
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
                    CostPrice,
                    SellingPrice,
                    ProductImage,
                    Status
                FROM Products
                WHERE ProductID=@ProductID
                AND BachatGatID=@BachatGatID";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@ProductID", productID);
            cmd.Parameters.AddWithValue("@BachatGatID", RoleHelper.GetBachatGatID());

            con.Open();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    hfProductID.Value = dr["ProductID"].ToString();
                    ddlBachatGat.SelectedValue = dr["BachatGatID"].ToString();
                    txtProductName.Text = dr["ProductName"].ToString();
                    txtCategory.Text = dr["Category"].ToString();
                    txtDescription.Text = dr["Description"].ToString();
                    txtUnit.Text = dr["Unit"].ToString();
                    txtQuantity.Text = dr["Quantity"].ToString();
                    txtCostPrice.Text = dr["CostPrice"].ToString();
                    txtSellingPrice.Text = dr["SellingPrice"].ToString();
                    ddlStatus.SelectedValue = dr["Status"].ToString();
                    btnSave.Text = "Update Product";
                }
            }
        }
    }

    private void DeleteProduct(int productID)
    {
        try
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = @"
                    DELETE FROM Products
                    WHERE ProductID=@ProductID
                    AND BachatGatID=@BachatGatID";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ProductID", productID);
                cmd.Parameters.AddWithValue("@BachatGatID", RoleHelper.GetBachatGatID());

                con.Open();

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    ShowMessage("Product deleted successfully.", "success");
                }
                else
                {
                    ShowMessage("Product not found.", "danger");
                }
            }

            LoadProducts();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, "danger");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    P.ProductID,
                    BG.GatName,
                    P.ProductName,
                    P.ProductImage,
                    P.Category,
                    P.Unit,
                    P.Quantity,
                    P.CostPrice,
                    P.SellingPrice,
                    P.Status
                FROM Products P
                INNER JOIN BachatGat BG
                    ON P.BachatGatID=BG.BachatGatID
                WHERE P.BachatGatID=@BachatGatID
                AND
                (
                    P.ProductName LIKE @Search
                    OR P.Category LIKE @Search
                )
                ORDER BY P.ProductID DESC";

            SqlDataAdapter da = new SqlDataAdapter(query, con);

            da.SelectCommand.Parameters.AddWithValue(
                "@BachatGatID",
                RoleHelper.GetBachatGatID());

            da.SelectCommand.Parameters.AddWithValue(
                "@Search",
                "%" + txtSearch.Text.Trim() + "%");

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvProducts.DataSource = dt;
            gvProducts.DataBind();
        }
    }

    protected void btnShowAll_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        LoadProducts();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    {
        hfProductID.Value = "";
        txtProductName.Text = "";
        txtCategory.Text = "";
        txtUnit.Text = "";
        txtQuantity.Text = "";
        txtCostPrice.Text = "";
        txtSellingPrice.Text = "";
        txtDescription.Text = "";
        lblImageMessage.Text = "";

        if (ddlStatus.Items.FindByValue("Available") != null)
        {
            ddlStatus.SelectedValue = "Available";
        }

        if (ddlBachatGat.Items.FindByValue(
            RoleHelper.GetBachatGatID().ToString()) != null)
        {
            ddlBachatGat.SelectedValue =
                RoleHelper.GetBachatGatID().ToString();
        }

        btnSave.Text = "Save Product";
    }

    public string GetImageUrl(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return ResolveUrl("~/Images/no-image.png");
        }

        string path = value.ToString().Trim();

        if (path == "")
        {
            return ResolveUrl("~/Images/no-image.png");
        }

        path = path.Replace("\\", "/");

        if (!path.StartsWith("Images/", StringComparison.OrdinalIgnoreCase))
        {
            path = "Images/" + Path.GetFileName(path);
        }

        return ResolveUrl("~/" + path);
    }

    private void ShowMessage(string message, string type)
    {
        lblMessage.Text =
            "<div class='alert alert-" + type + "'>" +
            message +
            "</div>";
    }
}