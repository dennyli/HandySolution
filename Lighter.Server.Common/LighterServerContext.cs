using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Lighter.ServerEvents;
using Microsoft.Practices.Prism.ViewModel;

namespace Lighter.Server.Common
{
    [Export(typeof(ILighterServerContext))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class LighterServerContext : NotificationObject, ILighterServerContext
    {
        #region ILighterServerContext 成员

        private ObservableCollection<UserInfo> _loginedAccounts = new ObservableCollection<UserInfo>();

        public ObservableCollection<UserInfo> LoginedAccounts
        {
            get { return _loginedAccounts; }
        }

        public void AddAccount(UserInfo info)
        {
            if (FindUserInfo(info.Name) != null)
                return;

            _loginedAccounts.Add(info);
            RaisePropertyChanged("LoginedAccounts");
        }

        public void RemoveAccount(UserInfo info)
        {
            if (FindUserInfo(info.Id) == null)
                return;

            _loginedAccounts.Remove(info);
            RaisePropertyChanged("LoginedAccounts");
        }

        public void RemoveAccount(string userId)
        {
            UserInfo info = FindUserInfo(userId);
            if (info == null)
                return;

            _loginedAccounts.Remove(info);
            RaisePropertyChanged("LoginedAccounts");
        }

        public bool IsAccountLogined(string userId)
        {
            return FindUserInfo(userId) != null;
        }

        private UserInfo FindUserInfo(string userId)
        {
            try
            {
                return _loginedAccounts.First<UserInfo>(u => u.Id == userId);
            }
            catch (InvalidOperationException ex)
            { }
            catch (ArgumentNullException ex)
            { }

            return null;
        }

        #endregion

    }
}
