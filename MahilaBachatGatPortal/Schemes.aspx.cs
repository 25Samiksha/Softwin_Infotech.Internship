using System;
using System.Data;
using System.Data.SqlClient;

public partial class Schemes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadSchemes();
        }
    }

    private void LoadSchemes()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"SELECT SchemeID, SchemeName, Department,
                             StartDate, EndDate, Status
                             FROM GovernmentSchemes
                             ORDER BY SchemeID DESC";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();

            da.Fill(dt);

            gvSchemes.DataSource = dt;
            gvSchemes.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DateTime startDate;
        DateTime endDate;

        if (txtSchemeName.Text.Trim() == "")
        {
            ShowMessage("Please enter scheme name.", "danger");
            return;
        }

        if (!DateTime.TryParse(txtStartDate.Text, out startDate))
        {
            ShowMessage("Please enter valid start date.", "danger");
            return;
        }

        if (!DateTime.TryParse(txtEndDate.Text, out endDate))
        {
            ShowMessage("Please enter valid end date.", "danger");
            return;
        }

        using (SqlConnection con = DBHelper.GetConnection())
        {
            con.Open();

            string query;

            if (hfSchemeID.Value == "")
            {
                query = @"INSERT INTO GovernmentSchemes
                          (SchemeName, Department, Description, Eligibility,
                           Benefits, RequiredDocuments, ApplicationProcess,
                           OfficialWebsite, StartDate, EndDate, Status)
                          VALUES
                          (@SchemeName, @Department, @Description, @Eligibility,
                           @Benefits, @RequiredDocuments, @ApplicationProcess,
                           @OfficialWebsite, @StartDate, @EndDate, @Status)";
            }
            else
            {
                query = @"UPDATE GovernmentSchemes SET
                          SchemeName=@SchemeName,
                          Department=@Department,
                          Description=@Description,
                          Eligibility=@Eligibility,
                          Benefits=@Benefits,
                          RequiredDocuments=@RequiredDocuments,
                          ApplicationProcess=@ApplicationProcess,
                          OfficialWebsite=@OfficialWebsite,
                          StartDate=@StartDate,
                          EndDate=@EndDate,
                          Status=@Status
                          WHERE SchemeID=@SchemeID";
            }

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@SchemeName", txtSchemeName.Text.Trim());
            cmd.Parameters.AddWithValue("@Department", txtDepartment.Text.Trim());
            cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
            cmd.Parameters.AddWithValue("@Eligibility", txtEligibility.Text.Trim());
            cmd.Parameters.AddWithValue("@Benefits", txtBenefits.Text.Trim());
            cmd.Parameters.AddWithValue("@RequiredDocuments", txtRequiredDocuments.Text.Trim());
            cmd.Parameters.AddWithValue("@ApplicationProcess", txtApplicationProcess.Text.Trim());
            cmd.Parameters.AddWithValue("@OfficialWebsite", txtOfficialWebsite.Text.Trim());
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);

            if (hfSchemeID.Value != "")
            {
                cmd.Parameters.AddWithValue("@SchemeID",
                    Convert.ToInt32(hfSchemeID.Value));
            }

            cmd.ExecuteNonQuery();
        }

        ShowMessage("Scheme saved successfully.", "success");

        ClearForm();
        LoadSchemes();
    }

    protected void gvSchemes_RowCommand(object sender,
        System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);

        int schemeID = Convert.ToInt32(
            gvSchemes.DataKeys[index].Value);

        if (e.CommandName == "EditScheme")
        {
            LoadScheme(schemeID);
        }
        else if (e.CommandName == "DeleteScheme")
        {
            DeleteScheme(schemeID);
        }
    }

    private void LoadScheme(int schemeID)
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            con.Open();

            string query = @"SELECT *
                             FROM GovernmentSchemes
                             WHERE SchemeID=@SchemeID";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@SchemeID", schemeID);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                hfSchemeID.Value = dr["SchemeID"].ToString();

                txtSchemeName.Text = dr["SchemeName"].ToString();
                txtDepartment.Text = dr["Department"].ToString();
                txtDescription.Text = dr["Description"].ToString();
                txtEligibility.Text = dr["Eligibility"].ToString();
                txtBenefits.Text = dr["Benefits"].ToString();
                txtRequiredDocuments.Text =
                    dr["RequiredDocuments"].ToString();

                txtApplicationProcess.Text =
                    dr["ApplicationProcess"].ToString();

                txtOfficialWebsite.Text =
                    dr["OfficialWebsite"].ToString();

                txtStartDate.Text =
                    Convert.ToDateTime(dr["StartDate"])
                    .ToString("yyyy-MM-dd");

                txtEndDate.Text =
                    Convert.ToDateTime(dr["EndDate"])
                    .ToString("yyyy-MM-dd");

                ddlStatus.SelectedValue =
                    dr["Status"].ToString();

                btnSave.Text = "Update Scheme";
            }

            dr.Close();
        }
    }

    private void DeleteScheme(int schemeID)
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            con.Open();

            string checkQuery =
                "SELECT COUNT(*) FROM SchemeApplications WHERE SchemeID=@SchemeID";

            SqlCommand checkCmd =
                new SqlCommand(checkQuery, con);

            checkCmd.Parameters.AddWithValue("@SchemeID", schemeID);

            int count = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (count > 0)
            {
                ShowMessage(
                    "This scheme cannot be deleted because applications exist.",
                    "danger");

                return;
            }

            string query =
                "DELETE FROM GovernmentSchemes WHERE SchemeID=@SchemeID";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@SchemeID", schemeID);

            cmd.ExecuteNonQuery();
        }

        LoadSchemes();

        ShowMessage("Scheme deleted successfully.", "success");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"SELECT SchemeID, SchemeName,
                             Department, StartDate, EndDate, Status
                             FROM GovernmentSchemes
                             WHERE SchemeName LIKE @Search
                             OR Department LIKE @Search
                             ORDER BY SchemeID DESC";

            SqlDataAdapter da =
                new SqlDataAdapter(query, con);

            da.SelectCommand.Parameters.AddWithValue(
                "@Search", "%" + txtSearch.Text.Trim() + "%");

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvSchemes.DataSource = dt;
            gvSchemes.DataBind();
        }
    }

    protected void btnShowAll_Click(object sender, EventArgs e)
    {
        LoadSchemes();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    {
        hfSchemeID.Value = "";

        txtSchemeName.Text = "";
        txtDepartment.Text = "";
        txtDescription.Text = "";
        txtEligibility.Text = "";
        txtBenefits.Text = "";
        txtRequiredDocuments.Text = "";
        txtApplicationProcess.Text = "";
        txtOfficialWebsite.Text = "";
        txtStartDate.Text = "";
        txtEndDate.Text = "";

        ddlStatus.SelectedIndex = 0;

        btnSave.Text = "Save Scheme";
    }

    private void ShowMessage(string message, string type)
    {
        lblMessage.Text =
            "<div class='alert alert-" + type + "'>" +
            message +
            "</div>";
    }
}