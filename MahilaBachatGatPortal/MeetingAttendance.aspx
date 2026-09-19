<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="MeetingAttendance.aspx.cs" Inherits="MeetingAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
.page-title { margin-top: 0; font-weight: bold; }
.attendance-panel { background: #ffffff; padding: 20px; border-radius: 8px; margin-bottom: 25px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); }
.section-title { font-weight: bold; margin-top: 0; margin-bottom: 20px; }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2 class="page-title">Member Attendance</h2>
<p class="text-muted">Mark attendance for Bachat Gat meetings.</p>

<div class="attendance-panel">
<h4 class="section-title">Select Meeting</h4>
<div class="row">
<div class="col-md-5">
<div class="form-group">
<label>Bachat Gat</label>
<asp:DropDownList ID="ddlBachatGat" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBachatGat_SelectedIndexChanged"></asp:DropDownList>
</div>
</div>
<div class="col-md-5">
<div class="form-group">
<label>Meeting</label>
<asp:DropDownList ID="ddlMeeting" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMeeting_SelectedIndexChanged"></asp:DropDownList>
</div>
</div>
</div>
<asp:Label ID="lblMeetingInfo" runat="server" CssClass="text-info" Font-Bold="true"></asp:Label>
</div>

<div class="attendance-panel">
<h4 class="section-title">Mark Attendance</h4>
<div class="table-responsive">
<asp:GridView ID="gvAttendance" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" DataKeyNames="MemberID">
<Columns>
<asp:BoundField DataField="MemberID" HeaderText="ID" />
<asp:BoundField DataField="MemberCode" HeaderText="Member Code" />
<asp:BoundField DataField="MemberName" HeaderText="Member Name" />
<asp:BoundField DataField="Mobile" HeaderText="Mobile" />
<asp:TemplateField HeaderText="Attendance">
<ItemTemplate>
<asp:DropDownList ID="ddlAttendance" runat="server" CssClass="form-control">
<asp:ListItem Text="Present" Value="Present"></asp:ListItem>
<asp:ListItem Text="Absent" Value="Absent"></asp:ListItem>
<asp:ListItem Text="Late" Value="Late"></asp:ListItem>
</asp:DropDownList>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Remarks">
<ItemTemplate>
<asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"></asp:TextBox>
</ItemTemplate>
</asp:TemplateField>
</Columns>
<EmptyDataTemplate>No members found.</EmptyDataTemplate>
</asp:GridView>
</div>

<br />

<asp:Button ID="btnSaveAttendance" runat="server" Text="Save Attendance" CssClass="btn btn-primary" OnClick="btnSaveAttendance_Click" />
&nbsp;
<asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-default" OnClick="btnClear_Click" />

<br />
<br />

<asp:Label ID="lblMessage" runat="server" Font-Bold="true"></asp:Label>
</div>
</asp:Content>