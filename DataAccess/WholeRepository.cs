using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DataAccess
{
    public class WholeRepository : IDisposable
    {
        public readonly DbConnection connection;
        private readonly DbProviderFactory providerFactory;

        public AccountRepository Accounts { get; set; }
        
        public WholeRepository(string providerName, string connectionString)
        {
            providerFactory = DbProviderFactories.GetFactory(providerName);
            connection = providerFactory.CreateConnection();
            connection.ConnectionString = connectionString;

            Accounts = new AccountRepository(connection);         
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}
