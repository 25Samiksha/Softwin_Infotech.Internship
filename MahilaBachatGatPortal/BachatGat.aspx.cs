using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class BachatGat : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RoleHelper.RequireAdmin(this);
        if (!IsPostBack)
        {
            LoadBachatGat();
        }
    }
    private void LoadBachatGat()
    {
        try
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = @"
                    SELECT
                        BachatGatID,
                        GatName,
                        RegistrationNumber,
                        Village,
                        Taluka,
                        District,
                        PresidentName,
                        TotalMembers,
                        MonthlySavingAmount,
                        Status
                    FROM BachatGat
                    ORDER BY BachatGatID DESC";

                SqlDataAdapter da =
                    new SqlDataAdapter(query, con);

                DataTable dt = new DataTable();

                da.Fill(dt);

                gvBachatGat.DataSource = dt;
                gvBachatGat.DataBind();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error loading Bachat Gat: " + ex.Message,
                System.Drawing.Color.Red
            );
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtGatName.Text))
        {
            ShowMessage(
                "Please enter Gat Name.",
                System.Drawing.Color.Red
            );

            return;
        }

        if (string.IsNullOrWhiteSpace(txtFormationDate.Text))
        {
            ShowMessage(
                "Please select Formation Date.",
                System.Drawing.Color.Red
            );

            return;
        }


        decimal monthlySaving = 0;

        if (!string.IsNullOrWhiteSpace(txtMonthlySaving.Text))
        {
            if (!decimal.TryParse(
                txtMonthlySaving.Text.Trim(),
                out monthlySaving))
            {
                ShowMessage(
                    "Please enter a valid Monthly Saving Amount.",
                    System.Drawing.Color.Red
                );

                return;
            }
        }


        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                con.Open();


                // ==================================
                // UPDATE
                // ==================================

                if (hfBachatGatID.Value != "")
                {
                    string query = @"
                        UPDATE BachatGat
                        SET
                            GatName = @GatName,
                            RegistrationNumber = @RegistrationNumber,
                            FormationDate = @FormationDate,
                            Village = @Village,
                            Taluka = @Taluka,
                            District = @District,
                            Address = @Address,
                            PresidentName = @PresidentName,
                            SecretaryName = @SecretaryName,
                            BankName = @BankName,
                            BankAccountNumber = @BankAccountNumber,
                            IFSCCode = @IFSCCode,
                            MonthlySavingAmount = @MonthlySavingAmount,
                            Status = @Status
                        WHERE BachatGatID = @BachatGatID";


                    SqlCommand cmd =
                        new SqlCommand(query, con);


                    AddParameters(cmd);


                    cmd.Parameters.AddWithValue(
                        "@BachatGatID",
                        hfBachatGatID.Value
                    );


                    cmd.ExecuteNonQuery();


                    ShowMessage(
                        "Bachat Gat updated successfully!",
                        System.Drawing.Color.Green
                    );
                }


                // ==================================
                // INSERT
                // ==================================

                else
                {
                    string query = @"
                        INSERT INTO BachatGat
                        (
                            GatName,
                            RegistrationNumber,
                            FormationDate,
                            Village,
                            Taluka,
                            District,
                            Address,
                            PresidentName,
                            SecretaryName,
                            BankName,
                            BankAccountNumber,
                            IFSCCode,
                            TotalMembers,
                            MonthlySavingAmount,
                            Status
                        )
                        VALUES
                        (
                            @GatName,
                            @RegistrationNumber,
                            @FormationDate,
                            @Village,
                            @Taluka,
                            @District,
                            @Address,
                            @PresidentName,
                            @SecretaryName,
                            @BankName,
                            @BankAccountNumber,
                            @IFSCCode,
                            0,
                            @MonthlySavingAmount,
                            @Status
                        )";


                    SqlCommand cmd =
                        new SqlCommand(query, con);


                    AddParameters(cmd);


                    cmd.ExecuteNonQuery();


                    ShowMessage(
                        "Bachat Gat added successfully!",
                        System.Drawing.Color.Green
                    );
                }
            }


            ClearForm();

            LoadBachatGat();
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error saving Bachat Gat: " + ex.Message,
                System.Drawing.Color.Red
            );
        }
    }


    // ==========================================
    // ADD PARAMETERS
    // ==========================================

    private void AddParameters(SqlCommand cmd)
    {
        cmd.Parameters.AddWithValue(
            "@GatName",
            txtGatName.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@RegistrationNumber",
            txtRegistrationNumber.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@FormationDate",
            Convert.ToDateTime(txtFormationDate.Text)
        );

        cmd.Parameters.AddWithValue(
            "@Village",
            txtVillage.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@Taluka",
            txtTaluka.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@District",
            txtDistrict.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@Address",
            txtAddress.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@PresidentName",
            txtPresidentName.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@SecretaryName",
            txtSecretaryName.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@BankName",
            txtBankName.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@BankAccountNumber",
            txtBankAccount.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@IFSCCode",
            txtIFSC.Text.Trim()
        );


        decimal monthlySaving = 0;

        decimal.TryParse(
            txtMonthlySaving.Text.Trim(),
            out monthlySaving
        );


        cmd.Parameters.AddWithValue(
            "@MonthlySavingAmount",
            monthlySaving
        );

        cmd.Parameters.AddWithValue(
            "@Status",
            ddlStatus.SelectedValue
        );
    }


    // ==========================================
    // SEARCH
    // ==========================================

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query = @"
                    SELECT
                        BachatGatID,
                        GatName,
                        RegistrationNumber,
                        Village,
                        Taluka,
                        District,
                        PresidentName,
                        TotalMembers,
                        MonthlySavingAmount,
                        Status
                    FROM BachatGat
                    WHERE
                        GatName LIKE @Search
                        OR RegistrationNumber LIKE @Search
                        OR Village LIKE @Search
                    ORDER BY BachatGatID DESC";


                SqlCommand cmd =
                    new SqlCommand(query, con);


                cmd.Parameters.AddWithValue(
                    "@Search",
                    "%" + txtSearch.Text.Trim() + "%"
                );


                SqlDataAdapter da =
                    new SqlDataAdapter(cmd);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);


                gvBachatGat.DataSource = dt;
                gvBachatGat.DataBind();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Search error: " + ex.Message,
                System.Drawing.Color.Red
            );
        }
    }


    // ==========================================
    // EDIT / DELETE
    // ==========================================

    protected void gvBachatGat_RowCommand(
        object sender,
        GridViewCommandEventArgs e)
    {
        int gatID =
            Convert.ToInt32(e.CommandArgument);


        if (e.CommandName == "EditGat")
        {
            LoadGatForEdit(gatID);
        }


        if (e.CommandName == "DeleteGat")
        {
            DeleteGat(gatID);
        }
    }


    // ==========================================
    // LOAD FOR EDIT
    // ==========================================

    private void LoadGatForEdit(int gatID)
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query = @"
                    SELECT *
                    FROM BachatGat
                    WHERE BachatGatID = @BachatGatID";


                SqlCommand cmd =
                    new SqlCommand(query, con);


                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    gatID
                );


                con.Open();


                SqlDataReader dr =
                    cmd.ExecuteReader();


                if (dr.Read())
                {
                    hfBachatGatID.Value =
                        dr["BachatGatID"].ToString();

                    txtGatName.Text =
                        dr["GatName"].ToString();

                    txtRegistrationNumber.Text =
                        dr["RegistrationNumber"].ToString();


                    if (dr["FormationDate"] != DBNull.Value)
                    {
                        txtFormationDate.Text =
                            Convert.ToDateTime(
                                dr["FormationDate"]
                            ).ToString("yyyy-MM-dd");
                    }


                    txtVillage.Text =
                        dr["Village"].ToString();

                    txtTaluka.Text =
                        dr["Taluka"].ToString();

                    txtDistrict.Text =
                        dr["District"].ToString();

                    txtAddress.Text =
                        dr["Address"].ToString();

                    txtPresidentName.Text =
                        dr["PresidentName"].ToString();

                    txtSecretaryName.Text =
                        dr["SecretaryName"].ToString();

                    txtBankName.Text =
                        dr["BankName"].ToString();

                    txtBankAccount.Text =
                        dr["BankAccountNumber"].ToString();

                    txtIFSC.Text =
                        dr["IFSCCode"].ToString();

                    txtMonthlySaving.Text =
                        dr["MonthlySavingAmount"].ToString();

                    ddlStatus.SelectedValue =
                        dr["Status"].ToString();

                    btnSave.Text =
                        "Update Bachat Gat";


                    ShowMessage(
                        "Bachat Gat loaded for editing.",
                        System.Drawing.Color.Blue
                    );
                }


                dr.Close();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error loading Bachat Gat: " + ex.Message,
                System.Drawing.Color.Red
            );
        }
    }


    // ==========================================
    // DELETE
    // ==========================================

    private void DeleteGat(int gatID)
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query =
                    "DELETE FROM BachatGat WHERE BachatGatID = @BachatGatID";


                SqlCommand cmd =
                    new SqlCommand(query, con);


                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    gatID
                );


                con.Open();


                cmd.ExecuteNonQuery();


                ShowMessage(
                    "Bachat Gat deleted successfully!",
                    System.Drawing.Color.Green
                );
            }


            LoadBachatGat();
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error deleting Bachat Gat: " + ex.Message,
                System.Drawing.Color.Red
            );
        }
    }


    // ==========================================
    // CLEAR
    // ==========================================

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearForm();
    }


    private void ClearForm()
    {
        hfBachatGatID.Value = "";

        txtGatName.Text = "";
        txtRegistrationNumber.Text = "";
        txtFormationDate.Text = "";
        txtVillage.Text = "";
        txtTaluka.Text = "";
        txtDistrict.Text = "";
        txtAddress.Text = "";
        txtPresidentName.Text = "";
        txtSecretaryName.Text = "";
        txtBankName.Text = "";
        txtBankAccount.Text = "";
        txtIFSC.Text = "";
        txtMonthlySaving.Text = "";

        ddlStatus.SelectedValue = "Active";

        btnSave.Text = "Save Bachat Gat";
    }


    // ==========================================
    // MESSAGE
    // ==========================================

    private void ShowMessage(
        string message,
        System.Drawing.Color color)
    {
        lblMessage.Text = message;
        lblMessage.ForeColor = color;
    }
}