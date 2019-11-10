using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using DrinkIt.WebApp.Models;

namespace DrinkIt.WebApp.Dao
{
    public class BebidaDao : IDao<Bebida>, IBebida
    {
        private StringBuilder Sql = new StringBuilder();

        public void Alterar(Bebida entidade)
        {
            try
            {
                Sql.Append("UPDATE BEBIDAS SET ");
                Sql.Append("Nome = '" + entidade.Nome + "', ");
                Sql.Append("Descricao = '" + entidade.Descricao + "', ");
                Sql.Append("TipoBebida = " + entidade.TipoBebida.Id + ", ");
                Sql.Append("Marca = '" + entidade.Marca + "', ");
                Sql.Append("Valor = " + entidade.Valor.ToString(new CultureInfo("en-US")) + ", ");
                Sql.Append("Volume = '" + entidade.Volume + "', ");
                Sql.Append("Peso = '" + entidade.Peso + "', ");
                Sql.Append("Sabor = '" + entidade.Sabor + "', ");
                Sql.Append("Lote = '" + entidade.Lote + "', ");
                Sql.Append("DataFabricacao = '" + entidade.DataFabricacao.ToString("yyyy-MM-dd HH:mm:ss") + "', ");
                Sql.Append("DataValidade = '" + entidade.DataValidade.ToString("yyyy-MM-dd HH:mm:ss") + "', ");
                Sql.Append("Fabricante = '" + entidade.Fabricante + "', ");
                Sql.Append("Embalagem = '" + entidade.Embalagem + "', ");
                Sql.Append("CodigoBarras = '" + entidade.CodigoBarras + "', ");
                Sql.Append("Alcoolico = " + (entidade.Alcoolico == true ? 1 : 0) + ", ");
                Sql.Append("Teor = " + entidade.Teor.ToString(new CultureInfo("en-US")) + ", ");
                Sql.Append("Gaseificada = " + (entidade.Gaseificada == true ? 1 : 0) + ", ");
                Sql.Append("ContemGluten = " + (entidade.ContemGluten == true ? 1 : 0) + ", ");
                Sql.Append("DicaConservacao = '" + entidade.DicaConservacao + "', ");
                Sql.Append("Status = " + (entidade.Status == true ? 1 : 0) + ", ");
                Sql.Append("CaminhoImagem = '" + entidade.CaminhoImagem + "'");
                Sql.Append(" WHERE Id = " + entidade.Id);

                DbContext.ExecuteQuery(Sql.ToString());

                DeletarIngredientes(entidade.Id);

                Sql.Clear();

                foreach (var item in entidade.Ingredientes)
                {
                    Sql.Append("INSERT INTO INGREDIENTES (");
                    Sql.Append("BebidaId, ");
                    Sql.Append("Descricao ");
                    Sql.Append(")");
                    Sql.Append("VALUES (");
                    Sql.Append(entidade.Id + ", '");
                    Sql.Append(item.Descricao + "' ");
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

        public void Cadastrar(Bebida entidade)
        {
            Sql.Clear();

            try
            {
                int LastInsertID = 0;

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
                Sql.Append("CaminhoImagem");
                Sql.Append(")");
                Sql.Append("VALUES ('");
                Sql.Append(entidade.Nome + "', '");
                Sql.Append(entidade.Descricao + "', ");
                Sql.Append(entidade.TipoBebida.Id + ", '");
                Sql.Append(entidade.Marca + "', ");
                Sql.Append(entidade.Valor.ToString(new CultureInfo("en-US")) + ", '");
                Sql.Append(entidade.Volume + "', '");
                Sql.Append(entidade.Peso + "', '");
                Sql.Append(entidade.Sabor + "', '");
                Sql.Append(entidade.Lote + "', '");
                Sql.Append(entidade.DataFabricacao.ToString("yyyy-MM-dd HH:mm:ss") + "', '");
                Sql.Append(entidade.DataValidade.ToString("yyyy-MM-dd HH:mm:ss") + "', '");
                Sql.Append(entidade.Fabricante + "', '");
                Sql.Append(entidade.Embalagem + "', '");
                Sql.Append(entidade.CodigoBarras + "', ");
                Sql.Append((entidade.Alcoolico == true ? 1 : 0) + ", ");
                Sql.Append(entidade.Teor.ToString(new CultureInfo("en-US")) + ", ");
                Sql.Append((entidade.Gaseificada == true ? 1 : 0) + ", ");
                Sql.Append((entidade.ContemGluten == true ? 1 : 0) + ", '");
                Sql.Append(entidade.DicaConservacao + "', ");
                Sql.Append((entidade.Status == true ? 1 : 0) + ", '");
                Sql.Append(entidade.CaminhoImagem + "'");
                Sql.Append(");");

                DbContext.ExecuteQuery(Sql.ToString());

                LastInsertID = ObterUltimoIdInserido();

                Sql.Clear();

                foreach (var item in entidade.Ingredientes)
                {
                    Sql.Append("INSERT INTO INGREDIENTES (");
                    Sql.Append("BebidaId, ");
                    Sql.Append("Descricao ");
                    Sql.Append(")");
                    Sql.Append("VALUES (");
                    Sql.Append(LastInsertID + ", '");
                    Sql.Append(item.Descricao + "'");
                    Sql.Append(");");

                    DbContext.ExecuteQuery(Sql.ToString());
                    Sql.Clear();
                }

                InicializarEstoque(LastInsertID);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public Bebida ConsultarPorId(int id)
        {
            Bebida bebida = new Bebida();

            Sql.Clear();

            Sql.Append("SELECT B.*, P.MargemLucro FROM BEBIDAS B LEFT JOIN TIPOBEBIDA T ON B.Tipobebida = T.Id LEFT JOIN PRECIFICACAO P ON T.IdPrecificacao = P.IdGrupo WHERE B.Id = " + id);

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    bebida = ObterEntidadeReader(reader);
                    bebida.MargemLucro = Convert.ToDecimal(reader["MargemLucro"]);
                }
            }

            bebida.ValorVenda = Math.Round(bebida.Valor + (bebida.MargemLucro/100), 2);

            Sql.Clear();

            bebida.Ingredientes = GetIngredientes(bebida.Id);

            return bebida;
        }

        public List<Bebida> ConsultarTodos()
        {
            List<Bebida> bebidas = new List<Bebida>();

            Sql.Append("SELECT * FROM BEBIDAS;");

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
        {//O método EXLCUIR para a DAO de Bebidas troca o status atual de uma bebida.

            Bebida bebida = new Bebida();
            int statusBebida;

            Sql.Append("SELECT * FROM BEBIDAS WHERE Id = " + id);

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    bebida = ObterEntidadeReader(reader);
                }
            }

            if (bebida.Status)
            {
                statusBebida = 0;
            }
            else
            {
                statusBebida = 1;
            }

            Sql.Clear();


            Sql.Append("UPDATE BEBIDAS SET STATUS = " + statusBebida + " WHERE Id = " + id + ";");

            DbContext.ExecuteQuery(Sql.ToString());


        }

        //Sobrecarga, para registrar motivo
        public void GravarMotivoInativacao(int id, string motivo)
        {
            if (!string.IsNullOrEmpty(motivo))
            {
                Sql.Clear();

                Sql.Append("INSERT INTO INATIVACAOBEBIDAS (IDBEBIDA, DTINATIVACAO, MOTIVOINATIVACAO) VALUES (" + id + ", '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + motivo + "');");

                DbContext.ExecuteQuery(Sql.ToString());
            }

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
                Teor = Convert.ToDecimal(reader["Teor"]),
                Gaseificada = Convert.ToBoolean(reader["Gaseificada"]),
                ContemGluten = Convert.ToBoolean(reader["ContemGluten"]),
                DicaConservacao = Convert.ToString(reader["DicaConservacao"]),
                Status = Convert.ToBoolean(reader["Status"]),
                CaminhoImagem = Convert.ToString(reader["CaminhoImagem"])
            };
            return bebida;
        }

        public int ObterUltimoIdInserido()
        {
            int ID = 0;
            Sql.Clear();
            Sql.Append("SELECT MAX(ID) FROM BEBIDAS;");
            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    ID = reader.GetInt32(0);
                }
            }

            return ID;

        }

