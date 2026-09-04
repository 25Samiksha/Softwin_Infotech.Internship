using System;
using System.Globalization;

public partial class Leave : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlDetails.Visible = false;

        }
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";

        if (ddlEmployee.SelectedValue == "")
        {
            lblMessage.Text =
                "Please select an employee.";

            return;
        }
        if (ddlLeaveType.SelectedValue == "")
        {
            lblMessage.Text =
                "Please select leave type.";

            return;
        }
        DateTime fromDate;

        bool validFromDate =
            DateTime.TryParseExact(
                txtFromDate.Text.Trim(),
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out fromDate);


        if (!validFromDate)
        {
            lblMessage.Text =
                "Please enter a valid From Date.";

            return;
        }
        DateTime toDate;

        bool validToDate =
            DateTime.TryParseExact(
                txtToDate.Text.Trim(),
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out toDate);


        if (!validToDate)
        {
            lblMessage.Text =
                "Please enter a valid To Date.";

            return;
        }
        if (toDate < fromDate)
        {
            lblMessage.Text =
                "To Date cannot be before From Date.";

            return;
        }
        if (txtReason.Text.Trim() == "")
        {
            lblMessage.Text =
                "Please enter leave reason.";

            return;
        }
        int leaveDays =
            (toDate - fromDate).Days + 1;


        txtLeaveDays.Text =
            leaveDays.ToString();


        lblMessage.Text =
            "Leave applied successfully. Status: Pending.";


        ClearLeaveForm();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string searchText =
            txtSearch.Text.Trim();

    }
    protected void gvLeaves_RowCommand(
        object sender,
        System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        string leaveID =
            e.CommandArgument.ToString();


        if (e.CommandName == "ViewLeave")
        {
            ViewLeave(leaveID);
        }


        if (e.CommandName == "CancelLeave")
        {
            CancelLeave(leaveID);
        }


        if (e.CommandName == "ApproveLeave")
        {
            ApproveLeave(leaveID);
        }


        if (e.CommandName == "RejectLeave")
        {
            RejectLeave(leaveID);
        }
    }
    private void ViewLeave(string leaveID)
    {
        pnlDetails.Visible = true;


        lblDetailsID.Text =
            leaveID;


        lblDetailsEmployee.Text =
            "Will be loaded from API";

        lblDetailsLeaveType.Text =
            "Will be loaded from API";

        lblDetailsFromDate.Text =
            "Will be loaded from API";

        lblDetailsToDate.Text =
            "Will be loaded from API";

        lblDetailsDays.Text =
            "Will be loaded from API";

        lblDetailsReason.Text =
            "Will be loaded from API";

        lblDetailsStatus.Text =
            "Will be loaded from API";
    }
    private void CancelLeave(string leaveID)
    {
        

        lblMessage.Text =
            "Leave cancellation will be connected to REST API.";
    }

    private void ApproveLeave(string leaveID)
    {
        
        lblMessage.Text =
            "Leave approval will be connected to REST API.";
    }
    private void RejectLeave(string leaveID)
    {
        lblMessage.Text =
            "Leave rejection will be connected to REST API.";
    }
    protected void btnCloseDetails_Click(
        object sender,
        EventArgs e)
    {
        pnlDetails.Visible = false;
    }
    protected void btnClear_Click(
        object sender,
        EventArgs e)
    {
        ClearLeaveForm();

        lblMessage.Text = "";
    }
    private void ClearLeaveForm()
    {
        ddlEmployee.SelectedIndex = 0;

        ddlLeaveType.SelectedIndex = 0;

        txtFromDate.Text = "";

        txtToDate.Text = "";

        txtLeaveDays.Text = "";

        txtReason.Text = "";
    }
}