using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace DrinkIt.WebApp.Dao
{
    public sealed class DbContext : IDisposable
    {
        private static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DrinkItConnectionString"].ConnectionString;
            }
        }

        private static SqlConnection _SqlConnection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }
        }

        public static void ExecuteQuery(string query)
        {
            using (SqlConnection conn = _SqlConnection)
            {
                using (SqlCommand cmd = new SqlCommand(query, conn) { CommandType = CommandType.Text })
                {
                    try
                    {
                        OpenConnection(conn);

                        cmd.ExecuteScalar();
                    }
                    catch (ExternalException ex)
                    {
                        throw new ExternalException("Erro ao executar ação do banco de dados. ", ex);
                    }
                    finally
                    {
                        DisposeConnection();
                    }
                }
            }
        }

        public static IDataReader ExecuteReader(string Query)
        {
            SqlConnection conn = _SqlConnection;
            SqlCommand cmd = new SqlCommand(Query, conn) { CommandType = CommandType.Text };

            try
            {
                OpenConnection(conn);

                return cmd.ExecuteReader();
            }
            catch (ExternalException ex)
            {
                throw new ExternalException("Erro ao executar ação do banco de dados. ", ex);
            }
        }

        private static void OpenConnection(SqlConnection conn)
        {
            conn.Open();
        }

        public void Dispose()
        {
            DisposeConnection();
            GC.SuppressFinalize(this);
        }

        private static void DisposeConnection()
        {
            if (_SqlConnection.State != ConnectionState.Closed)
            {
                _SqlConnection.Close();
                _SqlConnection.Dispose();
            }
        }
    }
}