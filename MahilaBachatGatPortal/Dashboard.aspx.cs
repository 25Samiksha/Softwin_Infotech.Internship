using System;
using System.Data.SqlClient;

public partial class Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RoleHelper.RequireLogin(this);

        if (!IsPostBack)
        {
            LoadDashboardData();
        }
    }


    // =====================================================
    // LOAD DASHBOARD DATA
    // =====================================================

    private void LoadDashboardData()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT

                    /* TOTAL BACHAT GAT */
                    (
                        SELECT COUNT(*)
                        FROM BachatGat
                    ) AS TotalGats,


                    /* TOTAL MEMBERS */
                    (
                        SELECT COUNT(*)
                        FROM Members
                    ) AS TotalMembers,


                    /* TOTAL SAVINGS */
                    (
                       SELECT ISNULL(SUM(Amount), 0) FROM MemberSavings
                    ) AS TotalSavings,


                    /* TOTAL LOANS */
                    (
                        SELECT ISNULL(SUM(LoanAmount), 0)
                        FROM Loans
                    ) AS TotalLoans,


                    /* PENDING LOANS */
                    (
                        SELECT COUNT(*)
                        FROM Loans
                        WHERE Status = 'Pending'
                    ) AS PendingLoans,


                    /* ACTIVE SCHEMES */
                    (
                        SELECT COUNT(*)
                        FROM GovernmentSchemes
                        WHERE Status = 'Active'
                    ) AS ActiveSchemes
            ";


            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                con.Open();

                using (SqlDataReader dr =
                    cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        // ---------------------------------
                        // TOTAL BACHAT GAT
                        // ---------------------------------

                        lblTotalGats.Text =
                            dr["TotalGats"].ToString();


                        // ---------------------------------
                        // TOTAL MEMBERS
                        // ---------------------------------

                        lblTotalMembers.Text =
                            dr["TotalMembers"].ToString();


                        // ---------------------------------
                        // TOTAL SAVINGS
                        // ---------------------------------

                        lblTotalSavings.Text =
                            Convert.ToDecimal(
                                dr["TotalSavings"])
                                .ToString("N2");


                        // ---------------------------------
                        // TOTAL LOANS
                        // ---------------------------------

                        lblTotalLoans.Text =
                            Convert.ToDecimal(
                                dr["TotalLoans"])
                                .ToString("N2");


                        // ---------------------------------
                        // PENDING LOANS
                        // ---------------------------------

                        lblPendingLoans.Text =
                            dr["PendingLoans"].ToString();


                        // ---------------------------------
                        // ACTIVE SCHEMES
                        // ---------------------------------

                        lblActiveSchemes.Text =
                            dr["ActiveSchemes"].ToString();
                    }
                }
            }
        }
    }
}