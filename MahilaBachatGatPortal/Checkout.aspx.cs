using System;
using System.Data.SqlClient;

public partial class Checkout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("User.aspx?returnUrl=" +
                              Server.UrlEncode(Request.RawUrl));
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
            LoadCustomerDetails();
            LoadProduct();
        }
    }

    private void LoadCustomerDetails()
    {
        txtFullName.Text = Session["FullName"] != null
            ? Session["FullName"].ToString()
            : "";

        int userID = Convert.ToInt32(Session["UserID"]);

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT Mobile, Email
                FROM Users
                WHERE UserID = @UserID";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserID", userID);

            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                if (dr["Mobile"] != DBNull.Value)
                {
                    txtMobile.Text = dr["Mobile"].ToString();
                }
            }

            dr.Close();
        }
    }

    private void LoadProduct()
    {
        int productID;

        if (!int.TryParse(Request.QueryString["ProductID"], out productID))
        {
            lblMessage.Text = "Invalid product.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            btnContinue.Enabled = false;
            return;
        }

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    ProductName,
                    SellingPrice,
                    Quantity
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
                lblProductName.Text = dr["ProductName"].ToString();
                lblPrice.Text = Convert.ToDecimal(dr["SellingPrice"]).ToString("N2");
                lblQuantity.Text = dr["Quantity"].ToString();
            }
            else
            {
                lblMessage.Text = "Product is no longer available.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                btnContinue.Enabled = false;
            }

            dr.Close();
        }
    }

    protected void btnContinue_Click(object sender, EventArgs e)
    {
        if (txtFullName.Text.Trim() == "" ||
            txtMobile.Text.Trim() == "" ||
            txtAddress.Text.Trim() == "" ||
            txtCity.Text.Trim() == "" ||
            txtTaluka.Text.Trim() == "" ||
            txtDistrict.Text.Trim() == "" ||
            txtState.Text.Trim() == "" ||
            txtPincode.Text.Trim() == "")
        {
            lblMessage.Text = "Please fill all delivery address details.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }

        Session["DeliveryFullName"] = txtFullName.Text.Trim();
        Session["DeliveryMobile"] = txtMobile.Text.Trim();
        Session["DeliveryAddress"] = txtAddress.Text.Trim();
        Session["DeliveryCity"] = txtCity.Text.Trim();
        Session["DeliveryTaluka"] = txtTaluka.Text.Trim();
        Session["DeliveryDistrict"] = txtDistrict.Text.Trim();
        Session["DeliveryState"] = txtState.Text.Trim();
        Session["DeliveryPincode"] = txtPincode.Text.Trim();

        Response.Redirect("OrderReview.aspx?ProductID=" +
                          Request.QueryString["ProductID"]);
    }
}