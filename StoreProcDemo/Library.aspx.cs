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
                lblmsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
            if (txtaname.Text.Trim() == "")
            {
                lblmsg.Text = "Please enter Author Name.";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
            if (ddlcategory.SelectedValue == "")
            {
                lblmsg.Text = "Please select Category.";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
            decimal price;

            if (!decimal.TryParse(txtprice.Text.Trim(), out price))
            {
                lblmsg.Text = "Please enter valid Price.";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            int quantity;

            if (!int.TryParse(txtqty.Text.Trim(), out quantity))
            {
                lblmsg.Text = "Please enter valid Quantity.";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            using (SqlConnection con =
                new SqlConnection(connectionString))
            {
                
                using (SqlCommand cmd =
                    new SqlCommand("AddBook", con))
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

            ClearFields();

            lblmsg.Text =
                "Book saved successfully!";

            lblmsg.ForeColor =
                System.Drawing.Color.Green;

        }
        catch (Exception ex)
        {
            lblmsg.Text =
                "Error: " + ex.Message;

            lblmsg.ForeColor =
                System.Drawing.Color.Red;
        }
    }

    protected void btnview_Click(object sender, EventArgs e)
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
                    new SqlCommand("GetBooks", con))
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
        }
        catch (Exception ex)
        {
            lblmsg.Text =
                "Error: " + ex.Message;

            lblmsg.ForeColor =
                System.Drawing.Color.Red;
        }
    }


    private void ClearFields()
    {
        txtbname.Text = "";

        txtaname.Text = "";

        ddlcategory.SelectedIndex = 0;

        txtprice.Text = "";

        txtqty.Text = "";
    }
}