using System;
using System.Data;
using System.Data.SqlClient;

public partial class Sales : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtSaleDate.Text =
                DateTime.Today.ToString("yyyy-MM-dd");

            LoadBachatGats();
            LoadSales();
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

    protected void ddlBachatGat_SelectedIndexChanged(
        object sender, EventArgs e)
    {
        LoadProducts();
    }

    private void LoadProducts()
    {
        ddlProduct.Items.Clear();

        if (ddlBachatGat.SelectedValue == "")
            return;

        using (SqlConnection con = DBHelper.GetConnection())
        {
            SqlDataAdapter da = new SqlDataAdapter(
                @"SELECT ProductID, ProductName
                  FROM Products
                  WHERE BachatGatID=@BachatGatID
                  AND Status='Available'
                  AND Quantity > 0
                  ORDER BY ProductName", con);

            da.SelectCommand.Parameters.AddWithValue(
                "@BachatGatID",
                Convert.ToInt32(ddlBachatGat.SelectedValue));

            DataTable dt = new DataTable();

            da.Fill(dt);

            ddlProduct.DataSource = dt;
            ddlProduct.DataTextField = "ProductName";
            ddlProduct.DataValueField = "ProductID";
            ddlProduct.DataBind();

            ddlProduct.Items.Insert(0,
                new System.Web.UI.WebControls.ListItem(
                    "-- Select Product --", ""));
        }
    }

    protected void ddlProduct_SelectedIndexChanged(
        object sender, EventArgs e)
    {
        if (ddlProduct.SelectedValue == "")
            return;

        using (SqlConnection con = DBHelper.GetConnection())
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(
                @"SELECT Quantity, SellingPrice
                  FROM Products
                  WHERE ProductID=@ProductID", con);

            cmd.Parameters.AddWithValue(
                "@ProductID",
                Convert.ToInt32(ddlProduct.SelectedValue));

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                txtStock.Text =
                    dr["Quantity"].ToString();

                txtUnitPrice.Text =
                    dr["SellingPrice"].ToString();

                CalculateTotal();
            }

            dr.Close();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DateTime saleDate;
        decimal quantity;
        decimal unitPrice;

        if (ddlBachatGat.SelectedValue == "")
        {
            ShowMessage("Please select Bachat Gat.", "danger");
            return;
        }

        if (ddlProduct.SelectedValue == "")
        {
            ShowMessage("Please select product.", "danger");
            return;
        }

        if (!DateTime.TryParse(
            txtSaleDate.Text, out saleDate))
        {
            ShowMessage("Enter valid sale date.", "danger");
            return;
        }

        if (!decimal.TryParse(
            txtQuantity.Text, out quantity) ||
            quantity <= 0)
        {
            ShowMessage("Enter valid quantity.", "danger");
            return;
        }

        if (!decimal.TryParse(
            txtUnitPrice.Text, out unitPrice) ||
            unitPrice <= 0)
        {
            ShowMessage("Enter valid unit price.", "danger");
            return;
        }

        using (SqlConnection con = DBHelper.GetConnection())
        {
            con.Open();

            SqlTransaction transaction =
                con.BeginTransaction();

            try
            {
                SqlCommand stockCmd = new SqlCommand(
                    @"SELECT Quantity
                      FROM Products WITH (UPDLOCK)
                      WHERE ProductID=@ProductID
                      AND BachatGatID=@BachatGatID",
                    con, transaction);

                stockCmd.Parameters.AddWithValue(
                    "@ProductID",
                    Convert.ToInt32(ddlProduct.SelectedValue));

                stockCmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    Convert.ToInt32(ddlBachatGat.SelectedValue));

                object stockResult =
                    stockCmd.ExecuteScalar();

                if (stockResult == null)
                {
                    throw new Exception(
                        "Product not found.");
                }

                decimal stock =
                    Convert.ToDecimal(stockResult);

                if (quantity > stock)
                {
                    throw new Exception(
                        "Insufficient stock.");
                }

                string saleQuery = @"
                    INSERT INTO Sales
                    (
                        BachatGatID,
                        ProductID,
                        SaleDate,
                        CustomerName,
                        CustomerMobile,
                        Quantity,
                        UnitPrice,
                        PaymentMode,
                        ReceiptNumber,
                        Remarks
                    )
                    VALUES
                    (
                        @BachatGatID,
                        @ProductID,
                        @SaleDate,
                        @CustomerName,
                        @CustomerMobile,
                        @Quantity,
                        @UnitPrice,
                        @PaymentMode,
                        @ReceiptNumber,
                        @Remarks
                    )";

                SqlCommand saleCmd =
                    new SqlCommand(
                        saleQuery,
                        con,
                        transaction);

                saleCmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    Convert.ToInt32(
                        ddlBachatGat.SelectedValue));

                saleCmd.Parameters.AddWithValue(
                    "@ProductID",
                    Convert.ToInt32(
                        ddlProduct.SelectedValue));

                saleCmd.Parameters.AddWithValue(
                    "@SaleDate",
                    saleDate);

                saleCmd.Parameters.AddWithValue(
                    "@CustomerName",
                    txtCustomerName.Text.Trim());

                saleCmd.Parameters.AddWithValue(
                    "@CustomerMobile",
                    txtCustomerMobile.Text.Trim());

                saleCmd.Parameters.AddWithValue(
                    "@Quantity",
                    quantity);

                saleCmd.Parameters.AddWithValue(
                    "@UnitPrice",
                    unitPrice);

                saleCmd.Parameters.AddWithValue(
                    "@PaymentMode",
                    ddlPaymentMode.SelectedValue);

                saleCmd.Parameters.AddWithValue(
                    "@ReceiptNumber",
                    txtReceiptNumber.Text.Trim());

                saleCmd.Parameters.AddWithValue(
                    "@Remarks",
                    txtRemarks.Text.Trim());

                saleCmd.ExecuteNonQuery();

                SqlCommand updateStock =
                    new SqlCommand(
                    @"UPDATE Products
                      SET Quantity = Quantity - @Quantity,
                          Status =
                            CASE
                              WHEN Quantity - @Quantity <= 0
                              THEN 'Out of Stock'
                              ELSE Status
                            END
                      WHERE ProductID=@ProductID",
                    con,
                    transaction);

                updateStock.Parameters.AddWithValue(
                    "@Quantity", quantity);

                updateStock.Parameters.AddWithValue(
                    "@ProductID",
                    Convert.ToInt32(
                        ddlProduct.SelectedValue));

                updateStock.ExecuteNonQuery();

                transaction.Commit();

                ShowMessage(
                    "Sale saved and stock updated successfully.",
                    "success");

                ClearForm();
                LoadProducts();
                LoadSales();
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                ShowMessage(
                    ex.Message,
                    "danger");
            }
        }
    }

    private void CalculateTotal()
    {
        decimal quantity;
        decimal price;

        if (decimal.TryParse(
            txtQuantity.Text, out quantity) &&
            decimal.TryParse(
            txtUnitPrice.Text, out price))
        {
            txtTotalAmount.Text =
                (quantity * price).ToString("0.00");
        }
    }

    private void LoadSales()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT S.SaleID,
                       BG.GatName,
                       P.ProductName,
                       S.SaleDate,
                       S.CustomerName,
                       S.Quantity,
                       S.UnitPrice,
                       S.TotalAmount,
                       S.PaymentMode,
                       S.ReceiptNumber
                FROM Sales S
                INNER JOIN BachatGat BG
                    ON S.BachatGatID=BG.BachatGatID
                INNER JOIN Products P
                    ON S.ProductID=P.ProductID
                ORDER BY S.SaleID DESC";

            SqlDataAdapter da =
                new SqlDataAdapter(query, con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvSales.DataSource = dt;
            gvSales.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT S.SaleID,
                       BG.GatName,
                       P.ProductName,
                       S.SaleDate,
                       S.CustomerName,
                       S.Quantity,
                       S.UnitPrice,
                       S.TotalAmount,
                       S.PaymentMode,
                       S.ReceiptNumber
                FROM Sales S
                INNER JOIN BachatGat BG
                    ON S.BachatGatID=BG.BachatGatID
                INNER JOIN Products P
                    ON S.ProductID=P.ProductID
                WHERE S.CustomerName LIKE @Search
                   OR P.ProductName LIKE @Search
                ORDER BY S.SaleID DESC";

            SqlDataAdapter da =
                new SqlDataAdapter(query, con);

            da.SelectCommand.Parameters.AddWithValue(
                "@Search",
                "%" + txtSearch.Text.Trim() + "%");

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvSales.DataSource = dt;
            gvSales.DataBind();
        }
    }

    protected void btnShowAll_Click(object sender, EventArgs e)
    {
        LoadSales();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    {
        txtSaleDate.Text =
            DateTime.Today.ToString("yyyy-MM-dd");

        txtCustomerName.Text = "";
        txtCustomerMobile.Text = "";
        txtQuantity.Text = "";
        txtTotalAmount.Text = "";
        txtStock.Text = "";
        txtUnitPrice.Text = "";
        txtReceiptNumber.Text = "";
        txtRemarks.Text = "";
    }

    private void ShowMessage(string message, string type)
    {
        lblMessage.Text =
            "<div class='alert alert-" + type + "'>" +
            message +
            "</div>";
    }
}