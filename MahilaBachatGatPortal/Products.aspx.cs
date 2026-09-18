using System;
using System.Data;
using System.Data.SqlClient;

public partial class Products : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBachatGats();
            LoadProducts();
        }
    }

    private void LoadBachatGats()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT BachatGatID, GatName FROM BachatGat " +
                "WHERE Status='Active' ORDER BY GatName", con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            ddlBachatGat.DataSource = dt;
            ddlBachatGat.DataTextField = "GatName";
            ddlBachatGat.DataValueField = "BachatGatID";
            ddlBachatGat.DataBind();

            ddlBachatGat.Items.Insert(0,
                new System.Web.UI.WebControls.ListItem(
                    "-- Select Bachat Gat --", ""));
        }
    }

    private void LoadProducts()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT P.ProductID,
                       BG.GatName,
                       P.ProductName,
                       P.Category,
                       P.Unit,
                       P.Quantity,
                       P.CostPrice,
                       P.SellingPrice,
                       P.Status
                FROM Products P
                INNER JOIN BachatGat BG
                    ON P.BachatGatID=BG.BachatGatID
                ORDER BY P.ProductID DESC";

            SqlDataAdapter da =
                new SqlDataAdapter(query, con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvProducts.DataSource = dt;
            gvProducts.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        decimal quantity;
        decimal costPrice;
        decimal sellingPrice;

        if (ddlBachatGat.SelectedValue == "")
        {
            ShowMessage("Please select Bachat Gat.", "danger");
            return;
        }

        if (txtProductName.Text.Trim() == "")
        {
            ShowMessage("Please enter product name.", "danger");
            return;
        }

        if (!decimal.TryParse(txtQuantity.Text, out quantity))
        {
            ShowMessage("Enter valid quantity.", "danger");
            return;
        }

        if (!decimal.TryParse(txtCostPrice.Text, out costPrice))
        {
            ShowMessage("Enter valid cost price.", "danger");
            return;
        }

        if (!decimal.TryParse(txtSellingPrice.Text, out sellingPrice))
        {
            ShowMessage("Enter valid selling price.", "danger");
            return;
        }

        string status = ddlStatus.SelectedValue;

        if (quantity <= 0 && status == "Available")
        {
            status = "Out of Stock";
        }

        using (SqlConnection con = DBHelper.GetConnection())
        {
            con.Open();

            string query;

            if (hfProductID.Value == "")
            {
                query = @"
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
                        Status
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
                        @Status
                    )";
            }
            else
            {
                query = @"
                    UPDATE Products SET
                        BachatGatID=@BachatGatID,
                        ProductName=@ProductName,
                        Category=@Category,
                        Description=@Description,
                        Unit=@Unit,
                        Quantity=@Quantity,
                        CostPrice=@CostPrice,
                        SellingPrice=@SellingPrice,
                        Status=@Status
                    WHERE ProductID=@ProductID";
            }

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@BachatGatID",
                Convert.ToInt32(ddlBachatGat.SelectedValue));

            cmd.Parameters.AddWithValue("@ProductName",
                txtProductName.Text.Trim());

            cmd.Parameters.AddWithValue("@Category",
                txtCategory.Text.Trim());

            cmd.Parameters.AddWithValue("@Description",
                txtDescription.Text.Trim());

            cmd.Parameters.AddWithValue("@Unit",
                txtUnit.Text.Trim());

            cmd.Parameters.AddWithValue("@Quantity",
                quantity);

            cmd.Parameters.AddWithValue("@CostPrice",
                costPrice);

            cmd.Parameters.AddWithValue("@SellingPrice",
                sellingPrice);

            cmd.Parameters.AddWithValue("@Status",
                status);

            if (hfProductID.Value != "")
            {
                cmd.Parameters.AddWithValue("@ProductID",
                    Convert.ToInt32(hfProductID.Value));
            }

            cmd.ExecuteNonQuery();
        }

        ShowMessage("Product saved successfully.", "success");

        ClearForm();
        LoadProducts();
    }

    protected void gvProducts_RowCommand(
        object sender,
        System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);

        int productID =
            Convert.ToInt32(
                gvProducts.DataKeys[index].Value);

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
            con.Open();

            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Products " +
                "WHERE ProductID=@ProductID", con);

            cmd.Parameters.AddWithValue(
                "@ProductID", productID);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                hfProductID.Value =
                    dr["ProductID"].ToString();

                ddlBachatGat.SelectedValue =
                    dr["BachatGatID"].ToString();

                txtProductName.Text =
                    dr["ProductName"].ToString();

                txtCategory.Text =
                    dr["Category"].ToString();

                txtDescription.Text =
                    dr["Description"].ToString();

                txtUnit.Text =
                    dr["Unit"].ToString();

                txtQuantity.Text =
                    dr["Quantity"].ToString();

                txtCostPrice.Text =
                    dr["CostPrice"].ToString();

                txtSellingPrice.Text =
                    dr["SellingPrice"].ToString();

                ddlStatus.SelectedValue =
                    dr["Status"].ToString();

                btnSave.Text = "Update Product";
            }

            dr.Close();
        }
    }

    private void DeleteProduct(int productID)
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            con.Open();

            SqlCommand checkCmd = new SqlCommand(
                "SELECT COUNT(*) FROM Sales " +
                "WHERE ProductID=@ProductID", con);

            checkCmd.Parameters.AddWithValue(
                "@ProductID", productID);

            int count =
                Convert.ToInt32(checkCmd.ExecuteScalar());

            if (count > 0)
            {
                ShowMessage(
                    "Product cannot be deleted because sales exist.",
                    "danger");

                return;
            }

            SqlCommand cmd = new SqlCommand(
                "DELETE FROM Products " +
                "WHERE ProductID=@ProductID", con);

            cmd.Parameters.AddWithValue(
                "@ProductID", productID);

            cmd.ExecuteNonQuery();
        }

        LoadProducts();

        ShowMessage(
            "Product deleted successfully.",
            "success");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT P.ProductID,
                       BG.GatName,
                       P.ProductName,
                       P.Category,
                       P.Unit,
                       P.Quantity,
                       P.CostPrice,
                       P.SellingPrice,
                       P.Status
                FROM Products P
                INNER JOIN BachatGat BG
                    ON P.BachatGatID=BG.BachatGatID
                WHERE P.ProductName LIKE @Search
                   OR P.Category LIKE @Search
                ORDER BY P.ProductID DESC";

            SqlDataAdapter da =
                new SqlDataAdapter(query, con);

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
        LoadProducts();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    {
        hfProductID.Value = "";

        if (ddlBachatGat.Items.Count > 0)
            ddlBachatGat.SelectedIndex = 0;

        txtProductName.Text = "";
        txtCategory.Text = "";
        txtDescription.Text = "";
        txtUnit.Text = "";
        txtQuantity.Text = "";
        txtCostPrice.Text = "";
        txtSellingPrice.Text = "";

        ddlStatus.SelectedIndex = 0;

        btnSave.Text = "Save Product";
    }

    private void ShowMessage(string message, string type)
    {
        lblMessage.Text =
            "<div class='alert alert-" + type + "'>" +
            message +
            "</div>";
    }
}