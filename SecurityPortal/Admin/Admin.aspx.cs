using System;

public partial class AdminPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] == null ||
            Session["Role"].ToString() != "Admin")
        {
            Response.Redirect("../Login.aspx");

            return;
        }

        if (!IsPostBack)
        {
            lblName.Text =
                Session["FullName"].ToString();
        }
    }

    protected void btnDashboard_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminDashboard.aspx");
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();

        Response.Redirect("../Login.aspx");
    }
}