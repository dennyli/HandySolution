using System.Collections.ObjectModel;
using Lighter.MainService.Model;

namespace Lighter.MainService.Interface
{
    public interface ILighterConnect
    {
        ObservableCollection<Client> Clients { get;}

        bool Connect(Client client);
        void Disconnect(Client client);
    }
}
