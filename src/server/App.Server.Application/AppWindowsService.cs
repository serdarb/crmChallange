using System.Diagnostics;
using System.ServiceProcess;

namespace App.Server.Application
{
    partial class AppWindowsService : ServiceBase
    {
        public AppWindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("Service Starting", EventLogEntryType.Information);
            Bootstrapper.Initialize();
            EventLog.WriteEntry("Service Started", EventLogEntryType.Information);
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry("Service Stopped", EventLogEntryType.Information);
        }
    }
}
