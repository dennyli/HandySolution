using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.BaseService.Interface;
using System.ServiceModel;

namespace Lighter.Client.Infrastructure.Implement
{
    public class ServiceFactory<T>
    {
        public static T CreateService(Uri uri)
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
            T service = factory.CreateChannel();

            return service;
        }
    }
}
