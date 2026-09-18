using System;
using System.Data;
using System.Data.SqlClient;

public partial class Users : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RoleHelper.RequireAdmin(this);

        if (!IsPostBack)
        {
            LoadUsers();
        }
    }


    // =========================================================
    // LOAD USERS
    // =========================================================

    private void LoadUsers()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    UserID,
                    Username,
                    FullName,
                    Role,
                    Mobile,
                    Email,
                    IsActive,
                    CreatedDate
                FROM Users
                ORDER BY UserID DESC";

            SqlDataAdapter da = new SqlDataAdapter(query, con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvUsers.DataSource = dt;
            gvUsers.DataBind();
        }
    }


    // =========================================================
    // SAVE USER
    // =========================================================

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text.Trim();
        string fullName = txtFullName.Text.Trim();
        string role = ddlRole.SelectedValue;
        string mobile = txtMobile.Text.Trim();
        string email = txtEmail.Text.Trim();

        if (username == "")
        {
            ShowMessage("Please enter username.", "danger");
            return;
        }

        if (fullName == "")
        {
            ShowMessage("Please enter full name.", "danger");
            return;
        }

        if (role == "")
        {
            ShowMessage("Please select role.", "danger");
            return;
        }


        int userID = 0;

        if (hfUserID.Value != "")
        {
            int.TryParse(hfUserID.Value, out userID);
        }


        // =====================================================
        // ADD NEW USER
        // =====================================================

        if (userID == 0)
        {
            if (password == "")
            {
                ShowMessage("Please enter password for new user.", "danger");
                return;
            }


            using (SqlConnection con = DBHelper.GetConnection())
            {
                string checkQuery = @"
                    SELECT COUNT(*)
                    FROM Users
                    WHERE Username = @Username";

                SqlCommand checkCmd =
                    new SqlCommand(checkQuery, con);

                checkCmd.Parameters.AddWithValue(
                    "@Username",
                    username);

                con.Open();

                int count =
                    Convert.ToInt32(checkCmd.ExecuteScalar());

                con.Close();


                if (count > 0)
                {
                    ShowMessage(
                        "Username already exists. Please use another username.",
                        "danger");

                    return;
                }
            }


            using (SqlConnection con = DBHelper.GetConnection())
            {
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
                        CreatedDate
                    )
                    VALUES
                    (
                        @Username,
                        @Password,
                        @FullName,
                        @Role,
                        @Mobile,
                        @Email,
                        @IsActive,
                        GETDATE()
                    )";

                SqlCommand cmd =
                    new SqlCommand(insertQuery, con);

                cmd.Parameters.AddWithValue(
                    "@Username",
                    username);

                cmd.Parameters.AddWithValue(
                    "@Password",
                    password);

                cmd.Parameters.AddWithValue(
                    "@FullName",
                    fullName);

                cmd.Parameters.AddWithValue(
                    "@Role",
                    role);

                cmd.Parameters.AddWithValue(
                    "@Mobile",
                    mobile);

                cmd.Parameters.AddWithValue(
                    "@Email",
                    email);

                cmd.Parameters.AddWithValue(
                    "@IsActive",
                    Convert.ToBoolean(
                        Convert.ToInt32(
                            ddlStatus.SelectedValue)));

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }


            ShowMessage(
                "User added successfully.",
                "success");
        }


        // =====================================================
        // UPDATE EXISTING USER
        // =====================================================

        else
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                string checkQuery = @"
                    SELECT COUNT(*)
                    FROM Users
                    WHERE Username = @Username
                    AND UserID <> @UserID";

                SqlCommand checkCmd =
                    new SqlCommand(checkQuery, con);

                checkCmd.Parameters.AddWithValue(
                    "@Username",
                    username);

                checkCmd.Parameters.AddWithValue(
                    "@UserID",
                    userID);

                con.Open();

                int count =
                    Convert.ToInt32(checkCmd.ExecuteScalar());

                con.Close();


                if (count > 0)
                {
                    ShowMessage(
                        "Username already exists.",
                        "danger");

                    return;
                }
            }


            using (SqlConnection con = DBHelper.GetConnection())
            {
                string updateQuery;


                // If password is blank, keep old password
                if (password == "")
                {
                    updateQuery = @"
                        UPDATE Users
                        SET
                            Username = @Username,
                            FullName = @FullName,
                            Role = @Role,
                            Mobile = @Mobile,
                            Email = @Email,
                            IsActive = @IsActive
                        WHERE UserID = @UserID";
                }
                else
                {
                    updateQuery = @"
                        UPDATE Users
                        SET
                            Username = @Username,
                            Password = @Password,
                            FullName = @FullName,
                            Role = @Role,
                            Mobile = @Mobile,
                            Email = @Email,
                            IsActive = @IsActive
                        WHERE UserID = @UserID";
                }


                SqlCommand cmd =
                    new SqlCommand(updateQuery, con);

                cmd.Parameters.AddWithValue(
                    "@Username",
                    username);

                if (password != "")
                {
                    cmd.Parameters.AddWithValue(
                        "@Password",
                        password);
                }

                cmd.Parameters.AddWithValue(
                    "@FullName",
                    fullName);

                cmd.Parameters.AddWithValue(
                    "@Role",
                    role);

                cmd.Parameters.AddWithValue(
                    "@Mobile",
                    mobile);

                cmd.Parameters.AddWithValue(
                    "@Email",
                    email);

                cmd.Parameters.AddWithValue(
                    "@IsActive",
                    Convert.ToBoolean(
                        Convert.ToInt32(
                            ddlStatus.SelectedValue)));

                cmd.Parameters.AddWithValue(
                    "@UserID",
                    userID);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }


            ShowMessage(
                "User updated successfully.",
                "success");
        }


        ClearForm();

        LoadUsers();
    }


    // =========================================================
    // GRID COMMANDS
    // =========================================================

    protected void gvUsers_RowCommand(
        object sender,
        System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int userID;

        if (!int.TryParse(
            e.CommandArgument.ToString(),
            out userID))
        {
            return;
        }


        // =====================================================
        // EDIT
        // =====================================================

        if (e.CommandName == "EditUser")
        {
            LoadUserForEdit(userID);
        }


        // =====================================================
        // ACTIVATE / DEACTIVATE
        // =====================================================

        if (e.CommandName == "ToggleUser")
        {
            ToggleUser(userID);
        }
    }


    // =========================================================
    // LOAD USER FOR EDIT
    // =========================================================

    private void LoadUserForEdit(int userID)
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    UserID,
                    Username,
                    FullName,
                    Role,
                    Mobile,
                    Email,
                    IsActive
                FROM Users
                WHERE UserID = @UserID";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@UserID",
                userID);

            con.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();

            if (dr.Read())
            {
                hfUserID.Value =
                    dr["UserID"].ToString();

                txtUsername.Text =
                    dr["Username"].ToString();

                txtFullName.Text =
                    dr["FullName"].ToString();

                ddlRole.SelectedValue =
                    dr["Role"].ToString();

                txtMobile.Text =
                    dr["Mobile"].ToString();

                txtEmail.Text =
                    dr["Email"].ToString();

                bool isActive =
                    Convert.ToBoolean(
                        dr["IsActive"]);

                ddlStatus.SelectedValue =
                    isActive ? "1" : "0";

                txtPassword.Text = "";

                btnSave.Text =
                    "Update User";
            }

            dr.Close();
        }
    }


    // =========================================================
    // ACTIVATE / DEACTIVATE USER
    // =========================================================

    private void ToggleUser(int userID)
    {
        // Prevent Admin from deactivating themselves
        if (userID == RoleHelper.GetUserID())
        {
            ShowMessage(
                "You cannot deactivate your own account.",
                "danger");

            return;
        }


        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                UPDATE Users
                SET IsActive =
                    CASE
                        WHEN IsActive = 1 THEN 0
                        ELSE 1
                    END
                WHERE UserID = @UserID";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@UserID",
                userID);

            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();
        }


        ShowMessage(
            "User status updated successfully.",
            "success");

        LoadUsers();
    }


    // =========================================================
    // SEARCH
    // =========================================================

    protected void btnSearch_Click(
        object sender,
        EventArgs e)
    {
        string search =
            txtSearch.Text.Trim();

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    UserID,
                    Username,
                    FullName,
                    Role,
                    Mobile,
                    Email,
                    IsActive,
                    CreatedDate
                FROM Users
                WHERE
                    Username LIKE @Search
                    OR FullName LIKE @Search
                    OR Role LIKE @Search
                    OR Mobile LIKE @Search
                    OR Email LIKE @Search
                ORDER BY UserID DESC";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@Search",
                "%" + search + "%");

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            gvUsers.DataSource = dt;
            gvUsers.DataBind();
        }
    }


    // =========================================================
    // SHOW ALL
    // =========================================================

    protected void btnShowAll_Click(
        object sender,
        EventArgs e)
    {
        txtSearch.Text = "";

        LoadUsers();
    }


    // =========================================================
    // CLEAR
    // =========================================================

    protected void btnClear_Click(
        object sender,
        EventArgs e)
    {
        ClearForm();

        lblMessage.Visible = false;
    }


    private void ClearForm()
    {
        hfUserID.Value = "";

        txtUsername.Text = "";

        txtPassword.Text = "";

        txtFullName.Text = "";

        ddlRole.SelectedIndex = 0;

        txtMobile.Text = "";

        txtEmail.Text = "";

        ddlStatus.SelectedValue = "1";

        btnSave.Text = "Save User";
    }


    // =========================================================
    // MESSAGE
    // =========================================================

    private void ShowMessage(
        string message,
        string type)
    {
        lblMessage.Text =
            message;

        lblMessage.Visible =
            true;

        lblMessage.CssClass =
            "alert alert-" + type;
    }
}