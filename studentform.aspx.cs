using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class studentform : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void btnregister_Click1(object sender, EventArgs e)
    {
        string name = txtname.Text;
        string email = txtemail.Text;
        string phone = phno.Text;
        string address = txtaddress.Text;

        string course = "";

        if (chkcsharp.Checked)
        {
            course += "C# ";
        }

        if (chkdotnet.Checked)
        {
            course += ".NET ";
        }

        if (chkwebdp.Checked)
        {
            course += "Web Development ";
        }

        if (chkpython.Checked)
        {
            course += "Python ";
        }

        lblOutName.Text = name;
        lblOutEmail.Text = email;
        lblOutPhone.Text = phone;
        lblOutAddress.Text = address;
        lblOutCourse.Text = course;
        pnlDetails.Visible = true;
    }
}
