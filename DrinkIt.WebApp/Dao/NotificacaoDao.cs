using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DrinkIt.WebApp.Dao
{
    public class NotificacaoDao : IDao<Notificacao>
    {
        private StringBuilder Sql = new StringBuilder();

        public void Cadastrar(Notificacao entidade)
        {
            Sql.Clear();

            Sql.Append("INSERT INTO Notificacoes (");
            Sql.Append("IdCliente, ");
            Sql.Append("Descricao ");
            Sql.Append(")");
            Sql.Append(" VALUES (");
            Sql.Append(entidade.IdCliente + ", ");
            Sql.Append("'" + entidade.Descricao + "'");
            Sql.Append(");");

            DbContext.ExecuteQuery(Sql.ToString());
        }

        public void Excluir(int id)
        {
            Sql.Clear();

            Sql.Append("DELETE FROM NOTIFICACOES WHERE Id = ");
            Sql.Append(id);
            Sql.Append(";");

            DbContext.ExecuteQuery(Sql.ToString());
        }

        public List<Notificacao> ConsultarPorCliente(int idCliente)
        {
            List<Notificacao> notificacoes = new List<Notificacao>();

            Sql.Append("SELECT * FROM Notificacoes where idCliente = " + idCliente);

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    notificacoes.Add(ObterEntidadeReader(reader));
                }
            }

            return notificacoes;
        }

        public Notificacao ObterEntidadeReader(IDataReader reader)
        {
            Notificacao notificacao = new Notificacao
            {
                Id = Convert.ToInt32(reader["Id"]),
                IdCliente = Convert.ToInt32(reader["IdCliente"]),
                Descricao = Convert.ToString(reader["Descricao"])
            };

            return notificacao;
        }

        public void Alterar(Notificacao entidade)
        {
            throw new NotImplementedException();
        }

        public Notificacao ConsultarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Notificacao> ConsultarTodos()
        {
            throw new NotImplementedException();
        }
    }
}