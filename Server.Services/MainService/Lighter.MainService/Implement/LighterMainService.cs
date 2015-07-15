﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using Lighter.MainService.Interface;
using Lighter.ServiceHostManager;
using Lighter.BaseService;
using Lighter.ServiceHostManager.Endpoints;
using Lighter.MainService.Model;
using System.ComponentModel.Composition;
using Lighter.ServiceHostManager.Hosting;

namespace Lighter.MainService.Implement
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    [ExportService("LighterMainService", typeof(LighterMainService)), TcpEndpoint(40001)]
    public class LighterMainService : LighterServiceBase, ILighterMainService, ILighterConnect
    {
        private Dictionary<string, ILighterConnectCallBack> _callbacks = new Dictionary<string, ILighterConnectCallBack>();
        private ObservableCollection<Client> _clients = new ObservableCollection<Client>();
        private object _syncObj = new object();


        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            //set { _clients = value; }
        }
        
        [Import]
        public IServiceHostManager ServiceHostManager { get; set; }



        [OperationContract(IsInitiating = true)]
        public bool Connect(Client client)
        {
            if ((client == null) || _callbacks.ContainsKey(client.IP))
                return false;

            ILighterConnectCallBack callback = OperationContext.Current.GetCallbackChannel<ILighterConnectCallBack>();
            lock (_syncObj)
            {
                _callbacks.Add(client.IP, callback);
                _clients.Add(client);
            }

            return true;
        }

        [OperationContract(IsOneWay = true)]
        public void Disconnect(Client client)
        {
            if ((client != null) && _callbacks.ContainsKey(client.IP))
            {
                lock (_syncObj)
                {
                    try
                    {
                        Client exists = _clients.Single<Client>(c => c.IP == client.IP);
                        _clients.Remove(exists);

                        _callbacks.Remove(exists.IP);
                    }
                    catch (InvalidOperationException)
                    {
                        
                    }
                }
            }
        }


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

        public Uri GetServiceAddress(string serviceName)
        {
            try
            {
                ExportServiceHost host = ServiceHostManager.Services.First<ExportServiceHost>(h => h.GetServiceName() == serviceName);
                if (host.BaseAddresses.Any())
                    return host.BaseAddresses[0];

                return null;
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
    }
}
