using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Core
{
    public class DatabaseContext : IDisposable
    {
        public SqlConnection Conection { get; private set; }

        public DatabaseContext(string dataSource, string initCatalog, string userId, string password)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = dataSource;
            builder.InitialCatalog = initCatalog;
            builder.UserID = userId;
            builder.Password = password;
            Conection = new SqlConnection(builder.ConnectionString);
        }

        public DatabaseContext()
        {
            IConfigurationRoot config = ConfigManager.InitConfiguration();
            var mSConfiguration = new MSConfiguration();
            config.Bind("ConnectionStrings", mSConfiguration);
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = mSConfiguration.Host;
            builder.InitialCatalog = WebUrl.GetDBName();
            builder.UserID = mSConfiguration.UserId;
            builder.Password = mSConfiguration.Password;
            Conection = new SqlConnection(builder.ConnectionString);
            Conection.Open();
        }

        public void Dispose()
        {
            Conection?.Dispose();
        }
    }

    internal class MSConfiguration
    {
        public string Host { get; set; }
        public string DBName { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
