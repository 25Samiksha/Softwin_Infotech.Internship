using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class Designation : System.Web.UI.Page
{
    string cs = ConfigurationManager
        .ConnectionStrings["EmployeeDBConnection"]
        .ConnectionString;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDesignations();

            pnlDesignationList.Visible = true;
            pnlDesignationForm.Visible = false;
            pnlDesignationDetails.Visible = false;
        }
    }


    private void LoadDesignations()
    {
        using (SqlConnection con =
            new SqlConnection(cs))
        {
            string query = @"
                SELECT
                    DesignationID,
                    DesignationName,
                    Description
                FROM Designation
                ORDER BY DesignationID DESC";

            SqlDataAdapter da =
                new SqlDataAdapter(query, con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvDesignations.DataSource = dt;
            gvDesignations.DataBind();
        }
    }

    protected void btnSearch_Click(
        object sender,
        EventArgs e)
    {
        string search =
            txtSearch.Text.Trim();

        using (SqlConnection con =
            new SqlConnection(cs))
        {
            string query = @"
                SELECT
                    DesignationID,
                    DesignationName,
                    Description
                FROM Designation

                WHERE DesignationName LIKE @Search
                   OR Description LIKE @Search

                ORDER BY DesignationID DESC";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@Search",
                "%" + search + "%");

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvDesignations.DataSource = dt;
            gvDesignations.DataBind();
        }
    }

    protected void btnAddDesignation_Click(
        object sender,
        EventArgs e)
    {
        ClearFields();

        lblFormTitle.Text =
            "Add Designation";

        pnlDesignationList.Visible = false;
        pnlDesignationForm.Visible = true;
        pnlDesignationDetails.Visible = false;

        lblMessage.Text = "";
    }


    protected void gvDesignations_RowCommand(
        object sender,
        GridViewCommandEventArgs e)
    {
        int id;

        if (!int.TryParse(
            e.CommandArgument.ToString(),
            out id))
        {
            return;
        }


        if (e.CommandName == "ViewDesignation")
        {
            ViewDesignation(id);
        }
        else if (e.CommandName == "EditDesignation")
        {
            EditDesignation(id);
        }
        else if (e.CommandName == "DeleteDesignation")
        {
            DeleteDesignation(id);
        }
    }

    private void ViewDesignation(int id)
    {
        using (SqlConnection con =
            new SqlConnection(cs))
        {
            string query = @"
                SELECT
                    DesignationID,
                    DesignationName,
                    Description
                FROM Designation
                WHERE DesignationID = @ID";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@ID",
                id);

            con.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();

            if (dr.Read())
            {
                lblDetailsID.Text =
                    dr["DesignationID"].ToString();

                lblDetailsName.Text =
                    dr["DesignationName"].ToString();

                lblDetailsDescription.Text =
                    dr["Description"].ToString();
            }

            dr.Close();
        }

        pnlDesignationList.Visible = false;
        pnlDesignationForm.Visible = false;
        pnlDesignationDetails.Visible = true;
    }

    private void EditDesignation(int id)
    {
        using (SqlConnection con =
            new SqlConnection(cs))
        {
            string query = @"
                SELECT
                    DesignationID,
                    DesignationName,
                    Description
                FROM Designation
                WHERE DesignationID = @ID";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@ID",
                id);

            con.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();

            if (dr.Read())
            {
                hfDesignationID.Value =
                    dr["DesignationID"].ToString();

                txtDesignationName.Text =
                    dr["DesignationName"].ToString();

                txtDescription.Text =
                    dr["Description"].ToString();

                lblFormTitle.Text =
                    "Edit Designation";
            }

            dr.Close();
        }

        pnlDesignationList.Visible = false;
        pnlDesignationForm.Visible = true;
        pnlDesignationDetails.Visible = false;
    }
    protected void btnSave_Click(
        object sender,
        EventArgs e)
    {
        string name =
            txtDesignationName.Text.Trim();

        string description =
            txtDescription.Text.Trim();


        if (name == "")
        {
            lblMessage.Text =
                "Please enter designation name.";

            return;
        }


        using (SqlConnection con =
            new SqlConnection(cs))
        {
            con.Open();


            if (hfDesignationID.Value != "")
            {
                string query = @"
                    UPDATE Designation

                    SET DesignationName = @Name,
                        Description = @Description

                    WHERE DesignationID = @ID";


                SqlCommand cmd =
                    new SqlCommand(query, con);

                cmd.Parameters.AddWithValue(
                    "@Name",
                    name);

                cmd.Parameters.AddWithValue(
                    "@Description",
                    description);

                cmd.Parameters.AddWithValue(
                    "@ID",
                    Convert.ToInt32(
                        hfDesignationID.Value));

                cmd.ExecuteNonQuery();

                lblMessage.Text =
                    "Designation updated successfully.";
            }

            else
            {
                string query = @"
                    INSERT INTO Designation
                    (
                        DesignationName,
                        Description
                    )

                    VALUES
                    (
                        @Name,
                        @Description
                    )";


                SqlCommand cmd =
                    new SqlCommand(query, con);

                cmd.Parameters.AddWithValue(
                    "@Name",
                    name);

                cmd.Parameters.AddWithValue(
                    "@Description",
                    description);

                cmd.ExecuteNonQuery();

                lblMessage.Text =
                    "Designation added successfully.";
            }
        }


        LoadDesignations();

        pnlDesignationList.Visible = true;
        pnlDesignationForm.Visible = false;
        pnlDesignationDetails.Visible = false;
    }

    private void DeleteDesignation(int id)
    {
        try
        {
            using (SqlConnection con =
                new SqlConnection(cs))
            {
                string query = @"
                    DELETE FROM Designation
                    WHERE DesignationID = @ID";

                SqlCommand cmd =
                    new SqlCommand(query, con);

                cmd.Parameters.AddWithValue(
                    "@ID",
                    id);

                con.Open();

                cmd.ExecuteNonQuery();
            }

            LoadDesignations();

            lblMessage.Text =
                "Designation deleted successfully.";
        }
        catch
        {
            lblMessage.Text =
                "Cannot delete this designation because it may be used by employees.";
        }
    }

    protected void btnBack_Click(
        object sender,
        EventArgs e)
    {
        pnlDesignationList.Visible = true;
        pnlDesignationForm.Visible = false;
        pnlDesignationDetails.Visible = false;
    }
    protected void btnCancel_Click(
        object sender,
        EventArgs e)
    {
        ClearFields();

        pnlDesignationList.Visible = true;
        pnlDesignationForm.Visible = false;
        pnlDesignationDetails.Visible = false;
    }
    private void ClearFields()
    {
        hfDesignationID.Value = "";

        txtDesignationName.Text = "";
        txtDescription.Text = "";

        lblMessage.Text = "";
    }
}