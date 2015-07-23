using System.ComponentModel.Composition;
using System.Linq;
using Lighter.BaseService;
using Lighter.Data;
using Lighter.Data.Repositories;
using Utility;

namespace Lighter.LoginService.LoginData
{
    public class LighterLoginDataService : LighterServiceBase, ILighterLoginDataService
    {
        [Import]
        protected IAccountRepository AccountRepository { get; set; }

        public IQueryable<Account> Accounts
        {
            get { return AccountRepository.Entities; }
        }

        public OperationResult Login(string userName, string userPwd)
        {
            PublicHelper.CheckArgument(userName, "User Name");
            PublicHelper.CheckArgument(userPwd, "User Password");

            Account account = Accounts.SingleOrDefault<Account>(a => a.Name == userName);
            if (account == null)
                return new OperationResult(OperationResultType.ParamError, "指定账号的用户不存在。");

            if (userPwd != account.Password)
                return new OperationResult(OperationResultType.ParamError, "登录密码不正确。");

            account.IsLogin = true;
            AccountRepository.Update(account);
            
            return new OperationResult(OperationResultType.Success, "登录成功。", account.Authority);
        }

        public OperationResult Logout(string userName)
        {
            PublicHelper.CheckArgument(userName, "User Name");

            Account account = Accounts.SingleOrDefault<Account>(a => a.Name == userName);
            if (account == null)
                return new OperationResult(OperationResultType.ParamError, "指定账号的用户不存在。");

            account.IsLogin = false;
            AccountRepository.Update(account);

            return new OperationResult(OperationResultType.Success, "退出成功。"); 
        }
    }
}
