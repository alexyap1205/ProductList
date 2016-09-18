using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoDataAccess
{
    internal class SQLDBConnectionHelper : IDBConnectionHelper
    {
        public SqlConnection CreateConnection()
        {
            string connectionString =
                @"";

            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch (SqlException sqlException)
            {
                connection = null;

                // To Do: Replace with logging mechanism
                Console.WriteLine(sqlException.ToString());
            }

            return connection;
        }

        public SqlDataReader CreateReader(string sql, SqlConnection connection)
        {
            if (String.IsNullOrEmpty(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = null;

            try
            {
                reader = command.ExecuteReader();

            }
            catch (SqlException sqlException)
            {
                reader = null;

                // To Do: Replace with logging mechanism
                Console.WriteLine(sqlException.ToString());
            }

            return reader;
        }

        public void DisposeReader(SqlDataReader reader)
        {
            if (reader != null)
            {
                reader.Close();
                reader.Dispose();
            }
        }

        public void DisposeConnection(SqlConnection connection)
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
