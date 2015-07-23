using System.Linq;
using Lighter.BaseService;
using Lighter.Data;
using Utility;

namespace Lighter.LoginService.LoginData
{
    public interface ILighterLoginDataService : ILighterService
    {
        IQueryable<Account> Accounts { get; }


        OperationResult Login(string userName, string userPwd);

        OperationResult Logout(string userName);
    }
}
