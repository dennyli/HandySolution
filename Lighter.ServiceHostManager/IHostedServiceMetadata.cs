namespace Lighter.ServiceManager
{
    using System;
    using Lighter.ServiceManager.TokenValidation;

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
        /// Get the description of the service.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the service type.
        /// </summary>
        Type ServiceType { get; }

        /// <summary>
        /// Gets the Contact type.
        /// </summary>
        Type ContactType { get; }

        /// <summary>
        /// Export order
        /// </summary>
        int Order { get; }

        TokenValidationMode TokenValidationMode { get; }
        #endregion
    }
}