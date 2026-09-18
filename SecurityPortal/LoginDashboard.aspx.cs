using System;

public partial class LoginDashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] == null)
        {
            Response.Redirect("Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadDashboard();
        }
    }


    private void LoadDashboard()
    {
        string role = Session["Role"].ToString();

        string fullName = "";

        if (Session["FullName"] != null)
        {
            fullName = Session["FullName"].ToString();
        }
        lblFullName.Text = fullName;

        lblRole.Text = role;

        lblRoleBadge.Text = role;

        pnlAdmin.Visible = false;
        pnlEmployee.Visible = false;
        pnlUser.Visible = false;



        if (role.Equals("Admin",
            StringComparison.OrdinalIgnoreCase))
        {
            pnlAdmin.Visible = true;
            pnlEmployee.Visible = true;
            pnlUser.Visible = true;
        }


        else if (role.Equals("Employee",
            StringComparison.OrdinalIgnoreCase))
        {
            pnlEmployee.Visible = true;
        }

        else if (role.Equals("User",
            StringComparison.OrdinalIgnoreCase))
        {
            pnlUser.Visible = true;
        }

        else
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("Login.aspx");
        }
    }
}