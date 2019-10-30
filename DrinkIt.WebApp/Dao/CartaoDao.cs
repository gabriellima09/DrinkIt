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
            if (entidade.Preferencial)
            {
                ZerarFlags(entidade.ClienteId);
            }

            Sql.Clear();

            Sql.Append("UPDATE Cartoes SET");
            Sql.Append(" Bandeira = " + (int)entidade.Bandeira + ", ");
            Sql.Append("ClienteId = " + entidade.ClienteId + ", ");
            Sql.Append("CodigoSeguranca = " + entidade.CodigoSeguranca + ", ");
            Sql.Append("NomeTitular = '" + entidade.NomeTitular + "', ");
            Sql.Append("Numero = '" + entidade.Numero + "', ");
            Sql.Append("MesValidade = '" + entidade.MesValidade + "', ");
            Sql.Append("AnoValidade = '" + entidade.AnoValidade + "', ");
            Sql.Append("Preferencial = " + (entidade.Preferencial ? 1 : 0));
            Sql.Append(" WHERE Id = " + entidade.Id);

            DbContext.ExecuteQuery(Sql.ToString());
        }

        public void Cadastrar(CartaoCredito entidade)
        {
            if (entidade.Preferencial)
            {
                ZerarFlags(entidade.ClienteId);
            }

            Sql.Clear();

            Sql.Append("INSERT INTO Cartoes (");
            Sql.Append("Bandeira, ");
            Sql.Append("ClienteId, ");
            Sql.Append("CodigoSeguranca, ");
            Sql.Append("NomeTitular, ");
            Sql.Append("Numero ,");
            Sql.Append("MesValidade,");
            Sql.Append("AnoValidade,");
            Sql.Append("Preferencial");
            Sql.Append(")");
            Sql.Append(" VALUES (");
            Sql.Append((int)entidade.Bandeira + ", ");
            Sql.Append(entidade.ClienteId + ", ");
            Sql.Append(entidade.CodigoSeguranca + ", ");
            Sql.Append("'" + entidade.NomeTitular + "', ");
            Sql.Append("'" + entidade.Numero + "', ");
            Sql.Append("'" + entidade.MesValidade + "', ");
            Sql.Append("'" + entidade.AnoValidade + "', ");
            Sql.Append(entidade.Preferencial ? 1 : 0);
            Sql.Append(");");

            DbContext.ExecuteQuery(Sql.ToString());
        }

        public CartaoCredito ConsultarPorId(int id)
        {
            CartaoCredito cartao = new CartaoCredito();

            Sql.Append("SELECT * FROM Cartoes WHERE Id = " + id);

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

            Sql.Append("SELECT * FROM Cartoes");

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
            Sql.Append("DELETE FROM Cartoes WHERE Id = ");
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
                Bandeira = (Bandeira)Convert.ToInt32(reader["Bandeira"]),
                CodigoSeguranca = Convert.ToInt32(reader["CodigoSeguranca"]),
                NomeTitular = Convert.ToString(reader["NomeTitular"]),
                Numero = Convert.ToString(reader["Numero"]),
                MesValidade = Convert.ToInt32(reader["MesValidade"]),
                AnoValidade = Convert.ToInt32(reader["AnoValidade"]),
                Preferencial = Convert.ToBoolean(reader["Preferencial"])
            };

            return cartao;
        }

        private void ZerarFlags(int idCliente)
        {
            Sql.Clear();

            Sql.Append("UPDATE Cartoes SET");
            Sql.Append(" Preferencial = 0 ");
            Sql.Append(" WHERE ClienteId = " + idCliente);

            DbContext.ExecuteQuery(Sql.ToString());
        }

        public int ObterUltimoIdInserido()
        {
            int ID = 0;
            Sql.Clear();
            Sql.Append("SELECT MAX(ID) FROM CARTOES;");
            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    ID = reader.GetInt32(0);
                }
            }

            return ID;
        }

        public List<CartaoCredito> ConsultarPorCliente(int IdCliente)
        {
            List<CartaoCredito> cartoes = new List<CartaoCredito>();

            Sql.Append("SELECT * FROM Cartoes WHERE CLIENTEID = " + IdCliente);

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    cartoes.Add(ObterEntidadeReader(reader));
                }
            }

            return cartoes;
        }
    }
}