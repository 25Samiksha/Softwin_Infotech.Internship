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

    private bool IsAdmin()
    {
        return Session["Role"] != null &&
               Session["Role"].ToString().Equals(
                   "Admin",
                   StringComparison.OrdinalIgnoreCase
               );
    }

    private int GetBachatGatID()
    {
        return RoleHelper.GetBachatGatID();
    }

    private void SetDefaultDates()
    {
        txtMeetingDate.Text =
            DateTime.Today.ToString("yyyy-MM-dd");

        txtNextMeetingDate.Text =
            DateTime.Today.AddMonths(1)
            .ToString("yyyy-MM-dd");
    }

    private void LoadBachatGat()
    {
        try
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query;

                if (IsAdmin())
                {
                    query = @"
                        SELECT
                            BachatGatID,
                            GatName
                        FROM BachatGat
                        WHERE Status = 'Active'
                        ORDER BY GatName";
                }
                else
                {
                    query = @"
                        SELECT
                            BachatGatID,
                            GatName
                        FROM BachatGat
                        WHERE Status = 'Active'
                        AND BachatGatID = @BachatGatID
                        ORDER BY GatName";
                }

                SqlCommand cmd = new SqlCommand(query, con);

                if (!IsAdmin())
                {
                    cmd.Parameters.AddWithValue(
                        "@BachatGatID",
                        GetBachatGatID()
                    );
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                ddlBachatGat.DataSource = dt;

                ddlBachatGat.DataTextField = "GatName";

                ddlBachatGat.DataValueField = "BachatGatID";

                ddlBachatGat.DataBind();
            }

            if (IsAdmin())
            {
                ddlBachatGat.Items.Insert(
                    0,
                    new ListItem(
                        "-- Select Bachat Gat --",
                        ""
                    )
                );
            }
            else
            {
                if (ddlBachatGat.Items.Count > 0)
                {
                    ddlBachatGat.SelectedIndex = 0;
                }
            }
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
            int selectedBachatGatID =
                Convert.ToInt32(
                    ddlBachatGat.SelectedValue
                );

            if (!IsAdmin())
            {
                if (selectedBachatGatID != GetBachatGatID())
                {
                    ShowMessage(
                        "You can manage meetings only for your Bachat Gat.",
                        System.Drawing.Color.Red
                    );

                    return;
                }
            }

            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                con.Open();

                if (hfMeetingID.Value != "")
                {
                    int meetingID =
                        Convert.ToInt32(
                            hfMeetingID.Value
                        );

                    if (!IsAdmin())
                    {
                        string checkQuery = @"
                            SELECT COUNT(*)
                            FROM Meetings
                            WHERE MeetingID = @MeetingID
                            AND BachatGatID = @BachatGatID";

                        SqlCommand checkCmd =
                            new SqlCommand(
                                checkQuery,
                                con
                            );

                        checkCmd.Parameters.AddWithValue(
                            "@MeetingID",
                            meetingID
                        );

                        checkCmd.Parameters.AddWithValue(
                            "@BachatGatID",
                            GetBachatGatID()
                        );

                        int count =
                            Convert.ToInt32(
                                checkCmd.ExecuteScalar()
                            );

                        if (count == 0)
                        {
                            ShowMessage(
                                "You cannot update this meeting.",
                                System.Drawing.Color.Red
                            );

                            return;
                        }
                    }

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

                    if (!IsAdmin())
                    {
                        query +=
                            " AND BachatGatID = @LoggedInBachatGatID";
                    }

                    SqlCommand cmd =
                        new SqlCommand(
                            query,
                            con
                        );

                    AddParameters(cmd);

                    cmd.Parameters.AddWithValue(
                        "@MeetingID",
                        meetingID
                    );

                    if (!IsAdmin())
                    {
                        cmd.Parameters.AddWithValue(
                            "@LoggedInBachatGatID",
                            GetBachatGatID()
                        );
                    }

                    int rows =
                        cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        ShowMessage(
                            "Meeting updated successfully!",
                            System.Drawing.Color.Green
                        );
                    }
                    else
                    {
                        ShowMessage(
                            "You cannot update this meeting.",
                            System.Drawing.Color.Red
                        );

                        return;
                    }
                }
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
                        new SqlCommand(
                            query,
                            con
                        );

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

    private void AddParameters(SqlCommand cmd)
    {
        int bachatGatID =
            Convert.ToInt32(
                ddlBachatGat.SelectedValue
            );

        if (!IsAdmin())
        {
            bachatGatID = GetBachatGatID();
        }

        cmd.Parameters.AddWithValue(
            "@BachatGatID",
            bachatGatID
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
                    WHERE 1 = 1";

                if (!IsAdmin())
                {
                    query +=
                        " AND M.BachatGatID = @BachatGatID";
                }

                query +=
                    " ORDER BY M.MeetingID DESC";

                SqlCommand cmd =
                    new SqlCommand(
                        query,
                        con
                    );

                if (!IsAdmin())
                {
                    cmd.Parameters.AddWithValue(
                        "@BachatGatID",
                        GetBachatGatID()
                    );
                }

                SqlDataAdapter da =
                    new SqlDataAdapter(
                        cmd
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
                        (
                            B.GatName LIKE @Search
                            OR
                            M.MeetingPlace LIKE @Search
                        )";

                if (!IsAdmin())
                {
                    query +=
                        " AND M.BachatGatID = @BachatGatID";
                }

                query +=
                    " ORDER BY M.MeetingID DESC";

                SqlCommand cmd =
                    new SqlCommand(
                        query,
                        con
                    );

                cmd.Parameters.AddWithValue(
                    "@Search",
                    "%" +
                    txtSearch.Text.Trim() +
                    "%"
                );

                if (!IsAdmin())
                {
                    cmd.Parameters.AddWithValue(
                        "@BachatGatID",
                        GetBachatGatID()
                    );
                }

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

    protected void btnShowAll_Click(
        object sender,
        EventArgs e)
    {
        txtSearch.Text = "";

        LoadMeetings();
    }

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

                if (!IsAdmin())
                {
                    query +=
                        " AND BachatGatID = @BachatGatID";
                }

                SqlCommand cmd =
                    new SqlCommand(
                        query,
                        con
                    );

                cmd.Parameters.AddWithValue(
                    "@MeetingID",
                    meetingID
                );

                if (!IsAdmin())
                {
                    cmd.Parameters.AddWithValue(
                        "@BachatGatID",
                        GetBachatGatID()
                    );
                }

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
                        ).ToString(
                            "yyyy-MM-dd"
                        );

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
                else
                {
                    ShowMessage(
                        "You cannot access this meeting.",
                        System.Drawing.Color.Red
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

    private void DeleteMeeting(
        int meetingID)
    {
        try
        {
            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query = @"
                    DELETE FROM Meetings
                    WHERE MeetingID = @MeetingID";

                if (!IsAdmin())
                {
                    query +=
                        " AND BachatGatID = @BachatGatID";
                }

                SqlCommand cmd =
                    new SqlCommand(
                        query,
                        con
                    );

                cmd.Parameters.AddWithValue(
                    "@MeetingID",
                    meetingID
                );

                if (!IsAdmin())
                {
                    cmd.Parameters.AddWithValue(
                        "@BachatGatID",
                        GetBachatGatID()
                    );
                }

                con.Open();

                int rows =
                    cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    ShowMessage(
                        "Meeting deleted successfully!",
                        System.Drawing.Color.Green
                    );
                }
                else
                {
                    ShowMessage(
                        "You cannot delete this meeting.",
                        System.Drawing.Color.Red
                    );
                }
            }

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

    protected void btnClear_Click(
        object sender,
        EventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    {
        hfMeetingID.Value = "";

        if (ddlBachatGat.Items.Count > 0)
        {
            ddlBachatGat.SelectedIndex = 0;
        }

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
            .ToString(
                "yyyy-MM-dd"
            );

        btnSave.Text =
            "Save Meeting";
    }

    private void ShowMessage(
        string message,
        System.Drawing.Color color)
    {
        lblMessage.Text = message;

        lblMessage.ForeColor = color;
    }
}