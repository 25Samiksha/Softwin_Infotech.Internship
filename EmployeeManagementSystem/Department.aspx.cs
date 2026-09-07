using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class Department : System.Web.UI.Page
{
    string cs = ConfigurationManager
        .ConnectionStrings["EmployeeDBConnection"]
        .ConnectionString;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDepartments();

            pnlDepartmentList.Visible = true;
            pnlDepartmentForm.Visible = false;
            pnlDepartmentDetails.Visible = false;
        }
    }

    private void LoadDepartments()
    {
        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = @"
                SELECT
                    DepartmentID,
                    DepartmentName,
                    Description
                FROM Department
                ORDER BY DepartmentID DESC";

            SqlDataAdapter da =
                new SqlDataAdapter(query, con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvDepartments.DataSource = dt;
            gvDepartments.DataBind();
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
                    DepartmentID,
                    DepartmentName,
                    Description
                FROM Department

                WHERE DepartmentName LIKE @Search
                   OR Description LIKE @Search

                ORDER BY DepartmentID DESC";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@Search",
                "%" + search + "%");

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvDepartments.DataSource = dt;
            gvDepartments.DataBind();
        }
    }
    protected void btnAddDepartment_Click(
        object sender,
        EventArgs e)
    {
        ClearFields();

        lblFormTitle.Text =
            "Add Department";

        pnlDepartmentList.Visible = false;
        pnlDepartmentForm.Visible = true;
        pnlDepartmentDetails.Visible = false;

        lblMessage.Text = "";
    }
    protected void gvDepartments_RowCommand(
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


        if (e.CommandName == "ViewDepartment")
        {
            ViewDepartment(id);
        }
        else if (e.CommandName == "EditDepartment")
        {
            EditDepartment(id);
        }
        else if (e.CommandName == "DeleteDepartment")
        {
            DeleteDepartment(id);
        }
    }

    private void ViewDepartment(int id)
    {
        using (SqlConnection con =
            new SqlConnection(cs))
        {
            string query = @"
                SELECT
                    DepartmentID,
                    DepartmentName,
                    Description
                FROM Department
                WHERE DepartmentID = @ID";

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
                    dr["DepartmentID"].ToString();

                lblDetailsName.Text =
                    dr["DepartmentName"].ToString();

                lblDetailsDescription.Text =
                    dr["Description"].ToString();
            }

            dr.Close();
        }

        pnlDepartmentList.Visible = false;
        pnlDepartmentForm.Visible = false;
        pnlDepartmentDetails.Visible = true;
    }

    private void EditDepartment(int id)
    {
        using (SqlConnection con =
            new SqlConnection(cs))
        {
            string query = @"
                SELECT
                    DepartmentID,
                    DepartmentName,
                    Description
                FROM Department
                WHERE DepartmentID = @ID";

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
                hfDepartmentID.Value =
                    dr["DepartmentID"].ToString();

                txtDepartmentName.Text =
                    dr["DepartmentName"].ToString();

                txtDescription.Text =
                    dr["Description"].ToString();

                lblFormTitle.Text =
                    "Edit Department";
            }

            dr.Close();
        }

        pnlDepartmentList.Visible = false;
        pnlDepartmentForm.Visible = true;
        pnlDepartmentDetails.Visible = false;
    }
    protected void btnSave_Click(
        object sender,
        EventArgs e)
    {
        string name =
            txtDepartmentName.Text.Trim();

        string description =
            txtDescription.Text.Trim();


        if (name == "")
        {
            lblMessage.Text =
                "Please enter department name.";

            return;
        }


        using (SqlConnection con =
            new SqlConnection(cs))
        {
            con.Open();


          
            if (hfDepartmentID.Value != "")
            {
                string query = @"
                    UPDATE Department

                    SET DepartmentName = @Name,
                        Description = @Description

                    WHERE DepartmentID = @ID";


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
                        hfDepartmentID.Value));

                cmd.ExecuteNonQuery();

                lblMessage.Text =
                    "Department updated successfully.";
            }


            
            else
            {
                string query = @"
                    INSERT INTO Department
                    (
                        DepartmentName,
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
                    "Department added successfully.";
            }
        }


        LoadDepartments();

        pnlDepartmentList.Visible = true;
        pnlDepartmentForm.Visible = false;
        pnlDepartmentDetails.Visible = false;
    }
    private void DeleteDepartment(int id)
    {
        try
        {
            using (SqlConnection con =
                new SqlConnection(cs))
            {
                string query = @"
                    DELETE FROM Department
                    WHERE DepartmentID = @ID";

                SqlCommand cmd =
                    new SqlCommand(query, con);

                cmd.Parameters.AddWithValue(
                    "@ID",
                    id);

                con.Open();

                cmd.ExecuteNonQuery();
            }

            LoadDepartments();

            lblMessage.Text =
                "Department deleted successfully.";
        }
        catch
        {
            lblMessage.Text =
                "Cannot delete this department because it may be used by employees.";
        }
    }

    protected void btnBack_Click(
        object sender,
        EventArgs e)
    {
        pnlDepartmentList.Visible = true;
        pnlDepartmentForm.Visible = false;
        pnlDepartmentDetails.Visible = false;
    }

    protected void btnCancel_Click(
        object sender,
        EventArgs e)
    {
        ClearFields();

        pnlDepartmentList.Visible = true;
        pnlDepartmentForm.Visible = false;
        pnlDepartmentDetails.Visible = false;
    }

    private void ClearFields()
    {
        hfDepartmentID.Value = "";

        txtDepartmentName.Text = "";
        txtDescription.Text = "";

        lblMessage.Text = "";
    }
}