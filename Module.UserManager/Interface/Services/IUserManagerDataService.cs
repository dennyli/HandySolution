using Client.Module.Common.Interface;
using System.Collections.ObjectModel;
using Lighter.UserManagerService.Model;
using Client.Module.UserManager.Models;

namespace Client.Module.UserManager.Interface.Services
{
    public interface IUserManagerDataService : IDataService
    {
        Accounts GetAccounts();
    }
}
