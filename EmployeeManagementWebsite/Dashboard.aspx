<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="container">

        <h2>Dashboard</h2>

        <ol class="breadcrumb">
            <li class="active">Home</li>
        </ol>

        <div class="row">

            <div class="col-md-3 col-sm-6">

                <div class="panel panel-primary">

                    <div class="panel-heading">
                        Total Employees
                    </div>

                    <div class="panel-body">
                        <h2>120</h2>
                    </div>

                </div>

            </div>

            <div class="col-md-3 col-sm-6">

                <div class="panel panel-success">

                    <div class="panel-heading">
                        Active Employees
                    </div>

                    <div class="panel-body">
                        <h2>105</h2>
                    </div>

                </div>

            </div>

            <div class="col-md-3 col-sm-6">

                <div class="panel panel-info">

                    <div class="panel-heading">
                        Departments
                    </div>

                    <div class="panel-body">
                        <h2>8</h2>
                    </div>

                </div>

            </div>

            <div class="col-md-3 col-sm-6">

                <div class="panel panel-warning">

                    <div class="panel-heading">
                        New Employees
                    </div>

                    <div class="panel-body">
                        <h2>15</h2>
                    </div>

                </div>

            </div>

        </div>

    </div>

</asp:Content>