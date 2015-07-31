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

namespace Lighter.Client.Infrastructure.Implement
{
    [Export(typeof(ILighterContext))]
    public class LighterContext : ILighterContext, IDisposable
    {
        private LighterContext()
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

        public void CreateMainService()
        {
            string serverIp = GetServerIp();
            int serverPort = -1;
            if (!Int32.TryParse(GetServerPort(), out serverPort))
                serverPort = 40000;

            var builder = new UriBuilder("net.tcp", serverIp, serverPort, ServiceFactory.MAIN_SERVICE_NAME);
            ILighterMainService mainService = ServiceFactory.CreateService<ILighterMainService>(builder.Uri);

            Lighter.MainService.Model.Client client = CreateNewClient();
            mainService.Connect(client);

            AddService(ServiceFactory.MAIN_SERVICE_NAME, mainService);
        }

        #endregion

        #region IConfigContext Ini File
        private string iniName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "client.ini");

        public string GetClientName()
        {
            return OperatorFile.GetIniFileString("client", "name", "订单管理系统(客户端)", iniName);
        }

        public string GetServerName()
        {
            return OperatorFile.GetIniFileString("server", "name", CommonUtility.GetHostName(), iniName);
        }

        public string GetServerIp()
        {
            return OperatorFile.GetIniFileString("server", "ip", CommonUtility.GetHostIP4vDotFormat(), iniName);
        }

        public string GetServerPort()
        {
            return OperatorFile.GetIniFileString("server", "port", "50000", iniName);
        }

        public bool WriteServerName(string name)
        {
            return OperatorFile.WriteIniFileString("server", "name", name, iniName);
        }

        public bool WriteServerIp(string ip)
        {
            return OperatorFile.WriteIniFileString("server", "ip", ip, iniName);
        }

        public bool WriteServerIp(int port)
        {
            return OperatorFile.WriteIniFileString("server", "port", port.ToString(), iniName);
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

        public bool CheckHasCommandAuthority(string commandId)
        {
            return (_account == null) ? false : _account.CheckHasCommandAuthority(commandId);
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            // Logout
            AccountLogout();

            // Disconnect main service
            DisconnectServer();

            // Close all service
            CloseAllService();
        }

        private void CloseAllService()
        {
            
        }

        private void DisconnectServer()
        {
            Lighter.MainService.Model.Client client = CreateNewClient();

            ILighterMainService mainService = FindService(ServiceFactory.MAIN_SERVICE_NAME) as ILighterMainService;
            mainService.Disconnect(client);
        }

        private Lighter.MainService.Model.Client CreateNewClient()
        {
            Lighter.MainService.Model.Client client = new MainService.Model.Client();
            client.Name = "Client";
            client.IP = CommonUtility.GetHostIP4vDotFormat();
            client.Time = DateTime.Now;

            return client;
        }

        private bool AccountLogout()
        {
            ILighterLoginService loginService = FindService(ServiceFactory.LOGIN_SERVICE_NAME) as ILighterLoginService;

            LoginInfo info = new LoginInfo(_account.Id, "", CommonUtility.GetHostIP4vDotFormat());
            OperationResult result = loginService.Logout(info);
            return result.ResultType == OperationResultType.Success;
        }

        #endregion
    }
}
