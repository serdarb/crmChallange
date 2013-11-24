using System;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using App.Client.Web.Services;
using App.Domain.Contracts;

using Castle.Windsor;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;

namespace App.Client.Web.App_Start
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel;

        public WindsorControllerFactory(IKernel kernel)
        {
            this._kernel = kernel;
        }

        public override void ReleaseController(IController controller)
        {
            _kernel.ReleaseComponent(controller);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
            }

            return (IController)_kernel.Resolve(controllerType);
        }
    }

    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient());
        }
    }

    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IFormsAuthenticationService>().ImplementedBy<FormsAuthenticationService>().LifestylePerWebRequest());

            var netNamedPipeBinding = new NetNamedPipeBinding
            {
                MaxBufferSize = 67108864,
                MaxReceivedMessageSize = 67108864,
                TransferMode = TransferMode.Streamed,
                ReceiveTimeout = new TimeSpan(0, 30, 0),
                SendTimeout = new TimeSpan(0, 30, 0)
            };

            container.AddFacility<WcfFacility>();
            container.Register(Component.For<ILocalizationService>()
                     .AsWcfClient(new DefaultClientModel { Endpoint = WcfEndpoint.BoundTo(netNamedPipeBinding).At("net.pipe://localhost/LocalizationService") }));

            container.Register(Component.For<ICustomerService>()
                     .AsWcfClient(new DefaultClientModel { Endpoint = WcfEndpoint.BoundTo(netNamedPipeBinding).At("net.pipe://localhost/CustomerService") })
                     .LifestylePerWebRequest());
            container.Register(Component.For<IUserService>()
                     .AsWcfClient(new DefaultClientModel { Endpoint = WcfEndpoint.BoundTo(netNamedPipeBinding).At("net.pipe://localhost/UserService") })
                     .LifestylePerWebRequest());
            container.Register(Component.For<ICompanyService>()
                     .AsWcfClient(new DefaultClientModel { Endpoint = WcfEndpoint.BoundTo(netNamedPipeBinding).At("net.pipe://localhost/CompanyService") })
                     .LifestylePerWebRequest());
        }
    }
}