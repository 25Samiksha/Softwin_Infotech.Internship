using System;
using System.Data;
using System.Data.SqlClient;

public partial class SchemeApplication : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadSchemes();
            LoadBachatGats();
            LoadApplications();
        }
    }

    private void LoadSchemes()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT SchemeID, SchemeName FROM GovernmentSchemes " +
                "WHERE Status='Active' ORDER BY SchemeName", con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            ddlScheme.DataSource = dt;
            ddlScheme.DataTextField = "SchemeName";
            ddlScheme.DataValueField = "SchemeID";
            ddlScheme.DataBind();

            ddlScheme.Items.Insert(0,
                new System.Web.UI.WebControls.ListItem(
                    "-- Select Scheme --", ""));
        }
    }

    private void LoadBachatGats()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT BachatGatID, GatName FROM BachatGat " +
                "WHERE Status='Active' ORDER BY GatName", con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            ddlBachatGat.DataSource = dt;
            ddlBachatGat.DataTextField = "GatName";
            ddlBachatGat.DataValueField = "BachatGatID";
            ddlBachatGat.DataBind();

            ddlBachatGat.Items.Insert(0,
                new System.Web.UI.WebControls.ListItem(
                    "-- Select Bachat Gat --", ""));
        }
    }

    protected void ddlBachatGat_SelectedIndexChanged(
        object sender, EventArgs e)
    {
        LoadMembers();
    }

    private void LoadMembers()
    {
        ddlMember.Items.Clear();

        if (ddlBachatGat.SelectedValue == "")
            return;

        using (SqlConnection con = DBHelper.GetConnection())
        {
            SqlDataAdapter da = new SqlDataAdapter(
                @"SELECT MemberID, MemberName
                  FROM Members
                  WHERE BachatGatID=@BachatGatID
                  AND Status='Active'
                  ORDER BY MemberName", con);

            da.SelectCommand.Parameters.AddWithValue(
                "@BachatGatID",
                Convert.ToInt32(ddlBachatGat.SelectedValue));

            DataTable dt = new DataTable();

            da.Fill(dt);

            ddlMember.DataSource = dt;
            ddlMember.DataTextField = "MemberName";
            ddlMember.DataValueField = "MemberID";
            ddlMember.DataBind();

            ddlMember.Items.Insert(0,
                new System.Web.UI.WebControls.ListItem(
                    "-- Select Member --", ""));
        }
    }

    private void LoadApplications()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT A.ApplicationID,
                       G.SchemeName,
                       BG.GatName,
                       M.MemberName,
                       A.ApplicationDate,
                       A.ApplicationNumber,
                       A.Status,
                       A.ApprovedDate
                FROM SchemeApplications A
                INNER JOIN GovernmentSchemes G
                    ON A.SchemeID = G.SchemeID
                INNER JOIN Members M
                    ON A.MemberID = M.MemberID
                INNER JOIN BachatGat BG
                    ON M.BachatGatID = BG.BachatGatID
                ORDER BY A.ApplicationID DESC";

            SqlDataAdapter da =
                new SqlDataAdapter(query, con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvApplications.DataSource = dt;
            gvApplications.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DateTime applicationDate;

        if (ddlScheme.SelectedValue == "")
        {
            ShowMessage("Please select scheme.", "danger");
            return;
        }

        if (ddlMember.SelectedValue == "")
        {
            ShowMessage("Please select member.", "danger");
            return;
        }

        if (!DateTime.TryParse(
            txtApplicationDate.Text, out applicationDate))
        {
            ShowMessage("Please enter valid application date.", "danger");
            return;
        }

        using (SqlConnection con = DBHelper.GetConnection())
        {
            con.Open();

            string query;

            if (hfApplicationID.Value == "")
            {
                query = @"
                    INSERT INTO SchemeApplications
                    (
                        SchemeID,
                        MemberID,
                        ApplicationDate,
                        ApplicationNumber,
                        Documents,
                        Remarks,
                        Status,
                        ApprovedDate,
                        RejectionReason
                    )
                    VALUES
                    (
                        @SchemeID,
                        @MemberID,
                        @ApplicationDate,
                        @ApplicationNumber,
                        @Documents,
                        @Remarks,
                        @Status,
                        @ApprovedDate,
                        @RejectionReason
                    )";
            }
            else
            {
                query = @"
                    UPDATE SchemeApplications SET
                        SchemeID=@SchemeID,
                        MemberID=@MemberID,
                        ApplicationDate=@ApplicationDate,
                        ApplicationNumber=@ApplicationNumber,
                        Documents=@Documents,
                        Remarks=@Remarks,
                        Status=@Status,
                        ApprovedDate=@ApprovedDate,
                        RejectionReason=@RejectionReason
                    WHERE ApplicationID=@ApplicationID";
            }

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@SchemeID",
                Convert.ToInt32(ddlScheme.SelectedValue));

            cmd.Parameters.AddWithValue("@MemberID",
                Convert.ToInt32(ddlMember.SelectedValue));

            cmd.Parameters.AddWithValue("@ApplicationDate",
                applicationDate);

            cmd.Parameters.AddWithValue("@ApplicationNumber",
                txtApplicationNumber.Text.Trim());

            cmd.Parameters.AddWithValue("@Documents",
                txtDocuments.Text.Trim());

            cmd.Parameters.AddWithValue("@Remarks",
                txtRemarks.Text.Trim());

            cmd.Parameters.AddWithValue("@Status",
                ddlStatus.SelectedValue);

            DateTime approvedDate;

            if (DateTime.TryParse(
                txtApprovedDate.Text, out approvedDate))
            {
                cmd.Parameters.AddWithValue(
                    "@ApprovedDate", approvedDate);
            }
            else
            {
                cmd.Parameters.AddWithValue(
                    "@ApprovedDate", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@RejectionReason",
                txtRejectionReason.Text.Trim());

            if (hfApplicationID.Value != "")
            {
                cmd.Parameters.AddWithValue("@ApplicationID",
                    Convert.ToInt32(hfApplicationID.Value));
            }

            cmd.ExecuteNonQuery();
        }

        ShowMessage(
            "Scheme application saved successfully.",
            "success");

        ClearForm();
        LoadApplications();
    }

    protected void gvApplications_RowCommand(
        object sender,
        System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);

        int applicationID =
            Convert.ToInt32(
                gvApplications.DataKeys[index].Value);

        if (e.CommandName == "EditApplication")
        {
            LoadApplication(applicationID);
        }
        else if (e.CommandName == "DeleteApplication")
        {
            DeleteApplication(applicationID);
        }
    }

    private void LoadApplication(int applicationID)
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            con.Open();

            string query = @"
                SELECT A.*,
                       M.BachatGatID
                FROM SchemeApplications A
                INNER JOIN Members M
                    ON A.MemberID = M.MemberID
                WHERE A.ApplicationID=@ApplicationID";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@ApplicationID", applicationID);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                hfApplicationID.Value =
                    dr["ApplicationID"].ToString();

                ddlScheme.SelectedValue =
                    dr["SchemeID"].ToString();

                ddlBachatGat.SelectedValue =
                    dr["BachatGatID"].ToString();

                dr.Close();

                LoadMembers();

                using (SqlConnection con2 =
                    DBHelper.GetConnection())
                {
                    con2.Open();

                    SqlCommand cmd2 =
                        new SqlCommand(
                        "SELECT * FROM SchemeApplications " +
                        "WHERE ApplicationID=@ID", con2);

                    cmd2.Parameters.AddWithValue(
                        "@ID", applicationID);

                    SqlDataReader dr2 =
                        cmd2.ExecuteReader();

                    if (dr2.Read())
                    {
                        ddlMember.SelectedValue =
                            dr2["MemberID"].ToString();

                        txtApplicationDate.Text =
                            Convert.ToDateTime(
                                dr2["ApplicationDate"])
                            .ToString("yyyy-MM-dd");

                        txtApplicationNumber.Text =
                            dr2["ApplicationNumber"].ToString();

                        txtDocuments.Text =
                            dr2["Documents"].ToString();

                        txtRemarks.Text =
                            dr2["Remarks"].ToString();

                        ddlStatus.SelectedValue =
                            dr2["Status"].ToString();

                        if (dr2["ApprovedDate"] != DBNull.Value)
                        {
                            txtApprovedDate.Text =
                                Convert.ToDateTime(
                                    dr2["ApprovedDate"])
                                .ToString("yyyy-MM-dd");
                        }

                        txtRejectionReason.Text =
                            dr2["RejectionReason"].ToString();

                        btnSave.Text = "Update Application";
                    }

                    dr2.Close();
                }

                return;
            }

            dr.Close();
        }
    }

    private void DeleteApplication(int applicationID)
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(
                "DELETE FROM SchemeApplications " +
                "WHERE ApplicationID=@ApplicationID", con);

            cmd.Parameters.AddWithValue(
                "@ApplicationID", applicationID);

            cmd.ExecuteNonQuery();
        }

        LoadApplications();

        ShowMessage(
            "Application deleted successfully.",
            "success");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT A.ApplicationID,
                       G.SchemeName,
                       BG.GatName,
                       M.MemberName,
                       A.ApplicationDate,
                       A.ApplicationNumber,
                       A.Status,
                       A.ApprovedDate
                FROM SchemeApplications A
                INNER JOIN GovernmentSchemes G
                    ON A.SchemeID=G.SchemeID
                INNER JOIN Members M
                    ON A.MemberID=M.MemberID
                INNER JOIN BachatGat BG
                    ON M.BachatGatID=BG.BachatGatID
                WHERE G.SchemeName LIKE @Search
                   OR M.MemberName LIKE @Search
                   OR A.ApplicationNumber LIKE @Search
                ORDER BY A.ApplicationID DESC";

            SqlDataAdapter da =
                new SqlDataAdapter(query, con);

            da.SelectCommand.Parameters.AddWithValue(
                "@Search",
                "%" + txtSearch.Text.Trim() + "%");

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvApplications.DataSource = dt;
            gvApplications.DataBind();
        }
    }

    protected void btnShowAll_Click(object sender, EventArgs e)
    {
        LoadApplications();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    {
        hfApplicationID.Value = "";

        ddlScheme.SelectedIndex = 0;

        ddlBachatGat.SelectedIndex = 0;

        ddlMember.Items.Clear();

        txtApplicationDate.Text = "";
        txtApplicationNumber.Text = "";
        txtDocuments.Text = "";
        txtRemarks.Text = "";
        txtApprovedDate.Text = "";
        txtRejectionReason.Text = "";

        ddlStatus.SelectedIndex = 0;

        btnSave.Text = "Save Application";
    }

    private void ShowMessage(string message, string type)
    {
        lblMessage.Text =
            "<div class='alert alert-" + type + "'>" +
            message +
            "</div>";
    }
}