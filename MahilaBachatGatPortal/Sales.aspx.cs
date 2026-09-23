using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

public partial class Sales : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RoleHelper.RequirePresidentSecretary(this);

        if (!IsPostBack)
        {
            LoadOrders();
        }
    }

    private void LoadOrders()
    {
        try
        {
            int bachatGatID = RoleHelper.GetBachatGatID();

            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = @"
                    SELECT
                        O.OrderID,
                        BG.GatName,
                        P.ProductName,
                        O.OrderDate,
                        O.CustomerName,
                        O.Mobile,
                        O.Quantity,
                        O.UnitPrice,
                        O.TotalAmount,
                        O.PaymentMode,
                        O.PaymentStatus,
                        O.UTRNumber,
                        O.PaymentScreenshot,
                        O.OrderStatus
                    FROM Orders O
                    INNER JOIN BachatGat BG
                        ON O.BachatGatID = BG.BachatGatID
                    INNER JOIN Products P
                        ON O.ProductID = P.ProductID
                    WHERE O.BachatGatID = @BachatGatID
                    ORDER BY O.OrderID DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue(
                        "@BachatGatID",
                        bachatGatID
                    );

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        gvOrders.DataSource = dt;
                        gvOrders.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error loading orders: " + ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void OrderCommand(object sender, CommandEventArgs e)
    {
        int orderID;

        if (!int.TryParse(e.CommandArgument.ToString(), out orderID))
        {
            lblMessage.Text = "Invalid Order ID.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }

        if (e.CommandName == "VerifyPayment")
        {
            VerifyPayment(orderID);
        }
        else if (e.CommandName == "RejectPayment")
        {
            RejectPayment(orderID);
        }
    }

    private void VerifyPayment(int orderID)
    {
        try
        {
            int bachatGatID = RoleHelper.GetBachatGatID();

            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = @"
                    UPDATE Orders
                    SET PaymentStatus = 'Paid'
                    WHERE OrderID = @OrderID
                    AND BachatGatID = @BachatGatID
                    AND PaymentMode = 'Online Payment'
                    AND PaymentStatus = 'Pending Verification'
                    AND UTRNumber IS NOT NULL
                    AND PaymentScreenshot IS NOT NULL";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderID);
                    cmd.Parameters.AddWithValue("@BachatGatID", bachatGatID);

                    con.Open();

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        lblMessage.Text = "Payment verified successfully.";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMessage.Text = "Payment could not be verified.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }

            LoadOrders();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error verifying payment: " + ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void RejectPayment(int orderID)
    {
        try
        {
            int bachatGatID = RoleHelper.GetBachatGatID();

            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = @"
                    UPDATE Orders
                    SET
                        PaymentStatus = 'Rejected',
                        OrderStatus = 'Cancelled'
                    WHERE OrderID = @OrderID
                    AND BachatGatID = @BachatGatID
                    AND PaymentMode = 'Online Payment'
                    AND PaymentStatus = 'Pending Verification'";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderID);
                    cmd.Parameters.AddWithValue("@BachatGatID", bachatGatID);

                    con.Open();

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        lblMessage.Text = "Payment rejected.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lblMessage.Text = "Payment could not be rejected.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }

            LoadOrders();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error rejecting payment: " + ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    public string GetPaymentScreenshotUrl(object value)
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

        if (path.StartsWith("~/"))
        {
            return ResolveUrl(path);
        }

        return ResolveUrl("~/" + path);
    }

    public bool HasPaymentScreenshot(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return false;
        }

        return !string.IsNullOrWhiteSpace(value.ToString());
    }

    public string GetPaymentStatusClass(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return "";
        }

        string status = value.ToString();

        if (status.Equals("Paid", StringComparison.OrdinalIgnoreCase))
        {
            return "status-paid";
        }

        if (status.Equals("Rejected", StringComparison.OrdinalIgnoreCase))
        {
            return "status-rejected";
        }

        return "status-pending";
    }
}