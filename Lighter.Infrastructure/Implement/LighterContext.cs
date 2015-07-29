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

namespace Lighter.Client.Infrastructure.Implement
{
    [Export(typeof(ILighterContext))]
    public class LighterContext : ILighterContext
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

            var builder = new UriBuilder("net.tcp", serverIp, serverPort, "LighterMainService");
            ILighterMainService mainService = ServiceFactory<ILighterMainService>.CreateService(builder.Uri);

            AddService("MainService", mainService);
        }

        #endregion

        #region IConfig Ini File
        private string iniName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "client.ini");

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
    }
}
