using Lighter.Client.Infrastructure.Accounts;
using Lighter.UserManagerService.Model;

namespace Lighter.Client.Infrastructure.Interface
{
    public interface IAccountContext
    {
        void SetCurrentAccount(AccountDTO accountDto);

        Account GetCurrentAccount();

        string GetCurrentAccountName();

        bool CheckHasCommandAuthority(string commandId);

        //void AccountLogout();
    }
}
