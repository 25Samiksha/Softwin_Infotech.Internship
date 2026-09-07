using System;

public partial class EmployeeDashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check whether user is logged in
        if (Session["Role"] == null)
        {
            Response.Redirect("../Login.aspx");
            return;
        }

        string role = Session["Role"].ToString();

        // Only Admin and Employee are allowed
        if (role != "Admin" && role != "Employee")
        {
            Response.Redirect("../Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            lblName.Text = Convert.ToString(Session["FullName"]);
            lblUsername.Text = Convert.ToString(Session["Username"]);
            lblFullName.Text = Convert.ToString(Session["FullName"]);
            lblEmail.Text = Convert.ToString(Session["Email"]);

            if (role == "Employee")
            {
                lblEmployeeID.Text =
                    Convert.ToString(Session["EmployeeID"]);

                lblDepartment.Text =
                    Convert.ToString(Session["Department"]);

                lblDesignation.Text =
                    Convert.ToString(Session["Designation"]);

                lblPhone.Text =
                    Convert.ToString(Session["Phone"]);
            }
            else if (role == "Admin")
            {
                lblEmployeeID.Text = "Admin Access";
                lblDepartment.Text = "All Departments";
                lblDesignation.Text = "Administrator";
                lblPhone.Text = "Admin Account";
            }
        }
    }
}