using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DrinkIt.WebApp.Dao
{
    public class EnderecoDao : IDao<Endereco>
    {
        private StringBuilder Sql = new StringBuilder();

        public void Alterar(Endereco entidade)
        {
            ZerarFlags(entidade.ClienteId);

            Sql.Clear();

            Sql.Append("UPDATE ENDERECOS SET");
            Sql.Append(" Bairro = '" + entidade.Bairro + "', ");
            Sql.Append("CEP = '" + entidade.CEP + "', ");
            Sql.Append("Cidade = '" + entidade.Cidade + "', ");
            Sql.Append("Cobranca = " + (entidade.Cobranca ? 1 : 0) + ", ");
            Sql.Append("Entrega = " + (entidade.Entrega ? 1 : 0) + ", ");
            Sql.Append("Complemento = '" + entidade.Complemento + "', ");
            Sql.Append("Descricao = '" + entidade.Descricao + "', ");
            Sql.Append("Estado = '" + entidade.Estado + "', ");
            Sql.Append("Logradouro = '" + entidade.Logradouro + "', ");
            Sql.Append("Numero = '" + entidade.Numero + "' ");
            Sql.Append(" WHERE Id = " + entidade.Id);

            DbContext.ExecuteQuery(Sql.ToString());
        }

        public void Cadastrar(Endereco entidade)
        {
            ZerarFlags(entidade.ClienteId);

            Sql.Clear();

            Sql.Append("INSERT INTO ENDERECOS (");
            Sql.Append("Bairro, ");
            Sql.Append("CEP, ");
            Sql.Append("Cidade, ");
            Sql.Append("Cobranca, ");
            Sql.Append("Entrega, ");
            Sql.Append("Complemento, ");
            Sql.Append("Descricao, ");
            Sql.Append("Estado, ");
            Sql.Append("Logradouro, ");
            Sql.Append("ClienteId, ");
            Sql.Append("Numero ");
            Sql.Append(")");
            Sql.Append(" VALUES (");
            Sql.Append("'" + entidade.Bairro + "', ");
            Sql.Append("'" + entidade.CEP + "', ");
            Sql.Append("'" + entidade.Cidade + "', ");
            Sql.Append((entidade.Cobranca ? 1 : 0) + ", ");
            Sql.Append((entidade.Entrega ? 1 : 0) + ", ");
            Sql.Append("'" + entidade.Complemento + "', ");
            Sql.Append("'" + entidade.Descricao + "', ");
            Sql.Append("'" + entidade.Estado + "', ");
            Sql.Append("'" + entidade.Logradouro + "', ");
            Sql.Append("'" + entidade.ClienteId + "', ");
            Sql.Append("'" + entidade.Numero + "'");
            Sql.Append(");");

            DbContext.ExecuteQuery(Sql.ToString());
        }

        public Endereco ConsultarPorId(int id)
        {
            Endereco endereco = new Endereco();

            Sql.Append("SELECT * FROM ENDERECOS WHERE Id = " + id);

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    endereco = ObterEntidadeReader(reader);
                }
            }

            return endereco;
        }

        public List<Endereco> ConsultarTodos()
        {
            List<Endereco> enderecos = new List<Endereco>();

            Sql.Append("SELECT * FROM ENDERECOS");

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    enderecos.Add(ObterEntidadeReader(reader));
                }
            }

            return enderecos;
        }

        public void Excluir(int id)
        {
            Sql.Append("DELETE FROM ENDERECOS WHERE Id = ");
            Sql.Append(id);
            Sql.Append(";");

            DbContext.ExecuteQuery(Sql.ToString());
        }

        public Endereco ObterEntidadeReader(IDataReader reader)
        {
            Endereco endereco = new Endereco
            {
                Id = Convert.ToInt32(reader["Id"]),
                Bairro = Convert.ToString(reader["Bairro"]),
                CEP = Convert.ToString(reader["CEP"]),
                Cidade = Convert.ToString(reader["Cidade"]),
                ClienteId = Convert.ToInt32(reader["ClienteId"]),
                Cobranca = Convert.ToBoolean(reader["Cobranca"]),
                Entrega = Convert.ToBoolean(reader["Entrega"]),
                Complemento = Convert.ToString(reader["Complemento"]),
                Descricao = Convert.ToString(reader["Descricao"]),
                Estado = Convert.ToString(reader["Estado"]),
                Logradouro = Convert.ToString(reader["Logradouro"]),
                Numero = Convert.ToString(reader["Numero"])
            };

            return endereco;
        }

        private void ZerarFlags(int idCliente)
        {
            Sql.Clear();

            Sql.Append("UPDATE ENDERECOS SET");
            Sql.Append("Cobranca = 0, ");
            Sql.Append("Entrega = 0");
            Sql.Append(" WHERE ClienteId = " + idCliente);

            DbContext.ExecuteQuery(Sql.ToString());
        }
    }
}