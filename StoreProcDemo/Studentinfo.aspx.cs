
using System;
using System.Data.SqlClient;
using System.Configuration;

public partial class Studentinfo : System.Web.UI.Page
{
    string cs = ConfigurationManager
        .ConnectionStrings["LibraryDBConnection"]
        .ConnectionString;


    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btnsave_Click(object sender, EventArgs e)
    {
        using (SqlConnection con =
            new SqlConnection(cs))
        {
            string query = @"
                INSERT INTO StudentsInfo
                (
                    StudentName,
                    Email,
                    Course,
                    Branch,
                    Phone
                )
                VALUES
                (
                    @StudentName,
                    @Email,
                    @Course,
                    @Branch,
                    @Phone
                )";


            SqlCommand cmd =
                new SqlCommand(query, con);


            cmd.Parameters.AddWithValue(
                "@StudentName",
                txtstdname.Text);


            cmd.Parameters.AddWithValue(
                "@Email",
                txtemail.Text);


            cmd.Parameters.AddWithValue(
                "@Course",
                ddlcourse.SelectedValue);


            cmd.Parameters.AddWithValue(
                "@Branch",
                ddlbranch.SelectedValue);


            cmd.Parameters.AddWithValue(
                "@Phone",
                txtphno.Text);


            con.Open();


            cmd.ExecuteNonQuery();


            lblmsg.Text =
                "Student saved successfully!";


            lblmsg.ForeColor =
                System.Drawing.Color.Green;


            ClearFields();
        }
    }


    private void ClearFields()
    {
        txtstdname.Text = "";

        txtemail.Text = "";

        ddlcourse.SelectedIndex = 0;

        ddlbranch.SelectedIndex = 0;

        txtphno.Text = "";
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
}

