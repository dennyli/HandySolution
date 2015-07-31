using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using Lighter.BaseService;
using Lighter.MainService.Interface;
using Lighter.MainService.Model;
using Lighter.ServiceManager;
using Lighter.ServiceManager.Endpoints;
using Lighter.ServiceManager.Hosting;
using Microsoft.Practices.Prism.Logging;

namespace Lighter.MainService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false, Namespace = "http://www.codestar.com/")]
    [ExportService("LighterMainService", typeof(LighterMainService),  typeof(ILighterMainService), 0), TcpEndpoint(40001)]
    public class LighterMainService : LighterServiceBase, ILighterMainService
    {
        //private Dictionary<string, ILighterConnectCallBack> _callbacks = new Dictionary<string, ILighterConnectCallBack>();
        private ObservableCollection<Client> _clients = new ObservableCollection<Client>();
        private object _syncObj = new object();


        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            //set { _clients = value; }
        }
        
        [Import]
        public IServiceHostManager ServiceHostManager { get; set; }


        //[OperationContract(IsInitiating = true)]
        public bool Connect(Client client)
        {
            //if ((client == null) || _callbacks.ContainsKey(client.IP))
            if (client == null)
                return false;

            Logger.Log(client.Name + " connecting from " + client.IP + "... ", Category.Debug, Priority.Low);

            //ILighterConnectCallBack callback = OperationContext.Current.GetCallbackChannel<ILighterConnectCallBack>();
            lock (_syncObj)
            {
                //_callbacks.Add(client.IP, callback);
                _clients.Add(client);
            }

            Logger.Log(client.Name + " connected from " + client.IP + "... ", Category.Debug, Priority.Low);

            return true;
        }

        //[OperationContract(IsOneWay = true)]
        public void Disconnect(Client client)
        {
            Logger.Log(client.Name + " disconnecting from " + client.IP + "... ", Category.Debug, Priority.Low);

            //if ((client != null) && _callbacks.ContainsKey(client.IP))
            if (client != null)
            {
                lock (_syncObj)
                {
                    try
                    {
                        Client exists = _clients.Single<Client>(c => c.IP == client.IP);
                        _clients.Remove(exists);

                        //_callbacks.Remove(exists.IP);
                    }
                    catch (InvalidOperationException)
                    {
                        
                    }
                }
            }

            Logger.Log(client.Name + " disconnected from " + client.IP + "... ", Category.Debug, Priority.Low);
        }

        //[OperationContract]
        public bool ServiceIsExists(string serviceName)
        {
            try
            {
                ExportServiceHost host = ServiceHostManager.Services.First<ExportServiceHost>(h => h.GetServiceName() == serviceName);
                return (host != null);
            }
            catch(ArgumentNullException)
            {
                return false;
            }
            catch(InvalidOperationException)
            {
                return false;
            }
        }

        //[OperationContract]
        public Uri[] GetServiceAddress(string serviceName)
        {
            try
            {
                ExportServiceHost host = ServiceHostManager.Services.First<ExportServiceHost>(h => h.GetServiceName() == serviceName);
                return host.GetAddresses();
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }


        public override void Initialize()
        {
            
        }
    }
}
