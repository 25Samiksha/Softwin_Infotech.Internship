<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeFile="Meetings.aspx.cs"
    Inherits="Meetings" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">

    <style>

        .page-title {
            margin-top: 0;
            font-weight: bold;
        }

        .form-panel,
        .grid-panel {
            background: #ffffff;
            padding: 20px;
            border-radius: 8px;
            margin-bottom: 25px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.08);
        }

        .section-title {
            margin-top: 0;
            margin-bottom: 20px;
            font-weight: bold;
        }

        .required {
            color: red;
        }

        .action-buttons {
            margin-top: 20px;
        }

        .meeting-info {
            background: #f5f7fa;
            padding: 12px;
            border-radius: 6px;
            margin-bottom: 15px;
        }

    </style>

</asp:Content>


<asp:Content ID="Content2"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2 class="page-title">
        Meeting Management
    </h2>

    <p class="text-muted">
        Schedule and manage Bachat Gat monthly meetings.
    </p>


    <!-- MEETING FORM -->

    <div class="form-panel">

        <h4 class="section-title">
            Meeting Registration
        </h4>

        <asp:HiddenField ID="hfMeetingID"
            runat="server" />


        <!-- ROW 1 -->

        <div class="row">

            <div class="col-md-4">

                <div class="form-group">

                    <label>
                        Bachat Gat
                        <span class="required">*</span>
                    </label>

                    <asp:DropDownList
                        ID="ddlBachatGat"
                        runat="server"
                        CssClass="form-control">

                    </asp:DropDownList>

                </div>

            </div>


            <div class="col-md-4">

                <div class="form-group">

                    <label>
                        Meeting Date
                        <span class="required">*</span>
                    </label>

                    <asp:TextBox
                        ID="txtMeetingDate"
                        runat="server"
                        TextMode="SingleLine"
                        CssClass="form-control">

                    </asp:TextBox>

                </div>

            </div>


            <div class="col-md-4">

                <div class="form-group">

                    <label>
                        Meeting Time
                    </label>

                    <asp:TextBox
                        ID="txtMeetingTime"
                        runat="server"
                        CssClass="form-control"
                        placeholder="Example: 10:30 AM">

                    </asp:TextBox>

                </div>

            </div>

        </div>


        <!-- ROW 2 -->

        <div class="row">

            <div class="col-md-4">

                <div class="form-group">

                    <label>
                        Meeting Place
                    </label>

                    <asp:TextBox
                        ID="txtMeetingPlace"
                        runat="server"
                        CssClass="form-control">

                    </asp:TextBox>

                </div>

            </div>


            <div class="col-md-4">

                <div class="form-group">

                    <label>
                        Meeting Type
                    </label>

                    <asp:DropDownList
                        ID="ddlMeetingType"
                        runat="server"
                        CssClass="form-control">

                        <asp:ListItem
                            Text="Monthly"
                            Value="Monthly">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Special"
                            Value="Special">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Emergency"
                            Value="Emergency">
                        </asp:ListItem>

                    </asp:DropDownList>

                </div>

            </div>


            <div class="col-md-4">

                <div class="form-group">

                    <label>
                        Next Meeting Date
                    </label>

                    <asp:TextBox
                        ID="txtNextMeetingDate"
                        runat="server"
                        TextMode="SingleLine"
                        CssClass="form-control">

                    </asp:TextBox>

                </div>

            </div>

        </div>


        <!-- AGENDA -->

        <div class="form-group">

            <label>
                Agenda
            </label>

            <asp:TextBox
                ID="txtAgenda"
                runat="server"
                TextMode="MultiLine"
                Rows="3"
                CssClass="form-control">

            </asp:TextBox>

        </div>


        <!-- MINUTES -->

        <div class="form-group">

            <label>
                Meeting Minutes
            </label>

            <asp:TextBox
                ID="txtMinutes"
                runat="server"
                TextMode="MultiLine"
                Rows="4"
                CssClass="form-control">

            </asp:TextBox>

        </div>


        <!-- DECISIONS -->

        <div class="form-group">

            <label>
                Decisions
            </label>

            <asp:TextBox
                ID="txtDecisions"
                runat="server"
                TextMode="MultiLine"
                Rows="3"
                CssClass="form-control">

            </asp:TextBox>

        </div>


        <!-- BUTTONS -->

        <div class="action-buttons">

            <asp:Button
                ID="btnSave"
                runat="server"
                Text="Save Meeting"
                CssClass="btn btn-primary"
                OnClick="btnSave_Click" />

            &nbsp;

            <asp:Button
                ID="btnClear"
                runat="server"
                Text="Clear"
                CssClass="btn btn-default"
                OnClick="btnClear_Click" />

        </div>


        <br />

        <asp:Label
            ID="lblMessage"
            runat="server"
            Font-Bold="true">

        </asp:Label>

    </div>


    <!-- MEETING LIST -->

    <div class="grid-panel">

        <h4 class="section-title">
            Meeting List
        </h4>


        <div class="row">

            <div class="col-md-5">

                <asp:TextBox
                    ID="txtSearch"
                    runat="server"
                    CssClass="form-control"
                    placeholder="Search by Bachat Gat or Meeting Place">

                </asp:TextBox>

            </div>


            <div class="col-md-2">

                <asp:Button
                    ID="btnSearch"
                    runat="server"
                    Text="Search"
                    CssClass="btn btn-info"
                    OnClick="btnSearch_Click" />

            </div>


            <div class="col-md-2">

                <asp:Button
                    ID="btnShowAll"
                    runat="server"
                    Text="Show All"
                    CssClass="btn btn-default"
                    OnClick="btnShowAll_Click" />

            </div>

        </div>


        <br />


        <div class="table-responsive">

            <asp:GridView
                ID="gvMeetings"
                runat="server"
                AutoGenerateColumns="False"
                CssClass="table table-bordered table-hover"
                DataKeyNames="MeetingID"
                OnRowCommand="gvMeetings_RowCommand">

                <Columns>

                    <asp:BoundField
                        DataField="MeetingID"
                        HeaderText="ID" />

                    <asp:BoundField
                        DataField="GatName"
                        HeaderText="Bachat Gat" />

                    <asp:BoundField
                        DataField="MeetingDate"
                        HeaderText="Meeting Date"
                        DataFormatString="{0:dd-MM-yyyy}" />

                    <asp:BoundField
                        DataField="MeetingTime"
                        HeaderText="Time" />

                    <asp:BoundField
                        DataField="MeetingPlace"
                        HeaderText="Place" />

                    <asp:BoundField
                        DataField="MeetingType"
                        HeaderText="Type" />

                    <asp:BoundField
                        DataField="NextMeetingDate"
                        HeaderText="Next Meeting"
                        DataFormatString="{0:dd-MM-yyyy}" />


                    <asp:TemplateField
                        HeaderText="Action">

                        <ItemTemplate>

                            <asp:LinkButton
                                ID="btnEdit"
                                runat="server"
                                Text="Edit"
                                CssClass="btn btn-warning btn-xs"
                                CommandName="EditMeeting"
                                CommandArgument='<%# Eval("MeetingID") %>'>
                            </asp:LinkButton>

                            &nbsp;

                            <asp:LinkButton
                                ID="btnDelete"
                                runat="server"
                                Text="Delete"
                                CssClass="btn btn-danger btn-xs"
                                CommandName="DeleteMeeting"
                                CommandArgument='<%# Eval("MeetingID") %>'
                                OnClientClick="return confirm('Are you sure you want to delete this meeting?');">
                            </asp:LinkButton>

                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>


                <EmptyDataTemplate>
                    No meetings found.
                </EmptyDataTemplate>

            </asp:GridView>

        </div>

    </div>

</asp:Content>