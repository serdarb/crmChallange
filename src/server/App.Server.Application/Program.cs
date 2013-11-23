using System;
using System.ServiceProcess;

namespace App.Server.Application
{
    public class Program
    {
        static void Main()
        {
            if (Environment.UserInteractive)
            {
                Bootstrapper.Initialize();
                Console.WriteLine("Service is ready!");
                Console.ReadLine();
            }
            else
            {
                ServiceBase.Run(new ServiceBase[] { new AppWindowsService() });
            }
        }
    }
}