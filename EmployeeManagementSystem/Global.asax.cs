using System;
using System.Web;
using System.Web.Http;

public partial class Global : HttpApplication
{
    protected void Application_Start(object sender, EventArgs e)
    {
        WebApiConfig.Register(GlobalConfiguration.Configuration);
    }

    protected void Application_End(object sender, EventArgs e)
    {
    }

    protected void Application_Error(object sender, EventArgs e)
    {
    }

    protected void Session_Start(object sender, EventArgs e)
    {
    }

    protected void Session_End(object sender, EventArgs e)
    {
    }
}