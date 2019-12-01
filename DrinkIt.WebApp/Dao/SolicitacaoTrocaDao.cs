using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DrinkIt.WebApp.Dao
{
    public class SolicitacaoTrocaDao
    {
        private StringBuilder Sql = new StringBuilder();

        public List<SolicitacaoTroca> ConsultarTodos()
        {
            List<SolicitacaoTroca> lista = new List<SolicitacaoTroca>();

            Sql.Clear();

            Sql.Append("SELECT st.*, cli.Nome DescCliente FROM solicitacoestroca st join clientes cli on st.idcliente = cli.Id WHERE st.status = 0");

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    SolicitacaoTroca st = new SolicitacaoTroca
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Descricao = Convert.ToString(reader["Descricao"]),
                        Data = Convert.ToDateTime(reader["Data"]),
                        Status = Convert.ToInt32(reader["Status"]),
                        IdCliente = Convert.ToInt32(reader["IdCliente"]),
                        DescCliente = Convert.ToString(reader["DescCliente"]),
                        IdPedido = Convert.ToInt32(reader["IdPedido"])
                    };

                    lista.Add(st);
                }
            }

            return lista;
        }

        public void Cadastrar(int idCliente, int idPedido, string motivo, List<Bebida> Bebidas)
        {
            try
            {
                Sql.Append("INSERT INTO SOLICITACOESTROCA (DESCRICAO, STATUS, IDCLIENTE, IDPEDIDO, DATA) VALUES ('" + motivo + "', 0, " + idCliente + ", " + idPedido + ", '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "');");
                DbContext.ExecuteReader(Sql.ToString());

                Sql.Clear();

                int LastInsertID = 0;

                Sql.Append("SELECT MAX(ID) ID FROM SOLICITACOESTROCA");
                using (var reader = DbContext.ExecuteReader(Sql.ToString()))
                {
                    if (reader.Read())
                    {
                        LastInsertID = Convert.ToInt32(reader["ID"]);
                    }
                }

                foreach (var item in Bebidas)
                {
                    Sql.Clear();
                    Sql.Append("INSERT INTO SOLICITACOESITENS VALUES (" + LastInsertID + ", " + item.Id + ", " + item.Quantidade + ");");
                    DbContext.ExecuteReader(Sql.ToString());
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Reprovar(int IdSolicitacao, string Motivo)
        {
            Sql.Append("UPDATE SOLICITACOESTROCA SET STATUS = " + 2 + ", MotivoReprovacao = '"
                + Motivo + "' WHERE ID = " + IdSolicitacao + "");
            DbContext.ExecuteReader(Sql.ToString());
        }

        public void Aprovar(int IdSolicitacao, int IdCupom)
        {
            Sql.Append("UPDATE SOLICITACOESTROCA SET STATUS = 1, IDCUPOM = " + IdCupom + " WHERE ID = " + IdSolicitacao + ";");
            DbContext.ExecuteReader(Sql.ToString());
        }
    }
}