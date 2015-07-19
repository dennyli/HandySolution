using System;
using Lighter.ServiceManager;
using Microsoft.Practices.ServiceLocation;

namespace ExportServiceHostManagerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //ServiceHostManager manager = new ServiceHostManager();
            //manager.Run();

            ServiceHostBootstrapper bootstrapper = new ServiceHostBootstrapper();
            bootstrapper.Run();

            //ServiceHostManager manager = bootstrapper._container.GetExportedValue<IServiceHostManager>() as ServiceHostManager;

            IServiceLocator serviceLocator = bootstrapper._container.GetExportedValue<IServiceLocator>();
            ServiceHostManager manager = serviceLocator.GetInstance<IServiceHostManager>() as ServiceHostManager;
            manager._container = bootstrapper._container;
            manager.LookupServices();

            int basePort = 50000;

            foreach (var service in manager.Services)
            {
                if (!service.UpdateAddressPort(basePort))
                    Console.WriteLine("Hosting Service: " + service.Meta.Name + " UpdateAddressPort " + basePort.ToString() + " failure.");

                foreach (var address in service.Description.Endpoints)
                {
                    Console.WriteLine("Hosting Service: " + service.Meta.Name + " at " + address.Address.Uri);
                }

                service.Open();

                basePort++;
            }

            Console.ReadKey();

            foreach (var service in manager.Services)
                service.Close();
        }
    }
}
