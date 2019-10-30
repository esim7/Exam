using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Service
{
    public class ConnectionInfo
    {
        public IConfigurationBuilder builder;
        public IConfigurationRoot configurationRoot { get; set; }
        public string providerName { get; set; }
        public ConnectionInfo()
        {
            builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false, true);
            configurationRoot = builder.Build();
            providerName = configurationRoot.GetSection("AppConfig").GetChildren().Single().Value;
        }

        public void ConnectToDb()
        {
            DbProviderFactories.RegisterFactory(providerName, SqlClientFactory.Instance);
        }
    }
}
