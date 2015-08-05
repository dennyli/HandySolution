namespace Lighter.ServiceManager.Endpoints
{
    using System;
    using System.ServiceModel.Description;
    using Utility;

    /// <summary>
    /// Describes an endpoint for a service.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public abstract class EndpointAttribute : Attribute
    {
        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="EndpointAttribute"/>.
        /// </summary>
        protected EndpointAttribute(int defaultPort)
        {
            Port = defaultPort;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the binding configuration.
        /// </summary>
        public string BindingConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the Url path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        public int Port { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Creates an instance of a <see cref="ServiceEndpoint"/> that represents the endpoint.
        /// </summary>
        /// <param name="description">The contract description.</param>
        /// <param name="meta">The hosted service metadata.</param>
        /// <returns>An instance of <see cref="ServiceEndpoint"/></returns>
        internal abstract ServiceEndpoint CreateEndpoint(ContractDescription description, IHostedServiceMetadata meta);

        /// <summary>
        /// Creates the <see cref="Uri"/> for this endpoint.
        /// </summary>
        /// <param name="scheme">The Uri scheme.</param>
        /// <param name="meta">The hosted service metadata.</param>
        /// <returns>An instance of <see cref="Uri"/>.</returns>
        protected virtual Uri CreateUri(string scheme, IHostedServiceMetadata meta)
        {
            //string IP4v = "localhost";
            string IP4v = CommonUtility.GetHostIP4v();
            var builder = new UriBuilder(scheme, IP4v, Port, Path ?? meta.Name);
            return builder.Uri;
        }

        /// <summary>
        /// Make any endpoint-specific changes to the service description.
        /// </summary>
        /// <param name="description">The service description.</param>
        internal virtual void UpdateServiceDescription(ServiceDescription description) { }
        #endregion
    }
}