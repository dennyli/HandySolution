namespace Lighter.ServiceManager
{
    using System;

    /// <summary>
    /// Defines the required contract for implementing hosted service metadata.
    /// </summary>
    public interface IHostedServiceMetadata
    {
        #region Properties
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the service type.
        /// </summary>
        Type ServiceType { get; }

        ///// <summary>
        ///// Gets the interface type.
        ///// </summary>
        //Type InterfaceType { get; }

        /// <summary>
        /// Export order
        /// </summary>
        int Order { get; }
        #endregion
    }
}