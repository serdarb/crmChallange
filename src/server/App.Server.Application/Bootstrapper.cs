using System;
using System.Linq;
using System.ServiceModel;
using App.Domain.Repo;
using App.Server.Service;
using Castle.Facilities.Logging;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace App.Server.Application
{
    public class Bootstrapper
    {
        public static IWindsorContainer Container { get; private set; }

        public static void Initialize()
        {
            Container = new WindsorContainer();
            Container.AddFacility<WcfFacility>();
            Container.AddFacility<LoggingFacility>(f => f.UseNLog());

            var netNamedPipeBinding = new NetNamedPipeBinding
            {
                MaxBufferSize = 67108864,
                MaxReceivedMessageSize = 67108864,
                TransferMode = TransferMode.Streamed,
                ReceiveTimeout = new TimeSpan(0, 30, 0),
                SendTimeout = new TimeSpan(0, 30, 0)
            };
            
            Container.Register(
                Component.For<ExceptionInterceptor>(),
                Component.For(typeof(IEntityRepository<>)).ImplementedBy(typeof(EntityRepository<>)),
                Types.FromAssemblyNamed("App.Server.Service")
                     .Pick()
                     .If(type => type.GetInterfaces().Any(i => i.IsDefined(typeof(ServiceContractAttribute), true) && i.Name != typeof(BaseService).Name))
                     .Configure(
                         configurer =>
                         configurer.Named(configurer.Implementation.Name)
                                   .LifestyleSingleton()
                                   .AsWcfService(
                                       new DefaultServiceModel().AddEndpoints(WcfEndpoint.BoundTo(netNamedPipeBinding)
                                                                                         .At(string.Format("net.pipe://localhost/{0}", configurer.Implementation.Name))).PublishMetadata()))
                     .WithService.Select((type, baseTypes) => type.GetInterfaces().Where(i => i.IsDefined(typeof(ServiceContractAttribute), true))));
            

        }
    }
}
