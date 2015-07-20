using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceModel;
using Lighter.MainService.Interface;
using Lighter.MainService.Model;
using Lighter.LoginService.Interface;
using Lighter.LoginService.Model;
using Utility;

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
                EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:40000/LighterMainService"));
                NetTcpBinding binding = new NetTcpBinding();
                binding.Security.Mode = SecurityMode.None;

                ChannelFactory<ILighterMainService> mainScrFactory = new ChannelFactory<ILighterMainService>(binding, address);
                ILighterMainService mainService = mainScrFactory.CreateChannel();

                Client client = new Client();
                client.Name = "test";
                client.IP = ip;
                client.Time = DateTime.Now;

                Console.WriteLine("Connecting to main service...");
                mainService.Connect(client);

                //string[] testsvrs = new string[] { "testService", "LighterMainService" };

                string[] testsvrs = new string[] { "LighterLoginService" };
                foreach (string svr in testsvrs)
                {
                    Console.Write("Finding " + svr + " ... ");
                    bool bExist = mainService.ServiceIsExists(svr);
                    Console.WriteLine(bExist);

                    if (bExist)
                    {
                        Console.WriteLine("\t" + svr + " has found, Getting " + svr + " address ... ");
                        Uri[] uris = mainService.GetServiceAddress(svr);
                        if (uris.Count<Uri>() > 0)
                        {
                            Console.WriteLine("\t" + svr + " address list:");
                            foreach (Uri uri in uris)
                                Console.WriteLine("\t\t" + uri.ToString());
                        }
                        else
                            Console.WriteLine("\tCan't find " + svr + " address!");

                        #region Test Login
                        Console.WriteLine("Connecting to login service...");
                        EndpointAddress addressLogin = new EndpointAddress(uris[0]);
                        NetTcpBinding bindingLogin = new NetTcpBinding();
                        bindingLogin.Security.Mode = SecurityMode.None;

                        ChannelFactory<ILighterLoginService> factoryLogin = new ChannelFactory<ILighterLoginService>(bindingLogin, addressLogin);
                        ILighterLoginService serviceLogin = factoryLogin.CreateChannel();

                        Console.WriteLine("Loginning ...");
                        LoginInfo info = new LoginInfo("A1", "123", ip);
                        OperationResult or = serviceLogin.Login(info);
                        if (or.ResultType == OperationResultType.Success)
                        {
                            Console.WriteLine("Login Success, " + or.Message + " \n\tGet All Accounts ...");
                            string allAccount = serviceLogin.GetAllAccounts();
                            Console.WriteLine(allAccount);

                            Console.WriteLine("Logouting ...");
                            serviceLogin.Logout(info);
                        }
                        else
                        {
                            Console.Write(or.ResultType.ToDescription() + " " + or.Message);
                            Console.WriteLine("Login Failure!");
                        }

                        #endregion Test Login
                    }
                    else
                        Console.WriteLine("\tCan't find service " + svr + "!");
                }

                Console.WriteLine("Disconnecting...");
                mainService.Disconnect(client);
            }
            catch (EndpointNotFoundException ex)
            {
                Console.WriteLine("Error: 无法连接到服务！\n" + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
