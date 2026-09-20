using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class BachatGat : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RoleHelper.RequireManagement(this);

        if (!IsPostBack)
        {
            LoadBachatGat();
        }
    }


    // =========================================================
    // LOAD ALL BACHAT GATS
    // =========================================================

    private void LoadBachatGat()
    {
        try
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "";
                if (RoleHelper.IsAdmin())
                {
                    query = @"
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
                }
                else
                {
                    query = @"
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
                        WHERE BachatGatID = @BachatGatID
                        ORDER BY BachatGatID DESC";
                }

                SqlCommand cmd = new SqlCommand(query, con);
                if (!RoleHelper.IsAdmin())
                {
                    cmd.Parameters.AddWithValue("@BachatGatID", RoleHelper.GetBachatGatID());
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);

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


    // =========================================================
    // SAVE / UPDATE
    // =========================================================

    protected void btnSave_Click(object sender, EventArgs e)
    {
        // -----------------------------------------------------
        // Gat Name validation
        // -----------------------------------------------------

        if (string.IsNullOrWhiteSpace(txtGatName.Text))
        {
            ShowMessage(
                "Please enter Gat Name.",
                System.Drawing.Color.Red
            );

            return;
        }


        // -----------------------------------------------------
        // Formation Date validation
        // -----------------------------------------------------

        if (string.IsNullOrWhiteSpace(txtFormationDate.Text))
        {
            ShowMessage(
                "Please select Formation Date.",
                System.Drawing.Color.Red
            );

            return;
        }


        DateTime formationDate;

        if (!DateTime.TryParse(
            txtFormationDate.Text.Trim(),
            out formationDate))
        {
            ShowMessage(
                "Please enter a valid Formation Date.",
                System.Drawing.Color.Red
            );

            return;
        }


        // -----------------------------------------------------
        // President Name validation
        // -----------------------------------------------------

        if (string.IsNullOrWhiteSpace(txtPresidentName.Text))
        {
            ShowMessage(
                "Please enter President Name.",
                System.Drawing.Color.Red
            );

            return;
        }


        // -----------------------------------------------------
        // Monthly Saving validation
        // -----------------------------------------------------

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


        // -----------------------------------------------------
        // Determine New or Update
        // -----------------------------------------------------

        bool isNewGat =
            string.IsNullOrWhiteSpace(
                hfBachatGatID.Value
            );

        if (!RoleHelper.IsAdmin())
        {
            if (isNewGat)
            {
                ShowMessage("Only Admins can create new Bachat Gats.", System.Drawing.Color.Red);
                return;
            }
            else if (hfBachatGatID.Value != RoleHelper.GetBachatGatID().ToString())
            {
                ShowMessage("You can only edit your own Bachat Gat.", System.Drawing.Color.Red);
                return;
            }
        }


        // -----------------------------------------------------
        // President login details required for NEW Gat
        // -----------------------------------------------------

        if (isNewGat)
        {
            if (string.IsNullOrWhiteSpace(
                txtPresidentUsername.Text))
            {
                ShowMessage(
                    "Please enter President Username.",
                    System.Drawing.Color.Red
                );

                return;
            }


            if (string.IsNullOrWhiteSpace(
                txtPresidentPassword.Text))
            {
                ShowMessage(
                    "Please enter President Password.",
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


                // ------------------------------------------------
                // Start transaction
                // ------------------------------------------------

                SqlTransaction transaction =
                    con.BeginTransaction();


                try
                {
                    if (isNewGat)
                    {
                        // -----------------------------------------
                        // NEW BACHAT GAT
                        // -----------------------------------------

                        int newBachatGatID =
                            InsertBachatGatAndPresident(
                                con,
                                transaction
                            );


                        transaction.Commit();


                        ShowMessage(
                            "Bachat Gat created successfully! President account created and assigned to Bachat Gat ID "
                            + newBachatGatID
                            + ".",
                            System.Drawing.Color.Green
                        );
                    }
                    else
                    {
                        // -----------------------------------------
                        // UPDATE EXISTING BACHAT GAT
                        // -----------------------------------------

                        UpdateBachatGat(
                            con,
                            transaction
                        );


                        transaction.Commit();


                        ShowMessage(
                            "Bachat Gat updated successfully!",
                            System.Drawing.Color.Green
                        );
                    }
                }
                catch
                {
                    transaction.Rollback();

                    throw;
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


    // =========================================================
    // INSERT BACHAT GAT + PRESIDENT
    // =========================================================

    private int InsertBachatGatAndPresident(
        SqlConnection con,
        SqlTransaction transaction)
    {
        // -----------------------------------------------------
        // STEP 1:
        // Insert Bachat Gat
        // -----------------------------------------------------

        string gatQuery = @"
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
            );

            SELECT CAST(SCOPE_IDENTITY() AS INT);
        ";


        SqlCommand gatCmd =
            new SqlCommand(
                gatQuery,
                con,
                transaction
            );


        AddParameters(gatCmd);


        // -----------------------------------------------------
        // Get newly generated BachatGatID
        // -----------------------------------------------------

        int newBachatGatID =
            Convert.ToInt32(
                gatCmd.ExecuteScalar()
            );


        // -----------------------------------------------------
        // STEP 2:
        // Check President username
        // -----------------------------------------------------

        string checkUsernameQuery = @"
            SELECT COUNT(*)
            FROM Users
            WHERE Username = @Username";


        SqlCommand checkCmd =
            new SqlCommand(
                checkUsernameQuery,
                con,
                transaction
            );


        checkCmd.Parameters.AddWithValue(
            "@Username",
            txtPresidentUsername.Text.Trim()
        );


        int existingUserCount =
            Convert.ToInt32(
                checkCmd.ExecuteScalar()
            );


        if (existingUserCount > 0)
        {
            throw new Exception(
                "President username already exists. Please enter a different username."
            );
        }


        // -----------------------------------------------------
        // STEP 3:
        // Create President User
        // -----------------------------------------------------

        string userQuery = @"
            INSERT INTO Users
            (
                Username,
                Password,
                FullName,
                Role,
                Mobile,
                Email,
                IsActive,
                BachatGatID,
                CreatedDate
            )
            VALUES
            (
                @Username,
                @Password,
                @FullName,
                'President',
                NULL,
                NULL,
                1,
                @BachatGatID,
                GETDATE()
            )";


        SqlCommand userCmd =
            new SqlCommand(
                userQuery,
                con,
                transaction
            );


        userCmd.Parameters.AddWithValue(
            "@Username",
            txtPresidentUsername.Text.Trim()
        );


        userCmd.Parameters.AddWithValue(
            "@Password",
            txtPresidentPassword.Text.Trim()
        );


        userCmd.Parameters.AddWithValue(
            "@FullName",
            txtPresidentName.Text.Trim()
        );


        // -----------------------------------------------------
        // THIS IS THE IMPORTANT LINK
        // -----------------------------------------------------

        userCmd.Parameters.AddWithValue(
            "@BachatGatID",
            newBachatGatID
        );


        userCmd.ExecuteNonQuery();


        // -----------------------------------------------------
        // Return newly generated Gat ID
        // -----------------------------------------------------

        return newBachatGatID;
    }


    // =========================================================
    // UPDATE BACHAT GAT
    // =========================================================

    private void UpdateBachatGat(
        SqlConnection con,
        SqlTransaction transaction)
    {
        int gatID =
            Convert.ToInt32(
                hfBachatGatID.Value
            );


        // -----------------------------------------------------
        // Update Bachat Gat
        // -----------------------------------------------------

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
            new SqlCommand(
                query,
                con,
                transaction
            );


        AddParameters(cmd);


        cmd.Parameters.AddWithValue(
            "@BachatGatID",
            gatID
        );


        cmd.ExecuteNonQuery();


        // -----------------------------------------------------
        // Update existing President's name
        // -----------------------------------------------------

        string presidentQuery = @"
            UPDATE Users
            SET
                FullName = @FullName
            WHERE
                BachatGatID = @BachatGatID
                AND Role = 'President'";


        SqlCommand presidentCmd =
            new SqlCommand(
                presidentQuery,
                con,
                transaction
            );


        presidentCmd.Parameters.AddWithValue(
            "@FullName",
            txtPresidentName.Text.Trim()
        );


        presidentCmd.Parameters.AddWithValue(
            "@BachatGatID",
            gatID
        );


        presidentCmd.ExecuteNonQuery();
    }


    // =========================================================
    // COMMON PARAMETERS
    // =========================================================

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
            Convert.ToDateTime(
                txtFormationDate.Text.Trim()
            )
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


    // =========================================================
    // SEARCH
    // =========================================================

    protected void btnSearch_Click(
        object sender,
        EventArgs e)
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query = "";
                if (RoleHelper.IsAdmin())
                {
                    query = @"
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
                }
                else
                {
                    query = @"
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
                        WHERE BachatGatID = @BachatGatID
                        AND (
                            GatName LIKE @Search
                            OR RegistrationNumber LIKE @Search
                            OR Village LIKE @Search
                        )
                        ORDER BY BachatGatID DESC";
                }


                SqlCommand cmd =
                    new SqlCommand(
                        query,
                        con
                    );

                if (!RoleHelper.IsAdmin())
                {
                    cmd.Parameters.AddWithValue("@BachatGatID", RoleHelper.GetBachatGatID());
                }

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


    // =========================================================
    // GRID ROW COMMAND
    // =========================================================

    protected void gvBachatGat_RowCommand(
        object sender,
        GridViewCommandEventArgs e)
    {
        int gatID =
            Convert.ToInt32(
                e.CommandArgument
            );


        if (e.CommandName == "EditGat")
        {
            LoadGatForEdit(gatID);
        }


        if (e.CommandName == "DeleteGat")
        {
            DeleteGat(gatID);
        }
    }


    // =========================================================
    // LOAD GAT FOR EDIT
    // =========================================================

    private void LoadGatForEdit(int gatID)
    {
        if (!RoleHelper.IsAdmin() && gatID != RoleHelper.GetBachatGatID())
        {
            ShowMessage("You can only edit your own Bachat Gat.", System.Drawing.Color.Red);
            return;
        }

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
                    new SqlCommand(
                        query,
                        con
                    );


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


                    // -----------------------------------------
                    // Load existing President username
                    // -----------------------------------------

                    LoadPresidentUsername(gatID);


                    // -----------------------------------------
                    // Never load existing password
                    // -----------------------------------------

                    txtPresidentPassword.Text = "";


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


    // =========================================================
    // LOAD PRESIDENT USERNAME
    // =========================================================

    private void LoadPresidentUsername(int gatID)
    {
        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                SELECT TOP 1 Username
                FROM Users
                WHERE
                    BachatGatID = @BachatGatID
                    AND Role = 'President'";


            SqlCommand cmd =
                new SqlCommand(
                    query,
                    con
                );


            cmd.Parameters.AddWithValue(
                "@BachatGatID",
                gatID
            );


            con.Open();


            object result =
                cmd.ExecuteScalar();


            if (result != null &&
                result != DBNull.Value)
            {
                txtPresidentUsername.Text =
                    result.ToString();
            }
            else
            {
                txtPresidentUsername.Text = "";
            }
        }
    }


    // =========================================================
    // DELETE
    // =========================================================

    private void DeleteGat(int gatID)
    {
        if (!RoleHelper.IsAdmin())
        {
            ShowMessage("Only Admins can delete a Bachat Gat.", System.Drawing.Color.Red);
            return;
        }

        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                con.Open();


                SqlTransaction transaction =
                    con.BeginTransaction();


                try
                {
                    // -----------------------------------------
                    // Delete President account first
                    // -----------------------------------------

                    string userQuery = @"
                        DELETE FROM Users
                        WHERE
                            BachatGatID = @BachatGatID
                            AND Role = 'President'";


                    SqlCommand userCmd =
                        new SqlCommand(
                            userQuery,
                            con,
                            transaction
                        );


                    userCmd.Parameters.AddWithValue(
                        "@BachatGatID",
                        gatID
                    );


                    userCmd.ExecuteNonQuery();


                    // -----------------------------------------
                    // Delete Bachat Gat
                    // -----------------------------------------

                    string gatQuery = @"
                        DELETE FROM BachatGat
                        WHERE BachatGatID = @BachatGatID";


                    SqlCommand gatCmd =
                        new SqlCommand(
                            gatQuery,
                            con,
                            transaction
                        );


                    gatCmd.Parameters.AddWithValue(
                        "@BachatGatID",
                        gatID
                    );


                    gatCmd.ExecuteNonQuery();


                    transaction.Commit();


                    ShowMessage(
                        "Bachat Gat and its President account deleted successfully!",
                        System.Drawing.Color.Green
                    );
                }
                catch
                {
                    transaction.Rollback();

                    throw;
                }
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


    // =========================================================
    // CLEAR
    // =========================================================

    protected void btnClear_Click(
        object sender,
        EventArgs e)
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

        txtPresidentUsername.Text = "";

        txtPresidentPassword.Text = "";


        txtSecretaryName.Text = "";


        txtBankName.Text = "";

        txtBankAccount.Text = "";

        txtIFSC.Text = "";


        txtMonthlySaving.Text = "";


        ddlStatus.SelectedValue =
            "Active";


        btnSave.Text =
            "Save Bachat Gat";
    }


    // =========================================================
    // MESSAGE
    // =========================================================

    private void ShowMessage(
        string message,
        System.Drawing.Color color)
    {
        lblMessage.Text = message;

        lblMessage.ForeColor = color;
    }
}