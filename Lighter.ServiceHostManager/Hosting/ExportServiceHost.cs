namespace Lighter.ServiceManager.Hosting
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Description;

    using Endpoints;
    using System.Diagnostics;
    using Utility;
    using Lighter.ServiceManager.ErrorHandler;

    /// <summary>
    /// Defines a service host created from exported parts.
    /// </summary>
    public class ExportServiceHost : ServiceHost
    {
        #region Fields
        private static readonly Type HostedServiceType = typeof(IHostedService);
        private readonly Uri[] _baseAddresses;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="ExportServiceHost"/>.
        /// </summary>
        /// <param name="meta">The service host metadata.</param>
        /// <param name="baseAddresses">The collection of base addresses</param>
        public ExportServiceHost(IHostedServiceMetadata meta, Uri[] baseAddresses)
        {
            if (meta == null)
                throw new ArgumentNullException("meta");

            Meta = meta;

            _baseAddresses = (baseAddresses == null || baseAddresses.Length == 0)
                                 ? null
                                 : baseAddresses;
            InitializeDescription(new UriSchemeKeyedCollection(baseAddresses));
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get the service host metadata.
        /// </summary>
        public IHostedServiceMetadata Meta { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// 刷新服务的地址端口，自动关闭，关闭后自动打开
        /// </summary>
        /// <param name="port">地址端口</param>
        public bool UpdateAddressPort(int port)
        {
            bool bResult = true;

            CommunicationState state = this.State;
            if (this.State == CommunicationState.Opened)
                Close();

            try
            {
                IEnumerable<ContractDescription> cds = GetContracts(Meta.ServiceType);
                ServiceEndpoint se = this.Description.Endpoints.First<ServiceEndpoint>(e => cds.Count(cd => cd.ContractType == e.Contract.ContractType) > 0);
                string IP4v = CommonUtility.GetHostIP4vDotFormat();
                var builder = new UriBuilder("net.tcp", IP4v, port, Meta.Name);
                EndpointAddress addrNew = new EndpointAddress(builder.Uri);
                se.Address = addrNew;
            }
            catch(ArgumentNullException)
            {
                bResult = false;
            }
            catch(InvalidOperationException)
            {
                bResult = false;
            }

            if (state == CommunicationState.Opened)
                Open();

            return bResult;
        }

        public string GetServiceName()
        {
            //var attrs = (ExportServiceAttribute[])Meta.ServiceType.GetCustomAttributes(typeof(ExportServiceAttribute), true);
            //if ((attrs == null) || !attrs.Any())
            //    return string.Empty;

            //return attrs[0].ContractName;

            return Meta.Name;
        }

        public Uri[] GetAddresses()
        {
            if (Description.Endpoints.Any())
            {
                List<Uri> uris = new List<Uri>();
                foreach (var address in Description.Endpoints)
                    uris.Add(address.Address.Uri);

                return uris.ToArray<Uri>();
            }
            else
                return null;
        }

        /// <summary>
        /// Adds the base addresses to the service.
        /// </summary>
        /// <param name="endpoints">The endpoints.</param>
        private void AddBaseAddresses(IEnumerable<ServiceEndpoint> endpoints)
        {
            if (_baseAddresses == null)
            {
                var addresses = endpoints
                    .Select(se => se.Address.Uri)
                    .Distinct();

                foreach (Uri address in addresses)
                    AddBaseAddress(address);
            }
        }

        /// <summary>
        /// Creates a service description
        /// </summary>
        /// <param name="implementedContracts">[Out] The set of contracts.</param>
        protected override ServiceDescription CreateDescription(out IDictionary<string, ContractDescription> implementedContracts)
        {
            var sd = new ServiceDescription { ServiceType = Meta.ServiceType };

            implementedContracts = GetContracts(Meta.ServiceType)
                .ToDictionary(cd => cd.ConfigurationName, cd => cd);

            var endpointAttributes = GetEndpoints(Meta.ServiceType);

            foreach (var cd in implementedContracts.Values)
            {
                foreach (var endpoint in GetServiceEndpoints(endpointAttributes, Meta, cd))
                {
                    sd.Endpoints.Add(endpoint);
                }
            }

            var serviceBehaviour = EnsureServiceBehavior(sd);
            serviceBehaviour.InstanceContextMode = InstanceContextMode.PerSession;

            foreach (var endpointAttribute in endpointAttributes)
                endpointAttribute.UpdateServiceDescription(sd);

            AddBaseAddresses(sd.Endpoints);
            return sd;
        }

        /// <summary>
        /// Ensures the <see cref="ServiceDescription"/> has a service behaviour attribute specified.
        /// </summary>
        /// <param name="description">The service description.</param>
        /// <returns>An instance of <see cref="ServiceBehaviorAttribute"/>.</returns>
        private static ServiceBehaviorAttribute EnsureServiceBehavior(ServiceDescription description)
        {
            var attr = description.Behaviors.Find<ServiceBehaviorAttribute>();
            if (attr == null)
            {
                attr = new ServiceBehaviorAttribute();
                description.Behaviors.Insert(0, attr);
            }

            return attr;
        }

        /// <summary>
        /// Gets the set of <see cref="ContractDescription"/> that describe the available contracts of the service.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <returns>An enumerable of <see cref="ContractDescription"/>.</returns>
        private static IEnumerable<ContractDescription> GetContracts(Type serviceType)
        {
            var collection = new ReflectedContractCollection();
            //Type[] types = serviceType.GetInterfaces();
            //foreach (var contract in types.Where(t => t != HostedServiceType))
            //{
            //    if (!collection.Contains(contract))
            //    {
            //        try
            //        {
            //            var cd = ContractDescription.GetContract(contract, serviceType);
            //            collection.Add(cd);
            //        }
            //        catch (InvalidOperationException)
            //        {
            //            Debug.WriteLine("Warning: Contract " + contract + " can't found contract description in " + serviceType);
            //        }

            //        //foreach (var icd in cd.GetInheritedContracts())
            //        //{
            //        //    if (!collection.Contains(icd.ContractType))
            //        //        collection.Add(icd);
            //        //}
            //    }
            //}

            var attrs = (ExportServiceAttribute[])serviceType.GetCustomAttributes(typeof(ExportServiceAttribute), true);
            if (!attrs.Any())
                throw new InvalidOperationException("ExportServiceAttribute must be defined!");

            foreach (var contract in attrs)
            {
                if (!collection.Contains(contract.ContactType))
                {
                    try
                    {
                        var cd = ContractDescription.GetContract(contract.ContactType, serviceType);
                        collection.Add(cd);
                    }
                    catch (InvalidOperationException)
                    {
                        Debug.WriteLine("Warning: Contract " + contract + " can't found contract description in " + serviceType);
                    }
                }
            }

            return collection;
        }

        /// <summary>
        /// Gets all <see cref="EndpointAttribute"/> which describe serviceable Uris.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <returns>The set of <see cref="EndpointAttribute"/> instances.</returns>
        private static IEnumerable<EndpointAttribute> GetEndpoints(Type serviceType)
        {
            var attrs = (EndpointAttribute[])serviceType.GetCustomAttributes(typeof(EndpointAttribute), true);
            if (!attrs.Any())
                attrs = new[] { new TcpEndpointAttribute() };

            return attrs;
        }

        /// <summary>
        /// Gets the service endpoints for the service.
        /// </summary>
        /// <param name="attributes">The set of endpoint attributes.</param>
        /// <param name="meta">The service metadata.</param>
        /// <param name="description">The contract description</param>
        /// <returns>The set of endpoints.</returns>
        private static IEnumerable<ServiceEndpoint> GetServiceEndpoints(IEnumerable<EndpointAttribute> attributes, IHostedServiceMetadata meta, ContractDescription description)
        {
            return attributes.Select(a => a.CreateEndpoint(description, meta));
        }
        #endregion

        #region Types
        /// <summary>
        /// Represents a collection of type / contract description mappings.
        /// </summary>
        private class ReflectedContractCollection : KeyedCollection<Type, ContractDescription>
        {
            #region Constructor
            /// <summary>
            /// Initialises a new instance of <see cref="ReflectedContractCollection"/>
            /// </summary>
            public ReflectedContractCollection() : base(null, 4) { }
            #endregion

            #region Methods
            /// <summary>
            /// Gets the key for the specified contract description.
            /// </summary>
            /// <param name="item">The contract description.</param>
            /// <returns>The key type.</returns>
            protected override Type GetKeyForItem(ContractDescription item)
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                return item.ContractType;
            }
            #endregion
        }
        #endregion
    }
}