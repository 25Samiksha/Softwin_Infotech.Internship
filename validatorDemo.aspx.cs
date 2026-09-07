using System;
using System.Configuration;
using System.Data.SqlClient;

public partial class validatorDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtdate.Attributes["min"] =DateTime.Today.ToString("yyyy-MM-dd");
        }
    }

    protected void btnbook_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string connectionString =ConfigurationManager.ConnectionStrings["MovieBookingDBConnection"]
                .ConnectionString;

            using (SqlConnection con =new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Booking
                                (
                                    CustomerName,
                                    MobileNumber,
                                    Email,
                                    MovieName,
                                    NumberOfTickets,
                                    BookingDate
                                )
                VALUES
                 ( @CustomerName,@MobileNumber,@Email,@MovieName,@NumberOfTickets, @BookingDate)";

                using (SqlCommand cmd =new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CustomerName",txtname.Text.Trim());

                    cmd.Parameters.AddWithValue("@MobileNumber",txtnum.Text.Trim());

                    cmd.Parameters.AddWithValue("@Email",txtemail.Text.Trim());

                    cmd.Parameters.AddWithValue("@MovieName",ddmovie.SelectedItem.Text.Trim());

                    cmd.Parameters.AddWithValue("@NumberOfTickets",Convert.ToInt32(txtticketnum.Text));

                    cmd.Parameters.AddWithValue("@BookingDate",Convert.ToDateTime(txtdate.Text));

                    try
                    {
                        con.Open();

                        int result =cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            lblmessage.ForeColor =System.Drawing.Color.Green;

                            lblmessage.Text ="Ticket booked successfully!";
                            txtname.Text = "";
                            txtnum.Text = "";
                            txtemail.Text = "";
                            txtticketnum.Text = "";
                            txtdate.Text = "";

                            ddmovie.SelectedIndex = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblmessage.ForeColor =System.Drawing.Color.Red;

                        lblmessage.Text ="Database Error: " + ex.Message;
                    }
                }
            }
        }
    }
}