        public List<Ingrediente> GetIngredientes(int id)
        {
            List<Ingrediente> lista = new List<Ingrediente>();

            Sql.Append("SELECT * FROM INGREDIENTES WHERE BebidaId = " + id);

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    Ingrediente ingrediente = new Ingrediente
                    {
                        Descricao = Convert.ToString(reader["Descricao"])
                    };

                    lista.Add(ingrediente);
                }
            }

            return lista;
        }

        public void DeletarIngredientes(int id)
        {
            Sql.Clear();
            Sql.Append("DELETE FROM INGREDIENTES WHERE BebidaId = " + id + ";");
            DbContext.ExecuteQuery(Sql.ToString());
        }

        public void InicializarEstoque(int idBebida)
        {
            Sql.Clear();

            Sql.Append("INSERT INTO Estoque (IdBebida, Qtde) VALUES (" + idBebida + ", 0)");
            DbContext.ExecuteQuery(Sql.ToString());
        }

        public void EntradaEstoque(int IdBebida, int Qtde)
        {
            bool flgHaRegistro = false;
            Sql.Append("SELECT * FROM ESTOQUE WHERE IdBebida = " + IdBebida);

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    flgHaRegistro = true;
                }
            }

            Sql.Clear();

            if (flgHaRegistro)
            {//UPDATE, POIS JÁ EXISTE LÁ
                int QtdeAtual = 0;
                Sql.Append("SELECT Qtde FROM Estoque WHERE IdBebida =" + IdBebida + ";");//PEGA QTDE ATUAL
                using (var reader = DbContext.ExecuteReader(Sql.ToString()))
                {
                    if (reader.Read())
                    {
                        QtdeAtual = reader.GetInt32(0);
                    }
                }
                Sql.Clear();
                QtdeAtual += Qtde;//SOMA
                Sql.Append("UPDATE ESTOQUE SET QTDE = " + QtdeAtual + " WHERE IDBEBIDA = " + IdBebida + ";");//ATUALIZA
                DbContext.ExecuteQuery(Sql.ToString());
            }
            else
            {//NÃO EXISTE LÁ, ENTÃO INSERT
                Sql.Append("INSERT INTO ESTOQUE (IDBEBIDA, QTDE) VALUES (" + IdBebida + ", " + Qtde + ");");//ATUALIZA
                DbContext.ExecuteQuery(Sql.ToString());
            }
        }

        //TRUE = BAIXA COM SUCESSO / FALSE = INSUFICIENTE NO ESTOQUE PARA BAIXA ou ITEM NÃO EXISTE
        public bool BaixaEstoque(int IdBebida, int Qtde)
        {
            bool flgHaRegistro = false;
            Sql.Append("SELECT * FROM ESTOQUE WHERE IdBebida = " + IdBebida);

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    flgHaRegistro = true;
                }
            }

            if (!flgHaRegistro)//Item não existe no estoque?
                return false;

            Sql.Clear();

            int QtdeAtual = 0;
            Sql.Append("SELECT Qtde FROM Estoque WHERE IdBebida =" + IdBebida + ";");//PEGA QTDE ATUAL
            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                if (reader.Read())
                {
                    QtdeAtual = reader.GetInt32(0);
                }
            }

            Sql.Clear();

            if (QtdeAtual < Qtde)//Tem menos no estoque do que pode tirar?
                return false;

            QtdeAtual -= Qtde;//Subtrai
            Sql.Append("UPDATE ESTOQUE SET QTDE = " + QtdeAtual + " WHERE IDBEBIDA = " + IdBebida + ";");//ATUALIZA
            DbContext.ExecuteQuery(Sql.ToString());

            return true;
        }

        public List<Bebida> ConsultarComFiltro(int idGas = 0, int idTeor = 0, int idValor = 0, int idTipo = 0, string textoBusca = "")
        {
            try
            {
                List<Bebida> bebidas = new List<Bebida>();

                Sql.Append("SELECT * FROM BEBIDAS B INNER JOIN ESTOQUE E ON B.ID = E.IDBEBIDA WHERE B.STATUS = 1 AND E.QTDE > 0 ");

                //GASEIFICADA
                if (idGas == 1)//COM GÁS
                {
                    Sql.Append("AND B.GASEIFICADA = 1 ");
                }
                if (idGas == 2)//SEM GÁS
                {
                    Sql.Append("AND B.GASEIFICADA = 0 ");
                }

                //TEOR
                if (idTeor == 1)//MENOR QUE 10%
                {
                    Sql.Append("AND B.TEOR < 10 ");
                }
                if (idTeor == 2)//MAIOR/IGUAL 10%
                {
                    Sql.Append("AND B.TEOR >= 10 ");
                }

                //VALOR
                if (idValor == 1)//15 REAIS OU MENOS
                {
                    Sql.Append("AND B.VALOR <= 15 ");
                }
                if (idValor == 2)//ENTRE 15 E 50 REAIS
                {
                    Sql.Append("AND B.VALOR > 15 AND VALOR <= 50 ");
                }
                if (idValor == 3)//50 REAIS OU MAIS
                {
                    Sql.Append("AND B.VALOR > 50 ");
                }

                //TIPO DE BEBIDA
                if (idTipo != 0)
                {
                    Sql.Append("AND B.TIPOBEBIDA = " + idTipo + " ");
                }

                //TEXTO DE BUSCA
                if (!textoBusca.Equals(""))//TEXTO NÃO VAZIO
                {
                    Sql.Append("AND UPPER(LTRIM(RTRIM(B.NOME))) LIKE '%" + textoBusca.ToUpper().Trim() + "%'");
                }

                Sql.Append(";");

                using (var reader = DbContext.ExecuteReader(Sql.ToString()))
                {
                    while (reader.Read())
                    {
                        bebidas.Add(ObterEntidadeReader(reader));
                    }
                }

                return bebidas;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<SelectListItem> GetTiposBebida()
        {
            List<SelectListItem> Items = new List<SelectListItem>();

            Sql.Append("SELECT * FROM TIPOBEBIDA;");

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    SelectListItem item = new SelectListItem
                    {
                        Value = Convert.ToString(reader["Id"]),
                        Text = Convert.ToString(reader["Descricao"])
                    };
                    Items.Add(item);
                }
            }

            return Items;
        }

        public List<Bebida> ConsultarDashBebidas()
        {
            List<Bebida> bebidas = new List<Bebida>();

            Sql.Append("SELECT * FROM BEBIDAS B INNER JOIN ESTOQUE E ON B.ID = E.IDBEBIDA WHERE B.STATUS = 1 AND E.QTDE > 0;");

            using (var reader = DbContext.ExecuteReader(Sql.ToString()))
            {
                while (reader.Read())
                {
                    bebidas.Add(ObterEntidadeReader(reader));
                }
            }

            return bebidas;
        }
        
    }
}