using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ADODBConnection.DAL
{
    public static class SQLConnectionFactory
    {
        //public static IDbConnection GetConnection()
        //{
        //    return GetConnection(ConfigurationManager.ConnectionString["GeoDataConnection"]);
        //}

        public static SqlConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
