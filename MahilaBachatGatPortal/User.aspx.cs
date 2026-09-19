using System;
using System.Data.SqlClient;

public partial class User : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            Response.Redirect("Dashboard.aspx");
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

                // Store common user information
                Session["UserID"] = dr["UserID"].ToString();
                Session["Username"] = dr["Username"].ToString();
                Session["FullName"] = dr["FullName"].ToString();
                Session["Role"] = role;

                // Store Bachat Gat ID
                if (dr["BachatGatID"] != DBNull.Value)
                {
                    Session["BachatGatID"] =
                        Convert.ToInt32(dr["BachatGatID"]);
                }
                else
                {
                    Session["BachatGatID"] = null;
                }

                // Store Member ID for Member users
                if (dr["MemberID"] != DBNull.Value)
                {
                    Session["MemberID"] =
                        Convert.ToInt32(dr["MemberID"]);
                }
                else
                {
                    Session["MemberID"] = null;
                }

                // President must have an assigned Bachat Gat
                if (role == "President" &&
                    Session["BachatGatID"] == null)
                {
                    Session.Clear();

                    lblMessage.Text =
                        "This President account is not assigned to a Bachat Gat.";
                    lblMessage.ForeColor =
                        System.Drawing.Color.Red;

                    return;
                }

                // Member must have an assigned Bachat Gat
                // and Member record
                if (role == "Member")
                {
                    if (Session["BachatGatID"] == null ||
                        Session["MemberID"] == null)
                    {
                        Session.Clear();

                        lblMessage.Text =
                            "This Member account is not properly assigned.";
                        lblMessage.ForeColor =
                            System.Drawing.Color.Red;

                        return;
                    }
                }

                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                lblMessage.Text = "Invalid username or password.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

            dr.Close();
        }
    }
}