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

    private void LoadDashboardData()
    {
        int bachatGatID = RoleHelper.GetBachatGatID();
        int memberID = RoleHelper.GetMemberID();

        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = "";

            // ============================
            // ADMIN
            // ============================
            if (RoleHelper.IsAdmin())
            {
                query = @"
                    SELECT

                        (SELECT COUNT(*)
                         FROM BachatGat) AS TotalGats,

                        (SELECT COUNT(*)
                         FROM Members) AS TotalMembers,

                        (SELECT ISNULL(SUM(Amount), 0)
                         FROM MemberSavings) AS TotalSavings,

                        (SELECT ISNULL(SUM(LoanAmount), 0)
                         FROM Loans) AS TotalLoans,

                        (SELECT COUNT(*)
                         FROM Loans
                         WHERE Status = 'Pending') AS PendingLoans,

                        (SELECT COUNT(*)
                         FROM GovernmentSchemes
                         WHERE Status = 'Active') AS ActiveSchemes
                ";
            }

            // ============================
            // PRESIDENT / SECRETARY
            // ============================
            else if (RoleHelper.IsPresidentOrSecretary())
            {
                if (bachatGatID <= 0)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                query = @"
                    SELECT

                        /* CURRENT BACHAT GAT */
                        (SELECT COUNT(*)
                         FROM BachatGat
                         WHERE BachatGatID = @BachatGatID) AS TotalGats,

                        /* MEMBERS OF CURRENT GAT */
                        (SELECT COUNT(*)
                         FROM Members
                         WHERE BachatGatID = @BachatGatID) AS TotalMembers,

                        /* SAVINGS OF CURRENT GAT */
                        (SELECT ISNULL(SUM(Amount), 0)
                         FROM MemberSavings
                         WHERE BachatGatID = @BachatGatID) AS TotalSavings,

                        /* LOANS OF CURRENT GAT */
                        (SELECT ISNULL(SUM(LoanAmount), 0)
                         FROM Loans
                         WHERE BachatGatID = @BachatGatID) AS TotalLoans,

                        /* PENDING LOANS */
                        (SELECT COUNT(*)
                         FROM Loans
                         WHERE BachatGatID = @BachatGatID
                         AND Status = 'Pending') AS PendingLoans,

                        /* ACTIVE GOVERNMENT SCHEMES */
                        (SELECT COUNT(*)
                         FROM GovernmentSchemes
                         WHERE Status = 'Active') AS ActiveSchemes
                ";
            }

            // ============================
            // MEMBER
            // ============================
            else if (RoleHelper.IsMember())
            {
                if (bachatGatID <= 0 || memberID <= 0)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                query = @"
                    SELECT

                        /* MEMBER'S BACHAT GAT */
                        (SELECT COUNT(*)
                         FROM BachatGat
                         WHERE BachatGatID = @BachatGatID) AS TotalGats,

                        /* MEMBER'S OWN RECORD */
                        (SELECT COUNT(*)
                         FROM Members
                         WHERE MemberID = @MemberID
                         AND BachatGatID = @BachatGatID) AS TotalMembers,

                        /* MEMBER'S SAVINGS */
                        (SELECT ISNULL(SUM(Amount), 0)
                         FROM MemberSavings
                         WHERE MemberID = @MemberID
                         AND BachatGatID = @BachatGatID) AS TotalSavings,

                        /* MEMBER'S LOANS */
                        (SELECT ISNULL(SUM(LoanAmount), 0)
                         FROM Loans
                         WHERE MemberID = @MemberID
                         AND BachatGatID = @BachatGatID) AS TotalLoans,

                        /* MEMBER'S PENDING LOANS */
                        (SELECT COUNT(*)
                         FROM Loans
                         WHERE MemberID = @MemberID
                         AND BachatGatID = @BachatGatID
                         AND Status = 'Pending') AS PendingLoans,

                        /* ACTIVE GOVERNMENT SCHEMES */
                        (SELECT COUNT(*)
                         FROM GovernmentSchemes
                         WHERE Status = 'Active') AS ActiveSchemes
                ";
            }

            else
            {
                return;
            }

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                // President / Secretary / Member
                if (!RoleHelper.IsAdmin())
                {
                    cmd.Parameters.AddWithValue(
                        "@BachatGatID",
                        bachatGatID);
                }

                // Member only
                if (RoleHelper.IsMember())
                {
                    cmd.Parameters.AddWithValue(
                        "@MemberID",
                        memberID);
                }

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        lblTotalGats.Text =
                            dr["TotalGats"].ToString();

                        lblTotalMembers.Text =
                            dr["TotalMembers"].ToString();

                        lblTotalSavings.Text =
                            Convert.ToDecimal(
                                dr["TotalSavings"])
                                .ToString("N2");

                        lblTotalLoans.Text =
                            Convert.ToDecimal(
                                dr["TotalLoans"])
                                .ToString("N2");

                        lblPendingLoans.Text =
                            dr["PendingLoans"].ToString();

                        lblActiveSchemes.Text =
                            dr["ActiveSchemes"].ToString();
                    }
                }
            }
        }
    }
}