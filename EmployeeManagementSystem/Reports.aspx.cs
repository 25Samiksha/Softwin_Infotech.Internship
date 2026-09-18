using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlAttendanceReport.Visible = false;
            pnlLeaveReport.Visible = false;

            btnExport.Enabled = false;

        }
    }
    protected void ddlReportType_SelectedIndexChanged(
        object sender,
        EventArgs e)
    {
        pnlAttendanceReport.Visible = false;
        pnlLeaveReport.Visible = false;

        btnExport.Enabled = false;


        if (ddlReportType.SelectedValue == "Attendance")
        {
            pnlAttendanceReport.Visible = true;

            lblReportTitle.Text =
                "Attendance Report";
        }


        if (ddlReportType.SelectedValue == "Leave")
        {
            pnlLeaveReport.Visible = true;

            lblReportTitle.Text =
                "Leave Report";
        }


        if (ddlReportType.SelectedValue == "")
        {
            lblReportTitle.Text =
                "Report Results";
        }
    }
    protected void btnSearch_Click(
        object sender,
        EventArgs e)
    {
        lblMessage.Text = "";


        string reportType =
            ddlReportType.SelectedValue;


        string employee =
            ddlEmployee.SelectedValue;


        string searchText =
            txtSearch.Text.Trim();


        string fromDate =
            txtFromDate.Text.Trim();


        string toDate =
            txtToDate.Text.Trim();


        string status =
            ddlStatus.SelectedValue;


        if (reportType == "")
        {
            lblMessage.Text =
                "Please select a report type.";

            return;
        }


        if (reportType == "Attendance")
        {
            pnlAttendanceReport.Visible = true;
            pnlLeaveReport.Visible = false;

            lblReportTitle.Text =
                "Attendance Report";


            btnExport.Enabled = true;
        }


        if (reportType == "Leave")
        {
            pnlAttendanceReport.Visible = false;
            pnlLeaveReport.Visible = true;

            lblReportTitle.Text =
                "Leave Report";

            btnExport.Enabled = true;
        }
    }
    protected void btnClear_Click(
        object sender,
        EventArgs e)
    {
        ddlReportType.SelectedIndex = 0;

        ddlEmployee.SelectedIndex = 0;

        ddlStatus.SelectedIndex = 0;

        txtFromDate.Text = "";

        txtToDate.Text = "";

        txtSearch.Text = "";


        pnlAttendanceReport.Visible = false;

        pnlLeaveReport.Visible = false;


        lblReportTitle.Text =
            "Report Results";


        lblMessage.Text = "";


        btnExport.Enabled = false;
    }
    protected void btnExport_Click(
        object sender,
        EventArgs e)
    {
        string reportType =
            ddlReportType.SelectedValue;


        if (reportType == "Attendance")
        {
            ExportAttendanceReport();
        }
        else if (reportType == "Leave")
        {
            ExportLeaveReport();
        }
        else
        {
            lblMessage.Text =
                "Please select a report type.";
        }
    }

    private void ExportAttendanceReport()
    {
        StringBuilder csv =
            new StringBuilder();


        csv.AppendLine(
            "Date,Employee,Check In,Check Out,Working Hours,Status");


        foreach (GridViewRow row in gvAttendance.Rows)
        {
            string date =
                GetCellText(row.Cells[0].Text);

            string employee =
                GetCellText(row.Cells[1].Text);

            string checkIn =
                GetCellText(row.Cells[2].Text);

            string checkOut =
                GetCellText(row.Cells[3].Text);

            string workingHours =
                GetCellText(row.Cells[4].Text);

            string status =
                GetCellText(row.Cells[5].Text);


            csv.AppendLine(
                "\"" + date + "\"," +
                "\"" + employee + "\"," +
                "\"" + checkIn + "\"," +
                "\"" + checkOut + "\"," +
                "\"" + workingHours + "\"," +
                "\"" + status + "\"");
        }


        Response.Clear();

        Response.Buffer = true;

        Response.AddHeader(
            "content-disposition",
            "attachment;filename=AttendanceReport.csv");

        Response.Charset = "";

        Response.ContentType =
            "text/csv";

        Response.Output.Write(
            csv.ToString());

        Response.Flush();

        Response.End();
    }

    private void ExportLeaveReport()
    {
        StringBuilder csv =
            new StringBuilder();


        csv.AppendLine(
            "ID,Employee,Leave Type,From Date,To Date,Days,Reason,Status");

        foreach (GridViewRow row in gvLeave.Rows)
        {
            string id =
                GetCellText(row.Cells[0].Text);

            string employee =
                GetCellText(row.Cells[1].Text);

            string leaveType =
                GetCellText(row.Cells[2].Text);

            string fromDate =
                GetCellText(row.Cells[3].Text);

            string toDate =
                GetCellText(row.Cells[4].Text);

            string days =
                GetCellText(row.Cells[5].Text);

            string reason =
                GetCellText(row.Cells[6].Text);

            string status =
                GetCellText(row.Cells[7].Text);


            csv.AppendLine(
                "\"" + id + "\"," +
                "\"" + employee + "\"," +
                "\"" + leaveType + "\"," +
                "\"" + fromDate + "\"," +
                "\"" + toDate + "\"," +
                "\"" + days + "\"," +
                "\"" + reason + "\"," +
                "\"" + status + "\"");
        }


        Response.Clear();

        Response.Buffer = true;

        Response.AddHeader(
            "content-disposition",
            "attachment;filename=LeaveReport.csv");

        Response.Charset = "";

        Response.ContentType =
            "text/csv";

        Response.Output.Write(
            csv.ToString());

        Response.Flush();

        Response.End();
    }

    private string GetCellText(string value)
    {
        if (value == "&nbsp;")
        {
            return "";
        }

        return
            Server.HtmlDecode(value);
    }
}