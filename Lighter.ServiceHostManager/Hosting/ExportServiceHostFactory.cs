#define CHECK_ACCOUNT_LOGIN

namespace Lighter.ServiceManager.Hosting
{
    using System;
    using System.ComponentModel.Composition.Hosting;
    using Lighter.ServiceManager.TokenValidation;

    /// <summary>
    /// Creates instances of <see cref="ExportServiceHost"/>.
    /// </summary>
    internal static class ExportServiceHostFactory
    {
        #region Methods
        /// <summary>
        /// Creates an instance of <see cref="ExportServiceHost"/> using service metadata.
        /// </summary>
        /// <param name="container">The composition container.</param>
        /// <param name="meta">The metadata.</param>
        public static ExportServiceHost CreateExportServiceHost(CompositionContainer container, IHostedServiceMetadata meta)
        {
            if (container == null) throw new ArgumentNullException("container");
            if (meta == null) throw new ArgumentNullException("meta");

            var host = new ExportServiceHost(meta, new Uri[0]);
            host.Description.Behaviors.Add(new ExportServiceBehavior(container, meta.Name));
#if CHECK_ACCOUNT_LOGIN
            if (meta.TokenValidationMode == TokenValidationMode.Check)
                host.Description.Behaviors.Add(new TokenValidationServiceBehavior(container));
#endif

            return host;
        }
        #endregion
    }
}
