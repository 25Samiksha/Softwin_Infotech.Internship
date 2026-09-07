<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
       <div class="row">
           <div class="col-md-6 col-md-offset-3">
        
               <div class="panel panel-primary">
           
                   <div class="panel-heading">
              
                       <h3 class="panel-title">Login</h3>
             </div>
               
                   <div class="panel-body">
         <div class="form-group">
          
              <label>Email</label>
             <br />
             
             <asp:TextBox ID="txtEmail" runat="server" CssClass="Form-control" placeholder="Enter Email" Height="34px" Width="100%"></asp:TextBox>
              </div>
         <div class="form-group">
            <label>Password</label>
                       <br />
 
              <asp:TextBox ID="txtpass" runat="server" CssClass="From-control" placeholder="Enter password" Height="34px" Width="100%"></asp:TextBox>
                  </div><div class="checkbox">
                    <label>
                       <asp:CheckBox ID="chkRemember"
                                    runat="server" />
                                Remember me
                        </label>
                        </div>
            <asp:Button ID="btnLogin"
                            runat="server"
                            Text="Login"
                            CssClass="btn btn-primary btn-block" />
</div>
 </div>
</div>
     </div>
</div>
</asp:Content>