using System;
using System.Globalization;

public partial class Attendance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetTodayDate();

            ClearAttendanceForm();

        }
    }
    private void SetTodayDate()
    {
        txtAttendanceDate.Text =
            DateTime.Now.ToString("dd/MM/yyyy");
    }
    protected void btnCheckIn_Click(object sender, EventArgs e)
    {
        if (ddlEmployee.SelectedValue == "")
        {
            lblMessage.Text =
                "Please select an employee.";

            return;
        }


        if (txtCheckIn.Text != "")
        {
            lblMessage.Text =
                "Employee has already checked in.";

            return;
        }


        txtCheckIn.Text =
            DateTime.Now.ToString("hh:mm tt");

        txtStatus.Text =
            "Present";

        lblMessage.Text =
            "Check-In recorded successfully.";
    }
    protected void btnCheckOut_Click(object sender, EventArgs e)
    {
        if (txtCheckIn.Text == "")
        {
            lblMessage.Text =
                "Please Check-In first.";

            return;
        }


        if (txtCheckOut.Text != "")
        {
            lblMessage.Text =
                "Employee has already checked out.";

            return;
        }


        txtCheckOut.Text =
            DateTime.Now.ToString("hh:mm tt");


        CalculateWorkingHours();


        lblMessage.Text =
            "Check-Out recorded successfully.";
    }
    private void CalculateWorkingHours()
    {
        DateTime checkIn;
        DateTime checkOut;


        bool checkInValid =
            DateTime.TryParseExact(
                txtCheckIn.Text,
                "hh:mm tt",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out checkIn);


        bool checkOutValid =
            DateTime.TryParseExact(
                txtCheckOut.Text,
                "hh:mm tt",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out checkOut);


        if (!checkInValid || !checkOutValid)
        {
            return;
        }


        TimeSpan workingTime =
            checkOut - checkIn;

        if (workingTime.TotalMinutes < 0)
        {
            workingTime =
                workingTime.Add(
                    TimeSpan.FromDays(1));
        }


        int hours =
            (int)workingTime.TotalHours;

        int minutes =
            workingTime.Minutes;


        txtWorkingHours.Text =
            hours + " hours " +
            minutes + " minutes";

        if (workingTime.TotalHours >= 8)
        {
            txtStatus.Text =
                "Present";
        }
        else if (workingTime.TotalHours >= 4)
        {
            txtStatus.Text =
                "Half Day";
        }
        else
        {
            txtStatus.Text =
                "Absent";
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlEmployee.SelectedValue == "")
        {
            lblMessage.Text =
                "Please select an employee.";

            return;
        }


        if (txtCheckIn.Text == "")
        {
            lblMessage.Text =
                "Please Check-In.";

            return;
        }


        if (txtCheckOut.Text == "")
        {
            lblMessage.Text =
                "Please Check-Out.";

            return;
        }


        if (txtWorkingHours.Text == "")
        {
            CalculateWorkingHours();
        }



        lblMessage.Text =
            "Attendance saved successfully.";


        ClearAttendanceForm();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string searchText =
            txtSearch.Text.Trim();


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAttendanceForm();

        lblMessage.Text = "";
    }
    private void ClearAttendanceForm()
    {
        ddlEmployee.SelectedIndex = 0;

        SetTodayDate();

        txtCheckIn.Text = "";

        txtCheckOut.Text = "";

        txtWorkingHours.Text = "";

        txtStatus.Text = "";

        lblMessage.Text = "";
    }
}