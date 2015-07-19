﻿using System;
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
                return new OperationResult(OperationResultType.ParamError, "指定账号的用户不存在。");

            if (userPwd != account.Password)
                return new OperationResult(OperationResultType.ParamError, "登录密码不正确。");

            account.IsLogin = true;
            AccountRepository.Update(account);
            
            return new OperationResult(OperationResultType.Success, "登录成功。", account.Modules);
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
