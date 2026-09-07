using System;

public partial class Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnBooks_Click(object sender, EventArgs e)
    {
        Response.Redirect("Library.aspx");
    }

    protected void btnStudents_Click(object sender, EventArgs e)
    {
        Response.Redirect("Studentinfo.aspx");
    }

    protected void btnIssueBook_Click(object sender, EventArgs e)
    {
        Response.Redirect("IssueBook.aspx");
    }
}