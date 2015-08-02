namespace Lighter.ServiceManager
{
    using System;
    using System.ComponentModel.Composition;
using Lighter.ServiceManager.TokenValidation;

    /// <summary>
    /// Allows the export of a hosted service.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true), MetadataAttribute]
    public class ExportServiceAttribute : ExportAttribute, IHostedServiceMetadata
    {
        #region Constructors
        /// <summary>
        /// Initialises a new instance of <see cref="ExportServiceAttribute" />.
        /// </summary>
        /// <param name="name">The name of the service.</param>
        /// <param name="serviceType">The service type.</param>
        public ExportServiceAttribute(string name, Type serviceType, Type contactType, int order = 1, TokenValidationMode mode = TokenValidationMode.Check)
            : base(typeof(IHostedService))
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is a required parameter.", "name");

            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            if (order < 0)
                throw new ArgumentException("Order is must great than zero.", "order");

            Name = name;
            ServiceType = serviceType;
            ContactType = contactType;
            Order = order;
            TokenValidationMode = mode;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the service type.
        /// </summary>
        public Type ServiceType { get; private set; }

        ///// <summary>
        ///// Gets the interface type.
        ///// </summary>
        public Type ContactType { get; private set; }

        /// <summary>
        /// Export order
        /// </summary>
        public int Order { get; private set; }

        public TokenValidationMode TokenValidationMode { get; private set; }
        #endregion
    }
}