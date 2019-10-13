using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace DrinkIt.WebApp.Dao
{
    public class TipoBebidaDao : IDao<TipoBebida>
    {
        private StringBuilder Sql = new StringBuilder();

        public void Alterar(TipoBebida entidade)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(TipoBebida entidade)
        {
            throw new NotImplementedException();
        }

        public TipoBebida ConsultarPorId(int id)
        {
            throw new NotImplementedException();
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

        public TipoBebida ObterEntidadeReader(IDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}