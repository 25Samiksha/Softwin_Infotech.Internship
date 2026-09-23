using System;
using System.Data.SqlClient;
using System.IO;

public partial class Payment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("User.aspx?returnUrl=" + Server.UrlEncode(Request.RawUrl));
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
            pnlOnlinePayment.Visible = false;
            LoadAmount();
        }
    }

    private void LoadAmount()
    {
        int userID = Convert.ToInt32(Session["UserID"]);

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT ISNULL(SUM(C.Quantity * P.SellingPrice), 0)
                FROM Cart C
                INNER JOIN Products P ON C.ProductID = P.ProductID
                WHERE C.UserID = @UserID
                AND P.Status = 'Available'
                AND P.Quantity > 0
                AND P.Quantity >= C.Quantity";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserID", userID);

            con.Open();

            object result = cmd.ExecuteScalar();
            decimal amount = result == null || result == DBNull.Value
                ? 0
                : Convert.ToDecimal(result);

            lblTotalAmount.Text = amount.ToString("N2");
            lblQRAmount.Text = amount.ToString("N2");

            if (amount <= 0)
            {
                lblMessage.Text = "Your cart is empty or products are no longer available.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                btnPlaceOrder.Enabled = false;
            }
            else
            {
                btnPlaceOrder.Enabled = true;
            }
        }
    }

    protected void rbCOD_CheckedChanged(object sender, EventArgs e)
    {
        pnlOnlinePayment.Visible = false;
        chkPaymentCompleted.Checked = false;
        txtUTRNumber.Text = "";
        lblMessage.Text = "";
    }

    protected void rbOnline_CheckedChanged(object sender, EventArgs e)
    {
        pnlOnlinePayment.Visible = true;
        chkPaymentCompleted.Checked = false;
        txtUTRNumber.Text = "";
        lblMessage.Text = "";
        LoadAmount();
    }

    protected void btnPlaceOrder_Click(object sender, EventArgs e)
    {
        string paymentMode;
        string utrNumber = "";
        string screenshotPath = "";

        if (rbCOD.Checked)
        {
            paymentMode = "Cash on Delivery";
        }
        else if (rbOnline.Checked)
        {
            if (!chkPaymentCompleted.Checked)
            {
                ShowMessage("Please complete the payment using the QR code.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUTRNumber.Text))
            {
                ShowMessage("Please enter the UTR number.");
                return;
            }

            if (!fuPaymentScreenshot.HasFile)
            {
                ShowMessage("Please upload the payment screenshot.");
                return;
            }

            string extension = Path.GetExtension(fuPaymentScreenshot.FileName).ToLower();

            if (extension != ".jpg" &&
                extension != ".jpeg" &&
                extension != ".png")
            {
                ShowMessage("Only JPG, JPEG and PNG screenshots are allowed.");
                return;
            }

            string folderPath = Server.MapPath("~/PaymentScreenshots/");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fileName = Guid.NewGuid().ToString() + extension;

            fuPaymentScreenshot.SaveAs(
                Path.Combine(folderPath, fileName));

            screenshotPath = "PaymentScreenshots/" + fileName;
            utrNumber = txtUTRNumber.Text.Trim();
            paymentMode = "Online Payment";
        }
        else
        {
            ShowMessage("Please select a payment method.");
            return;
        }

        Session["PaymentMode"] = paymentMode;
        Session["UTRNumber"] = utrNumber;
        Session["PaymentScreenshot"] = screenshotPath;

        Response.Redirect("OrderPlaced.aspx");
    }

    private void ShowMessage(string message)
    {
        lblMessage.Text = message;
        lblMessage.ForeColor = System.Drawing.Color.Red;
    }
}