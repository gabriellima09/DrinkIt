using System;
using System.Text;

namespace DrinkIt.WebApp.Dao
{
    public class ProcedimentoTrocaStatusDao
    {
        private StringBuilder Sql = new StringBuilder();

        public void TrocarStatus(int idPedido, int idStatus)
        {
            try
            {
                Sql.Append("INSERT INTO PEDIDOSHISTORICO (");
                Sql.Append("IdPedido, IdStatus, DataAtualizacao");
                Sql.Append(")");
                Sql.Append(" VALUES (");
                Sql.Append(idPedido + ", ");
                Sql.Append(idStatus + ", ");
                Sql.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                Sql.Append(");");

                DbContext.ExecuteQuery(Sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}