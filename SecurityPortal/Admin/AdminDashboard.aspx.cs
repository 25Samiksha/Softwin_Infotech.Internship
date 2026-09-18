using System;

public partial class Admin_AdminDashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] == null)
        {
            Response.Redirect("../Login.aspx");
            return;
        }
        if (Session["Role"].ToString() != "Admin")
        {
            Response.Redirect("../LoginDashboard.aspx");
            return;
        }

        if (!IsPostBack)
        {
            if (Session["FullName"] != null)
            {
                lblAdminName.Text =
                    Session["FullName"].ToString();
            }
            else
            {
                lblAdminName.Text = "Administrator";
            }
        }
    }
}