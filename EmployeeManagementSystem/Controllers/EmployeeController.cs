using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;

public class EmployeeController : ApiController
{
    // =========================================================
    // DATABASE CONNECTION
    // =========================================================

    private string cs =
        @"Data Source=DESKTOP-LDBJA37\SAMISHA;
          Initial Catalog=EmployeeManagementDB;
          Integrated Security=True";


    // =========================================================
    // GET ALL EMPLOYEES
    // GET: api/employee
    // =========================================================

    [HttpGet]
    public HttpResponseMessage Get()
    {
        List<EmployeeModel> employees =
            new List<EmployeeModel>();

        try
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
                        e.Gender,
                        e.DateOfBirth,
                        e.Address,
                        e.DepartmentID,
                        d.DepartmentName,
                        e.DesignationID,
                        dg.DesignationName,
                        e.Salary,
                        e.JoiningDate,
                        e.Status,
                        e.Photo
                    FROM Employee e
                    LEFT JOIN Department d
                        ON e.DepartmentID = d.DepartmentID
                    LEFT JOIN Designation dg
                        ON e.DesignationID = dg.DesignationID
                    ORDER BY e.EmployeeID DESC";

                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    con.Open();

                    using (SqlDataReader dr =
                        cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            EmployeeModel employee =
                                new EmployeeModel();

                            employee.EmployeeID =
                                Convert.ToInt32(
                                    dr["EmployeeID"]);

                            employee.EmployeeName =
                                dr["EmployeeName"].ToString();

                            employee.Email =
                                dr["Email"].ToString();

                            employee.Mobile =
                                dr["Mobile"].ToString();

                            employee.Gender =
                                dr["Gender"].ToString();


                            if (dr["DateOfBirth"] !=
                                DBNull.Value)
                            {
                                employee.DateOfBirth =
                                    Convert.ToDateTime(
                                        dr["DateOfBirth"]);
                            }


                            employee.Address =
                                dr["Address"].ToString();


                            if (dr["DepartmentID"] !=
                                DBNull.Value)
                            {
                                employee.DepartmentID =
                                    Convert.ToInt32(
                                        dr["DepartmentID"]);
                            }


                            if (dr["DepartmentName"] !=
                                DBNull.Value)
                            {
                                employee.DepartmentName =
                                    dr["DepartmentName"]
                                    .ToString();
                            }
                            else
                            {
                                employee.DepartmentName = "";
                            }


                            if (dr["DesignationID"] !=
                                DBNull.Value)
                            {
                                employee.DesignationID =
                                    Convert.ToInt32(
                                        dr["DesignationID"]);
                            }


                            if (dr["DesignationName"] !=
                                DBNull.Value)
                            {
                                employee.DesignationName =
                                    dr["DesignationName"]
                                    .ToString();
                            }
                            else
                            {
                                employee.DesignationName = "";
                            }


                            if (dr["Salary"] !=
                                DBNull.Value)
                            {
                                employee.Salary =
                                    Convert.ToDecimal(
                                        dr["Salary"]);
                            }


                            if (dr["JoiningDate"] !=
                                DBNull.Value)
                            {
                                employee.JoiningDate =
                                    Convert.ToDateTime(
                                        dr["JoiningDate"]);
                            }


                            if (dr["Status"] !=
                                DBNull.Value)
                            {
                                employee.Status =
                                    dr["Status"].ToString();
                            }
                            else
                            {
                                employee.Status = "";
                            }


                            if (dr["Photo"] !=
                                DBNull.Value)
                            {
                                employee.Photo =
                                    dr["Photo"].ToString();
                            }
                            else
                            {
                                employee.Photo = "";
                            }


                            employees.Add(employee);
                        }
                    }
                }
            }


            return Request.CreateResponse(
                HttpStatusCode.OK,
                employees);
        }
        catch (Exception ex)
        {
            return Request.CreateResponse(
                HttpStatusCode.InternalServerError,
                ex.Message);
        }
    }


    // =========================================================
    // GET EMPLOYEE BY ID
    // GET: api/employee/1
    // =========================================================

    [HttpGet]
    public HttpResponseMessage Get(int id)
    {
        EmployeeModel employee = null;

        try
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
                        e.Gender,
                        e.DateOfBirth,
                        e.Address,
                        e.DepartmentID,
                        d.DepartmentName,
                        e.DesignationID,
                        dg.DesignationName,
                        e.Salary,
                        e.JoiningDate,
                        e.Status,
                        e.Photo
                    FROM Employee e
                    LEFT JOIN Department d
                        ON e.DepartmentID = d.DepartmentID
                    LEFT JOIN Designation dg
                        ON e.DesignationID = dg.DesignationID
                    WHERE e.EmployeeID = @EmployeeID";

                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue(
                        "@EmployeeID",
                        id);

                    con.Open();

                    using (SqlDataReader dr =
                        cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            employee =
                                new EmployeeModel();


                            employee.EmployeeID =
                                Convert.ToInt32(
                                    dr["EmployeeID"]);

                            employee.EmployeeName =
                                dr["EmployeeName"].ToString();

                            employee.Email =
                                dr["Email"].ToString();

                            employee.Mobile =
                                dr["Mobile"].ToString();

                            employee.Gender =
                                dr["Gender"].ToString();


                            if (dr["DateOfBirth"] !=
                                DBNull.Value)
                            {
                                employee.DateOfBirth =
                                    Convert.ToDateTime(
                                        dr["DateOfBirth"]);
                            }


                            employee.Address =
                                dr["Address"].ToString();


                            if (dr["DepartmentID"] !=
                                DBNull.Value)
                            {
                                employee.DepartmentID =
                                    Convert.ToInt32(
                                        dr["DepartmentID"]);
                            }


                            if (dr["DepartmentName"] !=
                                DBNull.Value)
                            {
                                employee.DepartmentName =
                                    dr["DepartmentName"]
                                    .ToString();
                            }
                            else
                            {
                                employee.DepartmentName = "";
                            }


                            if (dr["DesignationID"] !=
                                DBNull.Value)
                            {
                                employee.DesignationID =
                                    Convert.ToInt32(
                                        dr["DesignationID"]);
                            }


                            if (dr["DesignationName"] !=
                                DBNull.Value)
                            {
                                employee.DesignationName =
                                    dr["DesignationName"]
                                    .ToString();
                            }
                            else
                            {
                                employee.DesignationName = "";
                            }


                            if (dr["Salary"] !=
                                DBNull.Value)
                            {
                                employee.Salary =
                                    Convert.ToDecimal(
                                        dr["Salary"]);
                            }


                            if (dr["JoiningDate"] !=
                                DBNull.Value)
                            {
                                employee.JoiningDate =
                                    Convert.ToDateTime(
                                        dr["JoiningDate"]);
                            }


                            if (dr["Status"] !=
                                DBNull.Value)
                            {
                                employee.Status =
                                    dr["Status"].ToString();
                            }
                            else
                            {
                                employee.Status = "";
                            }


                            if (dr["Photo"] !=
                                DBNull.Value)
                            {
                                employee.Photo =
                                    dr["Photo"].ToString();
                            }
                            else
                            {
                                employee.Photo = "";
                            }
                        }
                    }
                }
            }


            if (employee == null)
            {
                return Request.CreateResponse(
                    HttpStatusCode.NotFound,
                    "Employee not found.");
            }


            return Request.CreateResponse(
                HttpStatusCode.OK,
                employee);
        }
        catch (Exception ex)
        {
            return Request.CreateResponse(
                HttpStatusCode.InternalServerError,
                ex.Message);
        }
    }


    // =========================================================
    // ADD EMPLOYEE
    // POST: api/employee
    // =========================================================

    [HttpPost]
    public HttpResponseMessage Post(EmployeeModel employee)
    {
        try
        {
            // Check employee object
            if (employee == null)
            {
                return Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    "Employee data is required.");
            }


            using (SqlConnection con =
                new SqlConnection(cs))
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
                        @EmployeeName,
                        @Email,
                        @Mobile,
                        @Gender,
                        @DateOfBirth,
                        @Address,
                        @DepartmentID,
                        @DesignationID,
                        @Salary,
                        @JoiningDate,
                        @Status,
                        @Photo
                    );

                    SELECT CAST(SCOPE_IDENTITY() AS INT);";


                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    // Employee Name
                    cmd.Parameters.AddWithValue(
                        "@EmployeeName",
                        employee.EmployeeName ?? "");


                    // Email
                    cmd.Parameters.AddWithValue(
                        "@Email",
                        employee.Email ?? "");


                    // Mobile
                    cmd.Parameters.AddWithValue(
                        "@Mobile",
                        employee.Mobile ?? "");


                    // Gender
                    cmd.Parameters.AddWithValue(
                        "@Gender",
                        employee.Gender ?? "");


                    // Date Of Birth
                    cmd.Parameters.AddWithValue(
                        "@DateOfBirth",
                        employee.DateOfBirth.HasValue
                            ? (object)employee.DateOfBirth.Value
                            : DBNull.Value);


                    // Address
                    cmd.Parameters.AddWithValue(
                        "@Address",
                        employee.Address ?? "");


                    // Department ID
                    cmd.Parameters.AddWithValue(
                        "@DepartmentID",
                        employee.DepartmentID);


                    // Designation ID
                    cmd.Parameters.AddWithValue(
                        "@DesignationID",
                        employee.DesignationID);


                    // Salary
                    cmd.Parameters.AddWithValue(
                        "@Salary",
                        employee.Salary);


                    // Joining Date
                    cmd.Parameters.AddWithValue(
                        "@JoiningDate",
                        employee.JoiningDate.HasValue
                            ? (object)employee.JoiningDate.Value
                            : DBNull.Value);


                    // Status
                    cmd.Parameters.AddWithValue(
                        "@Status",
                        employee.Status ?? "");


                    // Photo
                    cmd.Parameters.AddWithValue(
                        "@Photo",
                        employee.Photo ?? "");


                    con.Open();


                    // Get newly created EmployeeID
                    int newEmployeeID =
                        Convert.ToInt32(
                            cmd.ExecuteScalar());


                    return Request.CreateResponse(
                        HttpStatusCode.Created,
                        new
                        {
                            Message =
                                "Employee added successfully.",

                            EmployeeID =
                                newEmployeeID
                        });
                }
            }
        }
        catch (Exception ex)
        {
            return Request.CreateResponse(
                HttpStatusCode.InternalServerError,
                ex.Message);
        }
    }


    // =========================================================
    // UPDATE EMPLOYEE
    // PUT: api/employee/1
    // =========================================================

    [HttpPut]
    public HttpResponseMessage Put(
        int id,
        EmployeeModel employee)
    {
        try
        {
            if (employee == null)
            {
                return Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    "Employee data is required.");
            }


            using (SqlConnection con =
                new SqlConnection(cs))
            {
                string query = @"
                    UPDATE Employee
                    SET
                        EmployeeName = @EmployeeName,
                        Email = @Email,
                        Mobile = @Mobile,
                        Gender = @Gender,
                        DateOfBirth = @DateOfBirth,
                        Address = @Address,
                        DepartmentID = @DepartmentID,
                        DesignationID = @DesignationID,
                        Salary = @Salary,
                        JoiningDate = @JoiningDate,
                        Status = @Status,
                        Photo = @Photo
                    WHERE EmployeeID = @EmployeeID";


                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue(
                        "@EmployeeID",
                        id);


                    cmd.Parameters.AddWithValue(
                        "@EmployeeName",
                        employee.EmployeeName ?? "");


                    cmd.Parameters.AddWithValue(
                        "@Email",
                        employee.Email ?? "");


                    cmd.Parameters.AddWithValue(
                        "@Mobile",
                        employee.Mobile ?? "");


                    cmd.Parameters.AddWithValue(
                        "@Gender",
                        employee.Gender ?? "");


                    cmd.Parameters.AddWithValue(
                        "@DateOfBirth",
                        employee.DateOfBirth.HasValue
                            ? (object)employee.DateOfBirth.Value
                            : DBNull.Value);


                    cmd.Parameters.AddWithValue(
                        "@Address",
                        employee.Address ?? "");


                    cmd.Parameters.AddWithValue(
                        "@DepartmentID",
                        employee.DepartmentID);


                    cmd.Parameters.AddWithValue(
                        "@DesignationID",
                        employee.DesignationID);


                    cmd.Parameters.AddWithValue(
                        "@Salary",
                        employee.Salary);


                    cmd.Parameters.AddWithValue(
                        "@JoiningDate",
                        employee.JoiningDate.HasValue
                            ? (object)employee.JoiningDate.Value
                            : DBNull.Value);


                    cmd.Parameters.AddWithValue(
                        "@Status",
                        employee.Status ?? "");


                    cmd.Parameters.AddWithValue(
                        "@Photo",
                        employee.Photo ?? "");


                    con.Open();


                    int rows =
                        cmd.ExecuteNonQuery();


                    if (rows == 0)
                    {
                        return Request.CreateResponse(
                            HttpStatusCode.NotFound,
                            "Employee not found.");
                    }


                    return Request.CreateResponse(
                        HttpStatusCode.OK,
                        "Employee updated successfully.");
                }
            }
        }
        catch (Exception ex)
        {
            return Request.CreateResponse(
                HttpStatusCode.InternalServerError,
                ex.Message);
        }
    }


    // =========================================================
    // DELETE EMPLOYEE
    // DELETE: api/employee/1
    // =========================================================

    [HttpDelete]
    public HttpResponseMessage Delete(int id)
    {
        try
        {
            using (SqlConnection con =
                new SqlConnection(cs))
            {
                string query = @"
                    DELETE FROM Employee
                    WHERE EmployeeID = @EmployeeID";


                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue(
                        "@EmployeeID",
                        id);


                    con.Open();


                    int rows =
                        cmd.ExecuteNonQuery();


                    if (rows == 0)
                    {
                        return Request.CreateResponse(
                            HttpStatusCode.NotFound,
                            "Employee not found.");
                    }


                    return Request.CreateResponse(
                        HttpStatusCode.OK,
                        "Employee deleted successfully.");
                }
            }
        }
        catch (Exception ex)
        {
            return Request.CreateResponse(
                HttpStatusCode.InternalServerError,
                ex.Message);
        }
    }
}
