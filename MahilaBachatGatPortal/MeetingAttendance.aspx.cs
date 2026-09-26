using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class MeetingAttendance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RoleHelper.RequirePresidentSecretary(this);

        if (!IsPostBack)
        {
            LoadBachatGat();
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

                ddlBachatGat.DataSource = dt;

                ddlBachatGat.DataTextField =
                    "GatName";

                ddlBachatGat.DataValueField =
                    "BachatGatID";

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

                    LoadMeetings();
                }
            }

            ddlMeeting.Items.Clear();

            ddlMeeting.Items.Insert(
                0,
                new ListItem(
                    "-- Select Meeting --",
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

    protected void ddlBachatGat_SelectedIndexChanged(
        object sender,
        EventArgs e)
    {
        if (!IsAdmin())
        {
            if (ddlBachatGat.SelectedValue !=
                GetBachatGatID().ToString())
            {
                ShowMessage(
                    "You can access only your own Bachat Gat.",
                    System.Drawing.Color.Red
                );

                ddlBachatGat.SelectedValue =
                    GetBachatGatID().ToString();

                LoadMeetings();

                return;
            }
        }

        LoadMeetings();

        gvAttendance.DataSource = null;

        gvAttendance.DataBind();

        lblMeetingInfo.Text = "";
    }

    private void LoadMeetings()
    {
        ddlMeeting.Items.Clear();

        if (ddlBachatGat.SelectedValue == "")
        {
            ddlMeeting.Items.Insert(
                0,
                new ListItem(
                    "-- Select Meeting --",
                    ""
                )
            );

            return;
        }

        int selectedBachatGatID =
            Convert.ToInt32(
                ddlBachatGat.SelectedValue
            );

        if (!IsAdmin())
        {
            if (selectedBachatGatID !=
                GetBachatGatID())
            {
                ddlMeeting.Items.Insert(
                    0,
                    new ListItem(
                        "-- Select Meeting --",
                        ""
                    )
                );

                return;
            }
        }

        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    MeetingID,
                    MeetingDate,
                    MeetingPlace,
                    MeetingType
                FROM Meetings
                WHERE BachatGatID = @BachatGatID
                ORDER BY MeetingDate DESC";

            SqlCommand cmd =
                new SqlCommand(
                    query,
                    con
                );

            cmd.Parameters.AddWithValue(
                "@BachatGatID",
                selectedBachatGatID
            );

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            ddlMeeting.DataSource = dt;

            ddlMeeting.DataTextField =
                "MeetingDate";

            ddlMeeting.DataValueField =
                "MeetingID";

            ddlMeeting.DataBind();
        }

        ddlMeeting.Items.Insert(
            0,
            new ListItem(
                "-- Select Meeting --",
                ""
            )
        );
    }

    protected void ddlMeeting_SelectedIndexChanged(
        object sender,
        EventArgs e)
    {
        if (ddlMeeting.SelectedValue == "")
        {
            gvAttendance.DataSource = null;

            gvAttendance.DataBind();

            lblMeetingInfo.Text = "";

            return;
        }

        if (!ValidateSelectedMeeting())
        {
            ddlMeeting.SelectedIndex = 0;

            gvAttendance.DataSource = null;

            gvAttendance.DataBind();

            lblMeetingInfo.Text = "";

            return;
        }

        LoadMeetingInfo();

        LoadMembers();
    }

    private bool ValidateSelectedMeeting()
    {
        try
        {
            int meetingID =
                Convert.ToInt32(
                    ddlMeeting.SelectedValue
                );

            int bachatGatID =
                Convert.ToInt32(
                    ddlBachatGat.SelectedValue
                );

            if (!IsAdmin())
            {
                if (bachatGatID !=
                    GetBachatGatID())
                {
                    ShowMessage(
                        "You can access only your own Bachat Gat.",
                        System.Drawing.Color.Red
                    );

                    return false;
                }
            }

            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                string query = @"
                    SELECT COUNT(*)
                    FROM Meetings
                    WHERE MeetingID = @MeetingID
                    AND BachatGatID = @BachatGatID";

                SqlCommand cmd =
                    new SqlCommand(
                        query,
                        con
                    );

                cmd.Parameters.AddWithValue(
                    "@MeetingID",
                    meetingID
                );

                cmd.Parameters.AddWithValue(
                    "@BachatGatID",
                    bachatGatID
                );

                con.Open();

                int count =
                    Convert.ToInt32(
                        cmd.ExecuteScalar()
                    );

                if (count == 0)
                {
                    ShowMessage(
                        "This meeting does not belong to the selected Bachat Gat.",
                        System.Drawing.Color.Red
                    );

                    return false;
                }
            }

            return true;
        }
        catch
        {
            ShowMessage(
                "Invalid meeting selected.",
                System.Drawing.Color.Red
            );

            return false;
        }
    }

    private void LoadMeetingInfo()
    {
        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    MeetingDate,
                    MeetingTime,
                    MeetingPlace,
                    MeetingType
                FROM Meetings
                WHERE MeetingID = @MeetingID
                AND BachatGatID = @BachatGatID";

            SqlCommand cmd =
                new SqlCommand(
                    query,
                    con
                );

            cmd.Parameters.AddWithValue(
                "@MeetingID",
                ddlMeeting.SelectedValue
            );

            cmd.Parameters.AddWithValue(
                "@BachatGatID",
                ddlBachatGat.SelectedValue
            );

            con.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();

            if (dr.Read())
            {
                string date =
                    Convert.ToDateTime(
                        dr["MeetingDate"]
                    ).ToString(
                        "dd-MM-yyyy"
                    );

                string time =
                    dr["MeetingTime"].ToString();

                string place =
                    dr["MeetingPlace"].ToString();

                string type =
                    dr["MeetingType"].ToString();

                lblMeetingInfo.Text =
                    "Date: " + date +
                    " | Time: " + time +
                    " | Place: " + place +
                    " | Type: " + type;
            }

            dr.Close();
        }
    }

    private void LoadMembers()
    {
        if (!ValidateSelectedMeeting())
        {
            return;
        }

        using (SqlConnection con =
            DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    MemberID,
                    MemberCode,
                    MemberName,
                    Mobile
                FROM Members
                WHERE BachatGatID = @BachatGatID
                AND Status = 'Active'
                ORDER BY MemberName";

            SqlCommand cmd =
                new SqlCommand(
                    query,
                    con
                );

            cmd.Parameters.AddWithValue(
                "@BachatGatID",
                ddlBachatGat.SelectedValue
            );

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            gvAttendance.DataSource = dt;

            gvAttendance.DataBind();
        }
    }

    protected void btnSaveAttendance_Click(
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

        if (ddlMeeting.SelectedValue == "")
        {
            ShowMessage(
                "Please select Meeting.",
                System.Drawing.Color.Red
            );

            return;
        }

        if (!IsAdmin())
        {
            if (ddlBachatGat.SelectedValue !=
                GetBachatGatID().ToString())
            {
                ShowMessage(
                    "You can save attendance only for your own Bachat Gat.",
                    System.Drawing.Color.Red
                );

                return;
            }
        }

        if (!ValidateSelectedMeeting())
        {
            return;
        }

        if (gvAttendance.Rows.Count == 0)
        {
            ShowMessage(
                "No members found.",
                System.Drawing.Color.Red
            );

            return;
        }

        try
        {
            int meetingID =
                Convert.ToInt32(
                    ddlMeeting.SelectedValue
                );

            int bachatGatID =
                Convert.ToInt32(
                    ddlBachatGat.SelectedValue
                );

            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                con.Open();

                foreach (
                    GridViewRow row
                    in gvAttendance.Rows)
                {
                    int memberID =
                        Convert.ToInt32(
                            gvAttendance.DataKeys[
                                row.RowIndex
                            ].Value
                        );

                    DropDownList ddlAttendance =
                        (DropDownList)
                        row.FindControl(
                            "ddlAttendance"
                        );

                    TextBox txtRemarks =
                        (TextBox)
                        row.FindControl(
                            "txtRemarks"
                        );

                    string memberCheckQuery = @"
                        SELECT COUNT(*)
                        FROM Members
                        WHERE MemberID = @MemberID
                        AND BachatGatID = @BachatGatID
                        AND Status = 'Active'";

                    SqlCommand memberCheckCmd =
                        new SqlCommand(
                            memberCheckQuery,
                            con
                        );

                    memberCheckCmd.Parameters.AddWithValue(
                        "@MemberID",
                        memberID
                    );

                    memberCheckCmd.Parameters.AddWithValue(
                        "@BachatGatID",
                        bachatGatID
                    );

                    int memberCount =
                        Convert.ToInt32(
                            memberCheckCmd.ExecuteScalar()
                        );

                    if (memberCount == 0)
                    {
                        continue;
                    }

                    string checkQuery = @"
                        SELECT COUNT(*)
                        FROM MeetingAttendance
                        WHERE MeetingID = @MeetingID
                        AND MemberID = @MemberID";

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
                        "@MemberID",
                        memberID
                    );

                    int count =
                        Convert.ToInt32(
                            checkCmd.ExecuteScalar()
                        );

                    if (count > 0)
                    {
                        string updateQuery = @"
                            UPDATE MeetingAttendance
                            SET
                                AttendanceStatus =
                                    @AttendanceStatus,
                                Remarks =
                                    @Remarks
                            WHERE MeetingID =
                                    @MeetingID
                            AND MemberID =
                                    @MemberID";

                        SqlCommand updateCmd =
                            new SqlCommand(
                                updateQuery,
                                con
                            );

                        updateCmd.Parameters.AddWithValue(
                            "@AttendanceStatus",
                            ddlAttendance.SelectedValue
                        );

                        updateCmd.Parameters.AddWithValue(
                            "@Remarks",
                            txtRemarks.Text.Trim()
                        );

                        updateCmd.Parameters.AddWithValue(
                            "@MeetingID",
                            meetingID
                        );

                        updateCmd.Parameters.AddWithValue(
                            "@MemberID",
                            memberID
                        );

                        updateCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        string insertQuery = @"
                            INSERT INTO MeetingAttendance
                            (
                                MeetingID,
                                MemberID,
                                AttendanceStatus,
                                Remarks
                            )
                            VALUES
                            (
                                @MeetingID,
                                @MemberID,
                                @AttendanceStatus,
                                @Remarks
                            )";

                        SqlCommand insertCmd =
                            new SqlCommand(
                                insertQuery,
                                con
                            );

                        insertCmd.Parameters.AddWithValue(
                            "@MeetingID",
                            meetingID
                        );

                        insertCmd.Parameters.AddWithValue(
                            "@MemberID",
                            memberID
                        );

                        insertCmd.Parameters.AddWithValue(
                            "@AttendanceStatus",
                            ddlAttendance.SelectedValue
                        );

                        insertCmd.Parameters.AddWithValue(
                            "@Remarks",
                            txtRemarks.Text.Trim()
                        );

                        insertCmd.ExecuteNonQuery();
                    }
                }
            }

            ShowMessage(
                "Attendance saved successfully!",
                System.Drawing.Color.Green
            );
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error saving attendance: " +
                ex.Message,
                System.Drawing.Color.Red
            );
        }
    }

    protected void btnClear_Click(
        object sender,
        EventArgs e)
    {
        if (IsAdmin())
        {
            ddlBachatGat.SelectedIndex = 0;
        }
        else
        {
            if (ddlBachatGat.Items.Count > 0)
            {
                ddlBachatGat.SelectedValue =
                    GetBachatGatID().ToString();
            }
        }

        LoadMeetings();

        gvAttendance.DataSource = null;

        gvAttendance.DataBind();

        lblMeetingInfo.Text = "";

        lblMessage.Text = "";
    }

    private void ShowMessage(
        string message,
        System.Drawing.Color color)
    {
        lblMessage.Text = message;

        lblMessage.ForeColor = color;
    }
}