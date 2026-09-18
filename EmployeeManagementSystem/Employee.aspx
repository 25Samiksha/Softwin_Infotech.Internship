<%@ Page Title="Employee Management"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeFile="Employee.aspx.cs"
    Inherits="Employee" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    Runat="Server">

    <style type="text/css">

        .employee-container
        {
            width: 95%;
            margin: 20px auto;
        }

        .page-title
        {
            font-size: 28px;
            font-weight: bold;
            margin-bottom: 20px;
        }

        .box
        {
            background-color: #ffffff;
            border: 1px solid #dddddd;
            padding: 20px;
            margin-bottom: 20px;
        }

        .search-box
        {
            width: 300px;
            padding: 10px;
            border: 1px solid #cccccc;
            font-size: 14px;
        }

        .button
        {
            padding: 9px 16px;
            border: none;
            cursor: pointer;
            color: white;
            margin-right: 5px;
            font-size: 14px;
        }

        .primary
        {
            background-color: #1f4e79;
        }

        .success
        {
            background-color: #27ae60;
        }

        .danger
        {
            background-color: #c0392b;
        }

        .gray
        {
            background-color: #777777;
        }

        .form-label
        {
            display: block;
            font-weight: bold;
            margin-top: 10px;
            margin-bottom: 5px;
        }

        .form-control
        {
            width: 95%;
            padding: 9px;
            border: 1px solid #cccccc;
            margin-bottom: 10px;
            font-size: 14px;
        }

        .textarea
        {
            height: 80px;
        }

        .grid
        {
            width: 100%;
            border-collapse: collapse;
        }

        .grid th
        {
            background-color: #1f4e79;
            color: white;
            padding: 10px;
            text-align: left;
        }

        .grid td
        {
            padding: 10px;
            border-bottom: 1px solid #dddddd;
        }

        .message
        {
            display: block;
            font-weight: bold;
            margin: 10px 0;
            font-size: 15px;
        }

        .profile-image
        {
            width: 130px;
            height: 130px;
            border: 1px solid #cccccc;
            padding: 3px;
        }

        .details-row
        {
            margin: 12px 0;
            font-size: 15px;
        }

        .details-label
        {
            font-weight: bold;
            display: inline-block;
            width: 160px;
        }

    </style>

</asp:Content>


<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    Runat="Server">

