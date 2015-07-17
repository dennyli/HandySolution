using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.BaseService.Interface;
using Utility;

using Lighter.LoginService.Data.AccountManager;

namespace Lighter.LoginService.Data
{
    public interface ILighterLoginDataService : ILighterService
    {
        IQueryable<Account> Accounts { get; }


        OperationResult Login(string userName, string userPwd);

        void Logout(string userName);
    }
}
