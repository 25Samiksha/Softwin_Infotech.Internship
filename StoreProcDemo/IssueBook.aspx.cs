using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class IssueBook : System.Web.UI.Page
{
    string cs = ConfigurationManager
        .ConnectionStrings["LibraryDBConnection"]
        .ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadStudents();
            LoadBooks();

            txtIssueDate.Text =
                DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
    private void LoadStudents()
    {
        using (SqlConnection con =
            new SqlConnection(cs))
        {
            string query = @"
                SELECT
                    StudentID,
                    StudentName
                FROM StudentsInfo
                ORDER BY StudentName";

            SqlDataAdapter da =
                new SqlDataAdapter(query, con);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            ddlStudent.DataSource = dt;
            ddlStudent.DataTextField = "StudentName";
            ddlStudent.DataValueField = "StudentID";
            ddlStudent.DataBind();

            ddlStudent.Items.Insert(
                0,
                new System.Web.UI.WebControls.ListItem(
                    "-- Select Student --",
                    "0"));
        }
    }

    private void LoadBooks()
    {
        using (SqlConnection con =
            new SqlConnection(cs))
        {
            string query = @"
                SELECT
                    BookID,
                    BookName
                FROM Books
                ORDER BY BookName";

            SqlDataAdapter da =
                new SqlDataAdapter(query, con);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            ddlBook.DataSource = dt;
            ddlBook.DataTextField = "BookName";
            ddlBook.DataValueField = "BookID";
            ddlBook.DataBind();

            ddlBook.Items.Insert(
                0,
                new System.Web.UI.WebControls.ListItem(
                    "-- Select Book --",
                    "0"));
        }
    }

    protected void btnIssue_Click(
        object sender,
        EventArgs e)
    {
        if (ddlStudent.SelectedValue == "0")
        {
            lblmsg.Text =
                "Please select a student.";

            lblmsg.ForeColor =
                System.Drawing.Color.Red;

            return;
        }
        if (ddlBook.SelectedValue == "0")
        {
            lblmsg.Text =
                "Please select a book.";

            lblmsg.ForeColor =
                System.Drawing.Color.Red;

            return;
        }
        DateTime issueDate;

        if (!DateTime.TryParse(
            txtIssueDate.Text,
            out issueDate))
        {
            lblmsg.Text =
                "Please enter a valid issue date.";

            lblmsg.ForeColor =
                System.Drawing.Color.Red;

            return;
        }

        using (SqlConnection con =
            new SqlConnection(cs))
        {
            SqlCommand cmd =
                new SqlCommand(
                    "IssueBook",
                    con);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@StudentID",
                Convert.ToInt32(
                    ddlStudent.SelectedValue));

            cmd.Parameters.AddWithValue(
                "@BookID",
                Convert.ToInt32(
                    ddlBook.SelectedValue));

            cmd.Parameters.AddWithValue(
                "@IssueDate",
                issueDate);

            con.Open();

            cmd.ExecuteNonQuery();

            lblmsg.Text =
                "Book issued successfully!";

            lblmsg.ForeColor =
                System.Drawing.Color.Green;

            ddlStudent.SelectedIndex = 0;
            ddlBook.SelectedIndex = 0;
        }
    }
    protected void btnView_Click(
        object sender,
        EventArgs e)
    {
        LoadIssuedBooks();
    }

    private void LoadIssuedBooks()
    {
        using (SqlConnection con =
            new SqlConnection(cs))
        {
            SqlCommand cmd =
                new SqlCommand(
                    "GetStudentBookDetails",
                    con);

            cmd.CommandType =
                CommandType.StoredProcedure;

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            gvIssuedBooks.DataSource =
                dt;

            gvIssuedBooks.DataBind();
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
}