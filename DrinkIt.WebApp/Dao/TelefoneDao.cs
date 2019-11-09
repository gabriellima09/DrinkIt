using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace DrinkIt.WebApp.Dao
{
    public class TelefoneDao : IDao<Telefone>, ITelefone
    {
        private StringBuilder Sql = new StringBuilder();

        public void Alterar(Telefone entidade)
        {
            try
            {
                Sql.Append("UPDATE TELEFONES SET ");
                Sql.Append("IdTipo = " + entidade.IdTipo + ", ");
                Sql.Append("Numero = '" + entidade.Numero.Replace("-", "") + "', ");
                Sql.Append("DDD = " + entidade.DDD);
                Sql.Append(" WHERE Id = " + entidade.Id);

                DbContext.ExecuteQuery(Sql.ToString());
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void Cadastrar(Telefone entidade)
        {
            try
            {
                Sql.Append("INSERT INTO TELEFONES (");
                Sql.Append("IdUsuario, ");
                Sql.Append("DDD, ");
                Sql.Append("Numero, ");
                Sql.Append("IdTipoTelefone");
                Sql.Append(")");
                Sql.Append("VALUES (");
                Sql.Append(entidade.Id + ", ");
                Sql.Append(entidade.DDD + ",'");
                Sql.Append(entidade.Numero.Replace("-", "") + "',");
                Sql.Append(entidade.IdTipo);
                Sql.Append(");");

                DbContext.ExecuteQuery(Sql.ToString());
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public List<Telefone> ConsultarPorCliente(int IdCliente)
        {
            List<Telefone> telefones = new List<Telefone>();

            Sql.Append("SELECT * FROM TELEFONES WHERE IdUsuario = " + IdCliente);

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    Telefone tel = new Telefone()
                    {
                        IdTipo = Convert.ToInt32(reader["IdTipoTelefone"]),
                        DDD = Convert.ToInt32(reader["DDD"]),
                        Numero = Convert.ToString(reader["Numero"])
                    };

                    switch(tel.IdTipo)
                    {
                        case 1:
                            tel.DescricaoTipo = "Residencial";
                            break;
                        case 2:
                            tel.DescricaoTipo = "Celular";
                            break;
                        case 3:
                            tel.DescricaoTipo = "Comercial";
                            break;
                    }
                    telefones.Add(tel);
                }
            }

            return telefones;
            
        }

        public Telefone ConsultarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Telefone> ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public Telefone ObterEntidadeReader(IDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}