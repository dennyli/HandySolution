using System.ComponentModel.Composition;
using System.Linq;
using Lighter.BaseService;
using Lighter.Data;
using Lighter.Data.Repositories;
using Lighter.LoginService.Model;
using Lighter.ServerEvents;
using Microsoft.Practices.Prism.Events;
using Utility;

namespace Lighter.LoginService.LoginData
{
    [Export(typeof(ILighterLoginDataService))]
    public class LighterLoginDataService : LighterServiceBase, ILighterLoginDataService
    {
        [Import]
        protected IEventAggregator _eventAggregator { get; set; }

        [Import]
        protected IAccountRepository _accountRepository { get; set; }

        public IQueryable<Account> Accounts
        {
            get { return _accountRepository.Entities; }
        }

        public OperationResult Login(LoginInfo info)
        {
            PublicHelper.CheckArgument(info.Account, "User Name");
            PublicHelper.CheckArgument(info.Password, "User Password");

            _accountRepository.Refresh(Accounts);

            Account account = Accounts.SingleOrDefault<Account>(a => a.Name == info.Account);
            if (account == null)
                return new OperationResult(OperationResultType.ParamError, "指定账号的用户不存在。");

            if (info.Password != account.Password)
                return new OperationResult(OperationResultType.ParamError, "登录密码不正确。");

            if (account.IsLogin)
                return new OperationResult(OperationResultType.IsLogined, "用户重复登陆。");

            account.IsLogin = true;
            _accountRepository.Update(account);

            UserInfo ui = new UserInfo(account.Id, info.Account, info.IpAddress, account.Authority);
            _eventAggregator.GetEvent<AccountLoginEvent>().Publish(ui);

            return new OperationResult(OperationResultType.Success, "登录成功。", ui.ToString());
        }

        public OperationResult Logout(string userId)
        {
            PublicHelper.CheckArgument(userId, "User Id");

            Account account = Accounts.SingleOrDefault<Account>(a => a.Id == userId);
            if (account == null)
                return new OperationResult(OperationResultType.ParamError, "指定账号的用户不存在。");

            if (!account.IsLogin)
                return new OperationResult(OperationResultType.IllegalOperation, "非法操作。");

            account.IsLogin = false;
            _accountRepository.Update(account);

            _eventAggregator.GetEvent<AccountLogoutEvent>().Publish(userId);

            return new OperationResult(OperationResultType.Success, "退出成功。"); 
        }

        public override void Initialize()
        {
            
        }

        public override void Dispose()
        {
        }
    }
}
