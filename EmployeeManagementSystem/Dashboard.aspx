<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeFile="Dashboard.aspx.cs"
    Inherits="Dashboard" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    Runat="Server">
</asp:Content>


<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    Runat="Server">

    <h1>Dashboard</h1>

    <p>Welcome to Employee Management System</p>

    <div class="dashboard-container">

        <div class="card">
            <h2>0</h2>
            <p>Total Employees</p>
        </div>

        <div class="card">
            <h2>0</h2>
            <p>Departments</p>
        </div>

        <div class="card">
            <h2>0</h2>
            <p>Designations</p>
        </div>

        <div class="card">
            <h2>0</h2>
            <p>Pending Leaves</p>
        </div>

    </div>

</asp:Content>