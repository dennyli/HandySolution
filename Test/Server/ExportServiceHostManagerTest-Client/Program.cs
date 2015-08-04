#define UserTest
#define LoginTest

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
using System.ServiceModel.Channels;
using Lighter.UserManagerService.Interface;
using Lighter.UserManagerService.Model;
using System.Collections.Generic;

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
                EndpointAddress address = new EndpointAddress(new Uri("net.tcp://" + ip + ":50000/LighterMainService"));
                NetTcpBinding binding = new NetTcpBinding();
                binding.Security.Mode = SecurityMode.None;
                binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.None;

                ChannelFactory<ILighterMainService> mainScrFactory = new ChannelFactory<ILighterMainService>(binding, address);
                ILighterMainService mainService = mainScrFactory.CreateChannel();

                //using (new OperationContextScope(mainService as IContextChannel))
                //{

                //MessageHeader Client1InstanceContextHeader = MessageHeader.CreateHeader("", "", "");
                //OperationContext.Current.OutgoingMessageHeaders.Add(Client1InstanceContextHeader);
                // }
               

                Client client = new Client();
                client.Name = "test";
                client.IP = ip;
                client.Time = DateTime.Now;

                Console.WriteLine("Connecting to main service...");
                mainService.Connect(client);

                bool bExist;
                string svr;

#if LoginTest
                svr = "LighterLoginService";
                Console.Write("Finding " + svr + " ... ");
                bExist = mainService.ServiceIsExists(svr);
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
                    LoginInfo info = new LoginInfo("Acc001", "123456", ip);
                    serviceLogin.Login(info);
                    //if (or.ResultType == OperationResultType.Success)
                    //{
                    //    string[] infos = or.LogMessage.Split(new char[] { '|' });

                    //    Console.WriteLine("Logouting ...");
                    //    serviceLogin.Logout(infos[0]);
                    //}
                    //else
                    //{
                    //    Console.Write(or.ResultType.ToDescription() + " " + or.Message);
                    //    Console.WriteLine("Login Failure!");
                    //}

                    (serviceLogin as IChannel).Close();

                    #endregion Test Login
                }
                else
                    Console.WriteLine("\tCan't find service " + svr + "!");
#endif

#if UserTest
                svr = "LighterUserManagerService";
                Console.Write("Finding " + svr + " ... ");
                bExist = mainService.ServiceIsExists(svr);
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

                    #region Test User Manager
                    Console.WriteLine("Connecting to UserManager service...");
                    EndpointAddress addressUM = new EndpointAddress(uris[0]);
                    
                    NetTcpBinding bindingUM = new NetTcpBinding();
                    bindingUM.Security.Mode = SecurityMode.None;

                    ChannelFactory<ILighterUserManagerService> factoryUserManager = new ChannelFactory<ILighterUserManagerService>(bindingUM, addressUM);
                    ILighterUserManagerService serviceUM = factoryUserManager.CreateChannel();
                    
                    Console.WriteLine("Get accounts ...");

                    try
                    {
                        List<AccountDTO> accounts = serviceUM.GetAccounts();
                        Console.WriteLine("Accounts: " + accounts.Count.ToString());
                        foreach (AccountDTO dto in accounts)
                            Console.WriteLine(dto.ToString() + "\n");

                        (serviceUM as IChannel).Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("异常: " + ex.Message);
                    }
                    #endregion
                }
#endif

                Console.WriteLine("Disconnecting...");
                mainService.Disconnect(client);
                Console.WriteLine("Disconnected.");

                (mainService as IChannel).Close();
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
