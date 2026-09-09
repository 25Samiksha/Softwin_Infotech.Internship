using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employees : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindEmployees();
        }
    }
    private string GetConnectionString()
    {
        return ConfigurationManager
            .ConnectionStrings["EmployeeDB"]
            .ConnectionString;
    }
    private void BindEmployees()
    {
        using (SqlConnection con =
            new SqlConnection(GetConnectionString()))
        {
            string query = @"
                SELECT
                    EmployeeId,
                    EmployeeName,
                    Email,
                    Department,
                    Designation,
                    JoiningDate,
                    Status
                FROM Employees
                ORDER BY EmployeeId";

            using (SqlDataAdapter da =
                new SqlDataAdapter(query, con))
            {
                DataTable dt = new DataTable();

                da.Fill(dt);

                gvEmployees.DataSource = dt;
                gvEmployees.DataBind();
            }
        }
    }
    public string GetStatusClass(object status)
    {
        if (status == null)
        {
            return "label label-default";
        }

        string value = status.ToString();

        if (value.Equals(
            "Active",
            StringComparison.OrdinalIgnoreCase))
        {
            return "label label-success";
        }

        if (value.Equals(
            "Inactive",
            StringComparison.OrdinalIgnoreCase))
        {
            return "label label-danger";
        }

        return "label label-default";
    }
    protected void btnAddEmployee_Click(object sender, EventArgs e)
    {
        hfEmployeeId.Value = "";

        txtEmployeeName.Text = "";
        txtEmail.Text = "";
        ddlDepartment.SelectedIndex = 0;
        txtDesignation.Text = "";
        txtJoiningDate.Text = "";
        ddlStatus.SelectedIndex = 0;

        txtEmployeeName.Enabled = true;
        txtEmail.Enabled = true;
        ddlDepartment.Enabled = true;
        txtDesignation.Enabled = true;
        txtJoiningDate.Enabled = true;
        ddlStatus.Enabled = true;

        btnSave.Text = "Save Employee";
        btnSave.Visible = true;
        btnClear.Visible = true;

        ClientScript.RegisterStartupScript(
            this.GetType(),
            "ShowEmployeeModal",
            "$(document).ready(function(){ $('#employeeModal').modal('show'); });",
            true);
    }
    protected void btnSave_Click(
        object sender,
        EventArgs e)
    {
        if (!Page.IsValid)
        {
            ShowEmployeeModal();
            return;
        }
        DateTime joiningDate;

        bool validDate =
            DateTime.TryParseExact(
                txtJoiningDate.Text.Trim(),
                "dd-MM-yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out joiningDate);


        if (!validDate)
        {
            ShowMessage(
                "Please enter a valid joining date in dd-MM-yyyy format.",
                "danger");

            ShowEmployeeModal();

            return;
        }
        bool isEdit =
            !string.IsNullOrEmpty(
                hfEmployeeId.Value);


        try
        {
            using (SqlConnection con =
                new SqlConnection(GetConnectionString()))
            {
                string query;

                if (isEdit)
                {
                    query = @"
                        UPDATE Employees
                        SET EmployeeName = @EmployeeName,
                            Email = @Email,
                            Department = @Department,
                            Designation = @Designation,
                            JoiningDate = @JoiningDate,
                            Status = @Status
                        WHERE EmployeeId = @EmployeeId";
                }
                else
                {
                    query = @"
                        INSERT INTO Employees
                        (
                            EmployeeName,
                            Email,
                            Department,
                            Designation,
                            JoiningDate,
                            Status
                        )
                        VALUES
                        (
                            @EmployeeName,
                            @Email,
                            @Department,
                            @Designation,
                            @JoiningDate,
                            @Status
                        )";
                }


                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    cmd.Parameters.Add(
                        "@EmployeeName",
                        SqlDbType.VarChar,
                        100).Value =
                        txtEmployeeName.Text.Trim();
                    cmd.Parameters.Add(
                        "@Email",
                        SqlDbType.VarChar,
                        150).Value =
                        txtEmail.Text.Trim();

                    cmd.Parameters.Add(
                        "@Department",
                        SqlDbType.VarChar,
                        100).Value =
                        ddlDepartment.SelectedValue;

                    cmd.Parameters.Add(
                        "@Designation",
                        SqlDbType.VarChar,
                        100).Value =
                        txtDesignation.Text.Trim();

                    cmd.Parameters.Add(
                        "@JoiningDate",
                        SqlDbType.Date).Value =
                        joiningDate;

                    cmd.Parameters.Add(
                        "@Status",
                        SqlDbType.VarChar,
                        20).Value =
                        ddlStatus.SelectedValue;

                    if (isEdit)
                    {
                        cmd.Parameters.Add(
                            "@EmployeeId",
                            SqlDbType.Int).Value =
                            Convert.ToInt32(
                                hfEmployeeId.Value);
                    }
                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
            if (isEdit)
            {
                ShowMessage(
                    "Employee updated successfully.",
                    "success");
            }
            else
            {
                ShowMessage(
                    "Employee added successfully.",
                    "success");
            }

            ClearEmployeeForm();

            BindEmployees();
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error while saving employee: " +
                ex.Message,
                "danger");

            ShowEmployeeModal();
        }
    }
    protected void gvEmployees_RowCommand(
        object sender,
        GridViewCommandEventArgs e)
    {
        int employeeId;

        if (!int.TryParse(
            e.CommandArgument.ToString(),
            out employeeId))
        {
            return;
        }
        if (e.CommandName == "ViewEmployee")
        {
            LoadEmployee(employeeId);

            SetModalTitle("View Employee");

            DisableFormFields();

            btnSave.Visible = false;
            btnClear.Visible = false;

            ShowEmployeeModal();
        }
        else if (e.CommandName == "EditEmployee")
        {
            LoadEmployee(employeeId);

            SetModalTitle("Edit Employee");

            EnableFormFields();

            btnSave.Visible = true;
            btnSave.Text = "Update Employee";

            btnClear.Visible = true;

            ShowEmployeeModal();
        }

        else if (e.CommandName == "DeleteEmployee")
        {
            DeleteEmployee(employeeId);
        }
    }
    private void LoadEmployee(int employeeId)
    {
        using (SqlConnection con =
            new SqlConnection(GetConnectionString()))
        {
            string query = @"
                SELECT
                    EmployeeId,
                    EmployeeName,
                    Email,
                    Department,
                    Designation,
                    JoiningDate,
                    Status
                FROM Employees
                WHERE EmployeeId = @EmployeeId";


            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                cmd.Parameters.Add(
                    "@EmployeeId",
                    SqlDbType.Int).Value =
                    employeeId;


                con.Open();


                using (SqlDataReader reader =
                    cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        hfEmployeeId.Value =
                            reader["EmployeeId"].ToString();

                        txtEmployeeName.Text =
                            reader["EmployeeName"].ToString();

                        txtEmail.Text =
                            reader["Email"].ToString();

                        string department =
                            reader["Department"].ToString();

                        if (ddlDepartment.Items.FindByValue(
                            department) != null)
                        {
                            ddlDepartment.SelectedValue =
                                department;
                        }
                        txtDesignation.Text =
                            reader["Designation"].ToString();

                        if (reader["JoiningDate"] != DBNull.Value)
                        {
                            DateTime date =
                                Convert.ToDateTime(
                                    reader["JoiningDate"]);

                            txtJoiningDate.Text =
                                date.ToString("dd-MM-yyyy");
                        }
                        else
                        {
                            txtJoiningDate.Text = "";
                        }
                        string status =
                            reader["Status"].ToString();

                        if (ddlStatus.Items.FindByValue(
                            status) != null)
                        {
                            ddlStatus.SelectedValue =
                                status;
                        }
                    }
                }
            }
        }
    }
    private void DeleteEmployee(int employeeId)
    {
        try
        {
            using (SqlConnection con =
                new SqlConnection(GetConnectionString()))
            {
                string query = @"
                    DELETE FROM Employees
                    WHERE EmployeeId = @EmployeeId";


                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    cmd.Parameters.Add(
                        "@EmployeeId",
                        SqlDbType.Int).Value =
                        employeeId;


                    con.Open();


                    cmd.ExecuteNonQuery();
                }
            }


            ShowMessage(
                "Employee deleted successfully.",
                "success");


            BindEmployees();
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error while deleting employee: " +
                ex.Message,
                "danger");
        }
    }
    protected void btnSearch_Click(
        object sender,
        EventArgs e)
    {
        gvEmployees.PageIndex = 0;

        SearchEmployees();
    }


    private void SearchEmployees()
    {
        using (SqlConnection con =
            new SqlConnection(GetConnectionString()))
        {
            string query = @"
                SELECT
                    EmployeeId,
                    EmployeeName,
                    Email,
                    Department,
                    Designation,
                    JoiningDate,
                    Status
                FROM Employees
                WHERE EmployeeName LIKE @EmployeeName";


            if (!string.IsNullOrEmpty(
                ddlSearchDepartment.SelectedValue))
            {
                query +=
                    " AND Department = @Department";
            }


            if (!string.IsNullOrEmpty(
                ddlSearchStatus.SelectedValue))
            {
                query +=
                    " AND Status = @Status";
            }


            query += " ORDER BY EmployeeId";


            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                cmd.Parameters.Add(
                    "@EmployeeName",
                    SqlDbType.VarChar,
                    100).Value =
                    "%" +
                    txtSearchName.Text.Trim() +
                    "%";


                if (!string.IsNullOrEmpty(
                    ddlSearchDepartment.SelectedValue))
                {
                    cmd.Parameters.Add(
                        "@Department",
                        SqlDbType.VarChar,
                        100).Value =
                        ddlSearchDepartment.SelectedValue;
                }


                if (!string.IsNullOrEmpty(
                    ddlSearchStatus.SelectedValue))
                {
                    cmd.Parameters.Add(
                        "@Status",
                        SqlDbType.VarChar,
                        20).Value =
                        ddlSearchStatus.SelectedValue;
                }


                using (SqlDataAdapter da =
                    new SqlDataAdapter(cmd))
                {
                    DataTable dt =
                        new DataTable();


                    da.Fill(dt);


                    gvEmployees.DataSource =
                        dt;

                    gvEmployees.DataBind();
                }
            }
        }
    }
    protected void btnResetSearch_Click(object sender,EventArgs e)
    {
        txtSearchName.Text = "";

        ddlSearchDepartment.SelectedIndex = 0;

        ddlSearchStatus.SelectedIndex = 0;

        gvEmployees.PageIndex = 0;

        BindEmployees();
    }
    protected void gvEmployees_PageIndexChanging(
        object sender,
        GridViewPageEventArgs e)
    {
        gvEmployees.PageIndex =
            e.NewPageIndex;


        if (!string.IsNullOrEmpty(
            txtSearchName.Text.Trim())
            ||
            !string.IsNullOrEmpty(
                ddlSearchDepartment.SelectedValue)
            ||
            !string.IsNullOrEmpty(
                ddlSearchStatus.SelectedValue))
        {
            SearchEmployees();
        }
        else
        {
            BindEmployees();
        }
    }
    private void ClearEmployeeForm()
    {
        hfEmployeeId.Value = "";

        txtEmployeeName.Text = "";

        txtEmail.Text = "";

        ddlDepartment.SelectedIndex = 0;

        txtDesignation.Text = "";

        txtJoiningDate.Text = "";

        ddlStatus.SelectedIndex = 0;

        btnSave.Text = "Save Employee";

        btnSave.Visible = true;

        btnClear.Visible = true;

        EnableFormFields();
    }
    protected void btnClear_Click(
        object sender,
        EventArgs e)
    {
        ClearEmployeeForm();

        SetModalTitle("Add Employee");

        ShowEmployeeModal();
    }
    private void EnableFormFields()
    {
        txtEmployeeName.Enabled = true;

        txtEmail.Enabled = true;

        ddlDepartment.Enabled = true;

        txtDesignation.Enabled = true;

        txtJoiningDate.Enabled = true;

        ddlStatus.Enabled = true;
    }
    private void DisableFormFields()
    {
        txtEmployeeName.Enabled = false;

        txtEmail.Enabled = false;

        ddlDepartment.Enabled = false;

        txtDesignation.Enabled = false;

        txtJoiningDate.Enabled = false;

        ddlStatus.Enabled = false;
    }
    private void SetModalTitle(string title)
    {
        string script =
            "$(document).ready(function(){ " +
            "$('#employeeModalTitle').text('" +
            title.Replace("'", "\\'") +
            "');" +
            "});";


        ClientScript.RegisterStartupScript(
            this.GetType(),
            "SetModalTitle",
            script,
            true);
    }
    private void ShowEmployeeModal()
    {
        string script =
            "$(document).ready(function(){ " +
            "$('#employeeModal').modal('show');" +
            "});";


        ClientScript.RegisterStartupScript(
            this.GetType(),
            "ShowEmployeeModal",
            script,
            true);
    }


    private void ShowMessage(string message, string alertType)
    {
        lblMessage.Text = message;

        lblMessage.CssClass = "alert alert-" + alertType;

        lblMessage.Visible = true;

        ClientScript.RegisterStartupScript(
            this.GetType(),
            "HideMessage",
            "setTimeout(function(){ $('#" + lblMessage.ClientID + "').fadeOut('slow'); }, 3000);",
            true);
    }
}