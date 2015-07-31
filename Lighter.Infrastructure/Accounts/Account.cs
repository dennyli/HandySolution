using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.UserManagerService.Model;

namespace Lighter.Client.Infrastructure.Accounts
{
    public class Account : IAcount
    {
        public Account(AccountDTO dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            Authority = dto.Authority;
            ShortName = dto.ShortName;
        }

        #region Fields
        /// <summary>
        /// 账户唯一标示
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 账户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 账户权限
        /// </summary>
        public string Authority { get; set; }

        /// <summary>
        /// 账户名称缩写
        /// </summary>
        public string ShortName { get; set; }

        #endregion

        public bool CheckHasCommandAuthority(string commandId)
        {
            string authority = Authority;

#if USE_SEC
#endif

            if (!string.IsNullOrWhiteSpace(authority))
                return (authority.IndexOf(commandId) != -1);

            return false;
        }
    }
}
