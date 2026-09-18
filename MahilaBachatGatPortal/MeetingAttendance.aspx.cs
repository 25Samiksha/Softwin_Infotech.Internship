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


    // ==========================================
    // LOAD BACHAT GAT
    // ==========================================

    private void LoadBachatGat()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT BachatGatID, GatName
                FROM BachatGat
                WHERE Status = 'Active'
                ORDER BY GatName";

            SqlDataAdapter da =
                new SqlDataAdapter(query, con);

            DataTable dt = new DataTable();

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

        ddlMeeting.Items.Clear();

        ddlMeeting.Items.Insert(
            0,
            new ListItem(
                "-- Select Meeting --",
                ""
            )
        );
    }


    // ==========================================
    // BACHAT GAT CHANGE
    // ==========================================

    protected void ddlBachatGat_SelectedIndexChanged(
        object sender,
        EventArgs e)
    {
        LoadMeetings();

        gvAttendance.DataSource = null;
        gvAttendance.DataBind();

        lblMeetingInfo.Text = "";
    }


    // ==========================================
    // LOAD MEETINGS
    // ==========================================

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


        using (SqlConnection con = DBHelper.GetConnection())
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
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@BachatGatID",
                ddlBachatGat.SelectedValue
            );

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

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


    // ==========================================
    // MEETING CHANGE
    // ==========================================

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


        LoadMeetingInfo();

        LoadMembers();
    }


    // ==========================================
    // MEETING INFORMATION
    // ==========================================

    private void LoadMeetingInfo()
    {
        using (SqlConnection con = DBHelper.GetConnection())
        {
            string query = @"
                SELECT
                    MeetingDate,
                    MeetingTime,
                    MeetingPlace,
                    MeetingType
                FROM Meetings
                WHERE MeetingID = @MeetingID";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@MeetingID",
                ddlMeeting.SelectedValue
            );

            con.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();

            if (dr.Read())
            {
                string date =
                    Convert.ToDateTime(
                        dr["MeetingDate"]
                    ).ToString("dd-MM-yyyy");

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


    // ==========================================
    // LOAD MEMBERS
    // ==========================================

    private void LoadMembers()
    {
        using (SqlConnection con = DBHelper.GetConnection())
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
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@BachatGatID",
                ddlBachatGat.SelectedValue
            );

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);


            gvAttendance.DataSource = dt;

            gvAttendance.DataBind();
        }
    }


    // ==========================================
    // SAVE ATTENDANCE
    // ==========================================

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
            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();


                foreach (GridViewRow row
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


                    // Check whether attendance
                    // already exists

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
                        ddlMeeting.SelectedValue
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
                        // UPDATE

                        string updateQuery = @"
                            UPDATE MeetingAttendance
                            SET
                                AttendanceStatus =
                                    @AttendanceStatus,
                                Remarks = @Remarks
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
                            ddlMeeting.SelectedValue
                        );


                        updateCmd.Parameters.AddWithValue(
                            "@MemberID",
                            memberID
                        );


                        updateCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // INSERT

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
                            ddlMeeting.SelectedValue
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


    // ==========================================
    // CLEAR
    // ==========================================

    protected void btnClear_Click(
        object sender,
        EventArgs e)
    {
        ddlBachatGat.SelectedIndex = 0;

        ddlMeeting.Items.Clear();

        ddlMeeting.Items.Insert(
            0,
            new ListItem(
                "-- Select Meeting --",
                ""
            )
        );


        gvAttendance.DataSource = null;

        gvAttendance.DataBind();


        lblMeetingInfo.Text = "";

        lblMessage.Text = "";
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