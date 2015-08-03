using System.Linq;
using Lighter.BaseService.Interface;
using Lighter.Data;
using Lighter.LoginService.Model;
using Utility;

namespace Lighter.LoginService.LoginData
{
    public interface ILighterLoginDataService : ILighterService
    {
        IQueryable<Account> Accounts { get; }


        OperationResult Login(LoginInfo info);

        OperationResult Logout(string userId);
    }
}
