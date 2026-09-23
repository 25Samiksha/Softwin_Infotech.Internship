using System;
using System.Data.SqlClient;

public partial class User : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string mode = Request.QueryString["mode"];

            if (Session["UserID"] != null && string.IsNullOrEmpty(mode))
            {
                string returnUrl = Request.QueryString["returnUrl"];

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    Response.Redirect(returnUrl);
                    return;
                }

                if (Session["Role"] != null &&
                    Session["Role"].ToString().Equals("Customer", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Redirect("PublicProducts.aspx");
                    return;
                }

                Response.Redirect("Dashboard.aspx");
                return;
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

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (!dr.Read())
                {
                    lblMessage.Text = "Invalid username or password.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                int userID = Convert.ToInt32(dr["UserID"]);
                string loggedUsername = dr["Username"].ToString();
                string fullName = dr["FullName"].ToString();
                string role = dr["Role"].ToString();

                int bachatGatID = 0;
                int memberID = 0;

                if (dr["BachatGatID"] != DBNull.Value)
                {
                    bachatGatID = Convert.ToInt32(dr["BachatGatID"]);
                }

                if (dr["MemberID"] != DBNull.Value)
                {
                    memberID = Convert.ToInt32(dr["MemberID"]);
                }

                Session["UserID"] = userID;
                Session["Username"] = loggedUsername;
                Session["FullName"] = fullName;
                Session["Role"] = role;

                if (dr["BachatGatID"] != DBNull.Value)
                {
                    Session["BachatGatID"] = bachatGatID;
                }
                else
                {
                    Session["BachatGatID"] = null;
                }

                if (dr["MemberID"] != DBNull.Value)
                {
                    Session["MemberID"] = memberID;
                }
                else
                {
                    Session["MemberID"] = null;
                }

                if (role.Equals("President", StringComparison.OrdinalIgnoreCase) &&
                    Session["BachatGatID"] == null)
                {
                    Session.Clear();
                    lblMessage.Text = "This President account is not assigned to a Bachat Gat.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (role.Equals("Secretary", StringComparison.OrdinalIgnoreCase) &&
                    Session["BachatGatID"] == null)
                {
                    Session.Clear();
                    lblMessage.Text = "This Secretary account is not assigned to a Bachat Gat.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
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
                        return;
                    }
                }

                string returnUrl = Request.QueryString["returnUrl"];

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    Response.Redirect(returnUrl);
                    return;
                }

                if (role.Equals("Customer", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Redirect("PublicProducts.aspx");
                    return;
                }

                Response.Redirect("Dashboard.aspx");
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
                return;
            }

            Response.Redirect("PublicProducts.aspx");
        }
    }
}