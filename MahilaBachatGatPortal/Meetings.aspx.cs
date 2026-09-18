using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Meetings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RoleHelper.RequirePresidentSecretary(this);
        if (!IsPostBack)
        {
            SetDefaultDates();

            LoadBachatGat();

            LoadMeetings();
        }
    }


    // ==========================================
    // DEFAULT DATES
    // ==========================================

    private void SetDefaultDates()
    {
        txtMeetingDate.Text =
            DateTime.Today.ToString("yyyy-MM-dd");

        txtNextMeetingDate.Text =
            DateTime.Today.AddMonths(1)
            .ToString("yyyy-MM-dd");
    }


    // ==========================================
    // LOAD BACHAT GAT
    // ==========================================

    private void LoadBachatGat()
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query = @"
                    SELECT
                        BachatGatID,
                        GatName
                    FROM BachatGat
                    WHERE Status = 'Active'
                    ORDER BY GatName";

                SqlDataAdapter da =
                    new SqlDataAdapter(query, con);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                ddlBachatGat.DataSource = dt;

                ddlBachatGat.DataTextField =
                    "GatName";

                ddlBachatGat.DataValueField =
                    "BachatGatID";

                ddlBachatGat.DataBind();
            }

            ddlBachatGat.Items.Insert(
                0,
                new ListItem(
                    "-- Select Bachat Gat --",
                    ""
                )
            );
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error loading Bachat Gat: " +
                ex.Message,
                System.Drawing.Color.Red
            );
        }
    }


    // ==========================================
    // SAVE / UPDATE MEETING
    // ==========================================

    protected void btnSave_Click(
        object sender,
        EventArgs e)
    {
        if (ddlBachatGat.SelectedValue == "")
        {
            ShowMessage(
                "Please select Bachat Gat.",
                System.Drawing.Color.Red
            );

            return;
        }


        if (string.IsNullOrWhiteSpace(
            txtMeetingDate.Text))
        {
            ShowMessage(
                "Please select Meeting Date.",
                System.Drawing.Color.Red
            );

            return;
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

                if (hfMeetingID.Value != "")
                {
                    string query = @"
                        UPDATE Meetings
                        SET
                            BachatGatID = @BachatGatID,
                            MeetingDate = @MeetingDate,
                            MeetingTime = @MeetingTime,
                            MeetingPlace = @MeetingPlace,
                            MeetingType = @MeetingType,
                            Agenda = @Agenda,
                            Minutes = @Minutes,
                            Decisions = @Decisions,
                            NextMeetingDate = @NextMeetingDate
                        WHERE MeetingID = @MeetingID";


                    SqlCommand cmd =
                        new SqlCommand(query, con);


                    AddParameters(cmd);


                    cmd.Parameters.AddWithValue(
                        "@MeetingID",
                        hfMeetingID.Value
                    );


                    cmd.ExecuteNonQuery();


                    ShowMessage(
                        "Meeting updated successfully!",
                        System.Drawing.Color.Green
                    );
                }


                // ==================================
                // INSERT
                // ==================================

                else
                {
                    string query = @"
                        INSERT INTO Meetings
                        (
                            BachatGatID,
                            MeetingDate,
                            MeetingTime,
                            MeetingPlace,
                            MeetingType,
                            Agenda,
                            Minutes,
                            Decisions,
                            NextMeetingDate
                        )
                        VALUES
                        (
                            @BachatGatID,
                            @MeetingDate,
                            @MeetingTime,
                            @MeetingPlace,
                            @MeetingType,
                            @Agenda,
                            @Minutes,
                            @Decisions,
                            @NextMeetingDate
                        )";


                    SqlCommand cmd =
                        new SqlCommand(query, con);


                    AddParameters(cmd);


                    cmd.ExecuteNonQuery();


                    ShowMessage(
                        "Meeting saved successfully!",
                        System.Drawing.Color.Green
                    );
                }
            }


            ClearForm();

            LoadMeetings();
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error saving meeting: " +
                ex.Message,
                System.Drawing.Color.Red
            );
        }
    }


    // ==========================================
    // PARAMETERS
    // ==========================================

    private void AddParameters(SqlCommand cmd)
    {
        cmd.Parameters.AddWithValue(
            "@BachatGatID",
            ddlBachatGat.SelectedValue
        );

        cmd.Parameters.AddWithValue(
            "@MeetingDate",
            Convert.ToDateTime(
                txtMeetingDate.Text
            )
        );

        cmd.Parameters.AddWithValue(
            "@MeetingTime",
            txtMeetingTime.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@MeetingPlace",
            txtMeetingPlace.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@MeetingType",
            ddlMeetingType.SelectedValue
        );

        cmd.Parameters.AddWithValue(
            "@Agenda",
            txtAgenda.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@Minutes",
            txtMinutes.Text.Trim()
        );

        cmd.Parameters.AddWithValue(
            "@Decisions",
            txtDecisions.Text.Trim()
        );


        if (string.IsNullOrWhiteSpace(
            txtNextMeetingDate.Text))
        {
            cmd.Parameters.AddWithValue(
                "@NextMeetingDate",
                DBNull.Value
            );
        }
        else
        {
            cmd.Parameters.AddWithValue(
                "@NextMeetingDate",
                Convert.ToDateTime(
                    txtNextMeetingDate.Text
                )
            );
        }
    }


    // ==========================================
    // LOAD MEETINGS
    // ==========================================

    private void LoadMeetings()
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query = @"
                    SELECT
                        M.MeetingID,
                        B.GatName,
                        M.MeetingDate,
                        M.MeetingTime,
                        M.MeetingPlace,
                        M.MeetingType,
                        M.NextMeetingDate
                    FROM Meetings M
                    INNER JOIN BachatGat B
                        ON M.BachatGatID =
                           B.BachatGatID
                    ORDER BY M.MeetingID DESC";


                SqlDataAdapter da =
                    new SqlDataAdapter(
                        query,
                        con
                    );


                DataTable dt =
                    new DataTable();


                da.Fill(dt);


                gvMeetings.DataSource = dt;

                gvMeetings.DataBind();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error loading meetings: " +
                ex.Message,
                System.Drawing.Color.Red
            );
        }
    }


    // ==========================================
    // SEARCH
    // ==========================================

    protected void btnSearch_Click(
        object sender,
        EventArgs e)
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query = @"
                    SELECT
                        M.MeetingID,
                        B.GatName,
                        M.MeetingDate,
                        M.MeetingTime,
                        M.MeetingPlace,
                        M.MeetingType,
                        M.NextMeetingDate
                    FROM Meetings M
                    INNER JOIN BachatGat B
                        ON M.BachatGatID =
                           B.BachatGatID
                    WHERE
                        B.GatName LIKE @Search
                        OR M.MeetingPlace LIKE @Search
                    ORDER BY M.MeetingID DESC";


                SqlCommand cmd =
                    new SqlCommand(query, con);


                cmd.Parameters.AddWithValue(
                    "@Search",
                    "%" +
                    txtSearch.Text.Trim() +
                    "%"
                );


                SqlDataAdapter da =
                    new SqlDataAdapter(cmd);


                DataTable dt =
                    new DataTable();


                da.Fill(dt);


                gvMeetings.DataSource = dt;

                gvMeetings.DataBind();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Search error: " +
                ex.Message,
                System.Drawing.Color.Red
            );
        }
    }


    // ==========================================
    // SHOW ALL
    // ==========================================

    protected void btnShowAll_Click(
        object sender,
        EventArgs e)
    {
        txtSearch.Text = "";

        LoadMeetings();
    }


    // ==========================================
    // EDIT / DELETE
    // ==========================================

    protected void gvMeetings_RowCommand(
        object sender,
        GridViewCommandEventArgs e)
    {
        int meetingID =
            Convert.ToInt32(
                e.CommandArgument
            );


        if (e.CommandName == "EditMeeting")
        {
            LoadMeetingForEdit(meetingID);
        }


        if (e.CommandName == "DeleteMeeting")
        {
            DeleteMeeting(meetingID);
        }
    }


    // ==========================================
    // LOAD MEETING FOR EDIT
    // ==========================================

    private void LoadMeetingForEdit(
        int meetingID)
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query = @"
                    SELECT *
                    FROM Meetings
                    WHERE MeetingID = @MeetingID";


                SqlCommand cmd =
                    new SqlCommand(
                        query,
                        con
                    );


                cmd.Parameters.AddWithValue(
                    "@MeetingID",
                    meetingID
                );


                con.Open();


                SqlDataReader dr =
                    cmd.ExecuteReader();


                if (dr.Read())
                {
                    hfMeetingID.Value =
                        dr["MeetingID"].ToString();


                    ddlBachatGat.SelectedValue =
                        dr["BachatGatID"].ToString();


                    txtMeetingDate.Text =
                        Convert.ToDateTime(
                            dr["MeetingDate"]
                        ).ToString("yyyy-MM-dd");


                    txtMeetingTime.Text =
                        dr["MeetingTime"].ToString();


                    txtMeetingPlace.Text =
                        dr["MeetingPlace"].ToString();


                    ddlMeetingType.SelectedValue =
                        dr["MeetingType"].ToString();


                    txtAgenda.Text =
                        dr["Agenda"].ToString();


                    txtMinutes.Text =
                        dr["Minutes"].ToString();


                    txtDecisions.Text =
                        dr["Decisions"].ToString();


                    if (dr["NextMeetingDate"]
                        != DBNull.Value)
                    {
                        txtNextMeetingDate.Text =
                            Convert.ToDateTime(
                                dr["NextMeetingDate"]
                            ).ToString(
                                "yyyy-MM-dd"
                            );
                    }


                    btnSave.Text =
                        "Update Meeting";


                    ShowMessage(
                        "Meeting loaded for editing.",
                        System.Drawing.Color.Blue
                    );
                }


                dr.Close();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error loading meeting: " +
                ex.Message,
                System.Drawing.Color.Red
            );
        }
    }


    // ==========================================
    // DELETE
    // ==========================================

    private void DeleteMeeting(
        int meetingID)
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query =
                    "DELETE FROM Meetings " +
                    "WHERE MeetingID = @MeetingID";


                SqlCommand cmd =
                    new SqlCommand(
                        query,
                        con
                    );


                cmd.Parameters.AddWithValue(
                    "@MeetingID",
                    meetingID
                );


                con.Open();

                cmd.ExecuteNonQuery();
            }


            ShowMessage(
                "Meeting deleted successfully!",
                System.Drawing.Color.Green
            );


            LoadMeetings();
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error deleting meeting: " +
                ex.Message,
                System.Drawing.Color.Red
            );
        }
    }


    // ==========================================
    // CLEAR
    // ==========================================

    protected void btnClear_Click(
        object sender,
        EventArgs e)
    {
        ClearForm();
    }


    private void ClearForm()
    {
        hfMeetingID.Value = "";

        ddlBachatGat.SelectedIndex = 0;

        txtMeetingDate.Text =
            DateTime.Today.ToString(
                "yyyy-MM-dd"
            );

        txtMeetingTime.Text = "";

        txtMeetingPlace.Text = "";

        ddlMeetingType.SelectedValue =
            "Monthly";

        txtAgenda.Text = "";

        txtMinutes.Text = "";

        txtDecisions.Text = "";

        txtNextMeetingDate.Text =
            DateTime.Today
            .AddMonths(1)
            .ToString("yyyy-MM-dd");

        btnSave.Text =
            "Save Meeting";
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