using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DrinkIt.WebApp.Dao
{
    public class UsuarioDao : IDao<Usuario>, IUsuario
    {
        private StringBuilder Sql = new StringBuilder();

        public void Alterar(Usuario entidade)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Usuario entidade)
        {
            throw new NotImplementedException();
        }

        public Usuario ConsultarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public bool Login(string email, string senha)
        {
            bool existe = false;

            Sql.Append("SELECT (SELECT COUNT(*) FROM Clientes ");
            Sql.Append("WHERE ");
            Sql.Append("Email LIKE '%" + email + "%' ");
            Sql.Append("AND Senha LIKE '%" + senha + "%') AS 'Exists'");

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    existe = Convert.ToInt32(reader["Exists"]) > 0;
                }
            }

            return existe;
        }

        public Usuario ObterEntidadeReader(IDataReader reader)
        {
            Usuario usuario = new Usuario
            {
                Id = Convert.ToInt32(reader["Id"]),
                Email = Convert.ToString(reader["Email"]),
                Login = Convert.ToString(reader["Login"]),
                Senha = Convert.ToString(reader["Senha"])
            };

            return usuario;
        }

        public Usuario RecuperarUsuario(string email)
        {
            Sql.Clear();

            Usuario usuario = new Usuario();

            Sql.Append("SELECT TOP 1 * FROM Clientes WHERE EMAIL = '"+ email +"'");

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    usuario = ObterEntidadeReader(reader);
                }
            }

            return usuario;
        }
    }
}