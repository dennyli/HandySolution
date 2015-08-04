using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Client.Module.Common.Commands;
using Lighter.BaseService.Interface;
using System.ServiceModel;
using Lighter.MainService.Interface;
using Utility;
using Lighter.Client.Infrastructure.Interface;
using System.ComponentModel.Composition;
using Lighter.Client.Infrastructure.Accounts;
using Lighter.UserManagerService.Model;
using Lighter.LoginService.Interface;
using Lighter.LoginService.Model;
using System.ServiceModel.Channels;

namespace Lighter.Client.Infrastructure.Implement
{
    [Export(typeof(ILighterClientContext))]
    public class LighterClientContext : ILighterClientContext, IDisposable
    {
        private LighterClientContext()
        {
        }

        #region ICommandContext Command infos
        /// <summary>
        /// For All Commands
        /// </summary>
        private ObservableCollection<CommandInfo> _allCommandInfos = new ObservableCollection<CommandInfo>();
        //private ObservableCollection<CommandInfo> AllCommandInfos
        //{
        //    get { return _allCommandInfos; }
        //}

        public void AddComandInfos(ObservableCollection<CommandInfo> commandInfos)
        {
            foreach (CommandInfo info in commandInfos)
                AddComandInfo(info);
        }

        public void AddComandInfo(CommandInfo commandInfo)
        {
            _allCommandInfos.Add(commandInfo);
        }

        public CommandInfo FindCommandInfoByName(string name)
        {
            IEnumerable<CommandInfo> cmds = _allCommandInfos.Where<CommandInfo>(t => t.Name == name);
            if (cmds.Count<CommandInfo>() > 0)
                return cmds.First<CommandInfo>();

            return null;
        }

        public IList<CommandInfo> CheckCommandInfosConflict(ObservableCollection<CommandInfo> commandInfos)
        {
            IList<CommandInfo> conflictCommands = new List<CommandInfo>();
            foreach (CommandInfo info in commandInfos)
            {
                if (!CheckCommandInfoConflict(info))
                    conflictCommands.Add(info);
            }

            return conflictCommands;
        }

        public bool CheckCommandInfoConflict(CommandInfo commandnfo)
        {
            return _allCommandInfos.Where<CommandInfo>(cmd => (cmd.Name == commandnfo.Name) || (cmd.Key.Key == commandnfo.Key.Key) || cmd.Text == commandnfo.Text).Count<CommandInfo>() > 0;
        }
        #endregion ICommandContext

        #region IServiceContext Services
        private Dictionary<string, ILighterService> _services = new Dictionary<string, ILighterService>();
        private Lighter.MainService.Model.Client _currentClient = null;

        public void AddService(string key, ILighterService service)
        {
            if (_services.ContainsKey(key))
                throw new InvalidOperationException("服务已存在, Key: " + key);

            _services.Add(key, service);
        }

        public ILighterService FindService(string key)
        {
            if (!_services.ContainsKey(key))
                return null;

            return _services[key];
        }

        public void RemoveService(string key)
        {
            if (_services.ContainsKey(key))
                _services.Remove(key);
        }

        private Lighter.MainService.Model.Client CreateNewClient()
        {
            Lighter.MainService.Model.Client client = new MainService.Model.Client();
            client.Name = "Client";
            client.IP = CommonUtility.GetHostIP4vDotFormat();
            client.Time = DateTime.Now;

            return client;
        }

        public Lighter.MainService.Model.Client GetCurrentClient()
        {
            if (_currentClient == null)
                _currentClient = CreateNewClient();

            return _currentClient;
        }

        public ILighterMainService GetMainService()
        {
            if (!_services.ContainsKey(ServiceFactory.MAIN_SERVICE_NAME))
                CreateMainService();

            return _services[ServiceFactory.MAIN_SERVICE_NAME] as ILighterMainService;
        }

