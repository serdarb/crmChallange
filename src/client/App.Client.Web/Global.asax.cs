using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using App.Client.Web.App_Start;

namespace App.Client.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            MvcHandler.DisableMvcResponseHeader = true;

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }        
    }
}