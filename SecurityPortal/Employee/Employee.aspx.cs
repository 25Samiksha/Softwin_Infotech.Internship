using System;

public partial class EmployeePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] == null)
        {
            Response.Redirect("../Login.aspx");
            return;
        }

        string role = Session["Role"].ToString();

        if (role != "Employee" && role != "Admin")
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
        Response.Redirect("EmployeeDashboard.aspx");
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();

        Response.Redirect("../Login.aspx");
    }
}