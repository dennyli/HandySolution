using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.ServiceModel;
using Lighter.MainService.Interface;
using System.Diagnostics;
using Lighter.MainService.Model;

namespace ExportServiceHostManagerTest_Client
{
    class Program
    {
        static IPAddress GetHostIP()
        {
            IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ip = ipe.AddressList[0];

            return ip;
        }

        static void Main(string[] args)
        {
            try
            {
                string ip = GetHostIP().ToString();

                //EndpointAddress address = new EndpointAddress(new Uri("net.tcp://" + ip + ":40001/LighterMainService"));
                EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:40001/LighterMainService"));
                NetTcpBinding binding = new NetTcpBinding();
                binding.Security.Mode = SecurityMode.None;

                ChannelFactory<ILighterMainService> mainScrFactory = new ChannelFactory<ILighterMainService>(binding, address);
                ILighterMainService mainService = mainScrFactory.CreateChannel();

                Client client = new Client();
                client.Name = "test";
                client.IP = ip;
                client.Time = DateTime.Now;

                Console.WriteLine("Connecting...");
                mainService.Connect(client);

                Console.Write("Finding testService ... ");
                bool bExist = mainService.ServiceIsExists("testService");
                Console.WriteLine(bExist);

                Console.Write("Finding LighterMainService ... ");
                bExist = mainService.ServiceIsExists("LighterMainService");
                Console.WriteLine(bExist);

                if (bExist)
                {
                    Console.Write("Getting LighterMainService address ... ");
                    Uri uri = mainService.GetServiceAddress("LighterMainService");
                    Console.WriteLine(uri.ToString());
                }

                Console.WriteLine("Disconnecting...");
                mainService.Disconnect(client);

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
