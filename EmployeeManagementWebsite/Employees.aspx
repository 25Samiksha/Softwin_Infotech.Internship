
<%@ Page Title="Employees" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Employees.aspx.cs" Inherits="Employees" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="container">
    <div class="page-header">
      <h1>Employee Management</h1>
       </div>
       <ol class="breadcrumb">
        <li>
          <a href="Dashboard.aspx">Home</a>
        </li>
        <li class="active">Employees</li>
         </ol>
     <div class="alert alert-danger">
             <strong>Information!</strong>
            Manage employee information from this page.
</div>
         <div class="row">
         <div class="col-md-12">
       <button type="button"class="btn btn-primary"data-toggle="modal"data-target="#employeeModal">
         <span class="glyphicon glyphicon-plus"></span>Add Employee</button>
<br /><br /></div></div>
        <div class="row">
        <div class="col-md-12">
        <div class="panel panel-primary">
         <div class="panel-heading">
         <h3 class="panel-title">Add Employee</h3>
</div>
        <div class="panel-body">
        <div class="row">
        <div class="col-md-6">
        <div class="form-group">
        <label>Name</label>
            <br />
            <asp:TextBox ID="txtname" runat="server" CssClass="Form-control" placeholder="Enter employee Name" Height="34px" Width="100%"></asp:TextBox>

</div></div>
<div class="col-md-6">
<div class="form-group">
<label>Email</label>
    <br />
    <asp:TextBox ID="txtemail" runat="server" CssClass="Form-control" placeholder="Enter employee Email" Height="34px" Width="100%"></asp:TextBox>
</div>
</div></div>
      <div class="row">
      <div class="col-md-6">
      <div class="form-group">
<label>Mobile</label>
    <br />
    <asp:TextBox ID="txtMobile" runat="server" CssClass="Form-control" placeholder="Enter Mobile Number" Height="34px" Width="100%"></asp:TextBox>
 </div>
 </div>
      <div class="col-md-6">
      <div class="form-group">
           <label>Department</label>
 <asp:DropDownList ID="ddlDepartment"
  runat="server"
    CssClass="form-control">
     <asp:ListItem Value="">Select Department</asp:ListItem>
     <asp:ListItem>IT</asp:ListItem>
     <asp:ListItem>Finance</asp:ListItem>
     <asp:ListItem>Marketing</asp:ListItem>
     <asp:ListItem>Sales</asp:ListItem>
     <asp:ListItem></asp:ListItem>
</asp:DropDownList>
</div>
   </div>
     </div>
      <div class="row">
      <div class="col-md-6">
     <div class="form-group">
    <label>Joining Date</label>
<asp:TextBox ID="txtJoiningDate"
      runat="server"
            TextMode="Date"
                  CssClass="form-control"></asp:TextBox>
</div>
</div>
      <div class="col-md-6">
      <div class="form-group">
      <label>Gender</label>
 <br />
        <asp:RadioButtonList ID="rbl1" runat="server">
             <asp:ListItem>Male</asp:ListItem>
             <asp:ListItem>Female</asp:ListItem>
              <asp:ListItem>Other</asp:ListItem>
             </asp:RadioButtonList>
 </div>
 </div>
</div>

<div class="row">
<div class="col-md-12">
<div class="form-group">
<label>Skills</label>
    <br />
    <asp:CheckBoxList ID="cb1" runat="server" Width="86px">
        <asp:ListItem>HTML</asp:ListItem>
        <asp:ListItem>SQL</asp:ListItem>
        <asp:ListItem>C#</asp:ListItem>
        <asp:ListItem>.NET</asp:ListItem>
    </asp:CheckBoxList>
</div>
</div>
</div>
        <div class="row">
           <div class="col-md-12">
   <asp:Button ID="btnSubmit" runat="server" CssClass="btn-success" Height="35px" Text="Submit" />

</div></div></div></div>
</div></div>
      <div class="row">
      <div class="col-md-12">
     <div class="panel panel-default">
<div class="panel-heading">
<h3 class="panel-title">Employee List</h3></div>
<div class="panel-body">
       <div class="table-responsive">
<table class="table table-striped table-bordered table-hover">
<thead>
<tr>
      <th>ID</th>
      <th>Name</th>
      <th>Email</th>
      <th>Department</th>
      <th>Joining Date</th>
      <th>Status</th>
      <th>Edit</th>
      <th>Delete</th>
</tr>
</thead>
<tbody>
<tr>
      <td>1</td>
      <td>Rahul Patil</td>
      <td>rahul@gmail.com</td>
      <td>IT</td>
      <td>01-09-2026</td>
<td>
       <span class="label label-success">Active</span>
</td>
<td>
      <button type="button"class="btn btn-primary btn-sm">
      <span class="glyphicon glyphicon-edit"></span>Edit</button>
</td>
<td>
    <button type="button"class="btn btn-danger btn-sm">
          <span class="glyphicon glyphicon-trash"></span>Delete</button></td></tr>
<tr>

    <td>2</td>
       <td>Priya Sharma</td>
       <td>priya@gmail.com</td>
       <td>HR</td>
       <td> 03-09-2026</td>
<td>
       <span class="label label-success">Active</span>
</td>
<td>
      <button type="button"class="btn btn-primary btn-sm">
          <span class="glyphicon glyphicon-edit"></span>Edit</button>
</td>
<td>
       <button type="button" class="btn btn-danger btn-sm">
       <span class="glyphicon glyphicon-trash"></span>Delete</button>
</td></tr>
<tr>
        <td>3</td>
        <td>Amit Joshi</td>
        <td>amit@gmail.com</td>
        <td>Finance</td>
        <td> 05-09-2026</td>
  <td>
       <span class="label label-warning">Inactive</span>
</td>
<td>
       <button type="button" class="btn btn-primary btn-sm">
      <span class="glyphicon glyphicon-edit"></span>Edit
</button>
</td> <td>
        <button type="button"class="btn btn-danger btn-sm">
        <span class="glyphicon glyphicon-trash"></span>Delete
</button>
</td>
     </tr>
</tbody>
 </table>
</div>
   </div>
      </div>
       </div>
     </div>
</div>
        <div id="employeeModal"class="modal fade"role="dialog">
        <div class="modal-dialog">
        <div class="modal-content">
        <div class="modal-header">

                    <button type="button"
                        class="close"
                        data-dismiss="modal">
                        &times;
                     </button>

                    <h4 class="modal-title">
                        Employee Information
                    </h4>

                </div>
                <div class="modal-body">

                    <p>
                        Employee form is available above.
                    </p>
                    <p>
                        This modal is included to practice
                        the Bootstrap modal component.
                    </p>

                </div>

                <div class="modal-footer">

                    <button type="button"
                        class="btn btn-default"
                        data-dismiss="modal">Close

                    </button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

