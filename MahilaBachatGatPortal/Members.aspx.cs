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


    // =========================================================
    // LOAD BACHAT GAT
    // =========================================================

    private void LoadBachatGat()
    {
        try
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query;

                // ADMIN
                if (RoleHelper.IsAdmin())
                {
                    query = @"
                        SELECT
                            BachatGatID,
                            GatName
                        FROM BachatGat
                        WHERE Status = 'Active'
                        ORDER BY GatName";
                }

                // PRESIDENT / SECRETARY
                else
                {
                    query = @"
                        SELECT
                            BachatGatID,
                            GatName
                        FROM BachatGat
                        WHERE BachatGatID = @BachatGatID
                        AND Status = 'Active'
                        ORDER BY GatName";
                }

                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    if (!RoleHelper.IsAdmin())
                    {
                        cmd.Parameters.AddWithValue(
                            "@BachatGatID",
                            RoleHelper.GetBachatGatID());
                    }

                    con.Open();

                    using (SqlDataReader dr =
                        cmd.ExecuteReader())
                    {
                        ddlBachatGat.DataSource = dr;

                        ddlBachatGat.DataTextField =
                            "GatName";

                        ddlBachatGat.DataValueField =
                            "BachatGatID";

                        ddlBachatGat.DataBind();
                    }
                }
            }


            // ADMIN
            // Add Select option

            if (RoleHelper.IsAdmin())
            {
                ddlBachatGat.Items.Insert(
                    0,
                    new ListItem(
                        "-- Select Bachat Gat --",
                        ""));
            }
            else
            {
                // PRESIDENT / SECRETARY
                // Automatically select their Gat

                if (ddlBachatGat.Items.Count > 0)
                {
                    ddlBachatGat.SelectedValue =
                        RoleHelper.GetBachatGatID().ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error loading Bachat Gat: " + ex.Message,
                System.Drawing.Color.Red);
        }
    }



    // =========================================================
    // LOAD MEMBERS
    // =========================================================

    private void LoadMembers()
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query;


                // ADMIN
                if (RoleHelper.IsAdmin())
                {
                    query = @"
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
                            ON M.BachatGatID =
                               B.BachatGatID
                        ORDER BY M.MemberID DESC";
                }

                // PRESIDENT / SECRETARY
                else
                {
                    query = @"
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
                            ON M.BachatGatID =
                               B.BachatGatID
                        WHERE M.BachatGatID =
                              @BachatGatID
                        ORDER BY M.MemberID DESC";
                }


                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    if (!RoleHelper.IsAdmin())
                    {
                        cmd.Parameters.AddWithValue(
                            "@BachatGatID",
                            RoleHelper.GetBachatGatID());
                    }


                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    DataTable dt =
                        new DataTable();

                    da.Fill(dt);

                    gvMembers.DataSource = dt;

                    gvMembers.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error loading members: " + ex.Message,
                System.Drawing.Color.Red);
        }
    }



    // =========================================================
    // SAVE MEMBER
    // =========================================================

    protected void btnSave_Click(
        object sender,
        EventArgs e)
    {
        // -----------------------------------------
        // VALIDATION
        // -----------------------------------------

        if (ddlBachatGat.SelectedValue == "")
        {
            ShowMessage(
                "Please select Bachat Gat.",
                System.Drawing.Color.Red);

            return;
        }


        if (string.IsNullOrWhiteSpace(
            txtMemberCode.Text))
        {
            ShowMessage(
                "Please enter Member Code.",
                System.Drawing.Color.Red);

            return;
        }


        if (string.IsNullOrWhiteSpace(
            txtMemberName.Text))
        {
            ShowMessage(
                "Please enter Member Name.",
                System.Drawing.Color.Red);

            return;
        }


        if (string.IsNullOrWhiteSpace(
            txtJoinDate.Text))
        {
            ShowMessage(
                "Please select Join Date.",
                System.Drawing.Color.Red);

            return;
        }


        // -----------------------------------------
        // DATE VALIDATION
        // -----------------------------------------

        DateTime joinDate;

        if (!DateTime.TryParse(
            txtJoinDate.Text.Trim(),
            out joinDate))
        {
            ShowMessage(
                "Please enter a valid Join Date.",
                System.Drawing.Color.Red);

            return;
        }


        DateTime? dateOfBirth = null;

        if (!string.IsNullOrWhiteSpace(
            txtDOB.Text))
        {
            DateTime dob;

            if (!DateTime.TryParse(
                txtDOB.Text.Trim(),
                out dob))
            {
                ShowMessage(
                    "Please enter a valid Date of Birth.",
                    System.Drawing.Color.Red);

                return;
            }

            dateOfBirth = dob;
        }


        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                con.Open();


                // -----------------------------------------
                // SECURITY CHECK
                // -----------------------------------------

                int selectedBachatGatID =
                    Convert.ToInt32(
                        ddlBachatGat.SelectedValue);


                // PRESIDENT / SECRETARY
                // can ONLY use their own Gat

                if (!RoleHelper.IsAdmin())
                {
                    if (selectedBachatGatID !=
                        RoleHelper.GetBachatGatID())
                    {
                        ShowMessage(
                            "You cannot manage members of another Bachat Gat.",
                            System.Drawing.Color.Red);

                        return;
                    }
                }


                // -----------------------------------------
                // UPDATE
                // -----------------------------------------

                if (!string.IsNullOrEmpty(
                    hfMemberID.Value))
                {
                    string query;


                    // ADMIN
                    if (RoleHelper.IsAdmin())
                    {
                        query = @"
                            UPDATE Members
                            SET
                                BachatGatID =
                                    @BachatGatID,
                                MemberCode =
                                    @MemberCode,
                                MemberName =
                                    @MemberName,
                                FatherOrHusbandName =
                                    @FatherOrHusbandName,
                                DateOfBirth =
                                    @DateOfBirth,
                                Mobile =
                                    @Mobile,
                                Email =
                                    @Email,
                                Address =
                                    @Address,
                                Village =
                                    @Village,
                                Taluka =
                                    @Taluka,
                                District =
                                    @District,
                                JoinDate =
                                    @JoinDate,
                                Occupation =
                                    @Occupation,
                                Status =
                                    @Status
                            WHERE MemberID =
                                  @MemberID";
                    }

                    // PRESIDENT / SECRETARY
                    else
                    {
                        query = @"
                            UPDATE Members
                            SET
                                BachatGatID =
                                    @BachatGatID,
                                MemberCode =
                                    @MemberCode,
                                MemberName =
                                    @MemberName,
                                FatherOrHusbandName =
                                    @FatherOrHusbandName,
                                DateOfBirth =
                                    @DateOfBirth,
                                Mobile =
                                    @Mobile,
                                Email =
                                    @Email,
                                Address =
                                    @Address,
                                Village =
                                    @Village,
                                Taluka =
                                    @Taluka,
                                District =
                                    @District,
                                JoinDate =
                                    @JoinDate,
                                Occupation =
                                    @Occupation,
                                Status =
                                    @Status
                            WHERE MemberID =
                                  @MemberID
                            AND BachatGatID =
                                  @BachatGatID";
                    }


                    using (SqlCommand cmd =
                        new SqlCommand(query, con))
                    {
                        AddMemberParameters(
                            cmd,
                            joinDate,
                            dateOfBirth);

                        cmd.Parameters.AddWithValue(
                            "@MemberID",
                            Convert.ToInt32(
                                hfMemberID.Value));


                        int rows =
                            cmd.ExecuteNonQuery();


                        if (rows == 0)
                        {
                            ShowMessage(
                                "Member not found or you do not have permission to update this member.",
                                System.Drawing.Color.Red);

                            return;
                        }
                    }


                    ShowMessage(
                        "Member updated successfully!",
                        System.Drawing.Color.Green);
                }


                // -----------------------------------------
                // INSERT
                // -----------------------------------------

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
                            Status,
                            CreatedDate
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
                            @Status,
                            GETDATE()
                        )";


                    using (SqlCommand cmd =
                        new SqlCommand(query, con))
                    {
                        AddMemberParameters(
                            cmd,
                            joinDate,
                            dateOfBirth);

                        cmd.ExecuteNonQuery();
                    }


                    ShowMessage(
                        "Member added successfully!",
                        System.Drawing.Color.Green);
                }
            }


            ClearForm();

            LoadMembers();
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error saving member: " + ex.Message,
                System.Drawing.Color.Red);
        }
    }



    // =========================================================
    // ADD PARAMETERS
    // =========================================================

    private void AddMemberParameters(
        SqlCommand cmd,
        DateTime joinDate,
        DateTime? dateOfBirth)
    {
        cmd.Parameters.AddWithValue(
            "@BachatGatID",
            Convert.ToInt32(
                ddlBachatGat.SelectedValue));


        cmd.Parameters.AddWithValue(
            "@MemberCode",
            txtMemberCode.Text.Trim());


        cmd.Parameters.AddWithValue(
            "@MemberName",
            txtMemberName.Text.Trim());


        cmd.Parameters.AddWithValue(
            "@FatherOrHusbandName",
            txtFatherHusband.Text.Trim());


        if (dateOfBirth.HasValue)
        {
            cmd.Parameters.AddWithValue(
                "@DateOfBirth",
                dateOfBirth.Value);
        }
        else
        {
            cmd.Parameters.AddWithValue(
                "@DateOfBirth",
                DBNull.Value);
        }


        cmd.Parameters.AddWithValue(
            "@Mobile",
            txtMobile.Text.Trim());


        cmd.Parameters.AddWithValue(
            "@Email",
            txtEmail.Text.Trim());


        cmd.Parameters.AddWithValue(
            "@Address",
            txtAddress.Text.Trim());


        cmd.Parameters.AddWithValue(
            "@Village",
            txtVillage.Text.Trim());


        cmd.Parameters.AddWithValue(
            "@Taluka",
            txtTaluka.Text.Trim());


        cmd.Parameters.AddWithValue(
            "@District",
            txtDistrict.Text.Trim());


        cmd.Parameters.AddWithValue(
            "@JoinDate",
            joinDate);


        cmd.Parameters.AddWithValue(
            "@Occupation",
            txtOccupation.Text.Trim());


        cmd.Parameters.AddWithValue(
            "@Status",
            ddlStatus.SelectedValue);
    }



    // =========================================================
    // SEARCH MEMBER
    // =========================================================

    protected void btnSearch_Click(
        object sender,
        EventArgs e)
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query;


                // ADMIN
                if (RoleHelper.IsAdmin())
                {
                    query = @"
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
                            ON M.BachatGatID =
                               B.BachatGatID
                        WHERE
                            M.MemberName LIKE @Search
                            OR M.Mobile LIKE @Search
                            OR M.MemberCode LIKE @Search
                        ORDER BY M.MemberID DESC";
                }

                // PRESIDENT / SECRETARY
                else
                {
                    query = @"
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
                            ON M.BachatGatID =
                               B.BachatGatID
                        WHERE
                            M.BachatGatID =
                                @BachatGatID
                            AND
                            (
                                M.MemberName LIKE @Search
                                OR M.Mobile LIKE @Search
                                OR M.MemberCode LIKE @Search
                            )
                        ORDER BY M.MemberID DESC";
                }


                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue(
                        "@Search",
                        "%" +
                        txtSearch.Text.Trim() +
                        "%");


                    if (!RoleHelper.IsAdmin())
                    {
                        cmd.Parameters.AddWithValue(
                            "@BachatGatID",
                            RoleHelper.GetBachatGatID());
                    }


                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    DataTable dt =
                        new DataTable();

                    da.Fill(dt);

                    gvMembers.DataSource = dt;

                    gvMembers.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Search error: " + ex.Message,
                System.Drawing.Color.Red);
        }
    }



    // =========================================================
    // GRID COMMAND
    // =========================================================

    protected void gvMembers_RowCommand(
        object sender,
        GridViewCommandEventArgs e)
    {
        int memberID;


        if (!int.TryParse(
            e.CommandArgument.ToString(),
            out memberID))
        {
            ShowMessage(
                "Invalid member ID.",
                System.Drawing.Color.Red);

            return;
        }


        if (e.CommandName == "EditMember")
        {
            LoadMemberForEdit(memberID);
        }


        if (e.CommandName == "DeleteMember")
        {
            DeactivateMember(memberID);
        }
    }



    // =========================================================
    // LOAD MEMBER FOR EDIT
    // =========================================================

    private void LoadMemberForEdit(
        int memberID)
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query;


                // ADMIN
                if (RoleHelper.IsAdmin())
                {
                    query = @"
                        SELECT *
                        FROM Members
                        WHERE MemberID =
                              @MemberID";
                }

                // PRESIDENT / SECRETARY
                else
                {
                    query = @"
                        SELECT *
                        FROM Members
                        WHERE MemberID =
                              @MemberID
                        AND BachatGatID =
                              @BachatGatID";
                }


                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue(
                        "@MemberID",
                        memberID);


                    if (!RoleHelper.IsAdmin())
                    {
                        cmd.Parameters.AddWithValue(
                            "@BachatGatID",
                            RoleHelper.GetBachatGatID());
                    }


                    con.Open();


                    using (SqlDataReader dr =
                        cmd.ExecuteReader())
                    {
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
                                dr[
                                    "FatherOrHusbandName"
                                ].ToString();


                            if (dr["DateOfBirth"] !=
                                DBNull.Value)
                            {
                                txtDOB.Text =
                                    Convert.ToDateTime(
                                        dr["DateOfBirth"])
                                    .ToString(
                                        "yyyy-MM-dd");
                            }
                            else
                            {
                                txtDOB.Text = "";
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


                            if (dr["JoinDate"] !=
                                DBNull.Value)
                            {
                                txtJoinDate.Text =
                                    Convert.ToDateTime(
                                        dr["JoinDate"])
                                    .ToString(
                                        "yyyy-MM-dd");
                            }


                            txtOccupation.Text =
                                dr["Occupation"].ToString();


                            ddlStatus.SelectedValue =
                                dr["Status"].ToString();


                            btnSave.Text =
                                "Update Member";


                            ShowMessage(
                                "Member loaded for editing.",
                                System.Drawing.Color.Blue);
                        }
                        else
                        {
                            ShowMessage(
                                "Member not found or you do not have permission to edit this member.",
                                System.Drawing.Color.Red);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error loading member: " +
                ex.Message,
                System.Drawing.Color.Red);
        }
    }



    // =========================================================
    // DEACTIVATE MEMBER
    // =========================================================

    private void DeactivateMember(
        int memberID)
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query;


                // ADMIN
                if (RoleHelper.IsAdmin())
                {
                    query = @"
                        UPDATE Members
                        SET Status = 'Inactive'
                        WHERE MemberID =
                              @MemberID";
                }

                // PRESIDENT / SECRETARY
                else
                {
                    query = @"
                        UPDATE Members
                        SET Status = 'Inactive'
                        WHERE MemberID =
                              @MemberID
                        AND BachatGatID =
                              @BachatGatID";
                }


                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue(
                        "@MemberID",
                        memberID);


                    if (!RoleHelper.IsAdmin())
                    {
                        cmd.Parameters.AddWithValue(
                            "@BachatGatID",
                            RoleHelper.GetBachatGatID());
                    }


                    con.Open();


                    int rows =
                        cmd.ExecuteNonQuery();


                    if (rows == 0)
                    {
                        ShowMessage(
                            "Member not found or you do not have permission to deactivate this member.",
                            System.Drawing.Color.Red);

                        return;
                    }
                }


                ShowMessage(
                    "Member deactivated successfully!",
                    System.Drawing.Color.Green);
            }


            ClearForm();

            LoadMembers();
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error deactivating member: " +
                ex.Message,
                System.Drawing.Color.Red);
        }
    }



    // =========================================================
    // CLEAR BUTTON
    // =========================================================

    protected void btnClear_Click(
        object sender,
        EventArgs e)
    {
        ClearForm();
    }



    // =========================================================
    // CLEAR FORM
    // =========================================================

    private void ClearForm()
    {
        hfMemberID.Value = "";


        if (RoleHelper.IsAdmin())
        {
            if (ddlBachatGat.Items.Count > 0)
            {
                ddlBachatGat.SelectedIndex = 0;
            }
        }
        else
        {
            if (ddlBachatGat.Items.Count > 0)
            {
                ddlBachatGat.SelectedValue =
                    RoleHelper.GetBachatGatID()
                    .ToString();
            }
        }


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


        if (ddlStatus.Items.FindByValue(
            "Active") != null)
        {
            ddlStatus.SelectedValue =
                "Active";
        }


        btnSave.Text =
            "Save Member";
    }



    // =========================================================
    // SHOW MESSAGE
    // =========================================================

    private void ShowMessage(
        string message,
        System.Drawing.Color color)
    {
        lblMessage.Text =
            message;

        lblMessage.ForeColor =
            color;
    }
}