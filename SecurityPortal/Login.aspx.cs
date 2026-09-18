using System;
using System.Configuration;
using System.Data.SqlClient;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text.Trim();

        if (username == "" || password == "")
        {
            lblMessage.Text =
                "Please enter username and password.";

            return;
        }

        string connectionString =
            ConfigurationManager
            .ConnectionStrings[
                "SecurityPortalConnection"
            ].ConnectionString;

        using (SqlConnection con =
               new SqlConnection(connectionString))
        {
            con.Open();


            string adminQuery = @"
                SELECT AdminID, Username, FullName, Email
                FROM Admin
                WHERE Username = @Username
                AND Password = @Password
                AND IsActive = 1";

            using (SqlCommand cmd =
                   new SqlCommand(adminQuery, con))
            {
                cmd.Parameters.AddWithValue(
                    "@Username", username);

                cmd.Parameters.AddWithValue(
                    "@Password", password);

                using (SqlDataReader reader =
                       cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Session["AdminID"] =
                            reader["AdminID"].ToString();

                        Session["Username"] =
                            reader["Username"].ToString();

                        Session["FullName"] =
                            reader["FullName"].ToString();

                        Session["Email"] =
                            reader["Email"].ToString();

                        Session["Role"] = "Admin";

                        reader.Close();
                        Response.Redirect(
                            "Admin/AdminDashboard.aspx");

                        return;
                    }
                }
            }


            string employeeQuery = @"
                SELECT EmployeeID, Username, FullName,
                       Email, Department, Designation, Phone
                FROM Employee
                WHERE Username = @Username
                AND Password = @Password
                AND IsActive = 1";

            using (SqlCommand cmd =
                   new SqlCommand(employeeQuery, con))
            {
                cmd.Parameters.AddWithValue(
                    "@Username", username);

                cmd.Parameters.AddWithValue(
                    "@Password", password);

                using (SqlDataReader reader =
                       cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Session["EmployeeID"] =
                            reader["EmployeeID"].ToString();

                        Session["Username"] =
                            reader["Username"].ToString();

                        Session["FullName"] =
                            reader["FullName"].ToString();

                        Session["Email"] =
                            reader["Email"].ToString();

                        Session["Department"] =
                            reader["Department"].ToString();

                        Session["Designation"] =
                            reader["Designation"].ToString();

                        Session["Phone"] =
                            reader["Phone"].ToString();

                        Session["Role"] = "Employee";

                        reader.Close();

                        Response.Redirect(
                            "Employee/EmployeeDashboard.aspx");

                        return;
                    }
                }
            }

            string userQuery = @"
                SELECT UserID, Username, FullName,
                       Email, Phone
                FROM Users
                WHERE Username = @Username
                AND Password = @Password
                AND IsActive = 1";

            using (SqlCommand cmd =
                   new SqlCommand(userQuery, con))
            {
                cmd.Parameters.AddWithValue(
                    "@Username", username);

                cmd.Parameters.AddWithValue(
                    "@Password", password);

                using (SqlDataReader reader =
                       cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Session["UserID"] =
                            reader["UserID"].ToString();

                        Session["Username"] =
                            reader["Username"].ToString();

                        Session["FullName"] =
                            reader["FullName"].ToString();

                        Session["Email"] =
                            reader["Email"].ToString();

                        Session["Phone"] =
                            reader["Phone"].ToString();

                        Session["Role"] = "User";

                        reader.Close();

                        Response.Redirect(
                            "User/UserDashboard.aspx");

                        return;
                    }
                }
            }
        }

        lblMessage.Text =
            "Invalid username or password.";
    }
}