using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace DrinkIt.WebApp.Dao
{
    public class CupomDao : IDao<Cupom>
    {
        private StringBuilder Sql = new StringBuilder();

        public void Alterar(Cupom entidade)
        {
            try
            {
                Sql.Append("UPDATE CUPONS SET ");
                Sql.Append("Descricao = '" + entidade.Descricao + "', ");
                Sql.Append("IdTipo = " + entidade.IdTipo + ", ");
                Sql.Append("DtCriacao = '" + entidade.DataEmissao.ToString("yyyy-MM-dd HH:mm:ss") + "', ");
                Sql.Append("DtExpiracao = '" + entidade.DataExpiracao.ToString("yyyy-MM-dd HH:mm:ss") + "', ");
                Sql.Append("Ativo = " + (entidade.Status == true ? 1 : 0));
                Sql.Append(" WHERE Id = " + entidade.Id);

                DbContext.ExecuteQuery(Sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Cadastrar(Cupom entidade)
        {
            try
            {
                Sql.Append("INSERT INTO CUPONS (");
                Sql.Append("Descricao, IdTipo, DtCriacao, DtExpiracao, Ativo");
                Sql.Append(")");
                Sql.Append(" VALUES ('");
                Sql.Append(entidade.Descricao + "', ");
                Sql.Append(entidade.IdTipo + ", '");
                Sql.Append(entidade.DataEmissao.ToString("yyyy-MM-dd HH:mm:ss") + "', '");
                Sql.Append(entidade.DataExpiracao.ToString("yyyy-MM-dd HH:mm:ss") + "', ");
                Sql.Append((entidade.Status == true ? 1 : 0));
                Sql.Append(");");

                DbContext.ExecuteQuery(Sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Cupom ConsultarPorId(int id)
        {
            Cupom cupom = new Cupom();

            Sql.Append("SELECT * FROM CUPONS WHERE Id = " + id);

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    cupom.Id = Convert.ToInt32(reader["Id"]);
                    cupom.Descricao = Convert.ToString(reader["Descricao"]);
                    cupom.DataEmissao = Convert.ToDateTime(reader["DtCriacao"]);
                    cupom.DataExpiracao = Convert.ToDateTime(reader["DtExpiracao"]);
                    cupom.IdTipo = Convert.ToInt32(reader["IdTipo"]);
                    cupom.Status = Convert.ToBoolean(reader["Ativo"]);
                }
            }
            return cupom;
        }

        public List<Cupom> ConsultarTodos()
        {
            List<Cupom> cupons = new List<Cupom>();

            Sql.Append("SELECT * FROM CUPONS");

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    Cupom cupom = new Cupom
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Descricao = Convert.ToString(reader["Descricao"]),
                        DataEmissao = Convert.ToDateTime(reader["DtCriacao"]),
                        DataExpiracao = Convert.ToDateTime(reader["DtExpiracao"]),
                        IdTipo = Convert.ToInt32(reader["IdTipo"]),
                        Status = Convert.ToBoolean(reader["Ativo"])
                    };

                    cupons.Add(cupom);
                }

                return cupons;
            }
        }

        public void Excluir(int id)
        {
            try
            {
                Cupom cupom = new Cupom();
                int statusCupom;

                Sql.Append("SELECT * FROM CUPONS WHERE Id = " + id);

                using (var reader = DbContext.ExecuteReader(Sql.ToString()))
                {
                    if (reader.Read())
                    {
                        cupom.Id = Convert.ToInt32(reader["Id"]);
                        cupom.Descricao = Convert.ToString(reader["Descricao"]);
                        cupom.DataEmissao = Convert.ToDateTime(reader["DtCriacao"]);
                        cupom.DataExpiracao = Convert.ToDateTime(reader["DtExpiracao"]);
                        cupom.IdTipo = Convert.ToInt32(reader["IdTipo"]);
                        cupom.Status = Convert.ToBoolean(reader["Ativo"]);
                    }
                }

                if (cupom.Status)
                {
                    statusCupom = 0;
                }
                else
                {
                    statusCupom = 1;
                }

                Sql.Clear();


                Sql.Append("UPDATE CUPONS SET ATIVO = " + statusCupom + " WHERE Id = " + id + ";");

                DbContext.ExecuteQuery(Sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Cupom ObterEntidadeReader(IDataReader reader)
        {
            throw new NotImplementedException();
        }

        public List<Cupom> ConsultarPorCliente(int IdCliente)
        {
            try
            {
                List<Cupom> cupons = new List<Cupom>();

                Sql.Append("SELECT * FROM CUPONS WHERE Id IN (SELECT IDCUPOM FROM CUPONSCLIENTE WHERE IDCLIENTE = " + IdCliente + ")");

                using (var reader = DbContext.ExecuteReader(Sql.ToString()))
                {
                    while (reader.Read())
                    {
                        Cupom cupom = new Cupom
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Descricao = Convert.ToString(reader["Descricao"]),
                            IdTipo = Convert.ToInt32(reader["IdTipo"]),
                            DataEmissao = Convert.ToDateTime(reader["DtCriacao"]),
                            DataExpiracao = Convert.ToDateTime(reader["DtExpiracao"]),
                            Status = Convert.ToBoolean(reader["Ativo"])
                        };
                        cupons.Add(cupom);
                    }
                }

                return cupons;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}