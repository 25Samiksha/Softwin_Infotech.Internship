using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls;

public partial class Employee : System.Web.UI.Page
{
    private string cs =
        ConfigurationManager
        .ConnectionStrings["EmployeeDBConnection"]
        .ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDepartments();
            LoadDesignations();
            LoadEmployees();

            ShowEmployeeList();
        }
    }

    private void ShowEmployeeList()
    {
        pnlEmployeeList.Visible = true;
        pnlEmployeeForm.Visible = false;
        pnlEmployeeDetails.Visible = false;
        pnlEmployeeProfile.Visible = false;
    }

    private void LoadEmployees()
    {
        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = @"
                SELECT
                    e.EmployeeID,
                    e.EmployeeName,
                    e.Email,
                    e.Mobile,
                    e.Gender,
                    e.DateOfBirth,
                    e.Address,
                    d.DepartmentName,
                    ds.DesignationName,
                    e.Salary,
                    e.JoiningDate,
                    e.Status,
                    e.Photo
                FROM Employee e
                LEFT JOIN Department d
                    ON e.DepartmentID = d.DepartmentID
                LEFT JOIN Designation ds
                    ON e.DesignationID = ds.DesignationID
                ORDER BY e.EmployeeID DESC";

            SqlDataAdapter da = new SqlDataAdapter(query, con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvEmployees.DataSource = dt;
            gvEmployees.DataBind();
        }
    }

    private void LoadDepartments()
    {
        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = @"
                SELECT DepartmentID, DepartmentName
                FROM Department
                ORDER BY DepartmentName";

            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            ddlDepartment.Items.Clear();

            ddlDepartment.Items.Add(
                new ListItem("-- Select Department --", "0"));

            while (dr.Read())
            {
                ddlDepartment.Items.Add(
                    new ListItem(
                        dr["DepartmentName"].ToString(),
                        dr["DepartmentID"].ToString()
                    )
                );
            }

            dr.Close();

            ddlDepartment.SelectedIndex = 0;
        }
    }

    private void LoadDesignations()
    {
        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = @"
                SELECT DesignationID, DesignationName
                FROM Designation
                ORDER BY DesignationName";

            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            ddlDesignation.Items.Clear();

            ddlDesignation.Items.Add(
                new ListItem("-- Select Designation --", "0"));

            while (dr.Read())
            {
                ddlDesignation.Items.Add(
                    new ListItem(
                        dr["DesignationName"].ToString(),
                        dr["DesignationID"].ToString()
                    )
                );
            }

            dr.Close();

            ddlDesignation.SelectedIndex = 0;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string search = txtSearch.Text.Trim();

        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = @"
                SELECT
                    e.EmployeeID,
                    e.EmployeeName,
                    e.Email,
                    e.Mobile,
                    e.Gender,
                    e.DateOfBirth,
                    e.Address,
                    d.DepartmentName,
                    ds.DesignationName,
                    e.Salary,
                    e.JoiningDate,
                    e.Status,
                    e.Photo
                FROM Employee e
                LEFT JOIN Department d
                    ON e.DepartmentID = d.DepartmentID
                LEFT JOIN Designation ds
                    ON e.DesignationID = ds.DesignationID
                WHERE
                    e.EmployeeName LIKE @Search
                    OR e.Email LIKE @Search
                    OR e.Mobile LIKE @Search
                    OR d.DepartmentName LIKE @Search
                    OR ds.DesignationName LIKE @Search
                ORDER BY e.EmployeeID DESC";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@Search",
                "%" + search + "%");

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvEmployees.DataSource = dt;
            gvEmployees.DataBind();
        }
    }


    protected void btnAddEmployee_Click(object sender, EventArgs e)
    {
        hfEmployeeID.Value = "";

        txtEmployeeName.Text = "";
        txtEmail.Text = "";
        txtMobile.Text = "";
        txtDateOfBirth.Text = "";
        txtAddress.Text = "";
        txtSalary.Text = "";
        txtJoiningDate.Text = "";

        lblMessage.Text = "";

        LoadDepartments();
        LoadDesignations();

        if (ddlGender.Items.Count > 0)
            ddlGender.SelectedIndex = 0;

        if (ddlDepartment.Items.Count > 0)
            ddlDepartment.SelectedIndex = 0;

        if (ddlDesignation.Items.Count > 0)
            ddlDesignation.SelectedIndex = 0;

        if (ddlStatus.Items.Count > 0)
            ddlStatus.SelectedIndex = 0;

        lblFormTitle.Text = "Add Employee";

        pnlEmployeeList.Visible = false;
        pnlEmployeeForm.Visible = true;
        pnlEmployeeDetails.Visible = false;
        pnlEmployeeProfile.Visible = false;
    }

    protected void gvEmployees_RowCommand(
        object sender,
        GridViewCommandEventArgs e)
    {
        int employeeID;

        if (!int.TryParse(
            e.CommandArgument.ToString(),
            out employeeID))
        {
            return;
        }

        if (e.CommandName == "ViewEmployee")
        {
            ViewEmployee(employeeID);
        }
        else if (e.CommandName == "EditEmployee")
        {
            EditEmployee(employeeID);
        }
        else if (e.CommandName == "DeleteEmployee")
        {
            DeleteEmployee(employeeID);
        }
        else if (e.CommandName == "ProfileEmployee")
        {
            EmployeeProfile(employeeID);
        }
    }
    private void ViewEmployee(int id)
    {
        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = @"
                SELECT
                    e.EmployeeID,
                    e.EmployeeName,
                    e.Email,
                    e.Mobile,
                    e.Gender,
                    e.DateOfBirth,
                    e.Address,
                    d.DepartmentName,
                    ds.DesignationName,
                    e.Salary,
                    e.JoiningDate,
                    e.Status,
                    e.Photo
                FROM Employee e
                LEFT JOIN Department d
                    ON e.DepartmentID = d.DepartmentID
                LEFT JOIN Designation ds
                    ON e.DesignationID = ds.DesignationID
                WHERE e.EmployeeID = @ID";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@ID", id);

            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                lblDetailsID.Text =
                    dr["EmployeeID"].ToString();

                lblDetailsName.Text =
                    dr["EmployeeName"].ToString();

                lblDetailsEmail.Text =
                    dr["Email"].ToString();

                lblDetailsMobile.Text =
                    dr["Mobile"].ToString();

                lblDetailsGender.Text =
                    dr["Gender"].ToString();

                lblDetailsDOB.Text =
                    FormatDate(dr["DateOfBirth"]);

                lblDetailsAddress.Text =
                    dr["Address"].ToString();

                lblDetailsDepartment.Text =
                    dr["DepartmentName"].ToString();

                lblDetailsDesignation.Text =
                    dr["DesignationName"].ToString();

                lblDetailsSalary.Text =
                    dr["Salary"].ToString();

                lblDetailsJoiningDate.Text =
                    FormatDate(dr["JoiningDate"]);

                lblDetailsStatus.Text =
                    dr["Status"].ToString();

                string photo =
                    dr["Photo"].ToString();

                if (!string.IsNullOrEmpty(photo))
                {
                    imgDetailsPhoto.ImageUrl = photo;
                }
                else
                {
                    imgDetailsPhoto.ImageUrl = "";
                }
            }

            dr.Close();
        }

        pnlEmployeeList.Visible = false;
        pnlEmployeeForm.Visible = false;
        pnlEmployeeDetails.Visible = true;
        pnlEmployeeProfile.Visible = false;
    }


    private void EditEmployee(int id)
    {
        LoadDepartments();
        LoadDesignations();

        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = @"
                SELECT
                    EmployeeID,
                    EmployeeName,
                    Email,
                    Mobile,
                    Gender,
                    DateOfBirth,
                    Address,
                    DepartmentID,
                    DesignationID,
                    Salary,
                    JoiningDate,
                    Status,
                    Photo
                FROM Employee
                WHERE EmployeeID = @ID";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@ID", id);

            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                hfEmployeeID.Value =
                    dr["EmployeeID"].ToString();

                txtEmployeeName.Text =
                    dr["EmployeeName"].ToString();

                txtEmail.Text =
                    dr["Email"].ToString();

                txtMobile.Text =
                    dr["Mobile"].ToString();

               
                if (dr["Gender"] != DBNull.Value)
                {
                    string gender =
                        dr["Gender"].ToString();

                    if (ddlGender.Items.FindByValue(gender) != null)
                    {
                        ddlGender.SelectedValue = gender;
                    }
                }

                txtDateOfBirth.Text =
                    FormatDateForInput(
                        dr["DateOfBirth"]);

                txtAddress.Text =
                    dr["Address"].ToString();

                if (dr["DepartmentID"] != DBNull.Value)
                {
                    string departmentID =
                        dr["DepartmentID"].ToString();

                    if (ddlDepartment.Items.FindByValue(
                        departmentID) != null)
                    {
                        ddlDepartment.SelectedValue =
                            departmentID;
                    }
                }

                if (dr["DesignationID"] != DBNull.Value)
                {
                    string designationID =
                        dr["DesignationID"].ToString();

                    if (ddlDesignation.Items.FindByValue(
                        designationID) != null)
                    {
                        ddlDesignation.SelectedValue =
                            designationID;
                    }
                }

                txtSalary.Text =
                    dr["Salary"].ToString();

                txtJoiningDate.Text =
                    FormatDateForInput(
                        dr["JoiningDate"]);

                if (dr["Status"] != DBNull.Value)
                {
                    string status =
                        dr["Status"].ToString();

                    if (ddlStatus.Items.FindByValue(status) != null)
                    {
                        ddlStatus.SelectedValue = status;
                    }
                }

                lblFormTitle.Text = "Edit Employee";
            }

            dr.Close();
        }

        pnlEmployeeList.Visible = false;
        pnlEmployeeForm.Visible = true;
        pnlEmployeeDetails.Visible = false;
        pnlEmployeeProfile.Visible = false;
    }

    protected void btnSaveEmployee_Click(
        object sender,
        EventArgs e)
    {
        string name =
            txtEmployeeName.Text.Trim();

        string email =
            txtEmail.Text.Trim();

        string mobile =
            txtMobile.Text.Trim();

        string gender =
            ddlGender.SelectedValue;

        string dob =
            txtDateOfBirth.Text.Trim();

        string address =
            txtAddress.Text.Trim();

        string department =
            ddlDepartment.SelectedValue;

        string designation =
            ddlDesignation.SelectedValue;

        string salary =
            txtSalary.Text.Trim();

        string joiningDate =
            txtJoiningDate.Text.Trim();

        string status =
            ddlStatus.SelectedValue;

        if (name == "")
        {
            lblMessage.Text =
                "Please enter employee name.";
            return;
        }

        if (email == "")
        {
            lblMessage.Text =
                "Please enter email.";
            return;
        }

        if (mobile == "")
        {
            lblMessage.Text =
                "Please enter mobile number.";
            return;
        }

        if (gender == "")
        {
            lblMessage.Text =
                "Please select gender.";
            return;
        }


        DateTime dobValue;

        if (!DateTime.TryParse(
            dob,
            out dobValue))
        {
            lblMessage.Text =
                "Please enter valid date of birth. Example: 25-10-2005";
            return;
        }


        int departmentID;

        if (!int.TryParse(
            department,
            out departmentID) ||
            departmentID == 0)
        {
            lblMessage.Text =
                "Please select department.";
            return;
        }


        int designationID;

        if (!int.TryParse(
            designation,
            out designationID) ||
            designationID == 0)
        {
            lblMessage.Text =
                "Please select designation.";
            return;
        }


        decimal salaryValue;

        if (!decimal.TryParse(
            salary,
            out salaryValue))
        {
            lblMessage.Text =
                "Please enter valid salary.";
            return;
        }


        DateTime joiningValue;

        if (!DateTime.TryParse(
            joiningDate,
            out joiningValue))
        {
            lblMessage.Text =
                "Please enter valid joining date. Example: 01-09-2026";
            return;
        }
        string photoPath = "";

        if (fuPhoto.HasFile)
        {
            string extension =
                Path.GetExtension(
                    fuPhoto.FileName);

            string fileName =
                Guid.NewGuid().ToString()
                + extension;

            string folder =
                Server.MapPath(
                    "~/Images/Employees/");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string fullPath =
                Path.Combine(
                    folder,
                    fileName);

            fuPhoto.SaveAs(fullPath);

            photoPath =
                "~/Images/Employees/"
                + fileName;
        }

        bool isEdit =
            hfEmployeeID.Value != "";


        using (SqlConnection con =
            new SqlConnection(cs))
        {
            con.Open();

            if (isEdit)
            {
                string query;

                if (photoPath != "")
                {
                    query = @"
                        UPDATE Employee
                        SET
                            EmployeeName = @Name,
                            Email = @Email,
                            Mobile = @Mobile,
                            Gender = @Gender,
                            DateOfBirth = @DOB,
                            Address = @Address,
                            DepartmentID = @DepartmentID,
                            DesignationID = @DesignationID,
                            Salary = @Salary,
                            JoiningDate = @JoiningDate,
                            Status = @Status,
                            Photo = @Photo
                        WHERE EmployeeID = @ID";
                }
                else
                {
                    query = @"
                        UPDATE Employee
                        SET
                            EmployeeName = @Name,
                            Email = @Email,
                            Mobile = @Mobile,
                            Gender = @Gender,
                            DateOfBirth = @DOB,
                            Address = @Address,
                            DepartmentID = @DepartmentID,
                            DesignationID = @DesignationID,
                            Salary = @Salary,
                            JoiningDate = @JoiningDate,
                            Status = @Status
                        WHERE EmployeeID = @ID";
                }


                SqlCommand cmd =
                    new SqlCommand(query, con);

                AddEmployeeParameters(
                    cmd,
                    name,
                    email,
                    mobile,
                    gender,
                    dobValue,
                    address,
                    departmentID,
                    designationID,
                    salaryValue,
                    joiningValue,
                    status);


                if (photoPath != "")
                {
                    cmd.Parameters.AddWithValue(
                        "@Photo",
                        photoPath);
                }


                cmd.Parameters.AddWithValue(
                    "@ID",
                    Convert.ToInt32(
                        hfEmployeeID.Value));

                cmd.ExecuteNonQuery();
            }
            else
            {
                string query = @"
                    INSERT INTO Employee
                    (
                        EmployeeName,
                        Email,
                        Mobile,
                        Gender,
                        DateOfBirth,
                        Address,
                        DepartmentID,
                        DesignationID,
                        Salary,
                        JoiningDate,
                        Status,
                        Photo
                    )
                    VALUES
                    (
                        @Name,
                        @Email,
                        @Mobile,
                        @Gender,
                        @DOB,
                        @Address,
                        @DepartmentID,
                        @DesignationID,
                        @Salary,
                        @JoiningDate,
                        @Status,
                        @Photo
                    )";


                SqlCommand cmd =
                    new SqlCommand(query, con);

                AddEmployeeParameters(
                    cmd,
                    name,
                    email,
                    mobile,
                    gender,
                    dobValue,
                    address,
                    departmentID,
                    designationID,
                    salaryValue,
                    joiningValue,
                    status);


                cmd.Parameters.AddWithValue(
                    "@Photo",
                    photoPath);

                cmd.ExecuteNonQuery();
            }
        }
        LoadEmployees();

        ClearFields();

        ShowEmployeeList();

        if (isEdit)
        {
            lblMessage.Text =
                "Employee updated successfully.";
        }
        else
        {
            lblMessage.Text =
                "Employee added successfully.";
        }
    }
    private void AddEmployeeParameters(
        SqlCommand cmd,
        string name,
        string email,
        string mobile,
        string gender,
        DateTime dob,
        string address,
        int departmentID,
        int designationID,
        decimal salary,
        DateTime joiningDate,
        string status)
    {
        cmd.Parameters.AddWithValue(
            "@Name",
            name);

        cmd.Parameters.AddWithValue(
            "@Email",
            email);

        cmd.Parameters.AddWithValue(
            "@Mobile",
            mobile);

        cmd.Parameters.AddWithValue(
            "@Gender",
            gender);

        cmd.Parameters.AddWithValue(
            "@DOB",
            dob);

        cmd.Parameters.AddWithValue(
            "@Address",
            address);

        cmd.Parameters.AddWithValue(
            "@DepartmentID",
            departmentID);

        cmd.Parameters.AddWithValue(
            "@DesignationID",
            designationID);

        cmd.Parameters.AddWithValue(
            "@Salary",
            salary);

        cmd.Parameters.AddWithValue(
            "@JoiningDate",
            joiningDate);

        cmd.Parameters.AddWithValue(
            "@Status",
            status);
    }
    private void DeleteEmployee(int id)
    {
        try
        {
            using (SqlConnection con =
                new SqlConnection(cs))
            {
                string query = @"
                    DELETE FROM Employee
                    WHERE EmployeeID = @ID";

                SqlCommand cmd =
                    new SqlCommand(
                        query,
                        con);

                cmd.Parameters.AddWithValue(
                    "@ID",
                    id);

                con.Open();

                cmd.ExecuteNonQuery();
            }

            LoadEmployees();

            lblMessage.Text =
                "Employee deleted successfully.";
        }
        catch
        {
            lblMessage.Text =
                "Employee could not be deleted.";
        }
    }
    private void EmployeeProfile(int id)
    {
        using (SqlConnection con =
            new SqlConnection(cs))
        {
            string query = @"
                SELECT
                    e.EmployeeID,
                    e.EmployeeName,
                    e.Email,
                    e.Mobile,
                    d.DepartmentName,
                    ds.DesignationName,
                    e.Salary,
                    e.JoiningDate,
                    e.Status,
                    e.Photo
                FROM Employee e
                LEFT JOIN Department d
                    ON e.DepartmentID = d.DepartmentID
                LEFT JOIN Designation ds
                    ON e.DesignationID = ds.DesignationID
                WHERE e.EmployeeID = @ID";

            SqlCommand cmd =
                new SqlCommand(
                    query,
                    con);

            cmd.Parameters.AddWithValue(
                "@ID",
                id);

            con.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();

            if (dr.Read())
            {
                lblProfileID.Text =
                    dr["EmployeeID"].ToString();

                lblProfileName.Text =
                    dr["EmployeeName"].ToString();

                lblProfileEmail.Text =
                    dr["Email"].ToString();

                lblProfileMobile.Text =
                    dr["Mobile"].ToString();

                lblProfileDepartment.Text =
                    dr["DepartmentName"].ToString();

                lblProfileDesignation.Text =
                    dr["DesignationName"].ToString();

                lblProfileSalary.Text =
                    dr["Salary"].ToString();

                lblProfileJoiningDate.Text =
                    FormatDate(
                        dr["JoiningDate"]);

                lblProfileStatus.Text =
                    dr["Status"].ToString();

                string photo =
                    dr["Photo"].ToString();

                if (!string.IsNullOrEmpty(photo))
                {
                    imgProfilePhoto.ImageUrl =
                        photo;
                }
                else
                {
                    imgProfilePhoto.ImageUrl = "";
                }
            }

            dr.Close();
        }

        pnlEmployeeList.Visible = false;
        pnlEmployeeForm.Visible = false;
        pnlEmployeeDetails.Visible = false;
        pnlEmployeeProfile.Visible = true;
    }
    protected void btnBack_Click(
        object sender,
        EventArgs e)
    {
        ShowEmployeeList();
    }


    protected void btnProfileBack_Click(
        object sender,
        EventArgs e)
    {
        ShowEmployeeList();
    }


    protected void btnCancelEmployee_Click(
        object sender,
        EventArgs e)
    {
        ClearFields();

        ShowEmployeeList();
    }

    private void ClearFields()
    {
        hfEmployeeID.Value = "";

        txtEmployeeName.Text = "";

        txtEmail.Text = "";

        txtMobile.Text = "";

        txtDateOfBirth.Text = "";

        txtAddress.Text = "";

        txtSalary.Text = "";

        txtJoiningDate.Text = "";


        if (ddlGender.Items.Count > 0)
        {
            ddlGender.SelectedIndex = 0;
        }

        if (ddlDepartment.Items.Count > 0)
        {
            ddlDepartment.SelectedIndex = 0;
        }

        if (ddlDesignation.Items.Count > 0)
        {
            ddlDesignation.SelectedIndex = 0;
        }

        if (ddlStatus.Items.Count > 0)
        {
            ddlStatus.SelectedIndex = 0;
        }
    }

    private string FormatDate(object value)
    {
        if (value == DBNull.Value ||
            value == null)
        {
            return "";
        }

        DateTime date =
            Convert.ToDateTime(value);

        return date.ToString(
            "dd-MM-yyyy");
    }
    private string FormatDateForInput(object value)
    {
        if (value == DBNull.Value ||
            value == null)
        {
            return "";
        }

        DateTime date =
            Convert.ToDateTime(value);

        return date.ToString(
            "dd-MM-yyyy");
    }
}