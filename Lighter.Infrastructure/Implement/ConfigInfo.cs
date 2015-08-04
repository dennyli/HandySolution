using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;

namespace Lighter.Client.Infrastructure.Implement
{
    internal class ConfigInfo
    {
        public ConfigInfo()
        {
            ClientName = "订单管理系统(客户端)";
            ServerName = CommonUtility.GetHostName();
            ServerIp = CommonUtility.GetHostIP4vDotFormat();
            ServerPort = "50000";
        }

        public string ClientName { get; set; }
        public string ServerName { get; set; }
        public string ServerIp { get; set; }
        public string ServerPort { get; set; }

        public void LoadFromFile(string fnIni)
        {
            ClientName = OperatorFile.GetIniFileString("client", "name", ClientName, fnIni);

            ServerName = OperatorFile.GetIniFileString("server", "name", ServerName, fnIni);
            ServerIp = OperatorFile.GetIniFileString("server", "ip", ServerIp, fnIni);
            ServerPort = OperatorFile.GetIniFileString("server", "port", "50000", fnIni);
        }

        public void SaveToFile(string fnIni)
        {
            OperatorFile.WriteIniFileString("server", "port", ClientName, fnIni);

            OperatorFile.WriteIniFileString("server", "name", ServerName, fnIni);
            OperatorFile.WriteIniFileString("server", "ip", ServerIp, fnIni);
            OperatorFile.WriteIniFileString("server", "port", ServerPort, fnIni);
        }
    }
}
