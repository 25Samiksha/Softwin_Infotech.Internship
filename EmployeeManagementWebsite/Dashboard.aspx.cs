using System;
using System.Configuration;
using System.Data.SqlClient;

public partial class Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDashboard();
        }
    }

    private string GetConnectionString()
    {
        return ConfigurationManager
            .ConnectionStrings["EmployeeDB"]
            .ConnectionString;
    }

    private void BindDashboard()
    {
        using (SqlConnection con =
            new SqlConnection(GetConnectionString()))
        {
            con.Open();
            using (SqlCommand cmd =
                new SqlCommand(
                    "SELECT COUNT(*) FROM Employees",
                    con))
            {
                lblTotalEmployees.Text =
                    cmd.ExecuteScalar().ToString();
            }
            using (SqlCommand cmd =
                new SqlCommand(
                    "SELECT COUNT(*) FROM Employees WHERE Status = 'Active'",
                    con))
            {
                lblActiveEmployees.Text =
                    cmd.ExecuteScalar().ToString();
            }
            using (SqlCommand cmd =
                new SqlCommand(
                    "SELECT COUNT(*) FROM Employees WHERE Status = 'Inactive'",
                    con))
            {
                lblInactiveEmployees.Text =
                    cmd.ExecuteScalar().ToString();
            }
            using (SqlCommand cmd =
                new SqlCommand(
                    "SELECT COUNT(DISTINCT Department) FROM Employees",
                    con))
            {
                lblDepartments.Text =
                    cmd.ExecuteScalar().ToString();
            }
        }
    }
}