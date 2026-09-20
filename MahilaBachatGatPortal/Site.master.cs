using System;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("User.aspx");
            return;
        }

        string role = "";

        if (Session["Role"] != null)
        {
            role = Session["Role"].ToString();
        }

        if (Session["FullName"] != null)
        {
            lblUser.Text = Session["FullName"].ToString() + " (" + role + ")";
        }
        else
        {
            lblUser.Text = role;
        }

        HideAllMenus();

        if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
        {
            ShowAdminMenus();
        }
        else if (role.Equals("President", StringComparison.OrdinalIgnoreCase) ||
                 role.Equals("Secretary", StringComparison.OrdinalIgnoreCase))
        {
            ShowManagementMenus();
        }
        else if (role.Equals("Member", StringComparison.OrdinalIgnoreCase))
        {
            ShowMemberMenus();
        }
    }

    private void HideAllMenus()
    {
        menuBachatGatTitle.Visible = false;
        menuBachatGat.Visible = false;

        menuMemberTitle.Visible = false;
        menuMembers.Visible = false;

        menuSavingsTitle.Visible = false;
        menuSavings.Visible = false;

        menuMeetingsTitle.Visible = false;
        menuMeetings.Visible = false;
        menuAttendance.Visible = false;

        menuLoanTitle.Visible = false;
        menuLoans.Visible = false;
        menuRepayment.Visible = false;

        menuFinanceTitle.Visible = false;
        menuIncome.Visible = false;
        menuExpenses.Visible = false;

        menuSchemeTitle.Visible = false;
        menuSchemes.Visible = false;
        menuApplications.Visible = false;

        menuProductsTitle.Visible = false;
        menuProducts.Visible = false;
        menuSales.Visible = false;

        menuReportsTitle.Visible = false;
        menuReports.Visible = false;

        menuAdminTitle.Visible = false;
        menuUsers.Visible = false;

        menuMemberAccountTitle.Visible = false;
        menuMyProfile.Visible = false;

        lnkUser.HRef = "Dashboard.aspx";
    }

    private void ShowAdminMenus()
    {
        menuBachatGatTitle.Visible = true;
        menuBachatGat.Visible = true;

        menuMemberTitle.Visible = true;
        menuMembers.Visible = true;

        menuSchemeTitle.Visible = true;
        menuSchemes.Visible = true;
        menuApplications.Visible = true;

        menuProductsTitle.Visible = true;
        menuProducts.Visible = true;
        menuSales.Visible = true;

        menuReportsTitle.Visible = true;
        menuReports.Visible = true;

        menuAdminTitle.Visible = true;
        menuUsers.Visible = true;

        lnkUser.HRef = "Users.aspx";
    }

    private void ShowManagementMenus()
    {
        menuMemberTitle.Visible = true;
        menuMembers.Visible = true;

        menuSavingsTitle.Visible = true;
        menuSavings.Visible = true;

        menuMeetingsTitle.Visible = true;
        menuMeetings.Visible = true;
        menuAttendance.Visible = true;

        menuLoanTitle.Visible = true;
        menuLoans.Visible = true;
        menuRepayment.Visible = true;

        menuFinanceTitle.Visible = true;
        menuIncome.Visible = true;
        menuExpenses.Visible = true;

        menuSchemeTitle.Visible = true;
        menuSchemes.Visible = true;
        menuApplications.Visible = true;

        menuReportsTitle.Visible = true;
        menuReports.Visible = true;

        menuProductsTitle.Visible = true;
        menuProducts.Visible = true;
        menuSales.Visible = true;

        lnkUser.HRef = "Dashboard.aspx";
    }

    private void ShowMemberMenus()
    {
        menuMemberAccountTitle.Visible = true;
        menuMyProfile.Visible = true;

        menuSavingsTitle.Visible = true;
        menuSavings.Visible = true;

        menuLoanTitle.Visible = true;
        menuLoans.Visible = true;
        menuRepayment.Visible = true;

        menuSchemeTitle.Visible = false;
        menuSchemes.Visible = false;
        menuApplications.Visible = true;

        menuProductsTitle.Visible = false;
        menuProducts.Visible = false;
        menuSales.Visible = false;

        lnkUser.HRef = "Dashboard.aspx";
    }
}