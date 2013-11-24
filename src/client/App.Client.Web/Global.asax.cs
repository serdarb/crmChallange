using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using App.Client.Web.App_Start;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace App.Client.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            MvcHandler.DisableMvcResponseHeader = true;

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var container = new WindsorContainer().Install(FromAssembly.This());
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            var dictionary = new Dictionary<string, string>();
            dictionary.Add("Email","Email");
            dictionary.Add("FirstName", "First Name");

            Application.Add("en_txt", dictionary);

            dictionary = new Dictionary<string, string>();
            dictionary.Add("Email", "Eposta");
            dictionary.Add("FirstName", "Ad");

            Application.Add("tr_txt", dictionary);
        }

        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("X-Powered-By");
            HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
            HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");

            HttpContext.Current.Response.Headers.Set("Server", "My Web Server 1");
        }
    }
}