        private void CreateMainService()
        {
            string serverIp = GetServerIp();
            int serverPort = -1;
            if (!Int32.TryParse(GetServerPort(), out serverPort))
                serverPort = 50000;

            var builder = new UriBuilder("net.tcp", serverIp, serverPort, ServiceFactory.MAIN_SERVICE_NAME);
            ILighterMainService mainService = ServiceFactory.CreateService<ILighterMainService>(builder.Uri);

            Lighter.MainService.Model.Client client = GetCurrentClient();
            mainService.Connect(client);

            AddService(ServiceFactory.MAIN_SERVICE_NAME, mainService);
        }

        public T CreateServiceByMainService<T>(string serviceName, InstanceContext contextCallback = null, bool bTokenValidation = true)
        {
            T service = (T)FindService(serviceName);
            if (service != null)
                return service;

            ILighterMainService mainService = GetMainService();
            bool bExist = mainService.ServiceIsExists(serviceName);
            if (!bExist)
            {
                throw new InvalidOperationException("服务端没有发现" + serviceName + "服务，无法创建" + serviceName + "服务!");
            }

            Uri[] uris = mainService.GetServiceAddress(serviceName);
            service = ServiceFactory.CreateService<T>(uris[0], contextCallback, bTokenValidation ? GetCurrentAccount() : null);

            AddService(serviceName, service as ILighterService);

            return service;
        }

        private void CloseAllService()
        {
            foreach(ILighterService service in _services.Values)
            {
                ((IChannel)service).Close();
            }

            _services.Clear();
        }

        private void DisconnectServer()
        {
            Lighter.MainService.Model.Client client = GetCurrentClient();

            ILighterMainService mainService = FindService(ServiceFactory.MAIN_SERVICE_NAME) as ILighterMainService;
            mainService.Disconnect(client);
        }
        #endregion

        #region IConfigContext Ini File
        private static string iniName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "client.ini");
        private ConfigInfo _configInfo = null;

        private void LoadConfig()
        {
            _configInfo = new ConfigInfo();
            _configInfo.LoadFromFile(iniName);
        }

        public string GetClientName()
        {
            if (_configInfo == null)
                LoadConfig();

            return _configInfo.ClientName;
        }

        public string GetServerName()
        {
            if (_configInfo == null)
                LoadConfig();

            return _configInfo.ServerName;
        }

        public string GetServerIp()
        {
            if (_configInfo == null)
                LoadConfig();

            return _configInfo.ServerIp;
        }

        public string GetServerPort()
        {
            if (_configInfo == null)
                LoadConfig();

            return _configInfo.ServerPort;
        }

        public void WriteServerName(string name)
        {
            if (_configInfo == null)
                LoadConfig();

            _configInfo.ServerName = name;
        }

        public void WriteServerIp(string ip)
        {
            if (_configInfo == null)
                LoadConfig();

            _configInfo.ServerIp = ip;
        }

        public void WriteServerPort(string port)
        {
            if (_configInfo == null)
                LoadConfig();

            _configInfo.ServerPort = port;
        }
        #endregion

        #region IAccountContext
        private Account _account = null;
        public void SetCurrentAccount(AccountDTO accountDto)
        {
            if (accountDto == null)
                _account = null;
            else
                _account = new Account(accountDto);
        }

        public Account GetCurrentAccount()
        {
            return _account;
        }

        public string GetCurrentAccountName()
        {
            return _account == null ? "" : _account.Name;
        }

        public bool CheckHasCommandAuthority(string commandId)
        {
            return (_account == null) ? false : _account.CheckHasCommandAuthority(commandId);
        }

        //public void AccountLogout()
        //{
        //    if (_account != null)
        //    {
        //        ILighterLoginService loginService = FindService(ServiceFactory.LOGIN_SERVICE_NAME) as ILighterLoginService;
        //        loginService.Logout(_account.Id);
        //    }
        //}
        #endregion

        #region IDisposable
        public void Dispose()
        {
            if (_configInfo == null)
                _configInfo.SaveToFile(iniName);

            // Logout
            //AccountLogout();

            // Disconnect main service
            DisconnectServer();

            // Close all service
            CloseAllService();
        }


        #endregion
    }
}
