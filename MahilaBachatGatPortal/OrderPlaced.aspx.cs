using System;
using System.Data;
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
            Response.Redirect("Checkout.aspx");
            return;
        }

        int userID = Convert.ToInt32(Session["UserID"]);
        string paymentMode = Session["PaymentMode"].ToString();
        string utrNumber = Session["UTRNumber"] != null ? Session["UTRNumber"].ToString() : "";
        string paymentScreenshot = Session["PaymentScreenshot"] != null ? Session["PaymentScreenshot"].ToString() : "";

        string paymentStatus = paymentMode == "Online Payment"
            ? "Pending Verification"
            : "Pending";

        if (paymentMode == "Online Payment" &&
            (string.IsNullOrWhiteSpace(utrNumber) || string.IsNullOrWhiteSpace(paymentScreenshot)))
        {
            Response.Redirect("Payment.aspx");
            return;
        }

        using (SqlConnection con = DBHelper.GetConnection())
        {
            con.Open();

            SqlTransaction transaction = con.BeginTransaction();
            bool committed = false;

            try
            {
                string cartQuery = @"
                    SELECT
                        C.CartID,
                        C.ProductID,
                        C.Quantity,
                        P.ProductName,
                        B.GatName,
                        P.BachatGatID,
                        P.Quantity AS AvailableQuantity,
                        P.SellingPrice AS UnitPrice,
                        C.Quantity * P.SellingPrice AS TotalAmount
                    FROM Cart C
                    INNER JOIN Products P
                        ON C.ProductID = P.ProductID
                    INNER JOIN BachatGat B
                        ON P.BachatGatID = B.BachatGatID
                    WHERE C.UserID = @UserID
                    AND P.Status = 'Available'
                    ORDER BY C.CartID";

                SqlCommand cartCmd = new SqlCommand(cartQuery, con, transaction);
                cartCmd.Parameters.AddWithValue("@UserID", userID);

                DataTable cartItems = new DataTable();

                using (SqlDataReader reader = cartCmd.ExecuteReader())
                {
                    cartItems.Load(reader);
                }

                if (cartItems.Rows.Count == 0)
                {
                    transaction.Rollback();
                    Response.Redirect("Cart.aspx");
                    return;
                }

                foreach (DataRow row in cartItems.Rows)
                {
                    int cartQuantity = Convert.ToInt32(row["Quantity"]);
                    int availableQuantity = Convert.ToInt32(row["AvailableQuantity"]);

                    if (availableQuantity < cartQuantity)
                    {
                        transaction.Rollback();
                        Response.Redirect("Cart.aspx");
                        return;
                    }
                }

                int firstOrderID = 0;
                decimal grandTotal = 0;

                foreach (DataRow row in cartItems.Rows)
                {
                    int cartID = Convert.ToInt32(row["CartID"]);
                    int productID = Convert.ToInt32(row["ProductID"]);
                    int quantity = Convert.ToInt32(row["Quantity"]);
                    int bachatGatID = Convert.ToInt32(row["BachatGatID"]);
                    decimal unitPrice = Convert.ToDecimal(row["UnitPrice"]);
                    decimal totalAmount = Convert.ToDecimal(row["TotalAmount"]);

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
                            OrderStatus,
                            UTRNumber,
                            PaymentScreenshot
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
                            @OrderStatus,
                            @UTRNumber,
                            @PaymentScreenshot
                        );

                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    SqlCommand insertCmd = new SqlCommand(insertQuery, con, transaction);

                    insertCmd.Parameters.AddWithValue("@UserID", userID);
                    insertCmd.Parameters.AddWithValue("@BachatGatID", bachatGatID);
                    insertCmd.Parameters.AddWithValue("@ProductID", productID);
                    insertCmd.Parameters.AddWithValue("@Quantity", quantity);
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
                    insertCmd.Parameters.AddWithValue("@PaymentStatus", paymentStatus);
                    insertCmd.Parameters.AddWithValue("@OrderStatus", "Placed");
                    insertCmd.Parameters.AddWithValue("@UTRNumber",
                        string.IsNullOrWhiteSpace(utrNumber) ? (object)DBNull.Value : utrNumber);
                    insertCmd.Parameters.AddWithValue("@PaymentScreenshot",
                        string.IsNullOrWhiteSpace(paymentScreenshot) ? (object)DBNull.Value : paymentScreenshot);

                    int orderID = Convert.ToInt32(insertCmd.ExecuteScalar());

                    if (firstOrderID == 0)
                    {
                        firstOrderID = orderID;
                    }

                    string updateProductQuery = @"
                        UPDATE Products
                        SET Quantity = Quantity - @Quantity
                        WHERE ProductID = @ProductID
                        AND Status = 'Available'
                        AND Quantity >= @Quantity";

                    SqlCommand updateProductCmd = new SqlCommand(updateProductQuery, con, transaction);
                    updateProductCmd.Parameters.AddWithValue("@Quantity", quantity);
                    updateProductCmd.Parameters.AddWithValue("@ProductID", productID);

                    int updatedRows = updateProductCmd.ExecuteNonQuery();

                    if (updatedRows == 0)
                    {
                        transaction.Rollback();
                        Response.Redirect("Cart.aspx");
                        return;
                    }

                    string deleteCartQuery = @"
                        DELETE FROM Cart
                        WHERE CartID = @CartID
                        AND UserID = @UserID";

                    SqlCommand deleteCartCmd = new SqlCommand(deleteCartQuery, con, transaction);
                    deleteCartCmd.Parameters.AddWithValue("@CartID", cartID);
                    deleteCartCmd.Parameters.AddWithValue("@UserID", userID);
                    deleteCartCmd.ExecuteNonQuery();

                    grandTotal += totalAmount;
                }

                transaction.Commit();
                committed = true;

                gvOrderItems.DataSource = cartItems;
                gvOrderItems.DataBind();

                lblOrderID.Text = firstOrderID.ToString();
                lblPaymentMode.Text = paymentMode;
                lblPaymentStatus.Text = paymentStatus;
                lblOrderStatus.Text = "Placed";
                lblTotalAmount.Text = grandTotal.ToString("N2");

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
            catch (Exception ex)
            {
                if (!committed)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch
                    {
                    }
                }

                Response.Write("<div style='color:red;padding:20px;font-size:16px;'>" + Server.HtmlEncode(ex.Message) + "</div>");
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
        Session.Remove("UTRNumber");
        Session.Remove("PaymentScreenshot");
    }
}