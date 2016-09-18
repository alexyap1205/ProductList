using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoDataAccess
{
    internal interface IDBConnectionHelper
    {
        SqlConnection CreateConnection();

        SqlDataReader CreateReader(string sql, SqlConnection connection);

        void DisposeReader(SqlDataReader reader);

        void DisposeConnection(SqlConnection connection);
    }
}
