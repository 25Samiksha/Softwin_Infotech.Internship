using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Members : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RoleHelper.RequireManagement(this);
        if (!IsPostBack)
        {
            LoadBachatGat();
            LoadMembers();
        }
    }
    private void LoadBachatGat()
    {
        try
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "SELECT BachatGatID, GatName FROM BachatGat ORDER BY GatName";

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                ddlBachatGat.DataSource = dr;
                ddlBachatGat.DataTextField = "GatName";
                ddlBachatGat.DataValueField = "BachatGatID";
                ddlBachatGat.DataBind();

                dr.Close();
            }

            ddlBachatGat.Items.Insert(
                0,
                new ListItem("-- Select Bachat Gat --", "")
            );
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error loading Bachat Gat: " + ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }


    // ==========================================
    // LOAD MEMBERS
    // ==========================================

    private void LoadMembers()
    {
        try
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = @"
                    SELECT
                        M.MemberID,
                        M.MemberCode,
                        M.MemberName,
                        B.GatName,
                        M.Mobile,
                        M.Village,
                        M.Status
                    FROM Members M
                    INNER JOIN BachatGat B
                        ON M.BachatGatID = B.BachatGatID
                    ORDER BY M.MemberID DESC";

                SqlDataAdapter da =
                    new SqlDataAdapter(query, con);

                DataTable dt = new DataTable();

                da.Fill(dt);

                gvMembers.DataSource = dt;
                gvMembers.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text =
                "Error loading members: " + ex.Message;

            lblMessage.ForeColor =
                System.Drawing.Color.Red;
        }
    }


    // ==========================================
    // SAVE MEMBER
    // ==========================================

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlBachatGat.SelectedValue == "")
        {
            ShowMessage(
                "Please select Bachat Gat.",
                System.Drawing.Color.Red
            );

            return;
        }

        if (string.IsNullOrWhiteSpace(txtMemberCode.Text))
        {
            ShowMessage(
                "Please enter Member Code.",
                System.Drawing.Color.Red
            );

            return;
        }

        if (string.IsNullOrWhiteSpace(txtMemberName.Text))
        {
            ShowMessage(
                "Please enter Member Name.",
                System.Drawing.Color.Red
            );

            return;
        }

        if (string.IsNullOrWhiteSpace(txtJoinDate.Text))
        {
            ShowMessage(
                "Please select Join Date.",
                System.Drawing.Color.Red
            );

            return;
        }


        try
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();


                // ==================================
                // UPDATE
                // ==================================

                if (hfMemberID.Value != "")
                {
                    string query = @"
                        UPDATE Members
                        SET
                            BachatGatID = @BachatGatID,
                            MemberCode = @MemberCode,
                            MemberName = @MemberName,
                            FatherOrHusbandName = @FatherOrHusbandName,
                            DateOfBirth = @DateOfBirth,
                            Mobile = @Mobile,
                            Email = @Email,
                            Address = @Address,
                            Village = @Village,
                            Taluka = @Taluka,
                            District = @District,
                            JoinDate = @JoinDate,
                            Occupation = @Occupation,
                            Status = @Status
                        WHERE MemberID = @MemberID";


                    SqlCommand cmd =
                        new SqlCommand(query, con);


                    AddMemberParameters(cmd);


                    cmd.Parameters.AddWithValue(
                        "@MemberID",
                        hfMemberID.Value
                    );


                    cmd.ExecuteNonQuery();


                    ShowMessage(
                        "Member updated successfully!",
                        System.Drawing.Color.Green
                    );
                }


                // ==================================
                // INSERT
                // ==================================

                else
                {
                    string query = @"
                        INSERT INTO Members
                        (
                            BachatGatID,
                            MemberCode,
                            MemberName,
                            FatherOrHusbandName,
                            Gender,
                            DateOfBirth,
                            Mobile,
                            Email,
                            Address,
                            Village,
                            Taluka,
                            District,
                            JoinDate,
                            Occupation,
                            Status
                        )
                        VALUES
                        (
                            @BachatGatID,
                            @MemberCode,
                            @MemberName,
                            @FatherOrHusbandName,
                            'Female',
                            @DateOfBirth,
                            @Mobile,
                            @Email,
                            @Address,
                            @Village,
                            @Taluka,
                            @District,
                            @JoinDate,
                            @Occupation,
                            @Status
                        )";


                    SqlCommand cmd =
                        new SqlCommand(query, con);


                    AddMemberParameters(cmd);


                    cmd.ExecuteNonQuery();


                    ShowMessage(
                        "Member added successfully!",
                        System.Drawing.Color.Green
                    );
                }
            }


            ClearForm();

            LoadMembers();
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error saving member: " + ex.Message,
                System.Drawing.Color.Red
            );
        }
    }


    // ==========================================
    // ADD PARAMETERS
    // ==========================================

    private void AddMemberParameters(SqlCommand cmd)
    {
        cmd.Parameters.AddWithValue(
            "@BachatGatID",
            ddlBachatGat.SelectedValue
        );

        cmd.Parameters.AddWithValue(
            "@MemberCode",
            txtMemberCode.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@MemberName",
            txtMemberName.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@FatherOrHusbandName",
            txtFatherHusband.Text.Trim()
        );


        if (string.IsNullOrWhiteSpace(txtDOB.Text))
        {
            cmd.Parameters.AddWithValue(
                "@DateOfBirth",
                DBNull.Value
            );
        }
        else
        {
            cmd.Parameters.AddWithValue(
                "@DateOfBirth",
                Convert.ToDateTime(txtDOB.Text)
            );
        }


        cmd.Parameters.AddWithValue(
            "@Mobile",
            txtMobile.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@Email",
            txtEmail.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@Address",
            txtAddress.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@Village",
            txtVillage.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@Taluka",
            txtTaluka.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@District",
            txtDistrict.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@JoinDate",
            Convert.ToDateTime(txtJoinDate.Text)
        );

        cmd.Parameters.AddWithValue(
            "@Occupation",
            txtOccupation.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@Status",
            ddlStatus.SelectedValue
        );
    }


    // ==========================================
    // SEARCH MEMBER
    // ==========================================

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = @"
                    SELECT
                        M.MemberID,
                        M.MemberCode,
                        M.MemberName,
                        B.GatName,
                        M.Mobile,
                        M.Village,
                        M.Status
                    FROM Members M
                    INNER JOIN BachatGat B
                        ON M.BachatGatID = B.BachatGatID
                    WHERE
                        M.MemberName LIKE @Search
                        OR M.Mobile LIKE @Search
                        OR M.MemberCode LIKE @Search
                    ORDER BY M.MemberID DESC";


                SqlCommand cmd =
                    new SqlCommand(query, con);


                cmd.Parameters.AddWithValue(
                    "@Search",
                    "%" + txtSearch.Text.Trim() + "%"
                );


                SqlDataAdapter da =
                    new SqlDataAdapter(cmd);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);


                gvMembers.DataSource = dt;
                gvMembers.DataBind();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Search error: " + ex.Message,
                System.Drawing.Color.Red
            );
        }
    }


    // ==========================================
    // EDIT / DELETE
    // ==========================================

    protected void gvMembers_RowCommand(
        object sender,
        GridViewCommandEventArgs e)
    {
        int memberID =
            Convert.ToInt32(e.CommandArgument);


        if (e.CommandName == "EditMember")
        {
            LoadMemberForEdit(memberID);
        }


        if (e.CommandName == "DeleteMember")
        {
            DeleteMember(memberID);
        }
    }


    // ==========================================
    // LOAD MEMBER FOR EDIT
    // ==========================================

    private void LoadMemberForEdit(int memberID)
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query = @"
                    SELECT *
                    FROM Members
                    WHERE MemberID = @MemberID";


                SqlCommand cmd =
                    new SqlCommand(query, con);


                cmd.Parameters.AddWithValue(
                    "@MemberID",
                    memberID
                );


                con.Open();


                SqlDataReader dr =
                    cmd.ExecuteReader();


                if (dr.Read())
                {
                    hfMemberID.Value =
                        dr["MemberID"].ToString();

                    ddlBachatGat.SelectedValue =
                        dr["BachatGatID"].ToString();

                    txtMemberCode.Text =
                        dr["MemberCode"].ToString();

                    txtMemberName.Text =
                        dr["MemberName"].ToString();

                    txtFatherHusband.Text =
                        dr["FatherOrHusbandName"].ToString();


                    if (dr["DateOfBirth"] != DBNull.Value)
                    {
                        txtDOB.Text =
                            Convert.ToDateTime(
                                dr["DateOfBirth"]
                            ).ToString("yyyy-MM-dd");
                    }


                    txtMobile.Text =
                        dr["Mobile"].ToString();

                    txtEmail.Text =
                        dr["Email"].ToString();

                    txtAddress.Text =
                        dr["Address"].ToString();

                    txtVillage.Text =
                        dr["Village"].ToString();

                    txtTaluka.Text =
                        dr["Taluka"].ToString();

                    txtDistrict.Text =
                        dr["District"].ToString();


                    if (dr["JoinDate"] != DBNull.Value)
                    {
                        txtJoinDate.Text =
                            Convert.ToDateTime(
                                dr["JoinDate"]
                            ).ToString("yyyy-MM-dd");
                    }


                    txtOccupation.Text =
                        dr["Occupation"].ToString();

                    ddlStatus.SelectedValue =
                        dr["Status"].ToString();


                    btnSave.Text =
                        "Update Member";


                    ShowMessage(
                        "Member loaded for editing.",
                        System.Drawing.Color.Blue
                    );
                }


                dr.Close();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error loading member: " + ex.Message,
                System.Drawing.Color.Red
            );
        }
    }


    // ==========================================
    // DELETE MEMBER
    // ==========================================

    private void DeleteMember(int memberID)
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query =
                    "DELETE FROM Members WHERE MemberID = @MemberID";


                SqlCommand cmd =
                    new SqlCommand(query, con);


                cmd.Parameters.AddWithValue(
                    "@MemberID",
                    memberID
                );


                con.Open();


                cmd.ExecuteNonQuery();


                ShowMessage(
                    "Member deleted successfully!",
                    System.Drawing.Color.Green
                );
            }


            LoadMembers();
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error deleting member: " + ex.Message,
                System.Drawing.Color.Red
            );
        }
    }


    // ==========================================
    // CLEAR BUTTON
    // ==========================================

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearForm();
    }


    // ==========================================
    // CLEAR FORM
    // ==========================================

    private void ClearForm()
    {
        hfMemberID.Value = "";

        ddlBachatGat.SelectedIndex = 0;

        txtMemberCode.Text = "";
        txtMemberName.Text = "";
        txtFatherHusband.Text = "";
        txtDOB.Text = "";
        txtMobile.Text = "";
        txtEmail.Text = "";
        txtAddress.Text = "";
        txtVillage.Text = "";
        txtTaluka.Text = "";
        txtDistrict.Text = "";
        txtJoinDate.Text = "";
        txtOccupation.Text = "";

        ddlStatus.SelectedValue = "Active";

        btnSave.Text = "Save Member";
    }


    // ==========================================
    // MESSAGE
    // ==========================================

    private void ShowMessage(
        string message,
        System.Drawing.Color color)
    {
        lblMessage.Text = message;
        lblMessage.ForeColor = color;
    }
}