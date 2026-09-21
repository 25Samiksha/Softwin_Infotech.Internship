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

    public static int GetBachatGatID()
    {
        if (System.Web.HttpContext.Current.Session["BachatGatID"] == null)
        {
            return 0;
        }

        return Convert.ToInt32(
            System.Web.HttpContext.Current.Session["BachatGatID"]);
    }

    public static int GetMemberID()
    {
        if (System.Web.HttpContext.Current.Session["MemberID"] == null)
        {
            return 0;
        }

        return Convert.ToInt32(
            System.Web.HttpContext.Current.Session["MemberID"]);
    }
    public static bool IsAdmin()
    {
        return GetRole().Equals(
            "Admin",
            StringComparison.OrdinalIgnoreCase);
    }


    public static bool IsPresident()
    {
        return GetRole().Equals(
            "President",
            StringComparison.OrdinalIgnoreCase);
    }


    public static bool IsSecretary()
    {
        return GetRole().Equals(
            "Secretary",
            StringComparison.OrdinalIgnoreCase);
    }


    public static bool IsPresidentOrSecretary()
    {
        return IsPresident() || IsSecretary();
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
            return;
        }

        // President/Secretary must have a Bachat Gat
        if (!IsAdmin() && GetBachatGatID() == 0)
        {
            page.Session.Clear();
            page.Response.Redirect("User.aspx");
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
            return;
        }

        if (GetBachatGatID() == 0)
        {
            page.Session.Clear();
            page.Response.Redirect("User.aspx");
        }
    }
    public static void RequirePresident(Page page)
    {
        if (!IsLoggedIn())
        {
            page.Response.Redirect("User.aspx");
            return;
        }

        if (!IsPresident())
        {
            page.Response.Redirect("Dashboard.aspx");
            return;
        }

        if (GetBachatGatID() == 0)
        {
            page.Session.Clear();
            page.Response.Redirect("User.aspx");
        }
    }

    public static void RequireMember(Page page)
    {
        if (!IsLoggedIn())
        {
            page.Response.Redirect("User.aspx");
            return;
        }

        if (!IsMember())
        {
            page.Response.Redirect("Dashboard.aspx");
            return;
        }
        if (GetBachatGatID() == 0 ||
            GetMemberID() == 0)
        {
            page.Session.Clear();
            page.Response.Redirect("User.aspx");
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