using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace DrinkIt.WebApp.Dao
{
    public class PrecificacaoDao : IDao<GrupoPrecificacao>
    {
        private StringBuilder Sql = new StringBuilder();

        public void Alterar(GrupoPrecificacao entidade)
        {
            try
            {
                Sql.Append("UPDATE PRECIFICACAO SET ");
                Sql.Append("Descricao = '" + entidade.Descricao + "', ");
                Sql.Append("MargemLucro = " + entidade.MargemLucro.ToString(new CultureInfo("en-US")));
                Sql.Append(" WHERE IdGrupo = " + entidade.Id);

                DbContext.ExecuteQuery(Sql.ToString());
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Cadastrar(GrupoPrecificacao entidade)
        {
            try
            {

                Sql.Append("INSERT INTO PRECIFICACAO (");
                Sql.Append("Descricao, ");
                Sql.Append("MargemLucro ");
                Sql.Append(")");
                Sql.Append("VALUES ('");
                Sql.Append(entidade.Descricao + "', ");
                Sql.Append(entidade.MargemLucro.ToString(new CultureInfo("en-US")));
                Sql.Append(");");

                DbContext.ExecuteQuery(Sql.ToString());
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GrupoPrecificacao ConsultarPorId(int id)
        {
            GrupoPrecificacao grupo = new GrupoPrecificacao();

            Sql.Append("SELECT * FROM PRECIFICACAO WHERE IdGrupo = " + id);

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    grupo.Id = Convert.ToInt32(reader["IdGrupo"]);
                    grupo.Descricao = Convert.ToString(reader["Descricao"]);
                    grupo.MargemLucro = Convert.ToDecimal(reader["MargemLucro"]);
                }
            }

            return grupo;

        }

        public List<GrupoPrecificacao> ConsultarTodos()
        {
            List<GrupoPrecificacao> lista = new List<GrupoPrecificacao>();
            Sql.Append("SELECT * FROM PRECIFICACAO");
            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    GrupoPrecificacao grupo = new GrupoPrecificacao
                    {
                        Id = Convert.ToInt32(reader["IdGrupo"]),
                        Descricao = Convert.ToString(reader["Descricao"]),
                        MargemLucro = Convert.ToDecimal(reader["MargemLucro"])
                    };
                    lista.Add(grupo);
                }
            }
            return lista;
        }

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public GrupoPrecificacao ObterEntidadeReader(IDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}