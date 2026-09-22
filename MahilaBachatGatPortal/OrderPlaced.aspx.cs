using System;
using System.Data.SqlClient;

public partial class OrderPlaced : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("User.aspx");
            return;
        }

        if (Session["Role"] == null ||
            !Session["Role"].ToString().Equals("Customer", StringComparison.OrdinalIgnoreCase))
        {
            Response.Redirect("Dashboard.aspx");
            return;
        }

        if (!IsPostBack)
        {
            PlaceOrder();
        }
    }

    private void PlaceOrder()
    {
        int productID;

        if (!int.TryParse(Request.QueryString["ProductID"], out productID))
        {
            Response.Redirect("PublicProducts.aspx");
            return;
        }

        if (Session["DeliveryFullName"] == null ||
            Session["DeliveryMobile"] == null ||
            Session["DeliveryAddress"] == null ||
            Session["DeliveryCity"] == null ||
            Session["DeliveryTaluka"] == null ||
            Session["DeliveryDistrict"] == null ||
            Session["DeliveryState"] == null ||
            Session["DeliveryPincode"] == null ||
            Session["PaymentMode"] == null)
        {
            Response.Redirect("Checkout.aspx?ProductID=" + productID);
            return;
        }

        int userID = Convert.ToInt32(Session["UserID"]);
        string paymentMode = Session["PaymentMode"].ToString();

        using (SqlConnection con = DBHelper.GetConnection())
        {
            con.Open();

            SqlTransaction transaction = con.BeginTransaction();

            try
            {
                string productQuery = @"
                    SELECT
                        ProductName,
                        BachatGatID,
                        Quantity,
                        SellingPrice
                    FROM Products
                    WHERE ProductID = @ProductID
                    AND Status = 'Available'
                    AND Quantity > 0";

                SqlCommand productCmd = new SqlCommand(productQuery, con, transaction);
                productCmd.Parameters.AddWithValue("@ProductID", productID);

                SqlDataReader dr = productCmd.ExecuteReader();

                if (!dr.Read())
                {
                    dr.Close();
                    transaction.Rollback();
                    Response.Redirect("PublicProducts.aspx");
                    return;
                }

                string productName = dr["ProductName"].ToString();
                int bachatGatID = Convert.ToInt32(dr["BachatGatID"]);
                int availableQuantity = Convert.ToInt32(dr["Quantity"]);
                decimal unitPrice = Convert.ToDecimal(dr["SellingPrice"]);

                dr.Close();

                int orderQuantity = 1;

                if (availableQuantity < orderQuantity)
                {
                    transaction.Rollback();
                    Response.Redirect("PublicProducts.aspx");
                    return;
                }

                decimal totalAmount = unitPrice * orderQuantity;

                string insertQuery = @"
                    INSERT INTO Orders
                    (
                        UserID,
                        BachatGatID,
                        ProductID,
                        OrderDate,
                        Quantity,
                        UnitPrice,
                        TotalAmount,
                        CustomerName,
                        Mobile,
                        DeliveryAddress,
                        City,
                        Taluka,
                        District,
                        State,
                        Pincode,
                        PaymentMode,
                        PaymentStatus,
                        OrderStatus
                    )
                    VALUES
                    (
                        @UserID,
                        @BachatGatID,
                        @ProductID,
                        GETDATE(),
                        @Quantity,
                        @UnitPrice,
                        @TotalAmount,
                        @CustomerName,
                        @Mobile,
                        @DeliveryAddress,
                        @City,
                        @Taluka,
                        @District,
                        @State,
                        @Pincode,
                        @PaymentMode,
                        @PaymentStatus,
                        @OrderStatus
                    );

                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

                SqlCommand insertCmd = new SqlCommand(insertQuery, con, transaction);

                insertCmd.Parameters.AddWithValue("@UserID", userID);
                insertCmd.Parameters.AddWithValue("@BachatGatID", bachatGatID);
                insertCmd.Parameters.AddWithValue("@ProductID", productID);
                insertCmd.Parameters.AddWithValue("@Quantity", orderQuantity);
                insertCmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                insertCmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                insertCmd.Parameters.AddWithValue("@CustomerName", Session["DeliveryFullName"].ToString());
                insertCmd.Parameters.AddWithValue("@Mobile", Session["DeliveryMobile"].ToString());
                insertCmd.Parameters.AddWithValue("@DeliveryAddress", Session["DeliveryAddress"].ToString());
                insertCmd.Parameters.AddWithValue("@City", Session["DeliveryCity"].ToString());
                insertCmd.Parameters.AddWithValue("@Taluka", Session["DeliveryTaluka"].ToString());
                insertCmd.Parameters.AddWithValue("@District", Session["DeliveryDistrict"].ToString());
                insertCmd.Parameters.AddWithValue("@State", Session["DeliveryState"].ToString());
                insertCmd.Parameters.AddWithValue("@Pincode", Session["DeliveryPincode"].ToString());
                insertCmd.Parameters.AddWithValue("@PaymentMode", paymentMode);
                insertCmd.Parameters.AddWithValue("@PaymentStatus",
                    paymentMode == "Cash on Delivery" ? "Pending" : "Pending");
                insertCmd.Parameters.AddWithValue("@OrderStatus", "Placed");

                int orderID = Convert.ToInt32(insertCmd.ExecuteScalar());

                string updateQuery = @"
                    UPDATE Products
                    SET Quantity = Quantity - @Quantity
                    WHERE ProductID = @ProductID
                    AND Quantity >= @Quantity";

                SqlCommand updateCmd = new SqlCommand(updateQuery, con, transaction);
                updateCmd.Parameters.AddWithValue("@Quantity", orderQuantity);
                updateCmd.Parameters.AddWithValue("@ProductID", productID);

                int updatedRows = updateCmd.ExecuteNonQuery();

                if (updatedRows == 0)
                {
                    transaction.Rollback();
                    Response.Redirect("PublicProducts.aspx");
                    return;
                }

                transaction.Commit();

                lblOrderID.Text = orderID.ToString();
                lblProductName.Text = productName;
                lblQuantity.Text = orderQuantity.ToString();
                lblPaymentMode.Text = paymentMode;
                lblPaymentStatus.Text = "Pending";
                lblOrderStatus.Text = "Placed";
                lblTotalAmount.Text = totalAmount.ToString("N2");

                lblFullName.Text = Session["DeliveryFullName"].ToString();
                lblMobile.Text = Session["DeliveryMobile"].ToString();
                lblAddress.Text = Session["DeliveryAddress"].ToString();
                lblCity.Text = Session["DeliveryCity"].ToString();
                lblTaluka.Text = Session["DeliveryTaluka"].ToString();
                lblDistrict.Text = Session["DeliveryDistrict"].ToString();
                lblState.Text = Session["DeliveryState"].ToString();
                lblPincode.Text = Session["DeliveryPincode"].ToString();

                ClearOrderSession();
            }
            catch
            {
                transaction.Rollback();
                Response.Redirect("Payment.aspx?ProductID=" + productID);
            }
        }
    }

    private void ClearOrderSession()
    {
        Session.Remove("DeliveryFullName");
        Session.Remove("DeliveryMobile");
        Session.Remove("DeliveryAddress");
        Session.Remove("DeliveryCity");
        Session.Remove("DeliveryTaluka");
        Session.Remove("DeliveryDistrict");
        Session.Remove("DeliveryState");
        Session.Remove("DeliveryPincode");
        Session.Remove("PaymentMode");
    }
}