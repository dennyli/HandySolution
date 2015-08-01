using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Lighter.ServerEvents;

namespace Lighter.Server.Common
{
    public interface ILoginedAccountContext
    {
        /// <summary>
        /// 已登陆的账户列表
        /// </summary>
        ObservableCollection<UserInfo> LoginedAccounts { get; }

        /// <summary>
        /// 添加一个已登陆的账户
        /// </summary>
        /// <param name="info">已登陆的账户</param>
        void AddAccount(UserInfo info);

        /// <summary>
        /// 删除一个已登陆的账户， 账户退出
        /// </summary>
        /// <param name="info">退出账户</param>
        void RemoveAccount(UserInfo info);
        /// <summary>
        /// 删除一个已登陆的账户， 账户退出
        /// </summary>
        /// <param name="userName">退出账户名</param>
        void RemoveAccount(string userName);

        /// <summary>
        /// 判断账户是否已登陆
        /// </summary>
        /// <param name="userName">账户名</param>
        /// <returns>判断结果，TRUE表示账户已登陆，FALSE表示账户未登陆</returns>
        bool IsAccountLogined(string userName);
    }
}
