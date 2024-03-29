﻿using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
                Sql.Append("Valor = " + entidade.Valor.ToString(new CultureInfo("en-US")) + ", ");
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
                Sql.Append("Descricao, IdTipo, DtCriacao, DtExpiracao, Valor, Ativo");
                Sql.Append(")");
                Sql.Append(" VALUES ('");
                Sql.Append(entidade.Descricao + "', ");
                Sql.Append(entidade.IdTipo + ", '");
                Sql.Append(entidade.DataEmissao.ToString("yyyy-MM-dd HH:mm:ss") + "', '");
                Sql.Append(entidade.DataExpiracao.ToString("yyyy-MM-dd HH:mm:ss") + "', ");
                Sql.Append(entidade.Valor.ToString(new CultureInfo("en-US")) + ", ");
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
                    cupom.Valor = Convert.ToDecimal(reader["Valor"]);
                }
            }
            return cupom;
        }

        public List<Cupom> ConsultarTodos()
        {
            List<Cupom> cupons = new List<Cupom>();

            Sql.Clear();

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
                        Status = Convert.ToBoolean(reader["Ativo"]),
                        Valor = Convert.ToDecimal(reader["Valor"])
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
                        cupom.Valor = Convert.ToDecimal(reader["Valor"]);
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
                            Status = Convert.ToBoolean(reader["Ativo"]),
                            Valor = Convert.ToDecimal(reader["Valor"])
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

        public Dictionary<bool, Cupom> ValidarCupom(string cupom)
        {
            bool existe = false;
            Cupom cupomRetorno = new Cupom();
            Dictionary<bool, Cupom> retorno = new Dictionary<bool, Cupom>();

            Sql.Clear();

            Sql.Append("SELECT (SELECT COUNT(*) FROM Cupons WHERE Descricao = '" + cupom + "') AS 'Exists'");

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    existe = Convert.ToInt32(reader["Exists"]) > 0;
                }
            }

            if (existe)
            {
                cupomRetorno = ConsultarTodos().Where(x => x.Descricao.ToUpper().Equals(cupom.ToUpper())).FirstOrDefault();
                retorno.Add(existe, cupomRetorno);
            }

            return retorno;
        }

        public int ObterUltimoIdInserido()
        {
            int ID = 0;
            Sql.Clear();
            Sql.Append("SELECT MAX(ID) FROM cupons;");
            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    ID = reader.GetInt32(0);
                }
            }

            return ID;
        }

        public void InsereCupomParaCliente(int idCupom, int idCliente)
        {
            try
            {
                Sql.Clear();

                Sql.Append("INSERT INTO CUPONSCLIENTE (");
                Sql.Append("IdCupom, IdCliente");
                Sql.Append(")");
                Sql.Append(" VALUES (");
                Sql.Append(idCupom + ", ");
                Sql.Append(idCliente);
                Sql.Append(");");

                DbContext.ExecuteQuery(Sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}