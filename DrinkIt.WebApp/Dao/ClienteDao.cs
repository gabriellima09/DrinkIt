using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DrinkIt.WebApp.Models;

namespace DrinkIt.WebApp.Dao
{
    public class ClienteDao : IDao<Cliente>
    {
        private StringBuilder Sql = new StringBuilder();

        public void Alterar(Cliente entidade)
        {
            Sql.Append("UPDATE CLIENTES SET");
            Sql.Append("Cpf = '" + entidade.Cpf +"', ");
            Sql.Append("DataNascimento = " + entidade.DataNascimento.ToString("yyyy-MM-dd HH:mm:ss") + "', ");
            Sql.Append("Email = '" + entidade.Email + "', ");
            Sql.Append("Genero = '" + entidade.Genero + "', ");
            Sql.Append("Telefone = '" + entidade.Telefone + "', ");
            Sql.Append("Nome = '" + entidade.Nome + "', ");
            Sql.Append("Login = '" + entidade.Login + "', ");
            Sql.Append("Senha = '" + entidade.Senha +"'");
            Sql.Append(" WHERE Id = " + entidade.Id);

            DbContext.ExecuteQuery(Sql.ToString());
        }

        public void Cadastrar(Cliente entidade)
        {
            Sql.Append("INSERT INTO CLIENTES (");
            Sql.Append("Cpf, ");
            Sql.Append("DataNascimento, ");
            Sql.Append("Email, ");
            Sql.Append("Genero, ");
            Sql.Append("Telefone, ");
            Sql.Append("Nome, ");
            Sql.Append("Login, ");
            Sql.Append("Senha ");
            Sql.Append(")");
            Sql.Append(" VALUES (");
            Sql.Append("'" + entidade.Cpf + "', ");
            Sql.Append("'" + entidade.DataNascimento.ToString("yyyy-MM-dd HH:mm:ss") + "', ");
            Sql.Append("'" + entidade.Email + "', ");
            Sql.Append("'" + entidade.Genero + "', ");
            Sql.Append("'" + entidade.Telefone + "', ");
            Sql.Append("'" + entidade.Nome + "', ");
            Sql.Append("'" + entidade.Login + "', ");
            Sql.Append("'" + entidade.Senha + "'");
            Sql.Append(");");

            DbContext.ExecuteQuery(Sql.ToString());

            int Id = 0;

            using (IDataReader reader = DbContext.ExecuteReader("SELECT MAX(id) as Id FROM Clientes"))
            {
                if (reader.Read())
                {
                    Id = Convert.ToInt32(reader["Id"]);
                }
            }
            
            entidade.Endereco.Cobranca = true;
            entidade.Endereco.Entrega = true;
            entidade.Endereco.ClienteId = Id;
            entidade.Cartao.ClienteId = Id;

            new EnderecoDao().Cadastrar(entidade.Endereco);
            new CartaoDao().Cadastrar(entidade.Cartao);
        }

        public Cliente ConsultarPorId(int id)
        {
            Cliente cliente = new Cliente();

            Sql.Append("SELECT * FROM CLIENTES WHERE Id = " + id);

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
            List<Cliente> clientes = new List<Cliente>();

            Sql.Append("SELECT * FROM CLIENTES");

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    clientes.Add(ObterEntidadeReader(reader));
                }
            }

            return clientes;
        }

        public void Excluir(int id)
        {
            Sql.Append("DELETE FROM CLIENTES WHERE Id = ");
            Sql.Append(id);
            Sql.Append(";");

            DbContext.ExecuteQuery(Sql.ToString());
        }

        public Cliente ObterEntidadeReader(IDataReader reader)
        {
            Cliente cliente = new Cliente
            {
                Id = Convert.ToInt32(reader["Id"]),
                Cpf = Convert.ToString(reader["Cpf"]),
                DataNascimento = Convert.ToDateTime(reader["DataNascimento"]),
                Email = Convert.ToString(reader["Email"]),
                Genero = Convert.ToString(reader["Genero"]),
                Telefone = Convert.ToString(reader["Telefone"]),
                Nome = Convert.ToString(reader["Nome"]),
                Login = Convert.ToString(reader["Login"]),
                Senha = Convert.ToString(reader["Senha"]),
                Endereco = new Endereco(),
                Enderecos = new List<Endereco>(),
                Cartao = new CartaoCredito(),
                Cartoes = new List<CartaoCredito>()
            };

            return cliente;
        }
    }
}