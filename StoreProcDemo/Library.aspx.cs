using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class Library : System.Web.UI.Page
{
    string connectionString =
        ConfigurationManager.ConnectionStrings["LibraryDBConnection"].ConnectionString;


    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtbname.Text.Trim() == "")
            {
                lblmsg.Text = "Please enter Book Name.";
                lblmsg.ForeColor =
                    System.Drawing.Color.Red;

                return;
            }


            if (txtaname.Text.Trim() == "")
            {
                lblmsg.Text = "Please enter Author Name.";
                lblmsg.ForeColor =
                    System.Drawing.Color.Red;

                return;
            }


            if (ddlcategory.SelectedValue == "")
            {
                lblmsg.Text = "Please select Category.";
                lblmsg.ForeColor =
                    System.Drawing.Color.Red;

                return;
            }


            decimal price;

            if (!decimal.TryParse(
                txtprice.Text.Trim(),
                out price))
            {
                lblmsg.Text =
                    "Please enter valid Price.";

                lblmsg.ForeColor =
                    System.Drawing.Color.Red;

                return;
            }


            int quantity;

            if (!int.TryParse(
                txtqty.Text.Trim(),
                out quantity))
            {
                lblmsg.Text =
                    "Please enter valid Quantity.";

                lblmsg.ForeColor =
                    System.Drawing.Color.Red;

                return;
            }


            using (SqlConnection con =
                new SqlConnection(connectionString))
            {
                using (SqlCommand cmd =
                    new SqlCommand(
                        "AddBook",
                        con))
                {
                    cmd.CommandType =
                        CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue(
                        "@BookName",
                        txtbname.Text.Trim());


                    cmd.Parameters.AddWithValue(
                        "@AuthorName",
                        txtaname.Text.Trim());


                    cmd.Parameters.AddWithValue(
                        "@Category",
                        ddlcategory.SelectedValue);


                    cmd.Parameters.AddWithValue(
                        "@Price",
                        price);


                    cmd.Parameters.AddWithValue(
                        "@Quantity",
                        quantity);


                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }


            lblmsg.Text =
                "Book saved successfully!";

            lblmsg.ForeColor =
                System.Drawing.Color.Green;


            ClearFields();

           
        }
        catch (Exception ex)
        {
            lblmsg.Text =
                "Error: " + ex.Message;

            lblmsg.ForeColor =
                System.Drawing.Color.Red;
        }
    }
    protected void btnview_Click(
        object sender,
        EventArgs e)
    {
        LoadBooks();
    }
    private void LoadBooks()
    {
        try
        {
            using (SqlConnection con =
                new SqlConnection(connectionString))
            {
                using (SqlCommand cmd =
                    new SqlCommand(
                        "GetBooks",
                        con))
                {
                    cmd.CommandType =
                        CommandType.StoredProcedure;


                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);


                    DataTable dt =
                        new DataTable();


                    da.Fill(dt);


                    gvBooks.DataSource = dt;

                    gvBooks.DataBind();


                    if (dt.Rows.Count == 0)
                    {
                        lblmsg.Text =
                            "No books found.";

                        lblmsg.ForeColor =
                            System.Drawing.Color.Red;
                    }
                    else
                    {
                        lblmsg.Text =
                            "Total Books: "
                            + dt.Rows.Count;

                        lblmsg.ForeColor =
                            System.Drawing.Color.Green;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text =
                "Error: " + ex.Message;

            lblmsg.ForeColor =
                System.Drawing.Color.Red;
        }
    }
    protected void gvBooks_SelectedIndexChanged(
        object sender,
        EventArgs e)
    {
        try
        {
            int rowIndex =
                gvBooks.SelectedIndex;


            GridViewRow row =
                gvBooks.Rows[rowIndex];

            int bookID =
                Convert.ToInt32(
                    gvBooks.DataKeys[rowIndex].Value);


            ViewState["SelectedBookID"] =
                bookID;

            txtbname.Text =
                Server.HtmlDecode(
                    row.Cells[1].Text);

            txtaname.Text =
                Server.HtmlDecode(
                    row.Cells[2].Text);

            ddlcategory.SelectedValue =
                Server.HtmlDecode(
                    row.Cells[3].Text);

            txtprice.Text =
                Server.HtmlDecode(
                    row.Cells[4].Text);

            txtqty.Text =
                Server.HtmlDecode(
                    row.Cells[5].Text);


            lblmsg.Text =
                "Book selected. You can now update it.";

            lblmsg.ForeColor =
                System.Drawing.Color.Blue;
        }
        catch (Exception ex)
        {
            lblmsg.Text =
                "Error: " + ex.Message;

            lblmsg.ForeColor =
                System.Drawing.Color.Red;
        }
    }
    protected void btnupdate_Click(
        object sender,
        EventArgs e)
    {
        try
        {
            if (ViewState["SelectedBookID"] == null)
            {
                lblmsg.Text =
                    "Please select a book first.";

                lblmsg.ForeColor =
                    System.Drawing.Color.Red;

                return;
            }


            int bookID =
                Convert.ToInt32(
                    ViewState["SelectedBookID"]);

            if (txtbname.Text.Trim() == "")
            {
                lblmsg.Text =
                    "Please enter Book Name.";

                lblmsg.ForeColor =
                    System.Drawing.Color.Red;

                return;
            }
            if (txtaname.Text.Trim() == "")
            {
                lblmsg.Text =
                    "Please enter Author Name.";

                lblmsg.ForeColor =
                    System.Drawing.Color.Red;

                return;
            }
            if (ddlcategory.SelectedValue == "")
            {
                lblmsg.Text =
                    "Please select Category.";

                lblmsg.ForeColor =
                    System.Drawing.Color.Red;

                return;
            }
            decimal price;

            if (!decimal.TryParse(
                txtprice.Text.Trim(),
                out price))
            {
                lblmsg.Text =
                    "Please enter valid Price.";

                lblmsg.ForeColor =
                    System.Drawing.Color.Red;

                return;
            }
            int quantity;

            if (!int.TryParse(
                txtqty.Text.Trim(),
                out quantity))
            {
                lblmsg.Text =
                    "Please enter valid Quantity.";

                lblmsg.ForeColor =
                    System.Drawing.Color.Red;

                return;
            }
            using (SqlConnection con =
                new SqlConnection(connectionString))
            {
                using (SqlCommand cmd =
                    new SqlCommand(
                        "UpdateBook",
                        con))
                {
                    cmd.CommandType =
                        CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue(
                        "@BookID",
                        bookID);


                    cmd.Parameters.AddWithValue(
                        "@BookName",
                        txtbname.Text.Trim());


                    cmd.Parameters.AddWithValue(
                        "@AuthorName",
                        txtaname.Text.Trim());


                    cmd.Parameters.AddWithValue(
                        "@Category",
                        ddlcategory.SelectedValue);


                    cmd.Parameters.AddWithValue(
                        "@Price",
                        price);


                    cmd.Parameters.AddWithValue(
                        "@Quantity",
                        quantity);


                    con.Open();


                    int result =
                        cmd.ExecuteNonQuery();


                    if (result > 0)
                    {
                        lblmsg.Text =
                            "Book updated successfully!";

                        lblmsg.ForeColor =
                            System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblmsg.Text =
                            "Book not found.";

                        lblmsg.ForeColor =
                            System.Drawing.Color.Red;
                    }
                }
            }

            LoadBooks();

            ClearFields();

            ViewState.Remove(
                "SelectedBookID");
        }
        catch (Exception ex)
        {
            lblmsg.Text =
                "Error: " + ex.Message;

            lblmsg.ForeColor =
                System.Drawing.Color.Red;
        }
    }
    protected void gvBooks_RowDeleting(
        object sender,
        GridViewDeleteEventArgs e)
    {
        try
        {
            int bookID =
                Convert.ToInt32(
                    gvBooks.DataKeys[
                        e.RowIndex].Value);


            using (SqlConnection con =
                new SqlConnection(connectionString))
            {
                using (SqlCommand cmd =
                    new SqlCommand(
                        "DeleteBook",
                        con))
                {
                    cmd.CommandType =
                        CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue(
                        "@BookID",
                        bookID);


                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }


            lblmsg.Text =
                "Book deleted successfully!";

            lblmsg.ForeColor =
                System.Drawing.Color.Green;


            LoadBooks();

            ClearFields();


            ViewState.Remove(
                "SelectedBookID");
        }
        catch (Exception ex)
        {
            lblmsg.Text =
                "Error: " + ex.Message;

            lblmsg.ForeColor =
                System.Drawing.Color.Red;
        }
    }
    protected void btnclear_Click(
        object sender,
        EventArgs e)
    {
        ClearFields();

        ViewState.Remove(
            "SelectedBookID");

        lblmsg.Text = "";
    }
    private void ClearFields()
    {
        txtbname.Text = "";

        txtaname.Text = "";

        ddlcategory.SelectedIndex = 0;

        txtprice.Text = "";

        txtqty.Text = "";
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
}