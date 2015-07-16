using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.ServiceManager;

namespace ExportServiceHostManagerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHostManager manager = new ServiceHostManager();
            manager.Run();

            //int basePort = 4000;

            foreach (var service in manager.Services)
            {
                //service.UpdateAddressPort(basePort);

                foreach (var address in service.Description.Endpoints)
                {
                    Console.WriteLine("Hosting Service: " + service.Meta.Name + " at " + address.Address.Uri);
                }

                service.Open();
            }

            Console.ReadKey();

            foreach (var service in manager.Services)
                service.Close();
        }
    }
}
