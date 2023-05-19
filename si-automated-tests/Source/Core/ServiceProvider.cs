using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace si_automated_tests.Source.Core
{
    public class ServiceProvider 
    {
        protected DatabaseContext DatabaseContext;

        public ServiceProvider(DatabaseContext context)
        {
            this.DatabaseContext = context ?? throw new ArgumentNullException("context"); 
        }

        public List<T> FindList<T>(string query)
        {
            SqlCommand command = new SqlCommand(query, DatabaseContext.Connection);
            SqlDataReader readers = command.ExecuteReader();
            List<T> data = ObjectExtention.DataReaderMapToList<T>(readers);
            readers.Close();
            return data;
        }
    }
}
