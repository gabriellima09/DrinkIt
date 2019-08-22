using System.Collections.Generic;
using System.Data;
using System.Text;
using DrinkIt.WebApp.Models;

namespace DrinkIt.WebApp.Dao
{
    public class ClienteDao : IDao<Cliente>
    {
        private StringBuilder Sql;

        public void Alterar(Cliente entidade)
        {
            throw new System.NotImplementedException();
        }

        public void Cadastrar(Cliente entidade)
        {
            Sql.Append("INSERT INTO USUARIOS (");

            Sql.Append(")");
            Sql.Append("VALUES (");

            Sql.Append(");");

            DbContext.ExecuteQuery(Sql.ToString());
        }

        public Cliente ConsultarPorId(int id)
        {
            Cliente cliente = new Cliente();

            Sql.Append("SELECT * FROM USUARIOS WHERE Id = " + id);

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    cliente = ObterEntidadeReader(reader);
                }
            }

            return cliente;
        }

        public List<Cliente> ConsultarTodos()
        {
            throw new System.NotImplementedException();
        }

        public void Excluir(int id)
        {
            throw new System.NotImplementedException();
        }

        public Cliente ObterEntidadeReader(IDataReader reader)
        {
            throw new System.NotImplementedException();
        }
    }
}