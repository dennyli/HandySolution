using System.Collections.ObjectModel;
using Lighter.Model;

namespace Lighter.Service.Interface
{
    public interface ILogin
    {
        public ObservableCollection<Client> Clients;

        bool Connect(Client client);
        void Disconnect(Client client);
    }
}
