using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DrinkIt.WebApp.Cryptography;
using DrinkIt.WebApp.Models;

namespace DrinkIt.WebApp.Dao
{
    public class ClienteDao : IDao<Cliente>
    {
        private StringBuilder Sql = new StringBuilder();

        public void Alterar(Cliente entidade)
        {
            try
            {
                Sql.Append("UPDATE CLIENTES SET ");
                Sql.Append("Cpf = '" + entidade.Cpf + "', ");
                Sql.Append("DataNascimento = '" + entidade.DataNascimento.ToString("yyyy-MM-dd HH:mm:ss") + "', ");
                Sql.Append("Email = '" + entidade.Email + "', ");
                Sql.Append("Genero = '" + entidade.Genero + "', ");
                Sql.Append("Nome = '" + entidade.Nome + "', ");
                Sql.Append("Login = '" + entidade.Login + "'");
                Sql.Append(" WHERE Id = " + entidade.Id);

                DbContext.ExecuteQuery(Sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Cadastrar(Cliente entidade)
        {
            try
            {
                Sql.Append("INSERT INTO CLIENTES (");
                Sql.Append("Cpf, ");
                Sql.Append("DataNascimento, ");
                Sql.Append("Email, ");
                Sql.Append("Genero, ");
                Sql.Append("Nome, ");
                Sql.Append("Login, ");
                Sql.Append("Senha, ");
                Sql.Append("Status ");
                Sql.Append(")");
                Sql.Append(" VALUES (");
                Sql.Append("'" + entidade.Cpf + "', ");
                Sql.Append("'" + entidade.DataNascimento.ToString("yyyy-MM-dd HH:mm:ss") + "', ");
                Sql.Append("'" + entidade.Email + "', ");
                Sql.Append("'" + entidade.Genero + "', ");
                Sql.Append("'" + entidade.Nome + "', ");
                Sql.Append("'" + entidade.Login + "', ");
                Sql.Append("'" + Criptografia.RetornarMD5(entidade.Senha) + "',");
                Sql.Append("1");//Status = ATIVO
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
                entidade.Cartao.Preferencial = true;

                new EnderecoDao().Cadastrar(entidade.Endereco);
                new CartaoDao().Cadastrar(entidade.Cartao);

                Sql.Clear();

                foreach (var item in entidade.Telefones)
                {
                    Sql.Append("INSERT INTO TELEFONES (");
                    Sql.Append("IdUsuario, ");
                    Sql.Append("DDD, ");
                    Sql.Append("Numero, ");
                    Sql.Append("IdTipoTelefone");
                    Sql.Append(")");
                    Sql.Append("VALUES (");
                    Sql.Append(Id + ", ");
                    Sql.Append(item.DDD + ",'");
                    Sql.Append(item.Numero + "',");
                    Sql.Append(item.IdTipo);
                    Sql.Append(");");

                    DbContext.ExecuteQuery(Sql.ToString());
                    Sql.Clear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


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
            try
            {
                Cliente cliente = new Cliente();
                int statusCliente;

                Sql.Append("SELECT * FROM CLIENTES WHERE Id = " + id);

                using (var reader = DbContext.ExecuteReader(Sql.ToString()))
                {
                    if (reader.Read())
                    {
                        cliente = ObterEntidadeReader(reader);
                    }
                }

                if (cliente.Status)
                {
                    statusCliente = 0;
                }
                else
                {
                    statusCliente = 1;
                }

                Sql.Clear();


                Sql.Append("UPDATE CLIENTES SET STATUS = " + statusCliente + " WHERE Id = " + id + ";");

                DbContext.ExecuteQuery(Sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

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
                Nome = Convert.ToString(reader["Nome"]),
                Login = Convert.ToString(reader["Login"]),
                Senha = Convert.ToString(reader["Senha"]),
                Status = Convert.ToBoolean(reader["Status"]),
                Endereco = new Endereco(),
                Enderecos = new List<Endereco>(),
                Cartao = new CartaoCredito(),
                Cartoes = new List<CartaoCredito>()
            };

            return cliente;
        }

        public List<Cliente> ConsultarComFiltro(string textoBusca)
        {
            List<Cliente> clientes = new List<Cliente>();

            Sql.Append("SELECT * FROM CLIENTES WHERE UPPER(LTRIM(RTRIM(NOME))) LIKE '%" + textoBusca.ToUpper().Trim() + "%'");

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    clientes.Add(ObterEntidadeReader(reader));
                }
            }

            return clientes;
        }
        
        public bool VerificarSenhaAtual(int idCliente, string senha)
        {
            bool existe = false;

            Sql.Append("SELECT (SELECT COUNT(*) FROM Clientes ");
            Sql.Append("WHERE ");
            Sql.Append("ID = " + idCliente);
            Sql.Append(" AND Senha LIKE '%" + Criptografia.RetornarMD5(senha) + "%') AS 'Exists'");

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    existe = Convert.ToInt32(reader["Exists"]) > 0;
                }
            }

            return existe;
        }

        public int GetRankingCliente(int IdCliente)
        {
            try
            {
                int ranking = 0;
                Sql.Clear();
                Sql.Append("SELECT Ranking FROM (SELECT RANK() OVER(ORDER BY SUM(VALORTOTAL) DESC) AS Ranking, ");
                Sql.Append("ClienteId, SUM(VALORTOTAL) SOMA FROM PEDIDOS GROUP BY ClienteId) Q ");
                Sql.Append("WHERE Q.ClienteId = " + IdCliente);

                using (var reader = DbContext.ExecuteReader(Sql.ToString()))
                {
                    if (reader.Read())
                    {
                        ranking = Convert.ToInt32(reader["Ranking"]);
                    }
                }

                return ranking;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}