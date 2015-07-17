using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.BaseService;
using Utility;
using Lighter.LoginService.Data.AccountManager;
using System.ComponentModel.Composition;

namespace Lighter.LoginService.Data
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
                return new OperationResult(OperationResultType.QueryNull, "指定账号的用户不存在。");

            if (userPwd != account.Password)
                return new OperationResult(OperationResultType.Error, "登录密码不正确。");

            return new OperationResult(OperationResultType.Success, "登录成功。", account.Modules);
        }

        public void Logout(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
