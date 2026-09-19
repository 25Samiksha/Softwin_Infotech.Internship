using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class MemberProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("User.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadBachatGat();
        }
    }

    private void LoadBachatGat()
    {
        string query = @"SELECT BachatGatID, GatName FROM BachatGat WHERE Status = 'Active' ORDER BY GatName";

        using (SqlConnection con = DBHelper.GetConnection())
        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            con.Open();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                ddlBachatGat.Items.Clear();
                ddlBachatGat.Items.Add(new ListItem("-- Select Bachat Gat --", ""));

                while (dr.Read())
                {
                    ddlBachatGat.Items.Add(new ListItem(dr["GatName"].ToString(), dr["BachatGatID"].ToString()));
                }
            }
        }

        ddlMember.Items.Clear();
        ddlMember.Items.Add(new ListItem("-- Select Member --", ""));
    }

    protected void ddlBachatGat_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMember.Items.Clear();
        ddlMember.Items.Add(new ListItem("-- Select Member --", ""));
        pnlProfile.Visible = false;
        lblMessage.Text = "";

        if (ddlBachatGat.SelectedValue == "")
        {
            return;
        }

        LoadMembers();
    }

    private void LoadMembers()
    {
        int bachatGatID = Convert.ToInt32(ddlBachatGat.SelectedValue);

        string query = @"SELECT MemberID, MemberName FROM Members WHERE BachatGatID = @BachatGatID AND Status = 'Active' ORDER BY MemberName";

        using (SqlConnection con = DBHelper.GetConnection())
        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue("@BachatGatID", bachatGatID);
            con.Open();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    ddlMember.Items.Add(new ListItem(dr["MemberName"].ToString(), dr["MemberID"].ToString()));
                }
            }
        }
    }

    protected void btnViewProfile_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        pnlProfile.Visible = false;

        if (ddlBachatGat.SelectedValue == "")
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Please select Bachat Gat.";
            return;
        }

        if (ddlMember.SelectedValue == "")
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Please select Member.";
            return;
        }

        int memberID = Convert.ToInt32(ddlMember.SelectedValue);
        int bachatGatID = Convert.ToInt32(ddlBachatGat.SelectedValue);

        if (!IsMemberInSelectedGat(memberID, bachatGatID))
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Invalid member selection.";
            return;
        }

        LoadProfile(memberID);
    }

    private bool IsMemberInSelectedGat(int memberID, int bachatGatID)
    {
        string query = @"SELECT COUNT(*) FROM Members WHERE MemberID = @MemberID AND BachatGatID = @BachatGatID";

        using (SqlConnection con = DBHelper.GetConnection())
        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue("@MemberID", memberID);
            cmd.Parameters.AddWithValue("@BachatGatID", bachatGatID);
            con.Open();

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }
    }

    private void LoadProfile(int memberID)
    {
        string query = @"
            SELECT M.MemberID, M.MemberCode, M.MemberName, M.FatherOrHusbandName, M.Gender, M.DateOfBirth, M.Mobile, M.Email, M.Address, M.Village, M.Taluka, M.District, M.JoinDate, M.Occupation, M.BankName, M.BankAccountNumber, M.IFSCCode, M.Status, B.GatName
            FROM Members M
            LEFT JOIN BachatGat B ON M.BachatGatID = B.BachatGatID
            WHERE M.MemberID = @MemberID";

        using (SqlConnection con = DBHelper.GetConnection())
        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue("@MemberID", memberID);
            con.Open();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    lblMemberCode.Text = GetValue(dr["MemberCode"]);
                    lblMemberName.Text = GetValue(dr["MemberName"]);
                    lblFatherHusband.Text = GetValue(dr["FatherOrHusbandName"]);
                    lblGender.Text = GetValue(dr["Gender"]);
                    lblDOB.Text = FormatDate(dr["DateOfBirth"]);
                    lblMobile.Text = GetValue(dr["Mobile"]);
                    lblEmail.Text = GetValue(dr["Email"]);
                    lblGatName.Text = GetValue(dr["GatName"]);
                    lblJoinDate.Text = FormatDate(dr["JoinDate"]);
                    lblOccupation.Text = GetValue(dr["Occupation"]);
                    lblVillage.Text = GetValue(dr["Village"]);
                    lblTaluka.Text = GetValue(dr["Taluka"]);
                    lblDistrict.Text = GetValue(dr["District"]);
                    lblBankName.Text = GetValue(dr["BankName"]);
                    lblAccountNumber.Text = GetValue(dr["BankAccountNumber"]);
                    lblIFSC.Text = GetValue(dr["IFSCCode"]);
                    lblStatus.Text = GetValue(dr["Status"]);

                    LoadSavings(memberID);
                    LoadLoans(memberID);
                    LoadRepayments(memberID);

                    pnlProfile.Visible = true;
                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Profile not found.";
                    pnlProfile.Visible = false;
                }
            }
        }
    }

    private void LoadSavings(int memberID)
    {
        string query = @"
            SELECT SavingID, SavingMonth, Amount, PaymentDate, PaymentMode, ReceiptNumber, Remarks
            FROM MemberSavings
            WHERE MemberID = @MemberID
            ORDER BY PaymentDate DESC";

        using (SqlConnection con = DBHelper.GetConnection())
        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue("@MemberID", memberID);

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvSavings.DataSource = dt;
                gvSavings.DataBind();

                decimal totalSavings = 0;

                foreach (DataRow row in dt.Rows)
                {
                    if (row["Amount"] != DBNull.Value)
                    {
                        totalSavings += Convert.ToDecimal(row["Amount"]);
                    }
                }

                lblTotalSavings.Text = totalSavings.ToString("N2");
            }
        }
    }

    private void LoadLoans(int memberID)
    {
        string query = @"
            SELECT LoanID, ApplicationDate, LoanAmount, ApprovedAmount, InterestRate, LoanPurpose, LoanTermMonths, EMIAmount, ApprovalDate, DistributionDate, Status
            FROM Loans
            WHERE MemberID = @MemberID
            ORDER BY LoanID DESC";

        using (SqlConnection con = DBHelper.GetConnection())
        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue("@MemberID", memberID);

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvLoans.DataSource = dt;
                gvLoans.DataBind();
            }
        }
    }

    private void LoadRepayments(int memberID)
    {
        string query = @"
            SELECT LR.LoanID, LR.EMIInstallmentNo, LR.DueDate, LR.PaymentDate, LR.EMIAmount, LR.PrincipalAmount, LR.InterestAmount, LR.PaidAmount, LR.PaymentMode, LR.ReceiptNumber, LR.Status
            FROM LoanRepayments LR
            INNER JOIN Loans L ON LR.LoanID = L.LoanID
            WHERE L.MemberID = @MemberID
            ORDER BY LR.PaymentDate DESC";

        using (SqlConnection con = DBHelper.GetConnection())
        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue("@MemberID", memberID);

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvRepayments.DataSource = dt;
                gvRepayments.DataBind();
            }
        }
    }

    private string GetValue(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return "-";
        }

        string text = value.ToString().Trim();

        if (text == "")
        {
            return "-";
        }

        return text;
    }

    private string FormatDate(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return "-";
        }

        return Convert.ToDateTime(value).ToString("dd-MM-yyyy");
    }
}