<div class="employee-container">

    <!-- MESSAGE -->

    <asp:Label
        ID="lblMessage"
        runat="server"
        CssClass="message">
    </asp:Label>


    <asp:Panel
        ID="pnlEmployeeList"
        runat="server">

        <div class="page-title">
            Employee Management
        </div>

        <div class="box">

            <asp:TextBox
                ID="txtSearch"
                runat="server"
                CssClass="search-box">
            </asp:TextBox>

            <asp:Button
                ID="btnSearch"
                runat="server"
                Text="Search"
                CssClass="button primary"
                OnClick="btnSearch_Click" />

            <asp:Button
                ID="btnAddEmployee"
                runat="server"
                Text="+ Add Employee"
                CssClass="button success"
                OnClick="btnAddEmployee_Click" />

        </div>


        <div class="box">

            <asp:GridView
                ID="gvEmployees"
                runat="server"
                AutoGenerateColumns="False"
                CssClass="grid"
                EmptyDataText="No employees found."
                OnRowCommand="gvEmployees_RowCommand">

                <Columns>

                    <asp:BoundField
                        DataField="EmployeeID"
                        HeaderText="ID" />

                    <asp:BoundField
                        DataField="EmployeeName"
                        HeaderText="Employee Name" />

                    <asp:BoundField
                        DataField="Email"
                        HeaderText="Email" />

                    <asp:BoundField
                        DataField="Mobile"
                        HeaderText="Mobile" />

                    <asp:BoundField
                        DataField="Gender"
                        HeaderText="Gender" />

                    <asp:BoundField
                        DataField="DepartmentName"
                        HeaderText="Department" />

                    <asp:BoundField
                        DataField="DesignationName"
                        HeaderText="Designation" />

                    <asp:BoundField
                        DataField="Salary"
                        HeaderText="Salary" />

                    <asp:BoundField
                        DataField="JoiningDate"
                        HeaderText="Joining Date"
                        DataFormatString="{0:dd-MM-yyyy}" />

                    <asp:BoundField
                        DataField="Status"
                        HeaderText="Status" />


                    <asp:TemplateField
                        HeaderText="Actions">

                        <ItemTemplate>

                            <asp:Button
                                ID="btnView"
                                runat="server"
                                Text="View"
                                CssClass="button primary"
                                CommandName="ViewEmployee"
                                CommandArgument='<%# Eval("EmployeeID") %>' />

                            <asp:Button
                                ID="btnEdit"
                                runat="server"
                                Text="Edit"
                                CssClass="button success"
                                CommandName="EditEmployee"
                                CommandArgument='<%# Eval("EmployeeID") %>' />

                            <asp:Button
                                ID="btnDelete"
                                runat="server"
                                Text="Delete"
                                CssClass="button danger"
                                CommandName="DeleteEmployee"
                                CommandArgument='<%# Eval("EmployeeID") %>'
                                OnClientClick="return confirm('Are you sure you want to delete this employee?');" />

                            <asp:Button
                                ID="btnProfile"
                                runat="server"
                                Text="Profile"
                                CssClass="button gray"
                                CommandName="ProfileEmployee"
                                CommandArgument='<%# Eval("EmployeeID") %>' />

                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>

            </asp:GridView>

        </div>

    </asp:Panel>

    <asp:Panel
        ID="pnlEmployeeForm"
        runat="server"
        Visible="false">

        <div class="box">

            <h2>
                <asp:Label
                    ID="lblFormTitle"
                    runat="server"
                    Text="Add Employee">
                </asp:Label>
            </h2>


            <asp:HiddenField
                ID="hfEmployeeID"
                runat="server" />

            <asp:Label
                ID="lblName"
                runat="server"
                Text="Employee Name"
                CssClass="form-label">
            </asp:Label>

            <asp:TextBox
                ID="txtEmployeeName"
                runat="server"
                CssClass="form-control">
            </asp:TextBox>


            <asp:Label
                ID="lblEmail"
                runat="server"
                Text="Email"
                CssClass="form-label">
            </asp:Label>

            <asp:TextBox
                ID="txtEmail"
                runat="server"
                CssClass="form-control">
            </asp:TextBox>

            <asp:Label
                ID="lblMobile"
                runat="server"
                Text="Mobile"
                CssClass="form-label">
            </asp:Label>

            <asp:TextBox
                ID="txtMobile"
                runat="server"
                MaxLength="15"
                CssClass="form-control">
            </asp:TextBox>

            <asp:Label
                ID="lblGender"
                runat="server"
                Text="Gender"
                CssClass="form-label">
            </asp:Label>

            <asp:DropDownList
                ID="ddlGender"
                runat="server"
                CssClass="form-control">

                <asp:ListItem
                    Text="-- Select Gender --"
                    Value="">
                </asp:ListItem>

                <asp:ListItem
                    Text="Male"
                    Value="Male">
                </asp:ListItem>

                <asp:ListItem
                    Text="Female"
                    Value="Female">
                </asp:ListItem>

                <asp:ListItem
                    Text="Other"
                    Value="Other">
                </asp:ListItem>

            </asp:DropDownList>


            <asp:Label
                ID="lblDOB"
                runat="server"
                Text="Date of Birth"
                CssClass="form-label">
            </asp:Label>

            <asp:TextBox
                ID="txtDateOfBirth"
                runat="server"
                CssClass="form-control">
            </asp:TextBox>

            <asp:Label
                ID="lblAddress"
                runat="server"
                Text="Address"
                CssClass="form-label">
            </asp:Label>

            <asp:TextBox
                ID="txtAddress"
                runat="server"
                TextMode="MultiLine"
                CssClass="form-control textarea">
            </asp:TextBox>

            <asp:Label
                ID="lblDepartment"
                runat="server"
                Text="Department"
                CssClass="form-label">
            </asp:Label>

            <asp:DropDownList
                ID="ddlDepartment"
                runat="server"
                CssClass="form-control">
            </asp:DropDownList>

            <asp:Label
                ID="lblDesignation"
                runat="server"
                Text="Designation"
                CssClass="form-label">
            </asp:Label>

            <asp:DropDownList
                ID="ddlDesignation"
                runat="server"
                CssClass="form-control">
            </asp:DropDownList>

            <asp:Label
                ID="lblSalary"
                runat="server"
                Text="Salary"
                CssClass="form-label">
            </asp:Label>

            <asp:TextBox
                ID="txtSalary"
                runat="server"
                CssClass="form-control">
            </asp:TextBox>

            <asp:Label
                ID="lblJoiningDate"
                runat="server"
                Text="Joining Date"
                CssClass="form-label">
            </asp:Label>

            <asp:TextBox
                ID="txtJoiningDate"
                runat="server"
                CssClass="form-control">
            </asp:TextBox>

            <asp:Label
                ID="lblStatus"
                runat="server"
                Text="Status"
                CssClass="form-label">
            </asp:Label>

            <asp:DropDownList
                ID="ddlStatus"
                runat="server"
                CssClass="form-control">

                <asp:ListItem
                    Text="Active"
                    Value="Active">
                </asp:ListItem>

                <asp:ListItem
                    Text="Inactive"
                    Value="Inactive">
                </asp:ListItem>

            </asp:DropDownList>

            <asp:Label
                ID="lblPhoto"
                runat="server"
                Text="Photo"
                CssClass="form-label">
            </asp:Label>

            <asp:FileUpload
                ID="fuPhoto"
                runat="server"
                CssClass="form-control">
            </asp:FileUpload>


            <br />

            <asp:Button
                ID="btnSaveEmployee"
                runat="server"
                Text="Save Employee"
                CssClass="button primary"
                OnClick="btnSaveEmployee_Click" />

            <asp:Button
                ID="btnCancelEmployee"
                runat="server"
                Text="Cancel"
                CssClass="button gray"
                OnClick="btnCancelEmployee_Click" />

        </div>

    </asp:Panel>


    <asp:Panel
        ID="pnlEmployeeDetails"
        runat="server"
        Visible="false">

        <div class="box">

            <h2>Employee Details</h2>


            <div class="details-row">
                <span class="details-label">Employee ID:</span>

                <asp:Label
                    ID="lblDetailsID"
                    runat="server">
                </asp:Label>
            </div>


            <div class="details-row">
                <span class="details-label">Employee Name:</span>

                <asp:Label
                    ID="lblDetailsName"
                    runat="server">
                </asp:Label>
            </div>


            <div class="details-row">
                <span class="details-label">Email:</span>

                <asp:Label
                    ID="lblDetailsEmail"
                    runat="server">
                </asp:Label>
            </div>


            <div class="details-row">
                <span class="details-label">Mobile:</span>

                <asp:Label
                    ID="lblDetailsMobile"
                    runat="server">
                </asp:Label>
            </div>


            <div class="details-row">
                <span class="details-label">Gender:</span>

                <asp:Label
                    ID="lblDetailsGender"
                    runat="server">
                </asp:Label>
            </div>


            <div class="details-row">
                <span class="details-label">Date of Birth:</span>

                <asp:Label
                    ID="lblDetailsDOB"
                    runat="server">
                </asp:Label>
            </div>


            <div class="details-row">
                <span class="details-label">Address:</span>

                <asp:Label
                    ID="lblDetailsAddress"
                    runat="server">
                </asp:Label>
            </div>


            <div class="details-row">
                <span class="details-label">Department:</span>

                <asp:Label
                    ID="lblDetailsDepartment"
                    runat="server">
                </asp:Label>
            </div>


            <div class="details-row">
                <span class="details-label">Designation:</span>

                <asp:Label
                    ID="lblDetailsDesignation"
                    runat="server">
                </asp:Label>
            </div>


            <div class="details-row">
                <span class="details-label">Salary:</span>

                <asp:Label
                    ID="lblDetailsSalary"
                    runat="server">
                </asp:Label>
            </div>


            <div class="details-row">
                <span class="details-label">Joining Date:</span>

                <asp:Label
                    ID="lblDetailsJoiningDate"
                    runat="server">
                </asp:Label>
            </div>


            <div class="details-row">
                <span class="details-label">Status:</span>

                <asp:Label
                    ID="lblDetailsStatus"
                    runat="server">
                </asp:Label>
            </div>


            <div class="details-row">

                <span class="details-label">
                    Photo:
                </span>

                <br /><br />

                <asp:Image
                    ID="imgDetailsPhoto"
                    runat="server"
                    CssClass="profile-image" />

            </div>


            <br />

            <asp:Button
                ID="btnBack"
                runat="server"
                Text="Back"
                CssClass="button gray"
                OnClick="btnBack_Click" />

        </div>

    </asp:Panel>


    <asp:Panel
        ID="pnlEmployeeProfile"
        runat="server"
        Visible="false">

        <div class="box">

            <h2>Employee Profile</h2>


            <asp:Image
                ID="imgProfilePhoto"
                runat="server"
                CssClass="profile-image" />

            <br /><br />


            <div class="details-row">
                <span class="details-label">Employee ID:</span>

                <asp:Label
                    ID="lblProfileID"
                    runat="server">
                </asp:Label>
            </div>


            <div class="details-row">
                <span class="details-label">Employee Name:</span>

                <asp:Label
                    ID="lblProfileName"
                    runat="server">
                </asp:Label>
            </div>


            <div class="details-row">
                <span class="details-label">Email:</span>

                <asp:Label
                    ID="lblProfileEmail"
                    runat="server">
                </asp:Label>
            </div>


            <div class="details-row">
                <span class="details-label">Mobile:</span>

                <asp:Label
                    ID="lblProfileMobile"
                    runat="server">
                </asp:Label>
            </div>


            <div class="details-row">
                <span class="details-label">Department:</span>

                <asp:Label
                    ID="lblProfileDepartment"
                    runat="server">
                </asp:Label>
            </div>


            <div class="details-row">
                <span class="details-label">Designation:</span>

                <asp:Label
                    ID="lblProfileDesignation"
                    runat="server">
                </asp:Label>
            </div>


            <div class="details-row">
                <span class="details-label">Salary:</span>

                <asp:Label
                    ID="lblProfileSalary"
                    runat="server">
                </asp:Label>
            </div>


            <div class="details-row">
                <span class="details-label">Joining Date:</span>

                <asp:Label
                    ID="lblProfileJoiningDate"
                    runat="server">
                </asp:Label>
            </div>


            <div class="details-row">
                <span class="details-label">Status:</span>

                <asp:Label
                    ID="lblProfileStatus"
                    runat="server">
                </asp:Label>
            </div>


            <br />

            <asp:Button
                ID="btnProfileBack"
                runat="server"
                Text="Back"
                CssClass="button gray"
                OnClick="btnProfileBack_Click" />

        </div>

    </asp:Panel>

</div>

</asp:Content>