<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Http" %>

<script runat="server">

    protected void Application_Start(object sender, EventArgs e)
    {
        GlobalConfiguration.Configure(EmployeeManagementSystem.WebApiConfig.Register);
    }

</script>