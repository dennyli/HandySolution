using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Lighter.Model;

namespace Lighter.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class LighterService : ILighterService, ILogin
    {
        private Dictionary<string, ILoginCallBack> callbacks = new Dictionary<string, ILoginCallBack>();
        private List<Client> clients = new List<Client>();
        private object syncObj = new object();

        [OperationContract(IsInitiating = true)]
        public bool Connect(Client client)
        {
            if ((client == null) || callbacks.ContainsKey(client.IP))
                return false;

            ILoginCallBack callback = OperationContext.Current.GetCallbackChannel<ILoginCallBack>();
            lock (syncObj)
            {
                callbacks.Add(client.IP, callback);
                clients.Add(client);
            }

            return true;
        }

        [OperationContract(IsOneWay = true, IsTerminating = true)]
        public void Disconnect(Client client)
        {
            if ((client != null) && callbacks.ContainsKey(client.IP))
            {
                lock (syncObj)
                {
                    try
                    {
                        Client exists = clients.Single<Client>(c => c.IP == client.IP);
                        clients.Remove(exists);

                        callbacks.Remove(exists.IP);
                    }
                    catch (InvalidOperationException)
                    {
                        
                    }
                }
            }
        }
    }
}
