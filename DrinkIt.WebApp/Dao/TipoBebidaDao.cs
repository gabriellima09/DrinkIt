using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Dao
{
    public class TipoBebidaDao : IDao<TipoBebida>, ITipoBebida
    {
        private StringBuilder Sql = new StringBuilder();

        public void Alterar(TipoBebida entidade)
        {
            try
            {
                Sql.Append("UPDATE TIPOBEBIDA SET ");
                Sql.Append("Descricao = '" + entidade.Descricao + "', ");
                Sql.Append("IdPrecificacao = " + entidade.IdGrupoPrecificacao);
                Sql.Append(" WHERE Id = " + entidade.Id);

                DbContext.ExecuteQuery(Sql.ToString());
                


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Cadastrar(TipoBebida entidade)
        {
            try
            {
                Sql.Append("INSERT INTO TIPOBEBIDA (");
                Sql.Append("Descricao, ");
                Sql.Append("IdPrecificacao");
                Sql.Append(")");
                Sql.Append("VALUES ('");
                Sql.Append(entidade.Descricao + "', ");
                Sql.Append(entidade.IdGrupoPrecificacao);
                Sql.Append(");");

                DbContext.ExecuteQuery(Sql.ToString());
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public TipoBebida ConsultarPorId(int id)
        {
            TipoBebida tipo = new TipoBebida();

            Sql.Append("SELECT * FROM TIPOBEBIDA WHERE Id = " + id);

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    tipo.Descricao = Convert.ToString(reader["Descricao"]);
                    tipo.IdGrupoPrecificacao = Convert.ToInt32(reader["IdPrecificacao"]);
                }
            }
            
            return tipo;
        }

        public List<TipoBebida> ConsultarTodos()
        {
            List<TipoBebida> lista = new List<TipoBebida>();
            Sql.Append("SELECT T.*, P.Descricao DescPrecificacao FROM TIPOBEBIDA T JOIN PRECIFICACAO P ON T.IdPrecificacao = P.Idgrupo;");
            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    TipoBebida tipo = new TipoBebida
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Descricao = Convert.ToString(reader["Descricao"]),
                        IdGrupoPrecificacao = Convert.ToInt32(reader["IdPrecificacao"]),
                        DescGrupoPrecificacao = Convert.ToString(reader["DescPrecificacao"])
                    };
                    lista.Add(tipo);
                }
            }
            return lista;
        }

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SelectListItem> GetGruposPrecificacao()
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Sql.Append("SELECT * FROM PRECIFICACAO");
            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    SelectListItem item = new SelectListItem
                    {
                        Value = Convert.ToString(reader["IdGrupo"]),
                        Text = Convert.ToString(reader["Descricao"])
                    };
                    Items.Add(item);
                }
            }
            return Items;
        }

        public TipoBebida ObterEntidadeReader(IDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}