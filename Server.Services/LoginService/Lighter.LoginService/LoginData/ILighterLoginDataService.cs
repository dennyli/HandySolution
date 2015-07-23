using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.BaseService.Interface;
using Utility;

using Lighter.Data;

namespace Lighter.LoginService.LoginData
{
    public interface ILighterLoginDataService : ILighterService
    {
        IQueryable<Account> Accounts { get; }


        OperationResult Login(string userName, string userPwd);

        OperationResult Logout(string userName);
    }
}
