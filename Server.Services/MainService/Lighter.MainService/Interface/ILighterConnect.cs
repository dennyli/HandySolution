using System.Collections.ObjectModel;
using Lighter.MainService.Model;
using System.ServiceModel;

namespace Lighter.MainService.Interface
{
    //[ServiceContract]
    //public 
        interface ILighterConnect
    {
        ObservableCollection<Client> Clients { get;}

        bool Connect(Client client);
        void Disconnect(Client client);
    }
}
