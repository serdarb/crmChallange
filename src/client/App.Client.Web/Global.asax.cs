using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using App.Client.Web.App_Start;
using App.Domain.Contracts;
using App.Utils;
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

            var container = PrepareIocContainer();

            PrepareLocalizationStrings(container);
        }

        private static IWindsorContainer PrepareIocContainer()
        {
            var container = new WindsorContainer().Install(FromAssembly.This());
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
            return container;
        }

        private void PrepareLocalizationStrings(IWindsorContainer container)
        {
            var localizationService = container.Resolve<ILocalizationService>();

            var trTexts = localizationService.GetAll(ConstHelper.tr).Result.ToDictionary(item => item.Name, item => item.Value);
            var enTexts = localizationService.GetAll(ConstHelper.en).Result.ToDictionary(item => item.Name, item => item.Value);
            Application.Add(ConstHelper.en_txt, enTexts);
            Application.Add(ConstHelper.tr_txt, trTexts);
            container.Release(localizationService);
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