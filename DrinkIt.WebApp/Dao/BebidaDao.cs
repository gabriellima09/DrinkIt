using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DrinkIt.WebApp.Models;

namespace DrinkIt.WebApp.Dao
{
    public class BebidaDao : IDao<Bebida>
    {
        private StringBuilder Sql = new StringBuilder();

        public void Alterar(Bebida entidade)
        {
            Sql.Append("UPDATE BEBIDAS SET");
            Sql.Append(" Nome = '" + entidade.Nome + "', ");
            Sql.Append("Descricao = '" + entidade.Descricao + "', ");
            Sql.Append("TipoBebida = '" + entidade.TipoBebida.Id + "', ");
            Sql.Append("Marca = '" + entidade.Marca + "', ");
            Sql.Append("Valor = " + entidade.Valor + ", ");
            Sql.Append("Volume = '" + entidade.Volume + "', ");
            Sql.Append("Peso = '" + entidade.Peso + "', ");
            Sql.Append("Sabor = '" + entidade.Sabor + "', ");
            Sql.Append("Lote = '" + entidade.Lote + "', ");
            Sql.Append("DataFabricacao = '" + entidade.DataFabricacao.ToString("yyyy-MM-dd HH:mm:ss") + "', ");
            Sql.Append("DataValidade = '" + entidade.DataValidade.ToString("yyyy-MM-dd HH:mm:ss") + "', ");
            Sql.Append("Fabricante = '" + entidade.Fabricante + "', ");
            Sql.Append("Embalagem = '" + entidade.Embalagem + "', ");
            Sql.Append("CodigoBarras = '" + entidade.CodigoBarras + "', ");
            Sql.Append("Alcoolico = '" + entidade.Alcoolico + "', ");
            Sql.Append("Teor = '" + entidade.Teor + "', ");
            Sql.Append("Gaseificada = '" + entidade.Gaseificada + "', ");
            Sql.Append("ContemGluten = '" + entidade.ContemGluten + "', ");
            Sql.Append("DicaConservacao = '" + entidade.DicaConservacao + "', ");
            Sql.Append("Status = '" + entidade.Status + "', ");            
            Sql.Append("CaminhoImagem = '" + entidade.CaminhoImagem + "' ");            
            Sql.Append(" WHERE Id = " + entidade.Id);

            DbContext.ExecuteQuery(Sql.ToString());
        }

        public void Cadastrar(Bebida entidade)
        {
            Sql.Append("INSERT INTO BEBIDAS (");
            Sql.Append("Nome, ");
            Sql.Append("Descricao, ");
            Sql.Append("TipoBebida, ");
            Sql.Append("Marca, ");
            Sql.Append("Valor, ");
            Sql.Append("Volume, ");
            Sql.Append("Peso, ");
            Sql.Append("Sabor,  ");
            Sql.Append("Lote, ");
            Sql.Append("DataFabricacao, ");
            Sql.Append("DataValidade, ");
            Sql.Append("Fabricante, ");
            Sql.Append("Embalagem, ");
            Sql.Append("CodigoBarras, ");
            Sql.Append("Alcoolico, ");
            Sql.Append("Teor, ");
            Sql.Append("Gaseificada, ");
            Sql.Append("ContemGluten, ");
            Sql.Append("DicaConservacao, ");
            Sql.Append("Status, ");
            Sql.Append("CaminhoImagem, ");
            Sql.Append(")");
            Sql.Append("VALUES ('");
            Sql.Append(entidade.Nome + "', '");
            Sql.Append(entidade.Descricao + "', '");
            Sql.Append(entidade.TipoBebida + "', '");
            Sql.Append(entidade.Marca + "', ");
            Sql.Append(entidade.Valor + ", '");
            Sql.Append(entidade.Volume + "', '");
            Sql.Append(entidade.Peso + "', '");
            Sql.Append(entidade.Sabor + "', '");
            Sql.Append(entidade.Lote + "', '");
            Sql.Append(entidade.DataFabricacao.ToString("yyyy-MM-dd HH:mm:ss") + "', '");
            Sql.Append(entidade.DataValidade.ToString("yyyy-MM-dd HH:mm:ss") + "', '");
            Sql.Append(entidade.Fabricante + "', '");
            Sql.Append(entidade.Embalagem + "', '");
            Sql.Append(entidade.CodigoBarras + "', '");
            Sql.Append((entidade.Alcoolico == true ? 1 : 0) + "', '");
            Sql.Append(entidade.Teor + "', '");
            Sql.Append((entidade.Gaseificada == true ? 1 : 0) + "', '");
            Sql.Append((entidade.ContemGluten == true ? 1 : 0) + "', '");
            Sql.Append(entidade.DicaConservacao + "', '");
            Sql.Append(entidade.Status + "', '");
            Sql.Append(entidade.CaminhoImagem);
            Sql.Append("');");

            DbContext.ExecuteQuery(Sql.ToString());
        }

        public Bebida ConsultarPorId(int id)
        {
            Bebida bebida = new Bebida();

            Sql.Append("SELECT * FROM BEBIDAS WHERE Id = " + id);

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    bebida = ObterEntidadeReader(reader);
                }
            }

            return bebida;
        }

        public List<Bebida> ConsultarTodos()
        {
            List<Bebida> bebidas = new List<Bebida>();

            Sql.Append("SELECT * FROM BEBIDAS");

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    bebidas.Add(ObterEntidadeReader(reader));
                }
            }

            return bebidas;
        }

        public void Excluir(int id)
        {
            Sql.Append("DELETE FROM BEBIDAS WHERE Id = ");
            Sql.Append(id);
            Sql.Append(";");

            DbContext.ExecuteQuery(Sql.ToString());
        }

        public Bebida ObterEntidadeReader(IDataReader reader)
        {
            Bebida bebida = new Bebida
            {
                Id = Convert.ToInt32(reader["Id"]),
                Nome = Convert.ToString(reader["Nome"]),
                Descricao = Convert.ToString(reader["Descricao"]),
                TipoBebida = new TipoBebida
                {
                    Id = Convert.ToInt32(reader["TipoBebida"])
                },
                Marca = Convert.ToString(reader["Marca"]),
                Valor = Convert.ToDecimal(reader["Valor"]),
                Volume = Convert.ToString(reader["Volume"]),
                Peso = Convert.ToString(reader["Peso"]),
                Sabor = Convert.ToString(reader["Sabor"]),
                Lote = Convert.ToString(reader["Lote"]),
                DataFabricacao = Convert.ToDateTime(reader["DataFabricacao"]),
                DataValidade = Convert.ToDateTime(reader["DataValidade"]),
                Fabricante = Convert.ToString(reader["Fabricante"]),
                CodigoBarras = Convert.ToString(reader["CodigoBarras"]),
                Embalagem = Convert.ToString(reader["Embalagem"]),
                Alcoolico = Convert.ToBoolean(reader["Alcoolico"]),
                Teor = Convert.ToString(reader["Teor"]),
                Gaseificada = Convert.ToBoolean(reader["Gaseificada"]),
                ContemGluten = Convert.ToBoolean(reader["ContemGluten"]),
                DicaConservacao = Convert.ToString(reader["DicaConservacao"]),
                Status = Convert.ToString(reader["Status"]),
                CaminhoImagem = Convert.ToString(reader["CaminhoImagem"])
            };
            return bebida;
        }
    }
}