using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_ManageUsers : System.Web.UI.Page
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
            LoadUsers();
        }
    }


    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        string fullName = txtFullName.Text.Trim();
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text.Trim();
        string email = txtEmail.Text.Trim();
        string phone = txtPhone.Text.Trim();


        if (fullName == "" ||
            username == "" ||
            password == "" ||
            email == "" ||
            phone == "")
        {
            lblMessage.Text =
                "Please fill all fields.";

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

            string checkQuery = @"
                SELECT COUNT(*)
                FROM Users
                WHERE Username = @Username";


            using (SqlCommand checkCmd =
                   new SqlCommand(checkQuery, con))
            {
                checkCmd.Parameters.AddWithValue(
                    "@Username", username);


                int count =
                    Convert.ToInt32(
                        checkCmd.ExecuteScalar());


                if (count > 0)
                {
                    lblMessage.Text =
                        "Username already exists.";

                    return;
                }
            }

            string insertQuery = @"
                INSERT INTO Users
                (
                    Username,
                    Password,
                    FullName,
                    Email,
                    Phone,
                    IsActive
                )
                VALUES
                (
                    @Username,
                    @Password,
                    @FullName,
                    @Email,
                    @Phone,
                    1
                )";


            using (SqlCommand cmd =
                   new SqlCommand(insertQuery, con))
            {
                cmd.Parameters.AddWithValue(
                    "@Username", username);

                cmd.Parameters.AddWithValue(
                    "@Password", password);

                cmd.Parameters.AddWithValue(
                    "@FullName", fullName);

                cmd.Parameters.AddWithValue(
                    "@Email", email);

                cmd.Parameters.AddWithValue(
                    "@Phone", phone);


                cmd.ExecuteNonQuery();
            }
        }


        lblMessage.Text =
            "User added successfully.";

        txtFullName.Text = "";
        txtUsername.Text = "";
        txtPassword.Text = "";
        txtEmail.Text = "";
        txtPhone.Text = "";


        LoadUsers();
    }


    private void LoadUsers()
    {
        string connectionString =
            ConfigurationManager
            .ConnectionStrings[
                "SecurityPortalConnection"
            ].ConnectionString;


        using (SqlConnection con =
               new SqlConnection(connectionString))
        {
            string query = @"
                SELECT
                    UserID,
                    Username,
                    FullName,
                    Email,
                    Phone,
                    IsActive
                FROM Users
                ORDER BY UserID DESC";


            using (SqlCommand cmd =
                   new SqlCommand(query, con))
            {
                using (SqlDataAdapter da =
                       new SqlDataAdapter(cmd))
                {
                    DataTable dt =
                        new DataTable();

                    da.Fill(dt);

                    gvUsers.DataSource = dt;

                    gvUsers.DataBind();
                }
            }
        }
    }
}