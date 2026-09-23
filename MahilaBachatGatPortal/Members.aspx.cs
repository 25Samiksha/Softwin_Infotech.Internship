using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Members : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtDateOfBirth.Attributes["type"] = "date";
        txtJoinDate.Attributes["type"] = "date";

        RoleHelper.RequireManagement(this);

        if (!IsPostBack)
        {
            LoadBachatGats();
            LoadFilterBachatGats();
            LoadFilterVillages();
            LoadMembers();
            ClearForm();
        }
    }

    private void LoadBachatGats()
    {
        ddlBachatGat.Items.Clear();

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query;

            if (RoleHelper.IsAdmin())
            {
                query = @"SELECT BachatGatID, GatName
                          FROM BachatGat
                          WHERE Status = 'Active'
                          ORDER BY GatName";
            }
            else
            {
                query = @"SELECT BachatGatID, GatName
                          FROM BachatGat
                          WHERE Status = 'Active'
                          AND BachatGatID = @BachatGatID
                          ORDER BY GatName";
            }

            SqlCommand cmd = new SqlCommand(query, con);

            if (!RoleHelper.IsAdmin())
            {
                cmd.Parameters.AddWithValue("@BachatGatID", RoleHelper.GetBachatGatID());
            }

            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            ddlBachatGat.DataSource = dr;
            ddlBachatGat.DataTextField = "GatName";
            ddlBachatGat.DataValueField = "BachatGatID";
            ddlBachatGat.DataBind();

            dr.Close();
        }

        if (ddlBachatGat.Items.Count > 0)
        {
            ddlBachatGat.SelectedIndex = 0;
        }
    }

    private void LoadFilterBachatGats()
    {
        ddlFilterBachatGat.Items.Clear();
        ddlFilterBachatGat.Items.Add(new ListItem("All Bachat Gats", ""));

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query;

            if (RoleHelper.IsAdmin())
            {
                query = @"SELECT BachatGatID, GatName
                          FROM BachatGat
                          WHERE Status = 'Active'
                          ORDER BY GatName";
            }
            else
            {
                query = @"SELECT BachatGatID, GatName
                          FROM BachatGat
                          WHERE Status = 'Active'
                          AND BachatGatID = @BachatGatID
                          ORDER BY GatName";
            }

            SqlCommand cmd = new SqlCommand(query, con);

            if (!RoleHelper.IsAdmin())
            {
                cmd.Parameters.AddWithValue("@BachatGatID", RoleHelper.GetBachatGatID());
            }

            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ddlFilterBachatGat.Items.Add(
                    new ListItem(
                        dr["GatName"].ToString(),
                        dr["BachatGatID"].ToString()
                    )
                );
            }

            dr.Close();
        }
    }

    private void LoadFilterVillages()
    {
        ddlFilterVillage.Items.Clear();
        ddlFilterVillage.Items.Add(new ListItem("All Villages", ""));

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT DISTINCT Village
                FROM Members
                WHERE Village IS NOT NULL
                AND Village <> ''";

            if (!RoleHelper.IsAdmin())
            {
                query += " AND BachatGatID = @BachatGatID";
            }

            query += " ORDER BY Village";

            SqlCommand cmd = new SqlCommand(query, con);

            if (!RoleHelper.IsAdmin())
            {
                cmd.Parameters.AddWithValue("@BachatGatID", RoleHelper.GetBachatGatID());
            }

            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ddlFilterVillage.Items.Add(
                    new ListItem(
                        dr["Village"].ToString(),
                        dr["Village"].ToString()
                    )
                );
            }

            dr.Close();
        }
    }

    private void LoadMembers()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    M.MemberID,
                    M.MemberName,
                    B.GatName,
                    M.Mobile,
                    M.Village,
                    M.Status
                FROM Members M
                INNER JOIN BachatGat B
                    ON M.BachatGatID = B.BachatGatID
                WHERE 1 = 1";

            if (!RoleHelper.IsAdmin())
            {
                query += " AND M.BachatGatID = @BachatGatID";
            }

            query += " ORDER BY M.MemberID DESC";

            SqlCommand cmd = new SqlCommand(query, con);

            if (!RoleHelper.IsAdmin())
            {
                cmd.Parameters.AddWithValue("@BachatGatID", RoleHelper.GetBachatGatID());
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            gvMembers.DataSource = dt;
            gvMembers.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string memberName = txtMemberName.Text.Trim();
        string joinDate = txtJoinDate.Text.Trim();

        if (ddlBachatGat.SelectedValue == "")
        {
            ShowMessage("Please select Bachat Gat.", true);
            return;
        }

        if (memberName == "")
        {
            ShowMessage("Please enter member name.", true);
            return;
        }

        if (joinDate == "")
        {
            ShowMessage("Please enter join date.", true);
            return;
        }

        int selectedGatID = Convert.ToInt32(ddlBachatGat.SelectedValue);

        if (!RoleHelper.IsAdmin() &&
            selectedGatID != RoleHelper.GetBachatGatID())
        {
            ShowMessage("You can manage members only from your assigned Bachat Gat.", true);
            return;
        }

        int memberID = 0;

        if (hfMemberID.Value != "")
        {
            memberID = Convert.ToInt32(hfMemberID.Value);
        }

        using (SqlConnection con = DBHelper.GetConnection())
        {
            con.Open();

            if (memberID > 0)
            {
                string query;

                if (RoleHelper.IsAdmin())
                {
                    query = @"
                        UPDATE Members SET
                            BachatGatID = @BachatGatID,
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
                }
                else
                {
                    query = @"
                        UPDATE Members SET
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
                        WHERE MemberID = @MemberID
                        AND BachatGatID = @BachatGatID";
                }

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@MemberID", memberID);
                cmd.Parameters.AddWithValue("@BachatGatID", selectedGatID);
                cmd.Parameters.AddWithValue("@MemberName", memberName);
                cmd.Parameters.AddWithValue("@FatherOrHusbandName", GetValue(txtFatherOrHusbandName.Text));
                cmd.Parameters.AddWithValue("@DateOfBirth", GetDate(txtDateOfBirth.Text));
                cmd.Parameters.AddWithValue("@Mobile", GetValue(txtMobile.Text));
                cmd.Parameters.AddWithValue("@Email", GetValue(txtEmail.Text));
                cmd.Parameters.AddWithValue("@Address", GetValue(txtAddress.Text));
                cmd.Parameters.AddWithValue("@Village", GetValue(txtVillage.Text));
                cmd.Parameters.AddWithValue("@Taluka", GetValue(txtTaluka.Text));
                cmd.Parameters.AddWithValue("@District", GetValue(txtDistrict.Text));
                cmd.Parameters.AddWithValue("@JoinDate", Convert.ToDateTime(txtJoinDate.Text));
                cmd.Parameters.AddWithValue("@Occupation", GetValue(txtOccupation.Text));
                cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    ShowMessage("Member updated successfully.", false);
                }
                else
                {
                    ShowMessage("Member was not found or access was denied.", true);
                }
            }
            else
            {
                string insertQuery = @"
                    INSERT INTO Members
                    (
                        BachatGatID,
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

                SqlCommand cmd = new SqlCommand(insertQuery, con);

                cmd.Parameters.AddWithValue("@BachatGatID", selectedGatID);
                cmd.Parameters.AddWithValue("@MemberName", memberName);
                cmd.Parameters.AddWithValue("@FatherOrHusbandName", GetValue(txtFatherOrHusbandName.Text));
                cmd.Parameters.AddWithValue("@DateOfBirth", GetDate(txtDateOfBirth.Text));
                cmd.Parameters.AddWithValue("@Mobile", GetValue(txtMobile.Text));
                cmd.Parameters.AddWithValue("@Email", GetValue(txtEmail.Text));
                cmd.Parameters.AddWithValue("@Address", GetValue(txtAddress.Text));
                cmd.Parameters.AddWithValue("@Village", GetValue(txtVillage.Text));
                cmd.Parameters.AddWithValue("@Taluka", GetValue(txtTaluka.Text));
                cmd.Parameters.AddWithValue("@District", GetValue(txtDistrict.Text));
                cmd.Parameters.AddWithValue("@JoinDate", Convert.ToDateTime(txtJoinDate.Text));
                cmd.Parameters.AddWithValue("@Occupation", GetValue(txtOccupation.Text));
                cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);

                cmd.ExecuteNonQuery();

                ShowMessage("Member added successfully.", false);
            }
        }

        LoadMembers();
        ClearForm();
        LoadFilterVillages();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    {
        hfMemberID.Value = "";
        txtMemberName.Text = "";
        txtFatherOrHusbandName.Text = "";
        txtDateOfBirth.Text = "";
        txtMobile.Text = "";
        txtEmail.Text = "";
        txtAddress.Text = "";
        txtVillage.Text = "";
        txtTaluka.Text = "";
        txtDistrict.Text = "";
        txtJoinDate.Text = "";
        txtOccupation.Text = "";
        ddlStatus.SelectedValue = "Active";
        lblMessage.Text = "";

        if (!RoleHelper.IsAdmin() &&
            ddlBachatGat.Items.Count > 0)
        {
            ddlBachatGat.SelectedValue = RoleHelper.GetBachatGatID().ToString();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string search = txtSearch.Text.Trim();
        string filterBachatGat = ddlFilterBachatGat.SelectedValue;
        string filterVillage = ddlFilterVillage.SelectedValue;

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    M.MemberID,
                    M.MemberName,
                    B.GatName,
                    M.Mobile,
                    M.Village,
                    M.Status
                FROM Members M
                INNER JOIN BachatGat B
                    ON M.BachatGatID = B.BachatGatID
                WHERE 1 = 1";

            if (search != "")
            {
                query += @"
                    AND
                    (
                        M.MemberName LIKE @Search
                        OR M.Mobile LIKE @Search
                        OR M.Village LIKE @Search
                    )";
            }

            if (!RoleHelper.IsAdmin())
            {
                query += " AND M.BachatGatID = @UserBachatGatID";
            }
            else if (filterBachatGat != "")
            {
                query += " AND M.BachatGatID = @FilterBachatGatID";
            }

            if (filterVillage != "")
            {
                query += " AND M.Village = @Village";
            }

            query += " ORDER BY M.MemberID DESC";

            SqlCommand cmd = new SqlCommand(query, con);

            if (search != "")
            {
                cmd.Parameters.AddWithValue("@Search", "%" + search + "%");
            }

            if (!RoleHelper.IsAdmin())
            {
                cmd.Parameters.AddWithValue("@UserBachatGatID", RoleHelper.GetBachatGatID());
            }
            else if (filterBachatGat != "")
            {
                cmd.Parameters.AddWithValue("@FilterBachatGatID", Convert.ToInt32(filterBachatGat));
            }

            if (filterVillage != "")
            {
                cmd.Parameters.AddWithValue("@Village", filterVillage);
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            gvMembers.DataSource = dt;
            gvMembers.DataBind();
        }
    }

    protected void btnShowAll_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";

        if (ddlFilterBachatGat.Items.Count > 0)
        {
            ddlFilterBachatGat.SelectedIndex = 0;
        }

        if (ddlFilterVillage.Items.Count > 0)
        {
            ddlFilterVillage.SelectedIndex = 0;
        }

        LoadMembers();
    }

    protected void gvMembers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int memberID = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "EditMember")
        {
            LoadMemberForEdit(memberID);
        }
        else if (e.CommandName == "DeleteMember")
        {
            DeactivateMember(memberID);
        }
    }

    private void LoadMemberForEdit(int memberID)
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    MemberID,
                    BachatGatID,
                    MemberName,
                    FatherOrHusbandName,
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
                FROM Members
                WHERE MemberID = @MemberID";

            if (!RoleHelper.IsAdmin())
            {
                query += " AND BachatGatID = @BachatGatID";
            }

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@MemberID", memberID);

            if (!RoleHelper.IsAdmin())
            {
                cmd.Parameters.AddWithValue("@BachatGatID", RoleHelper.GetBachatGatID());
            }

            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                hfMemberID.Value = dr["MemberID"].ToString();
                ddlBachatGat.SelectedValue = dr["BachatGatID"].ToString();
                txtMemberName.Text = dr["MemberName"].ToString();
                txtFatherOrHusbandName.Text = dr["FatherOrHusbandName"].ToString();

                if (dr["DateOfBirth"] != DBNull.Value)
                {
                    txtDateOfBirth.Text = Convert.ToDateTime(dr["DateOfBirth"]).ToString("yyyy-MM-dd");
                }
                else
                {
                    txtDateOfBirth.Text = "";
                }

                txtMobile.Text = dr["Mobile"].ToString();
                txtEmail.Text = dr["Email"].ToString();
                txtAddress.Text = dr["Address"].ToString();
                txtVillage.Text = dr["Village"].ToString();
                txtTaluka.Text = dr["Taluka"].ToString();
                txtDistrict.Text = dr["District"].ToString();

                if (dr["JoinDate"] != DBNull.Value)
                {
                    txtJoinDate.Text = Convert.ToDateTime(dr["JoinDate"]).ToString("yyyy-MM-dd");
                }
                else
                {
                    txtJoinDate.Text = "";
                }

                txtOccupation.Text = dr["Occupation"].ToString();
                ddlStatus.SelectedValue = dr["Status"].ToString();
            }

            dr.Close();
        }
    }

    private void DeactivateMember(int memberID)
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                UPDATE Members
                SET Status = 'Inactive'
                WHERE MemberID = @MemberID";

            if (!RoleHelper.IsAdmin())
            {
                query += " AND BachatGatID = @BachatGatID";
            }

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@MemberID", memberID);

            if (!RoleHelper.IsAdmin())
            {
                cmd.Parameters.AddWithValue("@BachatGatID", RoleHelper.GetBachatGatID());
            }

            con.Open();

            int rows = cmd.ExecuteNonQuery();

            if (rows > 0)
            {
                ShowMessage("Member deactivated successfully.", false);
            }
            else
            {
                ShowMessage("Member was not found or access was denied.", true);
            }
        }

        LoadMembers();
    }

    private object GetValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return DBNull.Value;
        }

        return value.Trim();
    }

    private object GetDate(string value)
    {
        DateTime date;

        if (DateTime.TryParse(value, out date))
        {
            return date;
        }

        return DBNull.Value;
    }

    private void ShowMessage(string message, bool isError)
    {
        lblMessage.Text = message;

        if (isError)
        {
            lblMessage.CssClass = "text-danger";
        }
        else
        {
            lblMessage.CssClass = "text-success";
        }
    }
}