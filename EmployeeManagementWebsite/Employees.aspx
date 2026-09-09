<%@ Page Title="Employees" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Employees.aspx.cs" Inherits="Employees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="container">

<div class="page-header">
<h2>Employee Management</h2>
<ol class="breadcrumb">
<li><a href="Dashboard.aspx">Home</a></li>
<li class="active">Employees</li>
</ol>
</div>

<div class="alert alert-info">
<strong>Information!</strong> Manage employee information from this page.
</div>

<asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>

<div class="panel panel-info">
<div class="panel-heading">
<h3 class="panel-title">Employee Search</h3>
</div>

<div class="panel-body">
<div class="row">

<div class="col-md-4">
<div class="form-group">
<label>Employee Name</label>
<asp:TextBox ID="txtSearchName" runat="server" CssClass="form-control" placeholder="Enter employee name"></asp:TextBox>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label>Department</label>
<asp:DropDownList ID="ddlSearchDepartment" runat="server" CssClass="form-control">
<asp:ListItem Text="All Departments" Value=""></asp:ListItem>
<asp:ListItem Text="IT" Value="IT"></asp:ListItem>
<asp:ListItem Text="HR" Value="HR"></asp:ListItem>
<asp:ListItem Text="Finance" Value="Finance"></asp:ListItem>
<asp:ListItem Text="Marketing" Value="Marketing"></asp:ListItem>
<asp:ListItem Text="Sales" Value="Sales"></asp:ListItem>
</asp:DropDownList>
</div>
</div>

<div class="col-md-4">
<div class="form-group">
<label>Status</label>
<asp:DropDownList ID="ddlSearchStatus" runat="server" CssClass="form-control">
<asp:ListItem Text="All Status" Value=""></asp:ListItem>
<asp:ListItem Text="Active" Value="Active"></asp:ListItem>
<asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
</asp:DropDownList>
</div>
</div>

</div>

<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnSearch_Click" />
&nbsp;
<asp:Button ID="btnResetSearch" runat="server" Text="Reset" CssClass="btn btn-default" CausesValidation="false" OnClick="btnResetSearch_Click" />

</div>
</div>

<div class="clearfix">
<div class="pull-right">
<asp:Button ID="btnAddEmployee" runat="server" Text="+ Add Employee" CssClass="btn btn-success" CausesValidation="false" OnClick="btnAddEmployee_Click" />
</div>
</div>

<br />

<div class="panel panel-default">
<div class="panel-heading">
<h3 class="panel-title">Employee List</h3>
</div>

<div class="panel-body">
<div class="table-responsive">

<asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover" EmptyDataText="No employees found." AllowPaging="true" PageSize="5" OnRowCommand="gvEmployees_RowCommand" OnPageIndexChanging="gvEmployees_PageIndexChanging">

<Columns>

<asp:BoundField DataField="EmployeeId" HeaderText="ID" />
<asp:BoundField DataField="EmployeeName" HeaderText="Name" />
<asp:BoundField DataField="Email" HeaderText="Email" />
<asp:BoundField DataField="Department" HeaderText="Department" />
<asp:BoundField DataField="Designation" HeaderText="Designation" />
<asp:BoundField DataField="JoiningDate" HeaderText="Joining Date" DataFormatString="{0:dd-MM-yyyy}" />

<asp:TemplateField HeaderText="Status">
<ItemTemplate>
<asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' CssClass='<%# GetStatusClass(Eval("Status")) %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Actions">
<ItemTemplate>

<asp:LinkButton ID="btnView" runat="server" Text="View" CommandName="ViewEmployee" CommandArgument='<%# Eval("EmployeeId") %>' CssClass="btn btn-info btn-xs" CausesValidation="false">
<span class="glyphicon glyphicon-eye-open"></span> View
</asp:LinkButton>

&nbsp;

<asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CommandName="EditEmployee" CommandArgument='<%# Eval("EmployeeId") %>' CssClass="btn btn-warning btn-xs" CausesValidation="false">
<span class="glyphicon glyphicon-edit"></span> Edit
</asp:LinkButton>

&nbsp;

<asp:LinkButton ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteEmployee" CommandArgument='<%# Eval("EmployeeId") %>' CssClass="btn btn-danger btn-xs" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this employee?');">
<span class="glyphicon glyphicon-trash"></span> Delete
</asp:LinkButton>

</ItemTemplate>
</asp:TemplateField>

</Columns>

<PagerStyle CssClass="pagination" />

</asp:GridView>

</div>
</div>
</div>

</div>

<div class="modal fade" id="employeeModal" tabindex="-1" role="dialog" aria-labelledby="employeeModalTitle">

<div class="modal-dialog" role="document">
<div class="modal-content">

<div class="modal-header">
<button type="button" class="close" data-dismiss="modal">
<span>&times;</span>
</button>
<h4 class="modal-title" id="employeeModalTitle">Add Employee</h4>
</div>

<div class="modal-body">

<asp:HiddenField ID="hfEmployeeId" runat="server" />

<div class="form-group">
<label>Employee Name</label>
<asp:TextBox ID="txtEmployeeName" runat="server" CssClass="form-control" placeholder="Enter employee name"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtEmployeeName" ErrorMessage="Employee name is required." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
</div>

<div class="form-group">
<label>Email</label>
<asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter email"></asp:TextBox>
<asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Enter a valid email address." CssClass="text-danger" Display="Dynamic" ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"></asp:RegularExpressionValidator>
</div>

<div class="form-group">
<label>Department</label>
<asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control">
<asp:ListItem Text="Select Department" Value=""></asp:ListItem>
<asp:ListItem Text="IT" Value="IT"></asp:ListItem>
<asp:ListItem Text="HR" Value="HR"></asp:ListItem>
<asp:ListItem Text="Finance" Value="Finance"></asp:ListItem>
<asp:ListItem Text="Marketing" Value="Marketing"></asp:ListItem>
<asp:ListItem Text="Sales" Value="Sales"></asp:ListItem>
</asp:DropDownList>
<asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlDepartment" InitialValue="" ErrorMessage="Please select a department." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
</div>

<div class="form-group">
<label>Designation</label>
<asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" placeholder="Enter designation"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ControlToValidate="txtDesignation" ErrorMessage="Designation is required." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
</div>

<div class="form-group">
<label>Joining Date</label>
<asp:TextBox ID="txtJoiningDate" runat="server" CssClass="form-control" placeholder="dd-MM-yyyy"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvJoiningDate" runat="server" ControlToValidate="txtJoiningDate" ErrorMessage="Joining date is required." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
</div>

<div class="form-group">
<label>Status</label>
<asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
<asp:ListItem Text="Active" Value="Active"></asp:ListItem>
<asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
</asp:DropDownList>
</div>

</div>

<div class="modal-footer">

<asp:Button ID="btnSave" runat="server" Text="Save Employee" CssClass="btn btn-primary" OnClick="btnSave_Click" />

<asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-default" CausesValidation="false" OnClick="btnClear_Click" />

<button type="button" class="btn btn-danger" data-dismiss="modal">Cancel</button>

</div>

</div>
</div>

</div>

</asp:Content>