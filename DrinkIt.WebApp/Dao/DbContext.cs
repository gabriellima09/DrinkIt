using DrinkIt.WebApp.Logger;
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

                        LoggerManager.Instance.Logger.Debug($"Executando query ... Query: {query}");

                        cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        LoggerManager.Instance.Logger.Error(ex, $"Erro ao executar query: {query}");
                        throw new ExternalException("Erro ao executar ação do banco de dados. ", ex);
                    }
                    finally
                    {
                        LoggerManager.Instance.Logger.Info($"Fim de execução de query ... Query: {query}");
                        _SqlConnection.Close();
                        DisposeConnection();
                    }
                }
            }
        }

        public static IDataReader ExecuteReader(string query)
        {
            SqlConnection conn = _SqlConnection;
            SqlCommand cmd = new SqlCommand(query, conn) { CommandType = CommandType.Text };

            try
            {
                OpenConnection(conn);

                LoggerManager.Instance.Logger.Debug($"Executando query reader ... Query: {query}");

                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                LoggerManager.Instance.Logger.Error(ex, $"Erro ao executar query: {query}");

                throw new ExternalException("Erro ao executar ação do banco de dados. ", ex);
            }
            finally
            {
                LoggerManager.Instance.Logger.Info($"Fim de execução de query ... Query: {query}");
                _SqlConnection.Close();
                DisposeConnection();
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