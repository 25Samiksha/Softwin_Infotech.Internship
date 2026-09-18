using System;

public partial class UserDashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] == null)
        {
            Response.Redirect("../Login.aspx");
            return;
        }

        string role = Session["Role"].ToString();

        if (role != "Admin" && role != "User")
        {
            Response.Redirect("../Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            lblName.Text =
                Convert.ToString(Session["FullName"]);

            lblUsername.Text =
                Convert.ToString(Session["Username"]);

            lblFullName.Text =
                Convert.ToString(Session["FullName"]);

            lblEmail.Text =
                Convert.ToString(Session["Email"]);

            if (role == "User")
            {
                lblUserID.Text =
                    Convert.ToString(Session["UserID"]);

                lblPhone.Text =
                    Convert.ToString(Session["Phone"]);
            }
            else if (role == "Admin")
            {
                lblUserID.Text =
                    "Admin Access";

                lblPhone.Text =
                    "Admin Account";
            }
        }
    }
}