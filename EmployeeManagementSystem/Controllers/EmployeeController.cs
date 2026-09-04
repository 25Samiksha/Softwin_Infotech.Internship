using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            string connectionString =
                ConfigurationManager.ConnectionStrings["EmployeeDBConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT 
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
                                 FROM Employee";

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Employee emp = new Employee();

                    emp.EmployeeID = Convert.ToInt32(reader["EmployeeID"]);
                    emp.EmployeeName = reader["EmployeeName"].ToString();
                    emp.Email = reader["Email"].ToString();
                    emp.Mobile = reader["Mobile"].ToString();
                    emp.Gender = reader["Gender"].ToString();

                    if (reader["DateOfBirth"] != DBNull.Value)
                        emp.DateOfBirth = Convert.ToDateTime(
                            reader["DateOfBirth"]).ToString("yyyy-MM-dd");

                    emp.Address = reader["Address"].ToString();

                    if (reader["DepartmentID"] != DBNull.Value)
                        emp.DepartmentID = Convert.ToInt32(reader["DepartmentID"]);

                    if (reader["DesignationID"] != DBNull.Value)
                        emp.DesignationID = Convert.ToInt32(reader["DesignationID"]);

                    if (reader["Salary"] != DBNull.Value)
                        emp.Salary = Convert.ToDecimal(reader["Salary"]);

                    if (reader["JoiningDate"] != DBNull.Value)
                        emp.JoiningDate = Convert.ToDateTime(
                            reader["JoiningDate"]).ToString("yyyy-MM-dd");

                    emp.Status = reader["Status"].ToString();
                    emp.Photo = reader["Photo"].ToString();

                    employees.Add(emp);
                }
            }

            return Ok(employees);
        }
    }
}