using System;
using Lighter.ServiceManager;
using Microsoft.Practices.ServiceLocation;
using Lighter.Server.Infrastructure.Initialize;
using System.Diagnostics;
using System.ServiceModel;
using Lighter.BaseService.Interface;

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

            DatabaseInitializer.Initialize();

            //ServiceHostManager manager = bootstrapper._container.GetExportedValue<IServiceHostManager>() as ServiceHostManager;

            IServiceLocator serviceLocator = bootstrapper._container.GetExportedValue<IServiceLocator>();
            ServiceHostManager manager = serviceLocator.GetInstance<IServiceHostManager>() as ServiceHostManager;
            manager._container = bootstrapper._container;
            manager.LookupServices();

            int basePort = 50000;

            foreach (var host in manager.Services)
            {
                if (!host.UpdateAddressPort(basePort))
                    Console.WriteLine("Hosting Service: " + host.Meta.Name + " UpdateAddressPort " + basePort.ToString() + " failure.");

                //foreach (var address in service.Description.Endpoints)
                //{
                //    Console.WriteLine("Hosting Service: " + service.Meta.Name + " at " + address.Address.Uri);
                //}

                Debug.Assert(host.Description.Endpoints.Count == 1);

                host.Open();

                OperationContext operationContext = OperationContext.Current;
                InstanceContext instanceContext = operationContext.InstanceContext;
                ILighterService service = instanceContext.GetServiceInstance() as ILighterService;
                service.Initialize();

                var address = host.Description.Endpoints[0];
                Console.WriteLine("Hosting Service: " + host.Meta.Name + " at " + address.Address.Uri);

                //basePort++;
            }

            Console.ReadKey();

            foreach (var service in manager.Services)
                service.Close();
        }
    }
}
