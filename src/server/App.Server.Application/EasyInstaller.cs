using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace App.Server.Application
{
    [RunInstaller(true)]
    public partial class EasyInstaller : Installer
    {
        public EasyInstaller()
        {
            InitializeComponent();

            var serviceProcess = new ServiceProcessInstaller { Account = ServiceAccount.NetworkService };
            var serviceInstaller = new ServiceInstaller
            {
                ServiceName = "AppService",
                DisplayName = "App Service",
                Description = "Apps windows service",
                StartType = ServiceStartMode.Automatic
            };
            Installers.Add(serviceProcess);
            Installers.Add(serviceInstaller);
        }
    }
}
