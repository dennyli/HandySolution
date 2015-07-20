using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;

namespace Lighter.Server.Infrastructure
{
    public class EFDbContextProviderBase : IEFDbContextProvider
    {
        protected string _providerName = null;
        protected string _serverName = null;
        protected string _dbName = null;

        public virtual string GetConnectionString()
        {
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();

            // Set the properties for the data source.
            sqlBuilder.DataSource = _serverName;
            sqlBuilder.InitialCatalog = _dbName;
            sqlBuilder.IntegratedSecurity = true;
            sqlBuilder.Pooling = true;

            // Build the SqlConnection connection string.
            string providerString = sqlBuilder.ToString();

            //// Initialize the EntityConnectionStringBuilder.
            //EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();

            ////Set the provider name.
            //entityBuilder.Provider = _providerName;

            //// Set the provider-specific connection string.
            //entityBuilder.ProviderConnectionString = providerString;
            ////entityBuilder.Name = "innerCnnStringProvider";

            //string cnnString = entityBuilder.ToString();
            //return cnnString;

            return providerString;
        }

        public void SetConnectionParameter(string providerName, string serverName, string dbName)
        {
            _providerName = providerName;
            _serverName = serverName;
            _dbName = dbName;
        }
    }
}
