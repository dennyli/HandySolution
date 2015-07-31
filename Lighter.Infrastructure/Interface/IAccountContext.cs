using Lighter.UserManagerService.Model;

namespace Lighter.Client.Infrastructure.Interface
{
    public interface IAccountContext
    {
        void SetCurrentAccount(AccountDTO accountDto);
        bool CheckHasCommandAuthority(string commandId);
    }
}
