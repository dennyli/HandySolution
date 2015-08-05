using System.ServiceModel;
using Lighter.Client.Infrastructure.Accounts;
using Lighter.LoginService.Model;

namespace Lighter.Client.Infrastructure.Interface
{
    public interface IAccountContext
    {
        void SetCurrentAccount(Account account);

        Account GetCurrentAccount();

        string GetCurrentAccountName();

        bool CheckHasCommandAuthority(string commandId);

        void AccountLogout();

        void AccountLogin(LoginInfo info, InstanceContext contextCallback);
    }
}
