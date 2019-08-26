using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DrinkIt.WebApp.Models;

namespace DrinkIt.WebApp.Dao
{
    public class CartaoDao : IDao<CartaoCredito>
    {
        private StringBuilder Sql = new StringBuilder();

        public void Alterar(CartaoCredito entidade)
        {
            Sql.Append("UPDATE CARTAO SET");
            Sql.Append("Bandeira = '" + entidade.Bandeira + "', ");
            Sql.Append("ClienteId = " + entidade.ClienteId + ", ");
            Sql.Append("CodigoSeguranca = " + entidade.CodigoSeguranca + ", ");
            Sql.Append("NomeTitular = '" + entidade.NomeTitular + "', ");
            Sql.Append("Numero = '" + entidade.Numero + "'");
            Sql.Append(" WHERE Id = " + entidade.Id);

            DbContext.ExecuteQuery(Sql.ToString());
        }

        public void Cadastrar(CartaoCredito entidade)
        {
            Sql.Append("INSERT INTO CARTAO (");
            Sql.Append("Bandeira, ");
            Sql.Append("ClienteId, ");
            Sql.Append("CodigoSeguranca, ");
            Sql.Append("NomeTitular, ");
            Sql.Append("Numero, ");
            Sql.Append(")");
            Sql.Append(" VALUES (");
            Sql.Append("'" + entidade.Bandeira + "', ");
            Sql.Append(entidade.ClienteId + ", ");
            Sql.Append(entidade.CodigoSeguranca + ", ");
            Sql.Append("'" + entidade.NomeTitular + "', ");
            Sql.Append("'" + entidade.Numero + "'");
            Sql.Append(");");

            DbContext.ExecuteQuery(Sql.ToString());
        }

        public CartaoCredito ConsultarPorId(int id)
        {
            CartaoCredito cartao = new CartaoCredito();

            Sql.Append("SELECT * FROM CARTAO WHERE Id = " + id);

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    cartao = ObterEntidadeReader(reader);
                }
            }

            return cartao;
        }

        public List<CartaoCredito> ConsultarTodos()
        {
            List<CartaoCredito> cartoes = new List<CartaoCredito>();

            Sql.Append("SELECT * FROM CARTAO");

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    cartoes.Add(ObterEntidadeReader(reader));
                }
            }

            return cartoes;
        }

        public void Excluir(int id)
        {
            Sql.Append("DELETE FROM CARTAO WHERE Id = ");
            Sql.Append(id);
            Sql.Append(";");

            DbContext.ExecuteQuery(Sql.ToString());
        }

        public CartaoCredito ObterEntidadeReader(IDataReader reader)
        {
            CartaoCredito cartao = new CartaoCredito
            {
                Id = Convert.ToInt32(reader["Id"]),
                ClienteId = Convert.ToInt32(reader["ClienteId"]),
                Bandeira = Convert.ToString(reader["Bandeira"]),
                CodigoSeguranca = Convert.ToInt32(reader["CodigoSeguranca"]),
                NomeTitular = Convert.ToString(reader["NomeTitular"]),
                Numero = Convert.ToString(reader["Numero"])
            };

            return cartao;
        }
    }
}