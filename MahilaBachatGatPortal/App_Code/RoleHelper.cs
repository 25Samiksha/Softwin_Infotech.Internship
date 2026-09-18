using System;
using System.Web.UI;

public static class RoleHelper
{
    public static bool IsLoggedIn()
    {
        return System.Web.HttpContext.Current.Session["UserID"] != null;
    }
    public static string GetRole()
    {
        if (System.Web.HttpContext.Current.Session["Role"] == null)
        {
            return "";
        }

        return System.Web.HttpContext.Current.Session["Role"].ToString();
    }

    public static int GetUserID()
    {
        if (System.Web.HttpContext.Current.Session["UserID"] == null)
        {
            return 0;
        }

        return Convert.ToInt32(
            System.Web.HttpContext.Current.Session["UserID"]);
    }
    public static bool IsAdmin()
    {
        return GetRole().Equals(
            "Admin",
            StringComparison.OrdinalIgnoreCase);
    }
    public static bool IsPresidentOrSecretary()
    {
        string role = GetRole();

        return role.Equals(
                   "President",
                   StringComparison.OrdinalIgnoreCase)
               ||
               role.Equals(
                   "Secretary",
                   StringComparison.OrdinalIgnoreCase);
    }
    public static bool IsMember()
    {
        return GetRole().Equals(
            "Member",
            StringComparison.OrdinalIgnoreCase);
    }
    public static void RequireAdmin(Page page)
    {
        if (!IsLoggedIn())
        {
            page.Response.Redirect("User.aspx");
            return;
        }

        if (!IsAdmin())
        {
            page.Response.Redirect("Dashboard.aspx");
        }
    }
    public static void RequireManagement(Page page)
    {
        if (!IsLoggedIn())
        {
            page.Response.Redirect("User.aspx");
            return;
        }

        string role = GetRole();

        if (!role.Equals("Admin",
                         StringComparison.OrdinalIgnoreCase)
            &&
            !role.Equals("President",
                         StringComparison.OrdinalIgnoreCase)
            &&
            !role.Equals("Secretary",
                         StringComparison.OrdinalIgnoreCase))
        {
            page.Response.Redirect("Dashboard.aspx");
        }
    }
    public static void RequirePresidentSecretary(Page page)
    {
        if (!IsLoggedIn())
        {
            page.Response.Redirect("User.aspx");
            return;
        }

        if (!IsPresidentOrSecretary())
        {
            page.Response.Redirect("Dashboard.aspx");
        }
    }
    public static void RequireLogin(Page page)
    {
        if (!IsLoggedIn())
        {
            page.Response.Redirect("User.aspx");
        }
    }
}