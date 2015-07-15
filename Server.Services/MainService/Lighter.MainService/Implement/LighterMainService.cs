using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using Lighter.MainService.Interface;
using Lighter.MainServiceModel;
using Lighter.ServiceHostManager;

namespace Lighter.MainService.Implement
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    [ExportService("LighterMainService", typeof(LighterMainService))]
    public class LighterMainService : ILighterMainService, ILighterConnect
    {
        private Dictionary<string, ILighterConnectCallBack> _callbacks = new Dictionary<string, ILighterConnectCallBack>();
        private ObservableCollection<Client> _clients = new ObservableCollection<Client>();

        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            //set { _clients = value; }
        }

        private object _syncObj = new object();

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

        [OperationContract(IsOneWay = true, IsTerminating = true)]
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
    }
}
