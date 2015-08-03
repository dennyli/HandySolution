using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.BaseService.Interface;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Lighter.Client.Infrastructure.TokenValidation;
using Lighter.Client.Infrastructure.Interface;

namespace Lighter.Client.Infrastructure.Implement
{
    public class ServiceFactory
    {
        public static string MAIN_SERVICE_NAME = "LighterMainService";
        public static string LOGIN_SERVICE_NAME = "LighterLoginService";

        public static T CreateService<T>(Uri uri, ILighterClientContext lighterContext = null)
        {
            EndpointAddress address = new EndpointAddress(uri);
            NetTcpBinding binding = new NetTcpBinding();
#if WindowsSecurityMode
            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
#else
            binding.Security.Mode = SecurityMode.None;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.None;
#endif
            ChannelFactory<T> factory = new ChannelFactory<T>(binding, address);
            if (lighterContext != null)
                factory.Endpoint.Behaviors.Add(new TokenValidationBehavior(lighterContext.GetCurrentAccount()));

            T service = factory.CreateChannel();
            
#if DEBUG
            // Timeout: 10 mins
            IContextChannel channel = service as IClientChannel;
            channel.OperationTimeout = new TimeSpan(0, 10, 0);
#endif

            return service;
        }
    }
}
