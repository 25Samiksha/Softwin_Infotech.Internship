using System;
using System.Data.SqlClient;

public partial class OrderReview : System.Web.UI.Page
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
            LoadOrderDetails();
            LoadDeliveryAddress();
        }
    }

    private void LoadOrderDetails()
    {
        int productID;

        if (!int.TryParse(Request.QueryString["ProductID"], out productID))
        {
            lblMessage.Text = "Invalid product.";
            btnContinuePayment.Enabled = false;
            return;
        }

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    ProductName,
                    SellingPrice
                FROM Products
                WHERE ProductID = @ProductID
                AND Status = 'Available'
                AND Quantity > 0";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ProductID", productID);

            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                decimal price = Convert.ToDecimal(dr["SellingPrice"]);

                lblProductName.Text = dr["ProductName"].ToString();
                lblPrice.Text = price.ToString("N2");
                lblQuantity.Text = "1";
                lblTotalAmount.Text = price.ToString("N2");
            }
            else
            {
                lblMessage.Text = "Product is no longer available.";
                btnContinuePayment.Enabled = false;
            }

            dr.Close();
        }
    }

    private void LoadDeliveryAddress()
    {
        if (Session["DeliveryFullName"] != null)
            lblFullName.Text = Session["DeliveryFullName"].ToString();

        if (Session["DeliveryMobile"] != null)
            lblMobile.Text = Session["DeliveryMobile"].ToString();

        if (Session["DeliveryAddress"] != null)
            lblAddress.Text = Session["DeliveryAddress"].ToString();

        if (Session["DeliveryCity"] != null)
            lblCity.Text = Session["DeliveryCity"].ToString();

        if (Session["DeliveryTaluka"] != null)
            lblTaluka.Text = Session["DeliveryTaluka"].ToString();

        if (Session["DeliveryDistrict"] != null)
            lblDistrict.Text = Session["DeliveryDistrict"].ToString();

        if (Session["DeliveryState"] != null)
            lblState.Text = Session["DeliveryState"].ToString();

        if (Session["DeliveryPincode"] != null)
            lblPincode.Text = Session["DeliveryPincode"].ToString();
    }

    protected void btnContinuePayment_Click(object sender, EventArgs e)
    {
        Response.Redirect("Payment.aspx?ProductID=" +
                          Request.QueryString["ProductID"]);
    }
}