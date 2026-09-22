using System;
using System.Data.SqlClient;

public partial class User : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserID"] != null)
            {
                string returnUrl = Request.QueryString["returnUrl"];

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    Response.Redirect(returnUrl);
                }
                else if (Session["Role"] != null &&
                         Session["Role"].ToString().Equals("Customer", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Redirect("PublicProducts.aspx");
                }
                else
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text.Trim();

        if (username == "" || password == "")
        {
            lblMessage.Text = "Please enter username and password.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    U.UserID,
                    U.Username,
                    U.Password,
                    U.FullName,
                    U.Role,
                    U.BachatGatID,
                    M.MemberID
                FROM Users U
                LEFT JOIN Members M
                    ON U.UserID = M.UserID
                WHERE U.Username = @Username
                AND U.Password = @Password
                AND U.IsActive = 1";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);

            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                string role = dr["Role"].ToString();

                Session["UserID"] = dr["UserID"].ToString();
                Session["Username"] = dr["Username"].ToString();
                Session["FullName"] = dr["FullName"].ToString();
                Session["Role"] = role;

                if (dr["BachatGatID"] != DBNull.Value)
                {
                    Session["BachatGatID"] = Convert.ToInt32(dr["BachatGatID"]);
                }
                else
                {
                    Session["BachatGatID"] = null;
                }

                if (dr["MemberID"] != DBNull.Value)
                {
                    Session["MemberID"] = Convert.ToInt32(dr["MemberID"]);
                }
                else
                {
                    Session["MemberID"] = null;
                }

                if (role.Equals("President", StringComparison.OrdinalIgnoreCase)
                    && Session["BachatGatID"] == null)
                {
                    Session.Clear();
                    lblMessage.Text = "This President account is not assigned to a Bachat Gat.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    dr.Close();
                    return;
                }

                if (role.Equals("Member", StringComparison.OrdinalIgnoreCase))
                {
                    if (Session["BachatGatID"] == null ||
                        Session["MemberID"] == null)
                    {
                        Session.Clear();
                        lblMessage.Text = "This Member account is not properly assigned.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        dr.Close();
                        return;
                    }
                }

                string returnUrl = Request.QueryString["returnUrl"];

                dr.Close();

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    Response.Redirect(returnUrl);
                }
                else if (role.Equals("Customer", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Redirect("PublicProducts.aspx");
                }
                else
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
            else
            {
                dr.Close();
                lblMessage.Text = "Invalid username or password.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void btnCreateAccount_Click(object sender, EventArgs e)
    {
        string fullName = txtFullName.Text.Trim();
        string mobile = txtMobile.Text.Trim();
        string email = txtEmail.Text.Trim();
        string username = txtNewUsername.Text.Trim();
        string password = txtNewPassword.Text.Trim();
        string confirmPassword = txtConfirmPassword.Text.Trim();

        if (fullName == "" ||
            mobile == "" ||
            email == "" ||
            username == "" ||
            password == "" ||
            confirmPassword == "")
        {
            lblSignupMessage.Text = "Please fill all fields.";
            lblSignupMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }

        if (password != confirmPassword)
        {
            lblSignupMessage.Text = "Password and confirm password do not match.";
            lblSignupMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string checkQuery = @"
                SELECT COUNT(*)
                FROM Users
                WHERE Username = @Username";

            SqlCommand checkCmd = new SqlCommand(checkQuery, con);
            checkCmd.Parameters.AddWithValue("@Username", username);

            con.Open();

            int existingUser = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (existingUser > 0)
            {
                lblSignupMessage.Text = "Username already exists.";
                lblSignupMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string insertQuery = @"
                INSERT INTO Users
                (
                    Username,
                    Password,
                    FullName,
                    Role,
                    Mobile,
                    Email,
                    IsActive,
                    CreatedDate,
                    BachatGatID
                )
                VALUES
                (
                    @Username,
                    @Password,
                    @FullName,
                    'Customer',
                    @Mobile,
                    @Email,
                    1,
                    GETDATE(),
                    NULL
                );

                SELECT SCOPE_IDENTITY();";

            SqlCommand insertCmd = new SqlCommand(insertQuery, con);

            insertCmd.Parameters.AddWithValue("@Username", username);
            insertCmd.Parameters.AddWithValue("@Password", password);
            insertCmd.Parameters.AddWithValue("@FullName", fullName);
            insertCmd.Parameters.AddWithValue("@Mobile", mobile);
            insertCmd.Parameters.AddWithValue("@Email", email);

            int userID = Convert.ToInt32(insertCmd.ExecuteScalar());

            Session["UserID"] = userID;
            Session["Username"] = username;
            Session["FullName"] = fullName;
            Session["Role"] = "Customer";
            Session["BachatGatID"] = null;
            Session["MemberID"] = null;

            string returnUrl = Request.QueryString["returnUrl"];

            if (!string.IsNullOrEmpty(returnUrl))
            {
                Response.Redirect(returnUrl);
            }
            else
            {
                Response.Redirect("PublicProducts.aspx");
            }
        }
    }
}