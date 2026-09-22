using System;
using System.Data.SqlClient;

public partial class Payment : System.Web.UI.Page
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
            LoadAmount();
        }
    }

    private void LoadAmount()
    {
        int productID;

        if (!int.TryParse(Request.QueryString["ProductID"], out productID))
        {
            lblMessage.Text = "Invalid product.";
            btnPlaceOrder.Enabled = false;
            return;
        }

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT SellingPrice
                FROM Products
                WHERE ProductID = @ProductID
                AND Status = 'Available'
                AND Quantity > 0";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ProductID", productID);

            con.Open();

            object result = cmd.ExecuteScalar();

            if (result == null)
            {
                lblMessage.Text = "Product is no longer available.";
                btnPlaceOrder.Enabled = false;
                return;
            }

            decimal amount = Convert.ToDecimal(result);
            lblTotalAmount.Text = amount.ToString("N2");
        }
    }

    protected void btnPlaceOrder_Click(object sender, EventArgs e)
    {
        string paymentMode;

        if (rbCOD.Checked)
        {
            paymentMode = "Cash on Delivery";
        }
        else if (rbOnline.Checked)
        {
            paymentMode = "Online Payment";
        }
        else
        {
            lblMessage.Text = "Please select a payment method.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }

        Session["PaymentMode"] = paymentMode;

        Response.Redirect("OrderPlaced.aspx?ProductID=" +
                          Request.QueryString["ProductID"]);
    }
}