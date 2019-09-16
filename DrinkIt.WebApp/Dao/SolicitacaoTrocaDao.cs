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

            Sql.Append("SELECT st.*, cli.Nome DescCliente FROM solicitacoestroca st join clientes cli on st.idcliente = cli.Id");

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
    }
}