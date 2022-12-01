using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Core
{
    public class DatabaseContext : IDisposable
    {
        public SqlConnection Connection { get; private set; }

        public DatabaseContext(string host, string dbName, string userId, string password)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = host;
            builder.InitialCatalog = dbName;
            builder.UserID = userId;
            builder.Password = password;
            Connection = new SqlConnection(builder.ConnectionString);
            Connection.Open();
        }

        public DatabaseContext()
        {
            IConfigurationRoot config = ConfigManager.InitConfiguration();
            var mSConfiguration = new MSConfiguration();
            config.Bind("ConnectionStrings", mSConfiguration);
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = mSConfiguration.Host;
            builder.InitialCatalog = mSConfiguration.DBName;
            builder.UserID = mSConfiguration.UserId;
            builder.Password = mSConfiguration.Password;
            Connection = new SqlConnection(builder.ConnectionString);
            Connection.Open();
        }
        public DatabaseContext(string host, string dbName)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = host;
            builder.InitialCatalog = dbName;
            builder.IntegratedSecurity = true;
            Connection = new SqlConnection(builder.ConnectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            Connection?.Dispose();
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
